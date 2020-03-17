#include <RobkoLib.h>
#include <RobkoPinOut.h>
#include <Arduino.h>
#include <AccelStepper.h>
#include <MultiStepper.h>

// реализация методов
Robko::Robko(/*byte color = 5, byte bright = 30*/) { // конструктор
	AccelStepper stepper1(1, M1_STEP_P, M1_DIR_P);//(.., step, dir)
	AccelStepper stepper2(1, M2_STEP_P, M2_DIR_P);
	AccelStepper stepper3(1, M3_STEP_P, M3_DIR_P);
	AccelStepper stepper4(1, M4_STEP_P, M4_DIR_P);
	AccelStepper stepper5(1, M5_STEP_P, M5_DIR_P); 
	AccelStepper stepper6(1, M6_STEP_P, M6_DIR_P);
	MultiStepper steppers;
}

void Robko::init() {

}
