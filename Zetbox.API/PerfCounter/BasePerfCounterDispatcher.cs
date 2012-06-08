// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
namespace Zetbox.API.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;

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
