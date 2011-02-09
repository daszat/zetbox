using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Reflection;

namespace Kistl.API
{
    public sealed class ApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register<InterfaceType>((c, p) => InterfaceType.Create(p.Named<Type>("type"), c.Resolve<IInterfaceTypeChecker>()))
                .InstancePerDependency();

            moduleBuilder
                .Register<LoggingProblemReporter>(c => new LoggingProblemReporter())
                .As<IProblemReporter>()
                .SingleInstance();

            moduleBuilder
                .RegisterDecorator<IKistlService>(
                    (c, inner) => new Utils.InfoLoggingKistlServiceDecorator(inner),
                    "implementor");
        }
    }
}
