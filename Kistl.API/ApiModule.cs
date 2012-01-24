using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;

namespace Kistl.API
{
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
                .RegisterModule(new Kistl.API.SmtpMailSender.Module());

            builder
                .RegisterAssemblyTypes(typeof(ApiModule).Assembly)
                .AssignableTo<CmdLineOption>()
                .As<CmdLineOption>()
                .InstancePerLifetimeScope();
        }
    }
}
