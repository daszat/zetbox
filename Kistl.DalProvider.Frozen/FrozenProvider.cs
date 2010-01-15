
namespace Kistl.DalProvider.Frozen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac.Builder;
    using Kistl.API;
    using Kistl.Server.Generators;

    public class FrozenProvider 
        : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // register the frozen context only when it's available
            // TODO: also check the version?
            var frozenContext = Type.GetType("Kistl.Objects.Frozen.FrozenContextImplementation, Kistl.Objects.Frozen", false);
            if (frozenContext != null)
            {
                moduleBuilder
                    .Register(frozenContext)
                    .As<IReadOnlyKistlContext>()
                    .SingletonScoped();
            }

            moduleBuilder
                .Register<Generator.FreezingGenerator>()
                .As<BaseDataObjectGenerator>()
                .MemberOf<IEnumerable<BaseDataObjectGenerator>>()
                .SingletonScoped();
        }
    }
}
