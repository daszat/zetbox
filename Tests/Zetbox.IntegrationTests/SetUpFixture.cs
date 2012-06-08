// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    [SetUpFixture]
    public class SetUpFixture : AbstractSetUpFixture, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Tests.Integration.SetUp");

        private ServerDomainManager manager;

        protected override void SetUp(IContainer container)
        {
            try
            {
                base.SetUp(container);
                ResetDatabase(container);

                var config = container.Resolve<ZetboxConfig>();

                manager = new ServerDomainManager();
                manager.Start(config);

                using (var initCtx = container.Resolve<IZetboxContext>())
                {
                    // load up all infrastructure from the DalProvider
                    // TODO: remove ToList() call!
                    Console.WriteLine(initCtx.GetQuery<Zetbox.App.Base.ObjectClass>().ToList().Count());
                }
            }
            catch (Exception error)
            {
                Log.Error("Error while initialising Integration Tests", error);
                DisposeManager();
                throw;
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
                    Log.Info("Shutting down Zetbox finished");
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
            return "Zetbox.IntegrationTests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Client;
        }
    }
}
