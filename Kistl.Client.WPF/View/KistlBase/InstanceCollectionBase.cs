
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

    public abstract class InstanceCollectionBase : UserControl, IHasViewModel<InstanceListViewModel>
    {
        protected abstract void SetHeaderTemplate(DependencyObject header, DataTemplate template);

        public InstanceCollectionBase()
        {
        }

        #region Sort dependency properties
        public static readonly DependencyProperty SortPropertyNameProperty =
            DependencyProperty.RegisterAttached("SortPropertyName", typeof(string), typeof(InstanceListDisplay));

        public static string GetSortPropertyName(DependencyObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            return (string)obj.GetValue(SortPropertyNameProperty);
        }

        public static void SetSortPropertyName(DependencyObject obj, string value)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            obj.SetValue(SortPropertyNameProperty, value);
        }
        #endregion

        #region ItemActivatedHandler
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
        #endregion

        #region Sorting management
        DependencyObject _lastColumnClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        protected void ApplyInitialSortTemplates(DependencyObject column)
        {
            _lastColumnClicked = column;
            _lastDirection = ViewModel.SortDirection;
            if (_lastColumnClicked != null)
            {
                // Add arrow
                if (ViewModel.SortDirection == ListSortDirection.Ascending)
                {
                    SetHeaderTemplate(_lastColumnClicked, TryFindResource("GridHeaderTemplateArrowUp") as DataTemplate);
                }
                else
                {
                    SetHeaderTemplate(_lastColumnClicked, TryFindResource("GridHeaderTemplateArrowDown") as DataTemplate);
                }
            }
        }

        protected void ApplySortHeaderTemplate(DependencyObject currentColum)
        {
            var propName = GetSortPropertyName(currentColum);
            if (string.IsNullOrEmpty(propName)) return;

            ListSortDirection direction;
            if (currentColum != _lastColumnClicked)
            {
                direction = ListSortDirection.Ascending;
            }
            else
            {
                direction = _lastDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }

            ViewModel.Sort(propName, direction);

            // Remove arrow from previously sorted header
            if (_lastColumnClicked != null && _lastColumnClicked != currentColum)
            {
                SetHeaderTemplate(_lastColumnClicked, null);
            }

            // Save
            _lastColumnClicked = currentColum;
            _lastDirection = direction;

            // Add arrow
            if (ViewModel.SortDirection == ListSortDirection.Ascending)
            {
                SetHeaderTemplate(_lastColumnClicked, TryFindResource("GridHeaderTemplateArrowUp") as DataTemplate);
            }
            else
            {
                SetHeaderTemplate(_lastColumnClicked, TryFindResource("GridHeaderTemplateArrowDown") as DataTemplate);
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
