
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;
using NUnit.Framework;
using Kistl.API.Configuration;
using Kistl.API;

[SetUpFixture]
public sealed class Setup
{
    private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.DalProvider.Memory.Tests");

    public static IContainer MasterContainer { get; private set; }

    [SetUp]
    public void CreateMasterContainer()
    {
        log4net.Config.BasicConfigurator.Configure();
        AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, KistlConfig.FromFile("Kistl.DalProvider.Memory.Tests.Config.xml"));

        Log.Info("Setting up master container");
        var builder = new ContainerBuilder();

        builder.RegisterModule(new Kistl.API.ApiModule());
        builder.RegisterModule(new Kistl.DalProvider.Memory.ClientProvider());

        MasterContainer = builder.Build();
    }

    [TearDown]
    public void DisposeMasterContainer()
    {
        Log.Info("Removing master container");
        if (MasterContainer != null)
        {
            MasterContainer.Dispose();
            MasterContainer = null;
        }
    }
}
