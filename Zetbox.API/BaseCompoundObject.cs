// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Implements basic (serialisation) infrastructure of ICompoundObject objects
    /// </summary>
    public abstract class BaseCompoundObject : BaseNotifyingObject, ICompoundObject
    {
        private readonly Func<IFrozenContext> _lazyCtx;
        protected BaseCompoundObject(Func<IFrozenContext> lazyCtx)
        // ignore for now: base(lazyCtx)
        {
            _lazyCtx = lazyCtx;
        }

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
        public virtual Zetbox.API.AccessRights CurrentAccessRights
        {
            get
            {
                return ParentObject != null ? ParentObject.CurrentAccessRights : Zetbox.API.AccessRights.Full;
            }
        }

        public virtual void ApplyChangesFrom(ICompoundObject other) { }

        public virtual void SynchronizeCollections<T>(ICollection<T> me, ICollection<T> other) where T : IPersistenceObject
        {
            throw new NotImplementedException();
        }

        public virtual void SynchronizeLists<T>(IList<T> me, IList<T> other) where T : IPersistenceObject
        {
            throw new NotImplementedException();
        }

        public abstract Guid CompoundObjectID { get; }

        #endregion

        #region IStreamable Members

        /// <summary>
        /// Base method for serializing this Object.
        /// Serializes a CompoundObject to the specified stream. Since CompoundObject have no 
        /// own identity the ParentObject has to be constructed somewhere else 
        /// using external means, e.g. by examining the position in the stream.
        /// </summary>
        /// <param name="sw">Stream to serialize to</param>
        /// <param name="auxObjects">pass a List here to collect auxiliary, eagerly loaded objects. Ignored if null.</param>
        /// <param name="eagerLoadLists">True if lists should be eager loaded</param>
        public virtual void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
        }

        /// <summary>
        /// reads a CompoundObject from the specified stream. Since CompoundObject have no 
        /// own identity the ParentObject has to be constructed somewhere else 
        /// using external means, e.g. by examining the position in the stream.
        /// </summary>
        /// <param name="sr">the stream to read from</param>
        public virtual IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            return null;
        }

        /// <summary>
        /// Serialize this Object to a XmlWriter
        /// </summary>
        /// <param name="xml">XmlWriter to serialize to</param>
        /// <param name="modules">a list of modules to filter the output</param>
        public void Export(XmlWriter xml, string[] modules)
        {
        }

        /// <summary>
        /// Deserialize this Object from a XmlReader
        /// </summary>
        /// <param name="xml">XmlReader to deserialize to.</param>
        public void MergeImport(XmlReader xml)
        {
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
        /// Returns the most specific System.Type implemented by this object.
        /// </summary>
        /// <returns>the System.Type of this object</returns>
        public virtual Type GetImplementedInterface()
        {
            return null;
        }

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

        public bool IsAttached { get { return ParentObject != null && ParentObject.IsAttached; } }

        protected DataObjectState ObjectState { get { return ParentObject != null ? ParentObject.ObjectState : DataObjectState.Detached; } }
        private IFrozenContext _frozenContext;
        protected IFrozenContext FrozenContext
        {
            get
            {
                if (_frozenContext == null)
                {
                    _frozenContext = _lazyCtx();
                }
                return _frozenContext;
            }
        }

        protected override void AuditPropertyChange(string property, object oldValue, object newValue)
        {
            // Compound objects do not audit locally
        }

        protected virtual ObjectIsValidResult ObjectIsValid()
        {
            return ObjectIsValidResult.Valid;
        }

        #region IComparable
        int System.IComparable.CompareTo(object other)
        {
            if (other == null) return 1;
            var aStr = this.ToString();
            var bStr = other.ToString();
            if (aStr == null && bStr == null) return 0;
            if (aStr == null) return -1;
            return aStr.CompareTo(bStr);
        }
        #endregion
    }
}