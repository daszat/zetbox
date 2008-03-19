using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    /// <summary>
    /// Interface for a LinqToNNN Context.
    /// </summary>
    public interface IKistlContext
    {
        /// <summary>
        /// Attach an IDataObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        void Attach(IDataObject obj);
        /// <summary>
        /// Dettach an IDataObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        void Dettach(IDataObject obj);
        /// <summary>
        /// Delete an IDataObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        void Delete(IDataObject obj);

        /// <summary>
        /// Attach an ICollectionEntry.
        /// </summary>
        /// <param name="obj">ICollectionEntry</param>
        void Attach(ICollectionEntry e);
        /// <summary>
        /// Dettach an ICollectionEntry.
        /// </summary>
        /// <param name="obj">ICollectionEntry</param>
        void Dettach(ICollectionEntry e);
        /// <summary>
        /// Delete an ICollectionEntry.
        /// </summary>
        /// <param name="obj">ICollectionEntry</param>
        void Delete(ICollectionEntry e);
    }
}
