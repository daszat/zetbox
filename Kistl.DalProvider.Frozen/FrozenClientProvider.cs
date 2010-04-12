
namespace Kistl.DalProvider.Frozen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.API.Configuration;
    using Kistl.App.Extensions;

    public class FrozenClientProvider 
        : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // register the frozen context only when it's available
            // TODO: also check the version?
            var frozenContextType = Type.GetType("Kistl.Objects.Frozen.FrozenContextImplementation, " + Kistl.API.Helper.FrozenAssembly, false);
            if (frozenContextType != null)
            {
                moduleBuilder
                    .RegisterType(frozenContextType)
                    .As<IReadOnlyKistlContext>()
                    .OnActivating(frozen => {
                        Logging.Log.Info("Initialising FrozenActionsManagerClient");
                        var fams = frozen.Context.Resolve<FrozenActionsManager>();
                        fams.Init((IReadOnlyKistlContext)frozen.Instance);
                    })
                    .SingleInstance();
            }
        }
    }
}
