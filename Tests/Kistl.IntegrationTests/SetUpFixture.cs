
namespace Kistl.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.AbstractConsumerTests;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    [SetUpFixture]
    public class SetUpFixture : AbstractSetUpFixture, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.Integration.SetUp");

        private ServerDomainManager manager;

        protected override void SetUp(IContainer container)
        {
            using (Log.InfoTraceMethodCall("Starting up"))
            {
                try
                {
                    base.SetUp(container);
                    ResetDatabase(container);

                    var config = container.Resolve<KistlConfig>();

                    manager = new ServerDomainManager();
                    manager.Start(config);

                    using (var initCtx = container.Resolve<IKistlContext>())
                    {
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

        public override void TearDown()
        {
            lock (typeof(SetUpFixture))
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

        protected override string GetConfigFile()
        {
            return "Kistl.IntegrationTests.Config.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Client;
        }
    }
}
