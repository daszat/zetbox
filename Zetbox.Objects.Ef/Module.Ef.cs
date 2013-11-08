// <autogenerated/>

namespace Zetbox.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
	using Zetbox.API;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Zetbox.API.Server;
    using Zetbox.App.Extensions;
    using Zetbox.DalProvider.Ef;

    public class EfModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register<EfImplementationTypeChecker>(
                    c => new EfImplementationTypeChecker(
                        c.Resolve<Func<IEnumerable<IImplementationTypeChecker>>>()))
                .As<IEfImplementationTypeChecker>()
                .As<IImplementationTypeChecker>()
                .InstancePerDependency();
                
            builder
                .Register<EfActionsManager>(
                    c => new EfActionsManager(
                        c.Resolve<ILifetimeScope>(),
                        c.Resolve<IEnumerable<ImplementorAssembly>>()))
                .As<IEfActionsManager>()
                .InstancePerLifetimeScope();
        }
    }


    internal sealed class EfImplementationTypeChecker
        : Zetbox.API.BaseImplementationTypeChecker, IEfImplementationTypeChecker
    {
        public EfImplementationTypeChecker(Func<IEnumerable<IImplementationTypeChecker>> implTypeCheckersFactory)
            : base(implTypeCheckersFactory)
        {
        }

        protected override System.Reflection.Assembly GetAssembly()
        {
            return typeof(EfImplementationTypeChecker).Assembly;
        }
    }

    // marker class to provide stable and correct assembly reference
    internal sealed class EfActionsManager
        : BaseCustomActionsManager, IEfActionsManager
    {
        private static object _syncRoot = new object();
        private static bool _isInitialised = false;

        protected override object SyncRoot { get { return _syncRoot; } }
        protected override bool IsInitialised
        {
            get { return _isInitialised; }
            set { _isInitialised = value; }
        }

        public EfActionsManager(ILifetimeScope container, IEnumerable<ImplementorAssembly> assemblies)
            : base(container, "EfImpl", assemblies)
        {
        }
    }
}
