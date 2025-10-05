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
    /// Interaction logic for OldWarpingReceiveYarnPage.xaml
    /// </summary>
    public partial class OldWarpingReceiveYarnPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public OldWarpingReceiveYarnPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button Handlers

        private void cmdOpenForm_Click(object sender, RoutedEventArgs e)
        {
            ReceiveDetailInfo receiveDetail = this.ShowReceiveDetailBox();
            if (receiveDetail != null)
            {

            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            "Save data success.".ShowMessageBox();
            PageManager.Instance.Back();
        }

        #endregion

        #region TextBox Handlers

        private void txtPalletNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                e.Handled = true;
            }
        }

        #endregion
    }
}
