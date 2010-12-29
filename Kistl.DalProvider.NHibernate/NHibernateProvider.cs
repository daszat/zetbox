
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Autofac.Core;
    //using AutofacContrib.NHibernate.Bytecode;
    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Extensions;
    using Kistl.App.Packaging;

    public class NHibernateProvider
        : Autofac.Module
    {
        public static readonly string ServerAssembly = "Kistl.Objects.NHibernateImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<NHibernateImplementationType>()
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
                    return new NHibernateContext(
                        c.Resolve<IMetaDataResolver>(),
                        param != null ? (Kistl.App.Base.Identity)param.Value : c.Resolve<IIdentityResolver>().GetCurrent(),
                        c.Resolve<KistlConfig>(),
                        c.Resolve<Func<IFrozenContext>>(),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<NHibernateImplementationType.Factory>(),
                        c.Resolve<global::NHibernate.ISession>()
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
                    return new NHibernateContext(
                        c.Resolve<IMetaDataResolver>(),
                        null,
                        c.Resolve<KistlConfig>(),
                        c.Resolve<Func<IFrozenContext>>(),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<NHibernateImplementationType.Factory>(),
                        c.Resolve<global::NHibernate.ISession>()
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
