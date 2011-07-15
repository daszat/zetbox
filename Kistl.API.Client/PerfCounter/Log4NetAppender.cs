namespace Kistl.API.Client.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using System.Diagnostics;
    using Kistl.API.Utils;
    using Autofac;
    using Kistl.API.Configuration;
    using log4net;
    using Kistl.API.PerfCounter;

    public class Log4NetAppender : BaseLog4NetAppender, IPerfCounterAppender
    {
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

        public Log4NetAppender()
        {
        }

        protected override long[] Values
        {
            get
            {
                return new[] { 
                    this.ViewModelCreate,
                    this.ViewModelFetch,
                };
            }
        }

        protected override void ResetValues()
        {
            base.ResetValues();
            this.ViewModelCreate =
            this.ViewModelFetch = 0;
        }

        private long ViewModelFetch = 0;
        public void IncrementViewModelFetch()
        {
            lock (sync)
            {
                ViewModelFetch++;

                ShouldDump();
            }
        }

        private long ViewModelCreate = 0;
        public void IncrementViewModelCreate()
        {
            lock (sync)
            {
                ViewModelCreate++;

                ShouldDump();
            }
        }
    }
}
