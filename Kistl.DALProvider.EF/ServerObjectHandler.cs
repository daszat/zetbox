using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.DALProvider.EF
{
    public class ServerObjectHandler<T>
        : BaseServerObjectHandler<T>
        where T : IDataObject
    {
        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected override T GetObjectInstance(IKistlContext ctx, int ID)
        {
            if (ID < Kistl.API.Helper.INVALIDID)
            {
                // new object -> look in current context
                ObjectContext efCtx = ((KistlDataContext)ctx).ObjectContext;
                return (T)efCtx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added)
                    .FirstOrDefault(e => e.Entity is IDataObject && ((IDataObject)e.Entity).ID == ID).Entity;
            }
            else
            {
                return ctx.GetQuery<T>().FirstOrDefault<T>(a => a.ID == ID);
            }
        }
    }

    public class ServerObjectSetHandler
        : BaseServerObjectSetHandler
    {
        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public override IEnumerable<IPersistenceObject> SetObjects(IKistlContext ctx, IEnumerable<IPersistenceObject> objects)
        {
            foreach (var obj in objects)
            {
                // Fist of all, Attach Object
                ctx.Attach(obj);

                if (obj.ObjectState != DataObjectState.Deleted)
                {
                    MarkEveryPropertyAsModified(ctx, obj);
                }
            }
            foreach (var obj in objects.OfType<IPersistenceObject>())
            {
                if (obj.ObjectState != DataObjectState.Deleted)
                {
                    obj.ReloadReferences();
                    //UpdateRelationships(ctx, obj);
                }
            }
            return base.SetObjects(ctx, objects);
        }

        /// <summary>
        /// Update Relationships
        /// Note: Change Tracking is "enabled" by setting every Reference
        /// </summary>
        /// <param name="obj"></param>
        protected static void UpdateRelationships(IKistlContext ctx, IDataObject obj)
        {
            using (IKistlContext frozenctx = FrozenContext.Single)
            {
                ObjectClass objClass = obj.GetObjectClass(frozenctx);
                while (objClass != null)
                {
                    var listProperties = objClass.Properties.OfType<ObjectReferenceProperty>()
                        .ToList().Where(p => p.HasStorage());
                    foreach (ObjectReferenceProperty p in listProperties)
                    {
                        if (!p.IsList)
                        {
                            int? fk = obj.GetPrivateFieldValue<int?>("_fk_" + p.PropertyName);

                            if (fk != null)
                            {
                                IServerObjectHandler so = ServerObjectHandlerFactory.GetServerObjectHandler(p.GetPropertyType());
                                IDataObject other = so.GetObject(ctx, fk.Value);
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
                            // List
                            foreach (ICollectionEntry ce in obj.GetPropertyValue<IEnumerable>(p.PropertyName + Kistl.API.Helper.ImplementationSuffix))
                            {
                                int fk = ce.GetPrivateFieldValue<int>("_fk_Value");

                                IServerObjectHandler so = ServerObjectHandlerFactory.GetServerObjectHandler(p.GetPropertyType());
                                IDataObject other = so.GetObject(ctx, fk);
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
        private static void MarkEveryPropertyAsModified(IKistlContext ctx, IPersistenceObject o)
        {
            IEntityStateObject obj = (IEntityStateObject)o;
            // TODO: Bad Hack!!
            if (obj.EntityState.In(EntityState.Modified, EntityState.Unchanged))
            {
                Kistl.DALProvider.EF.KistlDataContext efCtx = (KistlDataContext)ctx;
                ObjectStateEntry stateEntry = efCtx.ObjectContext.ObjectStateManager.GetObjectStateEntry(obj.EntityKey);
                MetadataWorkspace workspace = efCtx.ObjectContext.MetadataWorkspace;
                string typename;
                if (obj is ICollectionEntry)
                {
                    typename = obj.GetType().Name;
                    typename = typename.Substring(0, typename.Length - Kistl.API.Helper.ImplementationSuffix.Length);
                }
                else
                {
                    typename = ((IPersistenceObject)obj).GetInterfaceType().Type.Name;
                }

                EntityType entityType = workspace.GetItem<EntityType>("Model." + typename, DataSpace.CSpace);

                foreach (EdmProperty property in entityType.Properties)
                {
                    stateEntry.SetModifiedProperty(property.Name);
                }
            }

            if (obj is IDataObject)
            {
                using (IKistlContext frozenCtx = KistlContext.GetContext())
                {
                    Kistl.App.Base.ObjectClass objClass = (obj as IDataObject).GetObjectClass(frozenCtx);
                    while (objClass != null)
                    {
                        foreach (ValueTypeProperty p in objClass.Properties.OfType<ValueTypeProperty>().Where(p => p.IsList))
                        {
                            foreach (ICollectionEntry ce in obj.GetPropertyValue<IEnumerable>(p.PropertyName + Kistl.API.Helper.ImplementationSuffix))
                            {
                                MarkEveryPropertyAsModified(ctx, ce);
                            }
                        }
                        objClass = objClass.BaseObjectClass;
                    }
                }
            }
        }
    }

    public class ServerCollectionHandler<A, B, PARENT, CHILD>
        : IServerCollectionHandler
        where A : BaseServerDataObject_EntityFramework
        where B : BaseServerDataObject_EntityFramework
        where PARENT : BaseServerDataObject_EntityFramework
        where CHILD : BaseServerDataObject_EntityFramework
    {

        public IEnumerable<ICollectionEntry> GetCollectionEntries(
            IKistlContext ctx,
            int relId, RelationEndRole endRole,
            int parentId)
        {
            var rel = ctx.Find<Relation>(relId);
            var relEnd = rel.GetEnd(endRole);
            var relOtherEnd = rel.GetOtherEnd(relEnd);
            var parent = ctx.Find<PARENT>(parentId);
            var ceType = Type.GetType(rel.GetCollectionEntryFullName() +
                Kistl.API.Helper.ImplementationSuffix +
                ", " + ApplicationContext.Current.ImplementationAssembly);

            var method = this.GetType().GetMethod("GetCollectionEntriesInternal", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return (IEnumerable<ICollectionEntry>)method
                .MakeGenericMethod(ceType)
                .Invoke(this, new object[] { parent, rel, endRole });
        }

        private IEnumerable<ICollectionEntry> GetCollectionEntriesInternal<IMPL>(PARENT parent, Relation rel, RelationEndRole endRole)
            where IMPL : class, IEntityWithRelationships
        {
            var c = ((IEntityWithRelationships)(parent)).RelationshipManager
                    .GetRelatedCollection<IMPL>(
                        "Model." + rel.GetCollectionEntryAssociationName(endRole),
                        "CollectionEntry");
            if (parent.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                && !c.IsLoaded)
            {
                c.Load();
            }
            return c.Cast<ICollectionEntry>();
        }
    }
}
