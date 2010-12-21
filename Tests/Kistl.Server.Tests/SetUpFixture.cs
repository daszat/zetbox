
namespace Kistl.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Autofac.Integration.Wcf;
    using Kistl.API;
    using Kistl.API.AbstractConsumerTests;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.App.GUI;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    [SetUpFixture]
    public class SetUpFixture : AbstractSetUpFixture, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.Server.SetUp");

        private IKistlAppDomain manager;

        protected override void SetupBuilder(ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.RegisterModule(new Kistl.API.ApiModule());
            builder.RegisterModule(new Kistl.API.Server.ServerApiModule());
            builder.RegisterModule(new Kistl.Server.ServerModule());
            builder.RegisterModule(new Kistl.DalProvider.Ef.EfProvider());
            builder.RegisterModule(new Kistl.DalProvider.Memory.MemoryProvider());
            builder.RegisterModule(new Kistl.Objects.EfModule());
            builder.RegisterModule(new Kistl.Objects.MemoryModule());

            // load DB Utility from config
        }

        protected override void SetUp(IContainer container)
        {
            using (Log.InfoTraceMethodCall("Starting up"))
            {
                base.SetUp(container);
                ResetDatabase(container);

                AutofacServiceHostFactory.Container = container;

                var config = container.Resolve<KistlConfig>();

                manager = container.Resolve<IKistlAppDomain>();
                manager.Start(config);
            }
        }

        public override void TearDown()
        {
            lock (typeof(SetUpFixture))
            {
                if (manager != null)
                {
                    using (Log.InfoTraceMethodCall("Shutting down"))
                    {
                        manager.Stop();
                        manager = null;
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

        protected override string GetConfigFile()
        {
            return "Kistl.Server.Tests.Config{0}.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }
    }
}
