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

using Kistl.Client.Presentables;
using Kistl.Client.GUI;
using Kistl.Client.WPF.CustomControls;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for SelectionDialog.xaml
    /// </summary>
    public partial class SelectionDialog : WindowView, IHasViewModel<DataObjectSelectionTaskViewModel>
    {
        public SelectionDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ViewModel_PropertyChanged);

        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Show" && !ViewModel.Show) this.Close();
        }

        private void ChooseClickHandler(object sender, RoutedEventArgs e)
        {
            var choosen = ViewModel.SelectedItem;
            if (choosen == null) return;
            ViewModel.Choose(choosen);
            this.Close();
        }

        private void CancelClickHandler(object sender, RoutedEventArgs e)
        {
            ViewModel.Choose(null);
            this.Close();            
        }

        #region IHasViewModel<DataObjectSelectionTaskViewModel> Members

        public DataObjectSelectionTaskViewModel ViewModel
        {
            get { return (DataObjectSelectionTaskViewModel)DataContext; }
        }

        #endregion
    }
}
