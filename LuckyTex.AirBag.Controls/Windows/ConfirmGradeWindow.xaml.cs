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
using LuckyTex.Models;

#endregion

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for ConfirmGradeWindow.xaml
    /// </summary>
    public partial class ConfirmGradeWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public ConfirmGradeWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables

        private InspectionSession _session = null;
        private ConfirmGradeInfo _confirmResult = new ConfirmGradeInfo();

        #endregion

        #region Button Events

        private void cmdConfirm_Click(object sender, RoutedEventArgs e)
        {
            _confirmResult.IsConfirm = true;
            this.DialogResult = true;
        }

        private void cmdChangeGrade_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = false;
            ChangeGradeWindow window = new ChangeGradeWindow();
            window.ShowRemark = true;
            window.Setup(_confirmResult.Grade, _session); // Assigned Grade.
            if (window.ShowDialog() == true)
            {
                // Get New Grade
                _confirmResult.Grade = window.Grade;
                txtGrade.Text = _confirmResult.Grade; // Update text block
            }
            else
            {
                // User Close window without change grade.
            }
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Setup Current Grade.
        /// </summary>
        /// <param name="grade">The grade string.</param>
        /// <param name="session">The current session.</param>
        public void Setup(string grade, InspectionSession session)
        {
            _session = session;
            if (!string.IsNullOrWhiteSpace(grade))
            {
                txtGrade.Text = grade;
            }
            else txtGrade.Text = string.Empty;
            // Init Result value
            _confirmResult.Grade = txtGrade.Text;
            _confirmResult.IsConfirm = false;
            _confirmResult.ErrMessage = 
                "Cannot Stop Process." + Environment.NewLine + 
                "Please Confirm Grade to Stop process.";
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets Confirm Result.
        /// </summary>
        public ConfirmGradeInfo ConfirmResult { get { return _confirmResult; } }

        #endregion
    }
}
