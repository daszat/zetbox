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

    public abstract class InstanceGridBaseDisplay : UserControl, IHasViewModel<InstanceListViewModel>
    {
        public abstract ZBoxDataGrid DataGrid { get; }

        #region Sort dependency properties
        public static readonly DependencyProperty SortPropertyNameProperty =
            DependencyProperty.RegisterAttached("SortPropertyName", typeof(string), typeof(InstanceGridDisplay));

        public static string GetSortPropertyName(DependencyObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            return (string)obj.GetValue(SortPropertyNameProperty);
        }
        #endregion

        /// <summary>
        /// Opens a new WorkspaceModel in its default view with the double clicked item opened.
        /// </summary>
        /// <param name="sender">the sender of this event, a <see cref="ListViewItem"/> is expected</param>
        /// <param name="e">the arguments of this event</param>
        protected void ItemActivatedHandler(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null && ViewModel.SelectedItem != null)
            {
                ViewModel.OnItemsDefaultAction(new DataObjectViewModel[] { ViewModel.SelectedItem });
            }

            e.Handled = true;
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                WPFHelper.RefreshGridView(DataGrid, ViewModel.DisplayedColumns, SortPropertyNameProperty);
                var sortCol = DataGrid.Columns.FirstOrDefault(i => GetSortPropertyName(i) == ViewModel.SortProperty);
                _lastDirection = ViewModel.SortDirection;
                if (sortCol != null)
                {
                    // Add arrow
                    if (ViewModel.SortDirection == ListSortDirection.Ascending)
                    {
                        sortCol.HeaderTemplate =
                          TryFindResource("GridHeaderTemplateArrowUp") as DataTemplate;
                    }
                    else
                    {
                        sortCol.HeaderTemplate =
                          TryFindResource("GridHeaderTemplateArrowDown") as DataTemplate;
                    }
                }

                this.ApplyIsBusyBehaviour(ViewModel);
            }
        }

        #region HeaderClickManagement
        GridViewColumn _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        protected void ListView_HeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    var propName = GetSortPropertyName(headerClicked.Column);
                    if (string.IsNullOrEmpty(propName)) return;

                    ListSortDirection direction;
                    if (headerClicked.Column != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        direction = _lastDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    }

                    ViewModel.Sort(propName, direction);

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked.Column)
                    {
                        _lastHeaderClicked.HeaderTemplate = null;
                    }

                    // Save
                    _lastHeaderClicked = headerClicked.Column;
                    _lastDirection = direction;

                    // Add arrow
                    if (direction == ListSortDirection.Ascending)
                    {
                        _lastHeaderClicked.HeaderTemplate =
                          TryFindResource("GridHeaderTemplateArrowUp") as DataTemplate;
                    }
                    else
                    {
                        _lastHeaderClicked.HeaderTemplate =
                          TryFindResource("GridHeaderTemplateArrowDown") as DataTemplate;
                    }
                }
            }
        }
        #endregion

        protected void RefreshCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.ReloadInstances();
        }

        #region IHasViewModel<InstanceListViewModel> Members

        public InstanceListViewModel ViewModel
        {
            get { return (InstanceListViewModel)DataContext; }
        }

        #endregion
    }
}
