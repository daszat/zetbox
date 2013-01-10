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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.Configuration;

    [Feature]
    public class UnifiedModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule<Zetbox.API.ApiModule>();
            builder.RegisterModule<Zetbox.API.Server.ServerApiModule>();
            builder.RegisterModule<Zetbox.API.Common.ApiCommonModule>();
            builder.RegisterModule<Zetbox.Server.ServerModule>();
            builder.RegisterModule<Zetbox.Objects.InterfaceModule>();
            builder.RegisterModule((Module)Activator.CreateInstance(Type.GetType("Zetbox.Objects.MemoryModule, Zetbox.Objects.MemoryImpl", true)));
            builder.RegisterModule((Module)Activator.CreateInstance(Type.GetType("Zetbox.DalProvider.Memory.MemoryProvider, Zetbox.DalProvider.Memory", true)));
        }
    }
}
