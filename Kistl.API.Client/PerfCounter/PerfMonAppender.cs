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

    public class PerfMonAppender : IPerfCounterAppender
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

        public static string Category = "Kistl Client";
        public readonly string InstanceName;

        public PerfMonAppender(Kistl.API.Configuration.KistlConfig cfg)
        {
            if (cfg == null) throw new ArgumentNullException("cfg");
            //InstanceName = string.Format("{0} - {1}", AppDomain.CurrentDomain.FriendlyName, Process.GetCurrentProcess().Id);
            InstanceName = cfg.ConfigName;
        }

        public void Install()
        {
            Uninstall();

            Logging.Log.Info("Installing performance counter");
            CounterCreationDataCollection counters = new CounterCreationDataCollection();

            counters.Add(new CounterCreationData("QueriesPerSec", "# of Queries / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("QueriesTotal", "# of Queries.", PerformanceCounterType.NumberOfItems64));

            counters.Add(new CounterCreationData("SubmitChangesPerSec", "# of SubmitChanges / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("SubmitChangesTotal", "# of SubmitChanges.", PerformanceCounterType.NumberOfItems64));
            counters.Add(new CounterCreationData("SubmitChangesObjectsPerSec", "# Objects submitted / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("SubmitChangesObjectsTotal", "# Objects submitted.", PerformanceCounterType.NumberOfItems64));

            counters.Add(new CounterCreationData("GetListPerSec", "# of GetList calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("GetListTotal", "# of GetList calls.", PerformanceCounterType.NumberOfItems64));
            counters.Add(new CounterCreationData("GetListObjectsPerSec", "# of Objects returned by GetList calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("GetListObjectsTotal", "# of Objects returned by GetList calls.", PerformanceCounterType.NumberOfItems64));

            counters.Add(new CounterCreationData("GetListOfPerSec", "# of GetListOf calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("GetListOfTotal", "# of GetListOf calls.", PerformanceCounterType.NumberOfItems64));
            counters.Add(new CounterCreationData("GetListOfObjectsPerSec", "# of Objects returned by GetListOf calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("GetListOfObjectsTotal", "# of Objects returned by GetListOf calls.", PerformanceCounterType.NumberOfItems64));

            counters.Add(new CounterCreationData("FetchRelationPerSec", "# of FetchRelation calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("FetchRelationTotal", "# of FetchRelation calls.", PerformanceCounterType.NumberOfItems64));
            counters.Add(new CounterCreationData("FetchRelationObjectsPerSec", "# of Objects returned by FetchRelation calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("FetchRelationObjectsTotal", "# of Objects returned by FetchRelation calls.", PerformanceCounterType.NumberOfItems64));

            counters.Add(new CounterCreationData("SetObjectsPerSec", "# of SetObjects calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("SetObjectsTotal", "# of SetObjects calls.", PerformanceCounterType.NumberOfItems64));
            counters.Add(new CounterCreationData("SetObjectsObjectsPerSec", "# of Objects sent to the SetObjects call / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("SetObjectsObjectsTotal", "# of Objects sent to the SetObjects call.", PerformanceCounterType.NumberOfItems64));

            counters.Add(new CounterCreationData("ServerMethodInvocationPerSec", "# of ServerMethodInvocation calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("ServerMethodInvocationTotal", "# of ServerMethodInvocation calls.", PerformanceCounterType.NumberOfItems64));

            counters.Add(new CounterCreationData("ViewModelFetchPerSec", "# of ViewModels fetched / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("ViewModelFetchTotal", "# of ViewModels fetched.", PerformanceCounterType.NumberOfItems64));
            counters.Add(new CounterCreationData("ViewModelCreatePerSec", "# of ViewModels created / sec.", PerformanceCounterType.RateOfCountsPerSecond32));
            counters.Add(new CounterCreationData("ViewModelCreateTotal", "# of ViewModels created.", PerformanceCounterType.NumberOfItems64));

            PerformanceCounterCategory.Create(Category, "A custom counter category that tracks Kistl executions",
                PerformanceCounterCategoryType.MultiInstance, counters);
            Logging.Log.Info("Performance counter sucessfully installed");
        }

        public void Uninstall()
        {
            if (PerformanceCounterCategory.Exists(Category))
            {
                Logging.Log.Info("Uninstalling performance counter");
                PerformanceCounterCategory.Delete(Category);
                initialized = false;
                Logging.Log.Info("Performance counter sucessfully uninstalled");
            }
        }

        public void Initialize(IFrozenContext frozenCtx)
        {
            if (!PerformanceCounterCategory.Exists(Category))
            {
                initialized = false;
                Logging.Log.Warn("PerfCounters are not installed, execute 'sudo Kistl.Client.*.exe -installperfcounter'");
                return;
            }

            try
            {
                (_QueriesPerSec = Get("QueriesPerSec")).RawValue = 0;
                (_QueriesTotal = Get("QueriesTotal")).RawValue = 0;

                (_GetListPerSec = Get("GetListPerSec")).RawValue = 0;
                (_GetListTotal = Get("GetListTotal")).RawValue = 0;
                (_GetListObjectsPerSec = Get("GetListObjectsPerSec")).RawValue = 0;
                (_GetListObjectsTotal = Get("GetListObjectsTotal")).RawValue = 0;

                (_GetListOfPerSec = Get("GetListOfPerSec")).RawValue = 0;
                (_GetListOfTotal = Get("GetListOFTotal")).RawValue = 0;
                (_GetListOfObjectsPerSec = Get("GetListOfObjectsPerSec")).RawValue = 0;
                (_GetListOfObjectsTotal = Get("GetListOfObjectsTotal")).RawValue = 0;

                (_SubmitChangesPerSec = Get("SubmitChangesPerSec")).RawValue = 0;
                (_SubmitChangesTotal = Get("SubmitChangesTotal")).RawValue = 0;
                (_SubmitChangesObjectsPerSec = Get("SubmitChangesObjectsPerSec")).RawValue = 0;
                (_SubmitChangesObjectsTotal = Get("SubmitChangesObjectsTotal")).RawValue = 0;

                (_FetchRelationPerSec = Get("FetchRelationPerSec")).RawValue = 0;
                (_FetchRelationTotal = Get("FetchRelationTotal")).RawValue = 0;
                (_FetchRelationObjectsPerSec = Get("FetchRelationObjectsPerSec")).RawValue = 0;
                (_FetchRelationObjectsTotal = Get("FetchRelationObjectsTotal")).RawValue = 0;

                (_SetObjectsPerSec = Get("SetObjectsPerSec")).RawValue = 0;
                (_SetObjectsTotal = Get("SetObjectsTotal")).RawValue = 0;
                (_SetObjectsObjectsPerSec = Get("SetObjectsObjectsPerSec")).RawValue = 0;
                (_SetObjectsObjectsTotal = Get("SetObjectsObjectsTotal")).RawValue = 0;

                (_ServerMethodInvocationPerSec = Get("ServerMethodInvocationPerSec")).RawValue = 0;
                (_ServerMethodInvocationTotal = Get("ServerMethodInvocationTotal")).RawValue = 0;

                (_ViewModelFetchPerSec = Get("ViewModelFetchPerSec")).RawValue = 0;
                (_ViewModelFetchTotal = Get("ViewModelFetchTotal")).RawValue = 0;
                (_ViewModelCreatePerSec = Get("ViewModelCreatePerSec")).RawValue = 0;
                (_ViewModelCreateTotal = Get("ViewModelCreateTotal")).RawValue = 0;

                initialized = true;
            }
            catch (Exception ex)
            {
                Logging.Log.Error("Unable to initialize PerfCounters", ex);
                initialized = false;
            }
        }

        public void Dump()
        {
        }

        private bool initialized = false;

        private PerformanceCounter Get(string perfCounter)
        {
            if (string.IsNullOrEmpty(perfCounter)) throw new ArgumentNullException("perfCounter");
            var counter = new PerformanceCounter(PerfMonAppender.Category, perfCounter, InstanceName, false);
            counter.RawValue = 0; // Initialize & create instance, see MSDN
            return counter;
        }

        PerformanceCounter _QueriesTotal;
        PerformanceCounter _QueriesPerSec;
        public void IncrementQuery(InterfaceType ifType)
        {
            if (!initialized) return;

            _QueriesPerSec.Increment();
            _QueriesTotal.Increment();
        }

        PerformanceCounter _SubmitChangesPerSec;
        PerformanceCounter _SubmitChangesTotal;
        PerformanceCounter _SubmitChangesObjectsPerSec;
        PerformanceCounter _SubmitChangesObjectsTotal;
        public void IncrementSubmitChanges(int objectCount)
        {
            if (!initialized) return;

            _SubmitChangesPerSec.Increment();
            _SubmitChangesTotal.Increment();
            _SubmitChangesObjectsPerSec.IncrementBy(objectCount);
            _SubmitChangesObjectsTotal.IncrementBy(objectCount);
        }

        PerformanceCounter _GetListPerSec;
        PerformanceCounter _GetListTotal;
        PerformanceCounter _GetListObjectsPerSec;
        PerformanceCounter _GetListObjectsTotal;
        public void IncrementGetList(InterfaceType ifType, int resultSize)
        {
            if (!initialized) return;

            _GetListPerSec.Increment();
            _GetListTotal.Increment();
            _GetListObjectsPerSec.IncrementBy(resultSize);
            _GetListObjectsTotal.IncrementBy(resultSize);
        }

        PerformanceCounter _GetListOfPerSec;
        PerformanceCounter _GetListOfTotal;
        PerformanceCounter _GetListOfObjectsPerSec;
        PerformanceCounter _GetListOfObjectsTotal;
        public void IncrementGetListOf(InterfaceType ifType, int resultSize)
        {
            if (!initialized) return;

            _GetListOfPerSec.Increment();
            _GetListOfTotal.Increment();
            _GetListOfObjectsPerSec.IncrementBy(resultSize);
            _GetListOfObjectsTotal.IncrementBy(resultSize);
        }

        PerformanceCounter _FetchRelationPerSec;
        PerformanceCounter _FetchRelationTotal;
        PerformanceCounter _FetchRelationObjectsPerSec;
        PerformanceCounter _FetchRelationObjectsTotal;
        public void IncrementFetchRelation(InterfaceType ifType, int resultSize)
        {
            if (!initialized) return;

            _FetchRelationPerSec.Increment();
            _FetchRelationTotal.Increment();
            _FetchRelationObjectsPerSec.IncrementBy(resultSize);
            _FetchRelationObjectsTotal.IncrementBy(resultSize);
        }

        PerformanceCounter _SetObjectsPerSec;
        PerformanceCounter _SetObjectsTotal;
        PerformanceCounter _SetObjectsObjectsPerSec;
        PerformanceCounter _SetObjectsObjectsTotal;
        public void IncrementSetObjects(int objectCount)
        {
            if (!initialized) return;

            _SetObjectsPerSec.Increment();
            _SetObjectsTotal.Increment();
            _SetObjectsObjectsPerSec.IncrementBy(objectCount);
            _SetObjectsObjectsTotal.IncrementBy(objectCount);
        }

        PerformanceCounter _ServerMethodInvocationPerSec;
        PerformanceCounter _ServerMethodInvocationTotal;
        public void IncrementServerMethodInvocation()
        {
            if (!initialized) return;
            _ServerMethodInvocationPerSec.Increment();
            _ServerMethodInvocationTotal.Increment();
        }


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
