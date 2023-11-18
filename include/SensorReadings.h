#ifndef SensorReadings_h
#define SensorReadings_h

#include <Arduino.h>

const int sensorPin = A0; // رقم الرجل الخاص بمستشعر الضوء

//يتم الوصول الية من اي ملف اخر
extern float lastSensorValue ;// لتخزين آخر قيمة تم قراءتها من الحساس
// تعريف الوظائف والمتغيرات العامة لقراءة الحساس
void readSensor();
void sendReadings();

#endif
