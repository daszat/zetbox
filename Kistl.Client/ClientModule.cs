using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Extensions;
using Kistl.API.Client;
using Kistl.Client.Presentables;
using Autofac;
using Kistl.API.Configuration;

namespace Kistl.Client
{
    public sealed class ClientModule : Module
    {
        private class ViewModelDependencies : IViewModelDependencies
        {
            public ViewModelDependencies(IModelFactory f, IUiThreadManager ui, IAsyncThreadManager async)
            {
                Factory = f;
                UiThread = ui;
                AsyncThread = async;
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

            #endregion
        }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register(c =>
                {
                    var ctx = c.Resolve<IReadOnlyKistlContext>();
                    var aCfg = c.Resolve<IAssemblyConfiguration>();
                    var cams = new CustomActionsManagerClient(aCfg);
                    cams.Init(ctx);

                    return cams;
                })
                .As<BaseCustomActionsManager>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<FrozenActionsManagerClient>()
                .As<FrozenActionsManager>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<ModelFactory>()
                .As<IModelFactory>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<SynchronousThreadManager>()
                .As<IAsyncThreadManager>()
                .As<IUiThreadManager>();

            moduleBuilder
                .Register(c => new ViewModelDependencies(c.Resolve<IModelFactory>(), c.Resolve<IUiThreadManager>(), c.Resolve<IAsyncThreadManager>()))
                .As<IViewModelDependencies>();

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
