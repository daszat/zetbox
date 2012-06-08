
namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.API.Utils;

    public class NHibernateServerCollectionHandler<TA, TB, TParent, TChild>
        : IServerCollectionHandler
        where TA : BasePersistenceObject
        where TB : BasePersistenceObject
        where TParent : BasePersistenceObject
        where TChild : BasePersistenceObject
    {

        public IEnumerable<IRelationEntry> GetCollectionEntries(
            Guid version,
            IZetboxContext ctx,
            Guid relId, RelationEndRole endRole,
            int parentId)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            ZetboxGeneratedVersionAttribute.Check(version);

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
        private IEnumerable<IRelationEntry> GetCollectionEntriesInternal<IMPL>(TParent parent, Relation rel, RelationEndRole endRole)
            where IMPL : NHibernatePersistenceObject
        {
            return MagicCollectionFactory.WrapAsCollection<IRelationEntry>(parent.GetPrivatePropertyValue<object>(rel.GetEndFromRole(endRole).Navigator.Name)).ToList();
        }
    }
}
