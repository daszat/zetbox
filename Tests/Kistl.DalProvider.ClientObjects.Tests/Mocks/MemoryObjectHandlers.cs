
namespace Kistl.DalProvider.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.DalProvider.Memory;

    internal sealed class MemoryObjectHandler<T>
        : BaseServerObjectHandler<T>
        where T : class, IDataObject
    {
        protected override T GetObjectInstance(IKistlContext ctx, int ID)
        {
            return ctx.GetQuery<T>().FirstOrDefault<T>(a => a.ID == ID);
        }
    }
    
    internal sealed class MemoryCollectionHandler<TA, TB, TParent, TChild>
       : IServerCollectionHandler
        where TA : BasePersistenceObject
        where TB : BasePersistenceObject
        where TParent : BasePersistenceObject
        where TChild : BasePersistenceObject
    {

        public IEnumerable<IRelationEntry> GetCollectionEntries(
            Guid version, 
            IKistlContext ctx,
            Guid relId, RelationEndRole endRole,
            int parentId)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            KistlGeneratedVersionAttribute.Check(version);

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
            where IMPL : BaseMemoryPersistenceObject
        {
            return MagicCollectionFactory.WrapAsCollection<IRelationEntry>(parent.GetPrivatePropertyValue<object>(rel.GetEndFromRole(endRole).Navigator.Name)).ToList();
        }
    }

    internal sealed class MemoryObjectSetHandler
        : BaseServerObjectSetHandler
    {
        /// <inheritdoc/>
        public override IEnumerable<IPersistenceObject> SetObjects(Guid version, IKistlContext ctx, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            return base.SetObjects(version, ctx, objects, notificationRequests);
        }
    }

    internal sealed class MemoryObjectHandlerFactory
        : ServerObjectHandlerFactory
    {
        public override IServerCollectionHandler GetServerCollectionHandler(IKistlContext ctx, InterfaceType aType, InterfaceType bType, RelationEndRole endRole)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            return GetServerCollectionHandlerHelper(
                typeof(MemoryCollectionHandler<,,,>),
                ctx.ToImplementationType(aType),
                ctx.ToImplementationType(bType),
                endRole);
        }

        public override IServerObjectHandler GetServerObjectHandler(InterfaceType type)
        {
            return GetServerObjectHandlerHelper(typeof(MemoryObjectHandler<>), type);
        }

        public override IServerObjectSetHandler GetServerObjectSetHandler()
        {
            return new MemoryObjectSetHandler();
        }

        public override IServerDocumentHandler GetServerDocumentHandler()
        {
            return new ServerDocumentHandler();
        }
    }

}
