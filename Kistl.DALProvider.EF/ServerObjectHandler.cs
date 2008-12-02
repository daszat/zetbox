using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using System.Collections;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Data.Metadata.Edm;

namespace Kistl.DALProvider.EF
{
    public class ServerObjectHandler<T> : BaseServerObjectHandler<T> where T : IDataObject
    {
        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected override T GetObjectInstance(int ID)
        {
            if (ID < Kistl.API.Helper.INVALIDID)
            {
                // new object -> look in current context
                Kistl.DALProvider.EF.KistlDataContext efCtx = (KistlDataContext)Kistl.API.Server.KistlContext.Current;
                ObjectContext ctx = efCtx.ObjectContext;
                return (T)ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added)
                    .FirstOrDefault(e => e.Entity is IDataObject && ((IDataObject)e.Entity).ID == ID).Entity;
            }
            else
            {
                return Kistl.API.Server.KistlContext.Current.GetQuery<T>().FirstOrDefault<T>(a => a.ID == ID);
            }
        }
    }

    public class ServerObjectSetHandler : BaseServerObjectSetHandler
    {
        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public override IEnumerable<IDataObject> SetObjects(IEnumerable<IDataObject> objects)
        {
            foreach (IDataObject obj in objects)
            {
                // Fist of all, Attach Object
                Kistl.API.Server.KistlContext.Current.Attach(obj);

                if (obj.ObjectState != DataObjectState.Deleted)
                {
                    MarkEveryPropertyAsModified(obj);
                }
            }
            foreach (IDataObject obj in objects)
            {
                if (obj.ObjectState != DataObjectState.Deleted)
                {
                    UpdateRelationships(obj);
                }
            }
            return base.SetObjects(objects);
        }

        /// <summary>
        /// Update Relationships
        /// Note: Change Tracking is "enabled" by setting every Reference
        /// </summary>
        /// <param name="obj"></param>
        protected static void UpdateRelationships(IDataObject obj)
        {
            using (IKistlContext ctx = Kistl.API.Server.KistlContext.GetContext())
            {
                Kistl.App.Base.ObjectClass objClass = obj.GetObjectClass(ctx);
                while (objClass != null)
                {
                    var listProperties = objClass.Properties.OfType<Kistl.App.Base.ObjectReferenceProperty>()
                        .ToList().Where(p => p.HasStorage());
                    foreach (Kistl.App.Base.ObjectReferenceProperty p in listProperties)
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
                            // List
                            foreach (ICollectionEntry ce in obj.GetPropertyValue<IEnumerable>(p.PropertyName + Kistl.API.Helper.ImplementationSuffix))
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
        private static void MarkEveryPropertyAsModified(IPersistenceObject o)
        {
            IEntityStateObject obj = (IEntityStateObject)o;
            // TODO: Bad Hack!!
            if (obj.EntityState.In(EntityState.Modified, EntityState.Unchanged))
            {
                Kistl.DALProvider.EF.KistlDataContext efCtx = (KistlDataContext)Kistl.API.Server.KistlContext.Current;
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
                using (IKistlContext ctx = Kistl.API.Server.KistlContext.GetContext())
                {
                    Kistl.App.Base.ObjectClass objClass = (obj as IDataObject).GetObjectClass(ctx);
                    while (objClass != null)
                    {
                        foreach (Kistl.App.Base.ValueTypeProperty p in objClass.Properties.OfType<Kistl.App.Base.ValueTypeProperty>().Where(p => p.IsList))
                        {
                            foreach (ICollectionEntry ce in obj.GetPropertyValue<IEnumerable>(p.PropertyName + Kistl.API.Helper.ImplementationSuffix))
                            {
                                MarkEveryPropertyAsModified(ce);
                            }
                        }
                        objClass = objClass.BaseObjectClass;
                    }
                }
            }
        }
    }
}
