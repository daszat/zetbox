
namespace Zetbox.API.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Utils;
    using System.Diagnostics;

    public abstract class BasePerfMonAppender : IBasePerfCounterAppender
    {
        public readonly string InstanceName;
        public abstract string Category { get; }

        #region Fields
        private readonly MethodPerformanceCounter _FetchRelation = new MethodPerformanceCounter();
        private readonly MethodPerformanceCounter _GetList = new MethodPerformanceCounter();
        private readonly MethodPerformanceCounter _GetListOf = new MethodPerformanceCounter();
        private readonly MethodPerformanceCounter _Queries = new MethodPerformanceCounter();
        private readonly MethodPerformanceCounter _SetObjects = new MethodPerformanceCounter();
        private readonly MethodPerformanceCounter _SubmitChanges = new MethodPerformanceCounter();

        PerformanceCounter _ServerMethodInvocationPerSec;
        PerformanceCounter _ServerMethodInvocationTotal;
        #endregion

        public BasePerfMonAppender(Zetbox.API.Configuration.ZetboxConfig cfg)
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

        protected sealed class MethodPerformanceCounter
        {
            PerformanceCounter AvgDuration;
            PerformanceCounter AvgDurationBase;
            PerformanceCounter Current;
            PerformanceCounter ObjectsPerSec;
            PerformanceCounter ObjectsTotal;
            PerformanceCounter PerSec;
            PerformanceCounter Total;

            public class Desc
            {
                public Desc(string name, string verb, Func<BasePerfMonAppender, MethodPerformanceCounter> accessor)
                {
                    this.Name = name;
                    this.Verb = verb;
                    this.Accessor = accessor;
                }

                public readonly string Name;
                public readonly string Verb;
                public readonly Func<BasePerfMonAppender, MethodPerformanceCounter> Accessor;

                public CounterDesc[] CreateDescriptors()
                {
                    return new[] {
                        new CounterDesc(Name + "AvgDuration", string.Format("Avg. duration of {0} calls", Name), PerformanceCounterType.AverageTimer32, (pma, desc) => Accessor(pma).AvgDuration = desc.Get(pma)),
                        new CounterDesc(Name + "AvgDurationBase", string.Format("Base for avg. duration of {0} calls", Name), PerformanceCounterType.AverageBase, (pma, desc) => Accessor(pma).AvgDurationBase = desc.Get(pma)),
                        new CounterDesc(Name + "Current", string.Format("Current # of {0} calls.", Name), PerformanceCounterType.NumberOfItems64, (pma, desc) => Accessor(pma).Current = desc.Get(pma)),
                        new CounterDesc(Name + "ObjectsPerSec", string.Format("# Objects {1} by {0} / sec.", Name, Verb), PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => Accessor(pma).ObjectsPerSec = desc.Get(pma)),
                        new CounterDesc(Name + "ObjectsTotal", string.Format("# Objects {1} by {0}.", Name, Verb), PerformanceCounterType.NumberOfItems64, (pma, desc) => Accessor(pma).ObjectsTotal = desc.Get(pma)),
                        new CounterDesc(Name + "PerSec", string.Format("# of {0} calls / sec.", Name), PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => Accessor(pma).PerSec = desc.Get(pma)),
                        new CounterDesc(Name + "Total", string.Format("# of {0} calls.", Name), PerformanceCounterType.NumberOfItems64, (pma, desc) => Accessor(pma).Total = desc.Get(pma)),
                    };
                }
            }

            public void Increment()
            {
                Current.Increment();
            }

            public void Decrement(int resultSize, long startTicks, long endTicks)
            {
                PerSec.Increment();
                Total.Increment();
                ObjectsPerSec.IncrementBy(resultSize);
                ObjectsTotal.IncrementBy(resultSize);

                Current.Decrement();
                AvgDuration.IncrementBy(endTicks - startTicks);
                AvgDurationBase.Increment();
            }
        }

        protected abstract CounterDesc[] CounterDesciptors { get; }
        protected abstract MethodPerformanceCounter.Desc[] MethodCounterDesciptors { get; }

        public void Install()
        {
            Uninstall();

            Logging.Log.Info("Installing performance counter");
            CounterCreationDataCollection counters = new CounterCreationDataCollection();

            foreach (var desc in _counterDescs.Concat(CounterDesciptors).Concat(_methodDescs.Concat(MethodCounterDesciptors).SelectMany(desc => desc.CreateDescriptors())))
            {
                counters.Add(desc.Create());
            }

            PerformanceCounterCategory.Create(Category, "A custom counter category that tracks Zetbox executions",
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
                Logging.Log.Warn("PerfCounters are not installed, execute 'sudo Zetbox.*.exe -installperfcounter'");
                return;
            }

            try
            {
                foreach (var desc in _counterDescs.Concat(CounterDesciptors).Concat(_methodDescs.Concat(MethodCounterDesciptors).SelectMany(desc => desc.CreateDescriptors())))
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

        public void Dump(bool force) { }

        protected bool initialized = false;

        #region Base Descriptors

        private static readonly MethodPerformanceCounter.Desc[] _methodDescs = new[]
        {
            new MethodPerformanceCounter.Desc("FetchRelation", "returned", pma => pma._FetchRelation),
            new MethodPerformanceCounter.Desc("GetList", "returned", pma => pma._GetList),
            new MethodPerformanceCounter.Desc("GetListOf", "returned", pma => pma._GetListOf),
            new MethodPerformanceCounter.Desc("Queries", "returned", pma => pma._Queries),
            new MethodPerformanceCounter.Desc("SetObjects", "returned", pma => pma._SetObjects),
            new MethodPerformanceCounter.Desc("SubmitChanges", "submitted", pma => pma._SubmitChanges),
        };

        private static readonly CounterDesc[] _counterDescs = new[] 
        { 
            new CounterDesc("ServerMethodInvocationPerSec", "# of ServerMethodInvocation calls / sec.", PerformanceCounterType.RateOfCountsPerSecond32, (pma, desc) => pma._ServerMethodInvocationPerSec = desc.Get(pma)),
            new CounterDesc("ServerMethodInvocationTotal", "# of ServerMethodInvocation calls.", PerformanceCounterType.NumberOfItems64, (pma, desc) => pma._ServerMethodInvocationTotal = desc.Get(pma)),
        };

        #endregion

        #region Common Counters

        public void IncrementFetchRelation(InterfaceType ifType)
        {
            if (!initialized) return;
            _FetchRelation.Increment();
        }

        public void DecrementFetchRelation(InterfaceType ifType, int resultSize, long startTicks, long endTicks)
        {
            if (!initialized) return;
            _FetchRelation.Decrement(resultSize, startTicks, endTicks);
        }

        public void IncrementGetList(InterfaceType ifType)
        {
            if (!initialized) return;
            _GetList.Increment();
        }

        public void DecrementGetList(InterfaceType ifType, int resultSize, long startTicks, long endTicks)
        {
            if (!initialized) return;
            _GetList.Decrement(resultSize, startTicks, endTicks);
        }

        public void IncrementGetListOf(InterfaceType ifType)
        {
            if (!initialized) return;
            _GetListOf.Increment();
        }

        public void DecrementGetListOf(InterfaceType ifType, int resultSize, long startTicks, long endTicks)
        {
            if (!initialized) return;
            _GetListOf.Decrement(resultSize, startTicks, endTicks);
        }

        public void IncrementQuery(InterfaceType ifType)
        {
            if (!initialized) return;
            _Queries.Increment();
        }

        public void DecrementQuery(InterfaceType ifType, int objectCount, long startTicks, long endTicks)
        {
            if (!initialized) return;
            _Queries.Decrement(objectCount, startTicks, endTicks);
        }

        public void IncrementSetObjects()
        {
            if (!initialized) return;
            _SetObjects.Increment();
        }
        public void DecrementSetObjects(int objectCount, long startTicks, long endTicks)
        {
            if (!initialized) return;
            _SetObjects.Decrement(objectCount, startTicks, endTicks);
        }

        public void IncrementSubmitChanges()
        {
            if (!initialized) return;
            _SubmitChanges.Increment();
        }

        public void DecrementSubmitChanges(int objectCount, long startTicks, long endTicks)
        {
            if (!initialized) return;
            _SubmitChanges.Decrement(objectCount, startTicks, endTicks);
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
