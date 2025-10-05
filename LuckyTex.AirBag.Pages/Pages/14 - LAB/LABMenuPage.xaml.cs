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

using System.Globalization;
using System.Collections;

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;
using System.Threading;

#endregion


namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for LABMenuPage.xaml
    /// </summary>
    public partial class LABMenuPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public LABMenuPage()
        {
            InitializeComponent();

            cmdManualLaboratory.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        private void cmdGreige_Click(object sender, RoutedEventArgs e)
        {
            GreigeMenuPage page = new GreigeMenuPage();
            PageManager.Instance.Current = page;

            //LogInInfo logInfo = this.ShowLogInBox();
            //if (logInfo != null)
            //{
            //    int processId = 9; // for inspection
            //    List<LogInResult> operators = UserDataService.Instance
            //        .GetOperators(logInfo.UserName, logInfo.Password, processId);

            //    if (null == operators || operators.Count <= 0)
            //    {
            //        "This User can not be Use for This Menu".ShowMessageBox(true);
            //        return;
            //    }
            //    else
            //    {
            //        GreigePage page = new GreigePage();
            //        page.Setup(logInfo.UserName);

            //        PageManager.Instance.Current = page;
            //    }
            //}
        }

        private void cmdMasspro_Click(object sender, RoutedEventArgs e)
        {
            MassProMenuPage page = new MassProMenuPage();
            PageManager.Instance.Current = page;

            //LogInInfo logInfo = this.ShowLogInBox();
            //if (logInfo != null)
            //{
            //    int processId = 9; // for inspection
            //    List<LogInResult> operators = UserDataService.Instance
            //        .GetOperators(logInfo.UserName, logInfo.Password, processId);

            //    if (null == operators || operators.Count <= 0)
            //    {
            //        "This User can not be Use for This Menu".ShowMessageBox(true);
            //        return;
            //    }
            //    else
            //    {
            //        MassProPage page = new MassProPage();
            //        page.Setup(logInfo.UserName);

            //        PageManager.Instance.Current = page;
            //    }
            //}
            // ไม่ได้ใช้ตอนนี้
            //PageManager.Instance.Current = new MassProMenuPage();
        }

        private void cmdManualLaboratory_Click(object sender, RoutedEventArgs e)
        {
            MassProMenuPage page = new MassProMenuPage();
            PageManager.Instance.Current = page;

            //LogInInfo logInfo = this.ShowLogInBox();
            //if (logInfo != null)
            //{
            //    int processId = 9; // for inspection
            //    List<LogInResult> operators = UserDataService.Instance
            //        .GetOperators(logInfo.UserName, logInfo.Password, processId);

            //    if (null == operators || operators.Count <= 0)
            //    {
            //        "This User can not be Use for This Menu".ShowMessageBox(true);
            //        return;
            //    }
            //    else
            //    {

            //        LABPage page = new LABPage();
            //        page.Setup(logInfo.UserName);

            //        PageManager.Instance.Current = page;
            //    }
            //}
        }

        #endregion

    }
}
