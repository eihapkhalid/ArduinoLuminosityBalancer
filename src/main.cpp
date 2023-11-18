#include <Arduino.h>
#include "SensorReadings.h"
#include "LEDControl.h"

unsigned long startTime; // لتخزين وقت البدء

void setup() {
  startTime = millis(); // تسجيل وقت البدء
  Serial.begin(9600); // بدء الاتصال السيريال بسرعة 9600 بت في الثانية
  pinMode(ledPin, OUTPUT);
  pinMode(sensorPin, INPUT);
}

void loop() {
  readSensor(); // قراءة البيانات من الحساس
  
  if (millis() - startTime >= 600000) { // 10 دقائق = 600000 مللي ثانية
     sendReadings(); // إرسال البيانات
    startTime = millis(); // إعادة تعيين وقت البدء
  }

  //controlLED(averageOfSensorValue);
  delay(10);
}
