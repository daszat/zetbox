using System;
using System.Collections.Generic;
using System.ComponentModel;

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

    public enum RelationType
    {
        one_n,
        n_m,
        one_one,        
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
        void ToStream(System.IO.BinaryWriter sw);
        /// <summary>
        /// Deserialize this Object from a BinaryReader
        /// </summary>
        /// <param name="sr">BinaryReader to deserialize to.</param>
        void FromStream(System.IO.BinaryReader sr);

    }

    /// <summary>
    /// Interface for all persistent objects.
    /// </summary>
    public interface IPersistenceObject : IStreamable, INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// Every Object has at least an ID
        /// </summary>
        int ID { get; }

        /// <summary>
        /// State of this Object.
        /// </summary>
        DataObjectState ObjectState { get; }

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

        /// <summary>
        /// Current Context.
        /// </summary>
        IKistlContext Context { get; }
        /// <summary>
        /// Attach this Object to a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to attach this Object to.</param>
        void AttachToContext(IKistlContext ctx);

        /// <summary>
        /// Detach this Object from a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to detach this Object from.</param>
        void DetachFromContext(IKistlContext ctx);

        /// <summary>
        /// Returns true if this Object is attached to a Context
        /// </summary>
        bool IsAttached { get; }

        /// <summary>
        /// Returns true if this Object is readonly
        /// </summary>
        bool IsReadonly { get; }

        /// <summary>
        /// Returns the most specific implemented IDataObject interface
        /// </summary>
        /// <returns></returns>
        Type GetInterfaceType();
    }

    /// <summary>
    /// DataObject Interface
    /// </summary>
    public interface IDataObject : IPersistenceObject, IDataErrorInfo
    {
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
    }

    public interface IStruct : IStreamable, ICloneable
    {
        void AttachToObject(IPersistenceObject obj, string property);
        void DetachFromObject(IPersistenceObject obj, string property);

        /// <summary>
        /// Returns true if this Object is readonly
        /// </summary>
        bool IsReadonly { get; }
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
        /// <summary>
        /// Result of the Method Call
        /// </summary>
        public T Result { get; set; }
    }

    /// <summary>
    /// ToString Event Delegate. Wird von den Datenobjekten gefeuert
    /// </summary>
    /// <typeparam name="T">Type of the implementing Object.</typeparam>
    /// <param name="obj">Object that has fired this Event.</param>
    /// <param name="e">Method return Arguments.</param>
    public delegate void ToStringHandler<T>(T obj, MethodReturnEventArgs<string> e) where T : IDataObject;

    /// <summary>
    /// Handler for Custom Save Events. TODO: Außer SetObject hat's noch niemand implementiert.
    /// </summary>
    /// <typeparam name="T">Type of the implementing Object.</typeparam>
    /// <param name="obj">>Object that has fired this Event.</param>
    public delegate void ObjectEventHandler<T>(T obj) where T : IDataObject;
}
