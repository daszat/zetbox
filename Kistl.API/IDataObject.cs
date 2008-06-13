using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace Kistl.API
{
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
        /// Object is modified, shoud be updated.
        /// </summary>
        Modified,
        /// <summary>
        /// Object is unmodified, nothing should be done.
        /// </summary>
        Unmodified,
        /// <summary>
        /// Object is deleted, should be deleted from the Database, Caches, etc.
        /// </summary>
        Deleted,
    }

    public interface IPersistenceObject
    {
        /// <summary>
        /// Every Object has at least an ID
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// State of this Object.
        /// </summary>
        DataObjectState ObjectState { get; set; }

        /// <summary>
        /// Serialize this Object to a BinaryWriter
        /// </summary>
        /// <param name="sw">BinaryWriter to serialize to</param>
        void ToStream(System.IO.BinaryWriter sw);
        /// <summary>
        /// Deserialize this Object from a BinaryReader
        /// </summary>
        /// <param name="sw">BinaryReader to deserialize to.</param>
        void FromStream(System.IO.BinaryReader sr);

        /// <summary>
        /// Fires an Event before an Property is changed.
        /// </summary>
        /// <param name="property">Propertyname</param>
        void NotifyPropertyChanging(string property);
        /// <summary>
        /// Fires an Event after an Property is changed.
        /// </summary>
        /// <param name="property">Propertyname</param>
        void NotifyPropertyChanged(string property);

        IKistlContext Context { get; }
        void AttachToContext(IKistlContext ctx);

        void DetachFromContext(IKistlContext ctx);
    }

    /// <summary>
    /// DataObject Interface
    /// </summary>
    public interface IDataObject : IPersistenceObject, INotifyPropertyChanged, ICloneable
    {
        /// <summary>
        /// Type of this Object
        /// </summary>
        ObjectType Type { get; }

        /// <summary>
        /// Not implemented yet.
        /// </summary>
        void NotifyChange();

        /// <summary>
        /// Fires an Event before an Object is saved.
        /// </summary>
        void NotifyPreSave();
        /// <summary>
        /// Fires an Event after an Object is saved.
        /// </summary>
        void NotifyPostSave();

        /// <summary>
        /// Copies the current content to a other Object. Used by clone.
        /// </summary>
        /// <param name="obj">Object to copy Content to.</param>
        void CopyTo(IDataObject obj);
    }

    /// <summary>
    /// Collection Entry Interface. A Collection Entry is a "connection" Object between other Data Objects 
    /// (ObjectReferenceProperty, IsList=true) or just a simple Collection (eg. StringProperty, IsList=true).
    /// </summary>
    public interface ICollectionEntry : IPersistenceObject
    {
        /// <summary>
        /// Copies the current content to a other Object. Used by clone.
        /// </summary>
        /// <param name="obj">Object to copy Content to.</param>
        void CopyTo(ICollectionEntry obj);
    }

    public interface ICollectionEntry<VALUE, PARENT> : ICollectionEntry
    {
        VALUE Value { get; set; }
        PARENT Parent { get; set; }
        int fk_Parent { get; set; }
    }

    /// <summary>
    /// XML Collection Interface. Used to serialize collection of Objects to XML. Implemented by the Generator.
    /// </summary>
    public interface IXmlObjectCollection
    {
        /// <summary>
        /// List of Objects
        /// </summary>
        List<object> Objects { get; }
    }

    /// <summary>
    /// XML Object Interface. Used to serialize Objects to XML. Implemented by the Generator.
    /// </summary>
    public interface IXmlObject
    {
        /// <summary>
        /// Object to serialize.
        /// </summary>
        object Object { get; set; }
    }

    /// <summary>
    /// Return Arguments for a Method Call Event
    /// </summary>
    public class MethodReturnEventArgs<T>
    {
        public T Result { get; set; }
    }

    /// <summary>
    /// ToString Event Delegate. Wird von den Datenobjekten gefeuert
    /// </summary>
    /// <typeparam name="T">Type of the implementing Object.</typeparam>
    /// <param name="obj">Object that has fired this Event.</param>
    /// <param name="e">Method return Arguments.</param>
    public delegate void ToStringHandler<T>(T obj, MethodReturnEventArgs<string> e) where T : class, IDataObject, new();

    /// <summary>
    /// Handler for Custom Save Events. TODO: Außer SetObject hat's noch niemand implementiert.
    /// </summary>
    /// <typeparam name="T">Type of the implementing Object.</typeparam>
    /// <param name="ctx">Current IKistlContext</param>
    /// <param name="obj">>Object that has fired this Event.</param>
    public delegate void ObjectEventHandler<T>(T obj) where T : class, IDataObject, new();
}
