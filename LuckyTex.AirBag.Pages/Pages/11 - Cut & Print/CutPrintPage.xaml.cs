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

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;

using System.Globalization;
using System.Threading;

using System.Data;//import this namespace

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for CutPrintPage.xaml
    /// </summary>
    public partial class CutPrintPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public CutPrintPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);


            imgTrue1.Visibility = System.Windows.Visibility.Collapsed;
            imgTrue2.Visibility = System.Windows.Visibility.Collapsed;
            imgTrue3.Visibility = System.Windows.Visibility.Collapsed;
            imgTrue4.Visibility = System.Windows.Visibility.Collapsed;

            imgTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
            imgTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
            imgTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
            imgTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;

            img2ndTrue1.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrue2.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrue3.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrue4.Visibility = System.Windows.Visibility.Collapsed;

            img2ndTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;

            txtBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
            txtBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
            txtBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
            txtBEGINLINE4.Visibility = System.Windows.Visibility.Visible;

            txtENDLINE1.Visibility = System.Windows.Visibility.Visible;
            txtENDLINE2.Visibility = System.Windows.Visibility.Visible;
            txtENDLINE3.Visibility = System.Windows.Visibility.Visible;
            txtENDLINE4.Visibility = System.Windows.Visibility.Visible;

            txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
            txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
            txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
            txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Visible;

            txt2ndENDLINE1.Visibility = System.Windows.Visibility.Visible;
            txt2ndENDLINE2.Visibility = System.Windows.Visibility.Visible;
            txt2ndENDLINE3.Visibility = System.Windows.Visibility.Visible;
            txt2ndENDLINE4.Visibility = System.Windows.Visibility.Visible;

        }

        #endregion

        #region Internal Variables

        private CutPrintSession _session = new CutPrintSession();
        DateTime startDate;
        DateTime endDate;
        string PRODUCTTYPEID = string.Empty;

        string BEGINLINE1 = string.Empty;
        string BEGINLINE2 = string.Empty;
        string BEGINLINE3 = string.Empty;
        string BEGINLINE4 = string.Empty;

        string BEGINLINE12nd = string.Empty;
        string BEGINLINE22nd = string.Empty;
        string BEGINLINE32nd = string.Empty;
        string BEGINLINE42nd = string.Empty;

        string ENDLINE1 = string.Empty;
        string ENDLINE2 = string.Empty;
        string ENDLINE3 = string.Empty;
        string ENDLINE4 = string.Empty;

        string ENDLINE12nd = string.Empty;
        string ENDLINE22nd = string.Empty;
        string ENDLINE32nd = string.Empty;
        string ENDLINE42nd = string.Empty;

        bool chkGetData = false;

        // เพิ่ม 13/02/17
        string cmID = string.Empty;

        string ITEMLOT = string.Empty;
        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Private Methods

        #region Inspection Session methods

        private void InitSession()
        {
            if (null != _session)
            {
                _session.OnStateChanged += new EventHandler(_session_OnStateChanged);
            }
        }

        private void ReleaseSession()
        {
            if (null != _session)
            {
                _session.OnStateChanged -= new EventHandler(_session_OnStateChanged);
            }
            _session = null;
        }

        void _session_OnStateChanged(object sender, EventArgs e)
        {
            if (null != _session)
            {

            }
        }

        #endregion

        #endregion

        #region UserControl_Loaded && UserControl_Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStatusLeft();
            LoadStatusRight();

            cmdStart.IsEnabled = false;
            cmdPreview.IsEnabled = false;
            cmdEnd.IsEnabled = false;
            cmdBack.IsEnabled = true;

            cmdClear.IsEnabled = false;
            cmdSuspend.IsEnabled = false;

            cmID = string.Empty;

            //txtITEMLOT.Focus();
            //txtITEMLOT.SelectAll();

            Cut_GetMCSuspendData();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
           
        }

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            startDate = DateTime.Now;
            txtStartTime.Text = startDate.ToString("dd/MM/yy HH:mm");

            if (txtITEMLOT.Text != "" && txtStartTime.Text != "")
            {
                CUT_INSERTDATA();
            }
            else
                "Lot no. is not null".ShowMessageBox(true);
        }

        private void cmdEnd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLengthDetail.Text))
            {
                //เพิ่ม 28/06/17
                if (!string.IsNullOrEmpty(txtTension.Text))
                {
                    endDate = DateTime.Now;
                    txtEndTime.Text = endDate.ToString("dd/MM/yy HH:mm");

                    if (txtITEMLOT.Text != "" && txtEndTime.Text != "")
                    {
                        CUT_UPDATEDATA();

                        D365_CP();

                        PageManager.Instance.Back();
                    }
                }
                else
                {
                    "Tension isn't Null".ShowMessageBox();
                }
            }
            else
            {
                "Cutting Length isn't Null".ShowMessageBox();
            }
        }

        private void cmdPreview_Click(object sender, RoutedEventArgs e)
        {
            if (txtITEMLOT.Text != "")
            {
                Preview(txtITEMLOT.Text);
                cmdBack.IsEnabled = true;
            }
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearCutInfo userToClear = this.ShowClearCutBox(txtMCName.Text, true);
            if (userToClear != null)
            {
                string p_clearby = userToClear.UserName;
                string p_password = userToClear.Password;
                string p_clearremark = userToClear.Remark;

                string processId = "7";

                GetAuthorizeByProcessID(processId, p_clearby, p_password, p_clearremark);
            }
        }

        private void cmdSuspend_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to suspend this lot?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Suspend();
            }
        }

        #endregion

        #region TextBox Handlers

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        private void txtITEMLOT_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtITEMLOT.Text != "")
            {
                if (txtBATCHNO.Text == "" && txtFINISHINGLOT.Text == "")
                {
                    LoadCUT_GETFINISHINGDATA(txtITEMLOT.Text);
                }
                else
                {
                    string itemLot = txtITEMLOT.Text;

                    ClearCUT_GETFINISHINGDATA();

                    txtITEMLOT.Text = itemLot;
                    LoadCUT_GETFINISHINGDATA(itemLot);
                }
            }
            else
                ClearCUT_GETFINISHINGDATA();
        }

        private void txtITEMLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH1.Focus();
                txtWIDTH1.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTH1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH2.Focus();
                txtWIDTH2.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTH2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH3.Focus();
                txtWIDTH3.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTH3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH4.Focus();
                txtWIDTH4.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTH4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtDISTANTBAR1.IsEnabled == true)
                {
                    txtDISTANTBAR1.Focus();
                    txtDISTANTBAR1.SelectAll();
                }
                else
                {
                    txtDENWARP.Focus();
                    txtDENWARP.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtDISTANTBAR1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDISTANTBAR2.Focus();
                txtDISTANTBAR2.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDISTANTBAR2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDISTANTBAR3.Focus();
                txtDISTANTBAR3.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDISTANTBAR3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDISTANTBAR4.Focus();
                txtDISTANTBAR4.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDISTANTBAR4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDISTANTLINE1.Focus();
                txtDISTANTLINE1.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDISTANTLINE1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDISTANTLINE2.Focus();
                txtDISTANTLINE2.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDISTANTLINE2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDISTANTLINE3.Focus();
                txtDISTANTLINE3.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDISTANTLINE3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDENWARP.Focus();
                txtDENWARP.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDENWARP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDENWEFT.Focus();
                txtDENWEFT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDENWEFT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSPEED.Focus();
                txtSPEED.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSPEED_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTHBE.Focus();
                txtWIDTHBE.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTHBE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTHAF.Focus();
                txtWIDTHAF.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTHAF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTHAF_END.Focus();
                txtWIDTHAF_END.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTHAF_END_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBEGINLINE1.Focus();
                txtBEGINLINE1.SelectAll();
                e.Handled = true;
            }
        }


        #region BEGINLINE1 KeyDown

        private void txtBEGINLINE1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtBEGINLINE2.IsEnabled == true)
                {
                    txtBEGINLINE2.Focus();
                    txtBEGINLINE2.SelectAll();
                }

                #region Old
                //10/08/20
                //if (txt2ndBEGINLINE1.IsEnabled == true)
                //{
                //    txt2ndBEGINLINE1.Focus();
                //    txt2ndBEGINLINE1.SelectAll();
                //}
                //else
                //{
                //    txtBEGINLINE2.Focus();
                //    txtBEGINLINE2.SelectAll();
                //}

                //if (txt2ndBatchNo1.IsEnabled == false)
                //{
                //    txtENDLINE1.Focus();
                //    txtENDLINE1.SelectAll();
                //}
                //else
                //{
                //    txt2ndBEGINLINE1.Focus();
                //    txt2ndBEGINLINE1.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }

        private void txt2ndBEGINLINE1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txt2ndBEGINLINE2.IsEnabled == true)
                {
                    txt2ndBEGINLINE2.Focus();
                    txt2ndBEGINLINE2.SelectAll();
                }

                #region Old
                //10/08/20
                //if (txtBEGINLINE2.IsEnabled == true)
                //{
                //    txtBEGINLINE2.Focus();
                //    txtBEGINLINE2.SelectAll();
                //}
                //else
                //{
                //    txtENDLINE1.Focus();
                //    txtENDLINE1.SelectAll();
                //}

                //if (string.IsNullOrEmpty(txtENDLINE1.Text))
                //{
                //    txtENDLINE1.Focus();
                //    txtENDLINE1.SelectAll();
                //}
                //else
                //{
                //    txt2ndENDLINE1.Focus();
                //    txt2ndENDLINE1.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }

        private void txtENDLINE1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtENDLINE2.IsEnabled == true)
                {
                    txtENDLINE2.Focus();
                    txtENDLINE2.SelectAll();
                }
                else if (txtENDLINE2.IsEnabled == false && txt2ndENDLINE1.IsEnabled == true)
                {
                    txt2ndENDLINE1.Focus();
                    txt2ndENDLINE1.SelectAll();
                }

                #region Old
                //10/08/20
                //if (txt2ndENDLINE1.IsEnabled == true)
                //{
                //    txt2ndENDLINE1.Focus();
                //    txt2ndENDLINE1.SelectAll();
                //}
                //else
                //{
                //    txtENDLINE2.Focus();
                //    txtENDLINE2.SelectAll();
                //}

                //if (txt2ndBatchNo1.IsEnabled == false)
                //{
                //    txtBEGINLINE2.Focus();
                //    txtBEGINLINE2.SelectAll();
                //}
                //else
                //{
                //    txt2ndENDLINE1.Focus();
                //    txt2ndENDLINE1.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }
      
        private void txt2ndENDLINE1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txt2ndENDLINE2.IsEnabled == true)
                {
                    txt2ndENDLINE2.Focus();
                    txt2ndENDLINE2.SelectAll();
                }
                else if (txtREMARK.IsEnabled == true && txt2ndENDLINE2.IsEnabled == false)
                {
                    txtREMARK.Focus();
                    txtREMARK.SelectAll();
                }
                #region Old
                //txtBEGINLINE2.Focus();
                //txtBEGINLINE2.SelectAll();
                #endregion

                e.Handled = true;
            }
        }

        #endregion

        #region BEGINLINE2 KeyDown

        private void txtBEGINLINE2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtBEGINLINE3.IsEnabled == true)
                {
                    txtBEGINLINE3.Focus();
                    txtBEGINLINE3.SelectAll();
                }

                #region Old
                //10/08/20
                //if (txt2ndBEGINLINE2.IsEnabled == true)
                //{
                //    txt2ndBEGINLINE2.Focus();
                //    txt2ndBEGINLINE2.SelectAll();
                //}
                //else
                //{
                //    txtBEGINLINE3.Focus();
                //    txtBEGINLINE3.SelectAll();
                //}

                //if (txt2ndBatchNo2.IsEnabled == false)
                //{
                //    txtENDLINE2.Focus();
                //    txtENDLINE2.SelectAll();
                //}
                //else
                //{
                //    txt2ndBEGINLINE2.Focus();
                //    txt2ndBEGINLINE2.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }

        private void txt2ndBEGINLINE2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txt2ndBEGINLINE3.IsEnabled == true)
                {
                    txt2ndBEGINLINE3.Focus();
                    txt2ndBEGINLINE3.SelectAll();
                }

                #region Old
                //10/08/20
                //if (txtBEGINLINE2.IsEnabled == true)
                //{
                //    txtBEGINLINE3.Focus();
                //    txtBEGINLINE3.SelectAll();
                //}

                //if (string.IsNullOrEmpty(txtENDLINE2.Text))
                //{
                //    txtENDLINE2.Focus();
                //    txtENDLINE2.SelectAll();
                //}
                //else
                //{
                //    txt2ndENDLINE2.Focus();
                //    txt2ndENDLINE2.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }

        private void txtENDLINE2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtENDLINE3.IsEnabled == true)
                {
                    txtENDLINE3.Focus();
                    txtENDLINE3.SelectAll();
                }

                #region Old
                //10/08/20
                //if (txt2ndENDLINE2.IsEnabled == true)
                //{
                //    txt2ndENDLINE2.Focus();
                //    txt2ndENDLINE2.SelectAll();
                //}
                //else
                //{
                //    txtENDLINE3.Focus();
                //    txtENDLINE3.SelectAll();
                //}

                //if (txt2ndBatchNo2.IsEnabled == false)
                //{
                //    txtBEGINLINE3.Focus();
                //    txtBEGINLINE3.SelectAll();
                //}
                //else
                //{
                //    txt2ndENDLINE2.Focus();
                //    txt2ndENDLINE2.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }
  
        private void txt2ndENDLINE2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txt2ndENDLINE3.IsEnabled == true)
                {
                    txt2ndENDLINE3.Focus();
                    txt2ndENDLINE3.SelectAll();
                }
                else if (txt2ndENDLINE3.IsEnabled == false && txtREMARK.IsEnabled == true)
                {
                    txtREMARK.Focus();
                    txtREMARK.SelectAll();
                }

                #region Old
                //txtENDLINE3.Focus();
                //txtENDLINE3.SelectAll();

                //txtBEGINLINE3.Focus();
                //txtBEGINLINE3.SelectAll();
                #endregion

                e.Handled = true;
            }
        }

        #endregion

        #region BEGINLINE3 KeyDown

        private void txtBEGINLINE3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtBEGINLINE4.IsEnabled == true)
                {
                    txtBEGINLINE4.Focus();
                    txtBEGINLINE4.SelectAll();
                }

                #region Old
                //10/08/20
                //if (txt2ndBEGINLINE3.IsEnabled == true)
                //{
                //    txt2ndBEGINLINE3.Focus();
                //    txt2ndBEGINLINE3.SelectAll();
                //}
                //else
                //{
                //    txtBEGINLINE4.Focus();
                //    txtBEGINLINE4.SelectAll();
                //}

                //if (txt2ndBatchNo3.IsEnabled == false)
                //{
                //    txtENDLINE3.Focus();
                //    txtENDLINE3.SelectAll();
                //}
                //else
                //{
                //    txt2ndBEGINLINE3.Focus();
                //    txt2ndBEGINLINE3.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }

        private void txt2ndBEGINLINE3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txt2ndBEGINLINE4.IsEnabled == true)
                {
                    txt2ndBEGINLINE4.Focus();
                    txt2ndBEGINLINE4.SelectAll();
                }

                #region Old
                //10/08/20
                //txtBEGINLINE4.Focus();
                //txtBEGINLINE4.SelectAll();


                //if (string.IsNullOrEmpty(txtENDLINE3.Text))
                //{
                //    txtENDLINE3.Focus();
                //    txtENDLINE3.SelectAll();
                //}
                //else
                //{
                //    txt2ndENDLINE3.Focus();
                //    txt2ndENDLINE3.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }

        private void txtENDLINE3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtENDLINE4.IsEnabled == true)
                {
                    txtENDLINE4.Focus();
                    txtENDLINE4.SelectAll();
                }

                #region Old
                //10/08/20
                //if (txt2ndENDLINE3.IsEnabled == true)
                //{
                //    txt2ndENDLINE3.Focus();
                //    txt2ndENDLINE3.SelectAll();
                //}
                //else
                //{
                //    txtENDLINE4.Focus();
                //    txtENDLINE4.SelectAll();
                //}

                //if (txt2ndBatchNo3.IsEnabled == false)
                //{
                //    txtBEGINLINE4.Focus();
                //    txtBEGINLINE4.SelectAll();
                //}
                //else
                //{
                //    txt2ndENDLINE3.Focus();
                //    txt2ndENDLINE3.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }

        private void txt2ndENDLINE3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txt2ndENDLINE4.IsEnabled == true)
                {
                    txt2ndENDLINE4.Focus();
                    txt2ndENDLINE4.SelectAll();
                }
                else if (txt2ndENDLINE4.IsEnabled == false && txtREMARK.IsEnabled == true)
                {
                    txtREMARK.Focus();
                    txtREMARK.SelectAll();
                }

                #region Old
                //txtBEGINLINE4.Focus();
                //txtBEGINLINE4.SelectAll();
                #endregion

                e.Handled = true;
            }
        }

        #endregion

        #region BEGINLINE4 KeyDown

        private void txtBEGINLINE4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txt2ndBEGINLINE1.IsEnabled == true)
                {
                    txt2ndBEGINLINE1.Focus();
                    txt2ndBEGINLINE1.SelectAll();
                }
                else if (txt2ndBEGINLINE1.IsEnabled == false && txtENDLINE1.IsEnabled == true)
                {
                    txtENDLINE1.Focus();
                    txtENDLINE1.SelectAll();
                }

                #region Old
                //10/08/20
                //if (txt2ndBEGINLINE4.IsEnabled == true)
                //{
                //    txt2ndBEGINLINE4.Focus();
                //    txt2ndBEGINLINE4.SelectAll();
                //}
                //else
                //{
                //    txtENDLINE1.Focus();
                //    txtENDLINE1.SelectAll();
                //}

                //if (txt2ndBatchNo4.IsEnabled == false)
                //{
                //    txtENDLINE4.Focus();
                //    txtENDLINE4.SelectAll();
                //}
                //else
                //{
                //    txt2ndBEGINLINE4.Focus();
                //    txt2ndBEGINLINE4.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }

        private void txt2ndBEGINLINE4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtENDLINE1.Focus();
                txtENDLINE1.SelectAll();

                #region Old
                //if (string.IsNullOrEmpty(txtENDLINE4.Text))
                //{
                //    txtENDLINE4.Focus();
                //    txtENDLINE4.SelectAll();
                //}
                //else
                //{
                //    txt2ndENDLINE4.Focus();
                //    txt2ndENDLINE4.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }

        private void txtENDLINE4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txt2ndENDLINE1.IsEnabled == true)
                {
                    txt2ndENDLINE1.Focus();
                    txt2ndENDLINE1.SelectAll();
                }
                else if (txt2ndENDLINE1.IsEnabled == false && txtREMARK.IsEnabled == true)
                {
                    txtREMARK.Focus();
                    txtREMARK.SelectAll();
                }

                #region Old
                //if (txt2ndENDLINE4.IsEnabled == true)
                //{
                //    txt2ndENDLINE4.Focus();
                //    txt2ndENDLINE4.SelectAll();
                //}
                //else
                //{
                //    txtREMARK.Focus();
                //    txtREMARK.SelectAll();
                //}

                //if (txt2ndBatchNo4.IsEnabled == false)
                //{
                //    txtREMARK.Focus();
                //    txtREMARK.SelectAll();
                //}
                //else
                //{
                //    txt2ndENDLINE4.Focus();
                //    txt2ndENDLINE4.SelectAll();
                //}
                #endregion

                e.Handled = true;
            }
        }

        private void txt2ndENDLINE4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtREMARK.Focus();
                txtREMARK.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region BEGINLINE1 LostFocus

        private void txtBEGINLINE1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBEGINLINE1.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txtBatchNo1.Text == txtBEGINLINE1.Text)
                        //    {
                        //        imgTrue1.Visibility = System.Windows.Visibility.Visible;
                        //        txtBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE1 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtBEGINLINE1.Text = "";
                        //            txtBEGINLINE1.SelectAll();
                        //            txtBEGINLINE1.Focus();

                        //            imgTrue1.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE1 = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE1 = txtBEGINLINE1.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txtBEGINLINE1.Text.TrimStart().TrimEnd();

                        //    if (txtBatchNo1.Text == beging)
                        //    {
                        //        imgTrue1.Visibility = System.Windows.Visibility.Visible;
                        //        txtBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE1 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtBEGINLINE1.Text = "";
                        //            txtBEGINLINE1.SelectAll();
                        //            txtBEGINLINE1.Focus();

                        //            imgTrue1.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE1 = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE1 = beging;
                        //            txtBEGINLINE1.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtBEGINLINE1.Text.TrimStart().TrimEnd();

                        if (txtBatchNo1.Text == beging)
                        {
                            imgTrue1.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE1 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtBEGINLINE1.Text = "";
                                txtBEGINLINE1.SelectAll();
                                txtBEGINLINE1.Focus();

                                imgTrue1.Visibility = System.Windows.Visibility.Collapsed;
                                txtBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE1 = "";
                            }
                            else
                            {
                                BEGINLINE1 = beging;
                                txtBEGINLINE1.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtBEGINLINE1.Text.TrimStart().TrimEnd();

                        if (txtBatchNo1.Text == beging)
                        {
                            imgTrue1.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE1 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtBEGINLINE1.Text = "";
                                txtBEGINLINE1.SelectAll();
                                txtBEGINLINE1.Focus();

                                imgTrue1.Visibility = System.Windows.Visibility.Collapsed;
                                txtBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE1 = "";
                            }
                            else
                            {
                                BEGINLINE1 = beging;
                                txtBEGINLINE1.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txtBatchNo1.Text == txtBEGINLINE1.Text)
                    {
                        imgTrue1.Visibility = System.Windows.Visibility.Visible;
                        txtBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        BEGINLINE1 = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txtBEGINLINE1.Text = "";
                            txtBEGINLINE1.SelectAll();
                            txtBEGINLINE1.Focus();

                            imgTrue1.Visibility = System.Windows.Visibility.Collapsed;
                            txtBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE1 = "";
                        }
                        else
                        {
                            BEGINLINE1 = txtBEGINLINE1.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                imgTrue1.Visibility = System.Windows.Visibility.Collapsed;
                txtBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                BEGINLINE1 = "";
            }
        }

        private void txtENDLINE1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtENDLINE1.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txtBatchNo1.Text == txtENDLINE1.Text)
                        //    {
                        //        imgTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                        //        txtENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE1 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtENDLINE1.Text = "";
                        //            txtENDLINE1.SelectAll();
                        //            txtENDLINE1.Focus();

                        //            imgTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtENDLINE1.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE1 = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE1 = txtENDLINE1.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txtENDLINE1.Text.TrimStart().TrimEnd();

                        //    if (txtBatchNo1.Text == beging)
                        //    {
                        //        imgTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                        //        txtENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE1 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtENDLINE1.Text = "";
                        //            txtENDLINE1.SelectAll();
                        //            txtENDLINE1.Focus();

                        //            imgTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtENDLINE1.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE1 = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE1 = beging;
                        //            txtENDLINE1.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtENDLINE1.Text.TrimStart().TrimEnd();

                        if (txtBatchNo1.Text == beging)
                        {
                            imgTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE1 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtENDLINE1.Text = "";
                                txtENDLINE1.SelectAll();
                                txtENDLINE1.Focus();

                                imgTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                                txtENDLINE1.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE1 = "";
                            }
                            else
                            {
                                ENDLINE1 = beging;
                                txtENDLINE1.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtENDLINE1.Text.TrimStart().TrimEnd();

                        if (txtBatchNo1.Text == beging)
                        {
                            imgTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE1 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtENDLINE1.Text = "";
                                txtENDLINE1.SelectAll();
                                txtENDLINE1.Focus();

                                imgTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                                txtENDLINE1.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE1 = "";
                            }
                            else
                            {
                                ENDLINE1 = beging;
                                txtENDLINE1.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txtBatchNo1.Text == txtENDLINE1.Text)
                    {
                        imgTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                        txtENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        ENDLINE1 = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txtENDLINE1.Text = "";
                            txtENDLINE1.SelectAll();
                            txtENDLINE1.Focus();

                            imgTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                            txtENDLINE1.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE1 = "";
                        }
                        else
                        {
                            ENDLINE1 = txtENDLINE1.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                imgTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                txtENDLINE1.Visibility = System.Windows.Visibility.Visible;
                ENDLINE1 = "";
            }
        }
   
        private void txt2ndBEGINLINE1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt2ndBEGINLINE1.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txt2ndBatchNo1.Text == txt2ndBEGINLINE1.Text)
                        //    {
                        //        img2ndTrue1.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE12nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndBEGINLINE1.Text = "";
                        //            txt2ndBEGINLINE1.SelectAll();
                        //            txt2ndBEGINLINE1.Focus();

                        //            img2ndTrue1.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE12nd = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE12nd = txt2ndBEGINLINE1.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txt2ndBEGINLINE1.Text.TrimStart().TrimEnd();

                        //    if (txt2ndBatchNo1.Text == beging)
                        //    {
                        //        img2ndTrue1.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE12nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndBEGINLINE1.Text = "";
                        //            txt2ndBEGINLINE1.SelectAll();
                        //            txt2ndBEGINLINE1.Focus();

                        //            img2ndTrue1.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE12nd = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE12nd = beging;
                        //            txt2ndBEGINLINE1.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndBEGINLINE1.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo1.Text == beging)
                        {
                            img2ndTrue1.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE12nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndBEGINLINE1.Text = "";
                                txt2ndBEGINLINE1.SelectAll();
                                txt2ndBEGINLINE1.Focus();

                                img2ndTrue1.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE12nd = "";
                            }
                            else
                            {
                                BEGINLINE12nd = beging;
                                txt2ndBEGINLINE1.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndBEGINLINE1.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo1.Text == beging)
                        {
                            img2ndTrue1.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE12nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndBEGINLINE1.Text = "";
                                txt2ndBEGINLINE1.SelectAll();
                                txt2ndBEGINLINE1.Focus();

                                img2ndTrue1.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE12nd = "";
                            }
                            else
                            {
                                BEGINLINE12nd = beging;
                                txt2ndBEGINLINE1.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txt2ndBatchNo1.Text == txt2ndBEGINLINE1.Text)
                    {
                        img2ndTrue1.Visibility = System.Windows.Visibility.Visible;
                        txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        BEGINLINE12nd = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txt2ndBEGINLINE1.Text = "";
                            txt2ndBEGINLINE1.SelectAll();
                            txt2ndBEGINLINE1.Focus();

                            img2ndTrue1.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE12nd = "";
                        }
                        else
                        {
                            BEGINLINE12nd = txt2ndBEGINLINE1.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                img2ndTrue1.Visibility = System.Windows.Visibility.Collapsed;
                txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                BEGINLINE12nd = "";
            }
        }

        private void txt2ndENDLINE1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt2ndENDLINE1.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txt2ndBatchNo1.Text == txt2ndENDLINE1.Text)
                        //    {
                        //        img2ndTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE12nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndENDLINE1.Text = "";
                        //            txt2ndENDLINE1.SelectAll();
                        //            txt2ndENDLINE1.Focus();

                        //            img2ndTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndENDLINE1.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE12nd = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE12nd = txt2ndENDLINE1.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txt2ndENDLINE1.Text.TrimStart().TrimEnd();

                        //    if (txt2ndBatchNo1.Text == beging)
                        //    {
                        //        img2ndTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE12nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndENDLINE1.Text = "";
                        //            txt2ndENDLINE1.SelectAll();
                        //            txt2ndENDLINE1.Focus();

                        //            img2ndTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndENDLINE1.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE12nd = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE12nd = beging;
                        //            txt2ndENDLINE1.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndENDLINE1.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo1.Text == beging)
                        {
                            img2ndTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE12nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndENDLINE1.Text = "";
                                txt2ndENDLINE1.SelectAll();
                                txt2ndENDLINE1.Focus();

                                img2ndTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndENDLINE1.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE12nd = "";
                            }
                            else
                            {
                                ENDLINE12nd = beging;
                                txt2ndENDLINE1.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndENDLINE1.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo1.Text == beging)
                        {
                            img2ndTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE12nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndENDLINE1.Text = "";
                                txt2ndENDLINE1.SelectAll();
                                txt2ndENDLINE1.Focus();

                                img2ndTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndENDLINE1.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE12nd = "";
                            }
                            else
                            {
                                ENDLINE12nd = beging;
                                txt2ndENDLINE1.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txt2ndBatchNo1.Text == txt2ndENDLINE1.Text)
                    {
                        img2ndTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                        txt2ndENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                        ENDLINE12nd = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txt2ndENDLINE1.Text = "";
                            txt2ndENDLINE1.SelectAll();
                            txt2ndENDLINE1.Focus();

                            img2ndTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndENDLINE1.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE12nd = "";
                        }
                        else
                        {
                            ENDLINE12nd = txt2ndENDLINE1.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                img2ndTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                txt2ndENDLINE1.Visibility = System.Windows.Visibility.Visible;
                ENDLINE12nd = "";
            }
        }

        #endregion

        #region BEGINLINE2 LostFocus

        private void txtBEGINLINE2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBEGINLINE2.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txtBatchNo2.Text == txtBEGINLINE2.Text)
                        //    {
                        //        imgTrue2.Visibility = System.Windows.Visibility.Visible;
                        //        txtBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE2 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtBEGINLINE2.Text = "";
                        //            txtBEGINLINE2.SelectAll();
                        //            txtBEGINLINE2.Focus();

                        //            imgTrue2.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE2 = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE2 = txtBEGINLINE2.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txtBEGINLINE2.Text.TrimStart().TrimEnd();

                        //    if (txtBatchNo2.Text == beging)
                        //    {
                        //        imgTrue2.Visibility = System.Windows.Visibility.Visible;
                        //        txtBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE2 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtBEGINLINE2.Text = "";
                        //            txtBEGINLINE2.SelectAll();
                        //            txtBEGINLINE2.Focus();

                        //            imgTrue2.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE2 = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE2 = beging;
                        //            txtBEGINLINE2.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtBEGINLINE2.Text.TrimStart().TrimEnd();

                        if (txtBatchNo2.Text == beging)
                        {
                            imgTrue2.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE2 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtBEGINLINE2.Text = "";
                                txtBEGINLINE2.SelectAll();
                                txtBEGINLINE2.Focus();

                                imgTrue2.Visibility = System.Windows.Visibility.Collapsed;
                                txtBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE2 = "";
                            }
                            else
                            {
                                BEGINLINE2 = beging;
                                txtBEGINLINE2.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtBEGINLINE2.Text.TrimStart().TrimEnd();

                        if (txtBatchNo2.Text == beging)
                        {
                            imgTrue2.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE2 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtBEGINLINE2.Text = "";
                                txtBEGINLINE2.SelectAll();
                                txtBEGINLINE2.Focus();

                                imgTrue2.Visibility = System.Windows.Visibility.Collapsed;
                                txtBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE2 = "";
                            }
                            else
                            {
                                BEGINLINE2 = beging;
                                txtBEGINLINE2.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txtBatchNo2.Text == txtBEGINLINE2.Text)
                    {
                        imgTrue2.Visibility = System.Windows.Visibility.Visible;
                        txtBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        BEGINLINE2 = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txtBEGINLINE2.Text = "";
                            txtBEGINLINE2.SelectAll();
                            txtBEGINLINE2.Focus();

                            imgTrue2.Visibility = System.Windows.Visibility.Collapsed;
                            txtBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE2 = "";
                        }
                        else
                        {
                            BEGINLINE2 = txtBEGINLINE2.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                imgTrue2.Visibility = System.Windows.Visibility.Collapsed;
                txtBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                BEGINLINE2 = "";
            }
        }

        private void txtENDLINE2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtENDLINE2.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txtBatchNo2.Text == txtENDLINE2.Text)
                        //    {
                        //        imgTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                        //        txtENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE2 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtENDLINE2.Text = "";
                        //            txtENDLINE2.SelectAll();
                        //            txtENDLINE2.Focus();

                        //            imgTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtENDLINE2.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE2 = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE2 = txtENDLINE2.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txtENDLINE2.Text.TrimStart().TrimEnd();

                        //    if (txtBatchNo2.Text == beging)
                        //    {
                        //        imgTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                        //        txtENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE2 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtENDLINE2.Text = "";
                        //            txtENDLINE2.SelectAll();
                        //            txtENDLINE2.Focus();

                        //            imgTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtENDLINE2.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE2 = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE2 = beging;
                        //            txtENDLINE2.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtENDLINE2.Text.TrimStart().TrimEnd();

                        if (txtBatchNo2.Text == beging)
                        {
                            imgTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE2 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtENDLINE2.Text = "";
                                txtENDLINE2.SelectAll();
                                txtENDLINE2.Focus();

                                imgTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                                txtENDLINE2.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE2 = "";
                            }
                            else
                            {
                                ENDLINE2 = beging;
                                txtENDLINE2.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtENDLINE2.Text.TrimStart().TrimEnd();

                        if (txtBatchNo2.Text == beging)
                        {
                            imgTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE2 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtENDLINE2.Text = "";
                                txtENDLINE2.SelectAll();
                                txtENDLINE2.Focus();

                                imgTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                                txtENDLINE2.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE2 = "";
                            }
                            else
                            {
                                ENDLINE2 = beging;
                                txtENDLINE2.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txtBatchNo2.Text == txtENDLINE2.Text)
                    {
                        imgTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                        txtENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        ENDLINE2 = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txtENDLINE2.Text = "";
                            txtENDLINE2.SelectAll();
                            txtENDLINE2.Focus();

                            imgTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                            txtENDLINE2.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE2 = "";
                        }
                        else
                        {
                            ENDLINE2 = txtENDLINE2.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                imgTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                txtENDLINE2.Visibility = System.Windows.Visibility.Visible;
                ENDLINE2 = "";
            }
        }

        private void txt2ndBEGINLINE2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt2ndBEGINLINE2.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txt2ndBatchNo2.Text == txt2ndBEGINLINE2.Text)
                        //    {
                        //        img2ndTrue2.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE22nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndBEGINLINE2.Text = "";
                        //            txt2ndBEGINLINE2.SelectAll();
                        //            txt2ndBEGINLINE2.Focus();

                        //            img2ndTrue2.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE22nd = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE22nd = txt2ndBEGINLINE2.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txt2ndBEGINLINE2.Text.TrimStart().TrimEnd();

                        //    if (txt2ndBatchNo2.Text == beging)
                        //    {
                        //        img2ndTrue2.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE22nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndBEGINLINE2.Text = "";
                        //            txt2ndBEGINLINE2.SelectAll();
                        //            txt2ndBEGINLINE2.Focus();

                        //            img2ndTrue2.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE22nd = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE22nd = beging;
                        //            txt2ndBEGINLINE2.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndBEGINLINE2.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo2.Text == beging)
                        {
                            img2ndTrue2.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE22nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndBEGINLINE2.Text = "";
                                txt2ndBEGINLINE2.SelectAll();
                                txt2ndBEGINLINE2.Focus();

                                img2ndTrue2.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE22nd = "";
                            }
                            else
                            {
                                BEGINLINE22nd = beging;
                                txt2ndBEGINLINE2.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndBEGINLINE2.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo2.Text == beging)
                        {
                            img2ndTrue2.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE22nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndBEGINLINE2.Text = "";
                                txt2ndBEGINLINE2.SelectAll();
                                txt2ndBEGINLINE2.Focus();

                                img2ndTrue2.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE22nd = "";
                            }
                            else
                            {
                                BEGINLINE22nd = beging;
                                txt2ndBEGINLINE2.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txt2ndBatchNo2.Text == txt2ndBEGINLINE2.Text)
                    {
                        img2ndTrue2.Visibility = System.Windows.Visibility.Visible;
                        txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        BEGINLINE22nd = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txt2ndBEGINLINE2.Text = "";
                            txt2ndBEGINLINE2.SelectAll();
                            txt2ndBEGINLINE2.Focus();

                            img2ndTrue2.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE22nd = "";
                        }
                        else
                        {
                            BEGINLINE22nd = txt2ndBEGINLINE2.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                img2ndTrue2.Visibility = System.Windows.Visibility.Collapsed;
                txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                BEGINLINE22nd = "";
            }
        }

        private void txt2ndENDLINE2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt2ndENDLINE2.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txt2ndBatchNo2.Text == txt2ndENDLINE2.Text)
                        //    {
                        //        img2ndTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE22nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndENDLINE2.Text = "";
                        //            txt2ndENDLINE2.SelectAll();
                        //            txt2ndENDLINE2.Focus();

                        //            img2ndTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndENDLINE2.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE22nd = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE22nd = txt2ndENDLINE2.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txt2ndENDLINE2.Text.TrimStart().TrimEnd();

                        //    if (txt2ndBatchNo2.Text == beging)
                        //    {
                        //        img2ndTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE22nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndENDLINE2.Text = "";
                        //            txt2ndENDLINE2.SelectAll();
                        //            txt2ndENDLINE2.Focus();

                        //            img2ndTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndENDLINE2.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE22nd = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE22nd = beging;
                        //            txt2ndENDLINE2.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndENDLINE2.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo2.Text == beging)
                        {
                            img2ndTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE22nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndENDLINE2.Text = "";
                                txt2ndENDLINE2.SelectAll();
                                txt2ndENDLINE2.Focus();

                                img2ndTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndENDLINE2.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE22nd = "";
                            }
                            else
                            {
                                ENDLINE22nd = beging;
                                txt2ndENDLINE2.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndENDLINE2.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo2.Text == beging)
                        {
                            img2ndTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE22nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndENDLINE2.Text = "";
                                txt2ndENDLINE2.SelectAll();
                                txt2ndENDLINE2.Focus();

                                img2ndTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndENDLINE2.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE22nd = "";
                            }
                            else
                            {
                                ENDLINE22nd = beging;
                                txt2ndENDLINE2.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txt2ndBatchNo2.Text == txt2ndENDLINE2.Text)
                    {
                        img2ndTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                        txt2ndENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                        ENDLINE22nd = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txt2ndENDLINE2.Text = "";
                            txt2ndENDLINE2.SelectAll();
                            txt2ndENDLINE2.Focus();

                            img2ndTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndENDLINE2.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE22nd = "";
                        }
                        else
                        {
                            ENDLINE22nd = txt2ndENDLINE2.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                img2ndTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                txt2ndENDLINE2.Visibility = System.Windows.Visibility.Visible;
                ENDLINE22nd = "";
            }
        }

        #endregion

        #region BEGINLINE3 LostFocus

        private void txtBEGINLINE3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBEGINLINE3.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txtBatchNo3.Text == txtBEGINLINE3.Text)
                        //    {
                        //        imgTrue3.Visibility = System.Windows.Visibility.Visible;
                        //        txtBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE3 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtBEGINLINE3.Text = "";
                        //            txtBEGINLINE3.SelectAll();
                        //            txtBEGINLINE3.Focus();

                        //            imgTrue3.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE3 = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE3 = txtBEGINLINE3.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txtBEGINLINE3.Text.TrimStart().TrimEnd();

                        //    if (txtBatchNo3.Text == beging)
                        //    {
                        //        imgTrue3.Visibility = System.Windows.Visibility.Visible;
                        //        txtBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE3 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtBEGINLINE3.Text = "";
                        //            txtBEGINLINE3.SelectAll();
                        //            txtBEGINLINE3.Focus();

                        //            imgTrue3.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE3 = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE3 = beging;
                        //            txtBEGINLINE3.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtBEGINLINE3.Text.TrimStart().TrimEnd();

                        if (txtBatchNo3.Text == beging)
                        {
                            imgTrue3.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE3 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtBEGINLINE3.Text = "";
                                txtBEGINLINE3.SelectAll();
                                txtBEGINLINE3.Focus();

                                imgTrue3.Visibility = System.Windows.Visibility.Collapsed;
                                txtBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE3 = "";
                            }
                            else
                            {
                                BEGINLINE3 = beging;
                                txtBEGINLINE3.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtBEGINLINE3.Text.TrimStart().TrimEnd();

                        if (txtBatchNo3.Text == beging)
                        {
                            imgTrue3.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE3 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtBEGINLINE3.Text = "";
                                txtBEGINLINE3.SelectAll();
                                txtBEGINLINE3.Focus();

                                imgTrue3.Visibility = System.Windows.Visibility.Collapsed;
                                txtBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE3 = "";
                            }
                            else
                            {
                                BEGINLINE3 = beging;
                                txtBEGINLINE3.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txtBatchNo3.Text == txtBEGINLINE3.Text)
                    {
                        imgTrue3.Visibility = System.Windows.Visibility.Visible;
                        txtBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        BEGINLINE3 = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txtBEGINLINE3.Text = "";
                            txtBEGINLINE3.SelectAll();
                            txtBEGINLINE3.Focus();

                            imgTrue3.Visibility = System.Windows.Visibility.Collapsed;
                            txtBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE3 = "";
                        }
                        else
                        {
                            BEGINLINE3 = txtBEGINLINE3.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                imgTrue3.Visibility = System.Windows.Visibility.Collapsed;
                txtBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                BEGINLINE3 = "";
            }
        }

        private void txtENDLINE3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtENDLINE3.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txtBatchNo3.Text == txtENDLINE3.Text)
                        //    {
                        //        imgTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                        //        txtENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE3 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtENDLINE3.Text = "";
                        //            txtENDLINE3.SelectAll();
                        //            txtENDLINE3.Focus();

                        //            imgTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtENDLINE3.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE3 = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE3 = txtENDLINE3.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txtENDLINE3.Text.TrimStart().TrimEnd();

                        //    if (txtBatchNo3.Text == beging)
                        //    {
                        //        imgTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                        //        txtENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE3 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtENDLINE3.Text = "";
                        //            txtENDLINE3.SelectAll();
                        //            txtENDLINE3.Focus();

                        //            imgTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtENDLINE3.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE3 = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE3 = beging;
                        //            txtENDLINE3.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtENDLINE3.Text.TrimStart().TrimEnd();

                        if (txtBatchNo3.Text == beging)
                        {
                            imgTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE3 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtENDLINE3.Text = "";
                                txtENDLINE3.SelectAll();
                                txtENDLINE3.Focus();

                                imgTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                                txtENDLINE3.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE3 = "";
                            }
                            else
                            {
                                ENDLINE3 = beging;
                                txtENDLINE3.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtENDLINE3.Text.TrimStart().TrimEnd();

                        if (txtBatchNo3.Text == beging)
                        {
                            imgTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE3 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtENDLINE3.Text = "";
                                txtENDLINE3.SelectAll();
                                txtENDLINE3.Focus();

                                imgTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                                txtENDLINE3.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE3 = "";
                            }
                            else
                            {
                                ENDLINE3 = beging;
                                txtENDLINE3.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txtBatchNo3.Text == txtENDLINE3.Text)
                    {
                        imgTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                        txtENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        ENDLINE3 = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txtENDLINE3.Text = "";
                            txtENDLINE3.SelectAll();
                            txtENDLINE3.Focus();

                            imgTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                            txtENDLINE3.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE3 = "";
                        }
                        else
                        {
                            ENDLINE3 = txtENDLINE3.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                imgTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                txtENDLINE3.Visibility = System.Windows.Visibility.Visible;
                ENDLINE3 = "";
            }
        }

        private void txt2ndBEGINLINE3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt2ndBEGINLINE3.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txt2ndBatchNo3.Text == txt2ndBEGINLINE3.Text)
                        //    {
                        //        img2ndTrue3.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE32nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndBEGINLINE3.Text = "";
                        //            txt2ndBEGINLINE3.SelectAll();
                        //            txt2ndBEGINLINE3.Focus();

                        //            img2ndTrue3.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE32nd = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE32nd = txt2ndBEGINLINE3.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txt2ndBEGINLINE3.Text.TrimStart().TrimEnd();

                        //    if (txt2ndBatchNo3.Text == beging)
                        //    {
                        //        img2ndTrue3.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE32nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndBEGINLINE3.Text = "";
                        //            txt2ndBEGINLINE3.SelectAll();
                        //            txt2ndBEGINLINE3.Focus();

                        //            img2ndTrue3.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE32nd = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE32nd = beging;
                        //            txt2ndBEGINLINE3.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndBEGINLINE3.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo3.Text == beging)
                        {
                            img2ndTrue3.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE32nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndBEGINLINE3.Text = "";
                                txt2ndBEGINLINE3.SelectAll();
                                txt2ndBEGINLINE3.Focus();

                                img2ndTrue3.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE32nd = "";
                            }
                            else
                            {
                                BEGINLINE32nd = beging;
                                txt2ndBEGINLINE3.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndBEGINLINE3.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo3.Text == beging)
                        {
                            img2ndTrue3.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE32nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndBEGINLINE3.Text = "";
                                txt2ndBEGINLINE3.SelectAll();
                                txt2ndBEGINLINE3.Focus();

                                img2ndTrue3.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE32nd = "";
                            }
                            else
                            {
                                BEGINLINE32nd = beging;
                                txt2ndBEGINLINE3.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txt2ndBatchNo3.Text == txt2ndBEGINLINE3.Text)
                    {
                        img2ndTrue3.Visibility = System.Windows.Visibility.Visible;
                        txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        BEGINLINE32nd = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txt2ndBEGINLINE3.Text = "";
                            txt2ndBEGINLINE3.SelectAll();
                            txt2ndBEGINLINE3.Focus();

                            img2ndTrue3.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE32nd = "";
                        }
                        else
                        {
                            BEGINLINE32nd = txt2ndBEGINLINE3.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                img2ndTrue3.Visibility = System.Windows.Visibility.Collapsed;
                txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                BEGINLINE32nd = "";
            }
        }

        private void txt2ndENDLINE3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt2ndENDLINE3.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txt2ndBatchNo3.Text == txt2ndENDLINE3.Text)
                        //    {
                        //        img2ndTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE32nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndENDLINE3.Text = "";
                        //            txt2ndENDLINE3.SelectAll();
                        //            txt2ndENDLINE3.Focus();

                        //            img2ndTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndENDLINE3.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE32nd = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE32nd = txt2ndENDLINE3.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txt2ndENDLINE3.Text.TrimStart().TrimEnd();

                        //    if (txt2ndBatchNo3.Text == beging)
                        //    {
                        //        img2ndTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE32nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndENDLINE3.Text = "";
                        //            txt2ndENDLINE3.SelectAll();
                        //            txt2ndENDLINE3.Focus();

                        //            img2ndTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndENDLINE3.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE32nd = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE32nd = beging;
                        //            txt2ndENDLINE3.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndENDLINE3.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo3.Text == beging)
                        {
                            img2ndTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE32nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndENDLINE3.Text = "";
                                txt2ndENDLINE3.SelectAll();
                                txt2ndENDLINE3.Focus();

                                img2ndTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndENDLINE3.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE32nd = "";
                            }
                            else
                            {
                                ENDLINE32nd = beging;
                                txt2ndENDLINE3.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndENDLINE3.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo3.Text == beging)
                        {
                            img2ndTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE32nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndENDLINE3.Text = "";
                                txt2ndENDLINE3.SelectAll();
                                txt2ndENDLINE3.Focus();

                                img2ndTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndENDLINE3.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE32nd = "";
                            }
                            else
                            {
                                ENDLINE32nd = beging;
                                txt2ndENDLINE3.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txt2ndBatchNo3.Text == txt2ndENDLINE3.Text)
                    {
                        img2ndTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                        txt2ndENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                        ENDLINE32nd = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txt2ndENDLINE3.Text = "";
                            txt2ndENDLINE3.SelectAll();
                            txt2ndENDLINE3.Focus();

                            img2ndTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndENDLINE3.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE32nd = "";
                        }
                        else
                        {
                            ENDLINE32nd = txt2ndENDLINE3.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                img2ndTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                txt2ndENDLINE3.Visibility = System.Windows.Visibility.Visible;
                ENDLINE32nd = "";
            }
        }

        #endregion

        #region BEGINLINE4 LostFocus

        private void txtBEGINLINE4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBEGINLINE4.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txtBatchNo4.Text == txtBEGINLINE4.Text)
                        //    {
                        //        imgTrue4.Visibility = System.Windows.Visibility.Visible;
                        //        txtBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE4 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtBEGINLINE4.Text = "";
                        //            txtBEGINLINE4.SelectAll();
                        //            txtBEGINLINE4.Focus();

                        //            imgTrue4.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE4 = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE4 = txtBEGINLINE4.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txtBEGINLINE4.Text.TrimStart().TrimEnd();

                        //    if (txtBatchNo4.Text == beging)
                        //    {
                        //        imgTrue4.Visibility = System.Windows.Visibility.Visible;
                        //        txtBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE4 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtBEGINLINE4.Text = "";
                        //            txtBEGINLINE4.SelectAll();
                        //            txtBEGINLINE4.Focus();

                        //            imgTrue4.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE4 = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE4 = beging;
                        //            txtBEGINLINE4.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtBEGINLINE4.Text.TrimStart().TrimEnd();

                        if (txtBatchNo4.Text == beging)
                        {
                            imgTrue4.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE4 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtBEGINLINE4.Text = "";
                                txtBEGINLINE4.SelectAll();
                                txtBEGINLINE4.Focus();

                                imgTrue4.Visibility = System.Windows.Visibility.Collapsed;
                                txtBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE4 = "";
                            }
                            else
                            {
                                BEGINLINE4 = beging;
                                txtBEGINLINE4.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtBEGINLINE4.Text.TrimStart().TrimEnd();

                        if (txtBatchNo4.Text == beging)
                        {
                            imgTrue4.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE4 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtBEGINLINE4.Text = "";
                                txtBEGINLINE4.SelectAll();
                                txtBEGINLINE4.Focus();

                                imgTrue4.Visibility = System.Windows.Visibility.Collapsed;
                                txtBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE4 = "";
                            }
                            else
                            {
                                BEGINLINE4 = beging;
                                txtBEGINLINE4.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txtBatchNo4.Text == txtBEGINLINE4.Text)
                    {
                        imgTrue4.Visibility = System.Windows.Visibility.Visible;
                        txtBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        BEGINLINE4 = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txtBEGINLINE4.Text = "";
                            txtBEGINLINE4.SelectAll();
                            txtBEGINLINE4.Focus();

                            imgTrue4.Visibility = System.Windows.Visibility.Collapsed;
                            txtBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE4 = "";
                        }
                        else
                        {
                            BEGINLINE4 = txtBEGINLINE4.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                imgTrue4.Visibility = System.Windows.Visibility.Collapsed;
                txtBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                BEGINLINE4 = "";
            }
        }

        private void txtENDLINE4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtENDLINE4.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txtBatchNo4.Text == txtENDLINE4.Text)
                        //    {
                        //        imgTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                        //        txtENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE4 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtENDLINE4.Text = "";
                        //            txtENDLINE4.SelectAll();
                        //            txtENDLINE4.Focus();

                        //            imgTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtENDLINE4.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE4 = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE4 = txtENDLINE4.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txtENDLINE4.Text.TrimStart().TrimEnd();

                        //    if (txtBatchNo4.Text == beging)
                        //    {
                        //        imgTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                        //        txtENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE4 = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txtENDLINE4.Text = "";
                        //            txtENDLINE4.SelectAll();
                        //            txtENDLINE4.Focus();

                        //            imgTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                        //            txtENDLINE4.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE4 = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE4 = beging;
                        //            txtENDLINE4.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtENDLINE4.Text.TrimStart().TrimEnd();

                        if (txtBatchNo4.Text == beging)
                        {
                            imgTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE4 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtENDLINE4.Text = "";
                                txtENDLINE4.SelectAll();
                                txtENDLINE4.Focus();

                                imgTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                                txtENDLINE4.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE4 = "";
                            }
                            else
                            {
                                ENDLINE4 = beging;
                                txtENDLINE4.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txtENDLINE4.Text.TrimStart().TrimEnd();

                        if (txtBatchNo4.Text == beging)
                        {
                            imgTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE4 = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txtENDLINE4.Text = "";
                                txtENDLINE4.SelectAll();
                                txtENDLINE4.Focus();

                                imgTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                                txtENDLINE4.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE4 = "";
                            }
                            else
                            {
                                ENDLINE4 = beging;
                                txtENDLINE4.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txtBatchNo4.Text == txtENDLINE4.Text)
                    {
                        imgTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                        txtENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        ENDLINE4 = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txtENDLINE4.Text = "";
                            txtENDLINE4.SelectAll();
                            txtENDLINE4.Focus();

                            imgTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                            txtENDLINE4.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE4 = "";
                        }
                        else
                        {
                            ENDLINE4 = txtENDLINE4.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                imgTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                txtENDLINE4.Visibility = System.Windows.Visibility.Visible;
                ENDLINE4 = "";
            }
        }

        private void txt2ndBEGINLINE4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt2ndBEGINLINE4.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txt2ndBatchNo4.Text == txt2ndBEGINLINE4.Text)
                        //    {
                        //        img2ndTrue4.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE42nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndBEGINLINE4.Text = "";
                        //            txt2ndBEGINLINE4.SelectAll();
                        //            txt2ndBEGINLINE4.Focus();

                        //            img2ndTrue4.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE42nd = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE42nd = txt2ndBEGINLINE4.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txt2ndBEGINLINE4.Text.TrimStart().TrimEnd();

                        //    if (txt2ndBatchNo4.Text == beging)
                        //    {
                        //        img2ndTrue4.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        //        BEGINLINE42nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndBEGINLINE4.Text = "";
                        //            txt2ndBEGINLINE4.SelectAll();
                        //            txt2ndBEGINLINE4.Focus();

                        //            img2ndTrue4.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                        //            BEGINLINE42nd = "";
                        //        }
                        //        else
                        //        {
                        //            BEGINLINE42nd = beging;
                        //            txt2ndBEGINLINE4.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndBEGINLINE4.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo4.Text == beging)
                        {
                            img2ndTrue4.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE42nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndBEGINLINE4.Text = "";
                                txt2ndBEGINLINE4.SelectAll();
                                txt2ndBEGINLINE4.Focus();

                                img2ndTrue4.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE42nd = "";
                            }
                            else
                            {
                                BEGINLINE42nd = beging;
                                txt2ndBEGINLINE4.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndBEGINLINE4.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo4.Text == beging)
                        {
                            img2ndTrue4.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE42nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndBEGINLINE4.Text = "";
                                txt2ndBEGINLINE4.SelectAll();
                                txt2ndBEGINLINE4.Focus();

                                img2ndTrue4.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                                BEGINLINE42nd = "";
                            }
                            else
                            {
                                BEGINLINE42nd = beging;
                                txt2ndBEGINLINE4.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txt2ndBatchNo4.Text == txt2ndBEGINLINE4.Text)
                    {
                        img2ndTrue4.Visibility = System.Windows.Visibility.Visible;
                        txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        BEGINLINE42nd = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txt2ndBEGINLINE4.Text = "";
                            txt2ndBEGINLINE4.SelectAll();
                            txt2ndBEGINLINE4.Focus();

                            img2ndTrue4.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE42nd = "";
                        }
                        else
                        {
                            BEGINLINE42nd = txt2ndBEGINLINE4.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                img2ndTrue4.Visibility = System.Windows.Visibility.Collapsed;
                txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                BEGINLINE42nd = "";
            }
        }

        private void txt2ndENDLINE4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt2ndENDLINE4.Text != "")
            {
                if (cmID == "08")
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        #region Not Use ITEMCODE = 09300
                        //if (txtITEMCODE.Text == "09300")
                        //{
                        //    #region ITEMCODE == 09300
                        //    if (txt2ndBatchNo4.Text == txt2ndENDLINE4.Text)
                        //    {
                        //        img2ndTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE42nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndENDLINE4.Text = "";
                        //            txt2ndENDLINE4.SelectAll();
                        //            txt2ndENDLINE4.Focus();

                        //            img2ndTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndENDLINE4.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE42nd = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE42nd = txt2ndENDLINE4.Text;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    #region CMID == 08
                        //    // เพิ่ม 13/02/17
                        //    string beging = txt2ndENDLINE4.Text.TrimStart().TrimEnd();

                        //    if (txt2ndBatchNo4.Text == beging)
                        //    {
                        //        img2ndTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                        //        txt2ndENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        //        ENDLINE42nd = "Yes";
                        //    }
                        //    else
                        //    {
                        //        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //        {
                        //            txt2ndENDLINE4.Text = "";
                        //            txt2ndENDLINE4.SelectAll();
                        //            txt2ndENDLINE4.Focus();

                        //            img2ndTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                        //            txt2ndENDLINE4.Visibility = System.Windows.Visibility.Visible;
                        //            ENDLINE42nd = "";
                        //        }
                        //        else
                        //        {
                        //            ENDLINE42nd = beging;
                        //            txt2ndENDLINE4.Text = beging;
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion

                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndENDLINE4.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo4.Text == beging)
                        {
                            img2ndTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE42nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndENDLINE4.Text = "";
                                txt2ndENDLINE4.SelectAll();
                                txt2ndENDLINE4.Focus();

                                img2ndTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndENDLINE4.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE42nd = "";
                            }
                            else
                            {
                                ENDLINE42nd = beging;
                                txt2ndENDLINE4.Text = beging;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region CMID == 08
                        // เพิ่ม 13/02/17
                        string beging = txt2ndENDLINE4.Text.TrimStart().TrimEnd();

                        if (txt2ndBatchNo4.Text == beging)
                        {
                            img2ndTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE42nd = "Yes";
                        }
                        else
                        {
                            if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                txt2ndENDLINE4.Text = "";
                                txt2ndENDLINE4.SelectAll();
                                txt2ndENDLINE4.Focus();

                                img2ndTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                                txt2ndENDLINE4.Visibility = System.Windows.Visibility.Visible;
                                ENDLINE42nd = "";
                            }
                            else
                            {
                                ENDLINE42nd = beging;
                                txt2ndENDLINE4.Text = beging;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region CMID != 08
                    if (txt2ndBatchNo4.Text == txt2ndENDLINE4.Text)
                    {
                        img2ndTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                        txt2ndENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                        ENDLINE42nd = "Yes";
                    }
                    else
                    {
                        if (MessageBox.Show("Barcode is not correct. Do you want to Confirm again?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            txt2ndENDLINE4.Text = "";
                            txt2ndENDLINE4.SelectAll();
                            txt2ndENDLINE4.Focus();

                            img2ndTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndENDLINE4.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE42nd = "";
                        }
                        else
                        {
                            ENDLINE42nd = txt2ndENDLINE4.Text;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                img2ndTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                txt2ndENDLINE4.Visibility = System.Windows.Visibility.Visible;
                ENDLINE42nd = "";
            }
        }

        #endregion

        #region txtREMARK_KeyDown
        private void txtREMARK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdStart.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region txtLengthDetail_KeyDown
        private void txtLengthDetail_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumeric(e);

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTension.Focus();
                txtTension.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtLengthDetail_LostFocus
        private void txtLengthDetail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLengthDetail.Text))
            {
                Cal();
            }
            else
            {
                txtLengthPrint.Text = "0";
            }
        }
        #endregion

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string ITEMLOT)
        {
            try
            {
                ConmonReportService.Instance.ReportName = "CUT_GETSLIP";
                ConmonReportService.Instance.ITEMLOT = ITEMLOT;

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview

        private void Preview(string ITEMLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "CUT_GETSLIP";
                ConmonReportService.Instance.ITEMLOT = ITEMLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Private Methods

        #region LoadStatus

        private void LoadStatusLeft()
        {
            if (cbStatusLeft.ItemsSource == null)
            {
                string[] str = new string[] { "OK", "NG" };

                cbStatusLeft.ItemsSource = str;
                cbStatusLeft.SelectedIndex = 0;
            }
        }

        private void LoadStatusRight()
        {
            if (cbStatusRight.ItemsSource == null)
            {
                string[] str = new string[] { "OK", "NG" };

                cbStatusRight.ItemsSource = str;
                cbStatusRight.SelectedIndex = 0;
            }
        }

        #endregion

        #region ClearCUT_GETFINISHINGDATA

        private void ClearCUT_GETFINISHINGDATA()
        {
            txtFINISHINGLOT.Text = string.Empty;
            txtITEMLOT.Text = string.Empty;
            txtITEMCODE.Text = string.Empty;
            txtBATCHNO.Text = string.Empty;
            txtStartTime.Text = string.Empty;
            txtEndTime.Text = string.Empty;

            rbMass.IsChecked = true;
            rbTest.IsChecked = false;

            txtWIDTH1.Text = string.Empty;
            txtWIDTH2.Text = string.Empty;
            txtWIDTH3.Text = string.Empty;
            txtWIDTH4.Text = string.Empty;
            txtDISTANTBAR1.Text = string.Empty;
            txtDISTANTBAR2.Text = string.Empty;
            txtDISTANTBAR3.Text = string.Empty;
            txtDISTANTBAR4.Text = string.Empty;
            txtDISTANTLINE1.Text = string.Empty;
            txtDISTANTLINE2.Text = string.Empty;
            txtDISTANTLINE3.Text = string.Empty;
            txtDENWARP.Text = string.Empty;
            txtDENWEFT.Text = string.Empty;
            txtSPEED.Text = string.Empty;
            txtWIDTHBE.Text = string.Empty;
            txtWIDTHAF.Text = string.Empty;

            txtWIDTHAF_END.Text = string.Empty;

            txtBEGINLINE1.Text = string.Empty;
            txtENDLINE1.Text = string.Empty;
            txtBEGINLINE2.Text = string.Empty;
            txtENDLINE2.Text = string.Empty;
            txtBEGINLINE3.Text = string.Empty;
            txtENDLINE3.Text = string.Empty;
            txtBEGINLINE4.Text = string.Empty;
            txtENDLINE4.Text = string.Empty;
            txtREMARK.Text = string.Empty;

            txtBatchNo1.Text = string.Empty;
            txtBatchNo2.Text = string.Empty;
            txtBatchNo3.Text = string.Empty;
            txtBatchNo4.Text = string.Empty;

            txt2ndBatchNo1.Text = string.Empty;
            txt2ndBatchNo2.Text = string.Empty;
            txt2ndBatchNo3.Text = string.Empty;
            txt2ndBatchNo4.Text = string.Empty;

            txt2ndBEGINLINE1.Text = string.Empty;
            txt2ndBEGINLINE2.Text = string.Empty;
            txt2ndBEGINLINE3.Text = string.Empty;
            txt2ndBEGINLINE4.Text = string.Empty;

            txt2ndENDLINE1.Text = string.Empty;
            txt2ndENDLINE2.Text = string.Empty;
            txt2ndENDLINE3.Text = string.Empty;
            txt2ndENDLINE4.Text = string.Empty;

            txtBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
            txtBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
            txtBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
            txtBEGINLINE4.Visibility = System.Windows.Visibility.Visible;

            txtENDLINE1.Visibility = System.Windows.Visibility.Visible;
            txtENDLINE2.Visibility = System.Windows.Visibility.Visible;
            txtENDLINE3.Visibility = System.Windows.Visibility.Visible;
            txtENDLINE4.Visibility = System.Windows.Visibility.Visible;
    
            txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
            txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
            txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
            txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Visible;

            txt2ndENDLINE1.Visibility = System.Windows.Visibility.Visible;
            txt2ndENDLINE2.Visibility = System.Windows.Visibility.Visible;
            txt2ndENDLINE3.Visibility = System.Windows.Visibility.Visible;
            txt2ndENDLINE4.Visibility = System.Windows.Visibility.Visible;

            BEGINLINE1 = string.Empty;
            BEGINLINE2 = string.Empty;
            BEGINLINE3 = string.Empty;
            BEGINLINE4 = string.Empty;

            ENDLINE1 = string.Empty;
            ENDLINE2 = string.Empty;
            ENDLINE3 = string.Empty;
            ENDLINE4 = string.Empty;

            BEGINLINE12nd = string.Empty;
            BEGINLINE22nd = string.Empty;
            BEGINLINE32nd = string.Empty;
            BEGINLINE42nd = string.Empty;

            ENDLINE12nd = string.Empty;
            ENDLINE22nd = string.Empty;
            ENDLINE32nd = string.Empty;
            ENDLINE42nd = string.Empty;

            imgTrue1.Visibility = System.Windows.Visibility.Collapsed;
            imgTrue2.Visibility = System.Windows.Visibility.Collapsed;
            imgTrue3.Visibility = System.Windows.Visibility.Collapsed;
            imgTrue4.Visibility = System.Windows.Visibility.Collapsed;

            imgTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
            imgTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
            imgTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
            imgTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;

            img2ndTrue1.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrue2.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrue3.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrue4.Visibility = System.Windows.Visibility.Collapsed;

            img2ndTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
            img2ndTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;

            txtWIDTHSpecification.Text = "-";
            txtDISTANTBARSpecification.Text = "-";
            txtDISTANTLINESpecification.Text = "-";
            txtDENWARPSpecification.Text = "-";
            txtDENWeftSpecification.Text = "-";
            txtSPEEDSpecification.Text = "-";
            txtAfterSpecification.Text = "-";

            cbStatusLeft.IsEnabled = true;
            cbStatusRight.IsEnabled = true;
            _session.FINISHINGPROCESS = string.Empty;

            //txtWIDTHSpecification.Text = string.Empty;
            //txtDISTANTBARSpecification.Text = string.Empty;
            //txtDISTANTLINESpecification.Text = string.Empty;
            //txtDENWARPSpecification.Text = string.Empty;
            //txtDENWeftSpecification.Text = string.Empty;
            //txtSPEEDSpecification.Text = string.Empty;
            //txtAfterSpecification.Text = string.Empty;

            cbStatusLeft.Visibility = System.Windows.Visibility.Visible;
            cbStatusRight.Visibility = System.Windows.Visibility.Visible;

            labCutSelvage.Visibility = System.Windows.Visibility.Visible;
            labStatusLeft.Visibility = System.Windows.Visibility.Visible;
            labStatusRight.Visibility = System.Windows.Visibility.Visible;

            // เพิ่ม 13/02/17
            cmID = string.Empty;

             //เพิ่มใหม่ 28/06/17
            txtTension.Text = string.Empty;

            //เพิ่มใหม่ 25/08/17
            txtLengthDetail.Text = string.Empty;
            txtLengthPrint.Text = string.Empty;

            ITEMLOT = string.Empty;
            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;
        }

        #endregion

        #region LoadCUT_GETFINISHINGDATA

        private void LoadCUT_GETFINISHINGDATA(string ITEMLOT)
        {
            try
            {
                List<CUT_GETFINISHINGDATA> items = _session.GetCUT_GETFINISHINGDATA(ITEMLOT);

                if (items != null && items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(items[0].CUSTOMERID))
                    {
                        EnabledCM08(true);

                        cmID = items[0].CUSTOMERID;

                        txtITEMCODE.Text = items[0].ITEMCODE;

                        if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                        {
                            LoadCUT_GETCONDITIONBYITEMCODE(txtITEMCODE.Text);
                        }

                        txtBATCHNO.Text = items[0].BATCHNO;
                        txtFINISHINGLOT.Text = items[0].FINISHINGLOT;

                        if (items[0].CUSTOMERID == "08")
                        {
                            #region Not Use ITEMCODE = 09300
                            //if (items[0].ITEMCODE == "09300")
                            //{
                            //    #region ITEMCODE == "09300"
                            //    txtBatchNo1.Text = items[0].BATCHNO;
                            //    txtBatchNo2.Text = items[0].BATCHNO;
                            //    txtBatchNo3.Text = items[0].BATCHNO;
                            //    txtBatchNo4.Text = items[0].BATCHNO;

                            //    if (!string.IsNullOrEmpty(items[0].SND_BARCODE))
                            //    {
                            //        Enabled2ndBarcode(true);
                            //        txt2ndBatchNo1.Text = items[0].SND_BARCODE;
                            //        txt2ndBatchNo2.Text = items[0].SND_BARCODE;
                            //        txt2ndBatchNo3.Text = items[0].SND_BARCODE;
                            //        txt2ndBatchNo4.Text = items[0].SND_BARCODE;
                            //    }
                            //    else
                            //    {
                            //        Enabled2ndBarcode(false);
                            //    }
                            //    #endregion
                            //}
                            //else
                            //{
                            //    EnabledCM08(false);

                            //    #region CUSTOMERID == "08"

                            //    string batch = items[0].BATCHNO.TrimStart().TrimEnd();
                            //    string batchNo = string.Empty;
                            //    string batchNo2nd = string.Empty;

                            //    txtBATCHNO.Text = batch;

                            //    if (batch.Length > 0)
                            //    {
                            //        if (batch.Length >= 9)
                            //        {
                            //            //batchNo = batch.Substring(0, 5);
                            //            batchNo = "P" + batch.Substring(0, 9);

                            //            txtBatchNo1.Text = batchNo;

                            //            txtBatchNo2.Text = batchNo;
                            //            txtBatchNo3.Text = batchNo;
                            //            txtBatchNo4.Text = batchNo;
                            //        }

                            //        //if (batch.Length >= 10)
                            //        //{
                                        
                            //        //    batchNo2nd = batch.Substring(9, batch.Length - 9);
                                       
                            //        //    if (!string.IsNullOrEmpty(batchNo2nd))
                            //        //    {
                            //        //        Enabled2ndBarcode(true);
                            //        //        txt2ndBatchNo1.Text = batchNo2nd;
                            //        //        txt2ndBatchNo2.Text = batchNo2nd;
                            //        //        txt2ndBatchNo3.Text = batchNo2nd;
                            //        //        txt2ndBatchNo4.Text = batchNo2nd;
                            //        //    }
                            //        //    else
                            //        //    {
                            //        //        Enabled2ndBarcode(false);
                            //        //    }
                            //        //}
                            //        //else
                            //        //{
                            //        //    Enabled2ndBarcode(false);
                            //        //}

                            //        if (!string.IsNullOrEmpty(ITEMLOT))
                            //        {
                            //            batchNo2nd = "H" + ITEMLOT.Substring(0, ITEMLOT.Length - 1);

                            //            if (!string.IsNullOrEmpty(batchNo2nd))
                            //            {
                            //                //Enabled2ndBarcode(true);

                            //                txt2ndBatchNo1.Text = batchNo2nd;

                            //                txt2ndBatchNo2.Text = batchNo2nd;
                            //                txt2ndBatchNo3.Text = batchNo2nd;
                            //                txt2ndBatchNo4.Text = batchNo2nd;
                            //            }
                            //            else
                            //            {
                            //                //Enabled2ndBarcode(false);
                            //            }
                            //        }
                            //    }

                            //    #endregion
                            //}
                            #endregion

                            //EnabledCM08(false);

                            #region CUSTOMERID == "08"

                            string batch = items[0].BATCHNO.TrimStart().TrimEnd();
                            string batchNo = string.Empty;
                            string batchNo2nd = string.Empty;

                            txtBATCHNO.Text = batch;

                            if (batch.Length > 0)
                            {
                                if (batch.Length >= 9)
                                {
                                    //batchNo = batch.Substring(0, 5);
                                    batchNo = "P" + batch.Substring(0, 9);

                                    txtBatchNo1.Text = batchNo;

                                    txtBatchNo2.Text = batchNo;
                                    txtBatchNo3.Text = batchNo;
                                    txtBatchNo4.Text = batchNo;
                                }

                                //if (batch.Length >= 10)
                                //{

                                //    batchNo2nd = batch.Substring(9, batch.Length - 9);

                                //    if (!string.IsNullOrEmpty(batchNo2nd))
                                //    {
                                //        Enabled2ndBarcode(true);
                                //        txt2ndBatchNo1.Text = batchNo2nd;
                                //        txt2ndBatchNo2.Text = batchNo2nd;
                                //        txt2ndBatchNo3.Text = batchNo2nd;
                                //        txt2ndBatchNo4.Text = batchNo2nd;
                                //    }
                                //    else
                                //    {
                                //        Enabled2ndBarcode(false);
                                //    }
                                //}
                                //else
                                //{
                                //    Enabled2ndBarcode(false);
                                //}

                                if (!string.IsNullOrEmpty(ITEMLOT))
                                {
                                    batchNo2nd = "H" + ITEMLOT.Substring(0, ITEMLOT.Length - 1);

                                    if (!string.IsNullOrEmpty(batchNo2nd))
                                    {
                                        //Enabled2ndBarcode(true);

                                        txt2ndBatchNo1.Text = batchNo2nd;

                                        txt2ndBatchNo2.Text = batchNo2nd;
                                        txt2ndBatchNo3.Text = batchNo2nd;
                                        txt2ndBatchNo4.Text = batchNo2nd;
                                    }
                                    else
                                    {
                                        //Enabled2ndBarcode(false);
                                    }
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            txtBatchNo1.Text = items[0].BATCHNO;
                            txtBatchNo2.Text = items[0].BATCHNO;
                            txtBatchNo3.Text = items[0].BATCHNO;
                            txtBatchNo4.Text = items[0].BATCHNO;

                            if (!string.IsNullOrEmpty(items[0].SND_BARCODE))
                            {
                                Enabled2ndBarcode(true);
                                txt2ndBatchNo1.Text = items[0].SND_BARCODE;
                                txt2ndBatchNo2.Text = items[0].SND_BARCODE;
                                txt2ndBatchNo3.Text = items[0].SND_BARCODE;
                                txt2ndBatchNo4.Text = items[0].SND_BARCODE;
                            }
                            else
                            {
                                Enabled2ndBarcode(false);
                            }
                        }

                        #region SND_BARCODE
                        //if (string.IsNullOrEmpty(items[0].SND_BARCODE))
                        //{
                        //    Enabled2ndBarcode(false);
                        //}
                        //else
                        //{
                        //    Enabled2ndBarcode(true);

                        //    txt2ndBatchNo1.Text = items[0].SND_BARCODE;
                        //    txt2ndBatchNo2.Text = items[0].SND_BARCODE;
                        //    txt2ndBatchNo3.Text = items[0].SND_BARCODE;
                        //    txt2ndBatchNo4.Text = items[0].SND_BARCODE;
                        //}
                        #endregion

                        #region PRODUCTTYPEID
                        if (items[0].PRODUCTTYPEID != null)
                        {
                            PRODUCTTYPEID = items[0].PRODUCTTYPEID;
                            if (items[0].PRODUCTTYPEID == "1")
                            {
                                rbMass.IsChecked = true;
                                rbTest.IsChecked = false;
                            }
                            else if (items[0].PRODUCTTYPEID == "2")
                            {
                                rbMass.IsChecked = false;
                                rbTest.IsChecked = true;
                            }
                        }
                        else
                            PRODUCTTYPEID = "1";

                        #endregion

                        if (items[0].BEFORE_WIDTH != null)
                            txtWIDTHBE.Text = items[0].BEFORE_WIDTH.Value.ToString("#,##0.##");

                        _session.FINISHINGPROCESS = items[0].FINISHINGPROCESS;

                        if (items[0].FINISHINGPROCESS == "Scouring")
                        {
                            cbStatusLeft.SelectedValue = null;
                            cbStatusRight.SelectedValue = null;

                            cbStatusLeft.IsEnabled = false;
                            cbStatusRight.IsEnabled = false;
                        }
                        else
                        {
                            cbStatusLeft.SelectedValue = "OK";
                            cbStatusRight.SelectedValue = "OK";

                            cbStatusLeft.IsEnabled = true;
                            cbStatusRight.IsEnabled = true;
                        }

                        cmdStart.IsEnabled = true;
                    }
                    else
                    {
                        #region Msg This Customer ID is null Can not be used
                        string msg = "This Customer ID is null Can not be used";

                        msg.ShowMessageBox(false);
                        ClearCUT_GETFINISHINGDATA();

                        cmdStart.IsEnabled = false;
                        cmdPreview.IsEnabled = false;

                        cbStatusLeft.SelectedValue = "OK";
                        cbStatusRight.SelectedValue = "OK";

                        cbStatusLeft.IsEnabled = true;
                        cbStatusRight.IsEnabled = true;
                        _session.FINISHINGPROCESS = string.Empty;

                        // เพิ่ม 13/02/17
                        cmID = string.Empty;

                        txtITEMLOT.Focus();
                        txtITEMLOT.SelectAll();
                        #endregion
                    }
                }
                else
                {
                    #region Msg Can not be used
                    string msg = "This Lot No. " + ITEMLOT + " Can not be used";

                    msg.ShowMessageBox(false);
                    ClearCUT_GETFINISHINGDATA();

                    cmdStart.IsEnabled = false;
                    cmdPreview.IsEnabled = false;

                    cbStatusLeft.SelectedValue = "OK";
                    cbStatusRight.SelectedValue = "OK";

                    cbStatusLeft.IsEnabled = true;
                    cbStatusRight.IsEnabled = true;
                    _session.FINISHINGPROCESS = string.Empty;

                    // เพิ่ม 13/02/17
                    cmID = string.Empty;

                    txtITEMLOT.Focus();
                    txtITEMLOT.SelectAll();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                // เพิ่ม 13/02/17
                cmID = string.Empty;

                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadCUT_GETCONDITIONBYITEMCODE

        private void LoadCUT_GETCONDITIONBYITEMCODE(string P_ITMCODE)
        {
            try
            {
                List<CUT_GETCONDITIONBYITEMCODE> items = _session.GetCUT_GETCONDITIONBYITEMCODE(P_ITMCODE);

                if (items != null && items.Count > 0)
                {
                    txtWIDTHSpecification.Text = items[0].strWIDTHBARCODE;
                    txtDISTANTBARSpecification.Text = items[0].strDISTANTBARCODE;
                    txtDISTANTLINESpecification.Text = items[0].strDISTANTLINE;
                    txtDENWARPSpecification.Text = items[0].strDENSITYWARP;
                    txtDENWeftSpecification.Text = items[0].strDENSITYWEFT;
                    txtSPEEDSpecification.Text = items[0].strSPEED;
                    txtAfterSpecification.Text = items[0].strAFTER;

                    if (items[0].SHOWSELVAGE == "N")
                    {
                        cbStatusLeft.SelectedValue = null;
                        cbStatusRight.SelectedValue = null;

                        cbStatusLeft.IsEnabled = false;
                        cbStatusRight.IsEnabled = false;

                        cbStatusLeft.Visibility = System.Windows.Visibility.Collapsed;
                        cbStatusRight.Visibility = System.Windows.Visibility.Collapsed;

                        labCutSelvage.Visibility = System.Windows.Visibility.Collapsed;
                        labStatusLeft.Visibility = System.Windows.Visibility.Collapsed;
                        labStatusRight.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    else
                    {
                        cbStatusLeft.SelectedValue = "OK";
                        cbStatusRight.SelectedValue = "OK";

                        cbStatusLeft.IsEnabled = true;
                        cbStatusRight.IsEnabled = true;

                        cbStatusLeft.Visibility = System.Windows.Visibility.Visible;
                        cbStatusRight.Visibility = System.Windows.Visibility.Visible;

                        labCutSelvage.Visibility = System.Windows.Visibility.Visible;
                        labStatusLeft.Visibility = System.Windows.Visibility.Visible;
                        labStatusRight.Visibility = System.Windows.Visibility.Visible;
                    }

                }
                else
                {
                    string msg = "ไม่พบ Specification สำหรับ Item " + P_ITMCODE ;

                    msg.ShowMessageBox(false);

                    txtWIDTHSpecification.Text = "-";
                    txtDISTANTBARSpecification.Text = "-";
                    txtDISTANTLINESpecification.Text = "-";
                    txtDENWARPSpecification.Text = "-";
                    txtDENWeftSpecification.Text = "-";
                    txtSPEEDSpecification.Text = "-";
                    txtAfterSpecification.Text = "-";

                    if (cbStatusLeft.Visibility == System.Windows.Visibility.Collapsed)
                    {
                        cbStatusLeft.Visibility = System.Windows.Visibility.Visible;
                        cbStatusRight.Visibility = System.Windows.Visibility.Visible;

                        labCutSelvage.Visibility = System.Windows.Visibility.Visible;
                        labStatusLeft.Visibility = System.Windows.Visibility.Visible;
                        labStatusRight.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);

                if (cbStatusLeft.Visibility == System.Windows.Visibility.Collapsed)
                {
                    cbStatusLeft.Visibility = System.Windows.Visibility.Visible;
                    cbStatusRight.Visibility = System.Windows.Visibility.Visible;

                    labCutSelvage.Visibility = System.Windows.Visibility.Visible;
                    labStatusLeft.Visibility = System.Windows.Visibility.Visible;
                    labStatusRight.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        #endregion

        #region CUT_INSERTDATA

        private void CUT_INSERTDATA()
        {
            try
            {
                string FINISHLOT = txtFINISHINGLOT.Text;
                string ITEMLOT = txtITEMLOT.Text;
                string operatorid = txtOperator.Text;
                decimal? WIDTH1 = null;
                decimal? WIDTH2 = null;
                decimal? WIDTH3 = null;
                decimal? WIDTH4 = null;
                decimal? DISTANTBAR1 = null;
                decimal? DISTANTBAR2 = null;
                decimal? DISTANTBAR3 = null;
                decimal? DISTANTBAR4 = null;
                decimal? DISTANTLINE1 = null;
                decimal? DISTANTLINE2 = null;
                decimal? DISTANTLINE3 = null;
                decimal? DENWARP = null;
                decimal? DENWEFT = null;
                decimal? SPEED = null;
                decimal? WIDTHBE = null;
                decimal? WIDTHAF = null;
                decimal? Tension = null;

                //เพิ่ม 25/08/17
                string LENGTHDETAIL = string.Empty;
                decimal? LENGTHPRINT = null;

                //เพิ่มใหม่ 04/10/17
                decimal? WIDTHAF_END = null;

                if (ITEMLOT != "" && operatorid != "")
                {
                    _session.FINISHINGLOT = FINISHLOT;
                    _session.Operator = operatorid;
                    _session.ITEMLOT = ITEMLOT;
                    _session.STARTDATE = startDate;

                    if (rbMass.IsChecked == true && rbTest.IsChecked == false)
                        PRODUCTTYPEID = "1";
                    else
                        PRODUCTTYPEID = "2";

                    _session.PRODUCTTYPEID = PRODUCTTYPEID;

                    _session.REMARK = txtREMARK.Text;
                    _session.MCNO = txtPrintNo.Text;

                    
                    if (txtWIDTH1.Text != "")
                        WIDTH1 = decimal.Parse(txtWIDTH1.Text);

                    if (txtWIDTH2.Text != "")
                        WIDTH2 = decimal.Parse(txtWIDTH2.Text);

                    if (txtWIDTH3.Text != "")
                        WIDTH3 = decimal.Parse(txtWIDTH3.Text);

                    if (txtWIDTH4.Text != "")
                        WIDTH4 = decimal.Parse(txtWIDTH4.Text);

                    if (txtDISTANTBAR1.Text != "")
                        DISTANTBAR1 = decimal.Parse(txtDISTANTBAR1.Text);

                    if (txtDISTANTBAR2.Text != "")
                        DISTANTBAR2 = decimal.Parse(txtDISTANTBAR2.Text);

                    if (txtDISTANTBAR3.Text != "")
                        DISTANTBAR3 = decimal.Parse(txtDISTANTBAR3.Text);

                    if (txtDISTANTBAR4.Text != "")
                        DISTANTBAR4 = decimal.Parse(txtDISTANTBAR4.Text);

                    if (txtDISTANTLINE1.Text != "")
                        DISTANTLINE1 = decimal.Parse(txtDISTANTLINE1.Text);

                    if (txtDISTANTLINE2.Text != "")
                        DISTANTLINE2 = decimal.Parse(txtDISTANTLINE2.Text);

                    if (txtDISTANTLINE3.Text != "")
                        DISTANTLINE3 = decimal.Parse(txtDISTANTLINE3.Text);

                    if (txtDENWARP.Text != "")
                        DENWARP = decimal.Parse(txtDENWARP.Text);

                    if (txtDENWEFT.Text != "")
                        DENWEFT = decimal.Parse(txtDENWEFT.Text);

                    if (txtSPEED.Text != "")
                        SPEED = decimal.Parse(txtSPEED.Text);

                    if (txtWIDTHBE.Text != "")
                        WIDTHBE = decimal.Parse(txtWIDTHBE.Text);

                    if (txtWIDTHAF.Text != "")
                        WIDTHAF = decimal.Parse(txtWIDTHAF.Text);

                    //เพิ่มใหม่ 04/10/17
                    if (txtWIDTHAF_END.Text != "")
                        WIDTHAF_END = decimal.Parse(txtWIDTHAF_END.Text);

                    _session.WIDTH1 = WIDTH1;
                    _session.WIDTH2 = WIDTH2;
                    _session.WIDTH3 = WIDTH3;
                    _session.WIDTH4 = WIDTH4;
                    _session.DISTANTBAR1 = DISTANTBAR1;
                    _session.DISTANTBAR2 = DISTANTBAR2;
                    _session.DISTANTBAR3 = DISTANTBAR3;
                    _session.DISTANTBAR4 = DISTANTBAR4;
                    _session.DISTANTLINE1 = DISTANTLINE1;
                    _session.DISTANTLINE2 = DISTANTLINE2;
                    _session.DISTANTLINE3 = DISTANTLINE3;
                    _session.DENWARP = DENWARP;
                    _session.DENWEFT = DENWEFT;
                    _session.SPEED = SPEED;
                    _session.WIDTHBE = WIDTHBE;
                    _session.WIDTHAF = WIDTHAF;

                    //เพิ่มใหม่ 04/10/17
                    _session.WIDTHAF_END = WIDTHAF_END;

                    _session.BEGINLINE1 = BEGINLINE1;
                    _session.BEGINLINE2 = BEGINLINE2;
                    _session.BEGINLINE3 = BEGINLINE3;
                    _session.BEGINLINE4 = BEGINLINE4;
                    _session.ENDLINE1 = ENDLINE1;
                    _session.ENDLINE2 = ENDLINE2;
                    _session.ENDLINE3 = ENDLINE3;
                    _session.ENDLINE4 = ENDLINE4;

                    _session.P_2BEGINLINE1 = BEGINLINE12nd;
                    _session.P_2BEGINLINE2 = BEGINLINE22nd;
                    _session.P_2BEGINLINE3 = BEGINLINE32nd;
                    _session.P_2BEGINLINE4 = BEGINLINE42nd;
                    _session.P_2ENDLINE1 = ENDLINE12nd;
                    _session.P_2ENDLINE2 = ENDLINE22nd;
                    _session.P_2ENDLINE3 = ENDLINE32nd;
                    _session.P_2ENDLINE4 = ENDLINE42nd;

                    if (cbStatusLeft.SelectedValue != null)
                    {
                        _session.SELVAGELEFT = cbStatusLeft.SelectedValue.ToString();
                    }
                    if (cbStatusRight.SelectedValue != null)
                    {
                        _session.SELVAGERIGHT = cbStatusRight.SelectedValue.ToString();
                    }

                    if (chkGetData == true)
                    {
                        _session.SUSPENDDATE = DateTime.Now;
                    }

                    //เพิ่มใหม่ 28/06/17
                    if (txtTension.Text != "")
                        Tension = decimal.Parse(txtTension.Text);

                    _session.P_TENSION = Tension;

                    if (!string.IsNullOrEmpty(txtLengthDetail.Text))
                    {
                        LENGTHDETAIL = txtLengthDetail.Text;
                    }

                    _session.LENGTHDETAIL = LENGTHDETAIL;

                    if (!string.IsNullOrEmpty(txtLengthPrint.Text))
                    {
                        LENGTHPRINT = decimal.Parse(txtLengthPrint.Text);
                    }

                    _session.LENGTHPRINT = LENGTHPRINT;

                    if (_session.CUT_INSERTDATA() == true)
                    {
                        cmdStart.IsEnabled = false;
                        cmdPreview.IsEnabled = false;
                        cmdEnd.IsEnabled = true;
                        cmdBack.IsEnabled = false;

                        cmdClear.IsEnabled = true;
                        cmdSuspend.IsEnabled = true;
                    }
                    else
                    {
                        "Can't Insert Cut & Print Process".ShowMessageBox(true);
                    }
                }
                else
                {
                    "Lot no. is not null".ShowMessageBox(true);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region CUT_UPDATEDATA

        private void CUT_UPDATEDATA()
        {
            try
            {
                string ITEMLOT = txtITEMLOT.Text;
              
                decimal? WIDTH1 = null;
                decimal? WIDTH2 = null;
                decimal? WIDTH3 = null;
                decimal? WIDTH4 = null;
                decimal? DISTANTBAR1 = null;
                decimal? DISTANTBAR2 = null;
                decimal? DISTANTBAR3 = null;
                decimal? DISTANTBAR4 = null;
                decimal? DISTANTLINE1 = null;
                decimal? DISTANTLINE2 = null;
                decimal? DISTANTLINE3 = null;
                decimal? DENWARP = null;
                decimal? DENWEFT = null;
                decimal? SPEED = null;
                decimal? WIDTHBE = null;
                decimal? WIDTHAF = null;
                decimal? LENGTHPRINT = null;
                decimal? Tension = null;

                //เพิ่ม 25/08/17
                string LENGTHDETAIL = string.Empty;

                //เพิ่มใหม่ 04/10/17
                decimal? WIDTHAF_END = null;

                if (ITEMLOT != "" )
                {
                    _session.ITEMLOT = ITEMLOT;
                    _session.ENDDATE = endDate;
                    _session.REMARK = txtREMARK.Text;


                    if (txtWIDTH1.Text != "")
                        WIDTH1 = decimal.Parse(txtWIDTH1.Text);

                    if (txtWIDTH2.Text != "")
                        WIDTH2 = decimal.Parse(txtWIDTH2.Text);

                    if (txtWIDTH3.Text != "")
                        WIDTH3 = decimal.Parse(txtWIDTH3.Text);

                    if (txtWIDTH4.Text != "")
                        WIDTH4 = decimal.Parse(txtWIDTH4.Text);

                    if (txtDISTANTBAR1.Text != "")
                        DISTANTBAR1 = decimal.Parse(txtDISTANTBAR1.Text);

                    if (txtDISTANTBAR2.Text != "")
                        DISTANTBAR2 = decimal.Parse(txtDISTANTBAR2.Text);

                    if (txtDISTANTBAR3.Text != "")
                        DISTANTBAR3 = decimal.Parse(txtDISTANTBAR3.Text);

                    if (txtDISTANTBAR4.Text != "")
                        DISTANTBAR4 = decimal.Parse(txtDISTANTBAR4.Text);

                    if (txtDISTANTLINE1.Text != "")
                        DISTANTLINE1 = decimal.Parse(txtDISTANTLINE1.Text);

                    if (txtDISTANTLINE2.Text != "")
                        DISTANTLINE2 = decimal.Parse(txtDISTANTLINE2.Text);

                    if (txtDISTANTLINE3.Text != "")
                        DISTANTLINE3 = decimal.Parse(txtDISTANTLINE3.Text);

                    if (txtDENWARP.Text != "")
                        DENWARP = decimal.Parse(txtDENWARP.Text);

                    if (txtDENWEFT.Text != "")
                        DENWEFT = decimal.Parse(txtDENWEFT.Text);

                    if (txtSPEED.Text != "")
                        SPEED = decimal.Parse(txtSPEED.Text);

                    if (txtWIDTHBE.Text != "")
                        WIDTHBE = decimal.Parse(txtWIDTHBE.Text);

                    if (txtWIDTHAF.Text != "")
                        WIDTHAF = decimal.Parse(txtWIDTHAF.Text);

                    //เพิ่มใหม่ 04/10/17
                    if (txtWIDTHAF_END.Text != "")
                        WIDTHAF_END = decimal.Parse(txtWIDTHAF_END.Text);

                    _session.WIDTH1 = WIDTH1;
                    _session.WIDTH2 = WIDTH2;
                    _session.WIDTH3 = WIDTH3;
                    _session.WIDTH4 = WIDTH4;
                    _session.DISTANTBAR1 = DISTANTBAR1;
                    _session.DISTANTBAR2 = DISTANTBAR2;
                    _session.DISTANTBAR3 = DISTANTBAR3;
                    _session.DISTANTBAR4 = DISTANTBAR4;
                    _session.DISTANTLINE1 = DISTANTLINE1;
                    _session.DISTANTLINE2 = DISTANTLINE2;
                    _session.DISTANTLINE3 = DISTANTLINE3;
                    _session.DENWARP = DENWARP;
                    _session.DENWEFT = DENWEFT;
                    _session.SPEED = SPEED;
                    _session.WIDTHBE = WIDTHBE;
                    _session.WIDTHAF = WIDTHAF;

                    //เพิ่มใหม่ 04/10/17
                    _session.WIDTHAF_END = WIDTHAF_END;

                    if (txtBEGINLINE1.Text != "")
                    _session.BEGINLINE1 = txtBEGINLINE1.Text;

                    if (txtBEGINLINE2.Text != "")
                    _session.BEGINLINE2 = txtBEGINLINE2.Text;

                    if (txtBEGINLINE3.Text != "")
                    _session.BEGINLINE3 = txtBEGINLINE3.Text;

                    if (txtBEGINLINE4.Text != "")
                    _session.BEGINLINE4 = txtBEGINLINE4.Text;

                    if (txtENDLINE1.Text != "")
                    _session.ENDLINE1 = txtENDLINE1.Text;

                    if (txtENDLINE2.Text != "")
                    _session.ENDLINE2 = txtENDLINE2.Text;

                    if (txtENDLINE3.Text != "")
                    _session.ENDLINE3 = txtENDLINE3.Text;

                    if (txtENDLINE4.Text != "")
                    _session.ENDLINE4 = txtENDLINE4.Text;

                    //เพิ่มใหม่ 27/06/16
                    if (txt2ndBEGINLINE1.Text != "")
                        _session.P_2BEGINLINE1 = txt2ndBEGINLINE1.Text;

                    if (txt2ndBEGINLINE2.Text != "")
                        _session.P_2BEGINLINE2 = txt2ndBEGINLINE2.Text;

                    if (txt2ndBEGINLINE3.Text != "")
                        _session.P_2BEGINLINE3 = txt2ndBEGINLINE3.Text;

                    if (txt2ndBEGINLINE4.Text != "")
                        _session.P_2BEGINLINE4 = txt2ndBEGINLINE4.Text;

                    if (txt2ndENDLINE1.Text != "")
                        _session.P_2ENDLINE1 = txt2ndENDLINE1.Text;

                    if (txt2ndENDLINE2.Text != "")
                        _session.P_2ENDLINE2 = txt2ndENDLINE2.Text;

                    if (txt2ndENDLINE3.Text != "")
                        _session.P_2ENDLINE3 = txt2ndENDLINE3.Text;

                    if (txt2ndENDLINE4.Text != "")
                        _session.P_2ENDLINE4 = txt2ndENDLINE4.Text;
                    //---------------------------------------------------------------------//

                    if (cbStatusLeft.SelectedValue != null)
                    {
                        _session.SELVAGELEFT = cbStatusLeft.SelectedValue.ToString();
                    }
                    if (cbStatusRight.SelectedValue != null)
                    {
                        _session.SELVAGERIGHT = cbStatusRight.SelectedValue.ToString();
                    }

                    _session.STATUS = "F";

                    if (!string.IsNullOrEmpty(txtLengthDetail.Text))
                    {
                        LENGTHDETAIL = txtLengthDetail.Text;
                    }

                    _session.LENGTHDETAIL = LENGTHDETAIL;

                    if (!string.IsNullOrEmpty(txtLengthPrint.Text))
                    {
                        LENGTHPRINT = decimal.Parse(txtLengthPrint.Text);
                    }

                    _session.LENGTHPRINT = LENGTHPRINT;

                    //เพิ่มใหม่ 28/06/17
                    if (!string.IsNullOrEmpty(txtTension.Text))
                        Tension = decimal.Parse(txtTension.Text);

                    _session.P_TENSION = Tension;

                    if (_session.CUT_UPDATEDATA() == true)
                    {
                        cmdStart.IsEnabled = true;
                        cmdPreview.IsEnabled = true;
                        cmdEnd.IsEnabled = false;
                        cmdBack.IsEnabled = true;
                    }
                    else
                    {
                        "Can't Update Cut & Print Process".ShowMessageBox(true);
                    }
                }
                else
                {
                    "Lot no. is not null".ShowMessageBox(true);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region Suspend
        private void Suspend()
        {
            try
            {
                string ITEMLOT = txtITEMLOT.Text;

                decimal? WIDTH1 = null;
                decimal? WIDTH2 = null;
                decimal? WIDTH3 = null;
                decimal? WIDTH4 = null;
                decimal? DISTANTBAR1 = null;
                decimal? DISTANTBAR2 = null;
                decimal? DISTANTBAR3 = null;
                decimal? DISTANTBAR4 = null;
                decimal? DISTANTLINE1 = null;
                decimal? DISTANTLINE2 = null;
                decimal? DISTANTLINE3 = null;
                decimal? DENWARP = null;
                decimal? DENWEFT = null;
                decimal? SPEED = null;
                decimal? WIDTHBE = null;
                decimal? WIDTHAF = null;
                decimal? LENGTHPRINT = null;
                decimal? Tension = null;

                //เพิ่ม 25/08/17
                string LENGTHDETAIL = string.Empty;


                //เพิ่มใหม่ 04/10/17
                decimal? WIDTHAF_END = null;

                if (ITEMLOT != "")
                {
                    _session.ITEMLOT = ITEMLOT;
                    _session.ENDDATE = endDate;
                    _session.REMARK = txtREMARK.Text;


                    if (txtWIDTH1.Text != "")
                        WIDTH1 = decimal.Parse(txtWIDTH1.Text);

                    if (txtWIDTH2.Text != "")
                        WIDTH2 = decimal.Parse(txtWIDTH2.Text);

                    if (txtWIDTH3.Text != "")
                        WIDTH3 = decimal.Parse(txtWIDTH3.Text);

                    if (txtWIDTH4.Text != "")
                        WIDTH4 = decimal.Parse(txtWIDTH4.Text);

                    if (txtDISTANTBAR1.Text != "")
                        DISTANTBAR1 = decimal.Parse(txtDISTANTBAR1.Text);

                    if (txtDISTANTBAR2.Text != "")
                        DISTANTBAR2 = decimal.Parse(txtDISTANTBAR2.Text);

                    if (txtDISTANTBAR3.Text != "")
                        DISTANTBAR3 = decimal.Parse(txtDISTANTBAR3.Text);

                    if (txtDISTANTBAR4.Text != "")
                        DISTANTBAR4 = decimal.Parse(txtDISTANTBAR4.Text);

                    if (txtDISTANTLINE1.Text != "")
                        DISTANTLINE1 = decimal.Parse(txtDISTANTLINE1.Text);

                    if (txtDISTANTLINE2.Text != "")
                        DISTANTLINE2 = decimal.Parse(txtDISTANTLINE2.Text);

                    if (txtDISTANTLINE3.Text != "")
                        DISTANTLINE3 = decimal.Parse(txtDISTANTLINE3.Text);

                    if (txtDENWARP.Text != "")
                        DENWARP = decimal.Parse(txtDENWARP.Text);

                    if (txtDENWEFT.Text != "")
                        DENWEFT = decimal.Parse(txtDENWEFT.Text);

                    if (txtSPEED.Text != "")
                        SPEED = decimal.Parse(txtSPEED.Text);

                    if (txtWIDTHBE.Text != "")
                        WIDTHBE = decimal.Parse(txtWIDTHBE.Text);

                    if (txtWIDTHAF.Text != "")
                        WIDTHAF = decimal.Parse(txtWIDTHAF.Text);

                    //เพิ่มใหม่ 04/10/17
                    if (txtWIDTHAF_END.Text != "")
                        WIDTHAF_END = decimal.Parse(txtWIDTHAF_END.Text);

                    _session.WIDTH1 = WIDTH1;
                    _session.WIDTH2 = WIDTH2;
                    _session.WIDTH3 = WIDTH3;
                    _session.WIDTH4 = WIDTH4;
                    _session.DISTANTBAR1 = DISTANTBAR1;
                    _session.DISTANTBAR2 = DISTANTBAR2;
                    _session.DISTANTBAR3 = DISTANTBAR3;
                    _session.DISTANTBAR4 = DISTANTBAR4;
                    _session.DISTANTLINE1 = DISTANTLINE1;
                    _session.DISTANTLINE2 = DISTANTLINE2;
                    _session.DISTANTLINE3 = DISTANTLINE3;
                    _session.DENWARP = DENWARP;
                    _session.DENWEFT = DENWEFT;
                    _session.SPEED = SPEED;
                    _session.WIDTHBE = WIDTHBE;
                    _session.WIDTHAF = WIDTHAF;

                    //เพิ่มใหม่ 04/10/17
                    _session.WIDTHAF_END = WIDTHAF_END;

                    if (txtBEGINLINE1.Text != "")
                        _session.BEGINLINE1 = txtBEGINLINE1.Text;

                    if (txtBEGINLINE2.Text != "")
                        _session.BEGINLINE2 = txtBEGINLINE2.Text;

                    if (txtBEGINLINE3.Text != "")
                        _session.BEGINLINE3 = txtBEGINLINE3.Text;

                    if (txtBEGINLINE4.Text != "")
                        _session.BEGINLINE4 = txtBEGINLINE4.Text;

                    if (txtENDLINE1.Text != "")
                        _session.ENDLINE1 = txtENDLINE1.Text;

                    if (txtENDLINE2.Text != "")
                        _session.ENDLINE2 = txtENDLINE2.Text;

                    if (txtENDLINE3.Text != "")
                        _session.ENDLINE3 = txtENDLINE3.Text;

                    if (txtENDLINE4.Text != "")
                        _session.ENDLINE4 = txtENDLINE4.Text;

                    //เพิ่มใหม่ 27/06/16
                    if (txt2ndBEGINLINE1.Text != "")
                        _session.P_2BEGINLINE1 = txt2ndBEGINLINE1.Text;

                    if (txt2ndBEGINLINE2.Text != "")
                        _session.P_2BEGINLINE2 = txt2ndBEGINLINE2.Text;

                    if (txt2ndBEGINLINE3.Text != "")
                        _session.P_2BEGINLINE3 = txt2ndBEGINLINE3.Text;

                    if (txt2ndBEGINLINE4.Text != "")
                        _session.P_2BEGINLINE4 = txt2ndBEGINLINE4.Text;

                    if (txt2ndENDLINE1.Text != "")
                        _session.P_2ENDLINE1 = txt2ndENDLINE1.Text;

                    if (txt2ndENDLINE2.Text != "")
                        _session.P_2ENDLINE2 = txt2ndENDLINE2.Text;

                    if (txt2ndENDLINE3.Text != "")
                        _session.P_2ENDLINE3 = txt2ndENDLINE3.Text;

                    if (txt2ndENDLINE4.Text != "")
                        _session.P_2ENDLINE4 = txt2ndENDLINE4.Text;
                    //---------------------------------------------------------------------//

                    if (cbStatusLeft.SelectedValue != null)
                    {
                        _session.SELVAGELEFT = cbStatusLeft.SelectedValue.ToString();
                    }
                    if (cbStatusRight.SelectedValue != null)
                    {
                        _session.SELVAGERIGHT = cbStatusRight.SelectedValue.ToString();
                    }

                    _session.STATUS = "S";
                    _session.CLEARBY = null;
                    _session.CLEARREMARK = null;
                    _session.CLEARDATE = null;
                    _session.SUSPENDDATE = DateTime.Now;

                    if (!string.IsNullOrEmpty(txtLengthDetail.Text))
                    {
                        LENGTHDETAIL = txtLengthDetail.Text;
                    }

                    _session.LENGTHDETAIL = LENGTHDETAIL;

                    if (!string.IsNullOrEmpty(txtLengthPrint.Text))
                    {
                        LENGTHPRINT = decimal.Parse(txtLengthPrint.Text);
                    }

                    _session.LENGTHPRINT = LENGTHPRINT;

                    //เพิ่มใหม่ 28/06/17
                    if (!string.IsNullOrEmpty(txtTension.Text))
                        Tension = decimal.Parse(txtTension.Text);

                    _session.P_TENSION = Tension;

                    if (_session.Suspend() == true)
                    {
                        PageManager.Instance.Back();
                    }
                    else
                    {
                        "Can't Update Cut & Print Process".ShowMessageBox(true);
                    }
                }
                else
                {
                    "Lot no. is not null".ShowMessageBox(true);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region GetAuthorizeByProcessID
        private void GetAuthorizeByProcessID(string PROCESSID, string USER, string PASS, string Remark)
        {
            try
            {
                string ITEMLOT = txtITEMLOT.Text;

                if (ITEMLOT != "")
                {
                    if (_session.GetAuthorizeByProcessID(PROCESSID, USER, PASS) == true)
                    {

                        _session.ITEMLOT = ITEMLOT;
                        _session.STATUS = "C";
                        _session.CLEARBY = USER;
                        _session.CLEARREMARK = Remark;
                        _session.CLEARDATE = DateTime.Now;

                        if (_session.ClearCut() == true)
                        {
                            PageManager.Instance.Back();
                        }
                        else
                        {
                            "Can't Update Cut & Print Process".ShowMessageBox(true);
                        }
                    }
                    else
                    {
                        "Can't Get Authorize By ProcessID".ShowMessageBox(true);
                    }
                }
                else
                {
                    "Lot no. is not null".ShowMessageBox(true);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Cut_GetMCSuspendData
        private List<CUT_GETMCSUSPENDDATA> Cut_GetMCSuspendData()
        {
            List<CUT_GETMCSUSPENDDATA> GetMC = null;
            _session.MCNO = txtPrintNo.Text;

            try
            {
                List<CUT_GETMCSUSPENDDATA> items = _session.Cut_GetMCSuspendData();

                if (items != null && items.Count > 0)
                {
                    chkGetData = true;

                    // เพิ่ม 13/02/17
                    cmID = string.Empty;

                    txtITEMLOT.Text = items[0].ITEMLOT;
                    LoadCUT_GETFINISHINGDATA(txtITEMLOT.Text);

                    if (items[0].STARTDATE != null)
                        txtStartTime.Text = items[0].STARTDATE.Value.ToString("dd/MM/yy HH:mm");

                    if (items[0].ENDDATE != null)
                        txtEndTime.Text = items[0].ENDDATE.Value.ToString("dd/MM/yy HH:mm");

                    txtREMARK.Text = items[0].REMARK;

                    if (items[0].AFTER_WIDTH != null)
                        txtWIDTHAF.Text = items[0].AFTER_WIDTH.Value.ToString("#,##0.##");

                    if (items[0].BEFORE_WIDTH != null)
                        txtWIDTHBE.Text = items[0].BEFORE_WIDTH.Value.ToString("#,##0.##");

                    //เพิ่มใหม่ 04/10/17
                    if (items[0].AFTER_WIDTH_END != null)
                        txtWIDTHAF_END.Text = items[0].AFTER_WIDTH_END.Value.ToString("#,##0.##");

                    #region BEGINROLL_LINE1

                    if (items[0].BEGINROLL_LINE1 != null)
                    {
                        txtBEGINLINE1.Text = items[0].BEGINROLL_LINE1;

                        if (txtBatchNo1.Text == txtBEGINLINE1.Text)
                        {
                            imgTrue1.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE1 = "Yes";
                        }
                        else
                        {
                            imgTrue1.Visibility = System.Windows.Visibility.Collapsed;
                            txtBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE1 = "";
                        }
                    }
                    else
                    {
                        imgTrue1.Visibility = System.Windows.Visibility.Collapsed;
                        txtBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                        BEGINLINE1 = "";
                    }

                    #endregion

                    #region BEGINROLL_LINE2

                    if (items[0].BEGINROLL_LINE2 != null)
                    {
                        txtBEGINLINE2.Text = items[0].BEGINROLL_LINE2;

                        if (txtBatchNo2.Text == txtBEGINLINE2.Text)
                        {
                            imgTrue2.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE2 = "Yes";
                        }
                        else
                        {
                            imgTrue2.Visibility = System.Windows.Visibility.Collapsed;
                            txtBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE2 = "";
                        }
                    }
                    else
                    {
                        imgTrue2.Visibility = System.Windows.Visibility.Collapsed;
                        txtBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                        BEGINLINE2 = "";
                    }

                    #endregion

                    #region BEGINROLL_LINE3

                    if (items[0].BEGINROLL_LINE3 != null)
                    {
                        txtBEGINLINE3.Text = items[0].BEGINROLL_LINE3;

                        if (txtBatchNo3.Text == txtBEGINLINE3.Text)
                        {
                            imgTrue3.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE3 = "Yes";
                        }
                        else
                        {
                            imgTrue3.Visibility = System.Windows.Visibility.Collapsed;
                            txtBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE3 = "";
                        }
                    }
                    else
                    {
                        imgTrue3.Visibility = System.Windows.Visibility.Collapsed;
                        txtBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                        BEGINLINE3 = "";
                    }

                    #endregion

                    #region BEGINROLL_LINE4

                    if (items[0].BEGINROLL_LINE4 != null)
                    {
                        txtBEGINLINE4.Text = items[0].BEGINROLL_LINE4;

                        if (txtBatchNo4.Text == txtBEGINLINE4.Text)
                        {
                            imgTrue4.Visibility = System.Windows.Visibility.Visible;
                            txtBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE4 = "Yes";
                        }
                        else
                        {
                            imgTrue4.Visibility = System.Windows.Visibility.Collapsed;
                            txtBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE4 = "";
                        }
                    }
                    else
                    {
                        imgTrue4.Visibility = System.Windows.Visibility.Collapsed;
                        txtBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                        BEGINLINE4 = "";
                    }

                    #endregion

                    #region BEGINROLL2_LINE1

                    if (items[0].BEGINROLL2_LINE1 != null)
                    {
                        txt2ndBEGINLINE1.Text = items[0].BEGINROLL2_LINE1;

                        if (txtBatchNo1.Text == txt2ndBEGINLINE1.Text)
                        {
                            img2ndTrue1.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE12nd = "Yes";
                        }
                        else
                        {
                            img2ndTrue1.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE12nd = "";
                        }
                    }
                    else
                    {
                        img2ndTrue1.Visibility = System.Windows.Visibility.Collapsed;
                        txt2ndBEGINLINE1.Visibility = System.Windows.Visibility.Visible;
                        BEGINLINE12nd = "";
                    }

                    #endregion

                    #region BEGINROLL2_LINE2

                    if (items[0].BEGINROLL2_LINE2 != null)
                    {
                        txt2ndBEGINLINE2.Text = items[0].BEGINROLL2_LINE2;

                        if (txtBatchNo2.Text == txt2ndBEGINLINE2.Text)
                        {
                            img2ndTrue2.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE22nd = "Yes";
                        }
                        else
                        {
                            img2ndTrue2.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE22nd = "";
                        }
                    }
                    else
                    {
                        img2ndTrue2.Visibility = System.Windows.Visibility.Collapsed;
                        txt2ndBEGINLINE2.Visibility = System.Windows.Visibility.Visible;
                        BEGINLINE22nd = "";
                    }

                    #endregion

                    #region BEGINROLL2_LINE3

                    if (items[0].BEGINROLL2_LINE3 != null)
                    {
                        txt2ndBEGINLINE3.Text = items[0].BEGINROLL2_LINE3;

                        if (txtBatchNo3.Text == txt2ndBEGINLINE3.Text)
                        {
                            img2ndTrue3.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE32nd = "Yes";
                        }
                        else
                        {
                            img2ndTrue3.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE32nd = "";
                        }
                    }
                    else
                    {
                        img2ndTrue3.Visibility = System.Windows.Visibility.Collapsed;
                        txt2ndBEGINLINE3.Visibility = System.Windows.Visibility.Visible;
                        BEGINLINE32nd = "";
                    }

                    #endregion

                    #region BEGINROLL2_LINE4

                    if (items[0].BEGINROLL2_LINE4 != null)
                    {
                        txt2ndBEGINLINE4.Text = items[0].BEGINROLL2_LINE4;

                        if (txtBatchNo4.Text == txt2ndBEGINLINE4.Text)
                        {
                            img2ndTrue4.Visibility = System.Windows.Visibility.Visible;
                            txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            BEGINLINE42nd = "Yes";
                        }
                        else
                        {
                            img2ndTrue4.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                            BEGINLINE42nd = "";
                        }
                    }
                    else
                    {
                        img2ndTrue4.Visibility = System.Windows.Visibility.Collapsed;
                        txt2ndBEGINLINE4.Visibility = System.Windows.Visibility.Visible;
                        BEGINLINE42nd = "";
                    }

                    #endregion

                    if (items[0].DENSITYWARP != null)
                        txtDENWARP.Text = items[0].DENSITYWARP.Value.ToString("#,##0.##");

                    if (items[0].DENSITYWEFT != null)
                        txtDENWEFT.Text = items[0].DENSITYWEFT.Value.ToString("#,##0.##");

                    if (items[0].DISTANTBARCODE1 != null)
                        txtDISTANTBAR1.Text = items[0].DISTANTBARCODE1.Value.ToString("#,##0.##");
                    if (items[0].DISTANTBARCODE2 != null)
                        txtDISTANTBAR2.Text = items[0].DISTANTBARCODE2.Value.ToString("#,##0.##");
                    if (items[0].DISTANTBARCODE3 != null)
                        txtDISTANTBAR3.Text = items[0].DISTANTBARCODE3.Value.ToString("#,##0.##");
                    if (items[0].DISTANTBARCODE4 != null)
                        txtDISTANTBAR4.Text = items[0].DISTANTBARCODE4.Value.ToString("#,##0.##");

                    if (items[0].DISTANTLINE1 != null)
                        txtDISTANTLINE1.Text = items[0].DISTANTLINE1.Value.ToString("#,##0.##");
                    if (items[0].DISTANTLINE2 != null)
                        txtDISTANTLINE2.Text = items[0].DISTANTLINE2.Value.ToString("#,##0.##");
                    if (items[0].DISTANTLINE3 != null)
                        txtDISTANTLINE3.Text = items[0].DISTANTLINE3.Value.ToString("#,##0.##");

                    if (items[0].WIDTHBARCODE1 != null)
                        txtWIDTH1.Text = items[0].WIDTHBARCODE1.Value.ToString("#,##0.##");
                    if (items[0].WIDTHBARCODE2 != null)
                        txtWIDTH2.Text = items[0].WIDTHBARCODE2.Value.ToString("#,##0.##");
                    if (items[0].WIDTHBARCODE3 != null)
                        txtWIDTH3.Text = items[0].WIDTHBARCODE3.Value.ToString("#,##0.##");
                    if (items[0].WIDTHBARCODE4 != null)
                        txtWIDTH4.Text = items[0].WIDTHBARCODE4.Value.ToString("#,##0.##");

                    if (items[0].SELVAGE_LEFT != null)
                    {
                        cbStatusLeft.SelectedValue = items[0].SELVAGE_LEFT;
                    }

                    if (items[0].SELVAGE_RIGHT != null)
                    {
                        cbStatusRight.SelectedValue = items[0].SELVAGE_RIGHT;
                    }

                    if (items[0].SPEED != null)
                        txtSPEED.Text = items[0].SPEED.Value.ToString("#,##0.##");

                    #region ENDROLL_LINE1

                    if (items[0].ENDROLL_LINE1 != null)
                    {
                        txtENDLINE1.Text = items[0].ENDROLL_LINE1;

                        if (txtBatchNo1.Text == txtENDLINE1.Text)
                        {
                            imgTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE1 = "Yes";
                        }
                        else
                        {
                            imgTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                            txtENDLINE1.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE1 = "";
                        }
                    }
                    else
                    {
                        imgTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                        txtENDLINE1.Visibility = System.Windows.Visibility.Visible;
                        ENDLINE1 = "";
                    }

                    #endregion

                    #region ENDROLL_LINE2

                    if (items[0].ENDROLL_LINE2 != null)
                    {
                        txtENDLINE2.Text = items[0].ENDROLL_LINE2;

                        if (txtBatchNo2.Text == txtENDLINE2.Text)
                        {
                            imgTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE2 = "Yes";
                        }
                        else
                        {
                            imgTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                            txtENDLINE2.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE2 = "";
                        }
                    }
                    else
                    {
                        imgTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                        txtENDLINE2.Visibility = System.Windows.Visibility.Visible;
                        ENDLINE2 = "";
                    }

                    #endregion

                    #region ENDROLL_LINE3

                    if (items[0].ENDROLL_LINE3 != null)
                    {
                        txtENDLINE3.Text = items[0].ENDROLL_LINE3;

                        if (txtBatchNo3.Text == txtENDLINE3.Text)
                        {
                            imgTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE3 = "Yes";
                        }
                        else
                        {
                            imgTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                            txtENDLINE3.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE3 = "";
                        }
                    }
                    else
                    {
                        imgTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                        txtENDLINE3.Visibility = System.Windows.Visibility.Visible;
                        ENDLINE3 = "";
                    }

                    #endregion

                    #region ENDROLL_LINE4

                    if (items[0].ENDROLL_LINE4 != null)
                    {
                        txtENDLINE4.Text = items[0].ENDROLL_LINE4;

                        if (txtBatchNo4.Text == txtENDLINE4.Text)
                        {
                            imgTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                            txtENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE4 = "Yes";
                        }
                        else
                        {
                            imgTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                            txtENDLINE4.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE4 = "";
                        }
                    }
                    else
                    {
                        imgTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                        txtENDLINE4.Visibility = System.Windows.Visibility.Visible;
                        ENDLINE4 = "";
                    }

                    #endregion

                    #region ENDROLL2_LINE1

                    if (items[0].ENDROLL2_LINE1 != null)
                    {
                        txt2ndENDLINE1.Text = items[0].ENDROLL2_LINE1;

                        if (txtBatchNo1.Text == txt2ndENDLINE1.Text)
                        {
                            img2ndTrueEnd1.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE1.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE12nd = "Yes";
                        }
                        else
                        {
                            img2ndTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndENDLINE1.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE12nd = "";
                        }
                    }
                    else
                    {
                        img2ndTrueEnd1.Visibility = System.Windows.Visibility.Collapsed;
                        txt2ndENDLINE1.Visibility = System.Windows.Visibility.Visible;
                        ENDLINE12nd = "";
                    }

                    #endregion

                    #region ENDROLL2_LINE2

                    if (items[0].ENDROLL2_LINE2 != null)
                    {
                        txt2ndENDLINE2.Text = items[0].ENDROLL2_LINE2;

                        if (txtBatchNo2.Text == txt2ndENDLINE2.Text)
                        {
                            img2ndTrueEnd2.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE2.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE22nd = "Yes";
                        }
                        else
                        {
                            img2ndTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndENDLINE2.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE22nd = "";
                        }
                    }
                    else
                    {
                        img2ndTrueEnd2.Visibility = System.Windows.Visibility.Collapsed;
                        txt2ndENDLINE2.Visibility = System.Windows.Visibility.Visible;
                        ENDLINE22nd = "";
                    }

                    #endregion

                    #region ENDROLL2_LINE3

                    if (items[0].ENDROLL2_LINE3 != null)
                    {
                        txt2ndENDLINE3.Text = items[0].ENDROLL2_LINE3;

                        if (txtBatchNo3.Text == txt2ndENDLINE3.Text)
                        {
                            img2ndTrueEnd3.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE3.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE32nd = "Yes";
                        }
                        else
                        {
                            img2ndTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndENDLINE3.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE32nd = "";
                        }
                    }
                    else
                    {
                        img2ndTrueEnd3.Visibility = System.Windows.Visibility.Collapsed;
                        txt2ndENDLINE3.Visibility = System.Windows.Visibility.Visible;
                        ENDLINE32nd = "";
                    }

                    #endregion

                    #region ENDROLL2_LINE4

                    if (items[0].ENDROLL2_LINE4 != null)
                    {
                        txt2ndENDLINE4.Text = items[0].ENDROLL2_LINE4;

                        if (txtBatchNo4.Text == txt2ndENDLINE4.Text)
                        {
                            img2ndTrueEnd4.Visibility = System.Windows.Visibility.Visible;
                            txt2ndENDLINE4.Visibility = System.Windows.Visibility.Collapsed;
                            ENDLINE42nd = "Yes";
                        }
                        else
                        {
                            img2ndTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                            txt2ndENDLINE4.Visibility = System.Windows.Visibility.Visible;
                            ENDLINE42nd = "";
                        }
                    }
                    else
                    {
                        img2ndTrueEnd4.Visibility = System.Windows.Visibility.Collapsed;
                        txt2ndENDLINE4.Visibility = System.Windows.Visibility.Visible;
                        ENDLINE42nd = "";
                    }

                    #endregion

                    if (items[0].LENGTHPRINT != null)
                        txtLengthPrint.Text = items[0].LENGTHPRINT.Value.ToString("#,##0.##");

                    //เพิ่ม 28/06/17
                    if (items[0].TENSION != null)
                        txtTension.Text = items[0].TENSION.Value.ToString("#,##0.##");

                    //เพิ่ม 25/08/17
                    if (items[0].LENGTHDETAIL != null)
                        txtLengthDetail.Text = items[0].LENGTHDETAIL;

                    txtREMARK.Focus();
                    txtREMARK.SelectAll();
                }
                else
                {
                    chkGetData = false;

                    txtITEMLOT.Focus();
                    txtITEMLOT.SelectAll();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                chkGetData = false;
                GetMC = null;
            }

            return GetMC;
        }
        #endregion

        #region Enabled2ndBarcode
        private void Enabled2ndBarcode(bool status)
        {
            if (status == true)
            {
                txt2ndBatchNo1.IsEnabled = status;
                txt2ndBatchNo2.IsEnabled = status;
                txt2ndBatchNo3.IsEnabled = status;
                txt2ndBatchNo4.IsEnabled = status;

                txt2ndBEGINLINE1.IsEnabled = status;
                txt2ndBEGINLINE2.IsEnabled = status;
                txt2ndBEGINLINE3.IsEnabled = status;
                txt2ndBEGINLINE4.IsEnabled = status;

                txt2ndENDLINE1.IsEnabled = status;
                txt2ndENDLINE2.IsEnabled = status;
                txt2ndENDLINE3.IsEnabled = status;
                txt2ndENDLINE4.IsEnabled = status;

                txt2ndBatchNo1.Text = string.Empty;
                txt2ndBatchNo2.Text = string.Empty;
                txt2ndBatchNo3.Text = string.Empty;
                txt2ndBatchNo4.Text = string.Empty;

                txt2ndBEGINLINE1.Text = string.Empty;
                txt2ndBEGINLINE2.Text = string.Empty;
                txt2ndBEGINLINE3.Text = string.Empty;
                txt2ndBEGINLINE4.Text = string.Empty;

                txt2ndENDLINE1.Text = string.Empty;
                txt2ndENDLINE2.Text = string.Empty;
                txt2ndENDLINE3.Text = string.Empty;
                txt2ndENDLINE4.Text = string.Empty;
            }
            else
            {
                txt2ndBatchNo1.IsEnabled = status;
                txt2ndBatchNo2.IsEnabled = status;
                txt2ndBatchNo3.IsEnabled = status;
                txt2ndBatchNo4.IsEnabled = status;

                txt2ndBEGINLINE1.IsEnabled = status;
                txt2ndBEGINLINE2.IsEnabled = status;
                txt2ndBEGINLINE3.IsEnabled = status;
                txt2ndBEGINLINE4.IsEnabled = status;

                txt2ndENDLINE1.IsEnabled = status;
                txt2ndENDLINE2.IsEnabled = status;
                txt2ndENDLINE3.IsEnabled = status;
                txt2ndENDLINE4.IsEnabled = status;
            }
        }
        #endregion

        #region EnabledCM08
        private void EnabledCM08(bool status)
        {
            if (status == true)
            {
                txt2ndBatchNo1.IsEnabled = status;
                txt2ndBatchNo2.IsEnabled = status;
                txt2ndBatchNo3.IsEnabled = status;
                txt2ndBatchNo4.IsEnabled = status;

                txt2ndBEGINLINE1.IsEnabled = status;
                txt2ndBEGINLINE2.IsEnabled = status;
                txt2ndBEGINLINE3.IsEnabled = status;
                txt2ndBEGINLINE4.IsEnabled = status;

                txt2ndENDLINE1.IsEnabled = status;
                txt2ndENDLINE2.IsEnabled = status;
                txt2ndENDLINE3.IsEnabled = status;
                txt2ndENDLINE4.IsEnabled = status;

                txt2ndBatchNo1.Text = string.Empty;
                txt2ndBatchNo2.Text = string.Empty;
                txt2ndBatchNo3.Text = string.Empty;
                txt2ndBatchNo4.Text = string.Empty;

                txt2ndBEGINLINE1.Text = string.Empty;
                txt2ndBEGINLINE2.Text = string.Empty;
                txt2ndBEGINLINE3.Text = string.Empty;
                txt2ndBEGINLINE4.Text = string.Empty;

                txt2ndENDLINE1.Text = string.Empty;
                txt2ndENDLINE2.Text = string.Empty;
                txt2ndENDLINE3.Text = string.Empty;
                txt2ndENDLINE4.Text = string.Empty;
            }
            else
            {
                txt2ndBatchNo1.IsEnabled = status;
                txt2ndBatchNo2.IsEnabled = status;
                txt2ndBatchNo3.IsEnabled = status;
                txt2ndBatchNo4.IsEnabled = status;

                txt2ndBEGINLINE1.IsEnabled = status;
                txt2ndBEGINLINE2.IsEnabled = status;
                txt2ndBEGINLINE3.IsEnabled = status;
                txt2ndBEGINLINE4.IsEnabled = status;

                txt2ndENDLINE1.IsEnabled = status;
                txt2ndENDLINE2.IsEnabled = status;
                txt2ndENDLINE3.IsEnabled = status;
                txt2ndENDLINE4.IsEnabled = status;
            }

            #region Old
            //if (status == true)
            //{
            //    txt2ndBEGINLINE2.IsEnabled = status;
            //    txt2ndBEGINLINE3.IsEnabled = status;
            //    txt2ndBEGINLINE4.IsEnabled = status;

            //    txt2ndENDLINE2.IsEnabled = status;
            //    txt2ndENDLINE3.IsEnabled = status;
            //    txt2ndENDLINE4.IsEnabled = status;

            //    txt2ndBEGINLINE2.Text = string.Empty;
            //    txt2ndBEGINLINE3.Text = string.Empty;
            //    txt2ndBEGINLINE4.Text = string.Empty;

            //    txt2ndENDLINE2.Text = string.Empty;
            //    txt2ndENDLINE3.Text = string.Empty;
            //    txt2ndENDLINE4.Text = string.Empty;

            //    txtBEGINLINE2.IsEnabled = status;
            //    txtBEGINLINE3.IsEnabled = status;
            //    txtBEGINLINE4.IsEnabled = status;

            //    txtENDLINE2.IsEnabled = status;
            //    txtENDLINE3.IsEnabled = status;
            //    txtENDLINE4.IsEnabled = status;

            //    txtBEGINLINE2.Text = string.Empty;
            //    txtBEGINLINE3.Text = string.Empty;
            //    txtBEGINLINE4.Text = string.Empty;

            //    txtENDLINE2.Text = string.Empty;
            //    txtENDLINE3.Text = string.Empty;
            //    txtENDLINE4.Text = string.Empty;

            //    //labDistant1.Text = "Distant between barcode and number (cm.)";
            //    //labDistant2.Text = "Distant between line by line (cm.)";
            //}
            //else
            //{
            //    txt2ndBEGINLINE2.IsEnabled = status;
            //    txt2ndBEGINLINE3.IsEnabled = status;
            //    txt2ndBEGINLINE4.IsEnabled = status;

            //    txt2ndENDLINE2.IsEnabled = status;
            //    txt2ndENDLINE3.IsEnabled = status;
            //    txt2ndENDLINE4.IsEnabled = status;

            //    txtBEGINLINE2.IsEnabled = status;
            //    txtBEGINLINE3.IsEnabled = status;
            //    txtBEGINLINE4.IsEnabled = status;

            //    txtENDLINE2.IsEnabled = status;
            //    txtENDLINE3.IsEnabled = status;
            //    txtENDLINE4.IsEnabled = status;

            //    //labDistant1.Text = "Distant Between Set to Set";
            //    //labDistant2.Text = "Distant Between Edge L Side";
            //}
            #endregion
        }
        #endregion

        #region Cal
        private void Cal()
        {
            try
            {
                string math = txtLengthDetail.Text;
                math = math.Replace(",", "");
                string value = new DataTable().Compute(math, null).ToString();
                txtLengthPrint.Text = value;

                txtLengthPrint.Focus();
                txtLengthPrint.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region D365_CP
        private void D365_CP()
        {
            try
            {
                ITEMLOT = txtITEMLOT.Text;

                if (!string.IsNullOrEmpty(ITEMLOT))
                {
                    if (D365_CP_BPO() == true)
                    {
                        if (PRODID != null)
                        {
                            if (D365_CP_ISH(PRODID) == true)
                            {
                                if (HEADERID != null)
                                {
                                    if (D365_CP_ISL(HEADERID) == true)
                                    {
                                        if (D365_CP_OPH(PRODID) == true)
                                        {
                                            if (HEADERID != null)
                                            {
                                                if (D365_CP_OPL(HEADERID) == true)
                                                {
                                                    if (D365_CP_OUH(PRODID) == true)
                                                    {
                                                        if (HEADERID != null)
                                                        {
                                                            if (D365_CP_OUL(HEADERID) == true)
                                                            {
                                                                "Send D365 complete".Info();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            "HEADERID is null".Info();
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                "HEADERID is null".Info();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    "HEADERID is null".Info();
                                }
                            }
                        }
                        else
                        {
                            "PRODID is null".Info();
                        }
                    }
                }
                else
                {
                    "Item Lot is null".Info();
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        }
        #endregion

        #region D365_CP_BPO
        private bool D365_CP_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_CP_BPOData> results = new List<ListD365_CP_BPOData>();

                results = D365DataService.Instance.D365_CP_BPO(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].PRODID != null)
                            PRODID = Convert.ToInt64(results[i].PRODID);
                        else
                            PRODID = null;

                        if (!string.IsNullOrEmpty(results[i].LOTNO))
                            P_LOTNO = results[i].LOTNO;
                        else
                            P_LOTNO = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].ITEMID))
                            P_ITEMID = results[i].ITEMID;
                        else
                            P_ITEMID = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].LOADINGTYPE))
                            P_LOADINGTYPE = results[i].LOADINGTYPE;
                        else
                            P_LOADINGTYPE = string.Empty;

                        if (PRODID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABBPO(PRODID, results[i].LOTNO, results[i].ITEMID, results[i].LOADINGTYPE, 0, "N", results[i].QTY, results[i].UNIT, results[i].OPERATION);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_BPO Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_ISH
        private bool D365_CP_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_CP_ISHData> results = new List<D365_CP_ISHData>();

                results = D365DataService.Instance.D365_CP_ISH(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABISH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_ISH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_ISL
        private bool D365_CP_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_CP_ISLData> results = new List<ListD365_CP_ISLData>();

                results = D365DataService.Instance.D365_CP_ISL(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string issDate = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].ISSUEDATE != null)
                            issDate = results[i].ISSUEDATE.Value.ToString("yyyy-MM-dd");
                        else
                            issDate = string.Empty;

                        chkError = D365DataService.Instance.Insert_ABISL(HEADERID, results[i].LINENO, "N", 0, issDate, results[i].ITEMID, results[i].STYLEID, results[i].QTY, results[i].UNIT, results[i].SERIALID);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_ISL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_OPH
        private bool D365_CP_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_CP_OPHData> results = new List<D365_CP_OPHData>();

                results = D365DataService.Instance.D365_CP_OPH(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOPH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_OPH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_OPL
        private bool D365_CP_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_CP_OPLData> results = new List<ListD365_CP_OPLData>();

                results = D365DataService.Instance.D365_CP_OPL(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        chkError = D365DataService.Instance.Insert_ABOPL(HEADERID, results[i].LINENO, "N", 0, results[i].PROCQTY, results[i].OPRNO, results[i].OPRID, results[i].MACHINENO, results[i].STARTDATETIME, results[i].ENDDATETIME);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_OPL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_OUH
        private bool D365_CP_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_CP_OUHData> results = new List<D365_CP_OUHData>();

                results = D365DataService.Instance.D365_CP_OUH(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_OUH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_OUL
        private bool D365_CP_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_CP_OULData> results = new List<ListD365_CP_OULData>();

                results = D365DataService.Instance.D365_CP_OUL(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string outputDate = string.Empty;
                    int? finish = null;

                    foreach (var row in results)
                    {
                        if (results[i].OUTPUTDATE != null)
                            outputDate = results[i].OUTPUTDATE.Value.ToString("yyyy-MM-dd");
                        else
                            outputDate = string.Empty;

                        if (results[i].FINISH != null)
                            finish = Convert.ToInt32(results[i].FINISH);
                        else
                            finish = 0;

                        chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, "N", 0, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
                            , results[i].GROSSWEIGHT, results[i].NETWEIGHT, results[i].PALLETNO, results[i].GRADE, results[i].SERIALID, results[i].LOADINGTYPE, finish, results[i].MOVEMENTTRANS, results[i].WAREHOUSE, results[i].LOCATION);


                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_OUL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #endregion

        #region Public Methods

        #region Setup

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="session">The inspection session.</param>
        /// <param name="suspendData">The suspend data.</param>
        public void Setup(CutPrintSession session,
            Domains.INS_GETMCSUSPENDDATAResult suspendData)
        {
            _session = session;
            // Init Session
            InitSession();

            if (null != _session)
            {
                SetupOperatorAndMC(session.CurrentUser.OperatorId, session.Machine.DisplayName, session.Machine.MCId);
            }
        }

        #endregion

        #region SetupOperatorAndMC

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opera"></param>
        /// <param name="mc"></param>
        public void SetupOperatorAndMC(string opera, string mcName, string mcID)
        {
            if (null != opera)
            {
                txtOperator.Text = opera;
            }
            else
            {
                txtOperator.Text = "-";
            }

            if (null != mcName)
            {
                txtMCName.Text = mcName;
            }
            else
            {
                txtMCName.Text = "-";
            }

            txtPrintNo.Visibility = System.Windows.Visibility.Collapsed;

            if (null != mcID)
            {
                txtPrintNo.Text = mcID;
            }
            else
            {
                txtPrintNo.Text = "-";
            }
        }

        #endregion

        #endregion

    }
}
