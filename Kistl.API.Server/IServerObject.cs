using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Xml;
using System.Reflection;

namespace Kistl.API.Server
{
    /// <summary>
    /// Interface für das Server BL Objekt.
    /// </summary>
    public interface IServerObject
    {
        /// <summary>
        /// Implementiert den GetList Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        string GetList();

        /// <summary>
        /// Implementiert den GetListOf Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        string GetListOf(int ID, string property);

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        string GetObject(int ID);

        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        string SetObject(string xml);
    }

    /// <summary>
    /// Basis Objekt für Server BL. Implementiert Linq to SQL.
    /// TODO: Für die Custom Actions ist das noch nicht gut genug typisiert. 
    /// Es reicht einmal für den generischen Teil.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServerObject<T> : IServerObject where T : class, IDataObject, new()
    {
        /// <summary>
        /// Events registrieren
        /// </summary>
        public ServerObject()
        {
        }

        /// <summary>
        /// Implementiert den GetList Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public string GetList()
        {
            var result = from a in KistlDataContext.Current.GetTable<T>()
                         select a;

            List<T> list = result.ToList<T>();
            return list.ToXmlString();
        }

        /// <summary>
        /// Implementiert den GetListOf Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetListOf(int ID, string property)
        {
            if (ID == API.Helper.INVALIDID) throw new ArgumentException("ID must not be invalid");
            T obj = GetObjectInstance(ID);
            if (obj == null) throw new ApplicationException("Object not found");

            PropertyInfo pi = typeof(T).GetProperty(property);
            if (pi == null) throw new ArgumentException("Property does not exist");

            IEnumerable list = pi.GetValue(obj, null) as IEnumerable;
            return list.ToXmlString();
        }

        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T GetObjectInstance(int ID)
        {
            var result = from a in KistlDataContext.Current.GetTable<T>()
                         where a.ID == ID
                         select a;
            return result.Single<T>();
        }

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetObject(int ID)
        {
            T obj = GetObjectInstance(ID);
            return obj.ToXmlString();
        }

        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string SetObject(string xml)
        {
            T obj = xml.FromXmlString<T>();

            if (obj.ID != API.Helper.INVALIDID)
            {
                //KistlDataContext.Current.Attach(obj);
            }
            else
            {
                KistlDataContext.Current.AddObject(typeof(T).Name, obj);
            }
            KistlDataContext.Current.SubmitChanges();

            return obj.ToXmlString();
        }
    }
}
