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
        /// Der EntitySetName der Basisklasse
        /// </summary>
        string EntitySetName { get; }

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
    public class MethodReturnEventArgs<T>
    {
        public T Result { get; set; }
    }

    /// <summary>
    /// ToString Event Delegate. Wird von den Datenobjekten gefeuert
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="e"></param>
    public delegate void ToStringHandler<T>(T obj, MethodReturnEventArgs<string> e) where T : class, IDataObject, new();

    /// <summary>
    /// Handler für Server Custom Events. TODO: Außer SetObject hat's noch niemand implementiert.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ctx"></param>
    /// <param name="obj"></param>
    public delegate void ObjectEventHandler<T>(T obj) where T : class, IDataObject, new();

    /// <summary>
    /// Basis Datenobjekt. Attached sich automatisch an den CustomActionsManager zur Verteilung der Events
    /// </summary>
    public abstract class BaseDataObject : System.Data.Objects.DataClasses.EntityObject, IDataObject
    {
        /// <summary>
        /// Attach to Events
        /// </summary>
        public BaseDataObject()
        {
            API.CustomActionsManagerFactory.Current.AttachEvents(this);
            _type = new ObjectType(this.GetType().Namespace, this.GetType().Name);
        }

        protected ObjectType _type = null;

        public ObjectType Type
        {
            get
            {
                return _type;
            }
        }

        #region IDataObject Members
        /// <summary>
        /// Jeder hat eine ID
        /// </summary>
        public abstract int ID { get; set; }

        public abstract string EntitySetName { get; }

        public virtual void NotifyPreSave() {}
        public virtual void NotifyPostSave() {}

        #endregion

        /// <summary>
        /// Zum Melden, dass sich das Datenobjekt geänder hat.
        /// </summary>
        public void NotifyChange()
        {
            ReportPropertyChanged(null);
        }
    }
}
