﻿using ArduinoWebApp.Acsses.UnitOfWork;
using ArduinoWebApp.library;
using ArduinoWebApp.Model;
using ArduinoWebApp.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ArduinoWebApp.Controllers
{
    public class LdrSensorController : Controller
    {
        private SerialPortConnector _serialPortConnector;
        private IUnitOfWork _unitOfWork;
        public LdrSensorController(IUnitOfWork unitOfWork, SerialPortConnector serialPortConnector)
        {
            _serialPortConnector =  serialPortConnector;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var data = _serialPortConnector.Receive();
            var lightReadings = ParseDataToArray(data);
            var targetLightLevel = CalculateTargetLightLevel(lightReadings, GetCurrentTimeOfDay());
            _serialPortConnector.Send(targetLightLevel.ToString());
            SaveSensorReading(lightReadings.Average(), DateTime.Now);

            var viewModel = new LdrSensorViewModel
            {
                LightReadings = lightReadings,
                TargetLightLevel = targetLightLevel
            };

            return View(viewModel);
        }

        public IActionResult Send(string command)
        {
            try
            {
                var fullCommand = command;
                _serialPortConnector.Send(fullCommand);
                return Ok("تم الإرسال بنجاح");
            }
            catch (Exception ex)
            {
                return BadRequest("فشل الإرسال: " + ex.Message);
            }
        }


        private void SaveSensorReading(float averageValue, DateTime readingTime)
        {
            var sensorReading = new TbLdrSensorReading
            {
                LdrSensorReadingAvratgeValue = averageValue,
                LdrSensorReadingTime = readingTime
            };
            _unitOfWork.TbLdrSensorReading.Add(sensorReading);
            _unitOfWork.Save();
        }
        private float[] ParseDataToArray(string data)
        {
            // تقسيم البيانات النصية إلى عناصر، وتحويل كل عنصر إلى رقم عشري (float)
            // مع التعامل مع العناصر الفارغة أو غير الصحيحة
            return data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(s =>
                       {
                           float.TryParse(s, out float parsedValue);
                           return parsedValue;
                       })
                       .ToArray();
        }


        private TimeOfDay GetCurrentTimeOfDay()
        {
            // تحديد الوقت الحالي من اليوم
            var hour = DateTime.Now.Hour;
            if (hour < 12)
                return TimeOfDay.Morning;
            if (hour < 18)
                return TimeOfDay.Afternoon;
            return hour < 22 ? TimeOfDay.Evening : TimeOfDay.Night;
        }

        public int CalculateTargetLightLevel(float[] lightReadings, TimeOfDay timeOfDay)
        {
            float averageLightLevel = lightReadings.Average();
            int desiredLightLevel = DetermineDesiredLightLevel(timeOfDay);

            int targetLightLevel = averageLightLevel < desiredLightLevel
                ? IncreaseLightLevel(averageLightLevel, desiredLightLevel)
                : DecreaseLightLevel(averageLightLevel, desiredLightLevel);

            return Math.Clamp(targetLightLevel, 0, 255);
        }

        private int DetermineDesiredLightLevel(TimeOfDay timeOfDay)
        {
            switch (timeOfDay)
            {
                case TimeOfDay.Morning:
                case TimeOfDay.Afternoon:
                    return 200; // قيمة عالية خلال النهار
                case TimeOfDay.Evening:
                case TimeOfDay.Night:
                    return 100; // قيمة منخفضة خلال الليل
                default:
                    throw new ArgumentOutOfRangeException(nameof(timeOfDay), "وقت غير معروف");
            }
        }

        private int IncreaseLightLevel(float current, int desired)
        {
            // معادلة تفاضلية لزيادة مستوى الإضاءة
            // dx/dt = k * (desired - current)
            float k = 0.1f;
            float dx_dt = k * (desired - current);
            return (int)(current + dx_dt);
        }

        private int DecreaseLightLevel(float current, int desired)
        {
            // معادلة تفاضلية لتقليل مستوى الإضاءة
            // dx/dt = -k * (current - desired)
            float k = 0.1f;
            float dx_dt = -k * (current - desired);
            return (int)(current + dx_dt);
        }

    }

    public enum TimeOfDay
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }
}
