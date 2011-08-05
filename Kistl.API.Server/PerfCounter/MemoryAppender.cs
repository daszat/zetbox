namespace Kistl.API.Server.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.PerfCounter;
    using Kistl.API.Utils;
    using log4net;

    public class MemoryAppender : BaseMemoryAppender, IPerfCounterAppender
    {
        /// <summary>
        /// Default implementation does nothing. You need to read the values directly.
        /// </summary>
        public override void Dump(bool force) { }
    }
}
