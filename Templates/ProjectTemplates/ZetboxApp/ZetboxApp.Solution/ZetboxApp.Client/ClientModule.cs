
namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    $if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
    $endif$using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.Client;

    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterModule<CommonModule>();

            moduleBuilder.RegisterZetboxImplementors(typeof(ClientModule).Assembly);
            moduleBuilder.RegisterViewModels(typeof(ClientModule).Assembly);

            // Register explicit overrides here
            
        }
    }
}
