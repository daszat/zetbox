using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace Kistl.API
{
    public abstract class BasePersistenceObject : BaseNotifyingObject, IPersistenceObject
    {
        /// <summary>
        /// Everyone has an ID
        /// </summary>
        public abstract int ID { get; set; }

        public bool IsReadonly { get { return _context != null ? _context.IsReadonly : false; ; } }

        private IKistlContext _context;
        public IKistlContext Context { get { return _context; } }

        public abstract bool IsAttached { get; }

        public abstract DataObjectState ObjectState { get; }

        /// <summary>
        /// Attach this Object to a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to attach this Object to.</param>
        public virtual void AttachToContext(IKistlContext ctx)
        {
            if (_context != null && _context != ctx) throw new InvalidOperationException("Object cannot be attached to a new Context while attached to another Context.");
            _context = ctx;
        }

        /// <summary>
        /// Detach this Object from a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to detach this Object from.</param>
        public virtual void DetachFromContext(IKistlContext ctx)
        {
            if (_context != ctx) throw new InvalidOperationException("Object is not attached to the given context.");
            _context = null;
        }


        /// <summary>
        /// Applies changes from another IPersistenceObject of the same interface type.
        /// </summary>
        /// <param name="obj"></param>
        public virtual void ApplyChangesFrom(IPersistenceObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (obj.GetType().ToInterfaceType() != this.GetType().ToInterfaceType())
                throw new ArgumentOutOfRangeException("obj");

            this.ID = obj.ID;
        }

        #region IStreamable Members
        /// <summary>
        /// Base method for serializing this Object.
        /// </summary>
        /// <param name="sw">Stream to serialize to</param>
        public virtual void ToStream(BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");

            BinarySerializer.ToStream(new SerializableType(this.GetInterfaceType()), sw);
            BinarySerializer.ToStream(ID, sw);
        }

        /// <summary>
        /// Base method for deserializing this Object.
        /// </summary>
        /// <param name="sr">Stream to serialize from</param>
        public virtual void FromStream(BinaryReader sr)
        {
            if (sr == null) throw new ArgumentNullException("sr");
            if (this.IsAttached) throw new InvalidOperationException("Deserializing attached objects is not allowed");

            SerializableType t;
            BinarySerializer.FromStream(out t, sr);

            if (this.GetInterfaceType() != t.GetSystemType())
                throw new InvalidOperationException(string.Format("Unable to deserialize Object of Type {0} from Type {1}", GetType(), t));

            BinarySerializer.FromStreamConverter(i => ID = i, sr);
        }

        public virtual void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            if (xml == null) throw new ArgumentNullException("xml");
            xml.WriteAttributeString("ID", ID.ToString());
        }

        public virtual void FromStream(System.Xml.XmlReader xml)
        {
            if (xml == null) throw new ArgumentNullException("xml");
            // TODO: Da hats was - ist asymetrisch zu FromStream(BinaryReader)
            if (!this.IsAttached) throw new InvalidOperationException("Xml Deserializing dettached objects is not allowed");
        }

        public virtual void ReloadReferences() { }

        #endregion

        public abstract InterfaceType GetInterfaceType();
    }
}
