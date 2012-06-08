namespace Zetbox.API.Server.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.PerfCounter;

    // server-side clone of Zetbox.API.Client.PerfCounter.ResetOnReadAppender
    public class ResetOnReadAppender : MemoryAppender
    {
        #region Autofac Module
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                moduleBuilder
                    .RegisterType<ResetOnReadAppender>()
                    .AsSelf()
                    .As<IPerfCounterAppender>()
                    .SingleInstance();
            }
        }
        #endregion

        public sealed class Data
        {
            internal Data() { }
            public Dictionary<string, string> Totals = new Dictionary<string, string>();
            public Dictionary<string, ObjectMemoryCounters> Objects;
        }

        public ResetOnReadAppender() { }

        public Data ReadAndResetValues()
        {
            var data = new Data();

            lock (counterLock)
            {
                this.FormatTo(data.Totals);
                data.Objects = new Dictionary<string, ObjectMemoryCounters>(this.Objects);
                this.ResetValues();
            }

            return data;
        }
    }
}
