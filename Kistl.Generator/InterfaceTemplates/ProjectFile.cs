
namespace Kistl.Generator.InterfaceTemplates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;

    public class ProjectFile
        : Templates.ProjectFile
    {
        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, string projectGuid, List<string> fileNames, IEnumerable<ISchemaProvider> schemaProviders)
            : base(_host, ctx, projectGuid, fileNames, schemaProviders)
        {
        }

        protected override string GetAssemblyName()
        {
            // hardcode interface assembly name
            return "Kistl.Objects";
        }

        protected override void ApplyAdditionalReferences()
        {
            // do not add self-reference
            // base.ApplyAdditionalReferences();
        }

        protected override void ApplyAdditionalProperties()
        {
            base.ApplyAdditionalProperties();
            this.WriteObjects("     <DocumentationFile>$(OutputPath)\\", GetAssemblyName(), ".xml</DocumentationFile>\r\n");
        }
    }
}
