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

            // Register all non static ActionClasses
            foreach (var t in typeof(CustomCommonActionsModule).Assembly.GetTypes().Where(t => !t.IsStatic()))
            {
                moduleBuilder.RegisterType(t)
                    .SingleInstance();
            }

            // Register explicit overrides here
        }
    }
}
