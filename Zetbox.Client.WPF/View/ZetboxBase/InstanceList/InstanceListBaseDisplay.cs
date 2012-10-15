// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
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
    using Zetbox.Client.Presentables.GUI;

    public abstract class InstanceListBaseDisplay : InstanceCollectionBase
    {
        public abstract ListView ListView { get; }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                ViewModel.PropertyChanged += (s, pce) => 
                {
                    if (pce.PropertyName == "ViewMethod") ApplyViewMethod();
                    if (pce.PropertyName == "DisplayedColumns")
                    {
                        ViewModel.DisplayedColumns.Columns.CollectionChanged += (sncc, ncc) => ApplyColumns();
                        ApplyColumns();
                    }
                };
                ViewModel.DisplayedColumns.Columns.CollectionChanged += (s, ncc) => ApplyColumns();
                ApplyViewMethod();
                this.ApplyIsBusyBehaviour(ViewModel);
                ViewModel.SavedListConfigurations.GetColumnInformation += new SavedListConfiguratorViewModel.GetColumnInformationEventHandler(SavedListConfigurations_GetColumnInformation);
            }
        }

        List<SavedListConfiguratorViewModel.ColumnInformation> SavedListConfigurations_GetColumnInformation()
        {
            var grid = ListView.View as GridView;
            if (grid != null)
            {
                return grid.Columns
                    .Select(i => new SavedListConfiguratorViewModel.ColumnInformation() { Path = WPFHelper.GetGridColMemberSourcePath(i), Width = (int)i.ActualWidth }).ToList();
            }
            else
            {
                return null;
            }
        }
        
        private void ApplyColumns()
        {
            if(ViewModel != null && ListView != null)
                WPFHelper.RefreshGridView(ListView, ViewModel.DisplayedColumns, WpfSortHelper.SortPropertyNameProperty);
        }

        private void ApplyViewMethod()
        {
            if (ViewModel.ViewMethod == Zetbox.App.GUI.InstanceListViewMethod.Details)
            {
                ApplyColumns();
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
