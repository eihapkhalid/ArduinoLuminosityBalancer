#ifndef LEDControl_h
#define LEDControl_h

#include <Arduino.h>
const int ledPin = 9; // رقم الرجل الخاص بـ LED
// تعريف الوظائف والمتغيرات العامة للتحكم في الـ LED
void controlLED(float sensorValue);

#endif
