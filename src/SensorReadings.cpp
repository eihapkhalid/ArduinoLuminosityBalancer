#include "SensorReadings.h"

const int numReadings = 600; // عدد القراءات لتخزينها لمدة 10 دقائق (فرضًا قراءة كل ثانية)
float readings[numReadings]; // مصفوفة لتخزين القراءات
int readingIndex = 0; // مؤشر للمصفوفة

void readSensor() {
  if (readingIndex < numReadings) {
    readings[readingIndex] = analogRead(sensorPin); // تخزين القراءة في المصفوفة
    readingIndex++;
  }
}

void sendReadings() {
  for (int i = 0; i < numReadings; i++) {
    Serial.println(readings[i]); // إرسال القراءات عبر الاتصال السيريال
  }
  readingIndex = 0; // إعادة تعيين المؤشر لبدء تخزين جديد
}