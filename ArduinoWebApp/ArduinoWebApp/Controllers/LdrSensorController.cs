using Microsoft.AspNetCore.Mvc;

namespace ArduinoWebApp.Controllers
{
    public class LdrSensorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
