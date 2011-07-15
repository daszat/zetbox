namespace Kistl.API.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using System.Diagnostics;
    using Kistl.API.Utils;

    public class BasePerfCounterDispatcher : IBasePerfCounter
    {
        private readonly IEnumerable<IBasePerfCounterAppender> _appender;
        private static IEnumerable<IBasePerfCounterAppender> Empty = new IBasePerfCounterAppender[] { };

        public BasePerfCounterDispatcher(IEnumerable<IBasePerfCounterAppender> appender)
        {
            this._appender = appender;
        }

        public long IncrementFetchRelation(InterfaceType ifType)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementFetchRelation(ifType);
            }
            return DateTime.Now.Ticks;
        }

        public void DecrementFetchRelation(InterfaceType ifType, int resultSize, long startTicks)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.DecrementFetchRelation(ifType, resultSize, startTicks);
            }
        }


        public long IncrementGetList(InterfaceType ifType)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementGetList(ifType);
            }
            return DateTime.Now.Ticks;
        }

        public void DecrementGetList(InterfaceType ifType, int resultSize, long startTicks)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.DecrementGetList(ifType, resultSize, startTicks);
            }
        }

        public long IncrementGetListOf(InterfaceType ifType)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementGetListOf(ifType);
            }
            return DateTime.Now.Ticks;
        }

        public void DecrementGetListOf(InterfaceType ifType, int resultSize, long startTicks)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.DecrementGetListOf(ifType, resultSize, startTicks);
            }
        }

        public void IncrementQuery(InterfaceType ifType)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementQuery(ifType);
            }
        }

        public void IncrementServerMethodInvocation()
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementServerMethodInvocation();
            }
        }

        public long IncrementSetObjects()
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementSetObjects();
            }
            return DateTime.Now.Ticks;
        }

        public void DecrementSetObjects(int objectCount, long startTicks)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.DecrementSetObjects(objectCount, startTicks);
            }
        }


        public long IncrementSubmitChanges()
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementSubmitChanges();
            }
            return DateTime.Now.Ticks;
        }
        public void DecrementSubmitChanges(int objectCount, long startTicks)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.DecrementSubmitChanges(objectCount, startTicks);
            }
        }

        public void Initialize(IFrozenContext frozenCtx)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.Initialize(frozenCtx);
            }
        }

        public void Install()
        {
            foreach (var a in _appender ?? Empty)
            {
                a.Install();
            }
        }

        public void Uninstall()
        {
            foreach (var a in _appender ?? Empty)
            {
                a.Uninstall();
            }
        }

        public void Dump()
        {
            foreach (var a in _appender ?? Empty)
            {
                a.Dump();
            }
        }
    }

}
