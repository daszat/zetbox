
namespace Kistl.DalProvider.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Autofac;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.Server.Generators;
    using Autofac.Core;

    public class EfProvider
        : Autofac.Module
    {
        private static readonly object _lock = new object();

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            var interfaceAssembly = Assembly.Load(Kistl.API.Helper.InterfaceAssembly);
            if (interfaceAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects Assembly, no Entity Framework Metadata will be loaded");
            var serverAssembly = Assembly.Load(Kistl.API.Helper.ServerAssembly);
            if (serverAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects.Server Assembly, no Entity Framework Metadata will be loaded");

            // force-load a few assemblies to the reflection-only context so the DAL provider can find them
            // this uses the AssemblyLoader directly because Assembly.ReflectionOnlyLoad doesn't go through all 
            // the moves of resolving AssemblyNames to files. See http://stackoverflow.com/questions/570117/
            var reflectedInterfaceAssembly = AssemblyLoader.ReflectionOnlyLoadFrom(Kistl.API.Helper.InterfaceAssembly);
            if (reflectedInterfaceAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects Assembly for reflection, no Entity Framework Metadata will be loaded");
            var reflectedServerAssembly = AssemblyLoader.ReflectionOnlyLoadFrom(Kistl.API.Helper.ServerAssembly);
            if (reflectedServerAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects.Server Assembly for reflection, no Entity Framework Metadata will be loaded");

            moduleBuilder
                .Register(c =>
                {
                    // EF's meta data initialization is not thread-safe
                    lock (_lock)
                    {
                        return new KistlDataContext(
                            c.Resolve<IMetaDataResolver>(),
                            null,
                            c.Resolve<KistlConfig>(),
                            c.Resolve<ITypeTransformations>(),
                            c.Resolve<Func<IReadOnlyKistlContext>>(Kistl.API.Helper.FrozenContextServiceName)
                            );
                    }
                })
                .As<IKistlServerContext>()
                .InstancePerDependency();

            moduleBuilder
                .Register((c, p) =>
                {
                    // EF's meta data initialization is not thread-safe
                    lock (_lock)
                    {
                        var param = p.OfType<ConstantParameter>().FirstOrDefault();
                        return new KistlDataContext(
                            c.Resolve<IMetaDataResolver>(),
                            param != null ? (Kistl.App.Base.Identity)param.Value : c.Resolve<IIdentityResolver>().GetCurrent(),
                            c.Resolve<KistlConfig>(),
                            c.Resolve<ITypeTransformations>(),
                            c.Resolve<Func<IReadOnlyKistlContext>>(Kistl.API.Helper.FrozenContextServiceName)
                            );
                    }
                })
                .As<IKistlContext>()
                .InstancePerDependency();

            moduleBuilder
                .Register(c =>
                {
                    // EF's meta data initialization is not thread-safe
                    lock (_lock)
                    {
                        var resolver = new CachingMetaDataResolver();
                        var result = new KistlDataContext(
                            resolver, 
                            null, 
                            c.Resolve<KistlConfig>(),
                            c.Resolve<ITypeTransformations>(),
                            c.Resolve<Func<IReadOnlyKistlContext>>(Kistl.API.Helper.FrozenContextServiceName)
                            );
                        resolver.Context = result;
                        return result;
                    }
                })
                .As<IReadOnlyKistlContext>()
                .SingleInstance();

            moduleBuilder
                .Register<MemoryContext>(c =>
                {
                    // defer loading the assemblies until a memory context is needed to avoid 
                    // initialisation troubles

                    // TODO: decide whether to load this from configuration too?
                    // TODO: Replace the default TypeTransformations with other configuration
                    return new MemoryContext(c.Resolve<ITypeTransformations>());
                })
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<Generator.EntityFrameworkGenerator>()
                .As<BaseDataObjectGenerator>()
                .SingleInstance();

            moduleBuilder
                .Register(c => new EfServerObjectHandlerFactory())
                .As(typeof(IServerObjectHandlerFactory));
        }
    }
}
