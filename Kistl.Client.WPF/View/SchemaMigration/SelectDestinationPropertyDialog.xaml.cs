using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Kistl.Client.GUI;
using Kistl.Client.Presentables.SchemaMigration;
using Kistl.Client.WPF.CustomControls;

namespace Kistl.Client.WPF.View.SchemaMigration
{
    /// <summary>
    /// Interaction logic for SelectDestinationPropertyDialog.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class SelectDestinationPropertyDialog : WindowView, IHasViewModel<SelectDestinationPropertyViewModel>
    {
        public SelectDestinationPropertyDialog()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<SelectDestinationPropertyViewModel> Members

        public SelectDestinationPropertyViewModel ViewModel
        {
            get { return (SelectDestinationPropertyViewModel)DataContext; }
        }

        #endregion

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = treeValues.SelectedItem as Kistl.Client.Presentables.SchemaMigration.PossibleDestPropertyViewModel;
            if (item != null)
            {
                this.ViewModel.SelectedItem = item;
            }
        }
    }
}
