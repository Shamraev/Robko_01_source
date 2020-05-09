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
using CurveLib;
using static CurveLib.Methods;

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

        private bool _CycleStarted;
        public bool CycleStarted { get { return _CycleStarted; } set { } }

        private bool _CyclePause;
        public bool CyclePause { get { return _CyclePause; } set { } }

        private int pauseItem;//??

        Thread CSThread;
        ManualResetEvent CSThreadMREvent;


        private string currentStrCommand;//текущая строка в списке комманд
        private string previousStrCommand;//предыдущая строка в списке комманд

        private GCommand currentGCommand;//текущая комманда
        private GCommand previousGCommand;//предыдущая комманда

        Vector3d curGoalCoordts;//координаты текущей точки, в которую надо переместиться

        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();



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
            GC.Collect();

            if ((CSThread != null) && (CSThread.IsAlive)) CSThread.Abort();

            CSThread = new Thread(new ThreadStart(this.StartSycle));

            stopCycle = false;//---??--
            _CycleStarted = true;//---??
            CSThread.IsBackground = true;
            CSThread.Start(); // запускаем поток  
        }
        public void Pause()
        {
            if (!_CycleStarted) return;

            if (!_CyclePause)//предыдущее состояние 
            {
                CSThreadMREvent.Reset();
                //mCController.CommandHandle.Reset();
                _CyclePause = !_CyclePause;
            }
            else
            {
                CSThreadMREvent.Set();
                //mCController.CommandHandle.Set();
                _CyclePause = !_CyclePause;
            }

        }
        public void Stop()//??
        {
            if (!_CycleStarted) return;

            stopCycle = true;
            EndTime();//?? в Stop()??
            _CycleStarted = false;//---??

            owner.CommandSenderStopped();
            CSThread.Abort();   //??
        }
        public void StartSycle()//при запуске потока CSThread
        {
            if ((commandList == null) || (mCController == null)) return;


            UpdateStatus("выполнение G команд");
            StartTime();//?? в Start()??          


            mCController.commandHandle.Set();//задание mCController: можно начинать

            for (int i = pauseItem; i < commandList.Length; i++)
            {
                CSThreadMREvent.WaitOne();
                DoCommand(i);
                previousStrCommand = currentStrCommand;
            }

            Stop();
        }
        protected void StartTime()
        {
            if (stopwatch == null) return;

            stopwatch.Reset();
            //stopwatch.Elapsed += timer_Tick;
            stopwatch.Start();
            
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            owner.UpdateTimerState(stopwatch.Elapsed.Seconds.ToString());            
        }
        protected void EndTime()
        {
            if (stopwatch == null) return;

            stopwatch.Stop();
            UpdateStatus("G код выполнен за такое время: " + stopwatch.Elapsed);
        }

        public void DoCommand(int i)
        {
            string strCommand = commandList[i];
            if ((strCommand == null) || (strCommand == "")) return;

            DesplayCurGCodeStr(i + 1 + ": " + strCommand);
            if (owner.DoLog)
            {
                AddLog("-----------------------------------");
                AddLog("CommandSender.DoCommand(): Command: " + (i + 1) + " : " + strCommand);//??----
            }

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
        private void AddLog(string str)
        {
            owner.AddLog(str);
        }

        protected void DesplayCurGCodeStr(string str)
        {
            owner.DesplayCurGCodeStr(str);
        }

        public void Reset()
        {
            pauseItem = 0;

            _CyclePause = false;

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

            Cut ct = new Cut(owner.CurWorkCoorts, curGoalCoordts);
            double len = 0;
            Vector3d nextPoint = owner.CurWorkCoorts;

            do
            {
                CSThreadMREvent.WaitOne();//для паузы потока

                if (len > ct.Length)//возможно прошли весь отрезок
                {
                    if (nextPoint == ct.EndPoint[false])
                        break; //прошли весь отрезок
                    else
                        nextPoint = curGoalCoordts;
                }
                else
                    nextPoint = ct.GetPointLen(len);

                TaskGoToRelativeCoorts(nextPoint);

                //А если робот передвинется раньше, чем ПК посчитает следующию точку??
                mCController.CommandHandle.WaitOne();//следующая команда будет отправлена тогда, когда завершится предыдущая операция
                SendTask();

                len = len + intrpStep;

            } while (true);

        }
        private void DoCommandG02()
        {
            DoCommandG03_G03(false);
        }
        private void DoCommandG03()
        {
            DoCommandG03_G03(true);
        }
        /// <summary>
        /// Круговая интерполяция в плоскости (x, y)
        /// </summary>
        private void DoCommandG03_G03(bool isG03)
        {
            if ((owner == null) || (mCController == null) || (mCController.CommandHandle == null)) return;

            GetXYZ_FromStr(currentStrCommand, ref curGoalCoordts);//??если желаемая точка совпадает с начальной точкой??

            Vector3d a = owner.CurWorkCoorts;
            Vector3d b = curGoalCoordts;
            Vector3d cp = a;

            double[] IJK_D = new double[3];
            double[] R_D = new double[1];
            if (GetNumbers_FromStr(currentStrCommand, new char[] { 'I', 'J', 'K' }, ref IJK_D))
            {
                cp.x = a.x + IJK_D[0];
                cp.y = a.y + IJK_D[1];
                cp.z = a.z + IJK_D[2];
            }
            else if (GetNumbers_FromStr(currentStrCommand, new char[] { 'R' }, ref R_D))
            {
                Vector2d cp2d;
                if (!GetCP_FromArc(p2d(a), p2d(b), R_D[0], isG03, out cp2d)) return;//в плоскости (x, y) получаем центр дуги
                cp = p3d(cp2d, a.z);
            }
            else return;

            Arc arc = new Arc(p2d(a), p2d(b), p2d(cp), isG03);//в плоскости (x, y)
            double len = 0;
            Vector3d nextPoint = owner.CurWorkCoorts;

            do
            {
                CSThreadMREvent.WaitOne();//для паузы потока

                if (len > arc.Length)//возможно прошли весь отрезок
                {
                    if (nextPoint == p3d(arc.EndPoint[false], nextPoint.z))
                        break; //прошли всю дугу
                    else
                        nextPoint = curGoalCoordts;
                }
                else
                    nextPoint = p3d(arc.GetPointLen(len), nextPoint.z);

                TaskGoToRelativeCoorts(nextPoint);

                //А если робот передвинется раньше, чем ПК посчитает следующию точку??
                mCController.CommandHandle.WaitOne();//следующая команда будет отправлена тогда, когда завершится предыдущая операция
                SendTask();

                len = len + intrpStep;

            } while (true);
        }



        /*-----------------------------------реализация прочих методов и функций-------------------------------------------*/

        private void TaskGoToRelativeCoorts(Vector3d goalCoordts)
        {
            if ((owner == null) || (iKSolver == null) || (mCController == null)) return;
            if (IKSolverType.IK3DOF != iKSolver.GetType()) return;

            owner.CurWorkCoorts = goalCoordts;
            owner.CurWorkCoortsToAbs();

            iKSolver.SolveIK(owner.AbsWorkCoorts.x, owner.AbsWorkCoorts.y, owner.AbsWorkCoorts.z);

            if (owner.DoLog)
            {
                AddLog("----");
                AddLog(String.Format("CommandSender.TaskGoToRelativeCoorts(): owner.CurWorkCoorts: ({0}, {1}, {2})", owner.CurWorkCoorts.x, owner.CurWorkCoorts.y, owner.CurWorkCoorts.z));//---??----
                AddLog(String.Format("CommandSender.TaskGoToRelativeCoorts(): iKSolver.QDeg: ({0}, {1}, {2})", iKSolver.QDeg[0], iKSolver.QDeg[1], iKSolver.QDeg[2]));
            }

            mCController.TaskAngles(iKSolver.QDeg[0], iKSolver.QDeg[1], iKSolver.QDeg[2]);

        }

        private bool GetXYZ_FromStr(string str, ref Vector3d refV)
        {
            double[] XYZ_D = new double[] { refV.x, refV.y, refV.z };
            bool res = GetNumbers_FromStr(str, new char[] { 'X', 'Y', 'Z' }, ref XYZ_D);
            refV.x = XYZ_D[0];
            refV.y = XYZ_D[1];
            refV.z = XYZ_D[2];

            return res;
        }
        private bool GetNumbers_FromStr(string str, char[] charNumbers, ref double[] refNumbers)
        {
            if (str == "") return false;
            if (refNumbers.Length != charNumbers.Length) return false;

            string[] strNumbers = new string[charNumbers.Length];
            int[] indNumbers = new int[charNumbers.Length];
            //int[] indComment = { str.IndexOf('('), str.IndexOf(')') };//индексы скобок - открывающей и закрывающей комментарий

            for (int j = 0; j < indNumbers.Length; j++)
            {
                indNumbers[j] = str.IndexOf(charNumbers[j]);
            }

            for (int j = 0; j < indNumbers.Length; j++)
            {
                if (indNumbers[j] < 0) continue; //нет такого символа в строке 

                //if (SymbolInCommentSection(indNumbers[j], indComment[0], indComment[1])) break;//символ в комментарии//?? --- optimization --- ??


                if ((indNumbers[j] + 1) > str.Length - 1) break;//??

                for (int i = indNumbers[j] + 1; i < str.Length; i++)
                {
                    if (str[i] == ',')
                    {
                        Error("В G коде не допустим символ ',' !");//??
                        return false;
                    }
                    if (Char.IsDigit(str[i]) || (str[i] == '.') || (str[i] == '-'))
                        strNumbers[j] += str[i];
                    else break;
                }

            }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");//??
            bool AllNumbrsNull = true;
            for (int j = 0; j < refNumbers.Length; j++)
            {
                AllNumbrsNull = AllNumbrsNull && (strNumbers[j] == null);

                if ((strNumbers[j] != null) && (strNumbers[j] != ""))
                    refNumbers[j] = Convert.ToDouble(strNumbers[j]);
            }

            return !AllNumbrsNull;
        }

        private void Error(string strErr)
        {
            owner.Error(strErr);
            stopCycle = true;
        }

        private void UpdateStatus(string str)
        {
            owner.UpdateStatus(str);
        }

        private GCommand GetGCommandInStr(string strCmd)
        {
            GCommand res = GCommand.NONE;

            if ((strCmd == null) || (strCmd == "")) return res;

            int[] indComment = { strCmd.IndexOf('('), strCmd.IndexOf(')') };//индексы скобок - открывающей и закрывающей комментарий            
            if ((indComment[0] == 0) && (indComment[1] == strCmd.Count() - 1)) return res; // скобочки в начале и в конце строки  - вся строка в комментарии 

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

        protected bool SymbolInCommentSection(int symbolIndex, int CommentSectionStartIndex, int CommentSectionStopIndex)
        {
            bool res = true;

            if (symbolIndex < 0) return res; //не существует символа
            if ((CommentSectionStartIndex >= 0) && (CommentSectionStartIndex < symbolIndex))//'(' существует и стоит перед символом - возможно символ в комментарии
                if (CommentSectionStopIndex < 0) return res;                           //')' не существует, значит символ точно в комментарии
                else if (CommentSectionStopIndex > symbolIndex) return res;          //')' существует и она после символа, значит символ в комментарии

            res = false;
            return res;
        }



    }
}
