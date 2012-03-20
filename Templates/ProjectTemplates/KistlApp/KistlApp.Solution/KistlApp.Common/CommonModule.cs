
namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    $if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
    $endif$using System.Text;
    using Autofac;
    using Kistl.API;

	public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZBoxImplementors(typeof(CommonModule).Assembly);

            // Register explicit overrides here
        }
    }
}
