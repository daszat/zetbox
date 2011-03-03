
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
#if MONO
            // see https://bugzilla.novell.com/show_bug.cgi?id=676671
            eventSource.BuildFinished += AnyEventRaisedHandler;
            eventSource.BuildStarted += AnyEventRaisedHandler;
            eventSource.CustomEventRaised += AnyEventRaisedHandler;
            eventSource.ErrorRaised += AnyEventRaisedHandler;
            eventSource.MessageRaised += AnyEventRaisedHandler;
            eventSource.ProjectFinished += AnyEventRaisedHandler;
            eventSource.ProjectStarted += AnyEventRaisedHandler;
            eventSource.StatusEventRaised += AnyEventRaisedHandler;
            eventSource.TargetFinished += AnyEventRaisedHandler;
            eventSource.TargetStarted += AnyEventRaisedHandler;
            eventSource.TaskFinished += AnyEventRaisedHandler;
            eventSource.TaskStarted += AnyEventRaisedHandler;
            eventSource.WarningRaised += AnyEventRaisedHandler;
#else
            eventSource.AnyEventRaised += AnyEventRaisedHandler;
#endif
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
