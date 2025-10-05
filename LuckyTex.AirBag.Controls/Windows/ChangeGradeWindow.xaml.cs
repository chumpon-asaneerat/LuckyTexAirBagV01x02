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
using LuckyTex.Models;

#endregion

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for ChangeGradeWindow.xaml
    /// </summary>
    public partial class ChangeGradeWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public ChangeGradeWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables
        
        private InspectionSession _session = null;

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

        private bool UpdateHistory(string oldGrade, string newGrade)
        {
            bool result = false;

            if (null != _session)
            {
                result = _session.ChangeGrade(oldGrade, newGrade,
                    this.Remark, this.UserName, this.Password);
            }

            if (!result)
            {
                txtMsg.Text = 
                    "Invalid User/Password" + Environment.NewLine +
                    "or unauthorized user." + Environment.NewLine + 
                    "Please try again.";
                // Change focus.
                txtUserName.SelectAll();
                txtUserName.Focus();
            }

            return result;
        }

        #endregion

        #region Load

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Focus();
        }

        #endregion

        #region Button Events

        private void cmd1_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyInputs())
                return;
            if (!UpdateHistory(this.Grade, cmd1.Content.ToString()))
                return;

            // Set grade.
            this.Grade = cmd1.Content.ToString();

            this.DialogResult = true;
        }

        private void cmd2_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyInputs())
                return;
            if (!UpdateHistory(this.Grade, cmd2.Content.ToString()))
                return;
            
            // Set grade.
            this.Grade = cmd2.Content.ToString();

            this.DialogResult = true;
        }

        #endregion

        #region TextBox Handlers

        private void txtUserName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtPwd.SelectAll();
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
                    txtRemark.SelectAll();
                    txtRemark.Focus();
                }
                txtMsg.Text = string.Empty;
            }
        }

        private void txtRemark_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMsg.Text = string.Empty;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="grade">The grade string.</param>
        /// <param name="session">The current session.</param>
        public void Setup(string grade, InspectionSession session)
        {
            _session = session;
            this.Grade = grade;

            if (string.IsNullOrWhiteSpace(this.Grade))
            {
                cmd1.IsEnabled = false;
                cmd2.IsEnabled = false;
            }
            else if (this.Grade == "A")
            {
                cmd1.Content = "B";
                cmd2.Content = "C";
            }
            else if (this.Grade == "B")
            {
                cmd1.Content = "A";
                cmd2.Content = "C";
            }
            else if (this.Grade == "C")
            {
                cmd1.Content = "A";
                cmd2.Content = "B";
            }
            else
            {
                cmd1.IsEnabled = false;
                cmd2.IsEnabled = false;
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
                    remarkPanel.Visibility = System.Windows.Visibility.Visible;
                else remarkPanel.Visibility = System.Windows.Visibility.Hidden;
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
        /// Gets current grade.
        /// </summary>
        public string Grade { get; private set; }

        #endregion
    }
}
