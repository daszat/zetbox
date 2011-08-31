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
    using Kistl.Client.WPF.Toolkit;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Client.WPF.CustomControls;
    using Microsoft.Windows.Controls.Primitives;

    public abstract class InstanceGridBaseDisplay : InstanceCollectionBase
    {
        public abstract ZBoxDataGrid DataGrid { get; }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                WPFHelper.RefreshGridView(DataGrid, ViewModel.DisplayedColumns, WpfSortHelper.SortPropertyNameProperty);
                SortHelper.ApplyInitialSortTemplates(DataGrid.Columns.FirstOrDefault(i => WpfSortHelper.GetSortPropertyName(i) == ViewModel.SortProperty));
                this.ApplyIsBusyBehaviour(ViewModel);
            }
        }

        protected override void SetHeaderTemplate(DependencyObject header, DataTemplate template)
        {
            header.SetValue(DataGridColumn.HeaderTemplateProperty, template);
        }

        protected void ListView_HeaderClick(object sender, RoutedEventArgs e)
        {
            var header = e.OriginalSource as DataGridColumnHeader;
            if (header != null)
            {
                SortHelper.ApplySort(header.Column);
            }
        }
    }
}
