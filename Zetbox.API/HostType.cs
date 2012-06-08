using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API.Configuration;

namespace Zetbox.API
{

    /// <summary>
    /// Which kind of host to be
    /// </summary>
    public enum HostType
    {
        Client,
        Server,
        /// <summary>
        /// no predefined personality. This is used only in very rare cases.
        /// </summary>
        None
    }
}
