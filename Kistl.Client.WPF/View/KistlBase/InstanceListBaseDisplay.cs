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

    public abstract class InstanceListBaseDisplay : UserControl, IHasViewModel<InstanceListViewModel>
    {
        public abstract ListView ListView { get; }

        #region Sort dependency properties
        public static readonly DependencyProperty SortPropertyNameProperty =
            DependencyProperty.RegisterAttached("SortPropertyName", typeof(string), typeof(InstanceListDisplay));

        public static string GetSortPropertyName(GridViewColumn obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            return (string)obj.GetValue(SortPropertyNameProperty);
        }

        public static void SetSortPropertyName(GridViewColumn obj, string value)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            obj.SetValue(SortPropertyNameProperty, value);
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
                if (ViewModel.ViewMethod == InstanceListViewMethod.Details)
                {
                    WPFHelper.RefreshGridView(ListView, ViewModel.DisplayedColumns, SortPropertyNameProperty);
                    ListView.ItemContainerStyle = Application.Current.Resources["ListViewAsGridViewItemContainerStyle"] as Style;
                }

                ViewModel.PropertyChanged += new PropertyChangedEventHandler(ViewModel_PropertyChanged);
            }
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsBusy":
                    var child = LogicalTreeHelper.GetChildren(this).OfType<FrameworkElement>().FirstOrDefault();
                    if (child != null)
                        if (ViewModel.IsBusy)
                        {
                            ContentAdorner.ShowWaitDialog(child);
                        }
                        else
                        {
                            ContentAdorner.HideWaitDialog(child);
                        }
                    break;
            }
        }

        #region HeaderClickManagement
        GridViewColumnHeader _lastHeaderClicked = null;
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
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        direction = _lastDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    }

                    ViewModel.Sort(propName, direction);

                    // Add arrow
                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          TryFindResource("GridHeaderTemplateArrowUp") as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          TryFindResource("GridHeaderTemplateArrowDown") as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }


                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }
        #endregion

        protected void RefreshCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.ReloadInstances();
        }

        #region IHasViewModel<DataTypeViewModel> Members

        public InstanceListViewModel ViewModel
        {
            get { return (InstanceListViewModel)DataContext; }
        }

        #endregion
    }
}
