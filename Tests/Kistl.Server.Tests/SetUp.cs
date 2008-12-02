using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Configuration;
using Kistl.App.GUI;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;

namespace Kistl.Server.Tests
{
    [SetUpFixture]
    public class SetUp : IDisposable
    {
        private Server manager;

        [SetUp]
        public void Init()
        {
            System.Diagnostics.Trace.WriteLine("Setting up Kistl");

            var config = KistlConfig.FromFile("DefaultConfig_Server.Tests.xml");

            manager = new Server();
            manager.Start(config);

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
