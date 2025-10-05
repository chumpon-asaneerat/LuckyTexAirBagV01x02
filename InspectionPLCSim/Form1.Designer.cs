namespace InspectionPLCSim
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbActualLength = new System.Windows.Forms.Label();
            this.txtTotalLength = new System.Windows.Forms.TextBox();
            this.cmdSet = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.txtIncrement = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(43, 26);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(102, 34);
            this.cmdStart.TabIndex = 0;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Location = new System.Drawing.Point(178, 26);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(102, 34);
            this.cmdStop.TabIndex = 1;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Total Length";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(444, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Current";
            // 
            // lbActualLength
            // 
            this.lbActualLength.AutoSize = true;
            this.lbActualLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbActualLength.Location = new System.Drawing.Point(443, 113);
            this.lbActualLength.Name = "lbActualLength";
            this.lbActualLength.Size = new System.Drawing.Size(20, 24);
            this.lbActualLength.TabIndex = 5;
            this.lbActualLength.Text = "0";
            // 
            // txtTotalLength
            // 
            this.txtTotalLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.txtTotalLength.Location = new System.Drawing.Point(46, 112);
            this.txtTotalLength.Name = "txtTotalLength";
            this.txtTotalLength.Size = new System.Drawing.Size(100, 28);
            this.txtTotalLength.TabIndex = 6;
            this.txtTotalLength.Text = "2200";
            // 
            // cmdSet
            // 
            this.cmdSet.Location = new System.Drawing.Point(43, 166);
            this.cmdSet.Name = "cmdSet";
            this.cmdSet.Size = new System.Drawing.Size(102, 34);
            this.cmdSet.TabIndex = 7;
            this.cmdSet.Text = "Set";
            this.cmdSet.UseVisualStyleBackColor = true;
            this.cmdSet.Click += new System.EventHandler(this.cmdSet_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(178, 166);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(102, 34);
            this.cmdClear.TabIndex = 8;
            this.cmdClear.Text = "Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // txtIncrement
            // 
            this.txtIncrement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.txtIncrement.Location = new System.Drawing.Point(180, 112);
            this.txtIncrement.Name = "txtIncrement";
            this.txtIncrement.Size = new System.Drawing.Size(100, 28);
            this.txtIncrement.TabIndex = 10;
            this.txtIncrement.Text = "1.11";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(177, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Increment";
            // 
            // txtStartLength
            // 
            this.txtStartLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.txtStartLength.Location = new System.Drawing.Point(311, 112);
            this.txtStartLength.Name = "txtStartLength";
            this.txtStartLength.Size = new System.Drawing.Size(100, 28);
            this.txtStartLength.TabIndex = 12;
            this.txtStartLength.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(308, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Start Length";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 249);
            this.Controls.Add(this.txtStartLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtIncrement);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.cmdSet);
            this.Controls.Add(this.txtTotalLength);
            this.Controls.Add(this.lbActualLength);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Inspection PLC Simulator v1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbActualLength;
        private System.Windows.Forms.TextBox txtTotalLength;
        private System.Windows.Forms.Button cmdSet;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox txtIncrement;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStartLength;
        private System.Windows.Forms.Label label4;
    }
}

