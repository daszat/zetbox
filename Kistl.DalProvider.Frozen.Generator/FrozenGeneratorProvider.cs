
namespace Kistl.DalProvider.Frozen.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Kistl.API;
    using Kistl.Server;
    using Kistl.Server.Generators;
    using Kistl.API.Utils;
    using Kistl.API.Configuration;
    using Kistl.App.Extensions;

    public class FrozenGeneratorProvider 
        : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<FreezingGenerator>()
                .As<BaseDataObjectGenerator>()
                .SingleInstance();
        }
    }
}
