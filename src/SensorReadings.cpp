#include "SensorReadings.h"

const int numReadings = 10; // عدد القراءات لحساب المتوسط

float lastSensorValue = 0;// لتخزين آخر قيمة تم قراءتها من الحساس

void readSensor() {
    float total = 0; // مجموع قراءات الحساس

    for (int i = 0; i < numReadings; i++) {
        total += analogRead(sensorPin); // جمع قراءات الحساس
        delay(10); // تأخير قصير لثبات القراءة
    }

    lastSensorValue = total / numReadings; // حساب المتوسط وتخزينه في lastSensorValue
}

float getAverageReading() {
    return lastSensorValue;
}
