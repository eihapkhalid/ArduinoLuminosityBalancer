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
  /*********************readSensor()*********************************/
  readSensor(); // قراءة البيانات من الحساس
  
  /*********************sendReadings() ارسال البيانات كل 10 دقائق*********************************/
  if (millis() - startTime >= 600000) { // 10 دقائق = 600000 مللي ثانية
     sendReadings(); // إرسال البيانات
    startTime = millis(); // إعادة تعيين وقت البدء
  }

/*********************controlLED() اخز القيمة المطلوبة من السيريال وارسالها للخوارزمية للتحكم في شدة الاضاءة*********************************/
  if (Serial.available() > 0) {
    int targetValueFromSerial = Serial.parseInt(); // قراءة القيمة من الاتصال السيريال
    controlLED(targetValueFromSerial); // التحكم في الـ LED باستخدام القيمة المستلمة
  }

}
