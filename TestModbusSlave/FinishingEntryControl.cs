#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

#endregion

using NLib;
using NLib.Xml;
using NLib.Devices.Modbus;
using LuckyTex.Models;
using LuckyTex.Services;

namespace TestModbusSlave
{
    public partial class FinishingEntryControl : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public FinishingEntryControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        public void Update(FinishingCounter inst)
        {
            if (null == inst)
            {
                txtAllBatchCounter.Text = string.Empty;
                txtAllTotalCounter.Text = string.Empty;

                txtBatchFlag.Text = string.Empty;
                txtTotalFlag.Text = string.Empty;

                txtBatchCount.Text = string.Empty;

                txtRealTimeBatchCounter.Text = string.Empty;
                txtRealTimeTotalCounter.Text = string.Empty;

                txtBatchCounter1.Text = string.Empty;
                txtBatchCounter2.Text = string.Empty;
                txtBatchCounter3.Text = string.Empty;
                txtBatchCounter4.Text = string.Empty;
                txtBatchCounter5.Text = string.Empty;
                txtBatchCounter6.Text = string.Empty;
                txtBatchCounter7.Text = string.Empty;

                txtTotalCounter1.Text = string.Empty;
                txtTotalCounter2.Text = string.Empty;
                txtTotalCounter3.Text = string.Empty;
                txtTotalCounter4.Text = string.Empty;
                txtTotalCounter5.Text = string.Empty;
                txtTotalCounter6.Text = string.Empty;
                txtTotalCounter7.Text = string.Empty;
            }
            else
            {
                txtAllBatchCounter.Text = inst.OverallBatchCounter.ToString();
                txtAllTotalCounter.Text = inst.OverallTotalCounter.ToString();

                txtBatchFlag.Text = inst.BatchFlag.ToString();
                txtTotalFlag.Text = inst.TotalFlag.ToString();

                txtBatchCount.Text = inst.OverallBatchCount.ToString();

                txtRealTimeBatchCounter.Text = inst.RealTimeBatchCounter.ToString();
                txtRealTimeTotalCounter.Text = inst.RealTimeTotalCounter.ToString();

                txtBatchCounter1.Text = inst.BatchCounter1.ToString();
                txtBatchCounter2.Text = inst.BatchCounter2.ToString();
                txtBatchCounter3.Text = inst.BatchCounter3.ToString();
                txtBatchCounter4.Text = inst.BatchCounter4.ToString();
                txtBatchCounter5.Text = inst.BatchCounter5.ToString();
                txtBatchCounter6.Text = inst.BatchCounter6.ToString();
                txtBatchCounter7.Text = inst.BatchCounter7.ToString();

                txtTotalCounter1.Text = inst.TotalCounter1.ToString();
                txtTotalCounter2.Text = inst.TotalCounter2.ToString();
                txtTotalCounter3.Text = inst.TotalCounter3.ToString();
                txtTotalCounter4.Text = inst.TotalCounter4.ToString();
                txtTotalCounter5.Text = inst.TotalCounter5.ToString();
                txtTotalCounter6.Text = inst.TotalCounter6.ToString();
                txtTotalCounter7.Text = inst.TotalCounter7.ToString();
            }
        }

        #endregion
    }
}
