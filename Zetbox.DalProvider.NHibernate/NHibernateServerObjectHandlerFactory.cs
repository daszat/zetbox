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

namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.API.Server.Fulltext;

    public sealed class NHibernateServerObjectHandlerFactory
        : ServerObjectHandlerFactory
    {
        public NHibernateServerObjectHandlerFactory(LuceneSearchDeps searchDependencies = null)
            : base(searchDependencies)
        {
        }


        public override IServerCollectionHandler GetServerCollectionHandler(
            IReadOnlyZetboxContext ctx,
            InterfaceType aType,
            InterfaceType bType,
            RelationEndRole endRole)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            return GetServerCollectionHandlerHelper(
                typeof(NHibernateServerCollectionHandler<,,,>),
                ctx.ToImplementationType(aType),
                ctx.ToImplementationType(bType),
                endRole);
        }

        public override IServerObjectSetHandler GetServerObjectSetHandler()
        {
            return new NHibernateServerObjectSetHandler();
        }

        public override IServerDocumentHandler GetServerDocumentHandler()
        {
            return new ServerDocumentHandler();
        }
    }
}
