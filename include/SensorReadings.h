#ifndef SensorReadings_h
#define SensorReadings_h

#include <Arduino.h>

const int sensorPin = A0; // رقم الرجل الخاص بمستشعر الضوء

// تعريف الوظائف والمتغيرات العامة لقراءة الحساس
void readSensor();
void sendReadings();

#endif
