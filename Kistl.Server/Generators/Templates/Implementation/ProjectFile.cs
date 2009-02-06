using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Implementation
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

            // local project references
            this.WriteLine(@"    <ProjectReference Include=""..\Kistl.Objects\Kistl.Objects.csproj"">");
            this.WriteLine(@"      <Project>{0C9E6E69-309F-46F7-A936-D5762229DEB9}</Project>");
            this.WriteLine(@"      <Name>Kistl.Objects</Name>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </ProjectReference>");

        }

    }
}
