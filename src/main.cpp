#include <Arduino.h>
#include "SensorReadings.h"
#include "LEDControl.h"

unsigned long lastReadTime;   // لتخزين وقت آخر قراءة
unsigned long lastSendTime;   // لتخزين وقت آخر إرسال
int readingIndex=0; // مؤشر للمصفوفة

void setup() {
  lastReadTime = millis();    // تسجيل وقت بدء القراءة
  lastSendTime = millis();    // تسجيل وقت بدء الإرسال
  Serial.begin(9600); // بدء الاتصال السيريال بسرعة 9600 بت في الثانية
  pinMode(ledPin, OUTPUT);
  pinMode(sensorPin, INPUT);
}

void loop() {
  /*********************readSensor()*********************************/
 // قراءة البيانات من الحساس كل ثانية
  if (millis() - lastReadTime >= 1000) {
    readSensor();             // قراءة البيانات من الحساس
    readingIndex++;
    lastReadTime = millis();  // إعادة تعيين وقت آخر قراءة
  }
  /*********************sendReadings() ارسال البيانات كل 10 دقائق*********************************/
   // إرسال البيانات كل 60 ثانية
  if (millis() - lastSendTime >= 60000) { // 30 ثانية = 30000 مللي ثانية
    sendReadings();           // إرسال البيانات
    lastSendTime = millis();  // إعادة تعيين وقت آخر إرسال
  }

/*********************controlLED() اخز القيمة المطلوبة من السيريال وارسالها للخوارزمية للتحكم في شدة الاضاءة*********************************/
  // التحكم في الـ LED استناداً إلى القيمة المستلمة من السريال
  if (Serial.available() > 0) {
    int targetValueFromSerial = Serial.parseInt(); // قراءة القيمة من السريال
    controlLED(targetValueFromSerial);             // التحكم في الـ LED
  }

}
