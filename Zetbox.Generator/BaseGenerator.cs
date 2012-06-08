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
    using Zetbox.API.Server;

    public class BaseGenerator
        : AbstractBaseGenerator
    {
        public BaseGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        public override string BaseName { get { return "Base"; } }
        public override string Description { get { return "Base"; } }
        public override string ExtraSuffix { get { return "Base"; } }
        public override string ProjectGuid { get { return "{18AA936D-BA5E-47A1-9F43-0473D88E10A5}"; } }
        public override string TargetNameSpace { get { return "Zetbox.Objects.Base"; } }
        public override string TemplateProviderNamespace { get { return "Zetbox.Generator.Templates"; } }
        public override IEnumerable<string> RequiredNamespaces { get { return new string[] { "Zetbox.DalProvider.Base" }; } }
        public override int CompileOrder { get { return COMPILE_ORDER_Implementation; } }

        protected override string Generate_ProjectFile(API.IZetboxContext ctx, string projectGuid, List<string> generatedFileNames, IEnumerable<ISchemaProvider> schemaProviders)
        {
            return RunTemplate(ctx, "ProjectFile",
                         TargetNameSpace + ".csproj",
                         projectGuid,
                         generatedFileNames.Where(s => !String.IsNullOrEmpty(s)).ToList(),
                         schemaProviders);
        }
    }
}
