using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server
{
    /// <summary>
    /// A data context without identity, which is useful for various administrative tasks.
    /// </summary>
    public interface IKistlServerContext 
        : IKistlContext
    {
        /// <summary>
        /// Submits the changes and returns the number of affected Objects.
        /// This method does not fire any events or methods on added/changed objects. 
        /// It also does not change any IChanged property.
        /// </summary>
        /// <remarks>
        /// <para>This method is used when restoring data from backups or when importing. 
        /// In these cases it is important, that the object's live-cycles do not start 
        /// here, thus no events are triggered.</para>
        /// <para>Only IDataObjects are counted.</para>
        /// </remarks>
        /// <returns>Number of affected Objects</returns>
        int SubmitRestore();
    }
}
