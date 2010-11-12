
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
        public static readonly string GeneratedAssemblyName = "Kistl.Objects.MemoryImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<MemoryImplementationType>()
                .As<MemoryImplementationType>()
                .As<ImplementationType>()
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<MemoryContext>()
                .As<BaseMemoryContext>()
                .OnActivated(args =>
                {
                    var manager = args.Context.Resolve<IMemoryActionsManager>();
                    manager.Init(args.Context.Resolve<IFrozenContext>());
                })
                .InstancePerDependency();

            try
            {
                // TODO: Move to MemoryModule
                var generatedAssembly = Assembly.Load(GeneratedAssemblyName);
                moduleBuilder
                    .Register(c =>
                    {
                        FrozenMemoryContext memCtx = null;
                        memCtx = new FrozenMemoryContext(
                            c.Resolve<InterfaceType.Factory>(),
                            () => memCtx,
                            c.Resolve<MemoryImplementationType.MemoryFactory>());
                        Importer.LoadFromXml(memCtx, generatedAssembly.GetManifestResourceStream("Kistl.Objects.MemoryImpl.FrozenObjects.xml"));
                        memCtx.Seal();
                        return memCtx;
                    })
                    .As<IFrozenContext>()
                    .OnActivated(args =>
                    {
                        var manager = args.Context.Resolve<IMemoryActionsManager>();
                        manager.Init(args.Context.Resolve<IFrozenContext>());
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
