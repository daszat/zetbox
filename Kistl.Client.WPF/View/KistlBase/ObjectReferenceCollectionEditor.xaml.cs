
namespace Kistl.Client.WPF.View.KistlBase
{
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    using Kistl.API;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.WPF.Commands;
    using System.ComponentModel;

    /// <summary>
    /// Interaction logic for DataObjectListView.xaml
    /// </summary>
    [ViewDescriptor("KistlBase", Kistl.App.GUI.Toolkit.WPF, Kind = "Kistl.App.GUI.ObjectCollectionKind")]
    public partial class ObjectReferenceCollectionEditor
        : PropertyEditor, IHasViewModel<ObjectCollectionModel>
    {

        public static readonly DependencyProperty SortPropertyNameProperty =
            DependencyProperty.RegisterAttached("SortPropertyName", typeof(string), typeof(ObjectReferenceCollectionEditor));

        public static string GetSortPropertyName(GridViewColumn obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            return (string)obj.GetValue(SortPropertyNameProperty);
        }

        public static void SetSortPropertyName(GridViewColumn obj, string value)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            obj.SetValue(SortPropertyNameProperty, value);
        }

        public ObjectReferenceCollectionEditor()
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

        private void RefreshGridView()
        {
            GridView view = new GridView() { AllowsColumnReorder = true };
            lst.View = view;
            GridDisplayConfiguration cfg = ViewModel.DisplayedColumns;
            if (cfg.ShowIcon)
            {
                view.Columns.Add(new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("iconCellTemplate") });
            }

            if (cfg.ShowId)
            {
                var col = new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("idCellTemplate"), Header = "ID" };
                view.Columns.Add(col);
                SetSortPropertyName(col, "ID");
            }

            if (cfg.ShowName)
            {
                var col = new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("nameCellTemplate"), Header = "Name" };
                view.Columns.Add(col);
                // Not possible
                // SetSortPropertyName(col, "Name");               
            }

            foreach (var desc in cfg.Columns)
            {
                // TODO: use default controls after moving labeling to infrastructure
                var col = new GridViewColumn() { Header = desc.Header };
                SetSortPropertyName(col, desc.Name);

                DataTemplate result = new DataTemplate();
                var cpFef = new FrameworkElementFactory(typeof(ContentPresenter));
                if (desc.IsMethod)
                {
                    cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("ActionModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                }
                else
                {
                    cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("PropertyModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                }
                cpFef.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.ControlKind);
                cpFef.SetValue(ContentPresenter.ContentTemplateSelectorProperty, FindResource("defaultTemplateSelector"));
                result.VisualTree = cpFef;
                col.CellTemplate = result;
                view.Columns.Add(col);
            }

        }

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

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == FrameworkElement.DataContextProperty)
            {
                RefreshGridView();
            }
        }

        #region IHasViewModel<ObjectCollectionModel> Members

        public ObjectCollectionModel ViewModel
        {
            get { return (ObjectCollectionModel)DataContext; }
        }

        #endregion

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource == lst)
            {
                e.RemovedItems.ForEach<DataObjectModel>(i => ViewModel.SelectedItems.Remove(i));
                e.AddedItems.ForEach<DataObjectModel>(i => ViewModel.SelectedItems.Add(i));
            }
        }
    }
}
