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
    /// Interaction logic for CauseOfBeamerStopWindow.xaml
    /// </summary>
    public partial class CauseOfBeamerStopWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public CauseOfBeamerStopWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Load

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion
    }
}

