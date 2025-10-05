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
using LuckyTex.Services;
using LuckyTex.Models;

using System.Collections;
using System.Data;

#endregion

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for InspectionLotSelectionWindow.xaml
    /// </summary>
    public partial class InspectionLotSelectionWindow : Window
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public InspectionLotSelectionWindow()
        {
            InitializeComponent();
        }

        #endregion

        private string lotNo = string.Empty;

        #region Load/Unload

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LotNoText = "";
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="items">The item source.</param>
        public void Setup(List<INS_GetFinishinslotData> items)
        {
            // Binding.
            this.DataContext = items;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Selection Lot.
        /// </summary>
        public InspectionLotData SelectionLot 
        {
            get 
            { 
                return null; 
            }
        }

        #endregion

        #region Button Events

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            LotNoText = "";
            this.DialogResult = false;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets lotNo Text.
        /// </summary>
        public string LotNoText
        {
            get { return lotNo; }
            set
            {
                if (lotNo != value)
                {
                    lotNo = value;
                }
            }
        }

        #endregion

        #region gridInsLots_SelectedCellsChanged

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        private void gridInsLots_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                LotNoText = ((LuckyTex.Models.INS_GetFinishinslotData)(gridInsLots.CurrentCell.Item)).INSPECTIONLOT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
