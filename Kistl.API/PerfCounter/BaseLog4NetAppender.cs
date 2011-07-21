namespace Kistl.API.PerfCounter
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

    public abstract class BaseLog4NetAppender : IBasePerfCounterAppender
    {
        protected static readonly object sync = new object();
        private readonly static ILog _mainLogger = LogManager.GetLogger("Kistl.PerfCounter.Main");
        private readonly static ILog _objectsLogger = LogManager.GetLogger("Kistl.PerfCounter.Objects");
        private int dumpCounter = 0;
        private const int DUMPCOUNTERMAX = 100;
        private const long TICKS_TO_MILLIS = 10000;

        #region Counters
        private long QueriesTotal = 0;
        private long QueriesObjectsTotal;
        private long QueriesSumDuration;
        private long SubmitChangesTotal = 0;
        private long SubmitChangesObjectsTotal = 0;
        private long SubmitChangesSumDuration = 0;
        private long GetListTotal = 0;
        private long GetListObjectsTotal = 0;
        private long GetListSumDuration = 0;
        private long GetListOfTotal = 0;
        private long GetListOfObjectsTotal = 0;
        private long GetListOfSumDuration = 0;
        private long FetchRelationTotal = 0;
        private long FetchRelationObjectsTotal = 0;
        private long FetchRelationSumDuration = 0;
        private long SetObjectsTotal = 0;
        private long SetObjectsObjectsTotal = 0;
        private long SetObjectsSumDuration = 0;
        private long ServerMethodInvocation = 0;
        #endregion

        public BaseLog4NetAppender()
        {
        }

        public void Install()
        {
        }

        public void Uninstall()
        {
        }

        public void Initialize(IFrozenContext frozenCtx)
        {
        }

        protected abstract long[] Values { get; }

        private long Avg(long duration, long count)
        {
            return count != 0 ? duration / count / TICKS_TO_MILLIS : 0;
        }

        public void Dump()
        {
            lock (sync)
            {
                try
                {
                    if (_objectsLogger != null)
                    {
                        foreach (var i in _objects)
                        {
                            var vals = new object[] {
                                i.Value.ClassName,
                                i.Value.QueriesTotal,
                                i.Value.QueriesObjectsTotal,
                                i.Value.QueriesSumDuration / TICKS_TO_MILLIS,
                                Avg(i.Value.QueriesSumDuration, i.Value.QueriesTotal),
                                i.Value.GetListTotal,
                                i.Value.GetListObjectsTotal,
                                i.Value.GetListSumDuration / TICKS_TO_MILLIS,
                                Avg(i.Value.GetListSumDuration, i.Value.GetListTotal),
                                i.Value.GetListOfTotal,
                                i.Value.GetListOfObjectsTotal,
                                i.Value.GetListOfSumDuration / TICKS_TO_MILLIS,
                                Avg(i.Value.GetListOfSumDuration, i.Value.GetListOfTotal),
                                i.Value.FetchRelationObjectsTotal,
                                i.Value.FetchRelationTotal,
                                i.Value.FetchRelationSumDuration / TICKS_TO_MILLIS,
                                Avg(i.Value.FetchRelationSumDuration, i.Value.FetchRelationTotal),
                            };
                            _objectsLogger.InfoFormat(string.Format(GetFormatString(vals.Length), vals));
                        }
                        _objects = new Dictionary<string, ObjectsPerfCounter>();
                    }

                    if (_mainLogger != null)
                    {
                        var vals = new long[] {
                            this.QueriesTotal,
                            this.QueriesObjectsTotal,
                            this.QueriesSumDuration / TICKS_TO_MILLIS,
                            Avg(this.QueriesSumDuration, this.QueriesTotal),
                            this.GetListTotal,
                            this.GetListObjectsTotal,
                            this.GetListSumDuration / TICKS_TO_MILLIS,
                            Avg(this.GetListSumDuration, this.GetListTotal),
                            this.GetListOfTotal,
                            this.GetListOfObjectsTotal,
                            this.GetListOfSumDuration / TICKS_TO_MILLIS,
                            Avg(this.GetListOfSumDuration, this.GetListOfTotal),
                            this.FetchRelationTotal,
                            this.FetchRelationObjectsTotal,
                            this.FetchRelationSumDuration / TICKS_TO_MILLIS,
                            Avg(this.FetchRelationSumDuration, this.FetchRelationTotal),
                            this.ServerMethodInvocation,
                            this.SetObjectsTotal,
                            this.SetObjectsObjectsTotal,
                            this.SetObjectsSumDuration / TICKS_TO_MILLIS,
                            Avg(this.SetObjectsSumDuration, this.SetObjectsTotal),
                            this.SubmitChangesTotal,
                            this.SubmitChangesObjectsTotal,
                            this.SubmitChangesSumDuration,
                            Avg(this.SubmitChangesSumDuration, this.SubmitChangesTotal),
                        }.Concat(Values).ToArray();

                        _mainLogger.InfoFormat(string.Format(GetFormatString(vals.Length), vals.Cast<object>().ToArray()));

                        ResetValues();
                    }
                }
                catch
                {
                    // don't care
                }
            }
        }

        private static string GetFormatString(int count)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.AppendFormat("{{{0}}};", i);
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        protected virtual void ResetValues()
        {
            this.QueriesTotal =
            this.QueriesObjectsTotal =
            this.QueriesSumDuration =
            this.GetListTotal =
            this.GetListObjectsTotal =
            this.GetListSumDuration =
            this.GetListOfTotal =
            this.GetListOfObjectsTotal =
            this.GetListOfSumDuration =
            this.FetchRelationTotal =
            this.FetchRelationObjectsTotal =
            this.FetchRelationSumDuration =
            this.ServerMethodInvocation =
            this.SetObjectsTotal =
            this.SetObjectsObjectsTotal =
            this.SetObjectsSumDuration =
            this.SubmitChangesTotal =
            this.SubmitChangesObjectsTotal =
            this.SubmitChangesSumDuration = 0;
        }

        protected void ShouldDump()
        {
            if (++dumpCounter >= DUMPCOUNTERMAX)
            {
                Dump();
                dumpCounter = 0;
            }
        }

        private Dictionary<string, ObjectsPerfCounter> _objects = new Dictionary<string, ObjectsPerfCounter>();
        private ObjectsPerfCounter Get(InterfaceType ifType)
        {
            var name = ifType.Type.FullName;
            ObjectsPerfCounter result;
            if (!_objects.TryGetValue(name, out result))
            {
                result = new ObjectsPerfCounter(name);
                _objects[name] = result;
            }
            return result;
        }

        public void IncrementQuery(InterfaceType ifType)
        {
        }

        public void DecrementQuery(InterfaceType ifType, int objectCount, long startTicks)
        {
            lock (sync)
            {
                var duration = Stopwatch.GetTimestamp() - startTicks;
                QueriesTotal++;
                QueriesObjectsTotal += objectCount;
                QueriesSumDuration += duration;

                Get(ifType).QueriesTotal++;
                Get(ifType).QueriesObjectsTotal += objectCount;
                Get(ifType).QueriesSumDuration += duration;

                ShouldDump();
            }
        }

        public void IncrementSubmitChanges()
        {
        }

        public void DecrementSubmitChanges(int objectCount, long startTicks)
        {
            lock (sync)
            {
                var duration = Stopwatch.GetTimestamp() - startTicks;

                SubmitChangesTotal++;
                SubmitChangesObjectsTotal += objectCount;
                SubmitChangesSumDuration += duration;

                ShouldDump();
            }
        }


        public void IncrementGetList(InterfaceType ifType)
        {
        }
        public void DecrementGetList(InterfaceType ifType, int resultSize, long startTicks)
        {
            lock (sync)
            {
                var duration = Stopwatch.GetTimestamp() - startTicks;

                GetListTotal++;
                GetListObjectsTotal += resultSize;
                GetListSumDuration += duration;
                Get(ifType).GetListTotal++;
                Get(ifType).GetListObjectsTotal += resultSize;
                Get(ifType).GetListSumDuration += duration;

                ShouldDump();
            }
        }

        public void IncrementGetListOf(InterfaceType ifType)
        {
        }
        public void DecrementGetListOf(InterfaceType ifType, int resultSize, long startTicks)
        {
            lock (sync)
            {
                var duration = Stopwatch.GetTimestamp() - startTicks;

                GetListOfTotal++;
                GetListOfObjectsTotal += resultSize;
                GetListOfSumDuration += duration;
                Get(ifType).GetListOfTotal++;
                Get(ifType).GetListOfObjectsTotal += resultSize;
                Get(ifType).GetListOfSumDuration += duration;

                ShouldDump();
            }
        }

        public void IncrementFetchRelation(InterfaceType ifType)
        {
        }
        public void DecrementFetchRelation(InterfaceType ifType, int resultSize, long startTicks)
        {
            lock (sync)
            {
                var duration = Stopwatch.GetTimestamp() - startTicks;

                FetchRelationTotal++;
                FetchRelationObjectsTotal += resultSize;
                FetchRelationSumDuration += duration;
                Get(ifType).FetchRelationTotal++;
                Get(ifType).FetchRelationObjectsTotal += resultSize;
                Get(ifType).FetchRelationSumDuration += duration;
            
                ShouldDump();
            }
        }

        public void IncrementSetObjects()
        {
        }

        public void DecrementSetObjects(int objectCount, long startTicks)
        {
            lock (sync)
            {
                var duration = Stopwatch.GetTimestamp() - startTicks;

                SetObjectsTotal++;
                SetObjectsObjectsTotal += objectCount;
                SetObjectsSumDuration += duration;

                ShouldDump();
            }
        }

        public void IncrementServerMethodInvocation()
        {
            lock (sync)
            {
                ServerMethodInvocation++;

                ShouldDump();
            }
        }
    }
}
