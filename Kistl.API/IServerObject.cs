using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Linq;
using System.Xml;
using System.Reflection;

namespace Kistl.API
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
        string GetList(DataContext ctx);

        /// <summary>
        /// Implementiert den GetListOf Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        string GetListOf(DataContext ctx, int ID, string property);

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        string GetObject(DataContext ctx, int ID);

        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        string SetObject(DataContext ctx, string xml);
    }

    /// <summary>
    /// Handler für Server Custom Events. TODO: Außer SetObject hat's noch niemand implementiert.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ctx"></param>
    /// <param name="obj"></param>
    public delegate void ServerObjectHandler<T>(DataContext ctx, T obj) where T : class, IDataObject, new();

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
            API.ObjectBrokerFactory.Current.AttachEvents(this);
        }

        /// <summary>
        /// Implementiert den GetList Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public string GetList(DataContext ctx)
        {
            var result = from a in ctx.GetTable<T>()
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
        public string GetListOf(DataContext ctx, int ID, string property)
        {
            if (ID == API.Helper.INVALIDID) throw new ArgumentException("ID must not be invalid");
            T obj = GetObjectInstance(ctx, ID);
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
        public T GetObjectInstance(DataContext ctx, int ID)
        {
            var result = from a in ctx.GetTable<T>()
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
        public string GetObject(DataContext ctx, int ID)
        {
            T obj = GetObjectInstance(ctx, ID);
            return obj.ToXmlString();
        }

        public event ServerObjectHandler<T> OnPreSetObject = null;
        public event ServerObjectHandler<T> OnPostSetObject = null;

        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string SetObject(DataContext ctx, string xml)
        {
            T obj = xml.FromXmlString<T>();

            if (OnPreSetObject != null) OnPreSetObject(ctx, obj);

            if (obj.ID != API.Helper.INVALIDID)
            {
                ctx.GetTable<T>().Attach(obj, true);
            }
            else
            {
                ctx.GetTable<T>().Add(obj);
            }
            ctx.SubmitChanges();

            if (OnPostSetObject != null) OnPostSetObject(ctx, obj);

            return obj.ToXmlString();
        }
    }
}
