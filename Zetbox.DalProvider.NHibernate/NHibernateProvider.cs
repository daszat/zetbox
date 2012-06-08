
namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Autofac.Core;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Server.PerfCounter;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;

    public class NHibernateProvider
        : Autofac.Module
    {
        public static readonly string ServerAssembly = "Zetbox.Objects.NHibernateImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";
        private static readonly object _initLock = new object();
        private static bool _initQueryDone = false;

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register<NHibernateImplementationType>(
                    (c, p) => new NHibernateImplementationType(
                        p.Named<Type>("type"),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<INHibernateImplementationTypeChecker>()))
                    .As<NHibernateImplementationType>()
                    .As<ImplementationType>()
                    .InstancePerDependency();

            RegisterContext<IZetboxServerContext>(moduleBuilder)
                .InstancePerDependency();

            RegisterContext<IReadOnlyZetboxContext>(moduleBuilder)
                .InstancePerLifetimeScope();

            moduleBuilder
                .Register((c, p) =>
                {
                    var param = p.OfType<ConstantParameter>().FirstOrDefault();
                    return new NHibernateContext(
                        c.Resolve<IMetaDataResolver>(),
                        param != null ? (Zetbox.App.Base.Identity)param.Value : c.Resolve<IIdentityResolver>().GetCurrent(),
                        c.Resolve<ZetboxConfig>(),
                        c.Resolve<Func<IFrozenContext>>(),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<NHibernateImplementationType.Factory>(),
                        c.Resolve<global::NHibernate.ISession>(),
                        c.Resolve<INHibernateImplementationTypeChecker>(),
                        c.Resolve<IPerfCounter>()
                        );
                })
                .As<IZetboxContext>()
                .OnActivated(args =>
                {
                    var manager = args.Context.Resolve<INHibernateActionsManager>();
                    manager.Init(args.Context.Resolve<IFrozenContext>());
                    if (!_initQueryDone)
                    {
                        lock (_initLock)
                        {
                            if (!_initQueryDone)
                            {
                                var cls = args.Instance.GetQuery<ObjectClass>().FirstOrDefault();
                                // need to repeat initialization until the first proxy was created
                                _initQueryDone = cls != null;
                                Logging.Log.InfoFormat("Initialized NHibernate synchronously: done = {0}", _initQueryDone);
                            }
                        }
                    }
                })
                .InstancePerDependency();

            moduleBuilder
                .Register(c => new NHibernateServerObjectHandlerFactory())
                .As(typeof(IServerObjectHandlerFactory));

            moduleBuilder
                .RegisterType<LocalDateTimeInterceptor>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //moduleBuilder
            //    .RegisterType<AutofacBytecodeProvider>()
            //    .As<global::NHibernate.Bytecode.IBytecodeProvider>()
            //    .InstancePerDependency();
        }

        private static Autofac.Builder.IRegistrationBuilder<NHibernateContext, Autofac.Builder.SimpleActivatorData, Autofac.Builder.SingleRegistrationStyle> RegisterContext<TInterface>(ContainerBuilder moduleBuilder)
            where TInterface : IReadOnlyZetboxContext
        {
            return moduleBuilder
                .Register(c =>
                {
                    return new NHibernateContext(
                        c.Resolve<IMetaDataResolver>(),
                        null,
                        c.Resolve<ZetboxConfig>(),
                        c.Resolve<Func<IFrozenContext>>(),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<NHibernateImplementationType.Factory>(),
                        c.Resolve<global::NHibernate.ISession>(),
                        c.Resolve<INHibernateImplementationTypeChecker>(),
                        c.Resolve<IPerfCounter>()
                        );
                })
                .As<TInterface>()
                .OnActivated(args =>
                {
                    var manager = args.Context.Resolve<INHibernateActionsManager>();
                    manager.Init(args.Context.Resolve<IFrozenContext>());
                });
        }
    }
}
