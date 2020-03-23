#include <RobkoLib.h>
#include <RobkoPinOut.h>
#include <RobkoConsts.h>
#include <Arduino.h>
#include <AccelStepper.h>
#include <MultiStepper.h>

// реализация методов
Robko::Robko(/*byte color = 5, byte bright = 30*/) { // конструктор
	AccelStepper stepper1(1, M1_STEP_P, M1_DIR_P);//(.., step, dir)
	AccelStepper stepper2(1, M2_STEP_P, M2_DIR_P);
	AccelStepper stepper3(1, M3_STEP_P, M3_DIR_P);
	AccelStepper stepper4(1, M4_STEP_P, M4_DIR_P);
	AccelStepper stepper5(1, M5_STEP_P, M5_DIR_P); 
	AccelStepper stepper6(1, M6_STEP_P, M6_DIR_P);
	MultiStepper steppers;
	
	
}

void Robko::init() {
	oldA2 = a2;
	oldA3 = a3;
	Serial.begin(SerialRate);

	stepper1.setMaxSpeed(MotorMaxSpeed);
	stepper2.setMaxSpeed(MotorMaxSpeed);
	stepper4.setMaxSpeed(MotorMaxSpeed);
	stepper5.setMaxSpeed(MotorMaxSpeed5);
	stepper1.setSpeed(MotorSpeed);
	stepper2.setSpeed(MotorSpeed);
	stepper4.setSpeed(MotorSpeed);
	stepper5.setSpeed(MotorSpeed5);

	steppers.addStepper(stepper4);//q1
	steppers.addStepper(stepper1);//q2
	steppers.addStepper(stepper2);//q3
	steppers.addStepper(stepper5);
	InitLimits();
	
	task = (Task_type) {
    false, false, false, Command{none}
  };
  statusSteppers = {false};

	pinMode(notEn, OUTPUT);//?? сократить время
	digitalWrite(notEn, 1);
	delay(2000);
	digitalWrite(notEn, 0);

	//-----------только для проверки
	pinMode(ledPin, OUTPUT);
	digitalWrite(ledPin, 0);
	//-----------
	Done = false;
	GoToStartPositions();//--
}

void Robko::doTask() {
	 if ((dataLength <= Serial.available()) and (not statusSteppers.isRunning)) {
    a1 = GetFloatNumber();
    a2 = GetFloatNumber();
    a3 = GetFloatNumber();

    SendTaskToSteppers(a1 , a2, a3, a5);
  }

  CheckLimits();
  if (!task.DoneRun) steppersRun();
  if ((not stepper1.isRunning()) and (not stepper2.isRunning()) and (not stepper4.isRunning())) {
    Serial.println("tmpQ1, tmpQ2, tmpQ3: ");//----
    Serial.print(tmpQ1);
    Serial.print(tmpQ2);
    Serial.print(tmpQ3);
  }
  //идти в нулевое положение


  if (task.Received and (not statusSteppers.isRunning)) {
    task = (Task_type) {
      false, false, true, task.command                        //received == false??
    };
  }
  if (task.Complete and (not task.DoneRun)) {
    if (task.command == Command{goToStartPositions}) {
      task.command = Command{none};
      SetMotorsSpeed(normal);
      //Serial.print(tmpQ1);//----
      //SendTaskToSteppers(a1 , a2, a3, a5);
    }
    Serial.write(33);

    task.DoneRun = true;
    Done = true;//-----
  }

}

void Robko::InitLimits() {
	M_LIMIT[0] = (M_Limit_type) {
	M1_LIMIT_P, false, false
	};
	M_LIMIT[1] = (M_Limit_type) {
	M2_LIMIT_P, false, false
	};
	M_LIMIT[2] = (M_Limit_type) {
	M3_LIMIT_P, false, false
	};
	M_LIMIT[3] = (M_Limit_type) {
	M4_LIMIT_P, false, false
	};
	M_LIMIT[4] = (M_Limit_type) {
	M5_LIMIT_P, false, false
	};
	M_LIMIT[5] = (M_Limit_type) {
	M6_LIMIT_P, false, false
	};
	pinMode(M_LIMIT[0].pin,  INPUT);
	pinMode(M_LIMIT[1].pin,  INPUT);
	// pinMode(M_LIMIT[2].pin,  INPUT);
	pinMode(M_LIMIT[3].pin,  INPUT);
	pinMode(M_LIMIT[4].pin,  INPUT);
	//pinMode(M_LIMIT[5].pin,  INPUT);
}

float Robko::GetFloatNumber() {
  float number = 0;
  byte byteArray[4];

  for (int i = 0; i < sizeof(byteArray); i++)
  {
    byteArray[i] = Serial.read();
  }
  number = *((float*)(byteArray));
  return number;
}

