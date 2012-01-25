
namespace Kistl.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;

    public sealed class GeneratorModule
        : Module
    {
        internal static void RegisterCmdLine(ContainerBuilder builder)
        {
            builder
                .RegisterCmdLineAction("generate", "generates and compiles new data classes",
                scope =>
                {
                    scope.Resolve<Compiler>().GenerateCode();
                });

            builder
                .RegisterCmdLineAction("compile", "[DEVEL] compiles new data classes from already generated code; used mostly for testing",
                scope =>
                {
                    scope.Resolve<Compiler>().CompileCode();
                });
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
               .RegisterType<InterfaceGenerator>()
               .As<AbstractBaseGenerator>()
               .SingleInstance();

            builder
                .RegisterType<MsBuildCompiler>()
                .As<Compiler>()
                .SingleInstance();

            GeneratorModule.RegisterCmdLine(builder);
        }
    }

    public sealed class XBuildGeneratorModule
       : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
               .RegisterType<InterfaceGenerator>()
               .As<AbstractBaseGenerator>()
               .SingleInstance();

            builder
                .RegisterType<XBuildCompiler>()
                .As<Compiler>()
                .SingleInstance();

            GeneratorModule.RegisterCmdLine(builder);
        }
    }
}
