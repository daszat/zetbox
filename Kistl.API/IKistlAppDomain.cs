using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    /// <summary>
    /// App Domain Interface for Client & Server Hosts.
    /// </summary>
    public interface IKistlAppDomain
    {
        /// <summary>
        /// Start the Host.
        /// </summary>
        void Start();
        /// <summary>
        /// Stop the Host.
        /// </summary>
        void Stop();
    }
}
