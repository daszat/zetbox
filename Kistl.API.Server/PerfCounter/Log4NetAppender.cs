namespace Kistl.API.Server.PerfCounter
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
            get { return new long[] { }; }
        }

        protected override void ResetValues()
        {
            base.ResetValues();
        }
    }
}
