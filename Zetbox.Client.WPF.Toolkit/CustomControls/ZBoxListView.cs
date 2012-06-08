using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Kistl.API;

namespace Kistl.Client.WPF.CustomControls
{
    public class ZBoxListView : ListView
    {
        #region SelectionChanged
        public object SelectedZBoxItems
        {
            get { return (object)GetValue(SelectedZBoxItemsProperty); }
            set { SetValue(SelectedZBoxItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedZBoxItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedZBoxItemsProperty =
            DependencyProperty.Register("SelectedZBoxItems", typeof(object), typeof(ZBoxListView), new UIPropertyMetadata(null, new PropertyChangedCallback(OnSelectedZBoxItemsChanged)));

        public static void OnSelectedZBoxItemsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ZBoxListView)
            {
                ((ZBoxListView)obj).AttachSelectedZBoxItemsCollectionChanged();
            }
        }

        private void AttachSelectedZBoxItemsCollectionChanged()
        {
            if (SelectedZBoxItems is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)SelectedZBoxItems).CollectionChanged += new NotifyCollectionChangedEventHandler(ZBoxDataGrid_CollectionChanged);
                try
                {
                    _selectedItemsChangedByViewModel = true;
                    ((IEnumerable)SelectedZBoxItems).ForEach<object>(i => this.SelectedItems.Add(i));
                }
                finally
                {
                    _selectedItemsChangedByViewModel = false;
                }
            }
        }

        private bool _selectedItemsChangedByViewModel = false;
        private bool _selectedItemsChangedByList = false;

        private void ZBoxDataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
                    if (SelectedZBoxItems is IList)
                    {
                        var lst = (IList)SelectedZBoxItems;
                        e.RemovedItems.OfType<object>().ForEach(i => lst.Remove(i));
                        e.AddedItems.OfType<object>().ForEach(i => lst.Add(i));
                    }
                    else if (SelectedZBoxItems is ICollection)
                    {
                        var lst = (ICollection)SelectedZBoxItems;
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
