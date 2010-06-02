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
                .RegisterType<InterfaceType>()
                .InstancePerDependency();
            moduleBuilder
                .RegisterType<ImplementationType>()
                .InstancePerDependency();

            moduleBuilder
                .Register(c => new TypeTransformations(c.Resolve<InterfaceType.Factory>(), c.Resolve<InterfaceType.NameFactory>(), c.Resolve<ImplementationType.Factory>(), c.Resolve<IAssemblyConfiguration>()))
                .As<ITypeTransformations>();

            moduleBuilder
                .Register(c => FrozenContext.Single)
                .Named<IReadOnlyKistlContext>(Kistl.API.Helper.MetaContextServiceName)
                .SingleInstance()
                .ExternallyOwned();

            moduleBuilder
                .Register(c => FrozenContext.Single)
                .Named<IReadOnlyKistlContext>(Kistl.API.Helper.FrozenContextServiceName)
                .SingleInstance()
                .ExternallyOwned();
        }
    }
}
