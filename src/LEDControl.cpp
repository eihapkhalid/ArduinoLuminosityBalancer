#include "LEDControl.h"

int PreTarget = 0; // تهيئة القيمة السابقة للهدف
int currentLEDValue = 0; // تخزين القيمة الحالية لشدة الـ LED

void controlLED(int targetValueFromSerial) {
    // فقط حساب وتطبيق القيمة الجديدة إذا تغير الهدف
    if (PreTarget != targetValueFromSerial && targetValueFromSerial > 0) {
        int Target = targetValueFromSerial;
        float k = 0.1; // ثابت النسبة
        long t = millis(); // الزمن بالثواني == > 1 ثانية
        int C = abs(Target - PreTarget); // حساب الثابت C

        // حساب شدة الضوء بناءً على الصيغة الرياضية
        currentLEDValue = Target - exp(-k * t * 0.001 - C);
        currentLEDValue = constrain(currentLEDValue, 0, 255); // تأكد من أن x يبقى بين 0 و 255

        analogWrite(ledPin, currentLEDValue); // تعديل شدة الـ LED
        PreTarget = Target; // تحديث القيمة السابقة للهدف

        Serial.println(currentLEDValue); // طباعة قيمة x على الوحدة الطرفية السيريال
    }
}
