namespace Kistl.API.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;

    public abstract class BasePerfCounterDispatcher : IBasePerfCounter
    {
        private readonly IEnumerable<IBasePerfCounterAppender> _appender;
        private static IEnumerable<IBasePerfCounterAppender> Empty = new IBasePerfCounterAppender[] { };

        public BasePerfCounterDispatcher(IEnumerable<IBasePerfCounterAppender> appender)
        {
            this._appender = appender ?? Empty;
        }

        public long IncrementFetchRelation(InterfaceType ifType)
        {
            foreach (var a in _appender)
            {
                a.IncrementFetchRelation(ifType);
            }
            return Stopwatch.GetTimestamp();
        }

        public void DecrementFetchRelation(InterfaceType ifType, int resultSize, long startTicks)
        {
            var endTicks = Stopwatch.GetTimestamp();
            foreach (var a in _appender)
            {
                a.DecrementFetchRelation(ifType, resultSize, startTicks, endTicks);
            }
        }

        public long IncrementGetList(InterfaceType ifType)
        {
            foreach (var a in _appender)
            {
                a.IncrementGetList(ifType);
            }
            return Stopwatch.GetTimestamp();
        }

        public void DecrementGetList(InterfaceType ifType, int resultSize, long startTicks)
        {
            var endTicks = Stopwatch.GetTimestamp();
            foreach (var a in _appender)
            {
                a.DecrementGetList(ifType, resultSize, startTicks, endTicks);
            }
        }

        public long IncrementGetListOf(InterfaceType ifType)
        {
            foreach (var a in _appender)
            {
                a.IncrementGetListOf(ifType);
            }
            return Stopwatch.GetTimestamp();
        }

        public void DecrementGetListOf(InterfaceType ifType, int resultSize, long startTicks)
        {
            var endTicks = Stopwatch.GetTimestamp();
            foreach (var a in _appender)
            {
                a.DecrementGetListOf(ifType, resultSize, startTicks, endTicks);
            }
        }

        public long IncrementQuery(InterfaceType ifType)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementQuery(ifType);
            }
            return Stopwatch.GetTimestamp();
        }

        public void DecrementQuery(InterfaceType ifType, int objectCount, long startTicks)
        {
            var endTicks = Stopwatch.GetTimestamp();
            foreach (var a in _appender)
            {
                a.DecrementQuery(ifType, objectCount, startTicks, endTicks);
            }
        }

        public void IncrementServerMethodInvocation()
        {
            foreach (var a in _appender)
            {
                a.IncrementServerMethodInvocation();
            }
        }

        public long IncrementSetObjects()
        {
            foreach (var a in _appender)
            {
                a.IncrementSetObjects();
            }
            return Stopwatch.GetTimestamp();
        }

        public void DecrementSetObjects(int objectCount, long startTicks)
        {
            var endTicks = Stopwatch.GetTimestamp();
            foreach (var a in _appender)
            {
                a.DecrementSetObjects(objectCount, startTicks, endTicks);
            }
        }

        public long IncrementSubmitChanges()
        {
            foreach (var a in _appender)
            {
                a.IncrementSubmitChanges();
            }
            return Stopwatch.GetTimestamp();
        }

        public void DecrementSubmitChanges(int objectCount, long startTicks)
        {
            var endTicks = Stopwatch.GetTimestamp();
            foreach (var a in _appender)
            {
                a.DecrementSubmitChanges(objectCount, startTicks, endTicks);
            }
        }

        public void Initialize(IFrozenContext frozenCtx)
        {
            foreach (var a in _appender)
            {
                a.Initialize(frozenCtx);
            }
        }

        public void Install()
        {
            foreach (var a in _appender)
            {
                a.Install();
            }
        }

        public void Uninstall()
        {
            foreach (var a in _appender)
            {
                a.Uninstall();
            }
        }

        public void Dump()
        {
            foreach (var a in _appender)
            {
                a.Dump(true);
            }
        }
    }
}
