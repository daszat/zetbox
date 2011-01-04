
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Kistl.API;
using Kistl.API.Configuration;
using Kistl.DalProvider.Memory.Tests;
using NUnit.Framework;

[SetUpFixture]
public sealed class SetUpFixture : Kistl.API.AbstractConsumerTests.AbstractSetUpFixture
{
    protected override void SetupBuilder(ContainerBuilder builder)
    {
        base.SetupBuilder(builder);

        // Register modules -> Hosttype = none
        builder.RegisterModule(new Kistl.API.ApiModule());
        builder.RegisterModule(new Kistl.DalProvider.Memory.MemoryProvider());
        builder.RegisterModule(new Kistl.Objects.MemoryModule());

        builder
            .RegisterType<TestDeploymentRestrictor>()
            .As<IDeploymentRestrictor>()
            .SingleInstance();
    }

    protected override string GetConfigFile()
    {
        return "Kistl.DalProvider.Memory.Tests.Config{0}.xml";
    }

    protected override HostType GetHostType()
    {
        return HostType.None;
    }
}
