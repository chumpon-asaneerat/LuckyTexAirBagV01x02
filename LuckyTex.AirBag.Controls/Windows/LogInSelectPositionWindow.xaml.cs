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
    /// Interaction logic for LogInSelectPositionWindow.xaml
    /// </summary>
    public partial class LogInSelectPositionWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public LogInSelectPositionWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables
        
        private string _userId = string.Empty;
        private string _position = string.Empty;
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

        private void cmdPreparing_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyInputs())
                return;
            else
                _position = "Preparing";

            this.DialogResult = true;
        }

        private void cmdFinishing_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyInputs())
                return;
            else
                _position = "Finishing";

            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            _position = string.Empty;

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
                txtMsg.Text = string.Empty;
            }
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets User Name.
        /// </summary>
        public string UserName { get { return txtUserName.Text; } }
        /// <summary>
        /// Gets Password.
        /// </summary>
        public string Password { get { return txtPwd.Password; } }
        /// <summary>
        /// Gets Position.
        /// </summary>
        public string Position { get { return _position; } }

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

