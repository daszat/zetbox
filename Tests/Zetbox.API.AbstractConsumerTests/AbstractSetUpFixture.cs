// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using NUnit.Framework;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.Client.Presentables;

    public abstract class AbstractSetUpFixture
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AbstractSetUpFixture));

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

        [OneTimeSetUp]
        public void SetUpTestFixture()
        {
            using (Log.InfoTraceMethodCall("EarlySetup"))
            {
                EarlySetup();
            }
            using (Log.InfoTraceMethodCall("Starting up"))
            {
                var workDir = NUnit.Framework.TestContext.Parameters.Get<string>("WorkDirectory", "");
                if(!string.IsNullOrWhiteSpace(workDir ))
                {
                    System.Environment.CurrentDirectory = workDir;
                }
                var config = ZetboxConfig.FromFile(GetHostType(), null, GetConfigFile());

                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                ContainerBuilder builder;
                switch (GetHostType())
                {
                    case HostType.AspNetService:
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
            builder
                .RegisterType<MockedViewModelFactory>()
                .As<MockedViewModelFactory>()
                .As<IViewModelFactory>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<PerfCounterDispatcher>()
                .As<IPerfCounter>()
                .OnRelease(obj => obj.Dump())
                .SingleInstance();

            builder
                .RegisterType<NopFileOpener>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<MockCredentialsResolver>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        protected virtual void SetUp(IContainer container)
        {
            API.AppDomainInitializer.InitializeFrom(container);
            var config = container.Resolve<ZetboxConfig>();
            if (config.Server != null)
            {
                config.Server.DocumentStore = Path.Combine(Path.GetTempPath(), GetHostType().ToString());
                Log.InfoFormat("Setting Server.DocumentStore=[{0}]", config.Server.DocumentStore);
            }
        }

        [OneTimeTearDown]
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
