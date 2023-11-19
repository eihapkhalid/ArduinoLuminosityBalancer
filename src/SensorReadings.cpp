#include "SensorReadings.h"

const int numReadings = 60; // عدد القراءات لتخزينها لمدة 1 دقائق (فرضًا قراءة كل ثانية)
float readings[numReadings]; // مصفوفة لتخزين القراءات

// استدعاء الدالة كل ثانية
void readSensor() {
  if (readingIndex < numReadings) {
    readings[readingIndex] = analogRead(sensorPin); // تخزين القراءة في المصفوفة
  }
}

//استدعاء الدالة كل دقيقة
void sendReadings() {
  for (int i = 0; i < numReadings; i++) {
    Serial.println(readings[i]); // إرسال القراءات عبر الاتصال السيريال
  }
  readingIndex = 0; // إعادة تعيين المؤشر لبدء تخزين جديد
}