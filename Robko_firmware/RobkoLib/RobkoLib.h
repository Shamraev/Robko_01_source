#ifndef RobkoLib_h
#define RobkoLib_h
#include <Arduino.h>

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
