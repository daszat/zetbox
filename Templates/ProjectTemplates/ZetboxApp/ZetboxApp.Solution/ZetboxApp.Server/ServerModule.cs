
namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.API.Configuration;
    using System.ComponentModel;

    [Feature(NotOnFallback=true)]
    [Description("$safeprojectname$ server module")]
    public class ServerModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterModule<CommonModule>();

            moduleBuilder.RegisterZetboxImplementors(typeof(ServerModule).Assembly);

            // Register explicit overrides here
        }
    }
}
