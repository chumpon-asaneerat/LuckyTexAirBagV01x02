#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NLib;
using LuckyTex.Services;
using LuckyTex.Models;

using System.Globalization;
using System.Collections;

using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using DataControl.ClassData;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for BeamerMCEditWindow.xaml
    /// </summary>
    public partial class BeamerMCEditWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public BeamerMCEditWindow()
        {
            InitializeComponent();
            LoadBeamerMC();
        }

        #endregion

        #region Internal Variables

        BeamingSession _session = new BeamingSession();

        private string BEAMERNO = string.Empty;
        private string ITM_PREPARE = string.Empty;
        private string BeamerMC = string.Empty;
        private string OperatorText = string.Empty;

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (!string.IsNullOrEmpty(BEAMERNO))
                txtBEAMERNO.Text = BEAMERNO;

            if (!string.IsNullOrEmpty(ITM_PREPARE))
                txtITM_PREPARE.Text = ITM_PREPARE;

            if (!string.IsNullOrEmpty(BeamerMC))
                txtBEAMMC.Text = BeamerMC;

            if (!string.IsNullOrEmpty(OperatorText))
                txtOperator.Text = OperatorText;
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

            string mc = string.Empty;

            if (cbBeamerMC.SelectedValue != null)
            {
                mc = cbBeamerMC.SelectedValue.ToString();

                if (BeamerMC != mc)
                {
                    if (Save(mc) == true)
                        this.DialogResult = true;
                }
                else
                {
                    "Please Select New MC Before Save".ShowMessageBox();
                }
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region ClearControl

        private void ClearControl()
        {
            txtBEAMERNO.Text = string.Empty;
            txtITM_PREPARE.Text = string.Empty;
            txtBEAMMC.Text = string.Empty;
            txtOperator.Text = string.Empty;

            cbBeamerMC.SelectedValue = null;
            //cbBeamerMC.IsEnabled = false;
            cbBeamerMC.Focus();
        }

        #endregion

        #region Private Properties

        #region LoadBeamerMC

        private void LoadBeamerMC()
        {
            try
            {
                List<BeamingMCItem> items = _session.GetBeamingMCData();

                this.cbBeamerMC.ItemsSource = items;
                this.cbBeamerMC.DisplayMemberPath = "DisplayName";
                this.cbBeamerMC.SelectedValuePath = "MCId";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region Save

        private bool Save(string mc)
        {
            try
            {
                string P_BEAMERNO = string.Empty;
                string P_BEAMMC = string.Empty;

                string P_NEWBEAMMC = string.Empty;
                string P_OPERATOR = string.Empty;
                string RESULT = string.Empty;

                P_BEAMERNO = txtBEAMERNO.Text;
                P_BEAMMC = txtBEAMMC.Text;

                P_NEWBEAMMC = mc;
                P_OPERATOR = txtOperator.Text;

                RESULT = BeamingDataService.Instance.BEAM_EDITBEAMERMC(P_BEAMERNO, P_BEAMMC, P_NEWBEAMMC, P_OPERATOR);

                if (!string.IsNullOrEmpty(RESULT))
                {
                    RESULT.ShowMessageBox(true);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }

        #endregion

        #endregion

        #region Public Properties

        public void Setup(string P_BEAMERNO, string P_ITM_PREPARE, string P_BEAMMC, string P_OPERATORID)
        {
            BEAMERNO = P_BEAMERNO;
            ITM_PREPARE = P_ITM_PREPARE;
            BeamerMC = P_BEAMMC;
            OperatorText = P_OPERATORID;
        }

        #endregion

    }
}
