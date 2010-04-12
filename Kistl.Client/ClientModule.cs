using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Extensions;
using Kistl.API.Client;
using Kistl.Client.Presentables;
using Autofac;

namespace Kistl.Client
{
    public sealed class ClientModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register(c =>
                {
                    var ctx = c.Resolve<IReadOnlyKistlContext>();

                    var cams = new CustomActionsManagerClient();
                    cams.Init(ctx);

                    return cams;
                })
                .As<BaseCustomActionsManager>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<FrozenActionsManagerClient>()
                .As<FrozenActionsManager>()
                .SingleInstance();

            moduleBuilder.Register(c => KistlContext.GetContext())
                .As<IKistlContext>()
                .As<IReadOnlyKistlContext>()
                .InstancePerDependency();

            // Register all ViewModel Types
            moduleBuilder.RegisterAssemblyTypes(typeof(ClientModule).Assembly)
                .Where(t => typeof(ViewModel).IsAssignableFrom(t));
        }
    }
}
