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
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register(c =>
                {
                    var ctx = c.Resolve<IReadOnlyKistlContext>();
                    var appCtx = c.Resolve<ApplicationContext>();
                    var cams = new CustomActionsManagerClient(appCtx);
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
                .Register(c =>
                {
                    var cfg = c.Resolve<KistlConfig>();
                    var mf = c.Resolve<IModelFactory>();
                    return new GuiApplicationContext(cfg, mf);
                })
                .As<ApplicationContext>()
                .As<IGuiApplicationContext>()
                .SingleInstance();

            moduleBuilder.Register(c => KistlContext.GetContext())
                .As<IKistlContext>()
                .As<IReadOnlyKistlContext>()
                .InstancePerDependency();

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
