namespace Zetbox.Client.WPF.View.ZetboxBase
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
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.Base;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.Client.WPF.Toolkit;
    using Zetbox.Client.WPF.CustomControls;

    public abstract class InstanceListBaseDisplay : InstanceCollectionBase
    {
        public abstract ListView ListView { get; }

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
            if (ViewModel.ViewMethod == Zetbox.App.GUI.InstanceListViewMethod.Details)
            {
                WPFHelper.RefreshGridView(ListView, ViewModel.DisplayedColumns, WpfSortHelper.SortPropertyNameProperty);
                ListView.ItemContainerStyle = Application.Current.Resources["ListViewAsGridViewItemContainerStyle"] as Style;

                SortHelper.ApplyInitialSortTemplates(((GridView)ListView.View).Columns.FirstOrDefault(i => WpfSortHelper.GetSortPropertyName(i) == ViewModel.SortProperty));
            }
            else
            {
                ListView.ItemContainerStyle = Application.Current.Resources["ListViewItemContainerStyle"] as Style;
                ListView.View = null; // ??
            }
        }

        protected override void SetHeaderTemplate(DependencyObject header, DataTemplate template)
        {
            header.SetValue(GridViewColumn.HeaderTemplateProperty, template);
        }

        protected void ListView_HeaderClick(object sender, RoutedEventArgs e)
        {
            var header = e.OriginalSource as GridViewColumnHeader;
            if (header != null && header.Role != GridViewColumnHeaderRole.Padding)
            {
                SortHelper.ApplySort(header.Column);
            }
            e.Handled = true;
        }
    }
}
