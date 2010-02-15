
namespace Kistl.Client.WPF.View
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

    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.WPF.Commands;

    /// <summary>
    /// Interaction logic for DataObjectListView.xaml
    /// </summary>
    public partial class DataObjectListView
        : PropertyView
    {
        public DataObjectListView()
        {
            InitializeComponent();
        }

        private RelayCommand _moveUpCommand;
        public System.Windows.Input.ICommand MoveUpCommand
        {
            get
            {
                if (_moveUpCommand == null)
                {
                    _moveUpCommand = new RelayCommand(param =>
                    {
                        var model = (ObjectListModel)DataContext;
                        if (model.SelectedItem != null)
                        {
                            model.MoveItemUp(model.SelectedItem);
                        }
                    },
                    param =>
                    {
                        var model = (ObjectListModel)DataContext;
                        return model.SelectedItem != null;
                    });
                }
                return _moveUpCommand;
            }
        }

        private RelayCommand _moveDownCommand;
        public System.Windows.Input.ICommand MoveDownCommand
        {
            get
            {
                if (_moveDownCommand == null)
                {
                    _moveDownCommand = new RelayCommand(param =>
                    {
                        var model = (ObjectListModel)DataContext;
                        if (model.SelectedItem != null)
                        {
                            model.MoveItemDown(model.SelectedItem);
                        }
                    },
                    param =>
                    {
                        var model = (ObjectListModel)DataContext;
                        return model.SelectedItem != null;
                    });
                }
                return _moveDownCommand;
            }
        }

        private void AddNewHandler(object sender, RoutedEventArgs e)
        {
            var model = (ObjectListModel)DataContext;
            model.CreateNewItem(newitem =>
            {
                if (newitem != null)
                {
                    model.AddItem(newitem);
                    model.SelectedItem = newitem;
                    model.ActivateItem(model.SelectedItem, true);
                }
            });
        }

        private void AddExistingItemHandler(object sender, RoutedEventArgs e)
        {
            var model = (ObjectListModel)DataContext;
            model.AddExistingItem();
        }

        private void RemoveHandler(object sender, RoutedEventArgs e)
        {
            var model = (ObjectListModel)DataContext;
            if (model.SelectedItem != null)
            {
                model.RemoveItem(model.SelectedItem);
            }
        }

        private void DeleteHandler(object sender, RoutedEventArgs e)
        {
            var model = (ObjectListModel)DataContext;
            if (model.SelectedItem != null)
            {
                model.DeleteItem(model.SelectedItem);
            }
        }

        private void ItemActivatedHandler(object sender, MouseButtonEventArgs e)
        {
            var model = (ObjectListModel)DataContext;
            if (model.SelectedItem != null)
            {
                model.ActivateItem(model.SelectedItem, true);
            }
        }

        private void RefreshGridView(ObjectListModel model)
        {
            GridView view = new GridView() { AllowsColumnReorder = true };
            ListView.View = view;
            GridDisplayConfiguration cfg = model.DisplayedColumns;
            if (cfg.ShowIcon)
            {
                view.Columns.Add(new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("iconCellTemplate") });
            }

            if (cfg.ShowId)
            {
                view.Columns.Add(new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("idCellTemplate"), Header = "ID" });
            }

            if (cfg.ShowName)
            {
                view.Columns.Add(new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("nameCellTemplate"), Header = "Name" });
            }

            foreach (var desc in cfg.Columns)
            {
                // TODO: use default controls after moving labeling to infrastructure
                var col = new GridViewColumn() { Header = desc.Header };

                DataTemplate result = new DataTemplate();
                var cpFef = new FrameworkElementFactory(typeof(ContentPresenter));
                cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("PropertyModelsByName[{0}]", desc.PropertyName)), Mode = BindingMode.OneWay });
                cpFef.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.ControlKind);
                cpFef.SetValue(ContentPresenter.ContentTemplateSelectorProperty, FindResource("defaultTemplateSelector"));
                result.VisualTree = cpFef;
                col.CellTemplate = result;
                col.Width = desc.ControlKind.RequestedWidth ?? Double.NaN;
                view.Columns.Add(col);
            }

        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == FrameworkElement.DataContextProperty && e.NewValue is ObjectListModel)
            {
                var model = (ObjectListModel)DataContext;
                RefreshGridView(model);
            }
        }

        private void ListView_Initialized(object sender, EventArgs e)
        {
            if (DataContext is ObjectListModel)
            {
                var model = (ObjectListModel)DataContext;
                RefreshGridView(model);
            }
        }
    }
}
