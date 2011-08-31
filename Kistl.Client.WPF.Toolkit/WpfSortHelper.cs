
namespace Kistl.Client.WPF.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.ComponentModel;
    using Kistl.Client.Presentables;

    public class WpfSortHelper : DependencyObject
    {
        #region Sort dependency properties
        public static readonly DependencyProperty SortPropertyNameProperty =
            DependencyProperty.RegisterAttached("SortPropertyName", typeof(string), typeof(WpfSortHelper));

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

        private readonly FrameworkElement _parent;
        private readonly ISortableViewModel _viewModel;
        public WpfSortHelper(FrameworkElement parent, ISortableViewModel viewModel, Action<DependencyObject, DataTemplate> setHeaderTemplate)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            if (viewModel == null) throw new ArgumentNullException("viewModel");
            if (setHeaderTemplate == null) throw new ArgumentNullException("setHeaderTemplate");

            _parent = parent;
            _viewModel = viewModel;
            _setHeaderTemplate = setHeaderTemplate;
        }

        private Action<DependencyObject, DataTemplate> _setHeaderTemplate;

        DependencyObject _lastColumnClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void ApplyHeaderTemplate()
        {
            if (_viewModel.SortDirection == ListSortDirection.Ascending)
            {
                _setHeaderTemplate(_lastColumnClicked, _parent.TryFindResource("GridHeaderTemplateArrowUp") as DataTemplate);
            }
            else
            {
                _setHeaderTemplate(_lastColumnClicked, _parent.TryFindResource("GridHeaderTemplateArrowDown") as DataTemplate);
            }
        }

        public void ApplyInitialSortTemplates(DependencyObject column)
        {
            _lastColumnClicked = column;
            _lastDirection = _viewModel.SortDirection;
            if (_lastColumnClicked != null)
            {
                ApplyHeaderTemplate();
            }
        }

        public void ApplySort(DependencyObject currentColum)
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

            _viewModel.Sort(propName, direction);

            // Remove arrow from previously sorted header
            if (_lastColumnClicked != null && _lastColumnClicked != currentColum)
            {
                _setHeaderTemplate(_lastColumnClicked, null);
            }

            // Save
            _lastColumnClicked = currentColum;
            _lastDirection = direction;

            ApplyHeaderTemplate();
        }
    }
}
