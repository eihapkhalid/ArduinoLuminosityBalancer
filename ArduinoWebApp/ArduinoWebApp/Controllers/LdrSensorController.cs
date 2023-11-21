using ArduinoWebApp.Acsses.UnitOfWork;
using ArduinoWebApp.library;
using ArduinoWebApp.Utilities;
using ArduinoWebApp.Model;
using ArduinoWebApp.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ArduinoWebApp.Controllers
{
    public class LdrSensorController : Controller
    {
        private SerialPortConnector _serialPortConnector;
        private CalculateSensorReading _calculateSensorReading;
        private IUnitOfWork _unitOfWork;
        public LdrSensorController(IUnitOfWork unitOfWork, SerialPortConnector serialPortConnector, CalculateSensorReading calculateSensorReading)
        {
            _serialPortConnector =  serialPortConnector;
            _calculateSensorReading = calculateSensorReading;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetData()
        {
            // استدعاء الطريقة التي تجلب البيانات من SerialPortConnector
            var data = _serialPortConnector.Receive(); // افترض أن هذه الطريقة تعيد قراءات الإضاءة الحالية

            // قد تحتاج إلى تحويل البيانات إلى شكل مناسب إذا لزم الأمر
            // عودة البيانات كاستجابة JSON
            return Ok(data);
        }
        public IActionResult Index()
        {
            var data = _serialPortConnector.Receive();
            var lightReadings = _calculateSensorReading.ParseDataToArray(data);
            var targetLightLevel = _calculateSensorReading.CalculateTargetLightLevel(lightReadings, _calculateSensorReading.GetCurrentTimeOfDay());
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

    }

}
