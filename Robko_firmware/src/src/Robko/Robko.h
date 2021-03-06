#ifndef Robko_h
#define Robko_h

#include <Arduino.h>

#include "../AccelStepper/AccelStepper.h" //??
#include "../AccelStepper/MultiStepper.h" //??

#include "RobkoPinOut.h"
#include "RobkoTypes.h"
#include "FrameFormer.h"
#include "BusController.h"

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
	void move_To_Absolute_Angles_q1q2q3(float *q);
	void sendAngelsFromStartToEndSensor();
	void initLimits();
	void ResetGripper();
	float getFloatNumber();
	void sendTaskToSteppers(float a1, float a2, float a3, float a5);
	void checkLimits();
	void checkLimit(byte i);
	void steppersRun();
	void steppersRunStartPos();
	void steppersRunZeros();
	void steppersRunStandart();
	void gripperResetAndOpenTo(float a5);
	void gripperFindZeroAndOpenTo(float a5);
	void gripperOpenTo(float a5);
	void gripperGrip();
	void gripperUngrip();
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
	BusController busController_;
	MLimitType mLimit_[6];
	TaskType task_;
	StatusSteppers statusSteppers_;

	float motorSpeed_[4];
	float a5_offset_a2_a3_;

	float oldA2, oldA3; //----------

	long positions_[4];
	long tmpQ1, tmpQ2, tmpQ3; //------
	bool Done;				  //---------------------
};

#endif
