
namespace Kistl.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Build.Framework;

    public sealed class Log4NetLogger
        : ILogger
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Generator");

        public void Initialize(IEventSource eventSource)
        {
            eventSource.AnyEventRaised += AnyEventRaisedHandler;
        }

        private void AnyEventRaisedHandler(object sender, BuildEventArgs e)
        {
            if (e.Message != null)
                Log.InfoFormat("msbuild: {0}", e.Message);
        }

        public string Parameters
        {
            get;
            set;
        }

        public void Shutdown()
        {
        }

        public LoggerVerbosity Verbosity
        {
            get;
            set;
        }
    }
}
