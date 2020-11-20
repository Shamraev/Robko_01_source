using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using InverseKinematics;
using VecLib;
using static VecLib.VecLibMethods;
using MCControl;
using CommandSend;
using BusControl;
using System.Threading;
using System.Windows.Media;
using System.ComponentModel;

namespace RobotSpace
{

    public partial class MainForm : Form
    {
        IKSolver3DOF iKSolver3DOF;
        public Vector3d AbsWorkCoorts = new Vector3d(0, 257, 368);//абсолютные координаты//??
        public Vector3d CurWorkCoorts = new Vector3d(0, 257, 368);//относительные координаты
        public Vector3d CoortsOffset = new Vector3d(0, 0, 0);    // смещение для перевода из относительных координат в абсолютные и наоборот
                                                                 //AbsWorkCoorts = CurWorkCoorts + CoortsOffset

        private string[] portnames;
        private string robotPortName;

        public string RobotPortName { get { return robotPortName; } set { } }

        MCController mCController;
        CommandSender commandSender;

        static string AppPath = Directory.GetCurrentDirectory();
        string report = AppPath + @"\report.txt";//файл отчета        

        Boolean TuskComplited, TuskSend;

        byte[] buffer = new byte[1];

        System.Timers.Timer aTimer;
        private delegate void updateDelegate(string txt);

        string PortText;

        private bool _DoLog;
        public bool DoLog { get { return _DoLog; } set { } }
        static string _PathToLog = AppPath + @"\Logs";
        static string _LogFileName = String.Format("Log_{0}.txt", DateTime.Now.Ticks);
        TextWriter tw;

        private bool _DoCorrect;
        /// <summary>
        /// включить корректирование по корректирующей плоскости
        /// </summary>
        public bool DoCorrect { get { return _DoCorrect; } set { _DoCorrect = value; } }


        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadSettings();

            ToolTipInit();
            ResetCoordnts();
            if (DoLog) LogCreate();
            IKSolverCreate();
            MCControllerCreate();
            СommandSenderCreate();

            XyzDisplay();
            if (File.Exists(report)) { richTextBox2.Text = File.ReadAllText(report); }

            aTimer = new System.Timers.Timer(500);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            Drawer.Init();
            DrawerClear();
        }
        public void DrawerClear()
        {
            Invoke(new Action(() =>
            {
                if (Drawer != null)
                Drawer.Clear();
                XyzDisplay();
            }));
        }
        protected void ToolTipInit()
        {
            ToolTip toolTip = new ToolTip();//??

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip.SetToolTip(this.buttonRobot_FindAndGoToZeros, "Найти нули робота");
            toolTip.SetToolTip(this.buttonGripperGrip, "Сжать схват");
            toolTip.SetToolTip(this.buttonGripperUngrip, "Разжать схват");
            toolTip.SetToolTip(this.buttonGCodeStart, "Начать выполнение G кода");
            toolTip.SetToolTip(this.buttonGCodeStop, "Остановаить выполнение G кода");
        }
        private void LogCreate()
        {
            if (!Directory.Exists(_PathToLog)) Directory.CreateDirectory(_PathToLog);
            tw = new StreamWriter(Path.Combine(_PathToLog, _LogFileName), true);
            AddLog("Log file Date: " + DateTime.Now);
            AddLog("********************************");
        }
        public void AddLog(string str)
        {
            if (tw == null) return;

            tw.WriteLine(str + "\n");
        }

