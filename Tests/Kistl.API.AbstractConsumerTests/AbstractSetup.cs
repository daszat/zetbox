using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Autofac;
using Kistl.API.Utils;
using Kistl.API.Configuration;

namespace Kistl.API.AbstractConsumerTests
{
    public abstract class AbstractSetup
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.AbstractSetup");

        private static IContainer container;

        public static ILifetimeScope BeginLifetimeScope()
        {
            return container.BeginLifetimeScope();
        }

        protected abstract string GetConfigFile();
        protected abstract HostType GetHostType();

        protected virtual void SetupBuilder(ContainerBuilder builder)
        {

        }

        [SetUp]
        public void SetUp()
        {
            using (Log.InfoTraceMethodCall("Starting up"))
            {
                var config = KistlConfig.FromFile(GetConfigFile());
                config.Server.DocumentStore = System.IO.Path.Combine(System.IO.Path.GetTempPath(), GetHostType().ToString());
                config.Client.DocumentStore = System.IO.Path.Combine(System.IO.Path.GetTempPath(), GetHostType().ToString());

                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                ContainerBuilder builder;
                switch (GetHostType())
                {
                    case HostType.Server:
                        builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Server.Modules);
                        break;
                    case HostType.Client:
                        builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);
                        break;
                    case HostType.None:
                        builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config);
                        break;
                    default:
                        throw new InvalidOperationException("GetHostType() returned an unknown type");
                }
                SetupBuilder(builder);
                container = builder.Build();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (Log.InfoTraceMethodCall("Shutting down"))
            {
                container.Dispose();
                container = null;
            }
        }
    }
}
