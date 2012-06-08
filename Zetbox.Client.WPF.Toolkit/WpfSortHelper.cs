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

namespace Zetbox.Client.WPF.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.ComponentModel;
    using Zetbox.Client.Presentables;

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

            _viewModel.PropertyChanged += new PropertyChangedEventHandler(_viewModel_PropertyChanged);
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

        void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SortProperty" && string.IsNullOrEmpty(_viewModel.SortProperty) && _lastColumnClicked != null)
            {
                // Remove arrow from previously sorted header
                _setHeaderTemplate(_lastColumnClicked, null);
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
