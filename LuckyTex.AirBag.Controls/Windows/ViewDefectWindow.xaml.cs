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
    /// Interaction logic for ViewDefectWindow.xaml
    /// </summary>
    public partial class ViewDefectWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public ViewDefectWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Private Methods

        private string defectFile = string.Empty;

        #endregion

        #region Load

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DefectFile != "")
            {
                imgDefectFile.Source = new BitmapImage(new Uri(DefectFile));
            }
        }

        #endregion

        #region Button Events

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or set DefectFile message.
        /// </summary>
        public string DefectFile 
        {
            get { return defectFile; }
            set { defectFile = value; } 
        }

        #endregion
    }
}

