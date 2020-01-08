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
float s1, s2, s3, s4, s5;
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
int dataLength = 12;
long positions[3];
void setup()
{
  s1 = 59200 / 90;
  s2 = -36100 / 90;
  s3 = 59800 / 90;
  s5 = 1;
  Serial.begin(115200);

  stepper1.setMaxSpeed(MotorMaxSpeed);
  stepper2.setMaxSpeed(MotorMaxSpeed);
  stepper4.setMaxSpeed(MotorMaxSpeed);
  stepper1.setSpeed(MotorSpeed);
  stepper2.setSpeed(MotorSpeed);
  stepper4.setSpeed(MotorSpeed);

  steppers.addStepper(stepper1);
  steppers.addStepper(stepper2);
  steppers.addStepper(stepper4);

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
  steppers.moveTo(positions);
  steppers.runSpeedToPosition();
  Serial.write(33);
}
