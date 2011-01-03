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
using Kistl.Client.Presentables.ValueViewModels;
using Kistl.Client.Models;

namespace Kistl.Client.WPF.View.KistlBase
{
    /// <summary>
    /// Interaction logic for ObjectReferenceEditor.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class ObjectReferenceDropdownEditor : PropertyEditor, IHasViewModel<ObjectReferenceViewModel>
    {
        public ObjectReferenceDropdownEditor()
        {
            InitializeComponent();
        }

        #region RefreshGridView
        //private void RefreshGridView()
        //{
        //    if (ViewModel == null) return;

        //    ListView lst = cbValue.FindChild<ListView>("lst");
        //    if (lst == null) return; // not bound yet

        //    GridView view = new GridView() { AllowsColumnReorder = true };
        //    lst.View = view;
        //    GridDisplayConfiguration cfg = ViewModel.DisplayedColumns;
        //    if (cfg.ShowIcon)
        //    {
        //        view.Columns.Add(new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("iconCellTemplate") });
        //    }

        //    if (cfg.ShowId)
        //    {
        //        var col = new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("idCellTemplate"), Header = "ID" };
        //        view.Columns.Add(col);
        //    }

        //    if (cfg.ShowName)
        //    {
        //        var col = new GridViewColumn() { CellTemplate = (DataTemplate)FindResource("nameCellTemplate"), Header = "Name" };
        //        view.Columns.Add(col);
        //    }

        //    foreach (var desc in cfg.Columns)
        //    {
        //        // TODO: use default controls after moving labeling to infrastructure
        //        var col = new GridViewColumn() { Header = desc.Header };

        //        DataTemplate result = new DataTemplate();
        //        var cpFef = new FrameworkElementFactory(typeof(ContentPresenter));
        //        switch (desc.Type)
        //        {
        //            case ColumnDisplayModel.ColumnType.MethodModel:
        //                cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("ActionViewModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
        //                break;
        //            case ColumnDisplayModel.ColumnType.PropertyModel:
        //                cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("PropertyModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
        //                break;
        //            case ColumnDisplayModel.ColumnType.Property:
        //                cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(desc.Name), Mode = BindingMode.OneWay });
        //                break;
        //        }
        //        cpFef.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.ControlKind);
        //        cpFef.SetValue(ContentPresenter.ContentTemplateSelectorProperty, FindResource("defaultTemplateSelector"));
        //        result.VisualTree = cpFef;
        //        col.CellTemplate = result;
        //        view.Columns.Add(col);
        //    }

        //}

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            //if (e.Property == FrameworkElement.DataContextProperty)
            //{
            //    RefreshGridView();
            //}
        }
        #endregion

        #region IHasViewModel<ObjectReferenceViewModel> Members

        public ObjectReferenceViewModel ViewModel
        {
            get { return (ObjectReferenceViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return cbValue; }
        }
    }
}
