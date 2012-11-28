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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Zetbox.API;

namespace Zetbox.Client.WPF.CustomControls
{
    public class ZetboxListView : ListView
    {
        #region SelectionChanged
        public object SelectedZetboxItems
        {
            get { return (object)GetValue(SelectedZetboxItemsProperty); }
            set { SetValue(SelectedZetboxItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedZetboxItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedZetboxItemsProperty =
            DependencyProperty.Register("SelectedZetboxItems", typeof(object), typeof(ZetboxListView), new UIPropertyMetadata(null, new PropertyChangedCallback(OnSelectedZetboxItemsChanged)));

        public static void OnSelectedZetboxItemsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ZetboxListView)
            {
                ((ZetboxListView)obj).AttachSelectedZetboxItemsCollectionChanged();
            }
        }

        private void AttachSelectedZetboxItemsCollectionChanged()
        {
            if (SelectedZetboxItems is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)SelectedZetboxItems).CollectionChanged += new NotifyCollectionChangedEventHandler(ZetboxDataGrid_CollectionChanged);
                try
                {
                    _selectedItemsChangedByViewModel = true;
                    ((IEnumerable)SelectedZetboxItems).ForEach<object>(i => this.SelectedItems.Add(i));
                }
                finally
                {
                    _selectedItemsChangedByViewModel = false;
                }
            }
        }

        private bool _selectedItemsChangedByViewModel = false;
        private bool _selectedItemsChangedByList = false;

        private void ZetboxDataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_selectedItemsChangedByList) return;

            _selectedItemsChangedByViewModel = true;
            try
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                {
                    // do not touch SelectedItems in Single SelectionMode
                    if (this.SelectionMode == SelectionMode.Single)
                    {
                        this.SelectedItem = null;
                    }
                    else
                    {
                        this.SelectedItems.Clear();
                    }
                }
                else
                {
                    // do not touch SelectedItems in Single SelectionMode
                    if (this.SelectionMode == SelectionMode.Single)
                    {
                        this.SelectedItem = e.NewItems != null ? e.NewItems[0] : null;
                    }
                    else
                    {
                        if (e.OldItems != null) e.OldItems.ForEach<object>(i => this.SelectedItems.Remove(i));
                        if (e.NewItems != null) e.NewItems.ForEach<object>(i => this.SelectedItems.Add(i));
                    }
                }
            }
            finally
            {
                _selectedItemsChangedByViewModel = false;
            }
        }

        protected override void OnSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (_selectedItemsChangedByViewModel) return;

            _selectedItemsChangedByList = true;
            try
            {
                if (e.OriginalSource == this)
                {
                    e.Handled = true;
                    if (SelectedZetboxItems is IList)
                    {
                        var lst = (IList)SelectedZetboxItems;
                        e.RemovedItems.OfType<object>().ForEach(i => lst.Remove(i));
                        e.AddedItems.OfType<object>().ForEach(i => { if (!lst.Contains(i)) lst.Add(i); });
                    }
                    else if (SelectedZetboxItems is ICollection)
                    {
                        var lst = (ICollection)SelectedZetboxItems;
                        e.RemovedItems.OfType<object>().ForEach(i => lst.Remove(i));
                        e.AddedItems.OfType<object>().ForEach(i => lst.Add(i, true));
                    }
                }
            }
            finally
            {
                _selectedItemsChangedByList = false;
            }
        }
        #endregion
    }
}
