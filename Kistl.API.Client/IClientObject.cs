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
    internal interface IClientObject
    {
        /// <summary>
        /// Objekt holen
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        BaseClientDataObject GetObject(ObjectType type, int ID);
        /// <summary>
        /// List holen
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable GetList(ObjectType type);
        /// <summary>
        /// List holen
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable GetListOf(ObjectType type, int ID, string propName);
        /// <summary>
        /// Objekt speichern
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        BaseClientDataObject SetObject(ObjectType type, IDataObject obj);
    }

    /// <summary>
    /// Handler für Client Custom Events. TODO: Das feuern muss noch implementiert werden.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public delegate void ClientObjectHandler<T>(T obj) where T : class, IDataObject, new();


    internal class ClientObjectFactory
    {
        /// <summary>
        /// Helper Function for generic access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IClientObject GetClientObject()
        {
            Type t = typeof(ClientObject<,>);
            Type xmlCollection = Type.GetType("Kistl.API.XMLObjectCollection, Kistl.Objects.Client");
            Type xmlObj = Type.GetType("Kistl.API.XMLObject, Kistl.Objects.Client");

            Type result = t.MakeGenericType(xmlCollection, xmlObj);

            IClientObject obj = Activator.CreateInstance(result) as IClientObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }

        /// <summary>
        /// Helper Function for generic access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static BaseClientDataObject GetObject(ObjectType type)
        {
            if (type == null) throw new ArgumentException("Type is null");
            if (string.IsNullOrEmpty(type.FullNameClientDataObject)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type.FullNameClientDataObject);
            if (t == null) throw new ApplicationException("Invalid Type " + type);

            BaseClientDataObject obj = Activator.CreateInstance(t) as BaseClientDataObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }
    }


    /// <summary>
    /// Basis Client BL implementierung. Erzeugt und verwaltet typisierte Objekte.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ClientObject<XMLCOLLECTION, XMLOBJECT> : IClientObject 
        where XMLCOLLECTION : IXmlObjectCollection, new()
        where XMLOBJECT : IXmlObject, new()
    {
        public ClientObject()
        {
        }

        #region IClientObject Members
        public BaseClientDataObject GetObject(ObjectType type, int ID)
        {
            if (ID == Helper.INVALIDID) return null;
            return Proxy.Service.GetObject(type, ID).FromXmlString<XMLOBJECT>().Object as BaseClientDataObject;
        }

        public IEnumerable GetList(ObjectType type)
        {
            return Proxy.Service.GetList(type).FromXmlString<XMLCOLLECTION>().Objects;
        }

        public IEnumerable GetListOf(ObjectType type, int ID, string propName)
        {
            return Proxy.Service.GetListOf(type, ID, propName).FromXmlString<XMLCOLLECTION>().Objects;
        }

        public BaseClientDataObject SetObject(ObjectType type, IDataObject obj)
        {
            XMLOBJECT xml = new XMLOBJECT();
            xml.Object = (BaseClientDataObject)obj;
            return Proxy.Service.SetObject(type, xml.ToXmlString()).FromXmlString<XMLOBJECT>().Object as BaseClientDataObject;
        }
        #endregion
    }
}
