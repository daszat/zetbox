
namespace Kistl.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Configuration;
    using Kistl.App.Extensions;
    using Kistl.Client.Presentables;
    using Kistl.API.Common;

    public sealed class ClientModule : Module
    {
        private class ViewModelDependencies : IViewModelDependencies
        {
            public ViewModelDependencies(IViewModelFactory f, IUiThreadManager ui, IAsyncThreadManager async, IFrozenContext frozenCtx, IIdentityResolver idResolver)
            {
                Factory = f;
                UiThread = ui;
                AsyncThread = async;
                FrozenContext = frozenCtx;
                IdentityResolver = idResolver;
            }

            #region IViewModelDependencies Members

            public IViewModelFactory Factory
            {
                get;
                private set;
            }

            public IUiThreadManager UiThread
            {
                get;
                private set;
            }

            public IAsyncThreadManager AsyncThread
            {
                get;
                private set;
            }

            public IFrozenContext FrozenContext
            {
                get;
                private set;
            }

            public IIdentityResolver IdentityResolver
            {
                get;
                private set;
            }

            #endregion
        }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register<SynchronousThreadManager>(c => new SynchronousThreadManager())
                .As<IAsyncThreadManager>()
                .As<IUiThreadManager>();

            moduleBuilder
                .Register<ViewModelDependencies>(c => new ViewModelDependencies(
                    c.Resolve<IViewModelFactory>(), 
                    c.Resolve<IUiThreadManager>(), 
                    c.Resolve<IAsyncThreadManager>(), 
                    c.Resolve<IFrozenContext>(), 
                    c.Resolve<IIdentityResolver>()))
                .As<IViewModelDependencies>();

            moduleBuilder
                .Register<ThreadPrincipalResolver>(c=> new ThreadPrincipalResolver(c.Resolve<Func<IReadOnlyKistlContext>>()))
                .As<IIdentityResolver>()
                .InstancePerLifetimeScope();

            moduleBuilder
                .Register<LoggingProblemReporter>(c => new LoggingProblemReporter())
                .As<IProblemReporter>()
                .SingleInstance();

            moduleBuilder
                .Register<DefaultCredentialsResolver>(c => new DefaultCredentialsResolver())
                .As<ICredentialsResolver>()
                .SingleInstance();
            
            moduleBuilder.RegisterViewModels(typeof(ClientModule).Assembly);
        }
    }
}
