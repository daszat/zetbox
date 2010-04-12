using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Autofac;
using Autofac.Integration.Wcf;

using Kistl.API;
using Kistl.API.Configuration;
using Kistl.API.Utils;
using Kistl.App.GUI;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.Server.Tests
{
    [SetUpFixture]
    public class SetUp : Kistl.API.AbstractConsumerTests.DatabaseResetup, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.Server.SetUp");

        private static IContainer container;
        private IKistlAppDomain manager;

        internal static ILifetimeScope CreateInnerContainer()
        {
            return container.BeginLifetimeScope();
        }

        [SetUp]
        public void Init()
        {
            using (Log.InfoTraceMethodCall("Starting up"))
            {
                var config = KistlConfig.FromFile("Kistl.Server.Tests.Config.xml");
                config.Server.DocumentStore = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Server");

                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                var builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Server.Modules);

                container = builder.Build();
                AutofacServiceHostFactory.Container = container;

                ResetDatabase(config);

                manager = container.Resolve<IKistlAppDomain>();

                manager.Start(config);
            }
        }

        [TearDown]
        public void TearDown()
        {
            lock (typeof(SetUp))
            {
                if (manager != null)
                {
                    using (Log.InfoTraceMethodCall("Shutting down"))
                    {
                        manager.Stop();
                        manager = null;
                        container.Dispose();
                        container = null;
                    }
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            TearDown();
        }

        #endregion
    }
}
