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
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public MessageWindow()
        {
            InitializeComponent();
            cmdOk.Focus();
        }

        #endregion

        #region Internal Variables

        private bool _isError = false;
        private SolidColorBrush _errBrush = new SolidColorBrush(Colors.Red);
        private SolidColorBrush _defBrush = new SolidColorBrush(Colors.Black);

        #endregion

        #region Button Events

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
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
        /// <summary>
        /// Gets or sets is error message
        /// </summary>
        public bool IsError
        {
            get { return _isError; }
            set
            {
                if (_isError != value)
                {
                    _isError = value;
                    if (_isError)
                        txtMsg.Foreground = _errBrush;
                    else txtMsg.Foreground = _defBrush;
                }
            }
        }

        #endregion
    }
}
