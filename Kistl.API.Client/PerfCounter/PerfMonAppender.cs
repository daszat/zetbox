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

        public override string Category { get { return "Kistl Client"; } }

        public PerfMonAppender(Kistl.API.Configuration.KistlConfig cfg)
            : base(cfg)
        {
        }

        protected override BasePerfMonAppender.CounterDesc[] CounterDesciptors
        {
            get { return _counterDescs; }
        }

        private static readonly CounterDesc[] _counterDescs = new CounterDesc[] 
        { 
            new CounterDesc("ViewModelFetchPerSec", "# of ViewModels fetched / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => ((PerfMonAppender)pma)._ViewModelFetchPerSec = desc.Get(pma)),
            new CounterDesc("ViewModelFetchTotal", "# of ViewModels fetched.", PerformanceCounterType.NumberOfItems64, (pma, desc) => ((PerfMonAppender)pma)._ViewModelFetchTotal = desc.Get(pma)),
            new CounterDesc("ViewModelCreatePerSec", "# of ViewModels created / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => ((PerfMonAppender)pma)._ViewModelCreatePerSec = desc.Get(pma)),
            new CounterDesc("ViewModelCreateTotal", "# of ViewModels created.", PerformanceCounterType.NumberOfItems64, (pma, desc) => ((PerfMonAppender)pma)._ViewModelCreateTotal = desc.Get(pma)),
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
