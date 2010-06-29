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
    public abstract class AbstractSetUpFixture
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.AbstractSetup");

        static AbstractSetUpFixture()
        {
            Logging.Configure();
        }

        private static IContainer container;

        public static ILifetimeScope BeginLifetimeScope()
        {
            return container.BeginLifetimeScope();
        }

        protected abstract string GetConfigFile();
        protected abstract HostType GetHostType();

        [SetUp]
        public void SetUpTestFixture()
        {
            using (Log.InfoTraceMethodCall("Starting up"))
            {
                var config = KistlConfig.FromFile(GetConfigFile());
                if (config.Server != null) config.Server.DocumentStore = System.IO.Path.Combine(System.IO.Path.GetTempPath(), GetHostType().ToString());
                if (config.Client != null) config.Client.DocumentStore = System.IO.Path.Combine(System.IO.Path.GetTempPath(), GetHostType().ToString());

                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                ContainerBuilder builder;
                switch (GetHostType())
                {
                    case HostType.Server:
                        Log.Info("Adding Server Modules");
                        builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Server.Modules);
                        break;
                    case HostType.Client:
                        Log.Info("Adding Client Modules");
                        builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);
                        break;
                    case HostType.None:
                        Log.Info("Adding no Modules");
                        builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config);
                        break;
                    default:
                        throw new InvalidOperationException("GetHostType() returned an unknown type");
                }

                // TODO: totally replace this with test mocks?
                Log.Info("Adding Interface Module");
                builder.RegisterModule<Kistl.Objects.InterfaceModule>();
                
                SetupBuilder(builder);
                container = builder.Build();
                SetUp(container);
            }
        }

        protected virtual void SetupBuilder(ContainerBuilder builder)
        {

        }

        protected virtual void SetUp(IContainer container)
        {

        }


        [TearDown]
        public virtual void TearDown()
        {
            using (Log.InfoTraceMethodCall("Shutting down"))
            {
                container.Dispose();
                container = null;
            }
        }
    }
}
