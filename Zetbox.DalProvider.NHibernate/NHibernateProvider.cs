// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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

    [Feature]
    [Description("nHibernate (NH) provider")]
    public class NHibernateProvider
        : Autofac.Module
    {
        public static readonly string ServerAssembly = "Zetbox.Objects.NHibernateImpl";
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
                    var cfg = c.Resolve<ZetboxConfig>();
                    return new NHibernateContext(
                        c.Resolve<IMetaDataResolver>(),
                        param != null ? (ZetboxPrincipal)param.Value : c.Resolve<IPrincipalResolver>().GetCurrent().Result,
                        cfg,
                        c.Resolve<Func<IFrozenContext>>(),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<NHibernateImplementationType.Factory>(),
                        c.Resolve<global::NHibernate.ISessionFactory>(),
                        c.Resolve<INHibernateImplementationTypeChecker>(),
                        c.Resolve<IPerfCounter>(),
                        c.ResolveNamed<ISqlErrorTranslator>(cfg.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey).SchemaProvider),
                        c.Resolve<IEnumerable<IZetboxContextEventListener>>()
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
                .Register(c => new NHibernateServerObjectHandlerFactory(c.ResolveOptional<Zetbox.API.Server.Fulltext.LuceneSearchDeps>()))
                .As<IServerObjectHandlerFactory>();

            moduleBuilder
                .RegisterType<NHInterceptor>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //moduleBuilder
            //    .RegisterType<AutofacBytecodeProvider>()
            //    .As<global::NHibernate.Bytecode.IBytecodeProvider>()
            //    .InstancePerDependency();

            moduleBuilder.RegisterModule((Autofac.Module)Activator.CreateInstance(Type.GetType("Zetbox.Objects.NHibernateModule, Zetbox.Objects.NHibernateImpl", true)));
        }

        private static Autofac.Builder.IRegistrationBuilder<NHibernateContext, Autofac.Builder.SimpleActivatorData, Autofac.Builder.SingleRegistrationStyle> RegisterContext<TInterface>(ContainerBuilder moduleBuilder)
            where TInterface : IReadOnlyZetboxContext
        {
            return moduleBuilder
                .Register(c =>
                {
                    var cfg = c.Resolve<ZetboxConfig>();
                    return new NHibernateContext(
                        c.Resolve<IMetaDataResolver>(),
                        null,
                        cfg,
                        c.Resolve<Func<IFrozenContext>>(),
                        c.Resolve<InterfaceType.Factory>(),
                        c.Resolve<NHibernateImplementationType.Factory>(),
                        c.Resolve<global::NHibernate.ISessionFactory>(),
                        c.Resolve<INHibernateImplementationTypeChecker>(),
                        c.Resolve<IPerfCounter>(),
                        c.ResolveNamed<ISqlErrorTranslator>(cfg.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey).SchemaProvider),
                        c.Resolve<IEnumerable<IZetboxContextEventListener>>()
                        );
                })
                .As<TInterface>()
                .OnActivated(args =>
                {
                    var manager = args.Context.Resolve<INHibernateActionsManager>();
                    manager.Init(args.Context.Resolve<IFrozenContext>());

                    ZetboxContextEventListenerHelper.OnCreated(args.Context.Resolve<IEnumerable<IZetboxContextEventListener>>(), args.Instance);
                });
        }
    }
}
