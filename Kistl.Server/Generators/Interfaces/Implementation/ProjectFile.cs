using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Interfaces.Implementation
{
    public class ProjectFile
        : Templates.Interface.ProjectFile
    {
        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, string projectGuid, List<string> fileNames)
            : base(_host, ctx, projectGuid, fileNames)
        {
        }

        protected override void ApplyAdditionalReferences()
        {
            base.ApplyAdditionalReferences();

            // used for XMLSerialization annotations
            this.WriteLine(@"    <Reference Include=""System.Xml"" />");
        }
    }
}
