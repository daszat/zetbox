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
        IEnumerable GetList(IKistlContext ctx, int maxListCount, Expression filter, List<Expression> orderBy);

        /// <summary>
        /// Implementiert den GetListOf Befehl.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        IEnumerable GetListOf(IKistlContext ctx, int ID, string property);

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        IDataObject GetObject(IKistlContext ctx, int ID);
    }

    public interface IServerObjectSetHandler
    {
        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        IEnumerable<IPersistenceObject> SetObjects(IKistlContext ctx, IEnumerable<IPersistenceObject> objects);
    }

    public interface IServerCollectionHandler
    {
        IEnumerable<IRelationCollectionEntry> GetCollectionEntries(IKistlContext ctx, int relId, RelationEndRole endRole, int parentId);
    }

    /// <summary>
    /// Factory for Server Object Handlers
    /// </summary>
    public static class ServerObjectHandlerFactory
    {
        private static Type _ServerObjectHandlerType = null;
        private static Type _ServerObjectSetHandlerType = null;
        private static Type _ServerCollectionHandlerType = null;

        /// <summary>
        /// Returns a Object Handler for the given Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IServerObjectHandler GetServerObjectHandler(Type type)
        {
            if (type == null) throw new ArgumentNullException("Type is null");

            lock (typeof(ServerObjectHandlerFactory))
            {
                if (_ServerObjectHandlerType == null)
                {
                    _ServerObjectHandlerType = Type.GetType(ApplicationContext.Current.Configuration.Server.ServerObjectHandlerType);
                    if (_ServerObjectHandlerType == null)
                    {
                        throw new Configuration.ConfigurationException(string.Format("Unable to load Type '{0}' for IServerObjectHandler. Check your Configuration '/Server/ServerObjectHandlerType'.", ApplicationContext.Current.Configuration.Server.ServerObjectHandlerType));
                    }
                }
            }
            Type result = _ServerObjectHandlerType.MakeGenericType(type);

            IServerObjectHandler obj = Activator.CreateInstance(result) as IServerObjectHandler;
            if (obj == null) throw new ArgumentOutOfRangeException("Cannot create instance of Type " + type.FullName);

            return obj;
        }

        public static IServerObjectSetHandler GetServerObjectSetHandler()
        {
            lock (typeof(ServerObjectHandlerFactory))
            {
                if (_ServerObjectSetHandlerType == null)
                {
                    _ServerObjectSetHandlerType = Type.GetType(ApplicationContext.Current.Configuration.Server.ServerObjectSetHandlerType);
                    if (_ServerObjectSetHandlerType == null)
                    {
                        throw new Configuration.ConfigurationException(string.Format("Unable to load Type '{0}' for IServerObjectSetHandler. Check your Configuration '/Server/ServerObjectSetHandlerType'.", ApplicationContext.Current.Configuration.Server.ServerObjectSetHandlerType));
                    }
                }
            }
            object obj = Activator.CreateInstance(_ServerObjectSetHandlerType);
            if (!(obj is IServerObjectSetHandler))
            {
                throw new Configuration.ConfigurationException(string.Format("Type '{0}' is not a IServerObjectSetHandler object. Check your Configuration '/Server/ServerObjectSetHandlerType'.", ApplicationContext.Current.Configuration.Server.ServerObjectSetHandlerType));
            }
            return (IServerObjectSetHandler)obj;
        }

        public static IServerCollectionHandler GetServerCollectionHandler(Type aType, Type bType, RelationEndRole endRole)
        {
            aType = aType.ToImplementationType();
            bType = bType.ToImplementationType();

            lock (typeof(ServerObjectHandlerFactory))
            {
                if (_ServerCollectionHandlerType == null)
                {
                    _ServerCollectionHandlerType = Type.GetType(ApplicationContext.Current.Configuration.Server.ServerCollectionHandlerType);
                    if (_ServerCollectionHandlerType == null)
                    {
                        throw new Configuration.ConfigurationException(string.Format("Unable to load Type '{0}' for IServerCollectionHandler. Check your Configuration '/Server/ServerCollectionHandlerType'.", ApplicationContext.Current.Configuration.Server.ServerCollectionHandlerType));
                    }
                }
            }

            // dynamically translate generic types into provider-known types
            Type[] genericArgs;
            if (endRole == RelationEndRole.A)
            {
                genericArgs = new Type[] { aType, bType, aType, bType };
            }
            else
            {
                genericArgs = new Type[] { aType, bType, bType, aType };
            }

            Type result = _ServerCollectionHandlerType.MakeGenericType(genericArgs);
            object obj = Activator.CreateInstance(result);
            if (!(obj is IServerCollectionHandler))
            {
                throw new Configuration.ConfigurationException(string.Format("Type '{0}' is not a IKistlContext object. Check your Configuration '/Server/ServerCollectionHandlerType'.", ApplicationContext.Current.Configuration.Server.ServerCollectionHandlerType));
            }
            return (IServerCollectionHandler)obj;
        }
    }

    /// <summary>
    /// Basis Objekt für die generische Server BL. Implementiert Linq
    /// Das ist nur für den generischen Teil gedacht, alle anderen Custom Actions
    /// können mit Linq auf den Context direkt zugreifen, da die Actions am Objekt &amp; am Context
    /// selbst implementiert sind
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseServerObjectHandler<T> : IServerObjectHandler
        where T : class, IDataObject
    {
        /// <summary>
        /// Events registrieren
        /// </summary>
        public BaseServerObjectHandler()
        {
        }

        public IEnumerable GetList(IKistlContext ctx, int maxListCount, Expression filter, List<Expression> orderBy)
        {
            if (maxListCount > Kistl.API.Helper.MAXLISTCOUNT)
            {
                maxListCount = Kistl.API.Helper.MAXLISTCOUNT;
            }

            var result = ctx.GetQuery<T>();

            if (filter != null)
            {
                result = (IQueryable<T>)result.AddFilter(filter);
            }

            if (orderBy != null)
            {
                bool first = true;
                foreach (var o in orderBy)
                {
                    if (first) result = result.AddOrderBy<T>(o);
                    else result = result.AddThenBy<T>(o);
                    first = false;
                }
            }

            return result.Take(maxListCount);
        }

        /// <summary>
        /// Since IsList properties are not automatically transferred,
        /// GetListOf can be used to get the list of values in the property 
        /// <code>property</code> of the object with the <code>ID</code>
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns>the list of values in the property</returns>
        public IEnumerable GetListOf(IKistlContext ctx, int ID, string property)
        {
            if (ID <= API.Helper.INVALIDID) throw new ArgumentException("ID must not be invalid");
            T obj = GetObjectInstance(ctx, ID);
            if (obj == null) throw new ArgumentOutOfRangeException("ID", "Object not found");

            IEnumerable list = (IEnumerable)obj.GetPropertyValue<IEnumerable>(property);
            return list;
        }

        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>a typed object</returns>
        protected abstract T GetObjectInstance(IKistlContext ctx, int ID);
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
        /// <param name="ID"></param>
        /// <returns></returns>
        public IDataObject GetObject(IKistlContext ctx, int ID)
        {
            return GetObjectInstance(ctx, ID);
        }
    }

    public class BaseServerObjectSetHandler : IServerObjectSetHandler
    {
        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public virtual IEnumerable<IPersistenceObject> SetObjects(IKistlContext ctx, IEnumerable<IPersistenceObject> objList)
        {
            var objects = objList.Cast<BaseServerPersistenceObject>().ToList();
            var entityObjects = new List<IPersistenceObject>();

            // Fist of all, attach new Objects
            foreach (var obj in objects.Where(o => o.ClientObjectState == DataObjectState.New))
            {
                ctx.Attach(obj);
                if (!entityObjects.Contains(obj)) entityObjects.Add(obj);
            }

            // then apply changes
            foreach (var obj in objects.Where(o => o.ClientObjectState == DataObjectState.Modified))
            {
                var ctxObj = ctx.FindPersistenceObject(obj.GetInterfaceType(), obj.ID);
                ((BasePersistenceObject)ctxObj).RecordNotifications();
                ctxObj.ApplyChangesFrom(obj);
                if (!entityObjects.Contains(ctxObj)) entityObjects.Add(ctxObj);
            }

            // then update references
            foreach (var obj in objects.Where(o => o.ClientObjectState != DataObjectState.Deleted))
            {
                var ctxObj = ctx.FindPersistenceObject(obj.GetInterfaceType(), obj.ID);
                ((BasePersistenceObject)ctxObj).RecordNotifications();
                ctxObj.ReloadReferences();
                if (!entityObjects.Contains(ctxObj)) entityObjects.Add(ctxObj);
            }

            // then delete objects
            foreach (var obj in objects.Where(o => o.ClientObjectState == DataObjectState.Deleted))
            {
                var ctxObj = ctx.FindPersistenceObject(obj.GetInterfaceType(), obj.ID);
                ctx.Delete(ctxObj);
            }

            // Playback notifications
            foreach (var obj in entityObjects.Cast<BasePersistenceObject>())
            {
                obj.PlaybackNotifications();
            }

            ctx.SubmitChanges();

            // TODO: Detect changes made by server method calls
            return entityObjects;
        }
    }
}
