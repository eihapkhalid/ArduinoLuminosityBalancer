using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoWebApp.Model.ViewModel
{
    public class LdrSensorViewModel
    {
        public float[] LightReadings { get; set; }
        public int TargetLightLevel { get; set; }
    }
}
