using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Kistl.API
{
    /// <summary>
    /// A ObservableCollection implementation, that reports changes to a parent Object.
    /// Reports changes of the colelction _and_ changes of Items.
    /// </summary>
    /// <typeparam name="T">Collection Item Type</typeparam>
    public class NotifyingObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        private IDataObject _Parent;
        private int _UpdateCounter = 0;

        private string _PropertyName;

        /// <summary>
        /// Propertyname of the parent Object that holds the Collection
        /// </summary>
        public string PropertyName
        {
            get
            {
                return _PropertyName;
            }
        }

        /// <summary>
        /// Creates a new NotifyingObservableCollection
        /// </summary>
        /// <param name="parent">Parent Object that holds this collection</param>
        /// <param name="propertyName">Propertyname of the parent Object, that holds this collection</param>
        public NotifyingObservableCollection(IDataObject parent, string propertyName)
        {
            _Parent = parent;
            _PropertyName = propertyName;
        }

        /// <summary>
        /// Suppress Events while updating the Collection. 
        /// This method may be called more than once. EndUpdate() must be called for each BeginUpdate() call.
        /// </summary>
        public void BeginUpdate()
        {
            _UpdateCounter++;
        }

        /// <summary>
        /// Starts firing Events after updating the Collection. 
        /// This method may be called more than once. EndUpdate() must be called for each BeginUpdate() call.
        /// </summary>
        public void EndUpdate()
        {
            _UpdateCounter--;
        }

        /// <summary>
        /// Notifies the Parent Object that the Collection or one of it items has changed.
        /// </summary>
        protected void NotifyParent()
        {
            if (_UpdateCounter == 0)
            {
                _Parent.NotifyPropertyChanged(_PropertyName);
            }
        }

        /// <summary>
        /// Override OnCollectionChanged and report to Parent.
        /// </summary>
        /// <param name="e">NotifyCollectionChangedEventArgs</param>
        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            NotifyParent();
        }

        /// <summary>
        /// Override InsertItem to register change Events of this Item. 
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="item">Item</param>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
        }

        /// <summary>
        /// Override SetItem and register change Events of this Item.
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="item">Item</param>
        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyParent();
        }
    }
}
