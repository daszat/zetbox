using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

using NUnit.Framework;
using Kistl.API;

internal class FrozenAssemblyConfiguration : IAssemblyConfiguration
{
    #region IAssemblyConfiguration Members

    public string InterfaceAssemblyName
    {
        get { return Kistl.API.Helper.InterfaceAssembly; }
    }

    public IEnumerable<string> AllImplementationAssemblyNames
    {
        get { return new[] { Kistl.API.Helper.FrozenAssembly }; }
    }
    #endregion
}

[SetUpFixture]
public class SetUpFixture : Kistl.API.AbstractConsumerTests.AbstractSetUpFixture
{
    protected override void SetupBuilder(ContainerBuilder builder)
    {
        base.SetupBuilder(builder);
        // Register modules -> Hosttype = none
        builder.RegisterModule(new Kistl.API.ApiModule());

        builder
            .RegisterType<FrozenAssemblyConfiguration>()
            .As<IAssemblyConfiguration>()
            .SingleInstance();

    }

    protected override void SetUp(Autofac.IContainer container)
    {
        base.SetUp(container);
        FrozenContext.RegisterTypeTransformations(container.Resolve<ITypeTransformations>());
    }

    protected override string GetConfigFile()
    {
        return "Kistl.DalProvider.Frozen.Tests.Config.xml";
    }

    protected override Kistl.API.HostType GetHostType()
    {
        return Kistl.API.HostType.None;
    }
}
