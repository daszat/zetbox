using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Kistl.API;

namespace Kistl.App.Projekte.Common
{
    public class CustomCommonActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZBoxImplementors(typeof(CustomCommonActionsModule).Assembly);

            // Register explicit overrides here
        }
    }
}
