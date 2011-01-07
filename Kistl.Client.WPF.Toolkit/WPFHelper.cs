using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Windows.Controls;
using Kistl.Client.Models;
using System.Windows.Controls;
using Kistl.Client.WPF.View;

namespace Kistl.Client.WPF.Toolkit
{
    public static class WPFHelper
    {
        /// <summary>
        /// Don't ask. WPF isn't able to handle FocusLost in an acceptable, simple way
        /// </summary>
        public static void MoveFocus()
        {
            // Gets the element with keyboard focus.
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            // Change keyboard focus.
            if (elementWithFocus != null)
            {             
                // Causes infinite loops with error validation
                // elementWithFocus.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        public static void RefreshGridView(DataGrid lst, GridDisplayConfiguration cfg, DependencyProperty sortProperty)
        {
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
                if (sortProperty != null) col.SetValue(sortProperty, desc.Name);

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
                editorFactory.SetValue(ContentPresenter.ContentTemplateSelectorProperty, lst.FindResource("defaultTemplateSelector"));
                editorFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);

                labelFactory.SetValue(VisualTypeTemplateSelector.RequestedKindProperty, desc.GridPreEditKind);
                labelFactory.SetValue(ContentPresenter.ContentTemplateSelectorProperty, lst.FindResource("defaultTemplateSelector"));
                labelFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);

                col.CellTemplate = new DataTemplate() { VisualTree = labelFactory };
                col.CellEditingTemplate = new DataTemplate() { VisualTree = editorFactory };
                lst.Columns.Add(col);
            }
        }

        public static void RefreshGridView(ListView lst, GridDisplayConfiguration cfg, DependencyProperty sortProperty)
        {
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
                if (sortProperty != null) col.SetValue(sortProperty, desc.Name);

                DataTemplate result = new DataTemplate();
                var cpFef = new FrameworkElementFactory(typeof(ContentPresenter));
                switch (desc.Type)
                {
                    case ColumnDisplayModel.ColumnType.MethodModel:
                        cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(String.Format("ActionViewModelsByName[{0}]", desc.Name)), Mode = BindingMode.OneWay });
                        break;
                    case ColumnDisplayModel.ColumnType.PropertyModel:
                        {
                            var tmp = desc.Name.Split('.').Select(i => String.Format("PropertyModelsByName[{0}]", i));
                            var binding = string.Join(".Value.", tmp.ToArray());
                            cpFef.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath(binding), Mode = BindingMode.OneWay });
                            break;
                        }
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
    } 
}
