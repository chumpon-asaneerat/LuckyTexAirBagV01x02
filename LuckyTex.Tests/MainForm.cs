#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#endregion

using System.IO;

using NLib;
using LuckyTex.Configs;
using LuckyTex.Domains;
using LuckyTex.Services;

namespace LuckyTex
{
    public partial class MainForm : Form
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        #region ShowFileContent
        
        private void ShowFileContent(string fileName)
        {
            string content = string.Empty;
            try
            {
                content = File.ReadAllText(fileName);
            }
            catch (Exception)
            {
                content = string.Empty;
            }
            finally
            {
                txtConfig.Text = content;
            }
        }

        #endregion

        #endregion

        #region Form Load/Closing

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DatabaseManager.Instance.Shutdown();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Size Changed.");
        }

        #endregion

        #region Control Handlers for Tab - 1
        
        private void cmdLoadAirbagConfig_Click(object sender, EventArgs e)
        {
            // Load config
            ConfigManager.Instance.LoadConfigs();

            ConfigManager.Instance.LoadConfigs_D365();

            // Bind to property grid.
            pgAirbagConfig.SelectedObject = ConfigManager.Instance.DatabaseConfig;
            // Show content
            ShowFileContent(ConfigManager.Instance.ConfigurationFileName);
        }

        private void cmdSaveAirbagConfig_Click(object sender, EventArgs e)
        {
            // Save config
            ConfigManager.Instance.UpdateConfigs();

            ConfigManager.Instance.UpdateConfigs_D365();
            // Show content
            ShowFileContent(ConfigManager.Instance.ConfigurationFileName);
        }

        private void cmdOpenConfigFolder_Click(object sender, EventArgs e)
        {
            AirBagConsts.ShareConfigFolder.OpenFolder();
        }

        private void cmdTestConnect_Click(object sender, EventArgs e)
        {
            lbTestConnResult.Text = string.Empty;

            DatabaseManager.Instance.Start();
            if (DatabaseManager.Instance.IsConnected)
            {
                lbTestConnResult.Text = "Connect success.";
            }
            else
            {
                lbTestConnResult.Text = "Connect failed.";
            }
            DatabaseManager.Instance.Shutdown();
        }

        #endregion

        #region Control Handlers for Tab - 2
        
        public class SPTest
        {
            public object Paramter { get; set; }
            public Func<object, object> Process { get; set; }
            public object Result { get; set; }
        }

        //private SPTest test = null;

        private void cmdINSTINSPECTIONLOTDEFECT_Click(object sender, EventArgs e)
        {
            GETDEFECTCODEDETAILParameter paraInst = new GETDEFECTCODEDETAILParameter();
            paraInst.P_DEFECTID = "1";
            List<GETDEFECTCODEDETAILResult> result = null;
            //DataTable result = null;

            bool autoShutdown = false;
            if (!DatabaseManager.Instance.IsConnected)
            {
                autoShutdown = true;
                DatabaseManager.Instance.Start();
            }

            //this.SuspendLayout();
            if (DatabaseManager.Instance.IsConnected)
            {
                try
                {
                    //bool _completed = false;
                    result = DatabaseManager.Instance.GETDEFECTCODEDETAIL(paraInst);
                    /*
                    Action action = new Action(() => 
                    {
                        result = DatabaseManager.Instance.Query("SELECT 1 AS Const FROM DUAL");
                        _completed = true;
                    });
                    this.Invoke(action);
                    while (!_completed)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(20);
                    }
                    */
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            //this.ResumeLayout(false);

            if (autoShutdown)
            {
                DatabaseManager.Instance.Shutdown();
            }

            if (null != result)
            {
                if (result is System.Collections.IList /*|| result is DataTable */)
                    dataGridView1.DataSource = result;
                else dataGridView1.DataSource = null;
            }

            /*
            test = new SPTest();
            GETDEFECTCODEDETAILParameter paraInst =
                new GETDEFECTCODEDETAILParameter();
            test.Paramter = paraInst;
            test.Process = (object spPara) =>
            {
                GETDEFECTCODEDETAILParameter para =
                    (spPara as GETDEFECTCODEDETAILParameter);
                List<GETDEFECTCODEDETAILResult> result;
                result = DatabaseManager.Instance.GETDEFECTCODEDETAIL(para);

                return result;
            };

            pgSPPara.SelectedObject =  test.Paramter;
            */
        }

        private void cmdExecuteSP_Click(object sender, EventArgs e)
        {
            /*
            if (null == test)
                return;
            try
            {
                if (!DatabaseManager.Instance.IsConnected)
                    DatabaseManager.Instance.Start();

                test.Result = test.Process(test.Paramter);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            */

            /*
            if (test.Result is System.Collections.IList)
                dataGridView1.DataSource = test.Result;
            else dataGridView1.DataSource = null;

            pgSPResult.SelectedObject = test.Result;
            */
        }

        #endregion

        #region Control Handlers for Tab - 3
        
        private void cmdStart_Click(object sender, EventArgs e)
        {
            InspectionPLCService.Instance.OnDataArrived += new PLCDataArrivedEventHandler(Instance_OnDataArrived);
            InspectionPLCService.Instance.Start();
        }

        void Instance_OnDataArrived(object sender, PLCDataArrivedEventArgs e)
        {
            lbPLCData.Text = e.Value.ToString("n2");
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            InspectionPLCService.Instance.OnDataArrived -= new PLCDataArrivedEventHandler(Instance_OnDataArrived);
            InspectionPLCService.Instance.Stop();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            lbPLCData.Text = "-";
        }

        private void cmdResetPort_Click(object sender, EventArgs e)
        {
            InspectionPLCService.Instance.ResetPort();
        }

        private void cmdGarbage_Click(object sender, EventArgs e)
        {
            InspectionPLCService.Instance.SendGarbage();
        }

        #endregion
    }
}
