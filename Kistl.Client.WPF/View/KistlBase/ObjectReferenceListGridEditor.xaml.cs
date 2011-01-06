
namespace Kistl.Client.WPF.View.KistlBase
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
    using Kistl.API;
    using Kistl.Client.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Client.WPF.Commands;
    using Microsoft.Windows.Controls;
    using Kistl.Client.WPF.Toolkit;

    /// <summary>
    /// Interaction logic for DataObjectListView.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class ObjectReferenceListGridEditor
        : PropertyEditor, IHasViewModel<ObjectListViewModel>
    {
        public ObjectReferenceListGridEditor()
        {
            InitializeComponent();
        }

        private void ItemActivatedHandler(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.ActivateItem(ViewModel.SelectedItem, true);
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                WPFHelper.RefreshGridView(lst, ViewModel.DisplayedColumns, null);
                // Attach to selection changed event on ViewModel side
                ViewModel.SelectedProxies.CollectionChanged += ViewModel_SelectedItems_CollectionChanged;
            }
        }

        private bool _selectedItemsChangedByViewModel = false;
        private bool _selectedItemsChangedByList = false;

        protected void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectedItemsChangedByViewModel) return;

            _selectedItemsChangedByList = true;
            try
            {
                if (e.OriginalSource == lst)
                {
                    e.Handled = true;
                    e.RemovedItems.ForEach<DataObjectViewModelProxy>(i => ViewModel.SelectedProxies.Remove(i));
                    e.AddedItems.ForEach<DataObjectViewModelProxy>(i => ViewModel.SelectedProxies.Add(i, true));
                }
            }
            finally
            {
                _selectedItemsChangedByList = false;
            }
        }

        void ViewModel_SelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_selectedItemsChangedByList) return;

            _selectedItemsChangedByViewModel = true;
            try
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                {
                    lst.SelectedItems.Clear();
                }
                else
                {
                    if (e.OldItems != null) e.OldItems.ForEach<object>(i => lst.SelectedItems.Remove(i));
                    if (e.NewItems != null) e.NewItems.ForEach<object>(i => lst.SelectedItems.Add(i));
                }
            }
            finally
            {
                _selectedItemsChangedByViewModel = false;
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
    }
}
