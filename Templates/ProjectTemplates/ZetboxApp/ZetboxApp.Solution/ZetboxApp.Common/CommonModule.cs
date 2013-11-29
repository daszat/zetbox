
namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using System.ComponentModel;

    // No feature, implicit loaded
    [Description("$safeprojectname$ common module")]
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZetboxImplementors(typeof(CommonModule).Assembly);

            // Register additional module dependencies, for example workflow and parties
            // moduleBuilder.RegisterModule<Zetbox.Workflow.Common.CommonModule>();
            // moduleBuilder.RegisterModule<Zetbox.Parties.Common.CommonModule>();

            // Register explicit overrides here
        }
    }
}
