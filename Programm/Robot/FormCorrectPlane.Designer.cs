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
            this.textBoxM0 = new System.Windows.Forms.TextBox();
            this.textBoxM1 = new System.Windows.Forms.TextBox();
            this.textBoxM2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonDefolt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(262, 72);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(91, 31);
            this.buttonCancel.TabIndex = 40;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(262, 35);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(91, 31);
            this.buttonOK.TabIndex = 39;
            this.buttonOK.Text = "ОК";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxM0
            // 
            this.textBoxM0.Location = new System.Drawing.Point(48, 48);
            this.textBoxM0.Name = "textBoxM0";
            this.textBoxM0.Size = new System.Drawing.Size(165, 20);
            this.textBoxM0.TabIndex = 44;
            this.textBoxM0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxM0_KeyPress);
            // 
            // textBoxM1
            // 
            this.textBoxM1.Location = new System.Drawing.Point(48, 77);
            this.textBoxM1.Name = "textBoxM1";
            this.textBoxM1.Size = new System.Drawing.Size(165, 20);
            this.textBoxM1.TabIndex = 45;
            // 
            // textBoxM2
            // 
            this.textBoxM2.Location = new System.Drawing.Point(48, 106);
            this.textBoxM2.Name = "textBoxM2";
            this.textBoxM2.Size = new System.Drawing.Size(165, 20);
            this.textBoxM2.TabIndex = 46;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(19, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 15);
            this.label6.TabIndex = 41;
            this.label6.Text = "M0:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(19, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 15);
            this.label7.TabIndex = 42;
            this.label7.Text = "M1:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(19, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 15);
            this.label8.TabIndex = 43;
            this.label8.Text = "M2:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RobotSpace.Properties.Resources.corret_plane;
            this.pictureBox1.Location = new System.Drawing.Point(31, 155);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(306, 184);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 47;
            this.pictureBox1.TabStop = false;
            // 
            // buttonDefolt
            // 
            this.buttonDefolt.Location = new System.Drawing.Point(262, 109);
            this.buttonDefolt.Name = "buttonDefolt";
            this.buttonDefolt.Size = new System.Drawing.Size(91, 31);
            this.buttonDefolt.TabIndex = 48;
            this.buttonDefolt.Text = "По умолчанию";
            this.buttonDefolt.UseVisualStyleBackColor = true;
            this.buttonDefolt.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // FormCorrectPlane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 364);
            this.Controls.Add(this.buttonDefolt);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxM0);
            this.Controls.Add(this.textBoxM1);
            this.Controls.Add(this.textBoxM2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormCorrectPlane";
            this.Text = "Корректирующая плоскость";
            this.Shown += new System.EventHandler(this.FormCorrectPlane_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.TextBox textBoxM0;
        public System.Windows.Forms.TextBox textBoxM1;
        public System.Windows.Forms.TextBox textBoxM2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonDefolt;
    }
}