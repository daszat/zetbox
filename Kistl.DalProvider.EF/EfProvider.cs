
namespace Kistl.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Autofac.Core;
    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;

    public interface IEfActionsManager : ICustomActionsManager { }

    public class EfProvider
        : Autofac.Module
    {
        internal static readonly string ServerAssembly = "Kistl.Objects.EfImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";

        private static readonly object _lock = new object();

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            var serverAssembly = Assembly.Load(ServerAssembly);
            if (serverAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects.EfImpl Assembly, no Entity Framework Metadata will be loaded");

            // force-load a few assemblies to the reflection-only context so the DAL provider can find them
            // this uses the AssemblyLoader directly because Assembly.ReflectionOnlyLoad doesn't go through all 
            // the moves of resolving AssemblyNames to files. See http://stackoverflow.com/questions/570117/
            var reflectedInterfaceAssembly = AssemblyLoader.ReflectionOnlyLoadFrom(Kistl.API.Helper.InterfaceAssembly);
            if (reflectedInterfaceAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects Assembly for reflection, no Entity Framework Metadata will be loaded");
            var reflectedServerAssembly = AssemblyLoader.ReflectionOnlyLoadFrom(EfProvider.ServerAssembly);
            if (reflectedServerAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects.EfImpl Assembly for reflection, no Entity Framework Metadata will be loaded");

            moduleBuilder
                .Register(c =>
                {
                    // EF's meta data initialization is not thread-safe
                    lock (_lock)
                    {
                        return new EfDataContext(
                            c.Resolve<IMetaDataResolver>(),
                            null,
                            c.Resolve<KistlConfig>(),
                            c.Resolve<Func<IFrozenContext>>(),
                            c.Resolve<InterfaceType.Factory>(),
                            c.Resolve<EfImplementationType.EfFactory>()
                            );
                    }
                })
                .As<IKistlServerContext>()
                .OnActivated(args =>                              
                {
                    var manager = args.Context.Resolve<IEfActionsManager>();
                    manager.Init(args.Context.Resolve<IFrozenContext>());
                })
                .InstancePerDependency();

            moduleBuilder
                .Register((c, p) =>
                {
                    // EF's meta data initialization is not thread-safe
                    lock (_lock)
                    {
                        var param = p.OfType<ConstantParameter>().FirstOrDefault();
                        return new EfDataContext(
                            c.Resolve<IMetaDataResolver>(),
                            param != null ? (Kistl.App.Base.Identity)param.Value : c.Resolve<IIdentityResolver>().GetCurrent(),
                            c.Resolve<KistlConfig>(),
                            c.Resolve<Func<IFrozenContext>>(),
                            c.Resolve<InterfaceType.Factory>(),
                            c.Resolve<EfImplementationType.EfFactory>()
                            );
                    }
                })
                .As<IKistlContext>()
                .OnActivated(args =>
                {
                    var manager = args.Context.Resolve<IEfActionsManager>();
                    manager.Init(args.Context.Resolve<IFrozenContext>());
                })
                .InstancePerDependency();

            moduleBuilder
                .Register(c =>
                {
                    // EF's meta data initialization is not thread-safe
                    lock (_lock)
                    {
                        var result = new EfDataContext(
                            c.Resolve<CachingMetaDataResolver>(),
                            null,
                            c.Resolve<KistlConfig>(),
                            c.Resolve<Func<IFrozenContext>>(),
                            c.Resolve<InterfaceType.Factory>(),
                            c.Resolve<EfImplementationType.EfFactory>()
                            );
                        return result;
                    }
                })
                .As<IReadOnlyKistlContext>()
                .OnActivated(args =>
                {
                    var manager = args.Context.Resolve<IEfActionsManager>();
                    manager.Init(args.Context.Resolve<IFrozenContext>());
                })
                .InstancePerDependency();

            moduleBuilder
                .Register(c => new EfServerObjectHandlerFactory())
                .As(typeof(IServerObjectHandlerFactory));

            moduleBuilder.RegisterType<EfImplementationType>();
        }
    }
}
