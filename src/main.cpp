#include <Arduino.h>

const int ledPin = 9; // رقم الرجل الخاص بـ LED
const int sensorPin = A0; // رقم الرجل الخاص بمستشعر الضوء
float lastSensorValue = 0; // لتخزين آخر قيمة تم قراءتها من الحساس

void setup() {
  pinMode(ledPin, OUTPUT); // تحديد رجل الـ LED كمخرج
  pinMode(sensorPin, INPUT); // تحديد رجل مستشعر الضوء كمدخل
}

void loop() {
  /********** LDR Sensor reading ***********************/
  float CurrentSensorValue = analogRead(sensorPin); // get data From Sensor
  CurrentSensorValue = map(CurrentSensorValue, 0, 1023, 0, 255); // تحويل قراءة الحساس إلى نطاق 0-255

  /********** Use the differential equation to make the lighting adapt to the new value ***********************/
  // dx/dt = k * (Target - x) ===> -Ln(Target - x) + c2 = Kt + c1 ===> x = Target - e^ (-Kt-c)
  float Target = CurrentSensorValue;// جعل الهدف هو القيمة الحالية للحساس
  float k = 0.1; // ثابت النسبة
  long t = millis() / 1000; // الزمن بالثواني == > 1 ثانية
  float C = abs(CurrentSensorValue - lastSensorValue); //  حساب الثابت  بناءً على الفرق بين القراءتين الحالية والسابقة وتجنب القيم السالبة
  lastSensorValue = CurrentSensorValue; // تحديث آخر قيمة تم قراءتها ووضعها كأخر قيمة تم قرائتها بواسطة للحساس
  int x = Target - exp(-k * t - C); // حساب شدة الضوء بناءً على الصيغة الرياضية
}
