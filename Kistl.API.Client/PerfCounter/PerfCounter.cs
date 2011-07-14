namespace Kistl.API.Client.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using System.Diagnostics;
    using Kistl.API.Utils;

    public class PerfCounterDispatcher : Kistl.API.Client.PerfCounter.IPerfCounter
    {
        private readonly IEnumerable<IPerfCounterAppender> _appender;
        private static IEnumerable<IPerfCounterAppender> Empty = new IPerfCounterAppender[] { };

        public PerfCounterDispatcher(IEnumerable<IPerfCounterAppender> appender)
        {
            this._appender = appender;
        }

        public void IncrementFetchRelation(InterfaceType ifType, int resultSize)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementFetchRelation(ifType, resultSize);
            }
        }

        public void IncrementGetList(InterfaceType ifType, int resultSize)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementGetList(ifType, resultSize);
            }
        }

        public void IncrementGetListOf(InterfaceType ifType, int resultSize)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementGetListOf(ifType, resultSize);
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

        public void IncrementSetObjects(int objectCount)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementSetObjects(objectCount);
            }
        }

        public void IncrementSubmitChanges(int objectCount)
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementSubmitChanges(objectCount);
            }
        }

        public void IncrementViewModelFetch()
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementViewModelFetch();
            }
        }

        public void IncrementViewModelCreate()
        {
            foreach (var a in _appender ?? Empty)
            {
                a.IncrementViewModelCreate();
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
