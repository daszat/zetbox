
namespace Kistl.DalProvider.Base.RelationWrappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

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
