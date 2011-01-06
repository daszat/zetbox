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
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.Client.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.KistlBase;
    using Microsoft.Windows.Controls;
    using Kistl.Client.WPF.Toolkit;
    using Kistl.Client.Presentables.ValueViewModels;

    public abstract class InstanceGridBaseDisplay : UserControl, IHasViewModel<InstanceListViewModel>
    {
        public abstract DataGrid DataGrid { get; }

        #region Sort dependency properties
        public static readonly DependencyProperty SortPropertyNameProperty =
            DependencyProperty.RegisterAttached("SortPropertyName", typeof(string), typeof(InstanceGridDisplay));

        public static string GetSortPropertyName(DependencyObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            return (string)obj.GetValue(SortPropertyNameProperty);
        }
        #endregion

        /// <summary>
        /// Opens a new WorkspaceModel in its default view with the double clicked item opened.
        /// </summary>
        /// <param name="sender">the sender of this event, a <see cref="ListViewItem"/> is expected</param>
        /// <param name="e">the arguments of this event</param>
        protected void ObjectList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListViewItem;
            if (listItem == null)
            {
                return;
            }

            var dataObject = listItem.Content as DataObjectViewModel;
            if (dataObject == null)
            {
                return;
            }

            // only react to left mouse button double clicks
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            ViewModel.OnItemsDefaultAction(new DataObjectViewModel[] { dataObject });
            e.Handled = true;
        }

        private bool _selectedItemsChangedByViewModel = false;
        private bool _selectedItemsChangedByList = false;

        protected void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectedItemsChangedByViewModel) return;

            _selectedItemsChangedByList = true;
            try
            {
                if (e.OriginalSource == DataGrid)
                {
                    e.Handled = true;
                    e.RemovedItems.OfType<DataObjectViewModelProxy>().ForEach(i => ViewModel.SelectedProxies.Remove(i));
                    e.AddedItems.OfType<DataObjectViewModelProxy>().ForEach(i => ViewModel.SelectedProxies.Add(i, true));
                }
            }
            finally
            {
                _selectedItemsChangedByList = false;
            }
        }

        void ViewModel_SelectedProxies_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_selectedItemsChangedByList) return;

            _selectedItemsChangedByViewModel = true;
            try
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                {
                    DataGrid.SelectedItems.Clear();
                }
                else
                {
                    if (e.OldItems != null) e.OldItems.ForEach<object>(i => DataGrid.SelectedItems.Remove(i));
                    if (e.NewItems != null) e.NewItems.ForEach<object>(i => DataGrid.SelectedItems.Add(i));
                }
            }
            finally
            {
                _selectedItemsChangedByViewModel = false;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                if (ViewModel.ViewMethod == InstanceListViewMethod.Details)
                {
                    WPFHelper.RefreshGridView(DataGrid, ViewModel.DisplayedColumns, SortPropertyNameProperty);
                }
                // Attach to selection changed event on ViewModel side
                ViewModel.SelectedProxies.CollectionChanged += ViewModel_SelectedProxies_CollectionChanged;
                ViewModel.UpdateFromUI += new EventHandler(ViewModel_UpdateFromUI);
            }
        }

        void ViewModel_UpdateFromUI(object sender, EventArgs e)
        {
            WPFHelper.MoveFocus();
        }

        #region HeaderClickManagement
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        protected void ListView_HeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    var propName = GetSortPropertyName(headerClicked.Column);
                    if (string.IsNullOrEmpty(propName)) return;

                    ListSortDirection direction;
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        direction = _lastDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    }

                    ViewModel.Sort(propName, direction);

                    // Add arrow
                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          TryFindResource("GridHeaderTemplateArrowUp") as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          TryFindResource("GridHeaderTemplateArrowDown") as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }


                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }
        #endregion

        protected void RefreshCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.ReloadInstances();
        }

        #region IHasViewModel<DataTypeViewModel> Members

        public InstanceListViewModel ViewModel
        {
            get { return (InstanceListViewModel)DataContext; }
        }

        #endregion
    }
}
