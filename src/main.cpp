#include <Arduino.h>

const int ledPin = 9; // رقم الرجل الخاص بـ LED
const int sensorPin = A0; // رقم الرجل الخاص بمستشعر الضوء
int lastSensorValue = 0; // لتخزين آخر قيمة تم قراءتها من الحساس

void setup() {
  pinMode(ledPin, OUTPUT); // تحديد رجل الـ LED كمخرج
  pinMode(sensorPin, INPUT); // تحديد رجل مستشعر الضوء كمدخل
}

void loop() {
  // put your main code here, to run repeatedly:
}
