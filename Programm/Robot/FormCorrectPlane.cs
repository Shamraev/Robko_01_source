using RobotSpace.Properties;
using System;
using System.Windows.Forms;
using VecLib;
using static VecLib.VecLibMethods;

namespace RobotSpace
{
    public partial class FormCorrectPlane : Form
    {
        public MainForm mf;

        protected Vector3d M0, M1, M2;
        //protected Plane pl;

        public FormCorrectPlane()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string txtM0 = textBoxM0.Text;
            string txtM1 = textBoxM1.Text;
            string txtM2 = textBoxM2.Text;
            if (VectorFromStr(txtM0, out M0) && VectorFromStr(txtM1, out M1) && VectorFromStr(txtM2, out M2))
            {
                mf.CorrectPlanePts = new Vector3d [] {M0, M1, M2};
                this.Close();
            }
            else
                MessageBox.Show("Введите 3 числа в формате: 45.25,50.2,90"); 
            
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            textBoxM0.Text = Settings.Default.Properties["CorrectPlaneM0"].DefaultValue as string;
            textBoxM1.Text = Settings.Default.Properties["CorrectPlaneM1"].DefaultValue as string;
            textBoxM2.Text = Settings.Default.Properties["CorrectPlaneM2"].DefaultValue as string; 
        }

        private void FormCorrectPlane_Shown(object sender, EventArgs e)
        {
            textBoxM0.Text = VectorToStr(mf.CorrectPlanePts[0]);
            textBoxM1.Text = VectorToStr(mf.CorrectPlanePts[1]);
            textBoxM2.Text = VectorToStr(mf.CorrectPlanePts[2]);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxM0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar("-")) | (e.KeyChar == Convert.ToChar(",")) | (e.KeyChar == Convert.ToChar(".")) | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }
    }
}
