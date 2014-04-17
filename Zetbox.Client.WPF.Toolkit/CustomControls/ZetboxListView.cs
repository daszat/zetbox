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
using Zetbox.Client.WPF.Toolkit;
using System.Windows.Input;
using System.Windows.Media;

namespace Zetbox.Client.WPF.CustomControls
{
    public class ZetboxListView : ListView
    {
        static ZetboxListView()
        {
            EventManager.RegisterClassHandler(typeof(ListViewItem), MouseLeftButtonDownEvent, new MouseButtonEventHandler(ListViewItem_HandleMouseLeftButtonDownEvent));
            EventManager.RegisterClassHandler(typeof(ListViewItem), PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(ListViewItem_PreviewHandleMouseLeftButtonDownEvent));
            EventManager.RegisterClassHandler(typeof(ListViewItem), PreviewMouseLeftButtonUpEvent, new MouseButtonEventHandler(ListViewItem_PreviewHandleMouseLeftButtonUpEvent));
            EventManager.RegisterClassHandler(typeof(ListViewItem), MouseLeaveEvent, new MouseEventHandler(ListViewItem_HandleMouseLeaveEvent));
        }

        public ZetboxListView()
        {
            this.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(HandledMouseLeftButtonDownEvent), true);
        }

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

        public bool DisableSelectionOnPreview
        {
            get { return (bool)GetValue(DisableSelectionOnPreviewProperty); }
            set { SetValue(DisableSelectionOnPreviewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisableSelectionOnPreview.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisableSelectionOnPreviewProperty =
            DependencyProperty.Register("DisableSelectionOnPreview", typeof(bool), typeof(ZetboxListView), new UIPropertyMetadata(false));


        private class SelectionState
        {
            public MouseButtonEventArgs PreviewedEvent;
            public MouseButtonEventArgs ListViewItemEvent;
            public MouseButtonEventArgs HandledEvent;
        }

        private SelectionState _currentSelectionState;

        /// <summary>
        /// make use of event tunneling for selecting items when a click event occurred in a child element
        /// </summary>
        /// <remarks>http://joshsmithonwpf.wordpress.com/2007/06/22/overview-of-routed-events-in-wpf/</remarks>
        /// <param name="e"></param>
        protected override void OnPreviewMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            _currentSelectionState = new SelectionState();
            _currentSelectionState.PreviewedEvent = e;
        }

        ListViewItem _handledMouseDownLVI;

        private static void ListViewItem_PreviewHandleMouseLeftButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            var lvi = sender as ListViewItem;
            if (lvi == null) return;

            var listView = lvi.FindVisualParent<ZetboxListView>();
            if (listView == null) return;

            if (listView.SelectedItems.Contains(lvi.DataContext))
            {
                // the user may start a drag by clicking into selected items
                // delay destroying the selection to the Up event
                e.Handled = true;
                listView._handledMouseDownLVI = lvi;
            }
        }

        private static void ListViewItem_HandleMouseLeaveEvent(object sender, MouseEventArgs e)
        {
            var lvi = sender as ListViewItem;
            if (lvi == null) return;

            var listView = lvi.FindVisualParent<ZetboxListView>();
            if (listView == null) return;

            // reset currently clicked item when leaving it.
            listView._handledMouseDownLVI = null;
        }

        private static void ListViewItem_PreviewHandleMouseLeftButtonUpEvent(object sender, MouseButtonEventArgs e)
        {
            var lvi = sender as ListViewItem;
            if (lvi == null) return;

            var listView = lvi.FindVisualParent<ZetboxListView>();
            if (listView == null) return;

            // on letting the Button go, select/deselect the item(s) as usual
            if (listView._handledMouseDownLVI == lvi)
            {
                listView.SelectListViewItems(e);
                listView._handledMouseDownLVI = null;
            }
        }

        private static void ListViewItem_HandleMouseLeftButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            var lvi = sender as ListViewItem;
            if (lvi == null) return;

            var listView = lvi.FindVisualParent<ZetboxListView>();
            if (listView == null) return;

            if (listView._currentSelectionState == null) listView._currentSelectionState = new SelectionState();
            listView._currentSelectionState.ListViewItemEvent = e;
        }

        private void HandledMouseLeftButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            if (_currentSelectionState == null) _currentSelectionState = new SelectionState();
            _currentSelectionState.HandledEvent = e;

            if (_currentSelectionState.ListViewItemEvent == null
             && _currentSelectionState.HandledEvent != null
             && _currentSelectionState.PreviewedEvent != null
             && _currentSelectionState.HandledEvent.Timestamp == _currentSelectionState.PreviewedEvent.Timestamp)
            {
                SelectListViewItems(_currentSelectionState.HandledEvent);
            }
        }

        private void SelectListViewItems(MouseButtonEventArgs e)
        {
            if (!DisableSelectionOnPreview)
            {
                var src = e.OriginalSource as DependencyObject;
                if (src != null)
                {
                    var lvi = src.FindVisualParent<ListViewItem>();
                    if (lvi != null)
                    {
                        switch (SelectionMode)
                        {
                            case SelectionMode.Single:
                                this.SelectedItem = lvi.DataContext;
                                break;
                            case SelectionMode.Multiple:
                                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                                {
                                    lvi.IsSelected = !lvi.IsSelected;
                                }
                                else
                                {
                                    this.SelectedItem = lvi.DataContext;
                                }
                                break;
                            case SelectionMode.Extended:
                                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                                {
                                    lvi.IsSelected = !lvi.IsSelected;
                                }
                                else if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                                {
                                    // Do nothing, not supported yet
                                }
                                else
                                {
                                    this.SelectedItem = lvi.DataContext;
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
