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

        private KistlContext _context;
        public KistlContext Context { get { return _context; } }
        internal void AttachToContext(KistlContext ctx)
        {
            _context = ctx;
        }

        internal void DetachFromContext(KistlContext ctx)
        {
            if (_context != ctx) throw new InvalidOperationException("Object is not attached to the given context.");
            _context = null;
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
