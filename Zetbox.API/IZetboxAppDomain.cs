using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API.Configuration;

namespace Zetbox.API
{
    /// <summary>
    /// App Domain Interface for Client &amp; Server Hosts.
    /// </summary>
    public interface IZetboxAppDomain
    {
        /// <summary>
        /// Start the Host.
        /// </summary>
        void Start(ZetboxConfig config);
        /// <summary>
        /// Stop the Host.
        /// </summary>
        void Stop();
    }
}
