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
using Autofac;
using Zetbox.API;
using Zetbox.API.Configuration;
using Zetbox.DalProvider.Memory.Tests;
using NUnit.Framework;

[SetUpFixture]
public sealed class SetUpFixture : Zetbox.API.AbstractConsumerTests.AbstractSetUpFixture
{
    protected override void SetupBuilder(ContainerBuilder builder)
    {
        base.SetupBuilder(builder);

        // Register modules -> Hosttype = none
        builder.RegisterModule(new Zetbox.API.ApiModule());
        builder.RegisterModule(new Zetbox.DalProvider.Memory.MemoryProvider());
        builder.RegisterModule(new Zetbox.Objects.MemoryModule());

        builder
            .RegisterType<TestDeploymentRestrictor>()
            .As<IDeploymentRestrictor>()
            .SingleInstance();
    }

    protected override string GetConfigFile()
    {
        return "Zetbox.DalProvider.Memory.Tests.xml";
    }

    protected override HostType GetHostType()
    {
        return HostType.None;
    }
}
