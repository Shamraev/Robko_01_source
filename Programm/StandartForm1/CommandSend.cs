using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotSpace;
using MCControl;
using InverseKinematics;
using VecLib;
using static VecLib.Methods;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CommandSend
{
    enum GCommand//Поддерживаемые G коды и комманды
    {

        G00,    //ускоренное перемещение в указанную точку 
        G0 = G00,

        G01,    //перемещение в указанную точку по прямой
        G1 = G01,

        G02,    //перемещение в указанную точку по дуге по часовой стрелке; в плоскости XY
        G2 = G02,

        G03,    //перемещение в указанную точку по дуге против часовой стрелки; в плоскости XY
        G3 = G03,

        NONE

    }


    /*Класс для отправки списка команд*/
    class CommandSender
    {
        private string[] commandList;
        public string[] CommandList { get { return commandList; } set { commandList = value; } }//задание в котором есть G коды и свои коды

        private double intrpStep;
        public double IntrpStep { get { return intrpStep; } set { intrpStep = value; } } //шаг интерполяции в мм

        private MainForm owner;
        public MainForm Owner { get { return owner; } set { owner = value; } }

        private MCController mCController;
        public MCController MCController { get { return mCController; } set { mCController = value; } } //для приема и отправки пакета данных 

        private IKSolver iKSolver;
        public IKSolver IKSolver { get { return iKSolver; } set { iKSolver = value; } }

        private bool stopCycle;//??
        private bool CycleStated;
        private bool CyclePause;
        private int pauseItem;//??

        Thread CSThread;
        ManualResetEvent CSThreadMREvent;


        private string currentStrCommand;//текущая строка в списке комманд
        private string previousStrCommand;//предыдущая строка в списке комманд

        private GCommand currentGCommand;//текущая комманда
        private GCommand previousGCommand;//предыдущая комманда

        Vector3d curGoalCoordts;//координаты текущей точки, в которую надо переместиться




        /*-----------------------------------реализация-------------------------------------------*/


        public CommandSender()
        {
            CSThreadMREvent = new ManualResetEvent(true);
            Reset();
        }
        ~CommandSender()
        {

        }

        public void Start()
        {
            Reset();

            if ((CSThread != null) && (CSThread.IsAlive)) CSThread.Abort();

            CSThread = new Thread(new ThreadStart(this.StartSycle));
            CSThread.IsBackground = true;
            CSThread.Start(); // запускаем поток  
        }
        public void Pause()
        {
            if (!CyclePause)//предыдущее состояние 
            {
                CSThreadMREvent.Reset();
                //mCController.CommandHandle.Reset();
                CyclePause = !CyclePause;
            }
            else
            {
                CSThreadMREvent.Set();
                //mCController.CommandHandle.Set();
                CyclePause = !CyclePause;
            }

        }
        public void Stop()//??
        {
            if (!CycleStated) return;

            stopCycle = true;
            CSThread.Abort();

        }
        public void StartSycle()//при запуске потока CSThread
        {
            if ((commandList == null) || (mCController == null)) return;

            CycleStated = true;//---??
            stopCycle = false;//---??

            mCController.commandHandle.Set();//можно начинать

            for (int i = pauseItem; i < commandList.Length; i++)
            {
                CSThreadMREvent.WaitOne();
                DoCommand(commandList[i]);
                previousStrCommand = currentStrCommand;
            }
        }

        public void DoCommand(string strCommand)
        {
            currentStrCommand = strCommand.ToUpper();

            currentGCommand = GetGCommandInStr(currentStrCommand);

            switch (currentGCommand)
            {
                case GCommand.G00:
                    DoCommandG00();
                    break;
                case GCommand.G01:
                    DoCommandG01();
                    break;
                case GCommand.G02:
                    DoCommandG02();
                    break;
                case GCommand.G03:
                    DoCommandG03();
                    break;
                case GCommand.NONE:
                    break;
                default:
                    break;
            }
            previousGCommand = currentGCommand;

        }

        public void Reset()
        {
            pauseItem = 0;

            CyclePause = false;

            currentStrCommand = "";
            previousStrCommand = "";

            currentGCommand = GCommand.NONE;
            previousGCommand = GCommand.NONE;


        }


        /*-----------------------------------реализация G комманд-------------------------------------------*/


        private void DoCommandG00()
        {
            if (!GetXYZ_FromStr(currentStrCommand, ref curGoalCoordts)) return;

            TaskGoToRelativeCoorts(curGoalCoordts);
            SendTask();            

        }
       protected void SendTask()
        {
            mCController.Send();
        }
        private void DoCommandG01()
        {
            if ((owner == null) || (mCController == null) || (mCController.CommandHandle == null)) return;
            if (!GetXYZ_FromStr(currentStrCommand, ref curGoalCoordts)) return;

            Vector3d VDirection;
            Vector3d Nextpoint;

            do
            {
                CSThreadMREvent.WaitOne();

                VDirection = curGoalCoordts - owner.CurWorkCoorts;
                VDirection.Norm();

                Nextpoint = owner.CurWorkCoorts + intrpStep * VDirection;

                if (!PBetweenP1P2(Nextpoint, curGoalCoordts, owner.CurWorkCoorts))//вышла за пределы отрезка
                {
                    //реализвать если не между точками и текущая точка не последаняя - перейти в последнюю
                    if (VEC_MUL_Scalar(Nextpoint - owner.CurWorkCoorts, VDirection) >= 0)//находятся в одном направлении
                        Nextpoint = curGoalCoordts;
                    else break; //Прерываем цикл
                }

                TaskGoToRelativeCoorts(Nextpoint);

                //А если робот передвинется раньше, чем ПК посчитает следующию точку??
                mCController.CommandHandle.WaitOne();//следующая команда будет отправлена тогда, когда завершится предыдущая операция
                SendTask();


            } while (true);

        }
        private void DoCommandG02()
        {

        }
        private void DoCommandG03()
        {

        }


        /*-----------------------------------реализация прочих методов и функций-------------------------------------------*/

        private void TaskGoToRelativeCoorts(Vector3d goalCoordts)
        {
            if ((owner == null) || (iKSolver == null) || (mCController == null)) return;

            owner.CurWorkCoorts = goalCoordts;
            owner.CurWorkCoortsToAbs();

            iKSolver.SolveIK(owner.AbsWorkCoorts.x, owner.AbsWorkCoorts.y, owner.AbsWorkCoorts.z);

            if (IKSolverType.IK3DOF == iKSolver.GetType())
                mCController.TaskAngles(iKSolver.QDeg[0], iKSolver.QDeg[1], iKSolver.QDeg[2]);

        }

        private bool GetXYZ_FromStr(string str, ref Vector3d refV)
        {
            if (str == "") return false;

            string[] strXYZ = { "", "", "" };
            int[] indXYZ = new int[3];

            indXYZ[0] = str.IndexOf('X');
            indXYZ[1] = str.IndexOf('Y');
            indXYZ[2] = str.IndexOf('Z');


            for (int j = 0; j < indXYZ.Length; j++)
            {
                if (indXYZ[j] >= 0)
                {
                    if ((indXYZ[j] + 1) > str.Length - 1) break;

                    for (int i = indXYZ[j] + 1; i < str.Length; i++)
                    {
                        if (str[i] == ',')
                        {
                            Error("В G коде не допустим символ ',' !");
                            return false;
                        }
                        if (Char.IsDigit(str[i]) || (str[i] == '.') || (str[i] == '-'))
                            strXYZ[j] += str[i];
                        else break;
                    }
                }
            }

            if ((strXYZ[0] == "") && (strXYZ[1] == "") && (strXYZ[2] == "")) return false;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");//??
            if (strXYZ[0] != "")
                refV.x = Math.Round(Convert.ToDouble(strXYZ[0]), 2);
            if (strXYZ[1] != "")
                refV.y = Math.Round(Convert.ToDouble(strXYZ[1]), 2);
            if (strXYZ[2] != "")
                refV.z = Math.Round(Convert.ToDouble(strXYZ[2]), 2);

            return true;
        }

        private void Error(string strErr)
        {
            owner.Error(strErr);
            stopCycle = true;
        }

        private GCommand GetGCommandInStr(string strCmd)
        {
            GCommand res = GCommand.NONE;

            if (strCmd == "") return res;

            //проверим есть ли поддерживаемая  G команда
            if (strCmd.IndexOf("G00") >= 0)
                res = GCommand.G00;
            else if (strCmd.IndexOf("G01") >= 0)
                res = GCommand.G01;
            else if (strCmd.IndexOf("G02") >= 0)
                res = GCommand.G02;
            else if (strCmd.IndexOf("G03") >= 0)
                res = GCommand.G03;
            //------------------------------------------------------------------//теперь проверим не сокращенный ли вариант
            else if (strCmd.IndexOf("G0") >= 0)
                res = GCommand.G00;
            else if (strCmd.IndexOf("G1") >= 0)
                res = GCommand.G01;
            else if (strCmd.IndexOf("G2") >= 0)
                res = GCommand.G02;
            else if (strCmd.IndexOf("G3") >= 0)
                res = GCommand.G03;
            //---------------------------------------------//если G команды нет, но есть X или Y или Z: значит продолжение предыдущей команды
            else if ((strCmd.IndexOf('G') < 0) && ((strCmd.IndexOf('X') >= 0) || (strCmd.IndexOf('Y') >= 0) || (strCmd.IndexOf('Z') >= 0)))
                res = previousGCommand;

            return res;
        }



    }
}
