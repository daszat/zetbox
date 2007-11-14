using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace Kistl.API
{
    /// <summary>
    /// Datenobjekt Interface
    /// </summary>
    public interface IDataObject 
    {
        /// <summary>
        /// Jeder hat eine ID
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// Zum Melden, dass sich das Datenobjekt geänder hat.
        /// </summary>
        void NotifyChange();

        void NotifyPreSave();
        void NotifyPostSave();
    }

    /// <summary>
    /// Argumente für ToString Event
    /// </summary>
    public class ToStringEventArgs
    {
        public string Result { get; set; }
    }

    /// <summary>
    /// ToString Event Delegate. Wird von den Datenobjekten gefeuert
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="e"></param>
    public delegate void ToStringHandler<T>(T obj, ToStringEventArgs e) where T : class, IDataObject, new();

    /// <summary>
    /// Handler für Server Custom Events. TODO: Außer SetObject hat's noch niemand implementiert.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ctx"></param>
    /// <param name="obj"></param>
    public delegate void ObjectEventHandler<T>(T obj) where T : class, IDataObject, new();

    /// <summary>
    /// Basis Datenobjekt. Attached sich automatisch an den ObjectBroker zur Verteilung der Events
    /// TODO: Besseren Namen für den ObjectBroker finden - bin mir nicht sicher, ob der Name passt.
    /// </summary>
    public abstract class BaseDataObject : IDataObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Attach to Events
        /// </summary>
        public BaseDataObject()
        {
            API.CustomActionsManagerFactory.Current.AttachEvents(this);
        }

        #region IDataObject Members
        /// <summary>
        /// Jeder hat eine ID
        /// </summary>
        public abstract int ID { get; set; }

        public abstract void NotifyPreSave();
        public abstract void NotifyPostSave();

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Zum Melden, dass sich das Datenobjekt geänder hat.
        /// </summary>
        public void NotifyChange()
        {
            if (PropertyChanged != null)
            {
                System.ComponentModel.PropertyChangedEventArgs e = new System.ComponentModel.PropertyChangedEventArgs(null);
                PropertyChanged(this, e);
            }
        }
    }
}
