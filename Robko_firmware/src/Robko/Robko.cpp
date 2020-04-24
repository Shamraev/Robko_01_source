#include "Robko.h"

// реализация методов
Robko::Robko(/*byte color = 5, byte bright = 30*/)
{                                                         // конструктор
  stepper1 = *(new AccelStepper(1, M1_STEP_P, M1_DIR_P)); //(.., step, dir)
  stepper2 = *(new AccelStepper(1, M2_STEP_P, M2_DIR_P));
  stepper3 = *(new AccelStepper(1, M3_STEP_P, M3_DIR_P));
  stepper4 = *(new AccelStepper(1, M4_STEP_P, M4_DIR_P));
  stepper5 = *(new AccelStepper(1, M5_STEP_P, M5_DIR_P));
  stepper6 = *(new AccelStepper(1, M6_STEP_P, M6_DIR_P));
}

void Robko::init()
{
  oldA2 = a2;
  oldA3 = a3;
  Serial.begin(SERIAL_RATE);

  stepper1.setAcceleration(10000); //--------------
  stepper2.setAcceleration(10000); //--------------
  stepper4.setAcceleration(10000); //--------------
  stepper5.setAcceleration(10000); //--------------

  stepper1.setMaxSpeed(MOTOR_MAX_SPEED);
  stepper2.setMaxSpeed(MOTOR_MAX_SPEED);
  stepper4.setMaxSpeed(MOTOR_MAX_SPEED);
  stepper5.setMaxSpeed(MOTOR_MAX_SPEED_5);

  setMotorsSpeed(M_SPEED_STATE_NORMAL);

  steppers.addStepper(stepper4); //q1
  steppers.addStepper(stepper1); //q2
  steppers.addStepper(stepper2); //q3
  steppers.addStepper(stepper5);
  initLimits();

  task_.Received = false;
  task_.Complete = false;
  task_.DoneRun = false;
  task_.command = COMMAND_NONE;

  statusSteppers_.isRunning = false;

  pinMode(NOT_ENABLE, OUTPUT); //?? сократить время
  digitalWrite(NOT_ENABLE, 1);
  delay(2000);
  digitalWrite(NOT_ENABLE, 0);

  //-----------только для проверки
  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, 0);
  //-----------
  Done = false;

  //команда идти в ноль//убрать отсюда//temp
  task_.command = COMMAND_GO_TO_START_POSITIONS; //??---
  task_.Received = true;
  task_.Complete = false;
  goToStartPositions(); //-------------
}

void Robko::reciveCommand()
{
  if ((DATA_LENGTH <= Serial.available()) and (not statusSteppers_.isRunning))
  {
    a1 = getFloatNumber();
    a2 = getFloatNumber();
    a3 = getFloatNumber();

    //task_.;
    a5 = 0;
    sendTaskToSteppers(a1, a2, a3, a5);
  }
}

void Robko::doTask()
{
  if ((DATA_LENGTH <= Serial.available()) and (not statusSteppers_.isRunning)) //if ((DATA_LENGTH <= Serial.available()))
  {
    a1 = getFloatNumber();
    a2 = getFloatNumber();
    a3 = getFloatNumber();

    //task_.;
    a5 = 0;
    sendTaskToSteppers(a1, a2, a3, a5);
  }

  doCommand();

  steppersRun();//здесь происходит шаг двигателей

  transmitReply();

}

