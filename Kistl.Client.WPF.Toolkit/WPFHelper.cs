
namespace Kistl.Client.WPF.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using Kistl.Client.Models;
    using Kistl.Client.WPF.Toolkit;
    using Microsoft.Windows.Controls;
    using Kistl.Client.Presentables;
    using Kistl.Client.WPF.CustomControls;

    public static class WPFHelper
    {
        /// <summary>
        /// http://stackoverflow.com/questions/980120/finding-control-within-wpf-itemscontrol
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static T FindVisualChild<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }

        public static void RefreshGridView(DataGrid lst, GridDisplayConfiguration cfg, DependencyProperty sortProperty)
        {
            cfg.Columns.CollectionChanged += (s, e) => RefreshGridView(lst, cfg, sortProperty);

            lst.Columns.Clear();
            if (cfg.ShowIcon)
            {
                lst.Columns.Add(new DataGridTemplateColumn() { CellTemplate = (DataTemplate)lst.FindResource("iconCellTemplate") });
            }

            if (cfg.ShowId)
            {
                var col = new DataGridTemplateColumn() { CellTemplate = (DataTemplate)lst.FindResource("idCellTemplate"), Header = "ID" };
                lst.Columns.Add(col);
                // SetSortPropertyName(col, "ID");
            }

            if (cfg.ShowName)
            {
                var col = new DataGridTemplateColumn() { CellTemplate = (DataTemplate)lst.FindResource("nameCellTemplate"), Header = "Name" };
                lst.Columns.Add(col);
                // Not possible
                // SetSortPropertyName(col, "Name");               
            }

            foreach (var desc in cfg.Columns)
            {
                // TODO: use default controls after moving labeling to infrastructure
                var col = new DataGridTemplateColumn() { Header = desc.Header };
                if (desc.RequestedWidth > 0) col.Width = desc.RequestedWidth;

                var needEditor = desc.ControlKind != desc.GridPreEditKind;

                var editorFactory = needEditor ? new FrameworkElementFactory(typeof(ContentPresenter)) : null;
                var labelFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                switch (desc.Type)
                {
                    case ColumnDisplayModel.ColumnType.MethodModel:
                        if (needEditor) editorFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("Object.ActionViewModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        labelFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("Object.ActionViewModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        break;
                    case ColumnDisplayModel.ColumnType.PropertyModel:
                        {
                            // Only database properties can be sorted
                            // TODO: On the client side, maybe also Calculated Properties and ViewModels Properties
                            if (sortProperty != null) col.SetValue(sortProperty, desc.Name);

                            var tmp = desc.Name.Split('.').Select(i => String.Format("PropertyModelsByName[{0}]", i));
                            var binding = "Object." + string.Join(".Value.", tmp.ToArray());
                            if (needEditor) editorFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(binding), Mode = BindingMode.OneWay });
                            labelFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(binding), Mode = BindingMode.OneWay });
                            break;
                        }
                    case ColumnDisplayModel.ColumnType.CalculatedProperty:
                        if (needEditor) editorFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("Object.CalculatedPropertyModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        labelFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("Object.CalculatedPropertyModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        break;
                    case ColumnDisplayModel.ColumnType.Property:
                        if (needEditor) editorFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath("Object." + desc.Name), Mode = BindingMode.OneWay });
                        labelFactory.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath("Object." + desc.Name), Mode = BindingMode.OneWay });
                        break;
                }
                if (needEditor)
                {
                    editorFactory.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.ControlKind);
                    editorFactory.SetValue(ContentPresenter.ContentTemplateSelectorProperty, lst.FindResource("defaultTemplateSelector"));
                    editorFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                }

                labelFactory.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.GridPreEditKind);
                labelFactory.SetValue(ContentPresenter.ContentTemplateSelectorProperty, lst.FindResource("defaultTemplateSelector"));
                labelFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);

                col.CellTemplate = new DataTemplate() { VisualTree = labelFactory };
                // set editing template only if different
                if (needEditor)
                {
                    col.CellEditingTemplate = new DataTemplate() { VisualTree = editorFactory };
                }
                lst.Columns.Add(col);
            }
        }

        public static void RefreshGridView(ListView lst, GridDisplayConfiguration cfg, DependencyProperty sortProperty)
        {
            cfg.Columns.CollectionChanged += (s, e) => RefreshGridView(lst, cfg, sortProperty);

            GridView view = new GridView() { AllowsColumnReorder = true };
            lst.View = view;
            if (cfg.ShowIcon)
            {
                view.Columns.Add(new GridViewColumn() { CellTemplate = (DataTemplate)lst.FindResource("iconCellTemplate") });
            }

            if (cfg.ShowId)
            {
                var col = new GridViewColumn() { CellTemplate = (DataTemplate)lst.FindResource("idCellTemplate"), Header = "ID" };
                view.Columns.Add(col);
                // SetSortPropertyName(col, "ID");
            }

            if (cfg.ShowName)
            {
                var col = new GridViewColumn() { CellTemplate = (DataTemplate)lst.FindResource("nameCellTemplate"), Header = "Name" };
                view.Columns.Add(col);
                // Not possible
                // SetSortPropertyName(col, "Name");               
            }

            foreach (var desc in cfg.Columns)
            {
                // TODO: use default controls after moving labeling to infrastructure
                var col = new GridViewColumn() { Header = desc.Header };
                if (desc.RequestedWidth > 0) col.Width = desc.RequestedWidth;

                DataTemplate result = new DataTemplate();
                var cpFef = new FrameworkElementFactory(typeof(ContentPresenter));
                switch (desc.Type)
                {
                    case ColumnDisplayModel.ColumnType.MethodModel:
                        cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("ActionViewModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        break;
                    case ColumnDisplayModel.ColumnType.PropertyModel:
                        {
                            if (sortProperty != null) col.SetValue(sortProperty, desc.Name);

                            var tmp = desc.Name.Split('.').Select(i => String.Format("PropertyModelsByName[{0}]", i));
                            var binding = string.Join(".Value.", tmp.ToArray());
                            cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(binding), Mode = BindingMode.OneWay });
                            break;
                        }
                    case ColumnDisplayModel.ColumnType.CalculatedProperty:
                        cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("CalculatedPropertyModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        break;
                    case ColumnDisplayModel.ColumnType.Property:
                        cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(desc.Name), Mode = BindingMode.OneWay });
                        break;
                }
                cpFef.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.ControlKind);
                cpFef.SetValue(ContentPresenter.ContentTemplateSelectorProperty, lst.FindResource("defaultTemplateSelector"));
                cpFef.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                result.VisualTree = cpFef;
                col.CellTemplate = result;
                view.Columns.Add(col);
            }
        }

        public static void ApplyIsBusyBehaviour(this FrameworkElement ctrl, ViewModel mdl)
        {
            if (ctrl == null) throw new ArgumentNullException("ctrl");
            if (mdl == null) throw new ArgumentNullException("mdl");

            mdl.PropertyChanged += (sender, e) =>
            {
                switch (e.PropertyName)
                {
                    case "IsBusy":
                        if (mdl.IsBusy)
                        {
                            ContentAdorner.ShowWaitDialog(ctrl);
                        }
                        else
                        {
                            ContentAdorner.HideWaitDialog(ctrl);
                        }
                        break;
                }
            };
        }
    }
}
