namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Data Object State
    /// </summary>
    public enum DataObjectState
    {
        /// <summary>
        /// Object is new, should be inserted.
        /// </summary>
        New,

        /// <summary>
        /// Object is modified, should be updated.
        /// </summary>
        Modified,

        /// <summary>
        /// Object is unmodified, nothing needs to be done.
        /// </summary>
        Unmodified,

        /// <summary>
        /// Object is deleted, should be deleted from the data store, caches, etc.
        /// </summary>
        Deleted,

        /// <summary>
        /// The Entity was not deserialized. Used by the 
        /// BaseServerPersistenceObject to flag objects that were not 
        /// unpacked from the incoming stream yet.
        /// </summary>
        NotDeserialized,
    }

    /// <summary>
    /// Describes the possible types of relations between objects.
    /// </summary>
    public enum RelationType
    {
        /// <summary>
        /// 1:N relation
        /// </summary>
        one_n,

        /// <summary>
        /// N:M relation
        /// </summary>
        n_m,

        /// <summary>
        /// 1:1 relation
        /// </summary>
        one_one,
    }

    /// <summary>
    /// Describes access rights
    /// </summary>
    [System.Flags]
    public enum AccessRights
    {
        None    = 0x00,
        Read    = 0x01,
        Write   = 0x02,
        Delete  = 0x04,
        Create  = 0x08,

        Full    = Read | Write | Delete | Create,
        Change  = Read | Write,
    }

    /// <summary>
    /// Interface for Exporting/Importing Objects with XML
    /// </summary>
    public interface IExportableInternal
    {
        Guid ExportGuid { get; set; }

        /// <summary>
        /// Serialize this Object to a XmlWriter
        /// </summary>
        /// <param name="xml">XmlWriter to serialize to</param>
        /// <param name="modules">a list of modules to filter the output</param>
        void Export(XmlWriter xml, string[] modules);

        /// <summary>
        /// Deserialize this Object from a XmlReader
        /// </summary>
        /// <param name="xml">XmlReader to deserialize to.</param>
        void MergeImport(XmlReader xml);
    }

    /// <summary>
    /// Interface for Exporting/Importing Objects with XML
    /// </summary>
    public interface IExportableCollectionEntryInternal
    {
        /// <summary>
        /// Serialize this Object to a XmlWriter
        /// </summary>
        /// <param name="xml">XmlWriter to serialize to</param>
        /// <param name="modules">a list of modules to filter the output</param>
        void Export(XmlWriter xml, string[] modules);

        /// <summary>
        /// Deserialize this Object from a XmlReader
        /// </summary>
        /// <param name="xml">XmlReader to deserialize to.</param>
        void MergeImport(XmlReader xml);
    }

    /// <summary>
    /// Objects implementing this interface can be streamed over a binary stream.
    /// </summary>
    public interface IStreamable
    {
        /// <summary>
        /// Serialize this Object to a BinaryWriter
        /// </summary>
        /// <param name="sw">BinaryWriter to serialize to</param>
        /// <param name="auxObjects">pass a HashSet here to collect auxiliary, eagerly loaded objects. Ignored if null.</param>
        /// <param name="eagerLoadLists">True is lists should be eager loaded</param>
        void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists);

        /// <summary>
        /// Deserialize this Object from a BinaryReader
        /// </summary>
        /// <param name="sr">BinaryReader to deserialize to.</param>
        void FromStream(BinaryReader sr);

        /// <summary>
        /// Serialize this Object to a XmlWriter
        /// </summary>
        /// <param name="xml">XmlWriter to serialize to</param>
        void ToStream(XmlWriter xml);

        /// <summary>
        /// Deserialize this Object from a XmlReader
        /// </summary>
        /// <param name="xml">XmlReader to deserialize to.</param>
        void FromStream(XmlReader xml);

        /// <summary>
        /// Reloads Relations from internal storage into the providers caches.
        /// Should be called after de-serializing and attaching an object graph 
        /// to notify the provider of all references within the graph.
        /// </summary>
        void ReloadReferences();
    }

    /// <summary>
    /// Interface for all persistent objects.
    /// </summary>
    public interface IPersistenceObject : INotifyingObject, IStreamable, ICustomTypeDescriptor
    {
        /// <summary>
        /// Gets the primary key of this object. By convention all persistent objects have to have this synthesised primary key.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Gets a value indicating the current state of this object.
        /// </summary>
        DataObjectState ObjectState { get; }

        /// <summary>
        /// Gets the <see cref="IKistlContext"/> containing this object.
        /// </summary>
        IKistlContext Context { get; }

        /// <summary>
        /// Gets the <see cref="IReadOnlyKistlContext"/> containing this object.
        /// </summary>
        IReadOnlyKistlContext ReadOnlyContext { get; }

        /// <summary>
        /// Gets a value indicating whether or not this object is attached to a context.
        /// </summary>
        bool IsAttached { get; }

        /// <summary>
        /// Gets a value indicating whether values of this object can be set. This depends mostly on the state of the containing context.
        /// </summary>
        bool IsReadonly { get; }

        /// <summary>
        /// Notifies that a property is beeing changing
        /// </summary>
        /// <param name="property">the name of the changing property</param>
        /// <param name="oldValue">the old value of the changing property</param>
        /// <param name="newValue">the new value of the changing property</param>
        void NotifyPropertyChanging(string property, object oldValue, object newValue);

        /// <summary>
        /// Notifies that a property has been changed
        /// </summary>
        /// <param name="property">the name of the changed property</param>
        /// <param name="oldValue">the old value of the changed property</param>
        /// <param name="newValue">the new value of the changed property</param>
        void NotifyPropertyChanged(string property, object oldValue, object newValue);

        /// <summary>
        /// Attach this object to a context. This method is called by the context.
        /// </summary>
        /// <param name="ctx">Context to attach this object to.</param>
        void AttachToContext(IKistlContext ctx);

        /// <summary>
        /// Detach this Object from a Context. This method is called by the context.
        /// </summary>
        /// <param name="ctx">Context to detach this object from.</param>
        void DetachFromContext(IKistlContext ctx);

        /// <summary>
        /// Applies changes from another IPersistenceObject of the same interface type.
        /// </summary>
        /// <param name="obj">the object to copy from</param>
        void ApplyChangesFrom(IPersistenceObject obj);

        /// <summary>
        /// Returns the most specific <see cref="InterfaceType"/> implemented by this object.
        /// </summary>
        /// <returns>the <see cref="InterfaceType"/> of this object</returns>
        InterfaceType GetInterfaceType();
    }

    /// <summary>
    /// DataObject Interface
    /// </summary>
    public interface IDataObject : IPersistenceObject, IDataErrorInfo
    {
        /// <summary>
        /// Fires an Event before an Object is saved.
        /// </summary>
        void NotifyPreSave();

        /// <summary>
        /// Fires an Event after an Object is saved.
        /// </summary>
        void NotifyPostSave();

        /// <summary>
        /// Fires an Event after an Object is created.
        /// </summary>
        void NotifyCreated();

        /// <summary>
        /// Fires an Event before an Object is deleted.
        /// </summary>
        void NotifyDeleting();

        /// <summary>
        /// Reflects the current access rights by the current Identity. 
        /// </summary>
        AccessRights CurrentAccessRights { get; }
    }

    /// <summary>
    /// An ICompoundObject is a simple bag of named values.
    /// </summary>
    /// Supports <see cref="ICloneable"/>, because CompoundObject don't have a independent identity and thus can be copied freely.
    public interface ICompoundObject : INotifyingObject, IStreamable, ICloneable
    {
        /// <summary>
        /// Gets a value indicating whether values of this object can be set. This mostly depends on the state of the containing object.
        /// </summary>
        bool IsReadonly { get; }

        /// <summary>
        /// Attaches this CompoundObject to the specified object on the specified property.
        /// </summary>
        /// <param name="obj">the object to attach to</param>
        /// <param name="property">the property to attach to</param>
        void AttachToObject(IPersistenceObject obj, string property);

        /// <summary>
        /// Detaches this CompoundObject from the specified object on the specified property.
        /// </summary>
        /// <param name="obj">the object to detach from</param>
        /// <param name="property">the property to detach from</param>
        void DetachFromObject(IPersistenceObject obj, string property);
    }

    /// <summary>
    /// Return event arguments for a method call event.
    /// </summary>
    /// <typeparam name="T">the type of the return value</typeparam>
    public class MethodReturnEventArgs<T>
        : EventArgs
    {
        /// <summary>
        /// Gets or sets the result of the method call. This can be modified multiple times while handling the event.
        /// </summary>
        public T Result { get; set; }
    }

    /// <summary>
    /// ToString delegate. Is triggered by IDataObject implementors.
    /// </summary>
    /// <typeparam name="T">Type of the implementing Object.</typeparam>
    /// <param name="obj">Object that has fired this Event.</param>
    /// <param name="e">Method return Arguments.</param>
    public delegate void ToStringHandler<T>(T obj, MethodReturnEventArgs<string> e) where T : IDataObject;

    /// <summary>
    /// Handler for custom save events. TODO: Außer SetObject hat's noch niemand implementiert.
    /// </summary>
    /// <typeparam name="T">Type of the implementing Object.</typeparam>
    /// <param name="obj">>Object that has fired this Event.</param>
    public delegate void ObjectEventHandler<T>(T obj) where T : IDataObject;

    public class PropertyGetterEventArgs<V>
        : EventArgs
    {
        public PropertyGetterEventArgs(V orignal)
        {
            this.Original = orignal;
            this.Result = orignal;
        }

        public V Original { get; private set; }

        public V Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public class PropertyPreSetterEventArgs<V>
        : EventArgs
    {
        public PropertyPreSetterEventArgs(V oldVal, V newVal)
        {
            this.OldValue = oldVal;
            this.NewValue = newVal;
            this.Result = newVal;
        }

        /// <summary>
        /// 
        /// </summary>
        public V OldValue { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public V NewValue { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public V Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public class PropertyPostSetterEventArgs<V>
        : EventArgs
    {
        public PropertyPostSetterEventArgs(V oldVal, V newVal)
        {
            this.OldValue = oldVal;
            this.NewValue = newVal;
        }

        /// <summary>
        /// 
        /// </summary>
        public V OldValue { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public V NewValue { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>No Template restictions. Can be called on a Class or CompoundObject</remarks>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="obj"></param>
    /// <param name="e"></param>
    public delegate void PropertyGetterHandler<T, V>(T obj, PropertyGetterEventArgs<V> e);
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>No Template restictions. Can be called on a Class or CompoundObject</remarks>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="obj"></param>
    /// <param name="e"></param>
    public delegate void PropertyPreSetterHandler<T, V>(T obj, PropertyPreSetterEventArgs<V> e);
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>No Template restictions. Can be called on a Class or CompoundObject</remarks>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="obj"></param>
    /// <param name="e"></param>
    public delegate void PropertyPostSetterHandler<T, V>(T obj, PropertyPostSetterEventArgs<V> e);
}
