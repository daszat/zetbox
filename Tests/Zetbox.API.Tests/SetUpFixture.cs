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

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpFixture : AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            // Register missing Modules. HostType is None!
            builder.RegisterModule(new ApiModule());

            builder
                .RegisterType<Mocks.TestZetboxContext>()
                .As<IZetboxContext>()
                .As<IFrozenContext>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Zetbox.API.Tests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.None;
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);
        }
    }
}
