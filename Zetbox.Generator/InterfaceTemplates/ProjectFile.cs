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

namespace Zetbox.Generator.InterfaceTemplates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;

    public class ProjectFile
        : Templates.ProjectFile
    {
        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx, string projectGuid, List<string> fileNames, IEnumerable<ISchemaProvider> schemaProviders)
            : base(_host, ctx, projectGuid, fileNames, schemaProviders)
        {
        }

        protected override string GetAssemblyName()
        {
            // hardcode interface assembly name
            return "Zetbox.Objects";
        }

        protected override void ApplyAdditionalReferences()
        {
            // do not add self-reference
            // base.ApplyAdditionalReferences();
        }

        protected override void ApplyAdditionalProperties()
        {
            base.ApplyAdditionalProperties();
            this.WriteObjects("    <DocumentationFile>$(OutputPath)\\", GetAssemblyName(), ".xml</DocumentationFile>\r\n");
        }

        protected override string GetRelativeOutputPath()
        {
            return @"Common\Core";
        }
    }
}
