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
using System.ComponentModel;
using Kistl.API;
using Kistl.API.Server;

namespace Kistl.API.Server
{
    /// <summary>
    /// Interface für das Server BL Objekt.
    /// </summary>
    public interface IServerObjectHandler
    {
        /// <summary>
        /// Implementiert den GetList Befehl.
        /// </summary>
        /// <param name="maxListCount"></param>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IEnumerable GetList(int maxListCount, Expression filter, Expression orderBy);

        /// <summary>
        /// Implementiert den GetListOf Befehl.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        IEnumerable GetListOf(int ID, string property);

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        IDataObject GetObject(int ID);
    }

    public interface IServerObjectSetHandler
    {
        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        IEnumerable<IDataObject> SetObjects(IEnumerable<IDataObject> objects);
    }

    /// <summary>
    /// Factory for Server Object Handlers
    /// </summary>
    public static class ServerObjectHandlerFactory
    {
        private static Type _ServerObjectHandlerType = null;
        private static Type _ServerObjectSetHandlerType = null;

        /// <summary>
        /// Returns a Object Handler for the given Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IServerObjectHandler GetServerObjectHandler(Type type)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(type.ToString()))
            {
                if (type == null) throw new ArgumentNullException("Type is null");

                lock (typeof(ServerObjectHandlerFactory))
                {
                    if (_ServerObjectHandlerType == null)
                    {
                        _ServerObjectHandlerType = Type.GetType(Configuration.KistlConfig.Current.Server.ServerObjectHandlerType);
                        if (_ServerObjectHandlerType == null)
                        {
                            throw new Configuration.ConfigurationException(string.Format("Unable to load Type '{0}' for IServerObjectHandler. Check your Configuration '/Server/ServerObjectHandlerType'.", API.Configuration.KistlConfig.Current.Server.ServerObjectHandlerType));
                        }
                    }
                }
                Type result = _ServerObjectHandlerType.MakeGenericType(type);

                IServerObjectHandler obj = Activator.CreateInstance(result) as IServerObjectHandler;
                if (obj == null) throw new ArgumentOutOfRangeException("Cannot create instance of Type " + type.FullName);

                return obj;
            }
        }

        public static IServerObjectSetHandler GetServerObjectSetHandler()
        {
            lock (typeof(ServerObjectHandlerFactory))
            {
                if (_ServerObjectSetHandlerType == null)
                {
                    _ServerObjectSetHandlerType = Type.GetType(API.Configuration.KistlConfig.Current.Server.ServerObjectSetHandlerType);
                    if (_ServerObjectSetHandlerType == null)
                    {
                        throw new Configuration.ConfigurationException(string.Format("Unable to load Type '{0}' for IServerObjectSetHandler. Check your Configuration '/Server/ServerObjectSetHandlerType'.", API.Configuration.KistlConfig.Current.Server.ServerObjectSetHandlerType));
                    }
                }
            }
            object obj = Activator.CreateInstance(_ServerObjectSetHandlerType);
            if (!(obj is IServerObjectSetHandler))
            {
                throw new Configuration.ConfigurationException(string.Format("Type '{0}' is not a IKistlContext object. Check your Configuration '/Server/ServerObjectSetHandlerType'.", API.Configuration.KistlConfig.Current.Server.ServerObjectSetHandlerType));
            }
            return (IServerObjectSetHandler)obj;
        }
    }

    /// <summary>
    /// Basis Objekt für die generische Server BL. Implementiert Linq
    /// Das ist nur für den generischen Teil gedacht, alle anderen Custom Actions
    /// können mit Linq auf den Context direkt zugreifen, da die Actions am Objekt & am Context
    /// selbst implementiert sind
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseServerObjectHandler<T> : IServerObjectHandler
        where T : IDataObject
    {
        /// <summary>
        /// Events registrieren
        /// </summary>
        public BaseServerObjectHandler()
        {
        }

        /// <summary>
        /// Implementiert den GetList Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public IEnumerable GetList(int maxListCount, Expression filter, Expression orderBy)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                if (maxListCount > Kistl.API.Helper.MAXLISTCOUNT)
                {
                    maxListCount = Kistl.API.Helper.MAXLISTCOUNT;
                }

                var result = KistlDataContext.Current.GetQuery<T>();

                if (filter != null)
                {
                    result = (IQueryable<T>)result.AddFilter(filter);
                }

                if (orderBy != null)
                {
                    result = result.AddOrderBy<T>(orderBy);
                }

                return result.Take(maxListCount);
            }
        }

        /// <summary>
        /// Since IsList properties are not automatically transferred,
        /// GetListOf can be used to get the list of values in the property 
        /// <code>property</code> of the object with the <code>ID</code>
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public IEnumerable GetListOf(int ID, string property)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(string.Format("ID = {0}, Property = {1}", ID, property)))
            {
                if (ID <= API.Helper.INVALIDID) throw new ArgumentException("ID must not be invalid");
                T obj = GetObjectInstance(ID);
                if (obj == null) throw new ArgumentOutOfRangeException("ID", "Object not found");

                IEnumerable list = (IEnumerable)obj.GetPropertyValue <IEnumerable>(property);
                return list;
            }
        }

        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected abstract T GetObjectInstance(int ID);
        //{
        //    using (TraceClient.TraceHelper.TraceMethodCall(string.Format("ID = {0}", ID)))
        //    {
        //        if (ID < Kistl.API.Helper.INVALIDID)
        //        {
        //             new object -> look in current context
        //            ObjectContext ctx = (ObjectContext)KistlDataContext.Current;
        //            return (T)ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added)
        //                .FirstOrDefault(e => e.Entity is IDataObject && ((IDataObject)e.Entity).ID == ID).Entity;
        //        }
        //        else
        //        {
        //            return KistlDataContext.Current.GetQuery<T>().FirstOrDefault<T>(a => a.ID == ID);
        //        }
        //    }
        //}

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IDataObject GetObject(int ID)
        {
            return GetObjectInstance(ID);
        }
    }

    public class BaseServerObjectSetHandler : IServerObjectSetHandler
    {
        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public virtual IEnumerable<IDataObject> SetObjects(IEnumerable<IDataObject> objects)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                KistlDataContext.Current.SubmitChanges();

                // TODO: Detect changes made by server nethod calls
                return objects.Where(o => o.ObjectState != DataObjectState.Deleted);
            }
        }
    }
}
