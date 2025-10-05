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
using LuckyTex.Pages;
#endregion

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for ConfirmstartLengthWindow.xaml
    /// </summary>
    public partial class ConfirmstartLengthWindow : Window
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public ConfirmstartLengthWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables
        private string _confirmstartLength = null;

        #endregion

        #region Load/Unload

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           txtConfirmStarting.Text = _confirmstartLength;

           cmdSave.Focus();
        }
        #endregion

        #region Public Methods

        public void Setup(string confirmstartLength)
        {
            _confirmstartLength = confirmstartLength;
            cmdSave.Focus();
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        #endregion

        #region TextBox Handlers

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

    }
}
