#ifndef RobkoTypes_h
#define RobkoTypes_h
#include <Arduino.h>

//--------------------константы-------------------------------
const long SerialRate = 115200;//??в один файл параметров??
const int dataLength = 12;
//коэффициенты преобразования углов в микрошаги
const float  s1 = -59800 / 90;
const float  s2 = 59200 / 90;
const float  s3 = -36100 / 90.7;
const float  s5A2 = -(55000 / 90) * 0.04;
const float  s5A3 = (55000 / 90) * 0.7;

//стандартные скорости, ускореия для моторов
const float accelr = 1E+10;
const float MotorSpeed = 1000 * 3; //100*16
const float SlowMotorSpeed = MotorSpeed / 2;
const float MotorMaxSpeed = MotorSpeed;
const float MotorSpeed5 =  MotorSpeed * 0.6;
const float SlowMotorSpeed5 = MotorSpeed5 / 2;
const float MotorMaxSpeed5 = MotorSpeed5;

//-------------------перечесляемые типы------------------------
enum MotorSpeedState {slow, normal};

enum Command {none, goToStartPositions};

//--------------------структуры-------------------------------
typedef struct {
  byte  pin;
  bool  currentValue;
  bool  prevValue;
} M_Limit_type;

typedef struct {
  bool Received;
  //bool isDoing;
  bool Complete;
  bool DoneRun;
  Command  command;
} Task_type;

typedef struct {
  bool isRunning;
} StatusSteppers;

#endif
