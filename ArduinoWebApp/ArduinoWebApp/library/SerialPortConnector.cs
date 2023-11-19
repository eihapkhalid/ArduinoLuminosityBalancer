using System.IO.Ports;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArduinoWebApp.library
{
    public class SerialPortConnector
    {
        private readonly int _baudRate = 9600;
        private readonly string _portName = "COM1";
        private readonly Parity _parity = Parity.None;
        private readonly int _dataBits = 8;
        private readonly StopBits _stopBits = StopBits.One;

        public string Receive()
        {
            using (var serialPort = new SerialPort(_portName, _baudRate, _parity, _dataBits, _stopBits))
            {
                serialPort.Open();

                var data = serialPort.ReadLine(); // قراءة كل البيانات المتاحة

                return data;
            }
        }


        public void Send(string command)
        {
            using (var serialPort = new SerialPort(_portName, _baudRate, _parity, _dataBits, _stopBits))
            {
                serialPort.Open();
                Console.WriteLine("Received number: " + command);
                serialPort.WriteLine(command);
                // لا حاجة لإغلاق المنفذ هنا بسبب استخدام using
            }
        }
    }
}
