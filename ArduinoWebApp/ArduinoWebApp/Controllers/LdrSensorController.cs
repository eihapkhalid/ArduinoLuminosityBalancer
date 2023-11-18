using Microsoft.AspNetCore.Mvc;

namespace ArduinoWebApp.Controllers
{
    public class LdrSensorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public int CalculateTargetLightLevel(float[] lightReadings, TimeOfDay timeOfDay)
        {
            // حساب متوسط قراءات الإضاءة
            float averageLightLevel = lightReadings.Average();

            // تحديد القيمة المثالية للإضاءة بناءً على الوقت من اليوم
            int desiredLightLevel = DetermineDesiredLightLevel(timeOfDay);

            // تعديل الإضاءة بناءً على المتوسط
            int targetLightLevel;
            if (averageLightLevel < desiredLightLevel)
            {
                targetLightLevel = IncreaseLightLevel(averageLightLevel, desiredLightLevel);
            }
            else
            {
                targetLightLevel = DecreaseLightLevel(averageLightLevel, desiredLightLevel);
            }

            return Math.Clamp(targetLightLevel, 0, 255);
        }

        private int DetermineDesiredLightLevel(TimeOfDay timeOfDay)
        {
            // تحديد القيمة المثالية للإضاءة بناءً على الوقت من اليوم
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
            // المعادلة التفاضلية: dx/dt = 0.1 * (desired - current)
            // هذه المعادلة تستخدم لزيادة مستوى الإضاءة
            float delta = desired - current;
            float rateOfChange = delta * 0.1f;
            return (int)(current + rateOfChange);
        }

        private int DecreaseLightLevel(float current, int desired)
        {
            // المعادلة التفاضلية: dx/dt = 0.1 * (current - desired)
            // هذه المعادلة تستخدم لتقليل مستوى الإضاءة
            float delta = current - desired;
            float rateOfChange = delta * 0.1f;
            return (int)(current - rateOfChange);
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

