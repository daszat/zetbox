
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
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.ActivateItem(ViewModel.SelectedItem, true);
            }
        }

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource == lst)
            {
                e.Handled = true;
                e.RemovedItems.ForEach<DataObjectViewModel>(i => ViewModel.SelectedItems.Remove(i));
                e.AddedItems.ForEach<DataObjectViewModel>(i => ViewModel.SelectedItems.Add(i));
            }
        }
        #endregion

        #region RefreshGridView
        private void RefreshGridView()
        {
            if (ViewModel == null) return;

            GridDisplayConfiguration cfg = ViewModel.DisplayedColumns;
            if (cfg.ShowIcon)
            {
                lst.Columns.Add(new DataGridTemplateColumn() { CellTemplate = (DataTemplate)FindResource("iconCellTemplate") });
            }

            if (cfg.ShowId)
            {
                var col = new DataGridTemplateColumn() { CellTemplate = (DataTemplate)FindResource("idCellTemplate"), Header = "ID" };
                lst.Columns.Add(col);
                SetSortPropertyName(col, "ID");
            }

            if (cfg.ShowName)
            {
                var col = new DataGridTemplateColumn() { CellTemplate = (DataTemplate)FindResource("nameCellTemplate"), Header = "Name" };
                lst.Columns.Add(col);
                // Not possible
                // SetSortPropertyName(col, "Name");               
            }

            foreach (var desc in cfg.Columns)
            {
                // TODO: use default controls after moving labeling to infrastructure
                var editorFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                var labelFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                switch (desc.Type)
                {
                    case ColumnDisplayModel.ColumnType.MethodModel:
                        editorFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("ActionViewModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        labelFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("ActionViewModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        break;
                    case ColumnDisplayModel.ColumnType.PropertyModel:
                        editorFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("PropertyModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        labelFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("PropertyModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        break;
                    case ColumnDisplayModel.ColumnType.Property:
                        editorFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(desc.Name), Mode = BindingMode.OneWay });
                        labelFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(desc.Name), Mode = BindingMode.OneWay });
                        break;
                }
                editorFactory.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.ControlKind);
                editorFactory.SetValue(ContentPresenter.ContentTemplateSelectorProperty, FindResource("defaultTemplateSelector"));
                editorFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);

                labelFactory.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.GridPreEditKind);
                labelFactory.SetValue(ContentPresenter.ContentTemplateSelectorProperty, FindResource("defaultTemplateSelector"));
                labelFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);

                var col = new DataGridTemplateColumn() { Header = desc.Header };
                SetSortPropertyName(col, desc.Name);
                col.CellTemplate = new DataTemplate() { VisualTree = labelFactory };
                col.CellEditingTemplate = new DataTemplate() { VisualTree = editorFactory };
                lst.Columns.Add(col);
            }

        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == FrameworkElement.DataContextProperty)
            {
                RefreshGridView();
            }
        }
        #endregion

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
