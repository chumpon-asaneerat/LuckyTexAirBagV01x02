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
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;
using System.Threading;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for WarperMCStatusPage.xaml
    /// </summary>
    public partial class WarperMCStatusPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WarperMCStatusPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;
        string MCName = string.Empty;
        string MCno = string.Empty;
        string StatusA = string.Empty;
        string StatusB = string.Empty;

        string WARPHEADNOSideA = string.Empty;
        string WARPHEADNOSideB = string.Empty;
        string ITM_PREPARESideA = string.Empty;
        string ITM_PREPARESideB = string.Empty;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (MCName != "")
                txtMCName.Text = MCName;

            Warp_GetWarperMCStatusSideA(MCno);

            Warp_GetWarperMCStatusSideB(MCno);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region cmdStartSideA_Click

        private void cmdStartSideA_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(StatusB))
            {
                Start(WARPHEADNOSideA,ITM_PREPARESideA,"A");
            }
            else
            {
                if (StatusB == "C")
                {
                    "Start Warrping Process On Side A".ShowMessageBox(false);
                    Start(WARPHEADNOSideA, ITM_PREPARESideA, "A");
                }
                else if (StatusB == "S")
                {
                    "Can not Start Warping on Side A , Finish Side B first.".ShowMessageBox(false);
                }
            }
        }

        #endregion

        #region cmdWarperRollA_Click

        private void cmdWarperRollA_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(StatusB))
            {
                WarperRoll(WARPHEADNOSideA, ITM_PREPARESideA, "A");
            }
            else
            {
                if (StatusB == "C")
                {
                    "Start Warrping Process On Side A".ShowMessageBox(false);
                    WarperRoll(WARPHEADNOSideA, ITM_PREPARESideA, "A");
                }
                else if (StatusB == "S")
                {
                    "Can not Start Warping on Side A , Finish Side B first.".ShowMessageBox(false);
                }
            }
        }

        #endregion

        #region cmdStartSideB_Click

        private void cmdStartSideB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(StatusA))
            {
                Start(WARPHEADNOSideB, ITM_PREPARESideB, "B");
            }
            else
            {
                if (StatusA == "C")
                {
                    "Start Warrping Process On Side B".ShowMessageBox(false);
                    Start(WARPHEADNOSideB, ITM_PREPARESideB, "B");
                }
                else if (StatusA == "S")
                {
                    "Can not Start Warping on Side A , Finish Side B first.".ShowMessageBox(false);
                }
            }
        }

        #endregion

        #region cmdWarperRollB_Click

        private void cmdWarperRollB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(StatusA))
            {
                WarperRoll(WARPHEADNOSideB, ITM_PREPARESideB, "B");
            }
            else
            {
                if (StatusA == "C")
                {
                    "Start Warrping Process On Side B".ShowMessageBox(false);
                    WarperRoll(WARPHEADNOSideB, ITM_PREPARESideB, "B");
                }
                else if (StatusA == "S")
                {
                    "Can not Start Warping on Side A , Finish Side B first.".ShowMessageBox(false);
                }
            }
        }

        #endregion

        #endregion

        #region private Methods

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            txtWARPHEADNOSideA.Text = string.Empty;
            txtITM_PREPARESideA.Text = string.Empty;
            txtACTUALCHSideA.Text = string.Empty;
            txtCREATEBYSideA.Text = string.Empty;
            txtCONDITIONSTARTSideA.Text = string.Empty;
            txtCONDITIONINGSideA.Text = string.Empty;
            txtStatusSideA.Text = string.Empty;

            cmdStartSideA.IsEnabled = false;
            cmdWarperRollA.IsEnabled = false;

            StatusA = "";
            WARPHEADNOSideA = "";
            ITM_PREPARESideA = "";

            txtWARPHEADNOSideB.Text = string.Empty;
            txtITM_PREPARESideB.Text = string.Empty;
            txtACTUALCHSideB.Text = string.Empty;
            txtCREATEBYSideB.Text = string.Empty;
            txtCONDITIONSTARTSideB.Text = string.Empty;
            txtCONDITIONINGSideB.Text = string.Empty;
            txtStatusSideB.Text = string.Empty;

            cmdStartSideB.IsEnabled = false;
            cmdWarperRollB.IsEnabled = false;

            StatusB = "";
            WARPHEADNOSideB = "";
            ITM_PREPARESideB = "";

        }

        #endregion

        #region Warp_GetWarperMCStatusSideA

        private void Warp_GetWarperMCStatusSideA(string P_MCNO)
        {
            List<WARP_GETWARPERMCSTATUS> result = new List<Models.WARP_GETWARPERMCSTATUS>();
            result = WarpingDataService.Instance.Warp_GetWarperMCStatusSideA(P_MCNO);

            if (result.Count > 0)
            {
                WARPHEADNOSideA = result[0].WARPHEADNO;
                ITM_PREPARESideA = result[0].ITM_PREPARE;

                txtWARPHEADNOSideA.Text = WARPHEADNOSideA;
                txtITM_PREPARESideA.Text = ITM_PREPARESideA;
                txtACTUALCHSideA.Text = result[0].ACTUALCH.Value.ToString("#,##0.##");
                txtCREATEBYSideA.Text = result[0].CREATEBY;
                txtCONDITIONSTARTSideA.Text = result[0].CONDITIONSTART.Value.ToString("dd/MM/yy HH:mm");
                txtCONDITIONINGSideA.Text = result[0].CONDITIONING;

                string Status = result[0].STATUS;
                if (Status == "C")
                {
                    StatusA = "C";
                    txtStatusSideA.Text = "Conditioning";
                    cmdStartSideA.IsEnabled = true;

                    cmdWarperRollA.IsEnabled = false;
                }
                else if (Status == "S")
                {
                    StatusA = "S";
                    txtStatusSideA.Text = "Processing";
                    cmdStartSideA.IsEnabled = false;

                    cmdWarperRollA.IsEnabled = true;
                }
            }
            else
            {
                txtWARPHEADNOSideA.Text = string.Empty;
                txtITM_PREPARESideA.Text = string.Empty;
                txtACTUALCHSideA.Text = string.Empty;
                txtCREATEBYSideA.Text = string.Empty;
                txtCONDITIONSTARTSideA.Text = string.Empty;
                txtCONDITIONINGSideA.Text = string.Empty;
                txtStatusSideA.Text = string.Empty;

                cmdStartSideA.IsEnabled = false;

                StatusA = "";
                WARPHEADNOSideA = "";
                ITM_PREPARESideA = "";
            }
        }

        #endregion

        #region Warp_GetWarperMCStatusSideB

        private void Warp_GetWarperMCStatusSideB(string P_MCNO)
        {
            List<WARP_GETWARPERMCSTATUS> result = new List<Models.WARP_GETWARPERMCSTATUS>();
            result = WarpingDataService.Instance.Warp_GetWarperMCStatusSideB(P_MCNO);

            if (result.Count > 0)
            {
                WARPHEADNOSideB = result[0].WARPHEADNO;
                ITM_PREPARESideB = result[0].ITM_PREPARE;

                txtWARPHEADNOSideB.Text = WARPHEADNOSideB;
                txtITM_PREPARESideB.Text = ITM_PREPARESideB;
                txtACTUALCHSideB.Text = result[0].ACTUALCH.Value.ToString("#,##0.##");
                txtCREATEBYSideB.Text = result[0].CREATEBY;
                txtCONDITIONSTARTSideB.Text = result[0].CONDITIONSTART.Value.ToString("dd/MM/yy HH:mm");
                txtCONDITIONINGSideB.Text = result[0].CONDITIONING;

                string Status = result[0].STATUS;
                if (Status == "C")
                {
                    StatusB = "C";
                    txtStatusSideB.Text = "Conditioning";
                    cmdStartSideB.IsEnabled = true;

                    cmdWarperRollB.IsEnabled = false;

                }
                else if (Status == "S")
                {
                    StatusB = "S";
                    txtStatusSideB.Text = "Processing";
                    cmdStartSideB.IsEnabled = false;

                    cmdWarperRollB.IsEnabled = true;
                }
            }
            else
            {
                txtWARPHEADNOSideB.Text = string.Empty;
                txtITM_PREPARESideB.Text = string.Empty;
                txtACTUALCHSideB.Text = string.Empty;
                txtCREATEBYSideB.Text = string.Empty;
                txtCONDITIONSTARTSideB.Text = string.Empty;
                txtCONDITIONINGSideB.Text = string.Empty;
                txtStatusSideB.Text = string.Empty;

                cmdStartSideB.IsEnabled = false;

                StatusB = "";
                WARPHEADNOSideB = "";
                ITM_PREPARESideB = "";
            }
        }

        #endregion

        #region Start

        private void Start(string P_WARPHEADNO ,string ITM_PREPARE,string Side)
        {
            string P_CONDITONBY = opera;
            string P_STATUS = "S";

            if (WarpingDataService.Instance.WARP_UPDATESETTINGHEAD_MCStatus(P_WARPHEADNO, DateTime.Now, P_CONDITONBY, P_STATUS) == true)
            {
                WarpingProcessPage page = new WarpingProcessPage();
                page.Setup(opera, MCName, MCno, P_WARPHEADNO, ITM_PREPARE, Side,0);
                PageManager.Instance.Current = page;
            }
            else
            {
                "Can not Start Warping".ShowMessageBox(false);
            }
        }

        #endregion

        #region WarperRoll

        private void WarperRoll(string P_WARPHEADNO, string ITM_PREPARE, string Side)
        {
            WarpingProcessPage page = new WarpingProcessPage();
            page.Setup(opera, MCName, MCno, P_WARPHEADNO, ITM_PREPARE, Side,1);
            PageManager.Instance.Current = page;
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user,string mcName,string mcNo)
        {
            if (opera != null)
            {
                opera = user;
            }

            if (MCName != null)
            {
                MCName = mcName;
            }

            if(MCno != null)
            {
                MCno = mcNo;
            }
        }

        #endregion

    }
}
