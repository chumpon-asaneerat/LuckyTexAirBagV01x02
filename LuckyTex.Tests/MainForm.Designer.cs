namespace LuckyTex
{
    partial class MainForm
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbTestConnResult = new System.Windows.Forms.Label();
            this.cmdTestConnect = new System.Windows.Forms.Button();
            this.txtConfig = new System.Windows.Forms.RichTextBox();
            this.cmdOpenConfigFolder = new System.Windows.Forms.Button();
            this.cmdSaveAirbagConfig = new System.Windows.Forms.Button();
            this.cmdLoadAirbagConfig = new System.Windows.Forms.Button();
            this.pgAirbagConfig = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmdINSTINSPECTIONLOTDEFECT = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cmdGarbage = new System.Windows.Forms.Button();
            this.cmdResetPort = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.lbPLCData = new System.Windows.Forms.Label();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdStart = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1054, 577);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbTestConnResult);
            this.tabPage1.Controls.Add(this.cmdTestConnect);
            this.tabPage1.Controls.Add(this.txtConfig);
            this.tabPage1.Controls.Add(this.cmdOpenConfigFolder);
            this.tabPage1.Controls.Add(this.cmdSaveAirbagConfig);
            this.tabPage1.Controls.Add(this.cmdLoadAirbagConfig);
            this.tabPage1.Controls.Add(this.pgAirbagConfig);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1046, 544);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Airbag Config";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbTestConnResult
            // 
            this.lbTestConnResult.AutoSize = true;
            this.lbTestConnResult.Location = new System.Drawing.Point(580, 25);
            this.lbTestConnResult.Name = "lbTestConnResult";
            this.lbTestConnResult.Size = new System.Drawing.Size(16, 20);
            this.lbTestConnResult.TabIndex = 6;
            this.lbTestConnResult.Text = "-";
            // 
            // cmdTestConnect
            // 
            this.cmdTestConnect.Location = new System.Drawing.Point(437, 13);
            this.cmdTestConnect.Name = "cmdTestConnect";
            this.cmdTestConnect.Size = new System.Drawing.Size(138, 40);
            this.cmdTestConnect.TabIndex = 5;
            this.cmdTestConnect.Text = "Test Connect";
            this.cmdTestConnect.UseVisualStyleBackColor = true;
            this.cmdTestConnect.Click += new System.EventHandler(this.cmdTestConnect_Click);
            // 
            // txtConfig
            // 
            this.txtConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfig.Location = new System.Drawing.Point(437, 59);
            this.txtConfig.Name = "txtConfig";
            this.txtConfig.ReadOnly = true;
            this.txtConfig.Size = new System.Drawing.Size(601, 453);
            this.txtConfig.TabIndex = 4;
            this.txtConfig.Text = "";
            this.txtConfig.WordWrap = false;
            // 
            // cmdOpenConfigFolder
            // 
            this.cmdOpenConfigFolder.Location = new System.Drawing.Point(294, 13);
            this.cmdOpenConfigFolder.Name = "cmdOpenConfigFolder";
            this.cmdOpenConfigFolder.Size = new System.Drawing.Size(138, 40);
            this.cmdOpenConfigFolder.TabIndex = 3;
            this.cmdOpenConfigFolder.Text = "Open Folder";
            this.cmdOpenConfigFolder.UseVisualStyleBackColor = true;
            this.cmdOpenConfigFolder.Click += new System.EventHandler(this.cmdOpenConfigFolder_Click);
            // 
            // cmdSaveAirbagConfig
            // 
            this.cmdSaveAirbagConfig.Location = new System.Drawing.Point(151, 13);
            this.cmdSaveAirbagConfig.Name = "cmdSaveAirbagConfig";
            this.cmdSaveAirbagConfig.Size = new System.Drawing.Size(138, 40);
            this.cmdSaveAirbagConfig.TabIndex = 2;
            this.cmdSaveAirbagConfig.Text = "Save";
            this.cmdSaveAirbagConfig.UseVisualStyleBackColor = true;
            this.cmdSaveAirbagConfig.Click += new System.EventHandler(this.cmdSaveAirbagConfig_Click);
            // 
            // cmdLoadAirbagConfig
            // 
            this.cmdLoadAirbagConfig.Location = new System.Drawing.Point(8, 13);
            this.cmdLoadAirbagConfig.Name = "cmdLoadAirbagConfig";
            this.cmdLoadAirbagConfig.Size = new System.Drawing.Size(138, 40);
            this.cmdLoadAirbagConfig.TabIndex = 1;
            this.cmdLoadAirbagConfig.Text = "Load";
            this.cmdLoadAirbagConfig.UseVisualStyleBackColor = true;
            this.cmdLoadAirbagConfig.Click += new System.EventHandler(this.cmdLoadAirbagConfig_Click);
            // 
            // pgAirbagConfig
            // 
            this.pgAirbagConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pgAirbagConfig.Location = new System.Drawing.Point(8, 59);
            this.pgAirbagConfig.Name = "pgAirbagConfig";
            this.pgAirbagConfig.Size = new System.Drawing.Size(424, 453);
            this.pgAirbagConfig.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.cmdINSTINSPECTIONLOTDEFECT);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1046, 506);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Test SP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(363, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(675, 464);
            this.dataGridView1.TabIndex = 5;
            // 
            // cmdINSTINSPECTIONLOTDEFECT
            // 
            this.cmdINSTINSPECTIONLOTDEFECT.Location = new System.Drawing.Point(8, 20);
            this.cmdINSTINSPECTIONLOTDEFECT.Name = "cmdINSTINSPECTIONLOTDEFECT";
            this.cmdINSTINSPECTIONLOTDEFECT.Size = new System.Drawing.Size(336, 41);
            this.cmdINSTINSPECTIONLOTDEFECT.TabIndex = 3;
            this.cmdINSTINSPECTIONLOTDEFECT.Text = "Get Defect code";
            this.cmdINSTINSPECTIONLOTDEFECT.UseVisualStyleBackColor = true;
            this.cmdINSTINSPECTIONLOTDEFECT.Click += new System.EventHandler(this.cmdINSTINSPECTIONLOTDEFECT_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cmdGarbage);
            this.tabPage3.Controls.Add(this.cmdResetPort);
            this.tabPage3.Controls.Add(this.cmdClear);
            this.tabPage3.Controls.Add(this.lbPLCData);
            this.tabPage3.Controls.Add(this.cmdStop);
            this.tabPage3.Controls.Add(this.cmdStart);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1046, 506);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "PLC Service";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cmdGarbage
            // 
            this.cmdGarbage.Location = new System.Drawing.Point(410, 14);
            this.cmdGarbage.Name = "cmdGarbage";
            this.cmdGarbage.Size = new System.Drawing.Size(92, 35);
            this.cmdGarbage.TabIndex = 5;
            this.cmdGarbage.Text = "Garbage";
            this.cmdGarbage.UseVisualStyleBackColor = true;
            this.cmdGarbage.Click += new System.EventHandler(this.cmdGarbage_Click);
            // 
            // cmdResetPort
            // 
            this.cmdResetPort.Location = new System.Drawing.Point(312, 14);
            this.cmdResetPort.Name = "cmdResetPort";
            this.cmdResetPort.Size = new System.Drawing.Size(92, 35);
            this.cmdResetPort.TabIndex = 4;
            this.cmdResetPort.Text = "Reset";
            this.cmdResetPort.UseVisualStyleBackColor = true;
            this.cmdResetPort.Click += new System.EventHandler(this.cmdResetPort_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(215, 14);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(92, 35);
            this.cmdClear.TabIndex = 3;
            this.cmdClear.Text = "Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // lbPLCData
            // 
            this.lbPLCData.AutoSize = true;
            this.lbPLCData.Location = new System.Drawing.Point(14, 68);
            this.lbPLCData.Name = "lbPLCData";
            this.lbPLCData.Size = new System.Drawing.Size(16, 20);
            this.lbPLCData.TabIndex = 2;
            this.lbPLCData.Text = "-";
            // 
            // cmdStop
            // 
            this.cmdStop.Location = new System.Drawing.Point(116, 14);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(92, 35);
            this.cmdStop.TabIndex = 1;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(18, 14);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(92, 35);
            this.cmdStart.TabIndex = 0;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 577);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "AirBag Function tests";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdSaveAirbagConfig;
        private System.Windows.Forms.Button cmdLoadAirbagConfig;
        private System.Windows.Forms.PropertyGrid pgAirbagConfig;
        private System.Windows.Forms.Button cmdOpenConfigFolder;
        private System.Windows.Forms.RichTextBox txtConfig;
        private System.Windows.Forms.Button cmdTestConnect;
        private System.Windows.Forms.Label lbTestConnResult;
        private System.Windows.Forms.Button cmdINSTINSPECTIONLOTDEFECT;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Label lbPLCData;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdResetPort;
        private System.Windows.Forms.Button cmdGarbage;
    }
}