        private Plane CreateCorrectPlane()
        {
            //M0, M1, M2 - точки, полученные в реальности; координата z от плоскости стола 
            //(не важно откуда, важно для всех точек координата z считалась относительно одного и того же)
            //в глобальных координатах z~190
            Vector3d M0 = new Vector3d(0, 200, 5);
            Vector3d M1 = new Vector3d(-100, 400, 5);
            Vector3d M2 = new Vector3d(100, 400, 7.5);

            Plane pl = new Plane(M0, M1, M2);

            return pl;
        }
        protected void IKSolverCreate()
        {
            iKSolver3DOF = new IKSolver3DOF(0, 190, 178, 177, 80);//d4 = 178; d5 = 82; 
            iKSolver3DOF.DoCorrect = DoCorrect;
            iKSolver3DOF.CorrectPlane = CreateCorrectPlane();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            aTimer.Enabled = false;
            SaveSettings();
            LogClose();
            //commandSender.Stop();
            Environment.Exit(0);//??
        }
        protected void LoadSettings()
        {
            if (Properties.Settings.Default.RobotPortName != null)
                robotPortName = Properties.Settings.Default.RobotPortName;
            ToolStripMenuItemDoLog.Checked = Properties.Settings.Default.DoLog;
            ToolStripMenuItemCorrectPlane.Checked = Properties.Settings.Default.DoCorrect;
            DoCorrect = ToolStripMenuItemCorrectPlane.Checked;//??убрать DoCorrect??
            _DoLog = ToolStripMenuItemDoLog.Checked;
        }
        private void SaveSettings()
        {
            Properties.Settings.Default.RobotPortName = robotPortName;
            Properties.Settings.Default.DoLog = ToolStripMenuItemDoLog.Checked;
            Properties.Settings.Default.DoCorrect = ToolStripMenuItemCorrectPlane.Checked;

            //apply the changes to the settings file  
            Properties.Settings.Default.Save();
        }

        private void LogClose()
        {
            if (tw == null) return;

            tw.Close();
        }



        protected void GetPortNames()
        {
            if (PortNames == null) return;

            portnames = null;//---            
            PortNames.DropDown.Items.Clear();

            portnames = SerialPort.GetPortNames();
            // Проверяем есть ли доступные
            if (portnames == null)
            {
                robotPortName = null;
                UpdateStatus("COM PORT not found");
            }
            else
                foreach (string portName in portnames)
                {
                    //добавляем доступные COM порты в список
                    PortNames.DropDown.Items.Add(portName);
                    if (portName == robotPortName)
                        (PortNames.DropDown.Items[PortNames.DropDown.Items.Count - 1] as ToolStripMenuItem).Checked = true;

                    (PortNames.DropDown.Items[PortNames.DropDown.Items.Count - 1] as ToolStripMenuItem).CheckOnClick = true;

                }

        }
        private void OnTimedEvent(object sender, ElapsedEventArgs e)//---запрятать в отдельный класс??
        {
            if (chkRecievPrt.Checked)
            {
                if (mCController == null) return;

                bool PortOpen = false;
                Invoke(new Action(() => { PortOpen = mCController.SerialPortIsOpen(); }));

                if (!PortOpen)
                {
                    Invoke(new Action(() => { mCController.PortTurnOn(robotPortName); }));
                    return;
                }
                try // так как после закрытия окна таймер еще может выполнится или предел ожидания может быть превышен
                {
                    // удалим накопившееся в буфере
                    // serialPort1.DiscardInBuffer();


                    // считаем последнее значение                     
                    string strFromPort = mCController.SerialPortReadExisting();
                    richTextBox1.BeginInvoke(new updateDelegate(updateTextBox), strFromPort);
                    PortText = "";
                }
                catch (Exception ex)
                {
                    richTextBox1.BeginInvoke(new updateDelegate(updateTextBox), ex.Message);
                }
            }
        }

