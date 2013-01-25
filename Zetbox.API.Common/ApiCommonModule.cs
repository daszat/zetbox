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

namespace Zetbox.API.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.Common.GUI;

    public class ApiCommonModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterModule<Zetbox.API.ApiModule>();

            builder
                .RegisterType<Zetbox.API.Common.Reporting.LoggingErrorReporter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<CachingMetaDataResolver>()
                .As<CachingMetaDataResolver>()
                .As<IMetaDataResolver>()
                .SingleInstance();

            builder
                .RegisterType<InvocationExecutor>()
                .As<IInvocationExecutor>()
                .SingleInstance();

            builder
                .RegisterType<IconConverter>()
                .As<IIconConverter>()
                .SingleInstance();

            builder.RegisterModule<Zetbox.Objects.InterfaceModule>();
            builder.RegisterModule((Module)Activator.CreateInstance(Type.GetType("Zetbox.DalProvider.Memory.MemoryProvider, Zetbox.DalProvider.Memory", true)));
        }
    }
}
