
namespace Kistl.DalProvider.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Autofac;
    using Autofac.Builder;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.Server.Generators;

    public class EfProvider
        : Autofac.Builder.Module
    {
        private static readonly object _lock = new object();

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            var interfaceAssembly = Assembly.Load("Kistl.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            if (interfaceAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects Assembly, no Entity Framework Metadata will be loaded");
            var serverAssembly = Assembly.Load("Kistl.Objects.Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            if (serverAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects.Server Assembly, no Entity Framework Metadata will be loaded");

            // force-load a few assemblies to the reflection-only context so the DAL provider can find them
            // this uses the AssemblyLoader directly because Assembly.ReflectionOnlyLoad doesn't go through all 
            // the moves of resolving AssemblyNames to files. See http://stackoverflow.com/questions/570117/
            var reflectedInterfaceAssembly = AssemblyLoader.ReflectionOnlyLoadFrom("Kistl.Objects");
            if (reflectedInterfaceAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects Assembly for reflection, no Entity Framework Metadata will be loaded");
            var reflectedServerAssembly = AssemblyLoader.ReflectionOnlyLoadFrom("Kistl.Objects.Server");
            if (reflectedServerAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects.Server Assembly for reflection, no Entity Framework Metadata will be loaded");

            moduleBuilder
                .Register(c =>
                {
                    // EF's meta data initialization is not thread-safe
                    lock (_lock)
                    {
                        return new KistlDataContext(c.Resolve<KistlConfig>(), c.Resolve<IMetaDataResolver>(), null);
                    }
                })
                .As<IKistlServerContext>()
                .FactoryScoped();

            moduleBuilder
                .Register(c =>
                {
                    // EF's meta data initialization is not thread-safe
                    lock (_lock)
                    {
                        return new KistlDataContext(c.Resolve<KistlConfig>(), c.Resolve<IMetaDataResolver>(), c.Resolve<IIdentityResolver>().GetCurrent());
                    }
                })
                .As<IKistlContext>()
                .As<IReadOnlyKistlContext>()
                .FactoryScoped();

            moduleBuilder
                .Register<MemoryContext>(c =>
                {
                    // defer loading the assemblies until a memory context is needed to avoid 
                    // initialisation troubles

                    // TODO: decide whether to load this from configuration too?
                    System.Reflection.Assembly interfaces = System.Reflection.Assembly.Load("Kistl.Objects");
                    System.Reflection.Assembly implementation = System.Reflection.Assembly.Load("Kistl.Objects.Server");
                    return new MemoryContext(interfaces, implementation);
                })
                .FactoryScoped();

            moduleBuilder
                .RegisterGeneratedFactory<Func<MemoryContext>>()
                .SingletonScoped();

            moduleBuilder
                .Register<Generator.EntityFrameworkGenerator>()
                .As<BaseDataObjectGenerator>()
                .MemberOf<IEnumerable<BaseDataObjectGenerator>>()
                .SingletonScoped();

            moduleBuilder
                .Register(c => new ServerObjectHandlerFactory(
                    typeof(ServerCollectionHandler<,,,>),
                    typeof(ServerObjectHandler<>),
                    typeof(ServerObjectSetHandler)))
                .As(typeof(IServerObjectHandlerFactory));
        }
    }
}
