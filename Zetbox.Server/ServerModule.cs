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

namespace Zetbox.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.Extensions;

    public class ServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<Server>()
                .As<IServer>()
                .SingleInstance();

            builder
                .RegisterType<ThreadPrincipalResolver>()
                .As<IIdentityResolver>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ZetboxService>()
                .As<ZetboxService>() // registration for WCF
                .As<IZetboxService>() // registration for ZetboxServiceFacade
                .InstancePerLifetimeScope();

            builder
                .RegisterType<BootstrapperService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterModule(new SchemaManagement.SchemaModule());

#if !MONO
            builder
                .Register(c => new ActiveDirectoryIdentitySource())
                .As<IIdentitySource>()
                .InstancePerLifetimeScope();
#endif
        }
    }
}
