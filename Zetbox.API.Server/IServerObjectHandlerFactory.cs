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

namespace Zetbox.API.Server
{
    using System;

    public interface IServerObjectHandlerFactory
    {
        /// <summary>
        /// Creates an <see cref="IServerCollectionHandler"/> for the specified Relation.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="aType">The type of the A-side of the Relation.</param>
        /// <param name="bType">The type of the B-side of the Relation.</param>
        /// <param name="endRole">The "parent"-side of the collection.</param>
        /// <returns>A newly initialised <see cref="IServerCollectionHandler"/>.</returns>
        IServerCollectionHandler GetServerCollectionHandler(IZetboxContext ctx, InterfaceType aType, InterfaceType bType, RelationEndRole endRole);

        /// <summary>
        /// Creates an <see cref="IServerObjectHandler"/> for the specified Type.
        /// </summary>
        /// <param name="type">The Type which should be handled by the <see cref="IServerObjectHandler"/>.</param>
        /// <returns>A newly initialised <see cref="IServerObjectHandler"/>.</returns>
        IServerObjectHandler GetServerObjectHandler(InterfaceType type);

        /// <summary>
        /// Creates an <see cref="IServerObjectSetHandler"/>.
        /// </summary>
        /// <returns>A newly initialised <see cref="IServerObjectSetHandler"/>.</returns>
        IServerObjectSetHandler GetServerObjectSetHandler();

        /// <summary>
        /// Creates an <see cref="IServerDocumentHandler"/>.
        /// </summary>
        /// <returns>A newly initialised <see cref="IServerDocumentHandler"/>.</returns>
        IServerDocumentHandler GetServerDocumentHandler();
    }
}
