using System;
using System.Collections.Generic;
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
    public class SetUpFixture : Kistl.API.AbstractConsumerTests.DatabaseResetup, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Tests.Server.SetUp");

        private IKistlAppDomain manager;

        protected override void SetupBuilder(ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.RegisterModule(new Kistl.API.ApiModule());
            builder.RegisterModule(new Kistl.API.Server.ServerApiModule());
            builder.RegisterModule(new Kistl.Server.ServerModule());
            builder.RegisterModule(new Kistl.DalProvider.EF.EfProvider());
            builder.RegisterModule(new Kistl.DalProvider.Memory.MemoryProvider());
            builder.RegisterModule(new Kistl.Objects.EFModule());
            builder.RegisterModule(new Kistl.Objects.MemoryModule());
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);
            using (Log.InfoTraceMethodCall("Starting up"))
            {
                AutofacServiceHostFactory.Container = container;

                var config = container.Resolve<KistlConfig>();
                ResetDatabase(config);

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
            return "Kistl.Server.Tests.Config.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }
    }
}
