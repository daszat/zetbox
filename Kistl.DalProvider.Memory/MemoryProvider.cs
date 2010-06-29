
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Extensions;
    using Kistl.App.Packaging;

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
                .RegisterType<MemoryImplementationType>()
                .As<MemoryImplementationType>()
                .As<ImplementationType>()
                .InstancePerDependency();

            moduleBuilder
                .Register<MemoryContext>(c => new MemoryContext(
                    c.Resolve<InterfaceType.Factory>(),
                    c.Resolve<Func<IReadOnlyKistlContext>>(Kistl.API.Helper.FrozenContextServiceName),
                    c.Resolve<MemoryImplementationType.MemoryFactory>()
                    ))
                .As<BaseMemoryContext>()
                .OnActivated(args =>
                {
                    var manager = args.Context.Resolve<IMemoryActionsManager>();
                    manager.Init(args.Context.Resolve<IReadOnlyKistlContext>(Kistl.API.Helper.FrozenContextServiceName));
                })
                .InstancePerDependency();

            try
            {
                // TODO: Move to MemoryModule
                var generatedAssembly = Assembly.Load(GeneratedAssemblyName);
                moduleBuilder
                    .Register(c =>
                    {
                        MemoryContext memCtx = null;
                        memCtx = new MemoryContext(
                            c.Resolve<InterfaceType.Factory>(),
                            () => memCtx,
                            c.Resolve<MemoryImplementationType.MemoryFactory>());
                        Importer.LoadFromXml(memCtx, generatedAssembly.GetManifestResourceStream("Kistl.Objects.Memory.FrozenObjects.xml"));
                        return memCtx;
                    })
                    .Named<IReadOnlyKistlContext>(Kistl.API.Helper.FrozenContextServiceName)
                    .OnActivated(args =>
                    {
                        var manager = args.Context.Resolve<IMemoryActionsManager>();
                        manager.Init(args.Context.Resolve<IReadOnlyKistlContext>(Kistl.API.Helper.FrozenContextServiceName));
                    })
                    .SingleInstance();
            }
            catch (FileNotFoundException ex)
            {
                Log.Warn("Could not load memory context", ex);
            }
            catch (Exception ex)
            {
                Log.Error("Error while loading memory context", ex);
                throw;
            }
        }
    }
}
