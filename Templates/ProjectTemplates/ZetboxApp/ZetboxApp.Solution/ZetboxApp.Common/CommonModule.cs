
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

            // Register explicit overrides here
        }
    }
}
