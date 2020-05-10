namespace RobotSpace
{
    partial class FormCorrectPlane
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxZ = new System.Windows.Forms.TextBox();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageM0 = new System.Windows.Forms.TabPage();
            this.tabPageM1 = new System.Windows.Forms.TabPage();
            this.tabPageM2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPageM0.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(262, 77);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 31);
            this.buttonCancel.TabIndex = 40;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(262, 40);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 31);
            this.buttonOK.TabIndex = 39;
            this.buttonOK.Text = "ОК";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // textBoxZ
            // 
            this.textBoxZ.Location = new System.Drawing.Point(27, 64);
            this.textBoxZ.Name = "textBoxZ";
            this.textBoxZ.Size = new System.Drawing.Size(82, 20);
            this.textBoxZ.TabIndex = 38;
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(27, 35);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(82, 20);
            this.textBoxY.TabIndex = 37;
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(27, 6);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(82, 20);
            this.textBoxX.TabIndex = 36;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(8, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 15);
            this.label8.TabIndex = 35;
            this.label8.Text = "Z:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(8, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 15);
            this.label7.TabIndex = 34;
            this.label7.Text = "Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(8, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 15);
            this.label6.TabIndex = 33;
            this.label6.Text = "X:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageM0);
            this.tabControl1.Controls.Add(this.tabPageM1);
            this.tabControl1.Controls.Add(this.tabPageM2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 130);
            this.tabControl1.TabIndex = 56;
            // 
            // tabPageM0
            // 
            this.tabPageM0.Controls.Add(this.textBoxX);
            this.tabPageM0.Controls.Add(this.label6);
            this.tabPageM0.Controls.Add(this.label7);
            this.tabPageM0.Controls.Add(this.label8);
            this.tabPageM0.Controls.Add(this.textBoxY);
            this.tabPageM0.Controls.Add(this.textBoxZ);
            this.tabPageM0.Location = new System.Drawing.Point(4, 22);
            this.tabPageM0.Name = "tabPageM0";
            this.tabPageM0.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageM0.Size = new System.Drawing.Size(192, 104);
            this.tabPageM0.TabIndex = 0;
            this.tabPageM0.Text = "M0";
            this.tabPageM0.UseVisualStyleBackColor = true;
            // 
            // tabPageM1
            // 
            this.tabPageM1.Location = new System.Drawing.Point(4, 22);
            this.tabPageM1.Name = "tabPageM1";
            this.tabPageM1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageM1.Size = new System.Drawing.Size(192, 104);
            this.tabPageM1.TabIndex = 1;
            this.tabPageM1.Text = "M1";
            this.tabPageM1.UseVisualStyleBackColor = true;
            // 
            // tabPageM2
            // 
            this.tabPageM2.Location = new System.Drawing.Point(4, 22);
            this.tabPageM2.Name = "tabPageM2";
            this.tabPageM2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageM2.Size = new System.Drawing.Size(192, 104);
            this.tabPageM2.TabIndex = 2;
            this.tabPageM2.Text = "M2";
            this.tabPageM2.UseVisualStyleBackColor = true;
            // 
            // FormCorrectPlane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 173);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormCorrectPlane";
            this.Text = "Корректирующая плоскость";
            this.tabControl1.ResumeLayout(false);
            this.tabPageM0.ResumeLayout(false);
            this.tabPageM0.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.TextBox textBoxZ;
        public System.Windows.Forms.TextBox textBoxY;
        public System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageM0;
        private System.Windows.Forms.TabPage tabPageM1;
        private System.Windows.Forms.TabPage tabPageM2;
    }
}