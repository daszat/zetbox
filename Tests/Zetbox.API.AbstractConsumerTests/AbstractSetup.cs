
namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using NUnit.Framework;

    public abstract class AbstractSetUpFixture
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Tests.AbstractSetup");

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

        /// <summary>
        /// This method is called first for early setup tasks before any Zetbox stuff is touched.
        /// </summary>
        protected virtual void EarlySetup()
        {
        }

        [SetUp]
        public void SetUpTestFixture()
        {
            using (Log.InfoTraceMethodCall("EarlySetup"))
            {
                EarlySetup();
            }
            using (Log.InfoTraceMethodCall("Starting up"))
            {
                var config = ZetboxConfig.FromFile(null, GetConfigFile());

                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                ContainerBuilder builder;
                switch (GetHostType())
                {
                    case HostType.Server:
                        Log.Info("Adding Server Modules");
                        builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Server.Modules);
                        break;
                    case HostType.Client:
                        Log.Info("Adding Client Modules");
                        builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);
                        break;
                    case HostType.None:
                        Log.Info("Adding no Modules");
                        builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config);
                        break;
                    default:
                        throw new InvalidOperationException("GetHostType() returned an unknown type");
                }

                // TODO: totally replace this with test mocks?
                Log.Info("Adding Interface Module");
                builder.RegisterModule<Zetbox.Objects.InterfaceModule>();
                builder.RegisterInstance<TypeMapAssembly>(new TypeMapAssembly(this.GetType().Assembly));
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
            var config = container.Resolve<ZetboxConfig>();
            if (config.Server != null)
            {
                config.Server.DocumentStore = Path.Combine(Path.GetTempPath(), GetHostType().ToString());
                Log.InfoFormat("Setting Server.DocumentStore=[{0}]", config.Server.DocumentStore);

                var resetter = container.Resolve<IEnumerable<IDatabaseResetter>>().SingleOrDefault();
                if (resetter != null && config.Server.ConnectionStrings != null)
                {
                    Log.Info("Forcing test connection string");
                    for (int i = 0; i < config.Server.ConnectionStrings.Length; i++)
                    {
                        config.Server.ConnectionStrings[i].ConnectionString = resetter.ForceTestDB(config.Server.ConnectionStrings[i].ConnectionString);
                    }
                }
            }
        }

        /// <summary>
        /// Call this to reset the configured databases.
        /// </summary>
        /// <param name="container"></param>
        protected void ResetDatabase(IContainer container)
        {
            foreach (var resetter in container.Resolve<IEnumerable<IDatabaseResetter>>())
            {
                try
                {
                    resetter.ResetDatabase();
                }
                catch (Exception ex)
                {
                    Log.Error("Failed to reset database", ex);
                    throw;
                }
            }
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
