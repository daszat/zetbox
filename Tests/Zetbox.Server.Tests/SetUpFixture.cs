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
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.GUI;

    [SetUpFixture]
    public class SetUpFixture : AbstractSetUpFixture, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Tests.Server.SetUp");

        protected override void SetupBuilder(ContainerBuilder builder)
        {
            builder.RegisterModule(new Zetbox.Server.ServerModule());

            // load overrides after loading the default modules
            base.SetupBuilder(builder);
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);

            AutofacServiceHostFactory.Container = container;

            var config = container.Resolve<ZetboxConfig>();
        }

        public override void TearDown()
        {
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
