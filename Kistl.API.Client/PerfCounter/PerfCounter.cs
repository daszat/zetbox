namespace Kistl.API.Client.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using System.Diagnostics;
    using Kistl.API.Utils;
    using Kistl.API.PerfCounter;

    public class PerfCounterDispatcher : BasePerfCounterDispatcher, IPerfCounter
    {
        private readonly IEnumerable<IPerfCounterAppender> _appender;
        private static IEnumerable<IPerfCounterAppender> Empty = new IPerfCounterAppender[] { };

        public PerfCounterDispatcher(IEnumerable<IPerfCounterAppender> appender) :
            base(appender.Cast<IBasePerfCounterAppender>())
        {
            this._appender = appender;
        }

        public void IncrementViewModelFetch()
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementViewModelFetch();
            }
        }

        public void IncrementViewModelCreate()
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementViewModelCreate();
            }
        }

    }
}
