// MultiStepper.pde
// -*- mode: C++ -*-
//
// Shows how to multiple simultaneous steppers
// Runs one stepper forwards and backwards, accelerating and decelerating
// at the limits. Runs other steppers at the same time
//
// Copyright (C) 2009 Mike McCauley
// $Id: MultiStepper.pde,v 1.1 2011/01/05 01:51:01 mikem Exp mikem $

#include <AccelStepper.h>

// Define some steppers and the pins the will use
AccelStepper stepper1(1, 53, 51); 
AccelStepper stepper2(1, 33, 35); 
AccelStepper stepper3(1, 21, 20); 
AccelStepper stepper4(1, 49, 47); 
AccelStepper stepper5(1, 38, 39); //(.., step, dir)
AccelStepper stepper6(1, 36, 37); 
const int buttonPin = 46;     // the number of the pushbutton pin
const int ledPin =  13;      // the number of the LED pin
int buttonState = 1;         // variable for reading the pushbutton status
int currentValue, prevValue;
void setup()
{
  
  // initialize the LED pin as an output:
  pinMode(ledPin, OUTPUT);
  digitalWrite(ledPin, 0);
  // initialize the pushbutton pin as an input:
  pinMode(buttonPin,  INPUT);
  stepper1.setMaxSpeed(5000*16);
  stepper1.setAcceleration(100*16);
  stepper1.moveTo(-200*16);

    stepper2.setMaxSpeed(5000);
  stepper2.setAcceleration(100);
  stepper2.moveTo(-200);

    stepper3.setMaxSpeed(5000*16);
  stepper3.setAcceleration(100*16);


    stepper4.setMaxSpeed(5000*16);
  stepper4.setAcceleration(100*16);
  stepper4.moveTo(-200*16);

    stepper5.setMaxSpeed(5000*16);
  stepper5.setAcceleration(100*16);
  stepper5.moveTo(-200*16);

      stepper6.setMaxSpeed(5000*16);
  stepper6.setAcceleration(100*16);
    stepper3.moveTo(200*16);
  stepper6.moveTo(-200*16);
 
}

void loop()
{
  // Change direction at the limits
  if (stepper1.distanceToGo() == 0)
    stepper1.moveTo(-stepper1.currentPosition());
      if (stepper2.distanceToGo() == 0)
    stepper2.moveTo(-stepper2.currentPosition());
      if (stepper3.distanceToGo() == 0)
    stepper3.moveTo(-stepper3.currentPosition());
          if (stepper6.distanceToGo() == 0)
    stepper6.moveTo(-stepper6.currentPosition());
      if (stepper4.distanceToGo() == 0)
    stepper4.moveTo(-stepper4.currentPosition());
      if (stepper5.distanceToGo() == 0)
    stepper5.moveTo(-stepper5.currentPosition());

//  stepper1.run();
//   stepper2.run();
    stepper3.run();
//     stepper4.run();
//      stepper5.run();
       stepper6.run();
 currentValue = digitalRead(buttonPin);
  if (currentValue != prevValue) {
    // Что-то изменилось, здесь возможна зона неопределенности
    // Делаем задержку
    delay(10);
    // А вот теперь спокойно считываем значение, считая, что нестабильность исчезла
    currentValue = digitalRead(buttonPin);
     if (currentValue == 0) {
    // turn LED on:
    digitalWrite( ledPin, HIGH);
  } else {
    // turn LED off:
    digitalWrite( ledPin, LOW);
  }
  }
  prevValue = currentValue; 

}
