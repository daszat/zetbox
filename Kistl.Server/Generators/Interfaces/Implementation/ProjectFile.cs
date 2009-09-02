using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Interfaces.Implementation
{
    /// <summary>
    /// blank inheritance to have template at the right place
    /// </summary>
    public class ProjectFile
        : Templates.Interface.ProjectFile
    {
        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, string projectGuid, List<string> fileNames)
            : base(_host, ctx, projectGuid, fileNames)
        {
        }

        protected override void ApplyAdditionalProperties()
        {
            base.ApplyAdditionalProperties();
            this.WriteObjects("     <DocumentationFile>$(OutputPath)\\", GetAssemblyName(), ".xml</DocumentationFile>\r\n");
        }
    }
}
