using System.IO.Ports;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArduinoWebApp.library
{
    public class SerialPortConnector
    {
        private readonly int _baudRate = 9600;
        private readonly string _portName = "COM3";
        private readonly Parity _parity = Parity.None;
        private readonly int _dataBits = 8;
        private readonly StopBits _stopBits = StopBits.One;

        public string Receive()
        {
            try
            {
                using (var serialPort = new SerialPort(_portName, _baudRate, _parity, _dataBits, _stopBits))
                {
                    serialPort.Open();
                    // قد تحتاج إلى الانتظار هنا لبعض الوقت حتى تصل البيانات
                    while (serialPort.BytesToRead == 0)
                    {
                        System.Threading.Thread.Sleep(50); // انتظر 50 ميلي ثانية
                    }
                    string data = serialPort.ReadExisting(); // قراءة كل البيانات المتاحة
                    int number;

                    if (int.TryParse(data, out number))
                    {
                        // البيانات صحيحة وتم التحويل بنجاح
                        Console.WriteLine("Received number: " + number);
                    }
                    else
                    {
                        // البيانات غير صحيحة، قم بمعالجة الخطأ هنا
                        Console.WriteLine("Received data is not a valid number.");
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in receiving data: " + ex.Message);
                return null;
            }
        }


        public void Send(string command)
        {
            using (var serialPort = new SerialPort(_portName, _baudRate, _parity, _dataBits, _stopBits))
            {
                serialPort.Open();
                Console.WriteLine("Received number: " + command);
                serialPort.Write(command);
                // لا حاجة لإغلاق المنفذ هنا بسبب استخدام using
            }
        }
    }
}
