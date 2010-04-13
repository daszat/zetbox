
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.Server;
    using Kistl.Server.Generators;

    public class ServerProvider
        : Module
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.DalProvider.Memory");

        public static readonly string ContextClassName = "Kistl.Objects.Memory.MemoryContext";
        public static readonly string GeneratedAssemblyName = "Kistl.Objects.Memory, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // register the memory context only if available. for example, while bootstrapping it might not be available
            var memoryContextType = typeof(MemoryContext); //Type.GetType(ContextClassName + ", " + GeneratedAssemblyName, false);
            if (memoryContextType != null)
            {
                moduleBuilder
                    .RegisterType(memoryContextType)
                    .As<BaseMemoryContext>()
                    .OnActivating(args =>
                    {
                        // TODO: check validity of using FAMS here.
                        Logging.Log.Info("Initialising FrozenActionsManagerServer");
                        var fams = new FrozenActionsManagerServer();
                        fams.Init((IReadOnlyKistlContext)args.Instance);
                    })
                    .SingleInstance();
            }
            else
            {
                Log.Warn("Cannot load memory context");
            }

            moduleBuilder
                .RegisterType<Generator.MemoryGenerator>()
                .As<BaseDataObjectGenerator>()
                .SingleInstance();
        }
    }
}
