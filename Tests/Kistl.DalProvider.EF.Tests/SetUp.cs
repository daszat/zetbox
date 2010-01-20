using System;
using System.Collections.Generic;

using Autofac;
using Autofac.Builder;

using Kistl.API;
using Kistl.API.Configuration;
using Kistl.Server;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests
{
    [SetUpFixture]
    public class SetUp 
        : Kistl.API.AbstractConsumerTests.DatabaseResetup
    {
        private static IContainer container;

        internal static IContainer CreateInnerContainer()
        {
            return container.CreateInnerContainer();
        }

        [SetUp]
        public void Init()
        {
            var config = KistlConfig.FromFile("Kistl.DalProvider.EF.Tests.Config.xml");

            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
            
            var appCtx = new ServerApplicationContext(config);

            var builder = new ContainerBuilder();

            builder.Register(config).ExternallyOwned().SingletonScoped();
            builder.RegisterModule(new ServerModule());
            builder.RegisterModule(new EfProvider());

            container = builder.Build();

            KistlContext.Container = container;

            ResetDatabase(config);
        }
    }
}
