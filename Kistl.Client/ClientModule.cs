
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
            public ViewModelDependencies(IModelFactory f, IUiThreadManager ui, IAsyncThreadManager async, IFrozenContext frozenCtx)
            {
                Factory = f;
                UiThread = ui;
                AsyncThread = async;
                FrozenContext = frozenCtx;
            }

            #region IViewModelDependencies Members

            public IModelFactory Factory
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

            #endregion
        }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<SynchronousThreadManager>()
                .As<IAsyncThreadManager>()
                .As<IUiThreadManager>();

            moduleBuilder
                .RegisterType<ViewModelDependencies>()
                .As<IViewModelDependencies>();

            moduleBuilder
                .RegisterType<ThreadPrincipalResolver>()
                .As<IIdentityResolver>()
                .InstancePerLifetimeScope();

            // Register all ViewModel Types
            foreach (var t in typeof(ClientModule).Assembly.GetTypes()
                .Where(t => typeof(ViewModel).IsAssignableFrom(t)))
            {
                if (t.IsGenericTypeDefinition)
                {
                    moduleBuilder.RegisterGeneric(t)
                        .InstancePerDependency();
                }
                else
                {
                    moduleBuilder.RegisterType(t)
                        .InstancePerDependency();
                }
            }
        }
    }
}
