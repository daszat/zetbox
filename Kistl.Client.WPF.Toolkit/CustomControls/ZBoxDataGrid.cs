using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Specialized;
using Kistl.API;
using System.Collections;
using System.Windows.Data;
using Kistl.API.Utils;

namespace Kistl.Client.WPF.CustomControls
{
    public class ZBoxDataGrid : Microsoft.Windows.Controls.DataGrid
    {
        #region CellEdit
        bool continueEdit = false;
        bool isNewItemForContinueEdit = false;
        bool isNewItemInitialized = false;

        protected override void OnCellEditEnding(Microsoft.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            continueEdit = e.EditAction == Microsoft.Windows.Controls.DataGridEditAction.Commit;
            Logging.Client.DebugFormat("OnCellEditEnding(EditAction = {0})", e.EditAction);
            Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            base.OnCellEditEnding(e);
        }

        protected override void OnBeginningEdit(Microsoft.Windows.Controls.DataGridBeginningEditEventArgs e)
        {
            Logging.Client.DebugFormat("OnBeginningEdit(Row.Item.Type = {0})", e.Row.Item.GetType().Name);
            Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            base.OnBeginningEdit(e);
        }

        protected override void OnInitializingNewItem(Microsoft.Windows.Controls.InitializingNewItemEventArgs e)
        {
            Logging.Client.DebugFormat("OnInitializingNewItem(NewItem.Type = {0})", e.NewItem.GetType().Name);
            Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            isNewItemInitialized = true;
            base.OnInitializingNewItem(e);
        }

        protected override void OnPreparingCellForEdit(Microsoft.Windows.Controls.DataGridPreparingCellForEditEventArgs e)
        {
            Logging.Client.DebugFormat("OnPreparingCellForEdit(Row.Item.Type = {0})", e.Row.Item.GetType().Name);
            Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            base.OnPreparingCellForEdit(e);
        }

        protected override void OnRowEditEnding(Microsoft.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            Logging.Client.DebugFormat("OnRowEditEnding(Row.Item.Type = {0})", e.Row.Item.GetType().Name);
            Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            base.OnRowEditEnding(e);
        }

        protected override void OnCurrentCellChanged(EventArgs e)
        {
            base.OnCurrentCellChanged(e);
            Logging.Client.DebugFormat("OnCurrentCellChanged(CurrentCell.Item.Type={0})", CurrentCell.Item.GetType().Name);
            Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            if (continueEdit)
            {
                isNewItemForContinueEdit = CurrentCell.Item == CollectionView.NewItemPlaceholder;
                if (!isNewItemForContinueEdit)
                {
                    continueEdit = false;
                    Logging.Client.Debug("    OnCurrentCellChanged begins editing of existing item");
                    Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
                    BeginEdit();
                }
                else
                {
                    // delay until selectionChange
                    Logging.Client.Debug("    OnCurrentCellChanged delays edit of new item until selection changed");
                    Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
                }
            }
        }
        #endregion

        #region SelectionChanged
        public object SelectedZBoxItems
        {
            get { return (object)GetValue(SelectedZBoxItemsProperty); }
            set { SetValue(SelectedZBoxItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedZBoxItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedZBoxItemsProperty =
            DependencyProperty.Register("SelectedZBoxItems", typeof(object), typeof(ZBoxDataGrid), new UIPropertyMetadata(null, new PropertyChangedCallback(OnSelectedZBoxItemsChanged)));

        public static void OnSelectedZBoxItemsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ZBoxDataGrid)
            {
                ((ZBoxDataGrid)obj).AttachSelectedZBoxItemsCollectionChanged();
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
            Logging.Client.Debug("OnSelectionChanged()");
            Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
            base.OnSelectionChanged(e);

            if (isNewItemForContinueEdit)
            {
                isNewItemForContinueEdit = false;
                Logging.Client.Debug("    OnSelectionChanged begins edit for creating new item");
                Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
                BeginEdit(); // to create a new item
            }

            if(isNewItemInitialized)
            {
                isNewItemInitialized = false;
                Logging.Client.Debug("    OnSelectionChanged commits edit to trigger new row");
                Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
                CommitEdit(Microsoft.Windows.Controls.DataGridEditingUnit.Row, false);
                Logging.Client.Debug("    OnSelectionChanged begins edit for real");
                Logging.Client.DebugFormat("    continueEdit={0}, isNewItem={1}", continueEdit, isNewItemForContinueEdit);
                BeginEdit(); // to continue editing                            ^^^^^ didn't help
            }

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
                        e.RemovedItems.OfType<object>().Where(i=> i != CollectionView.NewItemPlaceholder).ForEach(i => lst.Remove(i));
                        e.AddedItems.OfType<object>().Where(i => i != CollectionView.NewItemPlaceholder).ForEach(i => lst.Add(i));
                    }
                    else if (SelectedZBoxItems is ICollection)
                    {
                        var lst = (ICollection)SelectedZBoxItems;
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
    }
}
