using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoWebApp.Model
{
    public class TbLdrSensorReading
    {
        [Key]
        public int LdrSensorReadingID { get; set; }

        [Required]
        [Range(0, 1024, ErrorMessage = "القيمة يجب أن تكون بين 0 و 1024")]
        public float LdrSensorReadingAvratgeValue { get; set; } // القيمة المتوسطة للقراءات

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "وقت تسجيل القراءة")]
        public DateTime LdrSensorReadingTime { get; set; }

    }
}
