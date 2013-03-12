
namespace Zetbox.ConfigEditor.Controls
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

    public class ListView : System.Windows.Controls.ListView
    {
        #region SelectionChanged
        public object SelectedViewModels
        {
            get { return (object)GetValue(SelectedViewModelsProperty); }
            set { SetValue(SelectedViewModelsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedZBoxItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedViewModelsProperty =
            DependencyProperty.Register("SelectedViewModels", typeof(object), typeof(ListView), new UIPropertyMetadata(null, new PropertyChangedCallback(OnSelectedViewModelsChanged)));

        public static void OnSelectedViewModelsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ListView)
            {
                ((ListView)obj).AttachSelectedViewModelsCollectionChanged();
            }
        }

        private void AttachSelectedViewModelsCollectionChanged()
        {
            if (SelectedViewModels is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)SelectedViewModels).CollectionChanged += new NotifyCollectionChangedEventHandler(list_CollectionChanged);
                try
                {
                    _selectedItemsChangedByViewModel = true;
                    ((IEnumerable)SelectedViewModels).ForEach<object>(i => this.SelectedItems.Add(i));
                }
                finally
                {
                    _selectedItemsChangedByViewModel = false;
                }
            }
        }

        private bool _selectedItemsChangedByViewModel = false;
        private bool _selectedItemsChangedByList = false;

        private void list_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
                    if (SelectedViewModels is IList)
                    {
                        var lst = (IList)SelectedViewModels;
                        e.RemovedItems.OfType<object>().ForEach(i => lst.Remove(i));
                        e.AddedItems.OfType<object>().ForEach(i => lst.Add(i));
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
