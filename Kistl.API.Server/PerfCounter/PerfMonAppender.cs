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
    using Kistl.API.PerfCounter;

    public class PerfMonAppender : BasePerfMonAppender, IPerfCounterAppender
    {
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                moduleBuilder
                    .RegisterType<PerfMonAppender>()
                    .As<IPerfCounterAppender>()
                    .SingleInstance();
            }
        }

        public override string Category { get { return "Kistl Server"; } }

        public PerfMonAppender(Kistl.API.Configuration.KistlConfig cfg)
            : base(cfg)
        {
        }

        #region Counter Descriptors
        protected override CounterDesc[] CounterDesciptors
        {
            get { return _counterDescs; }
        }
        private static readonly CounterDesc[] _counterDescs = new CounterDesc[] 
        { 
        };
        #endregion
    }
}
