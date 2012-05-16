
namespace Kistl.App.Projekte.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.Client;

    public class CustomClientActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZBoxImplementors(typeof(CustomClientActionsModule).Assembly);
            moduleBuilder.RegisterViewModels(typeof(CustomClientActionsModule).Assembly);

            // Register explicit overrides here
            moduleBuilder
                .Register<Kistl.App.Projekte.Client.Projekte.Reporting.ReportingHost>(c => new Kistl.App.Projekte.Client.Projekte.Reporting.ReportingHost(
                        c.Resolve<IFileOpener>()
                    )
                )
                .InstancePerDependency();
        }
    }
}
