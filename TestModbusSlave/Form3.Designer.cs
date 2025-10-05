namespace TestModbusSlave
{
    partial class Form3
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage24 = new System.Windows.Forms.TabPage();
            this.gbAirLabMaster = new System.Windows.Forms.GroupBox();
            this.txtMAirLabSlaveId = new System.Windows.Forms.TextBox();
            this.label296 = new System.Windows.Forms.Label();
            this.txtMAirLabPortNo = new System.Windows.Forms.TextBox();
            this.label297 = new System.Windows.Forms.Label();
            this.txtMAirLabIP = new System.Windows.Forms.TextBox();
            this.label298 = new System.Windows.Forms.Label();
            this.grpAirLabRead = new System.Windows.Forms.GroupBox();
            this.lbScouringLabLastUpdate = new System.Windows.Forms.Label();
            this.txtAIRLab = new System.Windows.Forms.TextBox();
            this.label302 = new System.Windows.Forms.Label();
            this.grpAirLabWrite = new System.Windows.Forms.GroupBox();
            this.cmdAirLabWrite = new System.Windows.Forms.Button();
            this.pgAirLabWrite = new System.Windows.Forms.PropertyGrid();
            this.gbAirLabSave = new System.Windows.Forms.GroupBox();
            this.cmdAirLabStop = new System.Windows.Forms.Button();
            this.cmdAirLabStart = new System.Windows.Forms.Button();
            this.txtAirLabSlaveId = new System.Windows.Forms.TextBox();
            this.label303 = new System.Windows.Forms.Label();
            this.txtAirLabPortNo = new System.Windows.Forms.TextBox();
            this.label304 = new System.Windows.Forms.Label();
            this.txtAirLabIP = new System.Windows.Forms.TextBox();
            this.label305 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage24.SuspendLayout();
            this.gbAirLabMaster.SuspendLayout();
            this.grpAirLabRead.SuspendLayout();
            this.grpAirLabWrite.SuspendLayout();
            this.gbAirLabSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage24);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1098, 683);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage24
            // 
            this.tabPage24.Controls.Add(this.gbAirLabMaster);
            this.tabPage24.Controls.Add(this.grpAirLabRead);
            this.tabPage24.Controls.Add(this.grpAirLabWrite);
            this.tabPage24.Controls.Add(this.gbAirLabSave);
            this.tabPage24.Location = new System.Drawing.Point(4, 22);
            this.tabPage24.Name = "tabPage24";
            this.tabPage24.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage24.Size = new System.Drawing.Size(1090, 657);
            this.tabPage24.TabIndex = 0;
            this.tabPage24.Text = "Inspection Weight";
            this.tabPage24.UseVisualStyleBackColor = true;
            // 
            // gbAirLabMaster
            // 
            this.gbAirLabMaster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAirLabMaster.Controls.Add(this.txtMAirLabSlaveId);
            this.gbAirLabMaster.Controls.Add(this.label296);
            this.gbAirLabMaster.Controls.Add(this.txtMAirLabPortNo);
            this.gbAirLabMaster.Controls.Add(this.label297);
            this.gbAirLabMaster.Controls.Add(this.txtMAirLabIP);
            this.gbAirLabMaster.Controls.Add(this.label298);
            this.gbAirLabMaster.Location = new System.Drawing.Point(8, 90);
            this.gbAirLabMaster.Name = "gbAirLabMaster";
            this.gbAirLabMaster.Size = new System.Drawing.Size(1074, 77);
            this.gbAirLabMaster.TabIndex = 14;
            this.gbAirLabMaster.TabStop = false;
            this.gbAirLabMaster.Text = " Master ";
            // 
            // txtMAirLabSlaveId
            // 
            this.txtMAirLabSlaveId.Enabled = false;
            this.txtMAirLabSlaveId.Location = new System.Drawing.Point(261, 40);
            this.txtMAirLabSlaveId.Name = "txtMAirLabSlaveId";
            this.txtMAirLabSlaveId.Size = new System.Drawing.Size(74, 20);
            this.txtMAirLabSlaveId.TabIndex = 5;
            // 
            // label296
            // 
            this.label296.AutoSize = true;
            this.label296.Location = new System.Drawing.Point(260, 20);
            this.label296.Name = "label296";
            this.label296.Size = new System.Drawing.Size(46, 13);
            this.label296.TabIndex = 4;
            this.label296.Text = "Slave Id";
            // 
            // txtMAirLabPortNo
            // 
            this.txtMAirLabPortNo.Enabled = false;
            this.txtMAirLabPortNo.Location = new System.Drawing.Point(181, 40);
            this.txtMAirLabPortNo.Name = "txtMAirLabPortNo";
            this.txtMAirLabPortNo.Size = new System.Drawing.Size(74, 20);
            this.txtMAirLabPortNo.TabIndex = 3;
            // 
            // label297
            // 
            this.label297.AutoSize = true;
            this.label297.Location = new System.Drawing.Point(180, 20);
            this.label297.Name = "label297";
            this.label297.Size = new System.Drawing.Size(46, 13);
            this.label297.TabIndex = 2;
            this.label297.Text = "Port No.";
            // 
            // txtMAirLabIP
            // 
            this.txtMAirLabIP.Enabled = false;
            this.txtMAirLabIP.Location = new System.Drawing.Point(14, 40);
            this.txtMAirLabIP.Name = "txtMAirLabIP";
            this.txtMAirLabIP.Size = new System.Drawing.Size(161, 20);
            this.txtMAirLabIP.TabIndex = 1;
            // 
            // label298
            // 
            this.label298.AutoSize = true;
            this.label298.Location = new System.Drawing.Point(13, 20);
            this.label298.Name = "label298";
            this.label298.Size = new System.Drawing.Size(58, 13);
            this.label298.TabIndex = 0;
            this.label298.Text = "IP Address";
            // 
            // grpAirLabRead
            // 
            this.grpAirLabRead.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAirLabRead.Controls.Add(this.lbScouringLabLastUpdate);
            this.grpAirLabRead.Controls.Add(this.txtAIRLab);
            this.grpAirLabRead.Controls.Add(this.label302);
            this.grpAirLabRead.Location = new System.Drawing.Point(331, 173);
            this.grpAirLabRead.Name = "grpAirLabRead";
            this.grpAirLabRead.Size = new System.Drawing.Size(751, 477);
            this.grpAirLabRead.TabIndex = 13;
            this.grpAirLabRead.TabStop = false;
            this.grpAirLabRead.Text = " Read ";
            // 
            // lbScouringLabLastUpdate
            // 
            this.lbScouringLabLastUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbScouringLabLastUpdate.AutoSize = true;
            this.lbScouringLabLastUpdate.Location = new System.Drawing.Point(15, 446);
            this.lbScouringLabLastUpdate.Name = "lbScouringLabLastUpdate";
            this.lbScouringLabLastUpdate.Size = new System.Drawing.Size(80, 13);
            this.lbScouringLabLastUpdate.TabIndex = 63;
            this.lbScouringLabLastUpdate.Text = "Last Updated : ";
            // 
            // txtAIRLab
            // 
            this.txtAIRLab.Enabled = false;
            this.txtAIRLab.Location = new System.Drawing.Point(23, 48);
            this.txtAIRLab.Name = "txtAIRLab";
            this.txtAIRLab.Size = new System.Drawing.Size(161, 20);
            this.txtAIRLab.TabIndex = 11;
            // 
            // label302
            // 
            this.label302.AutoSize = true;
            this.label302.Location = new System.Drawing.Point(23, 28);
            this.label302.Name = "label302";
            this.label302.Size = new System.Drawing.Size(25, 13);
            this.label302.TabIndex = 10;
            this.label302.Text = "AIR";
            // 
            // grpAirLabWrite
            // 
            this.grpAirLabWrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpAirLabWrite.Controls.Add(this.cmdAirLabWrite);
            this.grpAirLabWrite.Controls.Add(this.pgAirLabWrite);
            this.grpAirLabWrite.Location = new System.Drawing.Point(8, 173);
            this.grpAirLabWrite.Name = "grpAirLabWrite";
            this.grpAirLabWrite.Size = new System.Drawing.Size(317, 477);
            this.grpAirLabWrite.TabIndex = 12;
            this.grpAirLabWrite.TabStop = false;
            this.grpAirLabWrite.Text = " Write ";
            // 
            // cmdAirLabWrite
            // 
            this.cmdAirLabWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAirLabWrite.Location = new System.Drawing.Point(8, 438);
            this.cmdAirLabWrite.Name = "cmdAirLabWrite";
            this.cmdAirLabWrite.Size = new System.Drawing.Size(75, 33);
            this.cmdAirLabWrite.TabIndex = 8;
            this.cmdAirLabWrite.Text = "Write";
            this.cmdAirLabWrite.UseVisualStyleBackColor = true;
            // 
            // pgAirLabWrite
            // 
            this.pgAirLabWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgAirLabWrite.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pgAirLabWrite.LineColor = System.Drawing.SystemColors.ControlDark;
            this.pgAirLabWrite.Location = new System.Drawing.Point(9, 23);
            this.pgAirLabWrite.Name = "pgAirLabWrite";
            this.pgAirLabWrite.Size = new System.Drawing.Size(299, 409);
            this.pgAirLabWrite.TabIndex = 1;
            // 
            // gbAirLabSave
            // 
            this.gbAirLabSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAirLabSave.Controls.Add(this.cmdAirLabStop);
            this.gbAirLabSave.Controls.Add(this.cmdAirLabStart);
            this.gbAirLabSave.Controls.Add(this.txtAirLabSlaveId);
            this.gbAirLabSave.Controls.Add(this.label303);
            this.gbAirLabSave.Controls.Add(this.txtAirLabPortNo);
            this.gbAirLabSave.Controls.Add(this.label304);
            this.gbAirLabSave.Controls.Add(this.txtAirLabIP);
            this.gbAirLabSave.Controls.Add(this.label305);
            this.gbAirLabSave.Location = new System.Drawing.Point(8, 7);
            this.gbAirLabSave.Name = "gbAirLabSave";
            this.gbAirLabSave.Size = new System.Drawing.Size(1074, 77);
            this.gbAirLabSave.TabIndex = 11;
            this.gbAirLabSave.TabStop = false;
            this.gbAirLabSave.Text = " Slave ";
            // 
            // cmdAirLabStop
            // 
            this.cmdAirLabStop.Location = new System.Drawing.Point(422, 30);
            this.cmdAirLabStop.Name = "cmdAirLabStop";
            this.cmdAirLabStop.Size = new System.Drawing.Size(75, 33);
            this.cmdAirLabStop.TabIndex = 7;
            this.cmdAirLabStop.Text = "Stop";
            this.cmdAirLabStop.UseVisualStyleBackColor = true;
            // 
            // cmdAirLabStart
            // 
            this.cmdAirLabStart.Location = new System.Drawing.Point(341, 30);
            this.cmdAirLabStart.Name = "cmdAirLabStart";
            this.cmdAirLabStart.Size = new System.Drawing.Size(75, 33);
            this.cmdAirLabStart.TabIndex = 6;
            this.cmdAirLabStart.Text = "Start";
            this.cmdAirLabStart.UseVisualStyleBackColor = true;
            // 
            // txtAirLabSlaveId
            // 
            this.txtAirLabSlaveId.Enabled = false;
            this.txtAirLabSlaveId.Location = new System.Drawing.Point(261, 40);
            this.txtAirLabSlaveId.Name = "txtAirLabSlaveId";
            this.txtAirLabSlaveId.Size = new System.Drawing.Size(74, 20);
            this.txtAirLabSlaveId.TabIndex = 5;
            // 
            // label303
            // 
            this.label303.AutoSize = true;
            this.label303.Location = new System.Drawing.Point(260, 20);
            this.label303.Name = "label303";
            this.label303.Size = new System.Drawing.Size(46, 13);
            this.label303.TabIndex = 4;
            this.label303.Text = "Slave Id";
            // 
            // txtAirLabPortNo
            // 
            this.txtAirLabPortNo.Enabled = false;
            this.txtAirLabPortNo.Location = new System.Drawing.Point(181, 40);
            this.txtAirLabPortNo.Name = "txtAirLabPortNo";
            this.txtAirLabPortNo.Size = new System.Drawing.Size(74, 20);
            this.txtAirLabPortNo.TabIndex = 3;
            // 
            // label304
            // 
            this.label304.AutoSize = true;
            this.label304.Location = new System.Drawing.Point(180, 20);
            this.label304.Name = "label304";
            this.label304.Size = new System.Drawing.Size(46, 13);
            this.label304.TabIndex = 2;
            this.label304.Text = "Port No.";
            // 
            // txtAirLabIP
            // 
            this.txtAirLabIP.Enabled = false;
            this.txtAirLabIP.Location = new System.Drawing.Point(14, 40);
            this.txtAirLabIP.Name = "txtAirLabIP";
            this.txtAirLabIP.Size = new System.Drawing.Size(161, 20);
            this.txtAirLabIP.TabIndex = 1;
            // 
            // label305
            // 
            this.label305.AutoSize = true;
            this.label305.Location = new System.Drawing.Point(13, 20);
            this.label305.Name = "label305";
            this.label305.Size = new System.Drawing.Size(58, 13);
            this.label305.TabIndex = 0;
            this.label305.Text = "IP Address";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 683);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.tabControl1.ResumeLayout(false);
            this.tabPage24.ResumeLayout(false);
            this.gbAirLabMaster.ResumeLayout(false);
            this.gbAirLabMaster.PerformLayout();
            this.grpAirLabRead.ResumeLayout(false);
            this.grpAirLabRead.PerformLayout();
            this.grpAirLabWrite.ResumeLayout(false);
            this.gbAirLabSave.ResumeLayout(false);
            this.gbAirLabSave.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage24;
        private System.Windows.Forms.GroupBox gbAirLabMaster;
        private System.Windows.Forms.TextBox txtMAirLabSlaveId;
        private System.Windows.Forms.Label label296;
        private System.Windows.Forms.TextBox txtMAirLabPortNo;
        private System.Windows.Forms.Label label297;
        private System.Windows.Forms.TextBox txtMAirLabIP;
        private System.Windows.Forms.Label label298;
        private System.Windows.Forms.GroupBox grpAirLabRead;
        private System.Windows.Forms.Label lbScouringLabLastUpdate;
        private System.Windows.Forms.TextBox txtAIRLab;
        private System.Windows.Forms.Label label302;
        private System.Windows.Forms.GroupBox grpAirLabWrite;
        private System.Windows.Forms.Button cmdAirLabWrite;
        private System.Windows.Forms.PropertyGrid pgAirLabWrite;
        private System.Windows.Forms.GroupBox gbAirLabSave;
        private System.Windows.Forms.Button cmdAirLabStop;
        private System.Windows.Forms.Button cmdAirLabStart;
        private System.Windows.Forms.TextBox txtAirLabSlaveId;
        private System.Windows.Forms.Label label303;
        private System.Windows.Forms.TextBox txtAirLabPortNo;
        private System.Windows.Forms.Label label304;
        private System.Windows.Forms.TextBox txtAirLabIP;
        private System.Windows.Forms.Label label305;
    }
}