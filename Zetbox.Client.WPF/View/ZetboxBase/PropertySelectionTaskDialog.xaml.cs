
namespace Zetbox.Client.WPF.View
{
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
    using Zetbox.Client.GUI;
    using Zetbox.Client.WPF.CustomControls;
    using Zetbox.Client.Presentables.ZetboxBase;

    /// <summary>
    /// Interaction logic for SelectDestinationPropertyDialog.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class PropertySelectionTaskDialog : WindowView, IHasViewModel<PropertySelectionTaskViewModel>
    {
        public PropertySelectionTaskDialog()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<PropertySelectionTaskViewModel> Members

        public PropertySelectionTaskViewModel ViewModel
        {
            get { return (PropertySelectionTaskViewModel)DataContext; }
        }

        #endregion

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = treeValues.SelectedItem as SelectedPropertyViewModel;
            if (item != null)
            {
                this.ViewModel.SelectedItem = item;
            }
        }

        private void TreeView_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Choose();
            e.Handled = true;
        }
    }
}
