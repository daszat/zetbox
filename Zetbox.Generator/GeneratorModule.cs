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
