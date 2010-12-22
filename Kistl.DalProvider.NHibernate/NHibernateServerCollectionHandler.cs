
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;

    public class NHibernateServerCollectionHandler<TA, TB, TParent, TChild>
        : IServerCollectionHandler
        where TA : BasePersistenceObject
        where TB : BasePersistenceObject
        where TParent : BasePersistenceObject
        where TChild : BasePersistenceObject
    {

        public IEnumerable<IRelationEntry> GetCollectionEntries(
            IKistlContext ctx,
            Guid relId, RelationEndRole endRole,
            int parentId)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            var rel = ctx.FindPersistenceObject<Relation>(relId);
            //var relEnd = rel.GetEndFromRole(endRole);
            //var relOtherEnd = rel.GetOtherEnd(relEnd);
            var parent = ctx.Find(ctx.GetImplementationType(typeof(TParent)).ToInterfaceType(), parentId);
            var ceType = ctx.ToImplementationType(rel.GetEntryInterfaceType()).Type;

            var method = this.GetType().GetMethod("GetCollectionEntriesInternal", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return (IEnumerable<IRelationEntry>)method
                .MakeGenericMethod(ceType)
                .Invoke(this, new object[] { parent, rel, endRole });
        }

        //// Helper method which is only called by reflection from GetCollectionEntries
        //private IEnumerable<IRelationEntry> GetCollectionEntriesInternal<IMPL>(TParent parent, Relation rel, RelationEndRole endRole)
        //    where IMPL : class, IEntityWithRelationships
        //{
        //    var c = ((IEntityWithRelationships)(parent)).RelationshipManager
        //            .GetRelatedCollection<IMPL>(
        //                "Model." + rel.GetRelationAssociationName(endRole),
        //                "CollectionEntry");
        //    if (parent.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
        //        && !c.IsLoaded)
        //    {
        //        c.Load();
        //    }
        //    c.Cast<IRelationEntry>().ForEach(i => i.AttachToContext(parent.Context));
        //    return c.Cast<IRelationEntry>();
        //}
    }
}
