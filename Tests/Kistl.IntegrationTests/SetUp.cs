using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Configuration;
using Kistl.API.Utils;
using Kistl.App.GUI;
using Kistl.Client;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.IntegrationTests
{
    [SetUpFixture]
    public class SetUp : Kistl.API.AbstractConsumerTests.DatabaseResetup, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.Integration.SetUp");

        private ServerDomainManager manager;

        [SetUp]
        public void Init()
        {
            using (Log.InfoTraceMethodCall("Starting up"))
            {
                try
                {
                    var config = KistlConfig.FromFile("Kistl.IntegrationTests.Config.xml");

                    ResetDatabase(config);

                    manager = new ServerDomainManager();
                    manager.Start(config);

                    AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
                    var testCtx = new GuiApplicationContext(config, "WPF");

                }
                catch (Exception error)
                {
                    Log.ErrorFormat(String.Format("Error while initialising Integration Tests: [{0}]", error.GetType().Name), error);
                    throw error;
                }
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
                        manager.DisableUnloadAppDomainOnShutdown();
                        manager.Stop();
                        manager = null;
                        Log.Info("Shutting down Kistl finished");
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
