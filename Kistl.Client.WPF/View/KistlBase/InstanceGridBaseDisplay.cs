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

        public static void SetSortPropertyName(DependencyObject obj, string value)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            obj.SetValue(SortPropertyNameProperty, value);
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
                    e.RemovedItems.OfType<InstanceListViewModel.Proxy>().ForEach(i => ViewModel.SelectedProxies.Remove(i));
                    e.AddedItems.OfType<InstanceListViewModel.Proxy>().ForEach(i => ViewModel.SelectedProxies.Add(i, true));
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

        #region RefreshGridView
        protected void RefreshGridView()
        {
            GridDisplayConfiguration cfg = ViewModel.DisplayedColumns;
            if (cfg.ShowIcon)
            {
                DataGrid.Columns.Add(new DataGridTemplateColumn() { CellTemplate = (DataTemplate)FindResource("iconCellTemplate") });
            }

            if (cfg.ShowId)
            {
                var col = new DataGridTemplateColumn() { CellTemplate = (DataTemplate)FindResource("idCellTemplate"), Header = "ID" };
                DataGrid.Columns.Add(col);
                // SetSortPropertyName(col, "ID");
            }

            if (cfg.ShowName)
            {
                var col = new DataGridTemplateColumn() { CellTemplate = (DataTemplate)FindResource("nameCellTemplate"), Header = "Name" };
                DataGrid.Columns.Add(col);
                // Not possible
                // SetSortPropertyName(col, "Name");               
            }

            foreach (var desc in cfg.Columns)
            {
                // TODO: use default controls after moving labeling to infrastructure
                var col = new DataGridTemplateColumn() { Header = desc.Header };
                SetSortPropertyName(col, desc.Name);

                var editorFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                var labelFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                switch (desc.Type)
                {
                    case ColumnDisplayModel.ColumnType.MethodModel:
                        editorFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("Object.ActionViewModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        labelFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("Object.ActionViewModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        break;
                    case ColumnDisplayModel.ColumnType.PropertyModel:
                        {
                            var tmp = desc.Name.Split('.').Select(i => String.Format("PropertyModelsByName[{0}]", i));
                            var binding = "Object." + string.Join(".Value.", tmp.ToArray());
                            editorFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(binding), Mode = BindingMode.OneWay });
                            labelFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(binding), Mode = BindingMode.OneWay });
                            break;
                        }
                    case ColumnDisplayModel.ColumnType.Property:
                        editorFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath("Object." + desc.Name), Mode = BindingMode.OneWay });
                        labelFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath("Object." + desc.Name), Mode = BindingMode.OneWay });
                        break;
                }
                editorFactory.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.ControlKind);
                editorFactory.SetValue(ContentPresenter.ContentTemplateSelectorProperty, FindResource("defaultTemplateSelector"));
                editorFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);

                labelFactory.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.GridPreEditKind);
                labelFactory.SetValue(ContentPresenter.ContentTemplateSelectorProperty, FindResource("defaultTemplateSelector"));
                labelFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);

                col.CellTemplate = new DataTemplate() { VisualTree = labelFactory };
                col.CellEditingTemplate = new DataTemplate() { VisualTree = editorFactory }; 
                DataGrid.Columns.Add(col);
            }

        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                if (ViewModel.ViewMethod == InstanceListViewMethod.Details)
                {
                    RefreshGridView();
                }
                // Attach to selection changed event on ViewModel side
                ViewModel.SelectedProxies.CollectionChanged += ViewModel_SelectedProxies_CollectionChanged;
            }
        }
        #endregion

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

        #region IHasViewModel<DataTypeViewModel> Members

        public InstanceListViewModel ViewModel
        {
            get { return (InstanceListViewModel)DataContext; }
        }

        #endregion
    }
}
