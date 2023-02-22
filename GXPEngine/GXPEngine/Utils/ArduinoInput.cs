using System;
using System.IO.Ports;
namespace GXPEngine.GXPEngine.Utils
{

    class ArduinoInput
    {
        public static bool isConnected = false;
        public static int rotationCounter = 0;

        SerialPort port = new SerialPort();

        public ArduinoInput()
        {
            if (isConnected)
            {
                port.PortName = "COM7";
                port.BaudRate = 9600;
                port.RtsEnable = true;
                port.DtrEnable = true;
                port.Open();
            }
        }

        public void update()
        {
            if (isConnected)
            {
                string line = port.ReadExisting(); // read separated values
                                                   //string line = port.ReadExisting(); // when using characters
                if (line != "")
                {
                    if (int.TryParse(line, out _))
                    {
                        rotationCounter = int.Parse(line);
                    }

                    Console.WriteLine(rotationCounter);

                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    port.Write(key.KeyChar.ToString());  // writing a string to Arduino
                }
            }
        }

    }
}

