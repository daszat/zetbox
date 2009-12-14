using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server
{
    public interface IKistlServerContext : IKistlContext
    {
        /// <summary>
        /// Submits the changes and returns the number of affected Objects.
        /// This method does not fire any events or methods on added/changed objects. 
        /// It also does not change any IChanged property.
        /// </summary>
        /// <remarks>
        /// Only IDataObjects are counded.
        /// </remarks>
        /// <returns>Number of affected Objects</returns>
        int SubmitRestore();
    }
}
