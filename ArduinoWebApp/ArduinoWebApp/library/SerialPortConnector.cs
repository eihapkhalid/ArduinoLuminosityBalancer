using System.IO.Ports;

namespace ArduinoWebApp.library
{
    public class SerialPortConnector
    {
        private readonly int _baudRate = 9600;
        private readonly string _portName = "COM3";

        public string Receive()
        {
            using (var serialPort = new SerialPort(_portName, _baudRate))
            {
                serialPort.Open();
                return serialPort.ReadLine(); // قراءة البيانات من أردوينو
            }
        }

        public void Send(string command)
        {
            using (var serialPort = new SerialPort(_portName, _baudRate))
            {
                serialPort.Open();
                serialPort.Write(command);
            }
        }
    }
}
