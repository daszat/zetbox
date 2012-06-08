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

// TODO: move to Zetbox.DalProvider.Base project
namespace Zetbox.DalProvider.Base.RelationWrappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    public interface IRelationListSync<T>
        where T : IPersistenceObject
    {
        /// <summary>
        /// Adds the specified item without managing the parent. This is used
        /// by the dal provider to keep navigators synchronized.
        /// </summary>
        /// <param name="item">the item to add</param>
        void AddWithoutSetParent(T item);

        /// <summary>
        /// Removes the specified item without managing the parent. This is used
        /// by the dal provider to keep navigators synchronized.
        /// </summary>
        /// <param name="item">the item to remove</param>
        void RemoveWithoutClearParent(T item);
    }
}
