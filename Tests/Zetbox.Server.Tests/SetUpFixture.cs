
namespace Zetbox.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Autofac.Integration.Wcf;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.GUI;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    [SetUpFixture]
    public class SetUpFixture : AbstractSetUpFixture, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Tests.Server.SetUp");

        private IZetboxAppDomain manager;

        protected override void SetupBuilder(ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.RegisterModule(new Zetbox.API.ApiModule());
            builder.RegisterModule(new Zetbox.API.Common.ApiCommonModule());
            builder.RegisterModule(new Zetbox.API.Server.ServerApiModule());
            builder.RegisterModule(new Zetbox.Server.ServerModule());
            builder.RegisterModule(new Zetbox.DalProvider.Memory.MemoryProvider());
            builder.RegisterModule(new Zetbox.Objects.MemoryModule());

            // load DB Utility from config
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);
            ResetDatabase(container);

            AutofacServiceHostFactory.Container = container;

            var config = container.Resolve<ZetboxConfig>();

            using (Log.InfoTraceMethodCall("Starting server domain"))
            {
                manager = container.Resolve<IZetboxAppDomain>();
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
            return "Zetbox.Server.Tests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }
    }
}
