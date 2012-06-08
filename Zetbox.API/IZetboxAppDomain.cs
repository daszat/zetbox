using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Configuration;

namespace Kistl.API
{
    /// <summary>
    /// App Domain Interface for Client &amp; Server Hosts.
    /// </summary>
    public interface IKistlAppDomain
    {
        /// <summary>
        /// Start the Host.
        /// </summary>
        void Start(KistlConfig config);
        /// <summary>
        /// Stop the Host.
        /// </summary>
        void Stop();
    }
}
