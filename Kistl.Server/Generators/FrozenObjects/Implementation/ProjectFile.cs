using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.FrozenObjects.Implementation
{
    public class ProjectFile
        : Templates.Implementation.ProjectFile
    {

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, string projectGuid, List<string> fileNames)
            : base(_host, ctx, projectGuid, fileNames)
        {
        }

        protected override string GetAssemblyName()
        {
            return "Kistl.Objects.Frozen";
        }

        protected override void ApplyAdditionalReferences()
        {
            base.ApplyAdditionalReferences();

            // used for indexing 
            this.WriteLine(@"    <Reference Include=""System.Data.Linq"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");

            // Frozen Provider infrastructure
            this.WriteLine(@"    <ProjectReference Include=""$(SourcePath)\Kistl.DalProvider.Frozen\Kistl.DalProvider.Frozen.csproj"">");
            this.WriteLine(@"      <Project>{F3B74A4A-BE0C-4BC2-BE32-D1A1D179B4F7}</Project>");
            this.WriteLine(@"      <Name>Kistl.DalProvider.Frozen</Name>");
            this.WriteLine(@"      <Private>True</Private>");
            this.WriteLine(@"    </ProjectReference>");

        }

    }
}
