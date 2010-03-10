using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    /// <summary>
    /// Implements basic (serialisation) infrastructure of ICompoundObject objects
    /// </summary>
    public abstract class BaseCompoundObject : BaseNotifyingObject, ICompoundObject
    {

        #region ICompoundObject Members

        public IPersistenceObject ParentObject { get; protected set; }
        public string ParentProperty { get; protected set; }

        public virtual void AttachToObject(IPersistenceObject obj, string property)
        {
            if (ParentObject != null && ParentObject != obj)
                throw new ArgumentException("CompoundObject is already attached to another object", "obj");

            ParentProperty = property;
            ParentObject = obj;
        }

        public virtual void DetachFromObject(IPersistenceObject obj, string property)
        {
            if (ParentObject == null || ParentObject != obj)
                throw new ArgumentException("CompoundObject is not attached to this object", "obj");

            ParentObject = null;
            ParentProperty = null;
        }

        public virtual bool IsReadonly { get { return ParentObject != null ? ParentObject.IsReadonly : false; } }
        #endregion

        #region IStreamable Members

        /// <summary>
        /// Serializes a CompoundObject to the specified stream. Since CompoundObject have no 
        /// own identity the ParentObject has to be constructed somewhere else 
        /// using external means, e.g. by examining the position in the stream.
        /// </summary>
        /// <param name="sw">the stream to write to</param>
        public virtual void ToStream(BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
        }

        /// <summary>
        /// Base method for serializing this Object.
        /// </summary>
        /// <param name="sw">Stream to serialize to</param>
        /// <param name="auxObjects">pass a List here to collect auxiliary, eagerly loaded objects. Ignored if null.</param>
        public virtual void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects)
        {
            this.ToStream(sw);
        }

        /// <summary>
        /// reads a CompoundObject from the specified stream. Since CompoundObject have no 
        /// own identity the ParentObject has to be constructed somewhere else 
        /// using external means, e.g. by examining the position in the stream.
        /// </summary>
        /// <param name="sr">the stream to read from</param>
        public virtual void FromStream(BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
        }

        [Obsolete]
        public virtual void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            if (xml == null) throw new ArgumentNullException("xml");
        }

        public virtual void ToStream(System.Xml.XmlWriter xml)
        {
            if (xml == null) throw new ArgumentNullException("xml");
        }

        public virtual void FromStream(System.Xml.XmlReader xml)
        {
            if (xml == null) throw new ArgumentNullException("xml");
        }

        /// <summary>
        /// Empty implementation, since CompoundObject can't have ObjectReferences
        /// </summary>
        public void ReloadReferences() { }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// By default, this makes a memberwise clone of this object, but detaches it from its parent.
        /// </summary>
        public object Clone()
        {
            var clone = (BaseCompoundObject)this.MemberwiseClone();
            // detach CompoundObject when cloning
            clone.ParentObject = null;
            clone.ParentProperty = null;
            return clone;
        }

        #endregion

        /// <summary>
        /// returns the Kistl.Objects interface type of this CompoundObject
        /// </summary>
        /// <returns></returns>
        public abstract InterfaceType GetInterfaceType();

        protected override void OnPropertyChanging(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanging(property, oldValue, newValue);
            // Notifing parent is done in provider implementation
        }

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);
            // Notifing parent is done in provider implementation
        }

        protected override void SetModified()
        {
            // don't care
        }
    }
}
