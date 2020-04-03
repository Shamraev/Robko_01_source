#ifndef RobkoTypes_h
#define RobkoTypes_h

#include <Arduino.h>

/*--------------------константы-------------------------------*/
const long SERIAL_RATE = 115200; //??в один файл параметров??
const int DATA_LENGTH = 12;
//коэффициенты преобразования углов в микрошаги
const float S1 = -59800 / 90;
const float S2 = 59200 / 90;
const float S3 = -36100 / 90.7;
const float S5A2 = -(55000 / 90) * 0.04;
const float S5A3 = (55000 / 90) * 0.7;

//стандартные скорости, ускореия для моторов
const float ACCELERATION = 1E+10;
const float MOTOR_SPEED = 1000 * 3; //100*16
const float MOTOR_SLOW_SPEED = MOTOR_SPEED / 2;
const float MOTOR_MAX_SPEED = MOTOR_SPEED;
const float MOTOR_SPEED_5 = MOTOR_SPEED * 0.6;
const float MOTOR_SLOW_SPEED_5 = MOTOR_SPEED_5 / 2;
const float MOTOR_MAX_SPEED_5 = MOTOR_SPEED_5;

//Константы в перечислениях
enum MSpeedState
{
  M_SPEED_STATE_SLOW,
  M_SPEED_STATE_NORMAL
};

enum Command
{
  COMMAND_NONE,
  COMMAND_GO_TO_START_POSITIONS
};

/*--------------------структуры-------------------------------*/
typedef struct
{
  byte pin;
  bool currentValue;
  bool prevValue;
} MLimitType;

typedef struct
{
  bool Received;
  //bool isDoing;
  bool Complete;
  bool DoneRun;
  Command command;
} TaskType;

typedef struct
{
  bool isRunning;
} StatusSteppers;

#endif
