using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.API.Configuration;
using Kistl.API.Utils;
using Kistl.App.GUI;
using Kistl.Client;

using Autofac;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Kistl.App.Extensions;

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
                    config.Server.DocumentStore = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Server");
                    config.Client.DocumentStore = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Client");

                    ResetDatabase(config);

                    manager = new ServerDomainManager();
                    manager.Start(config);

                    AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
                    var builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);
                    var container = builder.Build();
                    // var testCtx = new GuiApplicationContext(config, "WPF");

                    // initialise custom actions manager
                    var cams = container.Resolve<BaseCustomActionsManager>(); 

                    using (var initCtx = Kistl.API.Client.KistlContext.GetContext()) {
                    	// load up all infrastructure from the DalProvider
                    	// TODO: remove ToList() call!
                    	Console.WriteLine(initCtx.GetQuery<Kistl.App.Base.ObjectClass>().ToList().Count());
                    }
                }
                catch (Exception error)
                {
                    Log.Error("Error while initialising Integration Tests", error);
                    DisposeManager();
                    throw;
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            lock (typeof(SetUp))
            {
                DisposeManager();
            }
        }

        private void DisposeManager()
        {
            if (manager != null)
            {
                using (Log.InfoTraceMethodCall("Shutting down"))
                {
                    manager.Stop();
                    manager = null;
                    Log.Info("Shutting down Kistl finished");
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
