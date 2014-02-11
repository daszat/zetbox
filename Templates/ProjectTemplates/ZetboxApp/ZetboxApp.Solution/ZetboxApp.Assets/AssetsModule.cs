
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
    [Description("$safeprojectname$ assets module")]
    public class AssetsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZetboxImplementors(typeof(AssetsModule).Assembly);
        }
    }
}
