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
namespace Zetbox.API.Client.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.PerfCounter;
    using Zetbox.API.Configuration;
    using System.ComponentModel;

    // client-side clone of Zetbox.API.Server.PerfCounter.ResetOnReadAppender
    public class ResetOnReadAppender : MemoryAppender
    {
        #region Autofac Module
        [Feature]
        [Description("PerfCounter: read & save data internal, but reset global data.")]
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                moduleBuilder
                    .RegisterType<ResetOnReadAppender>()
                    .As<IPerfCounterAppender>()
                    .SingleInstance();
            }
        }
        #endregion

        public sealed class Data
        {
            internal Data() { }
            public Dictionary<string, string> Totals = new Dictionary<string, string>();
            public Dictionary<string, ObjectMemoryCounters> Objects;
        }

        public ResetOnReadAppender() { }

        public Data ReadAndResetValues()
        {
            var data = new Data();

            lock (counterLock)
            {
                this.FormatTo(data.Totals);
                data.Objects = new Dictionary<string, ObjectMemoryCounters>(this.Objects);
                this.ResetValues();
            }

            return data;
        }
    }
}
