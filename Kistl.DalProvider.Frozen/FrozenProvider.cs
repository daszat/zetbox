
namespace Kistl.DalProvider.Frozen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac.Builder;
    using Kistl.API;
    using Kistl.Server;
    using Kistl.Server.Generators;
    using Kistl.API.Utils;
    using Kistl.API.Configuration;

    public class FrozenProvider 
        : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // register the frozen context only when it's available
            // TODO: also check the version?
            var frozenContextType = Type.GetType("Kistl.Objects.Frozen.FrozenContextImplementation, Kistl.Objects.Frozen", false);
            if (frozenContextType != null)
            {
                moduleBuilder
                    .Register(frozenContextType)
                    .As<IReadOnlyKistlContext>()
                    .OnActivating((sender, args) => {
                        Logging.Log.Error("Initialising FrozenActionsManagerServer");
                        var fams = new FrozenActionsManagerServer();
                        fams.Init((IReadOnlyKistlContext)args.Instance);
                    })
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
