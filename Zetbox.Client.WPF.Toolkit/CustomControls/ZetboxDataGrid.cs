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

namespace Zetbox.Client.WPF.CustomControls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.Client.WPF.Toolkit;

    public class ZetboxDataGrid : DataGrid
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ZetboxDataGrid));

        public ZetboxDataGrid()
        {
            this.ItemContainerStyle = Application.Current.Resources["DataGridItemContainerStyle"] as Style;
        }

        #region CellEdit
        bool continueEdit = false;
        bool isNewItemForContinueEdit = false;
        bool isNewItemInitialized = false;

        protected override void OnCellEditEnding(DataGridCellEditEndingEventArgs e)
        {
            continueEdit = e.EditAction == DataGridEditAction.Commit;
            Log.DebugFormat("OnCellEditEnding(EditAction = {0})", e.EditAction);
            Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            base.OnCellEditEnding(e);
        }

        protected override void OnBeginningEdit(DataGridBeginningEditEventArgs e)
        {
            Log.DebugFormat("OnBeginningEdit(Row.Item.Type = {0})", e.Row.Item.GetType().Name);
            Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            base.OnBeginningEdit(e);
        }

        protected override void OnInitializingNewItem(InitializingNewItemEventArgs e)
        {
            Log.DebugFormat("OnInitializingNewItem(NewItem.Type = {0})", e.NewItem.GetType().Name);
            Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            isNewItemInitialized = true;
            base.OnInitializingNewItem(e);
        }

        protected override void OnPreparingCellForEdit(DataGridPreparingCellForEditEventArgs e)
        {
            Log.DebugFormat("OnPreparingCellForEdit(Row.Item.Type = {0})", e.Row.Item.GetType().Name);
            Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            base.OnPreparingCellForEdit(e);
            var editor = e.EditingElement.FindVisualChild<PropertyEditor>();
            if (editor != null)
            {
                editor.Focus();
            }
        }

        protected override void OnRowEditEnding(DataGridRowEditEndingEventArgs e)
        {
            Log.DebugFormat("OnRowEditEnding(Row.Item.Type = {0})", e.Row.Item.GetType().Name);
            Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            base.OnRowEditEnding(e);
        }

        protected override void OnCurrentCellChanged(EventArgs e)
        {
            base.OnCurrentCellChanged(e);
            Log.DebugFormat("OnCurrentCellChanged(CurrentCell.Item.Type={0})", CurrentCell.Item.GetType().Name);
            Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            if (continueEdit)
            {
                isNewItemForContinueEdit = CurrentCell.Item == CollectionView.NewItemPlaceholder;
                if (!isNewItemForContinueEdit)
                {
                    continueEdit = false;
                    Log.Debug("    OnCurrentCellChanged begins editing of existing item");
                    Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
                    BeginEdit();
                }
                else
                {
                    // delay until selectionChange
                    Log.Debug("    OnCurrentCellChanged delays edit of new item until selection changed");
                    Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
                }
            }
        }
        #endregion

        #region SelectionChanged
        public object SelectedZetboxItems
        {
            get { return (object)GetValue(SelectedZetboxItemsProperty); }
            set { SetValue(SelectedZetboxItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedZetboxItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedZetboxItemsProperty =
            DependencyProperty.Register("SelectedZetboxItems", typeof(object), typeof(ZetboxDataGrid), new UIPropertyMetadata(null, new PropertyChangedCallback(OnSelectedZetboxItemsChanged)));

        public static void OnSelectedZetboxItemsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ZetboxDataGrid)
            {
                ((ZetboxDataGrid)obj).AttachSelectedZetboxItemsCollectionChanged();
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

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (this.ItemsSource != newValue) // JFC, WTF!
            {
                base.OnItemsSourceChanged(oldValue, newValue);
            }
        }

        private void ZetboxDataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_selectedItemsChangedByList) return;

            _selectedItemsChangedByViewModel = true;
            try
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                {
                    this.SelectedItems.Clear();
                }
                else
                {
                    if (e.OldItems != null) e.OldItems.ForEach<object>(i => this.SelectedItems.Remove(i));
                    if (e.NewItems != null) e.NewItems.ForEach<object>(i => this.SelectedItems.Add(i));
                }
            }
            finally
            {
                _selectedItemsChangedByViewModel = false;
            }
        }

        protected override void OnSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Log.Debug("OnSelectionChanged()");
            Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            base.OnSelectionChanged(e);

            if (isNewItemForContinueEdit)
            {
                isNewItemForContinueEdit = false;
                Log.Debug("    OnSelectionChanged begins edit for creating new item");
                Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
                BeginEdit(); // to create a new item
            }

            if (isNewItemInitialized)
            {
                isNewItemInitialized = false;
                Log.Debug("    OnSelectionChanged commits edit to trigger new row");
                Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
                CommitEdit(DataGridEditingUnit.Row, false);
                Log.Debug("    OnSelectionChanged begins edit for real");
                Log.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
                BeginEdit(); // to continue editing                            ^^^^^ didn't help
            }

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
                        e.RemovedItems.OfType<object>().Where(i => i != CollectionView.NewItemPlaceholder).ForEach(i => lst.Remove(i));
                        e.AddedItems.OfType<object>().Where(i => i != CollectionView.NewItemPlaceholder).ForEach(i => { if (!lst.Contains(i)) lst.Add(i); });
                    }
                    else if (SelectedZetboxItems is ICollection)
                    {
                        var lst = (ICollection)SelectedZetboxItems;
                        e.RemovedItems.OfType<object>().Where(i => i != CollectionView.NewItemPlaceholder).ForEach(i => lst.Remove(i));
                        e.AddedItems.OfType<object>().Where(i => i != CollectionView.NewItemPlaceholder).ForEach(i => lst.Add(i, true));
                    }
                }
            }
            finally
            {
                _selectedItemsChangedByList = false;
            }
        }
        #endregion

        // Case 6181
        // Avoid stack overflow. http://stackoverflow.com/questions/4017786/wpf-recursive-call-to-automation-peer-api-is-not-valid
        protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
        {
            return null;
        }
    }
}
