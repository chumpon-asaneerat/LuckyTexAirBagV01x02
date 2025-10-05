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

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for DrawingProcessPage.xaml
    /// </summary>
    public partial class DrawingProcessPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public DrawingProcessPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button Handlers

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new CreateNewWarpingLotPage();
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
        }

        private void cmdEnd_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
        }

        #endregion
    }
}
