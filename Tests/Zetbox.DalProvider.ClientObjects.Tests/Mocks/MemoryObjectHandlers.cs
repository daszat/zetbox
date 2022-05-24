// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.API.Server.Fulltext;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.DalProvider.Memory;

    internal sealed class MemoryCollectionHandler<TA, TB, TParent, TChild>
       : IServerCollectionHandler
        where TA : BasePersistenceObject
        where TB : BasePersistenceObject
        where TParent : BasePersistenceObject
        where TChild : BasePersistenceObject
    {

        public async Task<IEnumerable<IRelationEntry>> GetCollectionEntries(
            Guid version,
            IReadOnlyZetboxContext ctx,
            Guid relId, RelationEndRole endRole,
            int parentId)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            ZetboxGeneratedVersionAttribute.Check(version);

            var rel = ctx.FindPersistenceObject<Relation>(relId);
            //var relEnd = rel.GetEndFromRole(endRole);
            //var relOtherEnd = rel.GetOtherEnd(relEnd);
            var parent = await ctx.FindAsync(ctx.GetImplementationType(typeof(TParent)).ToInterfaceType(), parentId);
            var ceType = ctx.ToImplementationType(await rel.GetEntryInterfaceType()).Type;

            var method = this.GetType().GetMethod("GetCollectionEntriesInternal", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return (IEnumerable<IRelationEntry>)method
                .MakeGenericMethod(ceType)
                .Invoke(this, new object[] { parent, rel, endRole });
        }

        //// Helper method which is only called by reflection from GetCollectionEntries
        private async Task<IEnumerable<IRelationEntry>> GetCollectionEntriesInternal<IMPL>(TParent parent, Relation rel, RelationEndRole endRole)
            where IMPL : BaseMemoryPersistenceObject
        {
            return (IEnumerable<IRelationEntry>)MagicCollectionFactory.WrapAsCollection<IRelationEntry>(parent.GetPrivatePropertyValue<object>((await rel.GetEndFromRole(endRole)).Navigator.Name)).ToList();
        }
    }

    internal sealed class MemoryObjectSetHandler
        : BaseServerObjectSetHandler
    {
        /// <inheritdoc/>
        public override Task<IEnumerable<IPersistenceObject>> SetObjects(Guid version, IZetboxContext ctx, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            return base.SetObjects(version, ctx, objects, notificationRequests);
        }
    }

    internal sealed class MemoryObjectHandlerFactory
        : ServerObjectHandlerFactory
    {
        public MemoryObjectHandlerFactory(LuceneSearchDeps searchDependencies = null) : base(searchDependencies) { }

        public override IServerCollectionHandler GetServerCollectionHandler(IReadOnlyZetboxContext ctx, InterfaceType aType, InterfaceType bType, RelationEndRole endRole)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            return GetServerCollectionHandlerHelper(
                typeof(MemoryCollectionHandler<,,,>),
                ctx.ToImplementationType(aType),
                ctx.ToImplementationType(bType),
                endRole);
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
