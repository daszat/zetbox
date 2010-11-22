
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using AutofacContrib.NHibernate.Bytecode;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Extensions;
    using Kistl.App.Packaging;

    public class NHibernateProvider
        : Autofac.Module
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.DalProvider.NHibernate");

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<NHibernateImplementationType>()
                .As<NHibernateImplementationType>()
                .As<ImplementationType>()
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<NHibernateContext>()
                .As<BaseMemoryContext>()
                .As<IKistlServerContext>() // TODO initialize privileged context differently
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
    }
}
