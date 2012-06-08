
namespace Zetbox.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Autofac.Core;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.API.Server.PerfCounter;

    public interface IEfActionsManager : ICustomActionsManager { }

    public class EfProvider
        : Autofac.Module
    {
        internal static readonly string ServerAssembly = "Zetbox.Objects.EfImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";

        private static readonly object _lock = new object();

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // As we are using the concrete Assembly baseDir in our connection string, this should not be needed anymore
            //var serverAssembly = Assembly.Load(ServerAssembly);
            //if (serverAssembly == null)
            //    throw new InvalidOperationException("Unable to load Zetbox.Objects.EfImpl Assembly, no Entity Framework Metadata will be loaded");

            //// force-load a few assemblies to the reflection-only context so the DAL provider can find them
            //// this uses the AssemblyLoader directly because Assembly.ReflectionOnlyLoad doesn't go through all 
            //// the moves of resolving AssemblyNames to files. See http://stackoverflow.com/questions/570117/
            //var reflectedInterfaceAssembly = AssemblyLoader.ReflectionOnlyLoadFrom(Zetbox.API.Helper.InterfaceAssembly);
            //if (reflectedInterfaceAssembly == null)
            //    throw new InvalidOperationException("Unable to load Zetbox.Objects Assembly for reflection, no Entity Framework Metadata will be loaded");
            //var reflectedServerAssembly = AssemblyLoader.ReflectionOnlyLoadFrom(EfProvider.ServerAssembly);
            //if (reflectedServerAssembly == null)
            //    throw new InvalidOperationException("Unable to load Zetbox.Objects.EfImpl Assembly for reflection, no Entity Framework Metadata will be loaded");

            moduleBuilder
                .Register(c =>
                {
                    // EF's meta data initialization is not thread-safe
                    lock (_lock)
                    {
                        return new EfDataContext(
                            c.Resolve<IMetaDataResolver>(),
                            null,
                            c.Resolve<ZetboxConfig>(),
                            c.Resolve<Func<IFrozenContext>>(),
                            c.Resolve<InterfaceType.Factory>(),
                            c.Resolve<EfImplementationType.EfFactory>(),
                            c.Resolve<IPerfCounter>()
                            );
                    }
                })
                .As<IZetboxServerContext>()
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
                            param != null ? (Zetbox.App.Base.Identity)param.Value : c.Resolve<IIdentityResolver>().GetCurrent(),
                            c.Resolve<ZetboxConfig>(),
                            c.Resolve<Func<IFrozenContext>>(),
                            c.Resolve<InterfaceType.Factory>(),
                            c.Resolve<EfImplementationType.EfFactory>(),
                            c.Resolve<IPerfCounter>()
                            );
                    }
                })
                .As<IZetboxContext>()
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
                            c.Resolve<ZetboxConfig>(),
                            c.Resolve<Func<IFrozenContext>>(),
                            c.Resolve<InterfaceType.Factory>(),
                            c.Resolve<EfImplementationType.EfFactory>(),
                            c.Resolve<IPerfCounter>()
                            );
                        return result;
                    }
                })
                .As<IReadOnlyZetboxContext>()
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
