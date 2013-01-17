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
namespace Zetbox.API.Server.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.PerfCounter;
    using Zetbox.API.Configuration;
    using System.ComponentModel;

    // server-side clone of Zetbox.API.Client.PerfCounter.Log4NetAppender
    public class Log4NetAppender : MemoryAppender
    {
        #region Autofac Module
        [Feature]
        [Description("PerfCounter writing to log4net")]
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                moduleBuilder
                    .RegisterType<Log4NetAppender>()
                    .As<IPerfCounterAppender>()
                    .SingleInstance();
            }
        }
        #endregion

        private object dumpLock = new object();
        private int dumpCounter = 0;
        private Stopwatch dumpTimer = Stopwatch.StartNew();
        private const int DUMPCOUNTERMAX = 100;
        private const long DUMPTIMEMIN = 60 * 1000; // one minute in ms

        public Log4NetAppender() { }

        public override void Dump(bool force)
        {
            lock (dumpLock)
            {
                if (force || (++dumpCounter >= DUMPCOUNTERMAX && dumpTimer.ElapsedMilliseconds > DUMPTIMEMIN))
                {
                    var totalsCollector = new Dictionary<string, string>();
                    Dictionary<string, ObjectMemoryCounters> objectsClone;

                    lock (counterLock)
                    {
                        this.FormatTo(totalsCollector);
                        objectsClone = new Dictionary<string, ObjectMemoryCounters>(this.Objects);
                        this.ResetValues();
                    }

                    // don't write to log4net while holding the counterLock
                    Log4NetAppenderUtils.Dump(objectsClone, totalsCollector);

                    dumpCounter = 0;
                    dumpTimer.Reset();
                    dumpTimer.Start();
                }
            }
        }
    }
}
