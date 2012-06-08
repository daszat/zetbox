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

namespace Zetbox.API.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server.Mocks;
    using NUnit.Framework;
    
    [SetUpFixture]
    public class SetUpFixture : AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder.RegisterModule(new Zetbox.API.ApiModule());
            builder.RegisterModule(new Zetbox.API.Server.ServerApiModule());

            builder
                .RegisterType<MetaDataResolverMock>()
                .As<IMetaDataResolver>()
                .InstancePerDependency();

            builder.Register(c => new ZetboxContextMock(c.Resolve<IMetaDataResolver>(), null, c.Resolve<ZetboxConfig>(), c.Resolve<Func<IFrozenContext>>(), c.Resolve<InterfaceType.Factory>()))
                .As<IZetboxContext>()
                .As<IFrozenContext>()
                .As<IReadOnlyZetboxContext>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Zetbox.API.Server.Tests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }
    }
}
