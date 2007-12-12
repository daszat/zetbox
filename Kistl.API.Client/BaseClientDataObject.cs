using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    public abstract class BaseClientDataObject : IDataObject
    {
        public BaseClientDataObject()
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

        public abstract int ID { get; set; }

        public virtual void NotifyPreSave() { }
        public virtual void NotifyPostSave() { }

        #endregion

        /// <summary>
        /// Zum Melden, dass sich das Datenobjekt geänder hat.
        /// </summary>
        public void NotifyChange()
        {
        }
    }
}
