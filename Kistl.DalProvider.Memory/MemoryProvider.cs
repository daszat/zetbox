
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Autofac;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Extensions;
    using Kistl.App.Packaging;
    using System.IO;

    public class MemoryProvider
        : Autofac.Module
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.DalProvider.Memory");

        public static readonly string ContextClassName = "Kistl.Objects.Memory.MemoryContext";
        public static readonly string GeneratedAssemblyName = "Kistl.Objects.Memory, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<MemoryContext>()
                .As<BaseMemoryContext>()
                .OnActivating(args =>
                {
                    //// TODO: check validity of using FAMS here.
                    //Logging.Log.Info("Initialising FrozenActionsManagerClient");
                    //var fams = args.Context.Resolve<FrozenActionsManager>();
                    //fams.Init((IReadOnlyKistlContext)args.Instance);
                })
                .InstancePerDependency();

            try
            {
                var generatedAssembly = Assembly.Load(GeneratedAssemblyName);
                moduleBuilder
                    .Register(c =>
                    {
                        MemoryContext memCtx = null;
                        memCtx = new MemoryContext(c.Resolve<ITypeTransformations>(), () => memCtx);
                        // register empty context first, to avoid errors when trying to load defaultvalues
                        // TODO: remove, this should not be needed when using the container.
                        FrozenContext.RegisterFallback(memCtx);
                        Importer.LoadFromXml(memCtx, generatedAssembly.GetManifestResourceStream("Kistl.Objects.Memory.FrozenObjects.xml"));
                        return memCtx;
                    })
                    .Named<IReadOnlyKistlContext>(Kistl.API.Helper.FrozenContextServiceName)
                    .SingleInstance();
            }
            catch (FileNotFoundException ex)
            {
                Log.Warn("Could not load memory context", ex);
            }
        }
    }
}
