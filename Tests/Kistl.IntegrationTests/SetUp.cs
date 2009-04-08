using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                Trace.WriteLine("Setting up Kistl");

                var config = KistlConfig.FromFile("Kistl.IntegrationTests.Config.xml");

                ResetDatabase(config);

                manager = new ServerDomainManager();
                manager.Start(config);

                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
                var testCtx = new GuiApplicationContext(config, "WPF");

                Trace.WriteLine("Setting up Kistl finished");
            }
            catch (Exception error)
            {
                Trace.TraceError("Error ({0}) while initialising Integretation Tests: {1}", error.GetType().Name, error.Message);
                Trace.TraceError(error.ToString());
                Trace.TraceError(error.StackTrace);

                throw error;
            }
        }

        [TearDown]
        public void TearDown()
        {
            lock (typeof(SetUp))
            {
                if (manager != null)
                {
                    System.Diagnostics.Trace.WriteLine("Shutting down Kistl");
                    manager.DisableUnloadAppDomainOnShutdown();
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
