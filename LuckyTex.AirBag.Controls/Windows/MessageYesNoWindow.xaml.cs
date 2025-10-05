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
    /// Interaction logic for MessageYesNoWindow.xaml
    /// </summary>
    public partial class MessageYesNoWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public MessageYesNoWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables

        private SolidColorBrush _defBrush = new SolidColorBrush(Colors.Black);

        #endregion

        #region Button Events

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Message Text.
        /// </summary>
        public string MessageText
        {
            get { return txtMsg.Text; }
            set 
            {
                if (txtMsg.Text != value)
                {
                    txtMsg.Text = value;
                }
            }
        }

        #endregion
    }
}
