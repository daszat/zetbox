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
    using Kistl.Client.WPF.Toolkit;
    using Kistl.Client.WPF.CustomControls;

    public abstract class InstanceListBaseDisplay : InstanceCollectionBase
    {
        public abstract ListView ListView { get; }

        protected override void SetHeaderTemplate(DependencyObject header, DataTemplate template)
        {
            header.SetValue(GridViewColumn.HeaderTemplateProperty, template);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                ViewModel.PropertyChanged += (s, pce) => { if (pce.PropertyName == "ViewMethod") ApplyViewMethod(); };
                ApplyViewMethod();
                this.ApplyIsBusyBehaviour(ViewModel);
            }
        }

        private void ApplyViewMethod()
        {
            if (ViewModel.ViewMethod == InstanceListViewMethod.Details)
            {
                WPFHelper.RefreshGridView(ListView, ViewModel.DisplayedColumns, SortPropertyNameProperty);
                ListView.ItemContainerStyle = Application.Current.Resources["ListViewAsGridViewItemContainerStyle"] as Style;

                ApplyInitialSortTemplates(((GridView)ListView.View).Columns.FirstOrDefault(i => GetSortPropertyName(i) == ViewModel.SortProperty));
            }
            else
            {
                ListView.ItemContainerStyle = Application.Current.Resources["ListViewItemContainerStyle"] as Style;
                ListView.View = null; // ??
            }
        }

        protected void ListView_HeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader header = e.OriginalSource as GridViewColumnHeader;

            if (header != null && header.Role != GridViewColumnHeaderRole.Padding)
            {
                ApplySortHeaderTemplate(header.Column);
            }
        }
    }
}
