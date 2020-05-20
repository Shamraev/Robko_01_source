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

            if ((SerialPortIsOpen()) && (curByteFrame != null))
            {
                if (owner.DoLog) AddLog("MCControl.Send(): curByteArr = " + string.Join(" ", curByteFrame));//??----
                UpdateStatus_Operation(curByteFrame);//??время для G01 G02/G03 ??
                serialPort.Write(curByteFrame, 0, curByteFrame.Length);//try catch??
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
        public void TaskGripperGrip()
        {
            curByteFrame = frameFormer.DoFrame_Request_GripperGrip();
        }
        public void TaskGripperUngrip()
        {
            curByteFrame = frameFormer.DoFrame_Request_GripperUngrip();
        }

        public void TaskGripperOpenToAbsoluteDistance(double a)
        {
            curByteFrame = frameFormer.DoFrame_Request_GripperOpenToAbsoluteDistance(a);
        }
        public void TaskFindAndGoToZeros()
        {
            curByteFrame = frameFormer.DoFrame_Request_FindAndGoToZeros();
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
            //string str = serialPort.ReadExisting();//-----------------
            byte[] buf = new byte[serialPort.BytesToRead];//--
            serialPort.Read(buf, 0, serialPort.BytesToRead);//--

            UpdateStatus_Operation(buf);//??
            //Validate_frame проверка????
            if (buf[(int)FrameFormer.FrameIndexes.STATUS_CODE] == (byte)FrameFormer.STATUS_CODE.DONE)
            {
                TaskComplete();
            }
            else//error//?? сколько раз ??
            {
                if (owner.DoLog) AddLog("!!!!!!!!!!!!!");
                if (owner.DoLog) AddLog("Error in MCControl.GetResponse(): " + "данные переданы МК некорректно, отправить данные заново");
                SendPreviousFrame();//произошел сбой при передаче сообщения, отправим повторно
            }

        }
        public void TaskComplete()
        {
            //if (prevByteFrame[(int)FrameFormer.FrameIndexes.OPERATION_CODE] == (byte)FrameFormer.OPERATION_CODE.FIND_AND_GO_TO_ZEROS)
            //owner.FIND_AND_GO_TO_ZEROS_DONE();//----

            owner.XyzDisplay();
            taskCompleted = true;
            commandHandle.Set();
        }

        public void UpdateStatus_Operation(byte[] frame)
        {
            //if (frame[(byte)FrameFormer.FrameIndexes.REQUEST_RESPONSE] != (byte)FrameFormer.REQUEST_RESPONSE.RESPONSE) return;//обновляем только для ответов
            if (frame[(byte)FrameFormer.FrameIndexes.OPERATION_CODE] == (byte)FrameFormer.OPERATION_CODE.MOVE_TO_ABSOLUTE_ANGLES_Q1Q2Q3) return;//не нужно тратить время на вывод операции перемещение в углы

            string str_Operation = "none";
            string str_Status = "none";

            switch (frame[(byte)FrameFormer.FrameIndexes.OPERATION_CODE])
            {
                case (byte)FrameFormer.OPERATION_CODE.MOVE_TO_ABSOLUTE_ANGLES_Q1Q2Q3://сюда он не должен заходить
                    str_Operation = "Переместиться в углы";
                    break;
                case (byte)FrameFormer.OPERATION_CODE.FIND_AND_GO_TO_ZEROS:
                    str_Operation = "Найти нули робота";
                    owner.ResetCoordnts();
                    owner.XyzDisplay();
                    break;
                case (byte)FrameFormer.OPERATION_CODE.GRIPPER_GRIP:
                    str_Operation = "Сжать схват";
                    break;
                case (byte)FrameFormer.OPERATION_CODE.GRIPPER_UNGRIP:
                    str_Operation = "Разжать схват";
                    break;
                case (byte)FrameFormer.OPERATION_CODE.GRIPPER_OPEN_TO_ABSOLUTE_DISTANCE:
                    str_Operation = "Открыть схват на расстояние между губками";
                    break;
            }
            switch (frame[(byte)FrameFormer.FrameIndexes.STATUS_CODE])
            {
                case (byte)FrameFormer.STATUS_CODE.DONE:
                    str_Status = "выполнено";
                    break;
                case (byte)FrameFormer.STATUS_CODE.ERROR:
                    str_Status = "ошибка";
                    break;
            }
            if (frame[(byte)FrameFormer.FrameIndexes.REQUEST_RESPONSE] == (byte)FrameFormer.REQUEST_RESPONSE.REQUEST)
                str_Status = "старт";

            owner.UpdateStatus(str_Operation + ": " + str_Status);
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
