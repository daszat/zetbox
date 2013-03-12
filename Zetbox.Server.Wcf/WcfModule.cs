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

namespace Zetbox.Server.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Text;
    using Autofac;
    using Autofac.Integration.Wcf;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using System.ComponentModel;

    [Feature]
    [Description("Providing WCF infrastructure, either for selfhosting or IIS integration. For selfhosting start a WcfServer instance.")]
    public class WcfModule : Module
    {
        public static object NoWcfKey { get { return "nowcf"; } }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<WcfAppDomainInitializer>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<WcfServer>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterCmdLineFlag("nowcf", "Do not run the embedded WCF Server; running it is the default when no action parameter is specified", NoWcfKey);

            builder
                .Register(c => new AutofacServiceHostFactory())
                .As<AutofacServiceHostFactory>()
                .SingleInstance();

            builder
                .Register(c => new AutofacWebServiceHostFactory())
                .As<AutofacWebServiceHostFactory>()
                .SingleInstance();

        }
    }
}
