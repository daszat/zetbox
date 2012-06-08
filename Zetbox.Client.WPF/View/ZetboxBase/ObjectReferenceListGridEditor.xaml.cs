
namespace Zetbox.Client.WPF.View.ZetboxBase
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Zetbox.API;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.WPF.Commands;
    using Microsoft.Windows.Controls;
    using Zetbox.Client.WPF.Toolkit;
    using Zetbox.Client.WPF.CustomControls;
    using Microsoft.Windows.Controls.Primitives;

    /// <summary>
    /// Interaction logic for DataObjectListView.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class ObjectReferenceListGridEditor
        : PropertyEditor, IHasViewModel<ObjectListViewModel>
    {
        public ObjectReferenceListGridEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        private void ItemActivatedHandler(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.ActivateItem(ViewModel.SelectedItem, true);
            }
            e.Handled = true;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                WPFHelper.RefreshGridView(lst, ViewModel.DisplayedColumns, WpfSortHelper.SortPropertyNameProperty);
                this.ApplyIsBusyBehaviour(ViewModel);
            }
        }

        #region IHasViewModel<ObjectListModel> Members

        public ObjectListViewModel ViewModel
        {
            get { return (ObjectListViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return lst; }
        }

        #region HeaderClickManagement
        protected void SetHeaderTemplate(DependencyObject header, DataTemplate template)
        {
            header.SetValue(DataGridColumn.HeaderTemplateProperty, template);
        }

        protected void ListView_HeaderClick(object sender, RoutedEventArgs e)
        {
            var header = e.OriginalSource as DataGridColumnHeader;
            if (header != null)
            {
                SortHelper.ApplySort(header.Column);
            }
            e.Handled = true;
        }

        private WpfSortHelper _sortHelper;
        protected WpfSortHelper SortHelper
        {
            get
            {
                if (_sortHelper == null)
                {
                    _sortHelper = new WpfSortHelper(this, ViewModel, SetHeaderTemplate);
                }
                return _sortHelper;
            }
        }
        #endregion
    }
}
