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
    /// Interaction logic for DrawingStartPage.xaml
    /// </summary>
    public partial class DrawingStartPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DrawingStartPage()
        {
            InitializeComponent();

            LoadGroup();
        }

        #endregion

        #region Internal Variables

        private DrawingSession _session = new DrawingSession();

        string opera = string.Empty;
        string mcNo = string.Empty;
        string mcName = string.Empty;
        string PRODUCTTYPEID = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadItemGood();

            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;
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

        private void cmdConfirm_Click(object sender, RoutedEventArgs e)
        {
            Confirm();
        }

        #endregion

        #region cbItemCode_LostFocus

        private void cbItemCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbItemCode.SelectedValue != null)
            {
                List<DRAW_GETSPECBYCHOPNO> results = DrawingDataService.Instance.ITM_GETITEMPREPARELIST(cbItemCode.SelectedValue.ToString());

                if (results.Count > 0)
                {
                    txtREEDTYPE.Text = results[0].REEDTYPE.Value.ToString("#,##0.##");
                    txtNOYARN.Text = results[0].NOYARN.Value.ToString("#,##0.##");
                }
                else
                {
                    txtREEDTYPE.Text = "0";
                    txtNOYARN.Text = "0";
                }
            } 
        }

        #endregion

        #region cmbColors_LostFocus
        private void cmbColors_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbColors.Text == "Orange")
            {
                cmbColors.Background = Brushes.Orange;
                cmbColors.Foreground = Brushes.Orange;
            }
            else if (cmbColors.Text == "Green")
            {
                cmbColors.Background = Brushes.Green;
                cmbColors.Foreground = Brushes.Green;
            }
            else if (cmbColors.Text == "Blue")
            {
                cmbColors.Background = Brushes.Blue;
                cmbColors.Foreground = Brushes.Blue;
            }
            else if (cmbColors.Text == "Gray")
            {
                cmbColors.Background = Brushes.Gray;
                cmbColors.Foreground = Brushes.Gray;
            }
        }
        #endregion

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region txtReedNo_KeyDown

        private void txtReedNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHealdNo.Focus();
                txtHealdNo.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtHealdNo_KeyDown

        private void txtHealdNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBeamLot.Focus();
                txtBeamLot.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtBeamLot_KeyDown

        private void txtBeamLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdConfirm.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region txtBeamLot_LostFocus

        private void txtBeamLot_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBeamLot.Text))
            {
                string BEAMLOT = string.Empty;
                string ITM_PREPARE = string.Empty;

                BEAMLOT = txtBeamLot.Text;

                if (cbItemCode.SelectedValue != null)
                    ITM_PREPARE = cbItemCode.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(BEAMLOT) && !string.IsNullOrEmpty(ITM_PREPARE))
                {
                    CheckBeamLot_ITM_Prepare(BEAMLOT, ITM_PREPARE);
                }
                else
                {
                    if (string.IsNullOrEmpty(BEAMLOT))
                    {
                        "Beam Lot isn't null".ShowMessageBox(true);
                    }
                    else if (string.IsNullOrEmpty(ITM_PREPARE))
                    {
                        "Item Prepare isn't null".ShowMessageBox(true);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region private Methods

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<ITM_GETITEMPREPARELIST> items = _session.GetItemCodeData();

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_PREPARE";
                this.cbItemCode.SelectedValuePath = "ITM_PREPARE";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadGroup

        private void LoadGroup()
        {
            if (cbGroup.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C","D" };

                cbGroup.ItemsSource = str;
                cbGroup.SelectedIndex = 0;
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            cbItemCode.SelectedValue = null;

            cmbColors.SelectedValue = null;
            cmbColors.Text = "";
            cmbColors.Background = null;
            cmbColors.Foreground = null;
            txtReedNo.Text = "";

            txtHealdNo.Text = "";
            txtNOYARN.Text = "";
            txtREEDTYPE.Text = "";
            txtBeamLot.Text = "";
            txtTotalBeam.Text = "";

            rbTying.IsChecked = true;
            rbDrawing.IsChecked = false;

            cbGroup.SelectedIndex = 0;

            PRODUCTTYPEID = "";

            txtReedNo.SelectAll();
            txtReedNo.Focus();
        }

        #endregion

        private void CheckBeamLot_ITM_Prepare(string BEAMLOT, string ITM_PREPARE)
        {
            if (!string.IsNullOrEmpty(BEAMLOT) && !string.IsNullOrEmpty(ITM_PREPARE))
            {
                DRAW_GETBEAMLOTDETAIL results = new DRAW_GETBEAMLOTDETAIL();

                results = DrawingDataService.Instance.CheckBeamLot_ITM_Prepare(BEAMLOT, ITM_PREPARE);

                if (results != null)
                {
                    if (!string.IsNullOrEmpty(results.StrMsg))
                    {
                        results.StrMsg.ShowMessageBox(true);

                        txtBeamLot.Text = "";
                        txtBeamLot.Focus();
                        txtBeamLot.SelectAll();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(results.BEAMNO))
                        {
                            txtBEAMNO.Text = results.BEAMNO;
                        }
                        else
                        {
                            txtBEAMNO.Text = "";
                        }

                        if (!string.IsNullOrEmpty(results.PRODUCTTYPEID))
                        {
                            PRODUCTTYPEID = results.PRODUCTTYPEID;
                        }
                        else
                        {
                            PRODUCTTYPEID = "1";
                        }

                        if (results.TOTALYARN != null)
                        {
                            txtTotalBeam.Text = results.TOTALYARN.Value.ToString("#,##0.##");
                        }
                        else
                        {
                            txtTotalBeam.Text = "0";
                        }
                    }
                }
                else
                {
                    "Beam Lot is invalid".ShowMessageBox(true);

                    txtBeamLot.Text = "";
                    txtBeamLot.Focus();
                    txtBeamLot.SelectAll();
                }
            }
        }

        private void Confirm()
        {
            string result = string.Empty;

            string P_BEAMLOT = string.Empty;
            string P_ITMPREPARE = string.Empty;
            string P_PRODUCTID = string.Empty;
            string P_DRAWINGTYPE = string.Empty;
            string P_REEDNO = string.Empty;
            string P_HEALDCOLOR = string.Empty;
            decimal? P_HEALDNO = null;
            string P_OPERATOR = string.Empty;
            string P_GROUP = string.Empty;


            if (!string.IsNullOrEmpty(txtBeamLot.Text))
                P_BEAMLOT = txtBeamLot.Text;

            if (cbItemCode.SelectedValue != null)
                P_ITMPREPARE = cbItemCode.SelectedValue.ToString();

            P_PRODUCTID = PRODUCTTYPEID;

            #region P_DRAWINGTYPE

            if (rbTying.IsChecked == true && rbDrawing.IsChecked == false)
            {
                P_DRAWINGTYPE = "Tying";
            }
            else if (rbTying.IsChecked == false && rbDrawing.IsChecked == true)
            {
                P_DRAWINGTYPE = "Drawing";
            }

            #endregion

            if (!string.IsNullOrEmpty(txtReedNo.Text))
                P_REEDNO = txtReedNo.Text;

            if (!string.IsNullOrEmpty(cmbColors.Text))
                P_HEALDCOLOR = cmbColors.Text;

            if (!string.IsNullOrEmpty(txtHealdNo.Text))
            {
                try
                {
                    P_HEALDNO = decimal.Parse(txtHealdNo.Text);
                }
                catch
                {
                    P_HEALDNO = 0;
                }
            }

            if (!string.IsNullOrEmpty(txtOperator.Text))
                P_OPERATOR = txtOperator.Text;

            if (cbGroup.SelectedValue != null)
                P_GROUP = cbGroup.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(P_BEAMLOT) && !string.IsNullOrEmpty(P_ITMPREPARE) && !string.IsNullOrEmpty(P_PRODUCTID)
                && !string.IsNullOrEmpty(P_DRAWINGTYPE) && !string.IsNullOrEmpty(P_REEDNO) && !string.IsNullOrEmpty(P_HEALDCOLOR)
                && !string.IsNullOrEmpty(P_OPERATOR))
            {
                result = DrawingDataService.Instance.DRAW_INSERTDRAWING(P_BEAMLOT, P_ITMPREPARE, P_PRODUCTID, P_DRAWINGTYPE
                    , P_REEDNO, P_HEALDCOLOR, P_HEALDNO, P_OPERATOR, P_GROUP);

                string msg = "Beam Lot " + P_BEAMLOT + " start drawing process";


                if (string.IsNullOrEmpty(result))
                {
                    msg.ShowMessageBox();

                    PageManager.Instance.Back();
                }
                else
                    result.ShowMessageBox(true);
            }
            else
            {
                if (string.IsNullOrEmpty(P_BEAMLOT))
                {
                    "Beam Lot isn't null".ShowMessageBox(true);
                }
                else if (string.IsNullOrEmpty(P_ITMPREPARE))
                {
                    "Item Prepare isn't null".ShowMessageBox(true);
                }
                else if (string.IsNullOrEmpty(P_DRAWINGTYPE))
                {
                    "Reed Type isn't null".ShowMessageBox(true);
                }
                else if (string.IsNullOrEmpty(P_REEDNO))
                {
                    "Reed No isn't null".ShowMessageBox(true);
                }
                else if (string.IsNullOrEmpty(P_HEALDCOLOR))
                {
                    "Heald Color isn't null".ShowMessageBox(true);
                }
            }

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user, string DisplayName)
        {
            if (opera != null)
            {
                opera = user;
            }

            if (mcName != null)
            {
                mcName = DisplayName;
            }
        }

        #endregion

    }
}

