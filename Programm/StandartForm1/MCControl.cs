﻿using System;
using System.IO.Ports;
using RobotSpace;
using System.Threading;

namespace MCControl
{
    /*Класс для взаимодействия с микроконтроллером робота*/
    class MCController
    {
        private SerialPort serialPort;
        public SerialPort SerialPort { get { return serialPort; } set { /*serialPort = value; */} }

        private MainForm owner;
        public MainForm Owner { get { return owner; } set { /*owner = value; */} }

        private bool taskCompleted;
        public bool TaskCompleted { get { return taskCompleted; } set { taskCompleted = value; } }



        /*-----------------------------------реализация-------------------------------------------*/



        public MCController(SerialPort aSerialPort, MainForm aOwner)
        {
            this.serialPort = aSerialPort;
            this.owner = aOwner;
            PortTurnOn(owner.RobotPortName); //включить порт
        }
        ~MCController()
        {
            PortTurnOff();
        }

        public void SendAngles(double a1, double a2, double a3)
        {
            taskCompleted = false;
            if ((serialPort == null) || (!serialPort.IsOpen))
            {
                ErrPort();
                return;
            }

            var floatArray = new float[] { (float)Math.Round(a1, 2), (float)Math.Round(a2, 2), (float)Math.Round(a3, 2) };
            var byteArray = new byte[floatArray.Length * 4];

            Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
            serialPort.Write(byteArray, 0, byteArray.Length);
            //serialPort1.Read(buffer, 0, 1);//------------
        }
        public void TaskComplete()
        {
            owner.XyzDisplay();
            taskCompleted = true;
        }
        public void PortTurnOn(string aPortName)//включить порт
        {
            if (serialPort == null) return;

            PortTurnOff();
            if (aPortName != null)
            {
                try
                {
                    serialPort.PortName = aPortName;
                    serialPort.Open();
                    Thread.Sleep(1000);//---------
                                       // SendAngles();
                                       // SendAngelesToRobot(0, 0, 0);//-----??
                    OkPort();
                }

                catch
                {
                    ErrPort();
                }
            }
            else ErrPort();
        }
        public void PortTurnOff()//выключить порт----------------------
        {
            if (serialPort == null) return;

            try
            {
                if (serialPort.IsOpen)
                    serialPort.Close();
            }
            catch { }
        }
        public bool SerialPortIsOpen()
        {
            bool res = false;
            if (serialPort == null) return res;

            return serialPort.IsOpen;
        }
        public string SerialPortReadExisting()
        {
            string res = "";
            if (serialPort == null) return res;

            return serialPort.ReadExisting();
        }
        private void OkPort()
        {
            if (serialPort != null)
                owner.UpdateStatus("соединено с " + serialPort.PortName);
        }
        public void ErrPort()
        {
            PortTurnOff();//---------------------------------------------------------------????
            owner.UpdateStatus("соединение отсутствует");
        }

    }
}
