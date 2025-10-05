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
using System.Windows.Shapes;

using NLib;

#endregion

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for DeleteWeavingLotWindow.xaml
    /// </summary>
    public partial class DeleteWeavingLotWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public DeleteWeavingLotWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables
        
        private string _userId = string.Empty;
        private string mcno = string.Empty;

        #endregion

        #region Private Methods

        private bool VerifyInputs()
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(txtUserName.Text.Trim()))
            {
                txtUserName.Focus();
                txtMsg.Text = "User Name required.";
            }
            else if (string.IsNullOrWhiteSpace(txtPwd.Password.Trim()))
            {
                txtPwd.Focus();
                txtMsg.Text = "Password required.";
            }
            else if (this.ShowRemark && string.IsNullOrWhiteSpace(txtRemark.Text.Trim()))
            {
                txtRemark.Focus();
                txtMsg.Text = "Remark required.";
            }
            else
            {
                // All Ok
                txtMsg.Text = string.Empty;
                result = true;
            }

            return result;
        }

        #endregion

        #region Load

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Focus();

            if (mcno != "")
                txtMCNo.Text = "User Log In " + mcno;
        }

        #endregion

        #region Button Events

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyInputs())
                return;
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region TextBox Handlers

        private void txtUserName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtPwd.Focus();
                txtMsg.Text = string.Empty;
            }
        }

        private void txtPwd_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (this.ShowRemark)
                {
                    txtRemark.Focus();
                }
                else cmdOk.Focus();
                txtMsg.Text = string.Empty;
            }
        }

        private void txtRemark_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdOk.Focus();
                txtMsg.Text = string.Empty;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets is show remark panel.
        /// </summary>
        public bool ShowRemark
        {
            get { return remarkPanel.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                {
                    remarkPanel.Visibility = System.Windows.Visibility.Visible;
                    this.Height = 420;
                }
                else
                {
                    remarkPanel.Visibility = System.Windows.Visibility.Collapsed;
                    this.Height = 300;
                }
            }
        }
        /// <summary>
        /// Gets User Name.
        /// </summary>
        public string UserName { get { return txtUserName.Text; } }
        /// <summary>
        /// Gets Password.
        /// </summary>
        public string Password { get { return txtPwd.Password; } }
        /// <summary>
        /// Gets Remark.
        /// </summary>
        public string Remark { get { return txtRemark.Text; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mcno"></param>
        public void Setup(string _mcno)
        {
            mcno = _mcno;
        }

        #endregion
    }
}

