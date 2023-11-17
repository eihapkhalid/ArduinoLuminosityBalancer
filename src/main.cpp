#include <Arduino.h>
#include "SensorReadings.h"
#include "LEDControl.h"

void setup() {
  Serial.begin(9600); // بدء الاتصال السيريال بسرعة 9600 بت في الثانية
  pinMode(ledPin, OUTPUT);
  pinMode(sensorPin, INPUT);
}

void loop() {
  readSensor();
  float averageOfSensorValue = getAverageReading();
  controlLED(averageOfSensorValue);
  delay(10);
}
