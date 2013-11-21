
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

            moduleBuilder.RegisterModule<Common.CommonModule>();

            // Register additional module dependencies, for example workflow and parties
            // moduleBuilder.RegisterModule<Zetbox.Workflow.Server.ServerModule>();
            // moduleBuilder.RegisterModule<Zetbox.Parties.Server.ServerModule>();

            moduleBuilder.RegisterZetboxImplementors(typeof(ServerModule).Assembly);

            // Register explicit overrides here
        }
    }
}
