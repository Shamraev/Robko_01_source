using System;
using System.IO.Ports;
using RobotSpace;
using System.Threading;
using BusControl;

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

        private byte[] curByteFrame;
        private byte[] prevByteFrame;

        public ManualResetEvent commandHandle;

        public EventWaitHandle CommandHandle { get { return commandHandle; } set { } }

        protected FrameFormer frameFormer; 


        /*-----------------------------------реализация-------------------------------------------*/



        public MCController(SerialPort aSerialPort, MainForm aOwner)
        {
            this.serialPort = aSerialPort;
            this.owner = aOwner;
            this.frameFormer = new FrameFormer(this);

            commandHandle = new ManualResetEvent(true);
            PortTurnOn(owner.RobotPortName); //включить порт
        }
        ~MCController()
        {
            PortTurnOff();
        }

        /// <summary>
        /// Отправить уже созданный пакет
        /// </summary>
        public void Send()
        {
            //if (!SerialPortIsOpen())
            //{
            //   if (!PortTurnOn(owner.RobotPortName)) return;
            //}


            //---
            //byte[] frame = new byte[18];
            //string str = "1 0 18 1 10 215 35 192 123 20 244 65 41 92 99 66 255 93";
            //string[] nmStr = str.Split(' ');

            //for (int i = 0; i < 18; i++)
            //{
            //    frame[i] = Convert.ToByte(nmStr[i]);
            //}
            //bool bl = frameFormer.Validate_frame(frame, 18);
            //curByteFrame = frame;
            //---

            if ((SerialPortIsOpen()) && (curByteFrame != null))
            {
                if (owner.DoLog) AddLog("MCControl.Send(): curByteArr = " + string.Join(" ", curByteFrame));//??----
                serialPort.Write(curByteFrame, 0, curByteFrame.Length);
                prevByteFrame = curByteFrame;
                taskCompleted = false;
                commandHandle.Reset();//можно продолжать отправку
            }

        }
        public void SendPreviousFrame()
        {
            if ((SerialPortIsOpen()) && (prevByteFrame != null))
            {
                if (owner.DoLog) AddLog("MCControl.SendPreviousFrame(): prevByteFrame = " + string.Join(" ", prevByteFrame));//??----
                serialPort.Write(prevByteFrame, 0, prevByteFrame.Length);
                taskCompleted = false;
                commandHandle.Reset();//можно продолжать отправку
            }
        }

            private void AddLog(string str)
        {
            owner.AddLog(str);
        }

        public void TaskAngles(double q1, double q2, double q3)
        {
            curByteFrame = frameFormer.DoFrame_Request_MoveToAbsoluteAngles(q1, q2, q3);
        }
        public void SendAngles(double q1, double q2, double q3)//---убрать---
        {
            curByteFrame = frameFormer.DoFrame_Request_MoveToAbsoluteAngles(q1, q2, q3);
            Send();

            //serialPort.Write(byteArray, 0, byteArray.Length);
            //serialPort1.Read(buffer, 0, 1);//------------
        }
        public void GetResponse()
        {
            byte[] buf = new byte[FrameFormer.RESPONSE_FRAME_LENGTH];//--
            serialPort.Read(buf, 0, FrameFormer.RESPONSE_FRAME_LENGTH);//--
             
            //Validate_frame проверка????
            if (buf[(int)FrameFormer.FrameIndexes.PAYLOAD] == 1)
                TaskComplete();
            else
            {
                if (owner.DoLog) AddLog("!!!!!!!!!!!!!");
                if (owner.DoLog) AddLog("Error in MCControl.GetResponse(): " + "данные переданы МК некорректно, отправить данные заново");
                SendPreviousFrame();//произошел сбой при передаче сообщения, отправим повторно
            }
                
        }
        public void TaskComplete()
        {
            owner.XyzDisplay();
            taskCompleted = true;
            commandHandle.Set();
        }
        public bool PortTurnOn(string aPortName)//включить порт
        {
            bool res = false;
            if (serialPort == null) return res;

            PortTurnOff();
            if (aPortName != null)
            {
                try
                {
                    serialPort.PortName = aPortName;
                    serialPort.Open();
                    Thread.Sleep(100);//?? сколько лучше всего ??
                                      // SendAngles();
                                      // SendAngelesToRobot(0, 0, 0);//-----??                    
                    OkPort();
                    res = true;
                }

                catch
                {
                    ErrPort();
                }
            }
            else ErrPort();

            return res;
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
            res = (serialPort != null) && serialPort.IsOpen;

            if (res == false) ErrPort();
            return res;
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
