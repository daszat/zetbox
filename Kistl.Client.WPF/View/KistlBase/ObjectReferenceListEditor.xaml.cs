
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

    /// <summary>
    /// Interaction logic for DataObjectListView.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class ObjectReferenceListEditor
        : PropertyEditor, IHasViewModel<ObjectListViewModel>
    {
        public ObjectReferenceListEditor()
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
                view.Columns.Add(new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("idCellTemplate"), Header = "ID" });
            }

            if (cfg.ShowName)
            {
                view.Columns.Add(new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("nameCellTemplate"), Header = "Name" });
            }

            foreach (var desc in cfg.Columns)
            {
                // TODO: use default controls after moving labeling to infrastructure
                var col = new GridViewColumn() { Header = desc };

                DataTemplate result = new DataTemplate();
                var cpFef = new FrameworkElementFactory(typeof(ContentPresenter));
                switch (desc.Type)
                {
                    case ColumnDisplayModel.ColumnType.MethodModel:
                        cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("ActionViewModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        break;
                    case ColumnDisplayModel.ColumnType.PropertyModel:
                        cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("PropertyModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        break;
                    case ColumnDisplayModel.ColumnType.Property:
                        cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(desc.Name), Mode = BindingMode.OneWay });
                        break;
                }
                cpFef.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.ControlKind);
                cpFef.SetValue(ContentPresenter.ContentTemplateSelectorProperty, FindResource("defaultTemplateSelector"));
                result.VisualTree = cpFef;
                col.CellTemplate = result;
                view.Columns.Add(col);
            }

        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                RefreshGridView();
                // Attach to selection changed event on ViewModel side
                ViewModel.SelectedItems.CollectionChanged += ViewModel_SelectedItems_CollectionChanged;
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
                    e.RemovedItems.ForEach<DataObjectViewModel>(i => ViewModel.SelectedItems.Remove(i));
                    e.AddedItems.ForEach<DataObjectViewModel>(i => ViewModel.SelectedItems.Add(i));
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
