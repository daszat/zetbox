using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Xml;
using System.Reflection;
using System.ComponentModel;
using System.Data.Metadata.Edm;
using Kistl.API;
using Kistl.API.Server;

namespace Kistl.Server
{
    /// <summary>
    /// Interface für das Server BL Objekt.
    /// </summary>
    internal interface IServerObjectHandler
    {
        /// <summary>
        /// Implementiert den GetList Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        IEnumerable GetList(int maxListCount, Expression filter, Expression orderBy);

        /// <summary>
        /// Implementiert den GetListOf Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        IEnumerable GetListOf(int ID, string property);

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        IDataObject GetObject(int ID);
    }

    internal interface IServerObjectSetHandler
    {
        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        IEnumerable<IDataObject> SetObjects(IEnumerable<IDataObject> objects);
    }

    /// <summary>
    /// Factory for Server Object Handlers
    /// </summary>
    internal static class ServerObjectHandlerFactory
    {
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

                Type t = typeof(ServerObjectHandler<>);
                Type result = t.MakeGenericType(type);

                IServerObjectHandler obj = Activator.CreateInstance(result) as IServerObjectHandler;
                if (obj == null) throw new ArgumentOutOfRangeException("Cannot create instance of Type " + type.FullName);

                return obj;
            }
        }

        public static IServerObjectSetHandler GetServerObjectSetHandler()
        {
            return new ServerObjectSetHandler();
        }
    }

    /// <summary>
    /// Basis Objekt für die generische Server BL. Implementiert Linq
    /// Das ist nur für den generischen Teil gedacht, alle anderen Custom Actions
    /// können mit Linq auf den Context direkt zugreifen, da die Actions am Objekt & am Context
    /// selbst implementiert sind
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ServerObjectHandler<T> : IServerObjectHandler
        where T : IDataObject
    {
        /// <summary>
        /// Events registrieren
        /// </summary>
        public ServerObjectHandler()
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

                //using (IKistlContext ctx = KistlDataContext.GetContext())
                //{
                //    // If ObjectReferenc is a List -> convert data
                //    Kistl.App.Base.BackReferenceProperty prop = (Kistl.App.Base.BackReferenceProperty)obj.GetObjectClass(ctx)
                //        .GetProperty(property);
                //    if (prop.ReferenceProperty.IsList)
                //    {
                //        List<IDataObject> result = new List<IDataObject>();
                //        list.ForEach<ICollectionEntry>(ce => result.Add(ce.GetPropertyValue<IDataObject>("Parent")));
                //        return result;
                //    }
                //    else
                //    {
                //        return list;
                //    }
                //}
            }
        }

        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private static T GetObjectInstance(int ID)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(string.Format("ID = {0}", ID)))
            {
                if (ID < Kistl.API.Helper.INVALIDID)
                {
                    // new object -> look in current context
                    ObjectContext ctx = (ObjectContext)KistlDataContext.Current;
                    return (T)ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added)
                        .FirstOrDefault(e => e.Entity is IDataObject && ((IDataObject)e.Entity).ID == ID).Entity;
                }
                else
                {
                    return KistlDataContext.Current.GetQuery<T>().FirstOrDefault<T>(a => a.ID == ID);
                }
            }
        }

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

    internal class ServerObjectSetHandler : IServerObjectSetHandler
    {
        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public IEnumerable<IDataObject> SetObjects(IEnumerable<IDataObject> objects)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                foreach (IDataObject obj in objects)
                {
                    // Fist of all, Attach Object
                    KistlDataContext.Current.Attach(obj);

                    if (obj.ObjectState != DataObjectState.Deleted)
                    {
                        MarkEveryPropertyAsModified(obj as EntityObject);
                    }
                }
                foreach (IDataObject obj in objects)
                {
                    if (obj.ObjectState != DataObjectState.Deleted)
                    {
                        UpdateRelationships(obj);
                    }
                }

                KistlDataContext.Current.SubmitChanges();

                // TODO: Detect changes made by server nethod calls
                return objects.Where(o => o.ObjectState != DataObjectState.Deleted);
            }
        }

        /// <summary>
        /// Update Relationships
        /// TODO: Bad hack, weil EF keine Relationen serialisieren kann
        /// Note: Change Tracking is "enabled" by setting every Reference
        /// </summary>
        /// <param name="obj"></param>
        private static void UpdateRelationships(IDataObject obj)
        {
            using (IKistlContext ctx = KistlDataContext.GetContext())
            {
                Kistl.App.Base.ObjectClass objClass = obj.GetObjectClass(ctx);
                while (objClass != null)
                {
                    foreach (Kistl.App.Base.ObjectReferenceProperty p in objClass.Properties.OfType<Kistl.App.Base.ObjectReferenceProperty>())
                    {
                        if (!p.IsList)
                        {
                            int? fk = obj.GetPrivateFieldValue<int?>("_fk_" + p.PropertyName);

                            if (fk != null)
                            {
                                IServerObjectHandler so = ServerObjectHandlerFactory.GetServerObjectHandler(p.GetPropertyType());
                                IDataObject other = so.GetObject(fk.Value);
                                if (other == null) throw new InvalidOperationException(string.Format("UpdateRelationships: Cannot find Object {0}:{1}", p.GetPropertyType().FullName, fk));
                                obj.SetPropertyValue<IDataObject>(p.PropertyName, other);
                            }
                            else
                            {
                                obj.SetPropertyValue<IDataObject>(p.PropertyName, null);
                            }
                        }
                        else
                        {
                            // Liste
                            foreach (ICollectionEntry ce in obj.GetPropertyValue<IEnumerable>(p.PropertyName + "Impl"))
                            {
                                int fk = ce.GetPrivateFieldValue<int>("_fk_Value");

                                IServerObjectHandler so = ServerObjectHandlerFactory.GetServerObjectHandler(p.GetPropertyType());
                                IDataObject other = so.GetObject(fk);
                                ce.SetPropertyValue<IDataObject>("Value", other);
                            }
                        }
                    }

                    objClass = objClass.BaseObjectClass;
                }
            }
        }

        /// <summary>
        /// TODO: Bad Hack!
        /// Es gibt angeblich jetzt eine bessere MEthode
        /// </summary>
        /// <param name="obj"></param>
        private static void MarkEveryPropertyAsModified(EntityObject obj)
        {
            // TODO: Bad Hack!!
            if (obj.EntityState.In(EntityState.Modified, EntityState.Unchanged))
            {
                ObjectStateEntry stateEntry = ((KistlDataContextEntityFramework)KistlDataContext.Current).ObjectStateManager.GetObjectStateEntry(obj.EntityKey);
                MetadataWorkspace workspace = ((KistlDataContextEntityFramework)KistlDataContext.Current).MetadataWorkspace;
                string typename;
                if (obj is ICollectionEntry)
                {
                    typename = obj.GetType().Name;
                    typename = typename.Substring(0, typename.Length - 4);
                }
                else
                {
                    typename = ((IPersistenceObject)obj).GetInterfaceType().Name;
                }

                EntityType entityType = workspace.GetItem<EntityType>("Model." + typename, DataSpace.CSpace);

                foreach (EdmProperty property in entityType.Properties)
                {
                    stateEntry.SetModifiedProperty(property.Name);
                }
            }

            if (obj is IDataObject)
            {
                using (IKistlContext ctx = KistlDataContext.GetContext())
                {
                    Kistl.App.Base.ObjectClass objClass = (obj as IDataObject).GetObjectClass(ctx);
                    while (objClass != null)
                    {
                        foreach (Kistl.App.Base.ValueTypeProperty p in objClass.Properties.OfType<Kistl.App.Base.ValueTypeProperty>().Where(p => p.IsList))
                        {
                            foreach (ICollectionEntry ce in obj.GetPropertyValue<IEnumerable>(p.PropertyName + "Impl"))
                            {
                                EntityObject ce_eo = (EntityObject)ce;
                                MarkEveryPropertyAsModified(ce_eo);
                            }
                        }
                        objClass = objClass.BaseObjectClass;
                    }
                }
            }
        }
    }
}
