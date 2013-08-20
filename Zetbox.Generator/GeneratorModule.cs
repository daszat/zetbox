// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using System.ComponentModel;
    using Zetbox.Generator.ResourceGenerator;
    using Zetbox.API.Server;

    [Feature]
    [Description("Basic generator infrastructure using MS Build")]
    public sealed class GeneratorModule
        : Module
    {
        internal static void RegisterCommon(ContainerBuilder builder)
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

            builder
                .RegisterCmdLineListAction("generate-resources:", "generates resources for the given modules or * or empty for all modules",
                (scope, arg) =>
                {
                    if (arg == null || arg.Length == 0) arg = new string[] { "*" };
                    scope.Resolve<ResourceCmdLineHandler>().Generate(arg);
                });

            builder
                .RegisterType<ResourceCmdLineHandler>()
                .AsSelf()
                .InstancePerDependency();

            builder
                .RegisterType<Zetbox.Generator.ResourceGenerator.ResourceGenerator>()
                .As<IResourceGenerator>()
                .InstancePerDependency();

            #region Register resource tasks
            builder
                .RegisterType<DataTypeTask>()
                .As<IResourceGeneratorTask>()
                .SingleInstance();

            builder
                .RegisterType<CategoryTagTask>()
                .As<IResourceGeneratorTask>()
                .SingleInstance();

            builder
                .RegisterType<ModuleTask>()
                .As<IResourceGeneratorTask>()
                .SingleInstance();

            builder
                .RegisterType<ApplicationTask>()
                .As<IResourceGeneratorTask>()
                .SingleInstance();
            #endregion
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

            GeneratorModule.RegisterCommon(builder);
        }
    }

    [Feature]
    [Description("Basic generator infrastructure using XBuild")]
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

            GeneratorModule.RegisterCommon(builder);
        }
    }
}
