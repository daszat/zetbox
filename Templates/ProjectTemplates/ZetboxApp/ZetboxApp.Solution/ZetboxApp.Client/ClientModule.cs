
namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.Client;
    using Zetbox.API.Configuration;
    using System.ComponentModel;

    [Feature(NotOnFallback=true)]
    [Description("$safeprojectname$ client module")]
    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterModule<Common.CommonModule>();

            moduleBuilder.RegisterZetboxImplementors(typeof(ClientModule).Assembly);
            moduleBuilder.RegisterViewModels(typeof(ClientModule).Assembly);

            // Register explicit overrides here
            
        }
    }
}
