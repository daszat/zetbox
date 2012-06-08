namespace Zetbox.API.Server.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.PerfCounter;
    using Zetbox.API.Utils;

    public class PerfCounterDispatcher : BasePerfCounterDispatcher, IPerfCounter
    {
        // default implementation for overrider, currently unused.
        //private readonly IEnumerable<IPerfCounterAppender> _appender;
        //private static IEnumerable<IPerfCounterAppender> Empty = new IPerfCounterAppender[] { };

        public PerfCounterDispatcher(IEnumerable<IPerfCounterAppender> appender) :
            base(appender.Cast<IBasePerfCounterAppender>())
        {
            //this._appender = appender;
        }
    }
}
