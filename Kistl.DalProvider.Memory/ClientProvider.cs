
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Extensions;

    public class ClientProvider
        : Module
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.DalProvider.Memory");

        public static readonly string ContextClassName = "Kistl.Objects.Memory.MemoryContext";
        public static readonly string GeneratedAssemblyName = "Kistl.Objects.Memory, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            var memoryContextType = typeof(MemoryContext); // Type.GetType(ContextClassName + ", " + GeneratedAssemblyName, true);
            moduleBuilder
                .RegisterType(memoryContextType)
                .As<BaseMemoryContext>()
                .OnActivating(args =>
                {
                    //// TODO: check validity of using FAMS here.
                    //Logging.Log.Info("Initialising FrozenActionsManagerClient");
                    //var fams = args.Context.Resolve<FrozenActionsManager>();
                    //fams.Init((IReadOnlyKistlContext)args.Instance);
                })
                .SingleInstance();
        }
    }
}
