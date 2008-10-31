using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Configuration;
using Kistl.App.GUI;
using Kistl.Client;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.IntegrationTests
{
    [SetUpFixture]
    public class SetUp : IDisposable
    {
        private ServerDomainManager manager;

        [SetUp]
        public void Init()
        {
            System.Diagnostics.Trace.WriteLine("Setting up Kistl");

            var config = KistlConfig.FromFile("DefaultConfig_Integration.Tests.xml");

            manager = new ServerDomainManager();
            manager.Start(config);

            var testCtx = new GuiApplicationContext(config, Toolkit.WPF);

            System.Diagnostics.Trace.WriteLine("Setting up Kistl finished");
        }

        [TearDown]
        public void TearDown()
        {
            System.Diagnostics.Trace.WriteLine("Shutting down Kistl");
            manager.Stop();
            System.Diagnostics.Trace.WriteLine("Shutting down Kistl finished");
        }

        #region IDisposable Members

        public void Dispose()
        {
            TearDown();
        }

        #endregion
    }
}
