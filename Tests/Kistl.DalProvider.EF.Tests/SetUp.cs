
namespace Kistl.DalProvider.EF.Tests
{
    using System;
    using System.Collections.Generic;

    using Autofac;
    
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.App.Base;
    using Kistl.App.Projekte;
    using Kistl.Server;

    using NUnit.Framework;
    using Kistl.DalProvider.EF.Mocks;

    [SetUpFixture]
    public class SetUp
        : Kistl.API.AbstractConsumerTests.DatabaseResetup
    {
        private static IContainer container;

        internal static ILifetimeScope CreateInnerContainer()
        {
            return container.BeginLifetimeScope();
        }

        [SetUp]
        public void Init()
        {
            var config = KistlConfig.FromFile("Kistl.DalProvider.EF.Tests.Config.xml");
            config.Server.DocumentStore = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Server");

            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

            var appCtx = new ServerApiContextMock();

            var builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Server.Modules);
            builder.RegisterInstance(config).ExternallyOwned().SingleInstance();
            container = builder.Build();

            KistlContext.Container = container;

            ResetDatabase(config);

            Property__Implementation__.OnToString_Property
                += (obj, args) => { args.Result = String.Format("Prop, [{0}]", obj.Description); };
            Mitarbeiter__Implementation__.OnToString_Mitarbeiter
                += (obj, args) => { args.Result = String.Format("MA, [{0}]", obj.Name); };
            Projekt__Implementation__.OnToString_Projekt
                += (obj, args) => { args.Result = String.Format("Proj, [{0}]", obj.Name); };
        }
    }
}
