using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Kistl.API.Utils;

namespace Kistl.API.PerfCounter
{
    public abstract class BasePerfMonAppender
    {
        public readonly string InstanceName;
        public abstract string Category { get; }

        #region Fiedls
        PerformanceCounter _QueriesTotal;
        PerformanceCounter _QueriesPerSec;
        PerformanceCounter _SubmitChangesPerSec;
        PerformanceCounter _SubmitChangesTotal;
        PerformanceCounter _SubmitChangesObjectsPerSec;
        PerformanceCounter _SubmitChangesObjectsTotal;
        PerformanceCounter _GetListPerSec;
        PerformanceCounter _GetListTotal;
        PerformanceCounter _GetListObjectsPerSec;
        PerformanceCounter _GetListObjectsTotal;
        PerformanceCounter _GetListOfPerSec;
        PerformanceCounter _GetListOfTotal;
        PerformanceCounter _GetListOfObjectsPerSec;
        PerformanceCounter _GetListOfObjectsTotal;
        PerformanceCounter _ServerMethodInvocationPerSec;
        PerformanceCounter _ServerMethodInvocationTotal;
        PerformanceCounter _SubmitChangesCurrent;
        PerformanceCounter _SetObjectsPerSec;
        PerformanceCounter _SetObjectsTotal;
        PerformanceCounter _SetObjectsObjectsPerSec;
        PerformanceCounter _SetObjectsObjectsTotal;
        PerformanceCounter _FetchRelationPerSec;
        PerformanceCounter _FetchRelationTotal;
        PerformanceCounter _FetchRelationObjectsPerSec;
        PerformanceCounter _FetchRelationObjectsTotal;
        PerformanceCounter _GetListCurrent;
        PerformanceCounter _GetListOfCurrent;
        PerformanceCounter _SetObjectsCurrent;
        PerformanceCounter _FetchRelationCurrent;
        PerformanceCounter _SubmitChangesAvgDuration;
        PerformanceCounter _SubmitChangesAvgDurationBase;
        PerformanceCounter _GetListAvgDuration;
        PerformanceCounter _GetListAvgDurationBase;
        PerformanceCounter _GetListOfAvgDuration;
        PerformanceCounter _GetListOfAvgDurationBase;
        PerformanceCounter _FetchRelationAvgDuration;
        PerformanceCounter _FetchRelationAvgDurationBase;
        PerformanceCounter _SetObjectsAvgDuration;
        PerformanceCounter _SetObjectsAvgDurationBase;
        #endregion

        public BasePerfMonAppender(Kistl.API.Configuration.KistlConfig cfg)
        {
            if (cfg == null) throw new ArgumentNullException("cfg");
            //InstanceName = string.Format("{0} - {1}", AppDomain.CurrentDomain.FriendlyName, Process.GetCurrentProcess().Id);
            InstanceName = cfg.ConfigName;
        }

        protected class CounterDesc
        {
            public CounterDesc(string name, string desc, PerformanceCounterType type, Action<BasePerfMonAppender, CounterDesc> setter)
            {
                this.Name = name;
                this.Desc = desc;
                this.type = type;
                this.setter = setter;
            }

            public readonly string Name;
            public readonly string Desc;
            public readonly PerformanceCounterType type;
            private readonly Action<BasePerfMonAppender, CounterDesc> setter;

            public void Set(BasePerfMonAppender pma)
            {
                setter(pma, this);
            }

            public PerformanceCounter Get(BasePerfMonAppender pma)
            {
                if (pma == null) throw new ArgumentNullException("pma");
                var counter = new PerformanceCounter(pma.Category, Name, pma.InstanceName, false);
                counter.RawValue = 0; // Initialize & create instance, see MSDN
                return counter;
            }

            public CounterCreationData Create()
            {
                return new CounterCreationData(Name, Desc, type);
            }
        }

        protected abstract CounterDesc[] CounterDesciptors { get; }

        public void Install()
        {
            Uninstall();

            Logging.Log.Info("Installing performance counter");
            CounterCreationDataCollection counters = new CounterCreationDataCollection();

            foreach (var desc in _counterDescs.Concat(CounterDesciptors))
            {
                counters.Add(desc.Create());
            }

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
                Logging.Log.Warn("PerfCounters are not installed, execute 'sudo Kistl.*.exe -installperfcounter'");
                return;
            }

