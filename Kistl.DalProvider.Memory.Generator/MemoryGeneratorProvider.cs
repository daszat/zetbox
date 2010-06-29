
namespace Kistl.DalProvider.Memory.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.Server.Generators;

    public class MemoryGeneratorProvider
        : Autofac.Module
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.DalProvider.Memory.Generator");

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<MemoryGenerator>()
                .As<BaseDataObjectGenerator>()
                .SingleInstance();
        }
    }
}