void Robko::doCommand()
{
  if ((task_.command == COMMAND_GO_TO_START_POSITIONS) && (task_.Complete == true))
  {
    task_.command = COMMAND_GO_TO_ZEROS;
    task_.Complete = false;
    sendTaskToSteppers(-Q1_ZERO, -Q2_ZERO, -Q3_ZERO, 0);
  }
  else if ((task_.command == COMMAND_GO_TO_ZEROS) && (task_.Complete == true))
  {
    task_.command = COMMAND_NONE;
    task_.Complete = false;

    //обнулим шаговики, теперь мы в нулях
    stepper4.setCurrentPosition(0); //q1
    stepper1.setCurrentPosition(0); //q2
    stepper2.setCurrentPosition(0); //q3
    setMotorsSpeed(M_SPEED_STATE_NORMAL);
  }
}
void Robko::transmitReply()
{
  if (task_.command == COMMAND_GO_TO_START_POSITIONS)
  {
    if ((task_.Complete == true) && DEBUG)) //?? только для отладки - нахождения нулей
    {
      Serial.print("tmpQ1, tmpQ2, tmpQ3 in deg: ");
      Serial.print(tmpQ1 / S1);
      Serial.print(" ");
      Serial.print(tmpQ2 / S2);
      Serial.print(" ");
      Serial.print(tmpQ3 / S3);
      Serial.println("");
      Serial.println("-------------");
      Serial.print("tmpQ1, tmpQ2, tmpQ3 in steps: ");
      Serial.print(tmpQ1);
      Serial.print(" ");
      Serial.print(tmpQ2);
      Serial.print(" ");
      Serial.print(tmpQ3);
      Serial.println("");
      Serial.println("-------------");
    };
  }
  else
  {    
    bool steppersDistanceToGoIsZero = (stepper4.distanceToGo() == 0) && (stepper1.distanceToGo() == 0) && (stepper2.distanceToGo() == 0); //??остальные strppers
    task_.Complete = task_.Received && steppersDistanceToGoIsZero;

    if (task_.Complete)
    {
      task_.Received = false;
      Serial.write(33);
    }
  }
}

void Robko::initLimits()
{
  mLimit_[0] = (MLimitType){
      M1_LIMIT_P, false, false};
  mLimit_[1] = (MLimitType){
      M2_LIMIT_P, false, false};
  mLimit_[2] = (MLimitType){
      M3_LIMIT_P, false, false};
  mLimit_[3] = (MLimitType){
      M4_LIMIT_P, false, false};
  mLimit_[4] = (MLimitType){
      M5_LIMIT_P, false, false};
  mLimit_[5] = (MLimitType){
      M6_LIMIT_P, false, false};

  for (int i = 0; i < sizeof(mLimit_); i++)
  {
    if ((i != 2) || (i != 5))
    {
      pinMode(mLimit_[i].pin, INPUT);
    }
  }
}

float Robko::getFloatNumber()
{
  float number = 0;
  byte byteArray[4];

  for (int i = 0; i < sizeof(byteArray); i++)
  {
    byteArray[i] = Serial.read();
  }
  number = *((float *)(byteArray));
  return number;
}

void Robko::sendTaskToSteppers(float a1, float a2, float a3, float a5)
{
  task_.Received = true; //??для компа отправка сигнала
  //task_.isDoing = true;
  task_.Complete = false;
  task_.DoneRun = false;

  a5 = a5 + S5A2 * (a2 - oldA2) + S5A3 * (a3 - oldA3); //поправка для сжатия схвата//??для нуля слишком большие цифры
  oldA2 = a2;
  oldA3 = a3;

  positions_[0] = round(a1 * S1);
  positions_[1] = round(a2 * S2);
  positions_[2] = round(a3 * S3);
  positions_[3] = round(a5);
  steppers.moveTo(positions_);
  //steppers.runSpeedToPosition();
  //Serial.write(33);
}

void Robko::checkLimits()
{
  // for  (int i = 0; i < sizeof(mLimit_); i++)
  // {
  //   checkLimit(mLimit_[i]);
  // }
  checkLimit(0);
  checkLimit(1);
  checkLimit(3);
  checkLimit(4);
}

void Robko::checkLimit(byte i)
{
  mLimit_[i].currentValue = digitalRead(mLimit_[i].pin); //?? увеличить скорость считывания ??
  if (mLimit_[i].currentValue != mLimit_[i].prevValue)
  {
    // Что-то изменилось, здесь возможна зона неопределенности
    // Делаем задержку
    // delay(10);//можно убрать не для M5_LIMIT_P
    // А вот теперь спокойно считываем значение, считая, что нестабильность исчезла
    mLimit_[i].currentValue = digitalRead(mLimit_[i].pin);
    if (i == 4)
    {
      mLimit_[i].currentValue = not mLimit_[i].currentValue;
    }
    //отладка
    //if (mLimit_[i].currentValue == 1) digitalWrite(LED_PIN, 1);
    //else digitalWrite(LED_PIN, 0);
  }
}

void Robko::steppersRun()
{
  if (task_.command == COMMAND_GO_TO_START_POSITIONS)
  {
    steppersRunStartPos();
  }
  else if (task_.command == COMMAND_GO_TO_ZEROS)
  {
    steppersRunZeros();
  }
  else
  {
    steppersRunStandart();
  }
}