            try
            {
                foreach (var desc in _counterDescs.Concat(CounterDesciptors))
                {
                    desc.Set(this);
                }

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

        protected bool initialized = false;

        #region Base Descriptors
        private static readonly CounterDesc[] _counterDescs = new[] 
        { 
            new CounterDesc("QueriesPerSec", "# of Queries / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._QueriesPerSec = desc.Get(pma)),
            new CounterDesc("QueriesTotal", "# of Queries.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._QueriesTotal = desc.Get(pma)),

            new CounterDesc("SubmitChangesPerSec", "# of SubmitChanges / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._SubmitChangesPerSec = desc.Get(pma)),
            new CounterDesc("SubmitChangesTotal", "# of SubmitChanges.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._SubmitChangesTotal = desc.Get(pma)),
            new CounterDesc("SubmitChangesCurrent", "Current # of SubmitChanges.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._SubmitChangesCurrent = desc.Get(pma)),

            new CounterDesc("SubmitChangesAvgDuration", "Avg. duration of SubmitChanges.", PerformanceCounterType.AverageTimer32, (pma, desc) => pma._SubmitChangesAvgDuration = desc.Get(pma)),
            new CounterDesc("SubmitChangesAvgDurationBase", "Avg. duration of SubmitChanges Base.", PerformanceCounterType.AverageBase, (pma, desc) => pma._SubmitChangesAvgDurationBase = desc.Get(pma)),
            
            new CounterDesc("SubmitChangesObjectsPerSec", "# Objects submitted / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._SubmitChangesObjectsPerSec = desc.Get(pma)),
            new CounterDesc("SubmitChangesObjectsTotal", "# Objects submitted.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._SubmitChangesObjectsTotal = desc.Get(pma)),

            new CounterDesc("GetListPerSec", "# of GetList calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._GetListPerSec = desc.Get(pma)),
            new CounterDesc("GetListTotal", "# of GetList calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._GetListTotal = desc.Get(pma)),
            new CounterDesc("GetListCurrent", "Current # of GetList calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._GetListCurrent = desc.Get(pma)),

            new CounterDesc("GetListAvgDuration", "Avg. duration of GetList calls.", PerformanceCounterType.AverageTimer32, (pma, desc) => pma._GetListAvgDuration = desc.Get(pma)),
            new CounterDesc("GetListAvgDurationBase", "Avg. duration of GetList calls base.", PerformanceCounterType.AverageBase, (pma, desc) => pma._GetListAvgDurationBase = desc.Get(pma)),
                        
            new CounterDesc("GetListObjectsPerSec", "# of Objects returned by GetList calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._GetListObjectsPerSec = desc.Get(pma)),
            new CounterDesc("GetListObjectsTotal", "# of Objects returned by GetList calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._GetListObjectsTotal = desc.Get(pma)),

            new CounterDesc("GetListOfPerSec", "# of GetListOf calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._GetListOfPerSec = desc.Get(pma)),
            new CounterDesc("GetListOfTotal", "# of GetListOf calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._GetListOfTotal = desc.Get(pma)),
            new CounterDesc("GetListOfCurrent", "Current # of GetListOf calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._GetListOfCurrent = desc.Get(pma)),

            new CounterDesc("GetListOfAvgDuration", "Avg. duration of GetListOf calls.", PerformanceCounterType.AverageTimer32, (pma, desc) => pma._GetListOfAvgDuration = desc.Get(pma)),
            new CounterDesc("GetListOfAvgDurationBase", "Avg. duration of GetListOf calls base.", PerformanceCounterType.AverageBase, (pma, desc) => pma._GetListOfAvgDurationBase = desc.Get(pma)),

            new CounterDesc("GetListOfObjectsPerSec", "# of Objects returned by GetListOf calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._GetListOfObjectsPerSec = desc.Get(pma)),
            new CounterDesc("GetListOfObjectsTotal", "# of Objects returned by GetListOf calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._GetListOfObjectsTotal = desc.Get(pma)),

            new CounterDesc("FetchRelationPerSec", "# of FetchRelation calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._FetchRelationPerSec = desc.Get(pma)),
            new CounterDesc("FetchRelationTotal", "# of FetchRelation calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._FetchRelationTotal = desc.Get(pma)),
            new CounterDesc("FetchRelationCurrent", "Current # of FetchRelation calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._FetchRelationCurrent = desc.Get(pma)),

            new CounterDesc("FetchRelationAvgDuration", "Avg. duration of FetchRelation calls.", PerformanceCounterType.AverageTimer32, (pma, desc) => pma._FetchRelationAvgDuration = desc.Get(pma)),
            new CounterDesc("FetchRelationAvgDurationBase", "Avg. duration of FetchRelation calls base.", PerformanceCounterType.AverageBase, (pma, desc) => pma._FetchRelationAvgDurationBase = desc.Get(pma)),

            new CounterDesc("FetchRelationObjectsPerSec", "# of Objects returned by FetchRelation calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._FetchRelationObjectsPerSec = desc.Get(pma)),
            new CounterDesc("FetchRelationObjectsTotal", "# of Objects returned by FetchRelation calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._FetchRelationObjectsTotal = desc.Get(pma)),

            new CounterDesc("SetObjectsPerSec", "# of SetObjects calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._SetObjectsPerSec = desc.Get(pma)),
            new CounterDesc("SetObjectsTotal", "# of SetObjects calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._SetObjectsTotal = desc.Get(pma)),
            new CounterDesc("SetObjectsCurrent", "Current # of SetObjects calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._SetObjectsCurrent = desc.Get(pma)),

            new CounterDesc("SetObjectsAvgDuration", "Avg. duration of SetObjects calls.", PerformanceCounterType.AverageTimer32, (pma, desc) => pma._SetObjectsAvgDuration = desc.Get(pma)),
            new CounterDesc("SetObjectsAvgDurationBase", "Avg. duration of SetObjects calls base.", PerformanceCounterType.AverageBase, (pma, desc) => pma._SetObjectsAvgDurationBase = desc.Get(pma)),

            new CounterDesc("SetObjectsObjectsPerSec", "# of Objects sent to the SetObjects call / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._SetObjectsObjectsPerSec = desc.Get(pma)),
            new CounterDesc("SetObjectsObjectsTotal", "# of Objects sent to the SetObjects call.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._SetObjectsObjectsTotal = desc.Get(pma)),

            new CounterDesc("ServerMethodInvocationPerSec", "# of ServerMethodInvocation calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._ServerMethodInvocationPerSec = desc.Get(pma)),
            new CounterDesc("ServerMethodInvocationTotal", "# of ServerMethodInvocation calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._ServerMethodInvocationTotal = desc.Get(pma)),
        };
        #endregion

        #region Common Counters
        public void IncrementQuery(InterfaceType ifType)
        {
            if (!initialized) return;

            _QueriesPerSec.Increment();
            _QueriesTotal.Increment();
        }

        public void IncrementSubmitChanges()
        {
            _SubmitChangesCurrent.Increment();
        }

        public void DecrementSubmitChanges(int objectCount, long startTicks)
        {
            if (!initialized) return;

            _SubmitChangesPerSec.Increment();
            _SubmitChangesTotal.Increment();
            _SubmitChangesObjectsPerSec.IncrementBy(objectCount);
            _SubmitChangesObjectsTotal.IncrementBy(objectCount);

            _SubmitChangesCurrent.Decrement();
            _SubmitChangesAvgDuration.IncrementBy(Stopwatch.GetTimestamp() - startTicks);
            _SubmitChangesAvgDurationBase.Increment();
        }

        public void IncrementGetList(InterfaceType ifType)
        {
            _GetListCurrent.Increment();
        }

        public void DecrementGetList(InterfaceType ifType, int resultSize, long startTicks)
        {
            if (!initialized) return;

            _GetListPerSec.Increment();
            _GetListTotal.Increment();
            _GetListObjectsPerSec.IncrementBy(resultSize);
            _GetListObjectsTotal.IncrementBy(resultSize);

            _GetListCurrent.Decrement();
            _GetListAvgDuration.IncrementBy(Stopwatch.GetTimestamp() - startTicks);
            _GetListAvgDurationBase.Increment();
        }

        public void IncrementGetListOf(InterfaceType ifType)
        {
            _GetListOfCurrent.Increment();
        }
        public void DecrementGetListOf(InterfaceType ifType, int resultSize, long startTicks)
        {
            if (!initialized) return;

            _GetListOfPerSec.Increment();
            _GetListOfTotal.Increment();
            _GetListOfObjectsPerSec.IncrementBy(resultSize);
            _GetListOfObjectsTotal.IncrementBy(resultSize);

            _GetListOfCurrent.Decrement();
            _GetListOfAvgDuration.IncrementBy(Stopwatch.GetTimestamp() - startTicks);
            _GetListOfAvgDurationBase.Increment();
        }

        public void IncrementFetchRelation(InterfaceType ifType)
        {
            _FetchRelationCurrent.Increment();
        }

        public void DecrementFetchRelation(InterfaceType ifType, int resultSize, long startTicks)
        {
            if (!initialized) return;

            _FetchRelationPerSec.Increment();
            _FetchRelationTotal.Increment();
            _FetchRelationObjectsPerSec.IncrementBy(resultSize);
            _FetchRelationObjectsTotal.IncrementBy(resultSize);

            _FetchRelationCurrent.Decrement();
            _FetchRelationAvgDuration.IncrementBy(Stopwatch.GetTimestamp() - startTicks);
            _FetchRelationAvgDurationBase.Increment();
        }

        public void IncrementSetObjects()
        {
            _SetObjectsCurrent.Increment();
        }
        public void DecrementSetObjects(int objectCount, long startTicks)
        {
            if (!initialized) return;

            _SetObjectsPerSec.Increment();
            _SetObjectsTotal.Increment();
            _SetObjectsObjectsPerSec.IncrementBy(objectCount);
            _SetObjectsObjectsTotal.IncrementBy(objectCount);

            _SetObjectsCurrent.Decrement();
            _SetObjectsAvgDuration.IncrementBy(Stopwatch.GetTimestamp() - startTicks);
            _SetObjectsAvgDurationBase.Increment();
        }

        public void IncrementServerMethodInvocation()
        {
            if (!initialized) return;
            _ServerMethodInvocationPerSec.Increment();
            _ServerMethodInvocationTotal.Increment();
        }
        #endregion

    }
}