        private void updateTextBox(string txt)
        {
            if (chkRecievPrt.Checked)
            {
                richTextBox1.Text += txt;

                if (chkScrll.Checked)
                {
                    richTextBox1.SelectionStart = richTextBox1.TextLength;
                    richTextBox1.ScrollToCaret();
                }
            }

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*|Png files (*.png)|*.png";
            saveFileDialog1.ShowDialog();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SendAngles(0);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SendAngles(1);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SendAngles(2);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SendAngles(3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendAngles(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SendAngles(5);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            richTextBox2.Text = "Углы  β,δ,α | x,y,z в программе | x,y,z в реальности\n---------------------------------------------------------------------";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }


        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllLines(report, richTextBox2.Text.Split('\n'));
        }


        private void SendBtn_Click(object sender, EventArgs e)
        {
            if (mCController == null) return;
            //if (!mCController.SerialPortIsOpen()) return;

            bool angls = (data_angels.Text != "");//углы заданы
            bool coords = (data_coordinates.Text != "");//координаты заданы
            string CheckedNbrs = "";
            double[] NumbersDouble;


            if (angls && !coords)//углы заданы, а координы нет
            {
                CheckedNbrs = data_angels.Text;

                //CheckNumbers() - проверить введенные числа и окурглить до 2 цифр после точки
                if (CheckNumbers(ref CheckedNbrs, out NumbersDouble))//проверить прошели ли проверку, не обнулились ли
                {
                    data_angels.Text = CheckedNbrs;


                    SendAnglesToRobot(NumbersDouble[0], NumbersDouble[1], NumbersDouble[2]);

                    //если режим отладки включен, написать углы и координаты в окошке
                    if (checkBox3.Checked) richTextBox2.Text += "\n" + data_angels.Text + " | " + String.Format("{0},{1},{2}", AbsWorkCoorts.x, AbsWorkCoorts.y, AbsWorkCoorts.z) + " || ";
                }

            }
            else if (coords && !angls)//координаты  заданы, а углы нет
            {
                CheckedNbrs = data_coordinates.Text;

                //CheckNumbers() - проверить введенные числа и окурглить до 2 цифр после точки

                if (CheckNumbers(ref CheckedNbrs, out NumbersDouble))//проверить прошели ли проверку, не обнулились ли
                {
                    data_coordinates.Text = CheckedNbrs;

                    AbsWorkCoorts.x = NumbersDouble[0];//??может в относительных считать??
                    AbsWorkCoorts.y = NumbersDouble[1];
                    AbsWorkCoorts.z = NumbersDouble[2];
                    SendAngles();
                }

            }
            //заданы и углы, и координаты
            else if (angls && coords)
            {
                MessageBox.Show("Введите либо углы, либо координаты");
            }


        }

        public bool CheckNumbers(ref string nbrs, out double[] NumbersDouble)
        {

            String[] gh = nbrs.Split(',');//отделить числа запятой
            NumbersDouble = new double[gh.Length];
            //проверить введенные числа
            if (gh.Length != 3 || gh.Contains(""))
            {

                MessageBox.Show("Введите 3 числа в формате: 45.25,50.2,90");
                return false;

            }


            //сделать double  с раделителем - ".", а не ","
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            nbrs = "";
            for (int i = 0; i < gh.Length; i++)
            {
                try
                {
                    NumbersDouble[i] = Math.Round(Convert.ToDouble(gh[i]), 2);
                    if (i != gh.Length - 1) nbrs += Convert.ToString(NumbersDouble[i]) + ",";
                    else nbrs += Convert.ToString(NumbersDouble[i]);
                }
                catch
                {

                    MessageBox.Show("Введите 3 числа в формате: 45.25,50.2,90");
                    return false;
                }

            }
            return true;
        }


        public void SendAngles()
        {
            SendAngles(10);
        }

        public void SendAngles(int XyzDelta)//??
        {
            if (mCController == null) return;
            //if (!mCController.SerialPortIsOpen()) return;//??

            double xForStraightZero = AbsWorkCoorts.x;

            int checkedDelta = 0;
            if (radioButton10.Checked)
            {
                checkedDelta = 10;
            }
            if (radioButton50.Checked)
            {
                checkedDelta = 50;
            }
            if (radioButton100.Checked)
            {
                checkedDelta = 100;
            }
            if (radioButton200.Checked)
            {
                checkedDelta = 200;
            }

            switch (XyzDelta)
            {
                case 0:
                    AbsWorkCoorts.x = AbsWorkCoorts.x + checkedDelta;
                    break;
                case 1:
                    AbsWorkCoorts.y = AbsWorkCoorts.y + checkedDelta;
                    break;
                case 2:
                    AbsWorkCoorts.x = AbsWorkCoorts.x - checkedDelta;
                    break;
                case 3:
                    AbsWorkCoorts.y = AbsWorkCoorts.y - checkedDelta;
                    break;
                case 4:
                    AbsWorkCoorts.z = AbsWorkCoorts.z - checkedDelta;
                    break;
                case 5:
                    AbsWorkCoorts.z = AbsWorkCoorts.z + checkedDelta;
                    break;
                default:
                    break;

            }

            iKSolver3DOF.SolveIK(AbsWorkCoorts.x, AbsWorkCoorts.y, AbsWorkCoorts.z);
            double a1, a2, a3;
            a1 = 0; a2 = 0; a3 = 0;

            a1 = iKSolver3DOF.QDeg[0];
            a2 = iKSolver3DOF.QDeg[1];
            a3 = iKSolver3DOF.QDeg[2];

            SendAnglesToRobot(a1, a2, a3);

            XyzDisplay();


            if (checkBox3.Checked) richTextBox2.Text += "\n" + String.Format("{0},{1},{2}", a1, a2, a3) + " | " + String.Format("{0},{1},{2}", AbsWorkCoorts.x, AbsWorkCoorts.y, AbsWorkCoorts.z) + " | ";


        }


        protected void SendAnglesToRobot(double a1, double a2, double a3)
        {
            if (mCController == null) return;

            mCController.SendAngles(a1, a2, a3);
        }
        public void XyzDisplay()
        {
            Invoke(new Action(() =>
            {

                labelX.Text = CoortToString(AbsWorkCoorts.x);
                labelY.Text = CoortToString(AbsWorkCoorts.y);
                labelZ.Text = CoortToString(AbsWorkCoorts.z);

                AbsWorkCoortsToCur();

                buttonCurWorkX.Text = CoortToString(CurWorkCoorts.x);
                buttonCurWorkY.Text = CoortToString(CurWorkCoorts.y);
                buttonCurWorkZ.Text = CoortToString(CurWorkCoorts.z);
                    
                if (Drawer!=null)
                    Drawer.AddPoint(AbsWorkCoorts.x, AbsWorkCoorts.y, AbsWorkCoorts.z);
            }));
        }
        protected string CoortToString(double coordt)
        {
            return Convert.ToString(Math.Round(coordt, 2)).Replace(',', '.');
        }

        public void AbsWorkCoortsToCur()
        {
            CurWorkCoorts = Vector3d.Subtract(AbsWorkCoorts, CoortsOffset);
        }
        public void CurWorkCoortsToAbs()
        {
            AbsWorkCoorts = Vector3d.Add(CurWorkCoorts, CoortsOffset);
        }



        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            mCController.ErrPort();
        }


