using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Autofac;
using Autofac.Builder;

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

        private IContainer container;
        private IKistlAppDomain manager;

        [SetUp]
        public void Init()
        {
            using (Log.InfoTraceMethodCall("Starting up"))
            {
                var config = KistlConfig.FromFile("Kistl.Server.Tests.Config.xml");

                ResetDatabase(config);

                var builder = new ContainerBuilder();
                builder.RegisterModule(new ServerModule());
                // TODO: replace this with registering a mocked provider of some kind.
                builder.RegisterModule((IModule)Activator.CreateInstance(Type.GetType(config.Server.StoreProvider)));

                container = builder.Build();

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
