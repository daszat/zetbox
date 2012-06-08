
namespace Zetbox.App.Projekte.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.Client;

    public class CustomClientActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

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