        private void data_angels_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar("-")) | (e.KeyChar == Convert.ToChar(",")) | (e.KeyChar == Convert.ToChar(".")) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }

        public bool stopwhile;
       
        private void dataReceived(object sender, SerialDataReceivedEventArgs e)//---------
        {
            //  buffer += serialPort1.ReadExisting();
            // serialPort1.Read(buffer, 0, buffer.Length);
            //test for termination character in buffer
            if (buffer[0] == 33) // Синхронизирующий байт. После этого еще делаем проверку контрольной суммы.
            {
                TuskComplited = buffer[1] == 64;
            }
        }

        void SendListCoodinates(string[] lines, int TimeWait)//-----
        {
            double[] coordinates;
            for (int i = 2; i < lines.Length; i++)
            {
                string CheckedNbrs = lines[i];
                //CheckNumbers() - проверить введенные числа и окурглить до 2 цифр после точки
                if (CheckNumbers(ref CheckedNbrs, out coordinates))//проверить прошели ли проверку, не обнулились ли
                {
                    AbsWorkCoorts.x = coordinates[0];
                    AbsWorkCoorts.y = coordinates[1];
                    AbsWorkCoorts.z = coordinates[2];
                    SendAngles();
                }

            }
        }
        
        private void buttonCurWorkX_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CoortsOffset.x = AbsWorkCoorts.x;
                CurWorkCoorts.x = 0;
                buttonCurWorkX.Text = Convert.ToString(CurWorkCoorts.x);
            }
            if (e.Button == MouseButtons.Right)
            {
                ShowFormSetCurWorkCoordts();
            }
        }

        private void buttonCurWorkY_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CoortsOffset.y = AbsWorkCoorts.y;
                CurWorkCoorts.y = 0;
                buttonCurWorkY.Text = Convert.ToString(CurWorkCoorts.y);
            }
            if (e.Button == MouseButtons.Right)
            {
                ShowFormSetCurWorkCoordts();
            }

        }

        private void buttonCurWorkZ_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CoortsOffset.z = AbsWorkCoorts.z;
                CurWorkCoorts.z = 0;
                buttonCurWorkZ.Text = Convert.ToString(CurWorkCoorts.z);
            }
            if (e.Button == MouseButtons.Right)
            {
                ShowFormSetCurWorkCoordts();
            }

        }
        private void ShowFormSetCurWorkCoordts()
        {
            FormSetCurWorkCoordts fm = new FormSetCurWorkCoordts();
            fm.textBoxX.Text = buttonCurWorkX.Text;
            fm.textBoxY.Text = buttonCurWorkY.Text;
            fm.textBoxZ.Text = buttonCurWorkZ.Text;

            fm.mf = this;
            fm.StartPosition = FormStartPosition.CenterParent;
            fm.ShowDialog();
        }

        private void ShowFormCorrectPlane()
        {
            FormCorrectPlane fm = new FormCorrectPlane();

            fm.mf = this;
            fm.StartPosition = FormStartPosition.CenterParent;
            fm.Show();
        }        

        private void data_coordinates_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar("-")) | (e.KeyChar == Convert.ToChar(",")) | (e.KeyChar == Convert.ToChar(".")) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }

        private void buttonGCodeStart_Click(object sender, EventArgs e)
        {
            if (commandSender.CycleStarted)//уже запущен цикл работает, значит пауза цикла
            {
                commandSender.Pause();//при вызывании этого метода приостанавливает либо продолжает

                if (commandSender.CyclePause)
                    buttonGCodeStart.BackgroundImage = Properties.Resources.start_button;
                else
                    buttonGCodeStart.BackgroundImage = Properties.Resources.pause_button;

            }
            else//цикл не запущен - запустим цикл
            {
                commandSender.CommandList = richTextBoxGCode.Text.Split('\n');
                commandSender.IntrpStep = 1;
                commandSender.Start();
                buttonGCodeStart.BackgroundImage = Properties.Resources.pause_button;
            }

        }

        public void CommandSenderStopped()
        {
            buttonGCodeStart.BackgroundImage = Properties.Resources.start_button;
        }
        private void buttonGCodePause_Click(object sender, EventArgs e)//---
        {

        }
        private void buttonGCodeStop_Click(object sender, EventArgs e)
        {
            DrawerClear();
            commandSender.Stop();
        }
        protected void MCControllerCreate()
        {
            mCController = new MCController(this);
        }
        protected void СommandSenderCreate()
        {
            commandSender = new CommandSender();
            commandSender.Owner = this;
            commandSender.MCController = mCController;
            commandSender.IKSolver = iKSolver3DOF;
        }
        public void UpdateStatus(string str)
        {
            toolStripStatusLabel.Text = str;
            if (DoLog)
            {
                AddLog("********************************");
                AddLog(str);
            }
        }

        public void UpdateTimerState(string str)
        {
            labelGcodeTime.Text = str;
        }



        private void ToolStripMenuItemParameters_Click(object sender, EventArgs e)
        {
            GetPortNames();
        }

        private void PortNames_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            (e.ClickedItem as ToolStripMenuItem).Checked = true;
            robotPortName = e.ClickedItem.Text;
            if (mCController != null) mCController.PortTurnOn(robotPortName);
        }

        public void Error(string strErr)
        {
            MessageBox.Show(strErr);
        }

        private void ToolStripMenuItemSetCorrectPlane_Click(object sender, EventArgs e)
        {
            ShowFormCorrectPlane();
        }

        private void buttonRobot_FindAndGoToZeros_Click(object sender, EventArgs e)
        {
            if (mCController == null) return;

            mCController.TaskFindAndGoToZeros();
            mCController.Send();
        }
        private void buttonGripperGrip_Click(object sender, EventArgs e)
        {
            if (mCController == null) return;

            mCController.TaskGripperGrip();
            mCController.Send();
        }
        private void buttonGripperUngrip_Click(object sender, EventArgs e)
        {
            if (mCController == null) return;

            mCController.TaskGripperUngrip();
            mCController.Send();
        }

        public void DesplayCurGCodeStr(string str)
        {
            Invoke(new Action(() => { labelCurGcode.Text = str; }));

        }
        
        public Vector3d AbsWorkCoorts_ZEROS()
        {
            return new Vector3d(0, 257, 368);

        }

        public void ResetCoordnts()
        {
            AbsWorkCoorts = AbsWorkCoorts_ZEROS();
            CurWorkCoorts = AbsWorkCoorts;
            CurWorkCoorts = Vector3DZeros;
        }
        public void OutFeedStandart(Feed ffeed)
        {
            if (Drawer == null) return;

            if (ffeed==Feed.ffWork)
                Drawer.color = Colors.Blue;
            else if (ffeed == Feed.ffRapid)
                Drawer.color = Colors.Red;
        }

    }


}


