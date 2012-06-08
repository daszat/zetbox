namespace Zetbox.API.Client.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.PerfCounter;
    using Zetbox.API.Utils;

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

        public override string Category { get { return "Zetbox Client"; } }

        public PerfMonAppender(Configuration.ZetboxConfig cfg)
            : base(cfg)
        {
        }

        protected override CounterDesc[] CounterDesciptors
        {
            get { return _counterDescs; }
        }

        private static readonly CounterDesc[] _counterDescs = new[] 
        { 
            new CounterDesc("ViewModelFetchPerSec", "# of ViewModels fetched / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => ((PerfMonAppender)pma)._ViewModelFetchPerSec = desc.Get(pma)),
            new CounterDesc("ViewModelFetchTotal", "# of ViewModels fetched.", PerformanceCounterType.NumberOfItems64, (pma, desc) => ((PerfMonAppender)pma)._ViewModelFetchTotal = desc.Get(pma)),
            new CounterDesc("ViewModelCreatePerSec", "# of ViewModels created / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => ((PerfMonAppender)pma)._ViewModelCreatePerSec = desc.Get(pma)),
            new CounterDesc("ViewModelCreateTotal", "# of ViewModels created.", PerformanceCounterType.NumberOfItems64, (pma, desc) => ((PerfMonAppender)pma)._ViewModelCreateTotal = desc.Get(pma)),
        };

        protected override MethodPerformanceCounter.Desc[] MethodCounterDesciptors
        {
            get { return _methodDescs; }
        }

        private MethodPerformanceCounter.Desc[] _methodDescs = new MethodPerformanceCounter.Desc[] 
        {
        };

        PerformanceCounter _ViewModelFetchPerSec;
        PerformanceCounter _ViewModelFetchTotal;
        public void IncrementViewModelFetch()
        {
            if (!initialized) return;
            _ViewModelFetchPerSec.Increment();
            _ViewModelFetchTotal.Increment();
        }

        PerformanceCounter _ViewModelCreatePerSec;
        PerformanceCounter _ViewModelCreateTotal;
        public void IncrementViewModelCreate()
        {
            if (!initialized) return;
            _ViewModelCreatePerSec.Increment();
            _ViewModelCreateTotal.Increment();
        }
    }
}
