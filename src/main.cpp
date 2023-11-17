#include <Arduino.h>

const int ledPin = 9; // رقم الرجل الخاص بـ LED
const int sensorPin = A0; // رقم الرجل الخاص بمستشعر الضوء
const int numReadings = 10; // عدد القراءات لحساب المتوسط

float lastSensorValue = 0; // لتخزين آخر قيمة تم قراءتها من الحساس

void setup() {
  Serial.begin(9600); // بدء الاتصال السيريال بسرعة 9600 بت في الثانية
  pinMode(ledPin, OUTPUT); // تحديد رجل الـ LED كمخرج
  pinMode(sensorPin, INPUT); // تحديد رجل مستشعر الضوء كمدخل
}

void loop() {
  /********** LDR Sensor reading ***********************/
   float total = 0; // مجموع قراءات الحساس
  float average = 0; // متوسط قراءات الحساس

  /********** قراءة الحساس وحساب المتوسط *******************/
  for (int i = 0; i < numReadings; i++) {
    total += analogRead(sensorPin); // جمع قراءات الحساس
    delay(10); // تأخير قصير لثبات القراءة
  }
  average = total / numReadings; // حساب المتوسط
  float CurrentSensorValue = map(average, 0, 1023, 0, 255); // تحويل قراءة الحساس إلى نطاق 0-255

  /********** Use the differential equation to make the lighting adapt to the new value ***********************/
  // dx/dt = k * (Target - x) ===> -Ln(Target - x) + c2 = Kt + c1 ===> x = Target - e^ (-Kt-c)
  float Target = CurrentSensorValue;// جعل الهدف هو القيمة الحالية للحساس
  float k = 0.1; // ثابت النسبة
  long t = millis(); // الزمن بالثواني == > 1 ثانية
  float C = abs(CurrentSensorValue - lastSensorValue); //  حساب الثابت  بناءً على الفرق بين القراءتين الحالية والسابقة وتجنب القيم السالبة
  int x = Target - exp(-k * t *.001 - C); // حساب شدة الضوء بناءً على الصيغة الرياضية

  lastSensorValue = CurrentSensorValue; // تحديث آخر قيمة تم قراءتها ووضعها كأخر قيمة تم قرائتها بواسطة للحساس
 
 /********** الحماية من القيم المتطرفة *******************/
  x = constrain(x, 0, 255); // تأكد من أن x يبقى بين 0 و 255
 
 /****************** Control LED Light *******************/
  analogWrite(ledPin, x); // تعديل شدة الـ LED

/****************** Print to Serial Monitor *******************/
  Serial.println(x); // طباعة قيمة x على الوحدة الطرفية السيريال
  
  delay(10); // تأخير لمدة 10 مللي ثانية
}
