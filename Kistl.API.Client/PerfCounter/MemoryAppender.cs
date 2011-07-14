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

    public class MemoryAppender : IPerfCounterAppender
    {
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                moduleBuilder
                    .RegisterType<MemoryAppender>()
                    .As<IPerfCounterAppender>()
                    .SingleInstance();
            }
        }

        private static readonly object _lock = new object();
        private readonly static ILog _mainLogger = LogManager.GetLogger("Kistl.PerfCounter.Main");
        private readonly static ILog _objectsLogger = LogManager.GetLogger("Kistl.PerfCounter.Objects");
        private int dumpCounter = 0;
        private const int DUMPCOUNTERMAX = 100;

        public MemoryAppender()
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

        public void Dump()
        {
            lock (_lock)
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
                    _mainLogger.InfoFormat("{0}; {1}; {2}; {3}; {4}; {5}; {6}; {7}; {8}; {9}; {10}; {11}; {12}; {13}",
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
                        this.ViewModelFetch,
                        this.ViewModelCreate);

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
                    this.SubmitChangesObjectsTotal = 
                    this.ViewModelFetch =
                    this.ViewModelCreate = 0;
                }
            }
        }

        private void ShouldDump()
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
            lock (_lock)
            {
                QueriesTotal++;
                Get(ifType).QueriesTotal++;

                ShouldDump();
            }
        }

        private long SubmitChangesTotal = 0;
        private long SubmitChangesObjectsTotal = 0;
        public void IncrementSubmitChanges(int objectCount)
        {
            lock (_lock)
            {
                SubmitChangesTotal++;
                SubmitChangesObjectsTotal += objectCount;

                ShouldDump();
            }
        }

        private long GetListTotal = 0;
        private long GetListObjectsTotal = 0;
        public void IncrementGetList(InterfaceType ifType, int resultSize)
        {
            lock (_lock)
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
        public void IncrementGetListOf(InterfaceType ifType, int resultSize)
        {
            lock (_lock)
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
        public void IncrementFetchRelation(InterfaceType ifType, int resultSize)
        {
            lock (_lock)
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
        public void IncrementSetObjects(int objectCount)
        {
            lock (_lock)
            {
                SetObjectsTotal++;
                SetObjectsObjectsTotal += objectCount;

                ShouldDump();
            }
        }

        private long ServerMethodInvocation = 0;
        public void IncrementServerMethodInvocation()
        {
            lock (_lock)
            {
                ServerMethodInvocation++;

                ShouldDump();
            }
        }


        private long ViewModelFetch = 0;
        public void IncrementViewModelFetch()
        {
            lock (_lock)
            {
                ViewModelFetch++;

                ShouldDump();
            }
        }

        private long ViewModelCreate = 0;
        public void IncrementViewModelCreate()
        {
            lock (_lock)
            {
                ViewModelCreate++;

                ShouldDump();
            }
        }
    }
}
