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
    /// Interaction logic for ReceiveYARNDetailPage.xaml
    /// </summary>
    public partial class ReceiveYARNDetailPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReceiveYARNDetailPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables


        #endregion

        #region Private Methods

        private void AddNewItem()
        {

        }

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        #region cmdOpenForm_Click

        private void cmdOpenForm_Click(object sender, RoutedEventArgs e)
        {
            ReceiveDetailInfo receiveDetail = this.ShowReceiveDetailBox();
            if (receiveDetail != null)
            {

            }
        }

        #endregion

        #region cmdBack_Click

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region cmdDelete_Click

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #endregion

        #region TextBox Handlers

        private void txtPalletNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                AddNewItem();
                e.Handled = true;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="logIn"></param>
        /// <param name="date"></param>
        /// <param name="yarnType"></param>
        /// <param name="isWarp"></param>
        /// <param name="isWeft"></param>
        public void Setup(LogInInfo logIn, DateTime date, string yarnType,
            bool isWarp, bool isWeft)
        {

        }

        #endregion
    }
}
