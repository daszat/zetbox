
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Autofac.Core;
    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.API.Configuration;
    using Kistl.API.Server;

    public class NHibernateProvider
        : Autofac.Module
    {
        public static readonly string ServerAssembly = "Kistl.Objects.NHibernateImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";

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

            RegisterContext<IKistlServerContext>(moduleBuilder)
                .InstancePerDependency();

            RegisterContext<IReadOnlyKistlContext>(moduleBuilder)
                .InstancePerLifetimeScope();

            moduleBuilder
                .Register((c, p) =>
                {
                    var param = p.OfType<ConstantParameter>().FirstOrDefault();
                    var interceptor = c.Resolve<NHibernateAttachInterceptor>();
                    var session = c.Resolve<global::NHibernate.ISession>(new NamedParameter("interceptor", interceptor));
                    return new NHibernateContext(
                        c.Resolve<IMetaDataResolver>(),
                        param != null ? (Kistl.App.Base.Identity)param.Value : c.Resolve<IIdentityResolver>().GetCurrent(),
                        c.Resolve<KistlConfig>(),
                        c.Resolve<Func<IFrozenContext>>(),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<NHibernateImplementationType.Factory>(),
                        session,
                        interceptor,
                        c.Resolve<INHibernateImplementationTypeChecker>()
                        );
                })
                .As<IKistlContext>()
                .OnActivated(args =>
                {
                    var manager = args.Context.Resolve<INHibernateActionsManager>();
                    manager.Init(args.Context.Resolve<IFrozenContext>());
                })
                .InstancePerDependency();

            moduleBuilder
                .Register(c => new NHibernateServerObjectHandlerFactory())
                .As(typeof(IServerObjectHandlerFactory));

            moduleBuilder
                .RegisterType<NHibernateAttachInterceptor>()
                .InstancePerDependency();

            moduleBuilder
                .Register<global::NHibernate.ISession>(
                    (c, p) =>
                    {
                        var interceptor = p.Named<global::NHibernate.IInterceptor>("interceptor");
                        return c.Resolve<global::NHibernate.ISessionFactory>()
                            .OpenSession(interceptor);
                    })
                // TODO: reconsider this configuration
                //       using IPD makes it safer, but requires passing the session manually
                //       on the other hand, the session should never escape the data context
                .InstancePerDependency();

            //moduleBuilder
            //    .RegisterType<AutofacBytecodeProvider>()
            //    .As<global::NHibernate.Bytecode.IBytecodeProvider>()
            //    .InstancePerDependency();
        }

        private static Autofac.Builder.IRegistrationBuilder<NHibernateContext, Autofac.Builder.SimpleActivatorData, Autofac.Builder.SingleRegistrationStyle> RegisterContext<TInterface>(ContainerBuilder moduleBuilder)
            where TInterface : IReadOnlyKistlContext
        {
            return moduleBuilder
                .Register(c =>
                {
                    var interceptor = c.Resolve<NHibernateAttachInterceptor>();
                    var session = c.Resolve<global::NHibernate.ISession>(new NamedParameter("interceptor", interceptor));
                    return new NHibernateContext(
                        c.Resolve<IMetaDataResolver>(),
                        null,
                        c.Resolve<KistlConfig>(),
                        c.Resolve<Func<IFrozenContext>>(),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<NHibernateImplementationType.Factory>(),
                        session,
                        interceptor,
                        c.Resolve<INHibernateImplementationTypeChecker>()
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
