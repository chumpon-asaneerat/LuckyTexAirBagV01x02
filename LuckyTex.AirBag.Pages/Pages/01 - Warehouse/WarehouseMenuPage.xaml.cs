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
    /// Interaction logic for WarehouseMenuPage.xaml
    /// </summary>
    public partial class WarehouseMenuPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WarehouseMenuPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        private void cmdYarn_Click(object sender, RoutedEventArgs e)
        {
            if (rbReceive.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    ReceiveYARNPage page = new ReceiveYARNPage();
                    page.Setup(logInfo.UserName);
                    PageManager.Instance.Current = page;
                }
            }
            else if (rbDeliver.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    PageManager.Instance.Current = new DeliverYarnDetailPage();
                }
            }
        }

        private void cmdSilicone_Click(object sender, RoutedEventArgs e)
        {
            if (rbReceive.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    PageManager.Instance.Current = new ReceiveSiliconePage();
                }
            }
            else if (rbDeliver.IsChecked == true)
            {
                "Deliver Silicone is on development.".ShowMessageBox();
            }
        }

        private void cmdFabric_Click(object sender, RoutedEventArgs e)
        {
            if (rbReceive.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    PageManager.Instance.Current = new ReceiveFabricPage();
                }
            }
            else if (rbDeliver.IsChecked == true)
            {
                "Deliver Fabric is on development.".ShowMessageBox();
            }
        }

        #endregion
    }
}
