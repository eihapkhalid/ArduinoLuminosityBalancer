#include "SensorReadings.h"
#include "LEDControl.h"

void controlLED(float sensorValue) {
    // dx/dt = k * (Target - x) ===> -Ln(Target - x) + c2 = Kt + c1 ===> x = Target - e^ (-Kt-c)
    float Target = map(sensorValue, 0, 1023, 0, 255); // تحويل قيمة الحساس إلى نطاق 0-255
    float k = 0.1; // ثابت النسبة
    long t = millis(); // الزمن بالثواني == > 1 ثانية
    float C = abs(Target - lastSensorValue); // حساب الثابت بناءً على الفرق بين القراءتين الحالية والسابقة وتجنب القيم السالبة
    int x = Target - exp(-k * t *.001 - C); // حساب شدة الضوء بناءً على الصيغة الرياضية

    x = constrain(x, 0, 255); // تأكد من أن x يبقى بين 0 و 255

    analogWrite(ledPin, x); // تعديل شدة الـ LED
    
    Serial.println(x); // طباعة قيمة x على الوحدة الطرفية السيريال
}
