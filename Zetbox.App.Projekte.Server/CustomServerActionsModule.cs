
namespace Zetbox.App.Projekte.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Server;

    public class CustomServerActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZetboxImplementors(typeof(CustomServerActionsModule).Assembly);

            // Register explicit overrides here
        }
    }
}
