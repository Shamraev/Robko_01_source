using System;
using System.Windows.Forms;
using StandartMainForm;
using VecLib;

namespace RobotSpace
{
    public partial class FormSetCurWorkCoordts : Form
    {
        public MainForm mf;

        public FormSetCurWorkCoordts()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //сделать double  с раделителем - ".", а не ","
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Vector3d v;
            v.x = Math.Round(Convert.ToDouble(textBoxX.Text), 2);
            v.y = Math.Round(Convert.ToDouble(textBoxY.Text), 2);
            v.z = Math.Round(Convert.ToDouble(textBoxZ.Text), 2);

            ((MainForm)mf).CurWorkCoorts = v;

            ((MainForm)mf).CoortsOffset = Vector3d.Subtract(((MainForm)mf).AbsWorkCoorts, ((MainForm)mf).CurWorkCoorts);

            ((MainForm)mf).XyzDisplay();
            this.Close();
        }

        private void textBoxX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(".")) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
