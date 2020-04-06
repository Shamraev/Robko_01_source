#ifndef Robko_h
#define Robko_h

#include <Arduino.h>

#include "../AccelStepper/AccelStepper.h" //??
#include "../AccelStepper/MultiStepper.h" //??

#include "RobkoPinOut.h"
#include "RobkoTypes.h"

class Robko
{
public:
	Robko();
	void init();
	void reciveCommand();
	void doTask(); //clear and focussed
	void doCommand();
	void transmitReply();
protected:
	void initLimits();
	float getFloatNumber();
	void sendTaskToSteppers(float a1, float a2, float a3, float a5);
	void checkLimits();
	void checkLimit(byte i);
	void steppersRun();
	void steppersRunStartPos();
	void steppersRunZeros();	
	void steppersRunStandart();
	void goToStartPositions(); //??
	void setMotorsSpeed(float motorSpeed_[]);
	void setMotorsSpeed(enum MSpeedState mSpeedState);

protected:
	AccelStepper stepper1; //(1, M1_STEP_P, M1_DIR_P);//(.., step, dir)
	AccelStepper stepper2; //(1, M2_STEP_P, M2_DIR_P);
	AccelStepper stepper3; //(1, M3_STEP_P, M3_DIR_P);
	AccelStepper stepper4; //(1, M4_STEP_P, M4_DIR_P);
	AccelStepper stepper5; //(1, M5_STEP_P, M5_DIR_P);
	AccelStepper stepper6; //(1, M6_STEP_P, M6_DIR_P);
	MultiStepper steppers;

private:
	MLimitType mLimit_[6];
	TaskType task_;
	StatusSteppers statusSteppers_;

	float motorSpeed_[4];
	float a1, a2, a3, a4, a5; //----
	float oldA2, oldA3;		  //----------

	long positions_[4];
	long tmpQ1, tmpQ2, tmpQ3; //------
	bool Done;				  //---------------------
};

#endif
