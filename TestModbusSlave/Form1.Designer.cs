namespace TestModbusSlave
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtSlaveId = new System.Windows.Forms.TextBox();
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.Label();
            this.gvCurrHoldingRegisters = new System.Windows.Forms.DataGridView();
            this.cmdWrite = new System.Windows.Forms.Button();
            this.cmdRead = new System.Windows.Forms.Button();
            this.gvEditHoldingRegisters = new System.Windows.Forms.DataGridView();
            this.txtStartAddr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumInputs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReadName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReadAddr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbReadDataTypes = new System.Windows.Forms.ComboBox();
            this.cmdReadAddModbusValue = new System.Windows.Forms.Button();
            this.chkReadSwapFP = new System.Windows.Forms.CheckBox();
            this.cmdReadDeleteModbusValue = new System.Windows.Forms.Button();
            this.chkWriteSwapFP = new System.Windows.Forms.CheckBox();
            this.cbWriteDataTypes = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWriteAddr = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWriteName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmdWriteAddModbusValue = new System.Windows.Forms.Button();
            this.cmdReadClearModbusValue = new System.Windows.Forms.Button();
            this.txtWriteValue = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmdWriteDeleteModbusValue = new System.Windows.Forms.Button();
            this.cmdWriteClearModbusValue = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrHoldingRegisters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEditHoldingRegisters)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Slave Id";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(12, 29);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(135, 22);
            this.txtIP.TabIndex = 3;
            this.txtIP.Text = "127.0.0.1";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(153, 29);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(86, 22);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "502";
            // 
            // txtSlaveId
            // 
            this.txtSlaveId.Location = new System.Drawing.Point(245, 29);
            this.txtSlaveId.Name = "txtSlaveId";
            this.txtSlaveId.Size = new System.Drawing.Size(64, 22);
            this.txtSlaveId.TabIndex = 5;
            this.txtSlaveId.Text = "1";
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(315, 12);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(126, 40);
            this.cmdStart.TabIndex = 6;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Location = new System.Drawing.Point(447, 12);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(126, 40);
            this.cmdStop.TabIndex = 7;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.Location = new System.Drawing.Point(12, 63);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(13, 17);
            this.txtStatus.TabIndex = 8;
            this.txtStatus.Text = "-";
            // 
            // gvCurrHoldingRegisters
            // 
            this.gvCurrHoldingRegisters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvCurrHoldingRegisters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCurrHoldingRegisters.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvCurrHoldingRegisters.Location = new System.Drawing.Point(12, 213);
            this.gvCurrHoldingRegisters.MultiSelect = false;
            this.gvCurrHoldingRegisters.Name = "gvCurrHoldingRegisters";
            this.gvCurrHoldingRegisters.ReadOnly = true;
            this.gvCurrHoldingRegisters.RowTemplate.Height = 24;
            this.gvCurrHoldingRegisters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvCurrHoldingRegisters.Size = new System.Drawing.Size(571, 160);
            this.gvCurrHoldingRegisters.TabIndex = 9;
            // 
            // cmdWrite
            // 
            this.cmdWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdWrite.Location = new System.Drawing.Point(608, 600);
            this.cmdWrite.Name = "cmdWrite";
            this.cmdWrite.Size = new System.Drawing.Size(126, 51);
            this.cmdWrite.TabIndex = 11;
            this.cmdWrite.Text = "Write";
            this.cmdWrite.UseVisualStyleBackColor = true;
            this.cmdWrite.Click += new System.EventHandler(this.cmdWrite_Click);
            // 
            // cmdRead
            // 
            this.cmdRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRead.Location = new System.Drawing.Point(608, 324);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(126, 49);
            this.cmdRead.TabIndex = 12;
            this.cmdRead.Text = "Read\r\n";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // gvEditHoldingRegisters
            // 
            this.gvEditHoldingRegisters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvEditHoldingRegisters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEditHoldingRegisters.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvEditHoldingRegisters.Location = new System.Drawing.Point(12, 434);
            this.gvEditHoldingRegisters.MultiSelect = false;
            this.gvEditHoldingRegisters.Name = "gvEditHoldingRegisters";
            this.gvEditHoldingRegisters.ReadOnly = true;
            this.gvEditHoldingRegisters.RowTemplate.Height = 24;
            this.gvEditHoldingRegisters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvEditHoldingRegisters.Size = new System.Drawing.Size(571, 217);
            this.gvEditHoldingRegisters.TabIndex = 13;
            // 
            // txtStartAddr
            // 
            this.txtStartAddr.Location = new System.Drawing.Point(12, 113);
            this.txtStartAddr.Name = "txtStartAddr";
            this.txtStartAddr.Size = new System.Drawing.Size(135, 22);
            this.txtStartAddr.TabIndex = 15;
            this.txtStartAddr.Text = "569";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Start Address";
            // 
            // txtNumInputs
            // 
            this.txtNumInputs.Location = new System.Drawing.Point(153, 113);
            this.txtNumInputs.Name = "txtNumInputs";
            this.txtNumInputs.Size = new System.Drawing.Size(86, 22);
            this.txtNumInputs.TabIndex = 17;
            this.txtNumInputs.Text = "30";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(155, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "No of Inputs";
            // 
            // txtReadName
            // 
            this.txtReadName.Location = new System.Drawing.Point(11, 171);
            this.txtReadName.Name = "txtReadName";
            this.txtReadName.Size = new System.Drawing.Size(86, 22);
            this.txtReadName.TabIndex = 19;
            this.txtReadName.Text = "TEMP1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Name";
            // 
            // txtReadAddr
            // 
            this.txtReadAddr.Location = new System.Drawing.Point(103, 171);
            this.txtReadAddr.Name = "txtReadAddr";
            this.txtReadAddr.Size = new System.Drawing.Size(86, 22);
            this.txtReadAddr.TabIndex = 21;
            this.txtReadAddr.Text = "569";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(106, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "Address";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(192, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 17);
            this.label8.TabIndex = 22;
            this.label8.Text = "Data Type";
            // 
            // cbReadDataTypes
            // 
            this.cbReadDataTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReadDataTypes.FormattingEnabled = true;
            this.cbReadDataTypes.Items.AddRange(new object[] {
            "Int16",
            "UInt16",
            "Single"});
            this.cbReadDataTypes.Location = new System.Drawing.Point(195, 170);
            this.cbReadDataTypes.Name = "cbReadDataTypes";
            this.cbReadDataTypes.Size = new System.Drawing.Size(138, 24);
            this.cbReadDataTypes.TabIndex = 23;
            // 
            // cmdReadAddModbusValue
            // 
            this.cmdReadAddModbusValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReadAddModbusValue.Location = new System.Drawing.Point(608, 167);
            this.cmdReadAddModbusValue.Name = "cmdReadAddModbusValue";
            this.cmdReadAddModbusValue.Size = new System.Drawing.Size(126, 29);
            this.cmdReadAddModbusValue.TabIndex = 24;
            this.cmdReadAddModbusValue.Text = "Add";
            this.cmdReadAddModbusValue.UseVisualStyleBackColor = true;
            this.cmdReadAddModbusValue.Click += new System.EventHandler(this.cmdReadAddModbusValue_Click);
            // 
            // chkReadSwapFP
            // 
            this.chkReadSwapFP.AutoSize = true;
            this.chkReadSwapFP.Location = new System.Drawing.Point(339, 171);
            this.chkReadSwapFP.Name = "chkReadSwapFP";
            this.chkReadSwapFP.Size = new System.Drawing.Size(85, 21);
            this.chkReadSwapFP.TabIndex = 25;
            this.chkReadSwapFP.Text = "Swap FP";
            this.chkReadSwapFP.UseVisualStyleBackColor = true;
            // 
            // cmdReadDeleteModbusValue
            // 
            this.cmdReadDeleteModbusValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReadDeleteModbusValue.Location = new System.Drawing.Point(608, 213);
            this.cmdReadDeleteModbusValue.Name = "cmdReadDeleteModbusValue";
            this.cmdReadDeleteModbusValue.Size = new System.Drawing.Size(126, 29);
            this.cmdReadDeleteModbusValue.TabIndex = 26;
            this.cmdReadDeleteModbusValue.Text = "Delete";
            this.cmdReadDeleteModbusValue.UseVisualStyleBackColor = true;
            this.cmdReadDeleteModbusValue.Click += new System.EventHandler(this.cmdReadDeleteModbusValue_Click);
            // 
            // chkWriteSwapFP
            // 
            this.chkWriteSwapFP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkWriteSwapFP.AutoSize = true;
            this.chkWriteSwapFP.Location = new System.Drawing.Point(486, 402);
            this.chkWriteSwapFP.Name = "chkWriteSwapFP";
            this.chkWriteSwapFP.Size = new System.Drawing.Size(85, 21);
            this.chkWriteSwapFP.TabIndex = 33;
            this.chkWriteSwapFP.Text = "Swap FP";
            this.chkWriteSwapFP.UseVisualStyleBackColor = true;
            // 
            // cbWriteDataTypes
            // 
            this.cbWriteDataTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbWriteDataTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWriteDataTypes.FormattingEnabled = true;
            this.cbWriteDataTypes.Items.AddRange(new object[] {
            "Int16",
            "UInt16",
            "Single"});
            this.cbWriteDataTypes.Location = new System.Drawing.Point(195, 401);
            this.cbWriteDataTypes.Name = "cbWriteDataTypes";
            this.cbWriteDataTypes.Size = new System.Drawing.Size(138, 24);
            this.cbWriteDataTypes.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(192, 381);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "Data Type";
            // 
            // txtWriteAddr
            // 
            this.txtWriteAddr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWriteAddr.Location = new System.Drawing.Point(103, 402);
            this.txtWriteAddr.Name = "txtWriteAddr";
            this.txtWriteAddr.Size = new System.Drawing.Size(86, 22);
            this.txtWriteAddr.TabIndex = 30;
            this.txtWriteAddr.Text = "569";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(106, 381);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 17);
            this.label10.TabIndex = 29;
            this.label10.Text = "Address";
            // 
            // txtWriteName
            // 
            this.txtWriteName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWriteName.Location = new System.Drawing.Point(11, 402);
            this.txtWriteName.Name = "txtWriteName";
            this.txtWriteName.Size = new System.Drawing.Size(86, 22);
            this.txtWriteName.TabIndex = 28;
            this.txtWriteName.Text = "TEMP1";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 381);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 17);
            this.label11.TabIndex = 27;
            this.label11.Text = "Name";
            // 
            // cmdWriteAddModbusValue
            // 
            this.cmdWriteAddModbusValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdWriteAddModbusValue.Location = new System.Drawing.Point(608, 396);
            this.cmdWriteAddModbusValue.Name = "cmdWriteAddModbusValue";
            this.cmdWriteAddModbusValue.Size = new System.Drawing.Size(126, 29);
            this.cmdWriteAddModbusValue.TabIndex = 34;
            this.cmdWriteAddModbusValue.Text = "Add";
            this.cmdWriteAddModbusValue.UseVisualStyleBackColor = true;
            this.cmdWriteAddModbusValue.Click += new System.EventHandler(this.cmdWriteAddModbusValue_Click);
            // 
            // cmdReadClearModbusValue
            // 
            this.cmdReadClearModbusValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReadClearModbusValue.Location = new System.Drawing.Point(608, 261);
            this.cmdReadClearModbusValue.Name = "cmdReadClearModbusValue";
            this.cmdReadClearModbusValue.Size = new System.Drawing.Size(126, 29);
            this.cmdReadClearModbusValue.TabIndex = 35;
            this.cmdReadClearModbusValue.Text = "Clear";
            this.cmdReadClearModbusValue.UseVisualStyleBackColor = true;
            this.cmdReadClearModbusValue.Click += new System.EventHandler(this.cmdReadClearModbusValue_Click);
            // 
            // txtWriteValue
            // 
            this.txtWriteValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWriteValue.Location = new System.Drawing.Point(338, 403);
            this.txtWriteValue.Name = "txtWriteValue";
            this.txtWriteValue.Size = new System.Drawing.Size(137, 22);
            this.txtWriteValue.TabIndex = 37;
            this.txtWriteValue.Text = "30";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(340, 383);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 17);
            this.label12.TabIndex = 36;
            this.label12.Text = "Value";
            // 
            // cmdWriteDeleteModbusValue
            // 
            this.cmdWriteDeleteModbusValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdWriteDeleteModbusValue.Location = new System.Drawing.Point(608, 438);
            this.cmdWriteDeleteModbusValue.Name = "cmdWriteDeleteModbusValue";
            this.cmdWriteDeleteModbusValue.Size = new System.Drawing.Size(126, 29);
            this.cmdWriteDeleteModbusValue.TabIndex = 38;
            this.cmdWriteDeleteModbusValue.Text = "Delete";
            this.cmdWriteDeleteModbusValue.UseVisualStyleBackColor = true;
            this.cmdWriteDeleteModbusValue.Click += new System.EventHandler(this.cmdWriteDeleteModbusValue_Click);
            // 
            // cmdWriteClearModbusValue
            // 
            this.cmdWriteClearModbusValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdWriteClearModbusValue.Location = new System.Drawing.Point(608, 482);
            this.cmdWriteClearModbusValue.Name = "cmdWriteClearModbusValue";
            this.cmdWriteClearModbusValue.Size = new System.Drawing.Size(126, 29);
            this.cmdWriteClearModbusValue.TabIndex = 39;
            this.cmdWriteClearModbusValue.Text = "Clear";
            this.cmdWriteClearModbusValue.UseVisualStyleBackColor = true;
            this.cmdWriteClearModbusValue.Click += new System.EventHandler(this.cmdWriteClearModbusValue_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 670);
            this.Controls.Add(this.cmdWriteClearModbusValue);
            this.Controls.Add(this.cmdWriteDeleteModbusValue);
            this.Controls.Add(this.txtWriteValue);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmdReadClearModbusValue);
            this.Controls.Add(this.cmdWriteAddModbusValue);
            this.Controls.Add(this.chkWriteSwapFP);
            this.Controls.Add(this.cbWriteDataTypes);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtWriteAddr);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtWriteName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmdReadDeleteModbusValue);
            this.Controls.Add(this.chkReadSwapFP);
            this.Controls.Add(this.cmdReadAddModbusValue);
            this.Controls.Add(this.cbReadDataTypes);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtReadAddr);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtReadName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNumInputs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStartAddr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gvEditHoldingRegisters);
            this.Controls.Add(this.cmdRead);
            this.Controls.Add(this.cmdWrite);
            this.Controls.Add(this.gvCurrHoldingRegisters);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.txtSlaveId);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(774, 707);
            this.Name = "Form1";
            this.Text = "Modbus TCP/IP Slave";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrHoldingRegisters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEditHoldingRegisters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtSlaveId;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.DataGridView gvCurrHoldingRegisters;
        private System.Windows.Forms.Button cmdWrite;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.DataGridView gvEditHoldingRegisters;
        private System.Windows.Forms.TextBox txtStartAddr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumInputs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReadName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtReadAddr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbReadDataTypes;
        private System.Windows.Forms.Button cmdReadAddModbusValue;
        private System.Windows.Forms.CheckBox chkReadSwapFP;
        private System.Windows.Forms.Button cmdReadDeleteModbusValue;
        private System.Windows.Forms.CheckBox chkWriteSwapFP;
        private System.Windows.Forms.ComboBox cbWriteDataTypes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWriteAddr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWriteName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button cmdWriteAddModbusValue;
        private System.Windows.Forms.Button cmdReadClearModbusValue;
        private System.Windows.Forms.TextBox txtWriteValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button cmdWriteDeleteModbusValue;
        private System.Windows.Forms.Button cmdWriteClearModbusValue;
    }
}

