
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Kistl.API.Utils;

    public sealed class ApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register<InterfaceType>((c, p) => InterfaceType.Create(p.Named<Type>("type"), c.Resolve<IInterfaceTypeChecker>()))
                .InstancePerDependency();

            builder
                .Register<LoggingProblemReporter>(c => new LoggingProblemReporter())
                .As<IProblemReporter>()
                .SingleInstance();

            builder
                .RegisterModule(new SmtpMailSender.Module());

            builder
                .RegisterAssemblyTypes(typeof(ApiModule).Assembly)
                .Except<SimpleCmdLineAction>()
                .AssignableTo<CmdLineAction>()
                .As<Option>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(typeof(ApiModule).Assembly)
                .Except<SimpleCmdLineData>()
                .Except<SimpleCmdLineFlag>()
                .AssignableTo<CmdLineData>()
                .As<Option>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ServiceControlManager>()
                .As<IServiceControlManager>()
                .SingleInstance();

            builder
                .RegisterType<DefaultFileOpener>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<KistlStreamReader>()
                .AsSelf()
                .InstancePerDependency();

            builder
                .RegisterType<KistlStreamWriter>()
                .AsSelf()
                .InstancePerDependency();

            builder
                .RegisterType<TypeMap>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
