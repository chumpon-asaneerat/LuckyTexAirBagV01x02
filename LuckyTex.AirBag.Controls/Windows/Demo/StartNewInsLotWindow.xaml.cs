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
    /// Interaction logic for StartNewInsLotWindow.xaml
    /// </summary>
    public partial class StartNewInsLotWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public StartNewInsLotWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Button Events

        private void cmdYES_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdNO_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion
    }
}
