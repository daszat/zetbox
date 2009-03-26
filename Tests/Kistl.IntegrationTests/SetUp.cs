using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Configuration;
using Kistl.App.GUI;
using Kistl.Client;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.IntegrationTests
{
    [SetUpFixture]
    public class SetUp : Kistl.API.AbstractConsumerTests.DatabaseResetup, IDisposable
    {
        private ServerDomainManager manager;

        [SetUp]
        public void Init()
        {
            System.Diagnostics.Trace.WriteLine("Setting up Kistl");

            var config = KistlConfig.FromFile("Kistl.IntegrationTests.Config.xml");

            ResetDatabase(config);

            manager = new ServerDomainManager();
            manager.Start(config);

            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
            var testCtx = new GuiApplicationContext(config, "WPF");

            System.Diagnostics.Trace.WriteLine("Setting up Kistl finished");
        }

        [TearDown]
        public void TearDown()
        {
            lock (typeof(SetUp))
            {
                if (manager != null)
                {
                    System.Diagnostics.Trace.WriteLine("Shutting down Kistl");
                    manager.Stop();
                    manager = null;
                    System.Diagnostics.Trace.WriteLine("Shutting down Kistl finished");
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
