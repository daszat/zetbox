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
                            _objectsLogger.InfoFormat("{0}; {1}; {2}; {3}; {4}; {5}; {6}; {7}", i.Value.ClassName,
                                i.Value.QueriesTotal,
                                i.Value.GetListTotal,
                                i.Value.GetListObjectsTotal,
                                i.Value.GetListOfTotal,
                                i.Value.GetListOfObjectsTotal,
                                i.Value.FetchRelationObjectsTotal,
                                i.Value.FetchRelationTotal);
                        }
                        _objects = new Dictionary<string, ObjectsPerfCounter>();
                    }

                    if (_mainLogger != null)
                    {
                        var vals = new long[] {
                                                this.QueriesTotal,
                        this.GetListTotal,
                        this.GetListObjectsTotal,
                        this.GetListOfTotal,
                        this.GetListOfObjectsTotal,
                        this.FetchRelationTotal,
                        this.FetchRelationObjectsTotal,
                        this.ServerMethodInvocation,
                        this.SetObjectsTotal,
                        this.SetObjectsObjectsTotal,
                        this.SubmitChangesTotal,
                        this.SubmitChangesObjectsTotal,
                    }.Concat(Values).ToArray();

                        var sb = new StringBuilder();
                        for (int i = 0; i < vals.Length; i++)
                        {
                            sb.AppendFormat("{{{0}}};", i);
                        }
                        sb.Remove(sb.Length - 1, 1);

                        _mainLogger.InfoFormat(string.Format(sb.ToString(), vals.Cast<object>().ToArray()));

                        ResetValues();
                    }
                }
                catch
                {
                    // don't care
                }
            }
        }

        protected virtual void ResetValues()
        {
            this.QueriesTotal =
            this.GetListTotal =
            this.GetListObjectsTotal =
            this.GetListOfTotal =
            this.GetListOfObjectsTotal =
            this.FetchRelationTotal =
            this.FetchRelationObjectsTotal =
            this.ServerMethodInvocation =
            this.SetObjectsTotal =
            this.SetObjectsObjectsTotal =
            this.SubmitChangesTotal =
            this.SubmitChangesObjectsTotal = 0;
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

        private long QueriesTotal = 0;
        public void IncrementQuery(InterfaceType ifType)
        {
            lock (sync)
            {
                QueriesTotal++;
                Get(ifType).QueriesTotal++;

                ShouldDump();
            }
        }

        private long SubmitChangesTotal = 0;
        private long SubmitChangesObjectsTotal = 0;
        public void IncrementSubmitChanges()
        {
        }

        public void DecrementSubmitChanges(int objectCount, long startTicks)
        {
            lock (sync)
            {
                SubmitChangesTotal++;
                SubmitChangesObjectsTotal += objectCount;

                ShouldDump();
            }
        }


        private long GetListTotal = 0;
        private long GetListObjectsTotal = 0;
        public void IncrementGetList(InterfaceType ifType)
        {
        }
        public void DecrementGetList(InterfaceType ifType, int resultSize, long startTicks)
        {
            lock (sync)
            {
                GetListTotal++;
                GetListObjectsTotal += resultSize;
                Get(ifType).GetListTotal++;
                Get(ifType).GetListObjectsTotal += resultSize;

                ShouldDump();
            }
        }

        private long GetListOfTotal = 0;
        private long GetListOfObjectsTotal = 0;
        public void IncrementGetListOf(InterfaceType ifType)
        {
        }
        public void DecrementGetListOf(InterfaceType ifType, int resultSize, long startTicks)
        {
            lock (sync)
            {
                GetListOfTotal++;
                GetListOfObjectsTotal += resultSize;
                Get(ifType).GetListOfTotal++;
                Get(ifType).GetListOfObjectsTotal += resultSize;

                ShouldDump();
            }
        }

        private long FetchRelationTotal = 0;
        private long FetchRelationObjectsTotal = 0;
        public void IncrementFetchRelation(InterfaceType ifType)
        {
        }
        public void DecrementFetchRelation(InterfaceType ifType, int resultSize, long startTicks)
        {
            lock (sync)
            {
                FetchRelationTotal++;
                FetchRelationObjectsTotal += resultSize;
                Get(ifType).FetchRelationTotal++;
                Get(ifType).FetchRelationObjectsTotal += resultSize;
            
                ShouldDump();
            }
        }

        private long SetObjectsTotal = 0;
        private long SetObjectsObjectsTotal = 0;
        public void IncrementSetObjects()
        {
        }

        public void DecrementSetObjects(int objectCount, long startTicks)
        {
            lock (sync)
            {
                SetObjectsTotal++;
                SetObjectsObjectsTotal += objectCount;

                ShouldDump();
            }
        }

        private long ServerMethodInvocation = 0;
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
