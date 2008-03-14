using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Kistl.API
{
    public class NotifyingObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        private IDataObject _Parent;
        private string _PropertyName;
        public string PropertyName
        {
            get
            {
                return _PropertyName;
            }
        }

        public NotifyingObservableCollection(IDataObject parent, string propertyName)
        {
            _Parent = parent;
            _PropertyName = propertyName;
        }

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            _Parent.NotifyPropertyChanged(_PropertyName);
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
        }

        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _Parent.NotifyPropertyChanged(_PropertyName);
        }
    }
}
