#include <Arduino.h>

const int ledPin = 9; // رقم الرجل الخاص بـ LED
const int sensorPin = A0; // رقم الرجل الخاص بمستشعر الضوء
float lastSensorValue = 0; // لتخزين آخر قيمة تم قراءتها من الحساس

void setup() {
  pinMode(ledPin, OUTPUT); // تحديد رجل الـ LED كمخرج
  pinMode(sensorPin, INPUT); // تحديد رجل مستشعر الضوء كمدخل
}

void loop() {
  float CurrentSensorValue = analogRead(sensorPin); // get data From Sensor
  CurrentSensorValue = map(CurrentSensorValue, 0, 1023, 0, 255); // تحويل قراءة الحساس إلى نطاق 0-255
}
