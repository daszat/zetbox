using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Kistl.API;
using System.Reflection;

namespace Kistl.API.Client
{
    /// <summary>
    /// Interface für das Client BL Objekt.
    /// </summary>
    public interface IClientObject
    {
        /// <summary>
        /// Neues Objekt erzeugen
        /// Wird von der GUI benötigt.
        /// </summary>
        /// <returns></returns>
        IDataObject CreateNewGeneric();
        /// <summary>
        /// Objekt holen
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        IDataObject GetObjectGeneric(int ID);
        /// <summary>
        /// List holen
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable GetListGeneric();
        /// <summary>
        /// List holen
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable GetListOfGeneric(int ID, string propName);
        /// <summary>
        /// Objekt speichern
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        IDataObject SetObjectGeneric(IDataObject obj);
    }

    /// <summary>
    /// Handler für Client Custom Events. TODO: Das feuern muss noch implementiert werden.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public delegate void ClientObjectHandler<T>(T obj) where T : class, IDataObject, new();


    public class ClientObjectFactory
    {
        /// <summary>
        /// Helper Function for generic access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IClientObject GetClientObject(ObjectType type)
        {
            if (type == null) throw new ArgumentException("Type is null");
            if (string.IsNullOrEmpty(type.FullNameClientObject)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type.FullNameClientObject);
            if (t == null) throw new ApplicationException("Invalid Type " + type);

            IClientObject obj = Activator.CreateInstance(t) as IClientObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }
    }


    /// <summary>
    /// Basis Client BL implementierung. Erzeugt und verwaltet typisierte Objekte.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClientObject<T> : IClientObject where T : BaseDataObject, IDataObject, new()
    {
        /// <summary>
        /// Events registrieren
        /// </summary>
        public ClientObject()
        {
            // TODO: Add to cache
            _type = new ObjectType(typeof(T).Namespace, typeof(T).Name);
        }

        protected ObjectType _type = null;

        public ObjectType Type
        {
            get
            {
                return _type;
            }
        }

        #region IClientObject Members
        /// <summary>
        /// Neues Objekt erzeugen
        /// Wird von der GUI benötigt.
        /// </summary>
        /// <returns></returns>
        public IDataObject CreateNewGeneric()
        {
            T obj = new T();
            return obj;
        }

        public IDataObject GetObjectGeneric(int ID)
        {
            if (ID == Helper.INVALIDID) return null;
            return Proxy.Service.GetObject(Type, ID).FromXmlString<XMLObject>().Object;
        }

        public IEnumerable GetListGeneric()
        {
            return Proxy.Service.GetList(Type).FromXmlString<XMLObjectCollection>().Objects;
        }

        public IEnumerable GetListOfGeneric(int ID, string propName)
        {
            if (ID == Helper.INVALIDID) return null;
            // Client Methode holen
            // TODO: Cachen!
            MethodInfo mi = this.GetType().GetMethod("GetListOf" + propName);
            if (mi != null)
            {
                // Liste vom Server holen & den DataContext setzen.
                //string xml = Proxy.Service.GetListOf(Type, ID, propName);
                return mi.Invoke(this, new object[] { ID }) as IEnumerable;
            }
            else
            {
                throw new ApplicationException("Property " + propName + " not found");
            }
        }

        public IDataObject SetObjectGeneric(IDataObject obj)
        {
            XMLObject xml = new XMLObject();
            xml.Object = (BaseDataObject)obj;
            return Proxy.Service.SetObject(Type, xml.ToXmlString()).FromXmlString<XMLObject>().Object;
        }
        #endregion

        #region Typed Acces members
        /// <summary>
        /// Neues Objekt erzeugen
        /// Wird von der GUI benötigt.
        /// </summary>
        /// <returns></returns>
        public T CreateNew()
        {
            T obj = new T();
            return obj;
        }

        public T GetObject(int ID)
        {
            if (ID == Helper.INVALIDID) return null;
            return Proxy.Service.GetObject(Type, ID).FromXmlString<XMLObject>().Object as T;
        }

        public List<T> GetList()
        {
            return Proxy.Service.GetList(Type).FromXmlString<XMLObjectCollection>().ToList<T>();
        }

        public T SetObject(T obj)
        {
            XMLObject xml = new XMLObject();
            xml.Object = obj;
            string result = Proxy.Service.SetObject(Type, xml.ToXmlString());
            return result.FromXmlString<XMLObject>().Object as T;
        }        
        #endregion
    }
}
