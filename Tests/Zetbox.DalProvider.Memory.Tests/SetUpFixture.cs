
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
