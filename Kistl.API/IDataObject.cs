using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace Kistl.API
{
    public enum DataObjectState
    {
        New,
        Modified,
        Unmodified,
        Deleted,
    }

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
        /// Typ des Objectes
        /// </summary>
        ObjectType Type { get; }

        DataObjectState ObjectState { get; set; }

        /// <summary>
        /// Zum Melden, dass sich das Datenobjekt geänder hat.
        /// </summary>
        void NotifyChange();

        void NotifyPreSave();
        void NotifyPostSave();

        void ToStream(System.IO.BinaryWriter sw);
        void FromStream(IKistlContext ctx, System.IO.BinaryReader sr);
    }

    public interface ICollectionEntry
    {
        /// <summary>
        /// Jeder hat eine ID
        /// </summary>
        int ID { get; set; }

        void ToStream(System.IO.BinaryWriter sw);
        void FromStream(IKistlContext ctx, System.IO.BinaryReader sr);
    }

    public interface IXmlObjectCollection
    {
        List<object> Objects { get; }
    }

    public interface IXmlObject
    {
        object Object { get; set; }
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
}
