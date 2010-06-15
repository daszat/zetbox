
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;
using NUnit.Framework;
using Kistl.API.Configuration;
using Kistl.API;


internal class MemoryAssemblyConfiguration : IAssemblyConfiguration
{
    #region IAssemblyConfiguration Members

    public string InterfaceAssemblyName
    {
        get { return Kistl.API.Helper.InterfaceAssembly; }
    }

    public IEnumerable<string> AllImplementationAssemblyNames
    {
        get { return new[] { Kistl.API.Helper.MemoryAssembly }; }
    }
    #endregion
}

[SetUpFixture]
public sealed class SetUpFixture : Kistl.API.AbstractConsumerTests.AbstractSetUpFixture
{
    private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.DalProvider.Memory.Tests");

    protected override void SetupBuilder(ContainerBuilder builder)
    {
        base.SetupBuilder(builder);

        // Register modules -> Hosttype = none
        builder.RegisterModule(new Kistl.API.ApiModule());
        builder.RegisterModule(new Kistl.DalProvider.Memory.MemoryProvider());

        builder
           .RegisterType<MemoryAssemblyConfiguration>()
           .As<IAssemblyConfiguration>()
           .SingleInstance();
    }

    protected override string GetConfigFile()
    {
        return "Kistl.DalProvider.Memory.Tests.Config.xml";
    }

    protected override HostType GetHostType()
    {
        return HostType.None;
    }
}
