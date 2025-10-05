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
    /// Interaction logic for RemarkWindow.xaml
    /// </summary>
    public partial class RemarkWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public RemarkWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Private Methods

        private bool VerifyInputs()
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(txtRemark.Text.Trim()))
            {
                txtRemark.Focus();
            }
            else
            {
                // All Ok
                result = true;
            }

            return result;
        }

        #endregion

        #region Load

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtRemark.Focus();
        }

        #endregion

        #region Button Events

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyInputs())
            {
                if (MessageBox.Show("Do you want to Save", "Please check data", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;
                else
                    this.DialogResult = true;
            }
            else
            {
                this.DialogResult = true;
            }
            
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region TextBox Handlers

        private void txtRemark_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter || e.Key == Key.Return)
            //{
            //    cmdOk.Focus();
            //}
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or set Remark message.
        /// </summary>
        public string Remark 
        { 
            get { return txtRemark.Text; }
            set { txtRemark.Text = value; } 
        }

        #endregion
    }
}

