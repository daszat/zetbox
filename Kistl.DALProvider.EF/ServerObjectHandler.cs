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
        where T : class, IDataObject
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
        /// <inheritdoc/>
        public override IEnumerable<IPersistenceObject> SetObjects(IKistlContext ctx, IEnumerable<IPersistenceObject> objects)
        {
            return base.SetObjects(ctx, objects);
        }
    }

    public class ServerCollectionHandler<A, B, PARENT, CHILD>
        : IServerCollectionHandler
        where A : BaseServerDataObject_EntityFramework
        where B : BaseServerDataObject_EntityFramework
        where PARENT : BaseServerDataObject_EntityFramework
        where CHILD : BaseServerDataObject_EntityFramework
    {

        public IEnumerable<IRelationCollectionEntry> GetCollectionEntries(
            IKistlContext ctx,
            Guid relId, RelationEndRole endRole,
            int parentId)
        {
            var rel = ctx.FindPersistenceObject<Relation>(relId);
            var relEnd = rel.GetEndFromRole(endRole);
            var relOtherEnd = rel.GetOtherEnd(relEnd);
            var parent = ctx.Find(new ImplementationType(typeof(PARENT)).ToInterfaceType(), parentId);
            var ceType = Type.GetType(rel.GetRelationFullName() +
                Kistl.API.Helper.ImplementationSuffix +
                ", " + ApplicationContext.Current.ImplementationAssembly);

            var method = this.GetType().GetMethod("GetCollectionEntriesInternal", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return (IEnumerable<IRelationCollectionEntry>)method
                .MakeGenericMethod(ceType)
                .Invoke(this, new object[] { parent, rel, endRole });
        }

        private IEnumerable<IRelationCollectionEntry> GetCollectionEntriesInternal<IMPL>(PARENT parent, Relation rel, RelationEndRole endRole)
            where IMPL : class, IEntityWithRelationships
        {
            var c = ((IEntityWithRelationships)(parent)).RelationshipManager
                    .GetRelatedCollection<IMPL>(
                        "Model." + rel.GetRelationAssociationName(endRole),
                        "CollectionEntry");
            if (parent.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                && !c.IsLoaded)
            {
                c.Load();
            }
            return c.Cast<IRelationCollectionEntry>();
        }
    }
}
