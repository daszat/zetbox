using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Kistl.API;

namespace Kistl.App.Projekte.Client
{
    public class CustomClientActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZBoxImplementors(typeof(CustomClientActionsModule).Assembly);

            // Register explicit overrides here
        }
    }
}
