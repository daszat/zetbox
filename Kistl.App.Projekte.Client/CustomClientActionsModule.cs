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

            // Register all non static ActionClasses
            foreach (var t in typeof(CustomClientActionsModule).Assembly.GetTypes())
            {
                if (!t.IsStatic())
                {
                    moduleBuilder.RegisterType(t)
                        .SingleInstance();
                }
            }

            // Register types explicit
        }
    }
}
