#include <AccelStepper.h>
#include <MultiStepper.h>

// Define some steppers and the pins the will use
AccelStepper stepper1(1, 53, 51);
AccelStepper stepper2(1, 33, 35);
AccelStepper stepper3(1, 21, 20);
AccelStepper stepper4(1, 49, 47);
AccelStepper stepper5(1, 38, 39); //(.., step, dir)
AccelStepper stepper6(1, 36, 37);
MultiStepper steppers;

String myString;
float a1, a2, a3, a4, a5;
//float s1, s2, s3, s4, s5;
float  s1 = 59200 / 90;
float  s2 = -36100 / 90;
float  s3 = 59800 / 90;
float  s5A1 = -(55000 / 90)*0.04;
float  s5A2 = (55000 / 90)*0.7;  
float oldA1, oldA2;
const int M5_L = 45;
int currentValue, prevValue;
const int ledPin =  13;
const byte val = B1;
bool atTurget;
byte buf[3];
int n;
float accelr = 1E+10;
int MotorSpeed = 1000 * 3; //100*16
int MotorMaxSpeed = MotorSpeed;
int MotorSpeed5 =  MotorSpeed*0.6;
int MotorMaxSpeed5 = MotorSpeed5;
int dataLength = 12;
long positions[4];
void setup()
{
  oldA1 = a1;
  oldA2 = a2;
  Serial.begin(115200);

  stepper1.setMaxSpeed(MotorMaxSpeed);
  stepper2.setMaxSpeed(MotorMaxSpeed);
  stepper4.setMaxSpeed(MotorMaxSpeed);
  stepper5.setMaxSpeed(MotorMaxSpeed5);
  stepper1.setSpeed(MotorSpeed);
  stepper2.setSpeed(MotorSpeed);
  stepper4.setSpeed(MotorSpeed);
  stepper5.setSpeed(MotorSpeed5);

  steppers.addStepper(stepper1);
  steppers.addStepper(stepper2);
  steppers.addStepper(stepper4);
  steppers.addStepper(stepper5);

  pinMode(31, OUTPUT);
  digitalWrite(31, 1);
  delay(5000);
  digitalWrite(31, 0);

  pinMode(ledPin, OUTPUT);
  digitalWrite(ledPin, 0);
  pinMode(M5_L,  INPUT);

  // stepper5.moveTo(100 * 16);
  prevValue = 0;
  atTurget = false;
}

void loop()
{

  if ((dataLength == Serial.available()) and (not steppers.run())) {
    a1 = GetFloatNumber();
    a2 = GetFloatNumber();
    a3 = GetFloatNumber();
    atTurget = false;

    a5 = a5 +  s5A1*(a1 - oldA1) +  s5A2*(a2 - oldA2);
   // a5 = a5 + (a1 - oldA1);
    oldA1 = a1;
    oldA2 = a2;
    SendTaskToServos(a1 , a2, a3, a5 );
  }

  //  if ((not atTurget) and (not steppers.run()))
  //  {
  //    Serial.write(33);
  //    atTurget = true;
  //  }

}
float GetFloatNumber() {
  float number = 0;
  byte byteArray[4];

  for (int i = 0; i < 4; i++)
  {
    byteArray[i] = Serial.read();
  }
  number = *((float*)(byteArray));
  return number;
}
void SendTaskToServos(float a1, float a2, float a3, float a5) {
  positions[0] = round(a1 * s1);
  positions[1] = round(a2 * s2);
  positions[2] = round(a3 * s3);
  positions[3] = round(a5);
  steppers.moveTo(positions);
  steppers.runSpeedToPosition();
  Serial.write(33);
}
