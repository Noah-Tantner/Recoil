namespace Recoil
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.testLabel = new System.Windows.Forms.Label();
            this.minigunTimer = new System.Windows.Forms.Timer(this.components);
            this.shotgunTimer = new System.Windows.Forms.Timer(this.components);
            this.testLabel2 = new System.Windows.Forms.Label();
            this.countingTimer = new System.Windows.Forms.Timer(this.components);
            this.testLabel3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 20;
            this.gameTimer.Tag = "20";
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // testLabel
            // 
            this.testLabel.AutoSize = true;
            this.testLabel.Location = new System.Drawing.Point(21, 9);
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(73, 16);
            this.testLabel.TabIndex = 0;
            this.testLabel.Text = "Version 1.0";
            // 
            // minigunTimer
            // 
            this.minigunTimer.Tick += new System.EventHandler(this.minigunTimer_Tick);
            // 
            // shotgunTimer
            // 
            this.shotgunTimer.Interval = 2000;
            this.shotgunTimer.Tick += new System.EventHandler(this.shotgunTimer_Tick);
            // 
            // testLabel2
            // 
            this.testLabel2.AutoSize = true;
            this.testLabel2.Location = new System.Drawing.Point(166, 9);
            this.testLabel2.Name = "testLabel2";
            this.testLabel2.Size = new System.Drawing.Size(73, 16);
            this.testLabel2.TabIndex = 1;
            this.testLabel2.Text = "Version 1.0";
            // 
            // countingTimer
            // 
            this.countingTimer.Enabled = true;
            this.countingTimer.Interval = 1;
            this.countingTimer.Tick += new System.EventHandler(this.countingTimer_Tick);
            // 
            // testLabel3
            // 
            this.testLabel3.AutoSize = true;
            this.testLabel3.Location = new System.Drawing.Point(311, 9);
            this.testLabel3.Name = "testLabel3";
            this.testLabel3.Size = new System.Drawing.Size(73, 16);
            this.testLabel3.TabIndex = 2;
            this.testLabel3.Text = "Version 1.0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.testLabel3);
            this.Controls.Add(this.testLabel2);
            this.Controls.Add(this.testLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label testLabel;
        private System.Windows.Forms.Timer minigunTimer;
        private System.Windows.Forms.Timer shotgunTimer;
        private System.Windows.Forms.Label testLabel2;
        private System.Windows.Forms.Timer countingTimer;
        private System.Windows.Forms.Label testLabel3;
    }
}

