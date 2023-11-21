using System.IO.Ports;
using System;

namespace ArduinoWebApp.library
{
    public class SerialPortConnector : IDisposable
    {
        private readonly SerialPort _serialPort;
        public event EventHandler<string> DataReceived;
        int x = 0;
        public SerialPortConnector()
        {
            _serialPort = new SerialPort("COM10", 9600, Parity.None, 8, StopBits.One);
            try
            {
                _serialPort.Open();
                _serialPort.DataReceived += OnDataReceived;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening serial port: " + ex.Message);
                // يمكنك أيضًا إعادة رمي الاستثناء إذا كان ذلك مناسبًا
            }
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_serialPort.BytesToRead > 0)
            {
                try
                {
                    string indata = _serialPort.ReadExisting();
                    DataReceived?.Invoke(this, indata);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading serial data: " + ex.Message);
                    // معالجة الخطأ أو إعادة رميه
                }
            }
        }

        public string Receive()
        {
            
            using (_serialPort)
            {

                if (x >= 1)
                {
                    _serialPort.Open();
                }
                x++;
                var data = _serialPort.ReadLine(); // قراءة كل البيانات المتاحة

                return data;
            }
        }

        public void Send(string command)
        {
            try
            {
                _serialPort.WriteLine(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending command: " + ex.Message);
                // معالجة الخطأ أو إعادة رميه
            }
        }

        public void Dispose()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            _serialPort.Dispose();
        }
    }
}
