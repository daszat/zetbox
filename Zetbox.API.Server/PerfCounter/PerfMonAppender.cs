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
    using Zetbox.API;
    using Zetbox.API.PerfCounter;
    using Zetbox.API.Utils;
    using Zetbox.API.Configuration;
    using System.ComponentModel;

    public class PerfMonAppender : BasePerfMonAppender, IPerfCounterAppender
    {
        [Feature]
        [Description("PerfCounter writing to the Windows Performance Monitor")]
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                moduleBuilder
                    .RegisterType<PerfMonAppender>()
                    .As<IPerfCounterAppender>()
                    .SingleInstance();
            }
        }

        public override string Category { get { return "Zetbox Server"; } }

        public PerfMonAppender(Zetbox.API.Configuration.ZetboxConfig cfg)
            : base(cfg)
        {
        }

        #region Counter Descriptors
        
        protected override CounterDesc[] CounterDesciptors
        {
            get { return _counterDescs; }
        }

        private static readonly CounterDesc[] _counterDescs = new CounterDesc[] 
        { 
        };

        protected override MethodPerformanceCounter.Desc[] MethodCounterDesciptors
        {
            get { return _methodDescs; }
        }

        private MethodPerformanceCounter.Desc[] _methodDescs = new MethodPerformanceCounter.Desc[] 
        {
        };

        #endregion
    }
}
