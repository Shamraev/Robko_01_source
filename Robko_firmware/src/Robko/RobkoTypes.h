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

//углы в градусах, в которых достигаются нули - срабатывают концевики
// tmpQ1, tmpQ2, tmpQ3 in steps: -49415 -14631 -14755
const float Q1_ZERO = 74.42;
const float Q2_ZERO = -22.27;
const float Q3_ZERO = 37.07;

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
  COMMAND_GO_TO_START_POSITIONS,
  COMMAND_GO_TO_ZEROS
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
