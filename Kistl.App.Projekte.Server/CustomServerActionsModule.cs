
namespace Kistl.App.Projekte.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Server;

    public class CustomServerActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // Register all non static ActionClasses
            moduleBuilder
                .RegisterAssemblyTypes(this.GetType().Assembly)
                .AsSelf()
                .SingleInstance();

            // Register all non static ActionClasses
            foreach (var t in typeof(CustomServerActionsModule).Assembly.GetTypes())
            {
                if (!t.IsStatic())
                {
                    moduleBuilder.RegisterType(t)
                        .SingleInstance();
                }
            }
        }
    }
}
