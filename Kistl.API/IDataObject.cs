using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace Kistl.API
{
    public interface IDataObject 
    {
        int ID { get; set; }
        void NotifyChange();
    }

    public abstract class BaseDataObject : IDataObject, INotifyPropertyChanged
    {
        #region IDataObject Members

        public abstract int ID { get; set; }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
        public void NotifyChange()
        {
            if (PropertyChanged != null)
            {
                System.ComponentModel.PropertyChangedEventArgs e = new System.ComponentModel.PropertyChangedEventArgs(null);
                PropertyChanged(this, e);
            }
        }
    }

    public class ServerObjectAttribute : Attribute
    {
        public string FullName { get; set; }
    }

    public class ClientObjectAttribute : Attribute
    {
        public string FullName { get; set; }
    }
}
