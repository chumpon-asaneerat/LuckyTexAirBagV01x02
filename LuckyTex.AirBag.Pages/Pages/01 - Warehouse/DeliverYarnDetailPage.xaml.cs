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
    /// Interaction logic for DeliverYarnDetailPage.xaml
    /// </summary>
    public partial class DeliverYarnDetailPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public DeliverYarnDetailPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        #region cmdConfirm_Click

        private void cmdConfirm_Click(object sender, RoutedEventArgs e)
        {
            "Confirm data success.".ShowMessageBox();
            PageManager.Instance.Back();
        }

        #endregion

        #region cmdBack_Click

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #endregion
    }
}
