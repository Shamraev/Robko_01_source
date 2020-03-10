#include <AccelStepper.h>
#include <MultiStepper.h>

// Define some steppers and the pins the will use
AccelStepper stepper1(1, 53, 51);
AccelStepper stepper2(1, 33, 35);
AccelStepper stepper3(1, 21, 20);
AccelStepper stepper4(1, 49, 47);
AccelStepper stepper5(1, 38, 39); //(.., step, dir)
AccelStepper stepper6(1, 36, 37);
MultiStepper steppers;

typedef struct {
  byte  pin;
  bool  currentValue;
  bool  prevValue;
} M_Limit_type;

M_Limit_type M_LIMIT[6];

enum MotorSpeedState {slow, normal};

enum Command {none, goToStartPositions};

typedef struct {
  bool Received;
  bool isDoing;
  bool Complete;
  bool DoneRun;
  Command  command;
} Task_type;

Task_type task;

typedef struct {
  bool isRunning;
} StatusSteppers;

StatusSteppers statusSteppers;
float motorSpeed[4];

String myString;
float a1, a2, a3, a4, a5;
float  s1 = -59800 / 90;
float  s2 = 59200 / 90;
float  s3 = -36100 / 90.7;
float  s5A2 = -(55000 / 90) * 0.04;
float  s5A3 = (55000 / 90) * 0.7;
float oldA2, oldA3;

const byte M1_LIMIT_P = 44;
const byte M2_LIMIT_P = 42;
const byte M3_LIMIT_P = 46;
const byte M4_LIMIT_P = 43;
const byte M5_LIMIT_P = 45;
const byte M6_LIMIT_P = 41;

const byte notEn = 31;
const int ledPin =  13;//светодиод на ардуино
const byte val = B1;
byte buf[3];
int n;
float accelr = 1E+10;
float MotorSpeed = 1000 * 3; //100*16
float SlowMotorSpeed = MotorSpeed / 2;
float MotorMaxSpeed = MotorSpeed;
float MotorSpeed5 =  MotorSpeed * 0.6;
float SlowMotorSpeed5 = MotorSpeed5 / 2;
float MotorMaxSpeed5 = MotorSpeed5;
int dataLength = 12;
long positions[4];
bool CanRun;
long tmpQ1, tmpQ2, tmpQ3;
bool Done;

void setup()
{
  oldA2 = a2;
  oldA3 = a3;
  Serial.begin(115200);
  //Serial.begin(9600);


  stepper1.setMaxSpeed(MotorMaxSpeed);
  stepper2.setMaxSpeed(MotorMaxSpeed);
  stepper4.setMaxSpeed(MotorMaxSpeed);
  stepper5.setMaxSpeed(MotorMaxSpeed5);
  stepper1.setSpeed(MotorSpeed);
  stepper2.setSpeed(MotorSpeed);
  stepper4.setSpeed(MotorSpeed);
  stepper5.setSpeed(MotorSpeed5);

  //stepper2.setAcceleration(accelr);

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

void loop()
{


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
float GetFloatNumber() {
  float number = 0;
  byte byteArray[4];

  for (int i = 0; i < sizeof(byteArray); i++)
  {
    byteArray[i] = Serial.read();
  }
  number = *((float*)(byteArray));
  return number;
}
void SendTaskToSteppers(float a1, float a2, float a3, float a5) {
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
void InitLimits() {
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
void CheckLimits() {
  // for  (int i = 0; i < sizeof(M_LIMIT); i++)
  // {
  //   CheckLimit(M_LIMIT[i]);
  // }
  CheckLimit(0);
  CheckLimit(1);
  CheckLimit(3);
  CheckLimit(4);
}
void CheckLimit(byte i) {
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

void steppersRun() {
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
//void CalibrateStartPosnts(){
//
//  GoToStartPositions();
//}
void GoToStartPositions() {
  task.command = Command{goToStartPositions};
  SetMotorsSpeed(slow);
  SendTaskToSteppers(300, -300, 300, 0); //--------------
  //SendTaskToSteppers(100, 0, 0, 0); //--------------
}
void SetMotorsSpeed(float motorSpeed[]) {
  stepper1.setSpeed(motorSpeed[0]);
  stepper2.setSpeed(motorSpeed[1]);
  stepper4.setSpeed(motorSpeed[3]);
  stepper5.setSpeed(motorSpeed[4]);
}
void SetMotorsSpeed(enum MotorSpeedState mSpeedState) {
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

