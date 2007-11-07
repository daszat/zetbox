using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Kistl.API
{
    /// <summary>
    /// Interface für das Client BL Objekt.
    /// </summary>
    public interface IClientObject
    {
        /// <summary>
        /// XML in typisiertes Objektarray umwandeln.
        /// Wird von der GUI benötigt.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        IEnumerable GetArrayFromXML(string xml);
        /// <summary>
        /// XML in typisiertes Objekt umwandeln
        /// Wird von der GUI benötigt.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        IDataObject GetObjectFromXML(string xml);
        /// <summary>
        /// Neues Objekt erzeugen
        /// Wird von der GUI benötigt.
        /// </summary>
        /// <returns></returns>
        IDataObject CreateNew();
    }

    /// <summary>
    /// Handler für Client Custom Events. TODO: Das feuern muss noch implementiert werden.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public delegate void ClientObjectHandler<T>(T obj) where T : class, IDataObject, new();

    /// <summary>
    /// Basis Client BL implementierung. Erzeugt und verwaltet typisierte Objekte.
    /// TODO: Die Servicemethoden ebenfalls implementieren (Service über ein Kontext-Objekt holen)
    /// Damit erhält man typisierte Service Calls.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClientObject<T> : IClientObject where T : class, IDataObject, new()
    {
        /// <summary>
        /// Events registrieren
        /// </summary>
        public ClientObject()
        {
            API.CustomActionsManagerFactory.Current.AttachEvents(this);
        }

        /// <summary>
        /// XML in typisiertes Objektarray umwandeln.
        /// Wird von der GUI benötigt.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public IEnumerable GetArrayFromXML(string xml)
        {
            IEnumerable result = xml.FromXmlString<List<T>>();
            return result;
        }

        /// <summary>
        /// XML in typisiertes Objekt umwandeln
        /// Wird von der GUI benötigt.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public IDataObject GetObjectFromXML(string xml)
        {
            T obj = xml.FromXmlString<T>();
            return obj;
        }

        /// <summary>
        /// Neues Objekt erzeugen
        /// Wird von der GUI benötigt.
        /// </summary>
        /// <returns></returns>
        public IDataObject CreateNew()
        {
            T obj = new T();
            return obj;
        }
    }
}