void Robko::steppersRunStartPos()
{
  if ((task_.command != COMMAND_GO_TO_START_POSITIONS) || task_.Complete) //task_.Complete??
    return;

  float a3;
  //??номера степперов сделать одинаковыми

  checkLimits();
  if (mLimit_[3].currentValue) //q1 сработал концевик пусть едет в анправлении q2
  {
    if (tmpQ1 == 0)                       //запись только один раз
      tmpQ1 = stepper4.currentPosition(); //отладка//------------
    stepper4.setCurrentPosition(0);
    sendTaskToSteppers(0, -300, 0, 0);
  }

  if (mLimit_[0].currentValue) //q2 сработал концевик
  {
    if (tmpQ2 == 0)
      tmpQ2 = stepper1.currentPosition(); //отладка//------------
    stepper1.setCurrentPosition(0);

    a3 = 0;
    if (not mLimit_[1].currentValue)
      a3 = 300; //q3 не сработал концевик

    oldA2 = 0;
    oldA3 = 0;
    sendTaskToSteppers(0, 0, a3, 0);
  }
  if (mLimit_[1].currentValue) //q3 сработал концевик
  {
    if (tmpQ3 == 0)
      tmpQ3 = stepper2.currentPosition(); //отладка//------------
    stepper2.setCurrentPosition(0);
    if (not mLimit_[0].currentValue) //q2 не сработал концевик
    {
      a3 = -15;
      stepper1.setCurrentPosition(0);
      stepper5.setCurrentPosition(0);

      positions_[0] = 0;
      positions_[1] = 0;
      positions_[2] = a3 * S3;
      positions_[3] = 0;
      steppers.moveTo(positions_);

      steppers.runSpeedToPosition(); //блок, пока не выполнит ничего не произойдет

      positions_[0] = 0;
      positions_[1] = -300 * S2;
      positions_[2] = 0;
      positions_[3] = 0;
      steppers.moveTo(positions_);
    }
    else
    {
      a3 = 0;
      oldA2 = 0;
      oldA3 = a3;
      sendTaskToSteppers(0, 0, a3, 0);
    }
  }

  bool mLimitsIsTrue = (mLimit_[0].currentValue == true) and (mLimit_[1].currentValue == true) and (mLimit_[3].currentValue == true);
  if (mLimitsIsTrue)
    task_.Complete = true; //??
  else
    statusSteppers_.isRunning = steppers.run(); //Run steppers
}

void Robko::steppersRunZeros()
{
  if ((task_.command != COMMAND_GO_TO_ZEROS) || task_.Complete) //task_.Complete??
    return;

  statusSteppers_.isRunning = steppers.run(); //Run steppers
}

void Robko::steppersRunStandart()
{
  checkLimits();
  bool mLimitsIsFalse = (mLimit_[0].currentValue == false) and (mLimit_[1].currentValue == false) and (mLimit_[3].currentValue == false);

  if (mLimitsIsFalse)
    statusSteppers_.isRunning = steppers.run(); //Run steppers
}

void Robko::goToStartPositions()
{
  setMotorsSpeed(M_SPEED_STATE_SLOW);
  sendTaskToSteppers(300, 0, 0, 0); //--------------(300, -300, 300, 0)  
}

void Robko::setMotorsSpeed(float motorSpeed_[])
{
  stepper1.setSpeed(motorSpeed_[0]);
  stepper2.setSpeed(motorSpeed_[1]);
  stepper4.setSpeed(motorSpeed_[3]);
  stepper5.setSpeed(motorSpeed_[4]);
}

void Robko::setMotorsSpeed(enum MSpeedState mSpeedState)
{
  float mSpeed, mSpeed5;
  switch (mSpeedState)
  {
  case M_SPEED_STATE_SLOW:
    mSpeed = MOTOR_SLOW_SPEED;
    mSpeed5 = MOTOR_SLOW_SPEED_5;
    break;
  default:
    mSpeed = MOTOR_SPEED;
    mSpeed5 = MOTOR_SPEED_5;
  }

  motorSpeed_[0] = mSpeed;
  motorSpeed_[1] = mSpeed;
  motorSpeed_[3] = mSpeed;
  motorSpeed_[4] = mSpeed5;
  setMotorsSpeed(motorSpeed_);
}
