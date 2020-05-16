#ifndef RobkoTypes_h
#define RobkoTypes_h

#include <Arduino.h>

/*--------------------константы-------------------------------*/
const bool DEBUG = false;//отладочный режим работы прошивки

const long SERIAL_RATE = 115200; //??в один файл параметров?? Работает с максимальной скоростью 203000

//коэффициенты преобразования углов в микрошаги
const float S1 = -59800 / 90;
const float S2 = 59200 / 90;
const float S3 = -36100 / 90.7;
const float S5 = 500;
const float S5A2 = -(55000 / 90) * 0.04;
const float S5A3 = (55000 / 90) * 0.7;//0.7

//углы в градусах, в которых достигаются нули - срабатывают концевики
//Находятся так: 
//1. Выставить звенья в нули; 
//2. В 'RobkoTypes.h' выставить 'DEBUG = true'
//3. Включить питание платы управления Robko; 
//4. Подключить usb кабель к ПК и сразу же открыть монитор порта;
//5. Когда робот дойдет до концевого положения и вернется обратно в мониторе порта отобразятся пройденные углы  
//6. Вбить эти углы в 'RobkoTypes.h' Q1_ZERO, Q2_ZERO, Q3_ZERO
const float Q1_ZERO = 75.32;
const float Q2_ZERO = -23.24;
const float Q3_ZERO = 37.35;

//после обнуления на сколько разомкнуть схват
const float A5_ZERO = 20;

//расстояние между губками и объектом после разжатия
const float A5_UNGRIP_DIST = 6;

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
  COMMAND_GO_TO_ZEROS,
  COMMAND_MOVE_TO_ABSOLUTE_ANGLES_Q1Q2Q3
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
  bool isDoing;
  bool Complete;
  bool DoneRun;//--
  Command command;
} TaskType;

typedef struct
{
  bool isRunning;
} StatusSteppers;

#endif
