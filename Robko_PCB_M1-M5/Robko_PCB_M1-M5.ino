#include <AccelStepper.h>

// Define some steppers and the pins the will use
AccelStepper stepper1(1, 53, 51);
AccelStepper stepper2(1, 33, 35);
AccelStepper stepper3(1, 21, 20);
AccelStepper stepper4(1, 49, 47);
AccelStepper stepper5(1, 38, 39); //(.., step, dir)
AccelStepper stepper6(1, 36, 37);
String myString;
float a1, a2, a3, a4, a5;
float s1, s2, s3, s4, s5;
const int M5_L = 45;
int currentValue, prevValue;
const int ledPin =  13;
void setup()
{
  s1 = 59200 / 90;
  s2 = -36100 / 90;
  s3 = 59800 / 90;
  s5 = 1;
  Serial.begin(9600);
  stepper1.setMaxSpeed(100 * 16);
  stepper1.setAcceleration(100 * 16);
  // stepper1.moveTo(-200*16);

  stepper2.setMaxSpeed(100 * 16);
  stepper2.setAcceleration(100 * 16);
  //stepper2.moveTo(-200*16);

  //    stepper3.setMaxSpeed(5000*16);
  //  stepper3.setAcceleration(100*16);
  //
  //
  stepper4.setMaxSpeed(100 * 16);
  stepper4.setAcceleration(100 * 16);
  //  stepper4.moveTo(-200*16);
  //
      stepper5.setMaxSpeed(100 * 16);
    stepper5.setAcceleration(100*16);
//    stepper5.moveTo(-200*16);
  //
  //      stepper6.setMaxSpeed(5000*16);
  //  stepper6.setAcceleration(100*16);
  //    stepper3.moveTo(200*16);
  //  stepper6.moveTo(-200*16);

  pinMode(31, OUTPUT);
  digitalWrite(31, 1);
  delay(5000);
  digitalWrite(31, 0);

  pinMode(ledPin, OUTPUT);
  digitalWrite(ledPin, 0);
  pinMode(M5_L,  INPUT);
  Serial.write(stepper5.currentPosition()+"_0");
 stepper5.moveTo(100*16);
  prevValue = 0;
}

void loop()
{

  if (Serial.available()) {
    myString = Serial.readString();
    int commaIndex = myString.indexOf(',');
    int secondCommaIndex = myString.indexOf(',', commaIndex + 1);
    int firdCommaIndex = myString.indexOf(',', commaIndex + 2);
    int forthCommaIndex = myString.indexOf(',', commaIndex + 3);
   // int fifthCommaIndex = myString.indexOf(',', commaIndex + 4);

    a1 = myString.substring(0, commaIndex).toFloat();
    a2 = myString.substring(commaIndex + 1, secondCommaIndex).toFloat();
    a3 = myString.substring(secondCommaIndex, secondCommaIndex + 1).toFloat(); // To the end of the string
    a5 = myString.substring(forthCommaIndex, forthCommaIndex + 1).toFloat();
    SendTaskToServos(a1, a2, a3, a5);
  }


  currentValue = digitalRead(M5_L);
  if (currentValue != prevValue) {
    // Что-то изменилось, здесь возможна зона неопределенности
    // Делаем задержку
    delay(10);
    // А вот теперь спокойно считываем значение, считая, что нестабильность исчезла
    currentValue = digitalRead(M5_L);
    if (currentValue == 0) {
      // turn LED on:
    //  digitalWrite( ledPin, 0);
    //  Serial.write(stepper5.currentPosition()+"_1");
    } else {
      // turn LED off:
     // digitalWrite( ledPin, 1);
     // Serial.write(stepper5.currentPosition()+"_1");
    }
  }
  prevValue = currentValue;


  stepper1.run(); 
  stepper2.run();
  //// stepper3.run();
  stepper4.run();
       stepper5.run();
  ////       stepper6.run();

}
void SendTaskToServos(float a1, float a2, float a3, float a5) {
  stepper1.moveTo(a1 * s1);
  stepper2.moveTo(a2 * s2);
  stepper4.moveTo(a3 * s3);
//  stepper5.moveTo(a5 * s5);
}
