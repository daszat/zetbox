using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Reflection;

namespace Kistl.API.Client
{
    public sealed class ClientApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterType<KistlContextImpl>()
                .As<IKistlContext>()
                .As<IReadOnlyKistlContext>()
                .InstancePerDependency();
        }
    }
}
