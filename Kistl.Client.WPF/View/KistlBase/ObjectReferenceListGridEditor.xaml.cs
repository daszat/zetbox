
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
    public partial class ObjectReferenceListGridEditor
        : PropertyEditor, IHasViewModel<ObjectListViewModel>
    {
        public ObjectReferenceListGridEditor()
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
                        if (ViewModel.SelectedItem != null)
                        {
                            ViewModel.MoveItemUp(ViewModel.SelectedItem);
                        }
                    },
                    param =>
                    {
                        return ViewModel.SelectedItem != null;
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
                        if (ViewModel.SelectedItem != null)
                        {
                            ViewModel.MoveItemDown(ViewModel.SelectedItem);
                        }
                    },
                    param =>
                    {
                        return ViewModel.SelectedItem != null;
                    });
                }
                return _moveDownCommand;
            }
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
            GridDisplayConfiguration cfg = ViewModel.DisplayedColumns;
            if (cfg.ShowIcon)
            {
                lst.Columns.Add(new DataGridTemplateColumn() { CellTemplate = (DataTemplate)FindResource("iconCellTemplate") });
            }

            if (cfg.ShowId)
            {
                lst.Columns.Add(new DataGridTemplateColumn() { CellTemplate = (DataTemplate)FindResource("idCellTemplate"), Header = "ID" });
            }

            if (cfg.ShowName)
            {
                lst.Columns.Add(new DataGridTemplateColumn() { CellTemplate = (DataTemplate)FindResource("nameCellTemplate"), Header = "Name" });
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
                
                var col = new DataGridTemplateColumn() { Header = desc };
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

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource == lst)
            {
                e.Handled = true;
                e.RemovedItems.ForEach<DataObjectViewModel>(i => ViewModel.SelectedItems.Remove(i));
                e.AddedItems.ForEach<DataObjectViewModel>(i => ViewModel.SelectedItems.Add(i));
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
