using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    public enum ClientIsolationLevel
    {
        /// <summary>
        /// Uses server data after each query
        /// </summary>
        MergeServerData = 1, 
        /// <summary>
        /// Uses client data after each query
        /// </summary>
        PrefereClientData = 2
    }
}
