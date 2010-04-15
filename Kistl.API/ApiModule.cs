using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Reflection;

namespace Kistl.API
{
    public sealed class ApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // InterfaceAssembly, should be provided by the generator
            moduleBuilder
                .RegisterInstance<Assembly>(Assembly.Load(Kistl.API.Helper.InterfaceAssembly))
                .As<Assembly>()
                .Named<Assembly>("InterfaceAssembly");

            moduleBuilder
                .RegisterInstance<Assembly>(this.GetType().Assembly)
                .As<Assembly>();

            moduleBuilder
                .Register<IInterfaceTypeFilter>(c => new DefaultInterfaceTypeFilter(c.Resolve<Assembly>("InterfaceAssembly")))
                .SingleInstance();
        }
    }
}
