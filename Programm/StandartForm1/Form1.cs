using Mathcad;
using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Threading;
using System.Timers;
using System.IO.Ports;
using System.Windows.Forms;
using InverseKinematics;

namespace StandartForm1
{

    public partial class Form1 : Form
    {
        RobkoIKSolver robkoIKSolver = new RobkoIKSolver(0,190, 178, 178, 82);
        Mathcad.Application mc;
        Mathcad.Worksheet ws;

        static string path = Directory.GetCurrentDirectory();
        string mathFile = path + @"\robot_1.xmcd";//файл маткада
        string report = path + @"\report.txt";//файл отчета

        double x = 0, y = 300, z = 100;
        double x0, y0, z0;
        int XyzDelta;
        Boolean TuskComplited, TuskSend;

        byte[] buffer = new byte[1];

        System.Timers.Timer aTimer;
        private delegate void updateDelegate(string txt);

        string PortText;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {

            mc = new Mathcad.Application();
            mc.Visible = false;
            Mathcad.Worksheets mwk = mc.Worksheets;
            ws = mwk.Open(mathFile);
            Thread.Sleep(50);


            PortTurnOn(serialPort1); //включить порт
            XyzDisplay();
            if (File.Exists(report)) { richTextBox2.Text = File.ReadAllText(report); }

            aTimer = new System.Timers.Timer(500);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (chkRecievPrt.Checked)
            {

                bool PortOpen = false;
                Invoke(new Action(() => { PortOpen = serialPort1.IsOpen; }));

                if (!PortOpen)
                {
                    Invoke(new Action(() => { PortTurnOn(serialPort1); }));
                    return;
                }
                try // так как после закрытия окна таймер еще может выполнится или предел ожидания может быть превышен
                {
                    // удалим накопившееся в буфере
                    // serialPort1.DiscardInBuffer();


                    // считаем последнее значение                     
                    string strFromPort = serialPort1.ReadExisting();                   
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mc.ActiveWorksheet.Close(Mathcad.MCSaveOption.mcDiscardChanges);
            mc.Quit(Mathcad.MCSaveOption.mcDiscardChanges);

            aTimer.Enabled = false;
            serPortClose(serialPort1);

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
            richTextBox2.Text = "Углы  β,δ,α | x,y,z в программе | x,y,z в реальности\n----------------------------------------------------------------------------------";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }


        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllLines(report, richTextBox2.Text.Split('\n'));
        }

        private void button8_Click(object sender, EventArgs e)//задать нулевую точку/---------------------------------------------доработать-----------------------
        {
            x0 = x;
            y0 = y;
            z0 = z;
        }


        private void SendBtn_Click(object sender, EventArgs e)
        {
            bool angls = (data_angels.Text != "");//углы заданы
            bool coords = (data_coordinates.Text != "");//координаты заданы
            string CheckedNbrs = "";
            if (serialPort1.IsOpen)
            {
                if (angls && !coords)//углы заданы, а координы нет
                {
                    CheckedNbrs = data_angels.Text;

                    //CheckNumbers() - проверить введенные числа и окурглить до 2 цифр после точки
                    if (CheckNumbers(ref CheckedNbrs))//проверить прошели ли проверку, не обнулились ли
                    {
                        data_angels.Text = CheckedNbrs;

                        serialPort1.Write(data_angels.Text);
                        serialPort1.DiscardInBuffer();

                        //если режим отладки включен, написать углы и координаты в окошке
                        if (checkBox3.Checked) richTextBox2.Text += "\n" + data_angels.Text + " | " + String.Format("{0},{1},{2}", x, y, z) + " || ";
                    }

                }
                else if (coords && !angls)//координы  заданы, а углы нет
                {
                    CheckedNbrs = data_coordinates.Text;

                    //CheckNumbers() - проверить введенные числа и окурглить до 2 цифр после точки
                    if (CheckNumbers(ref CheckedNbrs))//проверить прошели ли проверку, не обнулились ли
                    {
                        data_coordinates.Text = CheckedNbrs;

                        String[] coordinates = data_coordinates.Text.Split(',');//отделить числа запятой
                        x = Double.Parse(coordinates[0]);
                        y = Double.Parse(coordinates[1]);
                        z = Double.Parse(coordinates[2]);
                        SendAngles();
                    }

                }
                //заданы и углы, и координаты
                else if (angls && coords)
                {
                    MessageBox.Show("Введите либо углы, либо координаты");
                }
            }

        }

        public bool CheckNumbers(ref string nbrs)
        {
            String[] gh = nbrs.Split(',');//отделить числа запятой

            //проверить введенные числа
            if (gh.Length != 3 || gh.Contains(""))
            {

                MessageBox.Show("Введите 3 числа в формате: 45.25,50.2,90");
                return false;

            }

            double[] tmp = new double[gh.Length];
            //сделать double  с раделителем - ".", а не ","
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            nbrs = "";
            for (int i = 0; i < gh.Length; i++)
            {
                try
                {
                    tmp[i] = Math.Round(Convert.ToDouble(gh[i]), 2);
                    if (i != gh.Length - 1) nbrs += Convert.ToString(tmp[i]) + ",";
                    else nbrs += Convert.ToString(tmp[i]);
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

        public void SendAngles(int XyzDelta)
        {
            if (serialPort1.IsOpen)
            {
                double xForStraightZero = x;
                string β3;
                string δ3;
                string α3;

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
                        x = x + checkedDelta;
                        break;
                    case 1:
                        y = y + checkedDelta;
                        break;
                    case 2:
                        x = x - checkedDelta;
                        break;
                    case 3:
                        y = y - checkedDelta;
                        break;
                    case 4:
                        z = z - checkedDelta;
                        break;
                    case 5:
                        z = z + checkedDelta;
                        break;
                    default:
                        break;

                }


                ws.SetValue("x", x + 2);//!!
                ws.SetValue("y", y + 3);
                ws.SetValue("z", z + 22);//!!!!!!!!!!!!!



                //------ 
                // robkoIKSolver.SolveIK(x, y, z);   
                robkoIKSolver.SolveIK(x+2, y+3, z+22);//------
                //------

                Thread.Sleep(50);//---------------------------------избавиться---------------------------------------------------------
                ws.Recalculate();
                Thread.Sleep(50);
                β3 = Convert.ToString(Math.Round((ws.GetValue("β3") as INumericValue).Real, 2)).Replace(',', '.'); //string α3
                δ3 = Convert.ToString(Math.Round((ws.GetValue("δ3") as INumericValue).Real, 2)).Replace(',', '.');
                α3 = Convert.ToString(Math.Round((ws.GetValue("α3") as INumericValue).Real, 2)).Replace(',', '.');
                try
                {
                    float a1 = float.Parse(β3, System.Globalization.CultureInfo.InvariantCulture);
                    float a2 = float.Parse(δ3, System.Globalization.CultureInfo.InvariantCulture);
                    float a3 = float.Parse(α3, System.Globalization.CultureInfo.InvariantCulture);

                    var floatArray = new float[] { a1, a2, a3 };
                    var byteArray = new byte[floatArray.Length * 4];

                    Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);

                    serialPort1.Write(byteArray, 0, byteArray.Length);
                    serialPort1.Read(buffer, 0, 1);//------------
                    XyzDisplay();
                }
                catch
                {
                    ErrPort();
                }
                if (checkBox3.Checked) richTextBox2.Text += "\n" + String.Format("{0},{1},{2}", β3, δ3, α3) + " | " + String.Format("{0},{1},{2}", x, y, z) + " | ";

            }
        }



        public void XyzDisplay()
        {
            label1.Text = String.Format("{0},{1},{2}", Convert.ToString(Math.Round(x, 2)).Replace(',', '.'), Convert.ToString(Math.Round(y, 2)).Replace(',', '.'), Convert.ToString(Math.Round(z, 2)).Replace(',', '.'));
        }

        private void PortTurnOn(SerialPort serPort)//включить порт
        {

            serPortClose(serialPort1);
            if (FoundArdnoPort())
            {
                try
                {
                    serialPort1.Open();
                    Thread.Sleep(50);
                    SendAngles();
                    OkPort();
                }

                catch
                {
                    ErrPort();
                }
            }
            else ErrPort();
        }

        private void PortTurnOff(SerialPort serPort)//выключить порт----------------------
        {
            serPortClose(serialPort1);
        }

        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            ErrPort();
        }

        private bool FoundArdnoPort()
        {
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);
            //comboBox1.Items.AddRange(SerialPort.GetPortNames());
            try
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    string desc = item["Description"].ToString();
                    string deviceId = item["DeviceID"].ToString();
                    if (desc.Contains("Arduino"))
                    {
                        // serialPort1.PortName = deviceId;                        
                        return true;
                    }

                }
                serialPort1.PortName = "COM7";
                return true;
            }
            catch (ManagementException e)
            {
                return false;
            }
            return false;
        }


        private void data_angels_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(",")) | (e.KeyChar == Convert.ToChar(".")) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }

        private void паротвклToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PortTurnOn(serialPort1);
        }

        public bool stopwhile;
        private void button9_Click(object sender, EventArgs e)
        {
            int TimeWait = -1;
            if (textBox1.Text != "") TimeWait = Int32.Parse(textBox1.Text);
            if (TimeWait < 400)
            {
                MessageBox.Show("Введите число большее 400");
                return;
            }

            string[] lines = richTextBox3.Text.Split('\n');

            stopwhile = false;
            if (CycleChkBox.Checked)
            {
                do
                {
                    if (stopwhile == false)
                    {
                        SendListCoodinates(lines, TimeWait);
                        System.Windows.Forms.Application.DoEvents(); // Эту строку не трогать, это для того чтобы и другие события работали
                    }
                    else
                    {
                        break; //Прерываем цикл
                    }
                } while (true);
            }
            else SendListCoodinates(lines, TimeWait);

        }
        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //  buffer += serialPort1.ReadExisting();
           // serialPort1.Read(buffer, 0, buffer.Length);
            //test for termination character in buffer
            if (buffer[0] == 33) // Синхронизирующий байт. После этого еще делаем проверку контрольной суммы.
            {
                TuskComplited = buffer[1] == 64;
            }
        }

        void SendListCoodinates(string[] lines, int TimeWait)
        {
            String[] coordinates;
            for (int i = 2; i < lines.Length; i++)
            {
                string CheckedNbrs = lines[i];
                //CheckNumbers() - проверить введенные числа и окурглить до 2 цифр после точки
                if (CheckNumbers(ref CheckedNbrs))//проверить прошели ли проверку, не обнулились ли
                {
                    coordinates = CheckedNbrs.Split(',');//отделить числа запятой
                    x = Double.Parse(coordinates[0]);
                    y = Double.Parse(coordinates[1]);
                    z = Double.Parse(coordinates[2]);

                    SendAngles();

                }

            }
        }
        private void CycleChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CycleChkBox.Checked) stopwhile = false;
            else stopwhile = true;

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //  bool received;
            //  received = false;
            //  // if ((!TuskComplited) && (serialPort1.ReadByte() == 64)) { TuskComplited = true; }
            //  //serialPort1.DiscardInBuffer();

            //  // prevent error with closed port to appears
            //  if (!serialPort1.IsOpen)
            //      return;

            //  // if (serialPort1.BytesToRead == 2){ serialPort1.Read(buffer, 0, 2); received = true; }
            //  // else if (serialPort1.BytesToRead == 1) { serialPort1.Read(buffer, 0, 1); received = false; }
            //  serialPort1.Read(buffer, 0, 1);
            ////  richTextBox1.BeginInvoke(new updateDelegate(updateTextBox),Convert.ToString(buffer[0]));
            //  //test for termination character in buffer

            //  if ((TuskSend == true) && (buffer[0] == 33) )// Синхронизирующий байт. После этого еще делаем проверку контрольной суммы.
            //  {
            //      // Invoke(new Action(() => { TuskComplited = true; }));
            //      TuskComplited = true;
            //      return;
            //  }

            //  updateTextBox("!!!!!ddd");
           // PortText += serialPort1.ReadExisting();//------------------------------------
        }

        private void data_coordinates_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(",")) | (e.KeyChar == Convert.ToChar(".")) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }
        private void serPortClose(SerialPort sPort)
        {
            //  var portExists = SerialPort.GetPortNames().Any(x => x == sPort.PortName);
            //   if (portExists)
            try
            {
                if (sPort.IsOpen)
                    sPort.Close();
            }
            catch { }


        }
        private void ErrPort()
        {
            serPortClose(serialPort1);//---------------------------------------------------------------????
            StatusLabel.Text = "соединение отсутствует";
        }
        private void OkPort()
        {
            StatusLabel.Text = "соединено с Arduino";
        }

    }


}


