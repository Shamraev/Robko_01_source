#ifndef RobkoLib_h
#define RobkoLib_h
#include <Arduino.h>
#include "../AccelStepper/AccelStepper.h"//??
#include "../AccelStepper/MultiStepper.h"//??
#include "RobkoPinOut.h"
#include "RobkoTypes.h"

class Robko {
public:
	Robko();
	void init();
	void doTask();//clear and focussed
protected:
	void InitLimits();
	float GetFloatNumber();
	void SendTaskToSteppers(float a1, float a2, float a3, float a5);
	void CheckLimits();
	void CheckLimit(byte i);
	void steppersRun();
	void GoToStartPositions();//??
	void SetMotorsSpeed(float motorSpeed[]);
	void SetMotorsSpeed(enum MotorSpeedState mSpeedState);
protected:
	AccelStepper stepper1;//(1, M1_STEP_P, M1_DIR_P);//(.., step, dir)
	AccelStepper stepper2;//(1, M2_STEP_P, M2_DIR_P);
	AccelStepper stepper3;//(1, M3_STEP_P, M3_DIR_P);
	AccelStepper stepper4;//(1, M4_STEP_P, M4_DIR_P);
	AccelStepper stepper5;//(1, M5_STEP_P, M5_DIR_P);
	AccelStepper stepper6;//(1, M6_STEP_P, M6_DIR_P);
	MultiStepper steppers;
private:
	M_Limit_type M_LIMIT[6];
	Task_type task;
	StatusSteppers statusSteppers;

	float motorSpeed[4];
	float a1, a2, a3, a4, a5;
	float oldA2, oldA3;

	long positions[4];
	bool CanRun;
	long tmpQ1, tmpQ2, tmpQ3;
	bool Done;
};

#endif
