namespace Zetbox.API.Server.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.PerfCounter;

    // server-side clone of Zetbox.API.Client.PerfCounter.Log4NetAppender
    public class Log4NetAppender : MemoryAppender
    {
        #region Autofac Module
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                moduleBuilder
                    .RegisterType<Log4NetAppender>()
                    .As<IPerfCounterAppender>()
                    .SingleInstance();
            }
        }
        #endregion

        private object dumpLock = new object();
        private int dumpCounter = 0;
        private Stopwatch dumpTimer = Stopwatch.StartNew();
        private const int DUMPCOUNTERMAX = 100;
        private const long DUMPTIMEMIN = 60 * 1000; // one minute in ms

        public Log4NetAppender() { }

        public override void Dump(bool force)
        {
            lock (dumpLock)
            {
                if (force || (++dumpCounter >= DUMPCOUNTERMAX && dumpTimer.ElapsedMilliseconds > DUMPTIMEMIN))
                {
                    var totalsCollector = new Dictionary<string, string>();
                    Dictionary<string, ObjectMemoryCounters> objectsClone;

                    lock (counterLock)
                    {
                        this.FormatTo(totalsCollector);
                        objectsClone = new Dictionary<string, ObjectMemoryCounters>(this.Objects);
                        this.ResetValues();
                    }

                    // don't write to log4net while holding the counterLock
                    Log4NetAppenderUtils.Dump(objectsClone, totalsCollector);

                    dumpCounter = 0;
                    dumpTimer.Reset();
                    dumpTimer.Start();
                }
            }
        }
    }
}