void Robko::SendTaskToSteppers(float a1, float a2, float a3, float a5) {
  task.Received = true;//??для компа отправка сигнала
  task.isDoing = true;
  task.Complete = false;
  task.DoneRun = false;

  a5 = a5 +  s5A2 * (a2 - oldA2) +  s5A3 * (a3 - oldA3);//поправка для сжатия схвата//??для нуля слишком большие цифры
  oldA2 = a2;
  oldA3 = a3;

  positions[0] = round(a1 * s1);
  positions[1] = round(a2 * s2);
  positions[2] = round(a3 * s3);
  positions[3] = round(a5);
  steppers.moveTo(positions);
  //steppers.runSpeedToPosition();
  //Serial.write(33);
}

void Robko::CheckLimits() {
  // for  (int i = 0; i < sizeof(M_LIMIT); i++)
  // {
  //   CheckLimit(M_LIMIT[i]);
  // }
  CheckLimit(0);
  CheckLimit(1);
  CheckLimit(3);
  CheckLimit(4);
}

void Robko::CheckLimit(byte i) {
  M_LIMIT[i].currentValue = digitalRead(M_LIMIT[i].pin);//?? увеличить скорость считывания ??
  if (M_LIMIT[i].currentValue != M_LIMIT[i].prevValue) {
    // Что-то изменилось, здесь возможна зона неопределенности
    // Делаем задержку
    // delay(10);//можно убрать не для M5_LIMIT_P
    // А вот теперь спокойно считываем значение, считая, что нестабильность исчезла
    M_LIMIT[i].currentValue = digitalRead(M_LIMIT[i].pin);
    if (i == 4) {
      M_LIMIT[i].currentValue = not M_LIMIT[i].currentValue;
    }
    //отладка
    //if (M_LIMIT[i].currentValue == 1) digitalWrite(ledPin, 1);
    //else digitalWrite(ledPin, 0);
  }
}

void Robko::steppersRun() {
  CanRun = false;
  float a3 = 0;
  if ((M_LIMIT[0].currentValue == false) and (M_LIMIT[1].currentValue == false) and (M_LIMIT[3].currentValue == false)) {
    CanRun = true;
  }
  if (task.command == Command{goToStartPositions}) { //??номера степперов сделать одинаковыми
    CanRun = true;

    if (M_LIMIT[3].currentValue) {
      tmpQ1 = stepper4.currentPosition();//отладка//------------
      stepper4.setCurrentPosition(0);
      SendTaskToSteppers(0, positions[1], positions[2], 0);
    }
    if (M_LIMIT[0].currentValue) {
      tmpQ2 = stepper1.currentPosition();//отладка//------------
      stepper1.setCurrentPosition(0);
      SendTaskToSteppers(positions[0], 0 , positions[2], 0);
    }
    if (M_LIMIT[1].currentValue) {
      tmpQ3 = stepper2.currentPosition();//отладка//------------
      stepper2.setCurrentPosition(0);
      if (not M_LIMIT[0].currentValue) {
        a3 = -5;
      }
      else a3 = 0;
      stepper2.moveTo(a3);
      //SendTaskToSteppers(positions[0], positions[1], a3, 0);
    }

    //CanRun = not(M_LIMIT[0].currentValue and M_LIMIT[1].currentValue and M_LIMIT[3].currentValue);//??
  }

  if (CanRun) statusSteppers.isRunning = steppers.run();
}

void Robko::GoToStartPositions() {
  task.command = Command{goToStartPositions};
  SetMotorsSpeed(slow);
  SendTaskToSteppers(300, -300, 300, 0); //--------------
  //SendTaskToSteppers(100, 0, 0, 0); //--------------
}

void Robko::SetMotorsSpeed(float motorSpeed[]) {
  stepper1.setSpeed(motorSpeed[0]);
  stepper2.setSpeed(motorSpeed[1]);
  stepper4.setSpeed(motorSpeed[3]);
  stepper5.setSpeed(motorSpeed[4]);
}

void Robko::SetMotorsSpeed(enum MotorSpeedState mSpeedState) {
  float mSpeed, mSpeed5;
  switch (mSpeedState) {
    case slow:
      mSpeed = SlowMotorSpeed;
      mSpeed5 =  SlowMotorSpeed5;
      break;
    default:
      mSpeed = MotorSpeed;
      mSpeed5 =  MotorSpeed5;
  }

  motorSpeed[0] = mSpeed;
  motorSpeed[1] = mSpeed;
  motorSpeed[3] = mSpeed;
  motorSpeed[4] = mSpeed5;
  SetMotorsSpeed(motorSpeed);
}