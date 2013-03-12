
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

    [Feature(NotOnFallback = true)]
    [Description("$safeprojectname$ WPF module")]
    public class ClientWPFModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // Register explicit overrides here

        }
    }
}
