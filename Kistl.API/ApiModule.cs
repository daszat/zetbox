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

            moduleBuilder
                .RegisterType<InterfaceType>()
                .InstancePerDependency();
        }
    }
}
