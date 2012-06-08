namespace Zetbox.API.Server.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.PerfCounter;
    using Zetbox.API.Utils;
    using log4net;

    public class MemoryAppender : BaseMemoryAppender, IPerfCounterAppender
    {
        /// <summary>
        /// Default implementation does nothing. You need to read the values directly.
        /// </summary>
        public override void Dump(bool force) { }
    }
}
