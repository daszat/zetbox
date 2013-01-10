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

namespace Zetbox.App.Projekte.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.Client;
    using Zetbox.API.Configuration;

    [Feature]
    public class CustomClientActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterModule<Zetbox.App.Projekte.Common.CustomCommonActionsModule>();

            moduleBuilder.RegisterZetboxImplementors(typeof(CustomClientActionsModule).Assembly);
            moduleBuilder.RegisterViewModels(typeof(CustomClientActionsModule).Assembly);

            // Register explicit overrides here
            moduleBuilder
                .Register<Zetbox.App.Projekte.Client.Projekte.Reporting.ReportingHost>(c => new Zetbox.App.Projekte.Client.Projekte.Reporting.ReportingHost(
                        "Zetbox.App.Projekte.Client.DerivedReportTest",
                        typeof(CustomClientActionsModule).Assembly,
                        c.Resolve<IFileOpener>(),
                        c.Resolve<ITempFileService>()
                    )
                )
                .InstancePerDependency();
        }
    }
}
