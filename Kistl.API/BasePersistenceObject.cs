namespace Kistl.API
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Xml;
    using System.Diagnostics;
using System.Collections.Generic;
using System.Xml.Serialization;

    /// <summary>
    /// Implement basic functionality needed by all persistent objects.
    /// </summary>
    public abstract class BasePersistenceObject
        : BaseNotifyingObject, IPersistenceObject, IDataErrorInfo
    {
        /// <summary>
        /// Gets or sets the primary key of this object. By convention all persistent objects have to have this synthesised primary key.
        /// </summary>
        public abstract int ID { get; set; }

        /// <summary>
        /// Gets a value indicating whether values of this object can be set. This is only a shorthand for asking the context for read-only status.
        /// </summary>
        [XmlIgnore]
        public bool IsReadonly
        {
            get { return this.Context != null ? this.Context.IsReadonly : false; }
        }

        /// <summary>
        /// Gets the <see cref="IKistlContext"/> containing this object.
        /// </summary>
        [XmlIgnore]
        public IKistlContext Context { get; private set; }

        /// <summary>
        /// Gets a value indicating whether or not this object is attached to a context.
        /// </summary>
        [XmlIgnore]
        public abstract bool IsAttached { get; }

        /// <summary>
        /// Gets a value indicating the current state of this object.
        /// </summary>
        public abstract DataObjectState ObjectState { get; }

        /// <summary>
        /// Attach this Object to a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to attach this Object to.</param>
        public virtual void AttachToContext(IKistlContext ctx)
        {
            if (this.Context != null && this.Context != ctx)
                throw new WrongKistlContextException("Object cannot be attached to a new Context while attached to another Context.");

            this.Context = ctx;
        }

        /// <summary>
        /// Detach this Object from a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to detach this Object from.</param>
        public virtual void DetachFromContext(IKistlContext ctx)
        {
            if (this.Context != ctx)
                throw new WrongKistlContextException("Object is not attached to the given context.");

            this.Context = null;
        }

        /// <summary>
        /// Applies changes from another IPersistenceObject of the same interface type.
        /// </summary>
        /// <param name="obj">the object to copy from</param>
        public virtual void ApplyChangesFrom(IPersistenceObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (obj.GetType().ToInterfaceType() != this.GetType().ToInterfaceType())
                throw new ArgumentOutOfRangeException("obj");

            this.ID = obj.ID;
        }

        /// <summary>
        /// Returns the most specific <see cref="InterfaceType"/> implemented by this object.
        /// </summary>
        /// <returns>the <see cref="InterfaceType"/> of this object</returns>
        public abstract InterfaceType GetInterfaceType();

        #region IStreamable Members

        /// <summary>
        /// Base method for serializing this Object.
        /// </summary>
        /// <param name="sw">Stream to serialize to</param>
        /// <param name="auxObjects">pass a List here to collect auxiliary, eagerly loaded objects. Ignored if null.</param>
        public virtual void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");

            BinarySerializer.ToStream(new SerializableType(this.GetInterfaceType()), sw);
            BinarySerializer.ToStream(this.ID, sw);
        }

        /// <summary>
        /// Base method for deserializing this Object.
        /// </summary>
        /// <param name="sr">Stream to serialize from</param>
        public virtual void FromStream(BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            if (this.IsAttached)
                throw new InvalidOperationException("Deserializing attached objects is not allowed");

            SerializableType t;
            BinarySerializer.FromStream(out t, sr);

            if (this.GetInterfaceType() != t.GetSystemType())
                throw new InvalidOperationException(string.Format("Unable to deserialize Object of Type {0} from Type {1}", GetType(), t));

            BinarySerializer.FromStreamConverter(i => this.ID = i, sr);
        }

        /// <summary>
        /// Base method for serializing this Object to XML.
        /// </summary>
        /// <param name="xml">Stream to serialize to</param>
        /// <param name="modules">an array of module names to constrain the output</param>
        [Obsolete]
        public virtual void ToStream(XmlWriter xml, string[] modules)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            xml.WriteAttributeString("ID", this.ID.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public virtual void ToStream(XmlWriter xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            xml.WriteAttributeString("ID", this.ID.ToString());
        }

        /// <summary>
        /// Base method for deserializing this Object to XML.
        /// </summary>
        /// <param name="xml">Stream to serialize from</param>
        public virtual void FromStream(XmlReader xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            // TODO: Da hats was - ist asymetrisch zu FromStream(BinaryReader)
            if (!this.IsAttached)
                throw new InvalidOperationException("Xml Deserializing dettached objects is not allowed");
        }

        /// <summary>
        /// Reloads all references to other objects from the underlying storage. Providers may use this to update internal metadata after deserialisation.
        /// </summary>
        public virtual void ReloadReferences()
        {
        }

        #endregion

        #region IDataErrorInfo Members

        /// <summary>
        /// Returns a value indicating whether or not this object is in a valid configuration.
        /// </summary>
        /// <returns>a value indicating whether or not this object is in a valid configuration.</returns>
        public abstract bool IsValid();

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="prop">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. Returns 
        /// <value>String.Empty</value> if there is nothing to report.</returns>
        protected abstract string GetPropertyError(string prop);

        /// <summary>
        /// Gets an error message indicating what is wrong with this object. Returns 
        /// <value>String.Empty</value> if there is nothing to report.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [XmlIgnore]
        string IDataErrorInfo.Error
        {
            get
            {
                if (this.IsValid())
                {
                    return String.Empty;
                }
                else
                {
                    // TODO: implement a way to get all error messages from all properties of this object.
                    return "This object has errors.";
                }
            }
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. Returns 
        /// <value>String.Empty</value> if there is nothing to report.</returns>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [XmlIgnore]
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return GetPropertyError(columnName);
            }
        }


        #endregion

    }
}
