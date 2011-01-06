
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
    public partial class ObjectReferenceCollectionGridEditor
        : PropertyEditor, IHasViewModel<ObjectCollectionViewModel>
    {
        #region Sort dependency properties
        public static readonly DependencyProperty SortPropertyNameProperty =
            DependencyProperty.RegisterAttached("SortPropertyName", typeof(string), typeof(ObjectReferenceCollectionGridEditor));

        public static string GetSortPropertyName(DependencyObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            return (string)obj.GetValue(SortPropertyNameProperty);
        }

        public static void SetSortPropertyName(DependencyObject obj, string value)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            obj.SetValue(SortPropertyNameProperty, value);
        }
        #endregion

        public ObjectReferenceCollectionGridEditor()
        {
            InitializeComponent();
        }

        #region Item Management
        private void ItemActivatedHandler(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel != null && ViewModel.SelectedItem != null)
            {
                ViewModel.ActivateItem(ViewModel.SelectedItem, true);
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
        #endregion


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                WPFHelper.RefreshGridView(lst, ViewModel.DisplayedColumns, SortPropertyNameProperty);
                // Attach to selection changed event on ViewModel side
                ViewModel.SelectedProxies.CollectionChanged += ViewModel_SelectedItems_CollectionChanged;
            }
        }


        #region HeaderClickManagement
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void ListView_HeaderClick(object sender, RoutedEventArgs e)
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

        #region IHasViewModel<ObjectCollectionModel> Members

        public ObjectCollectionViewModel ViewModel
        {
            get { return (ObjectCollectionViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return lst; }
        }
    }
}
