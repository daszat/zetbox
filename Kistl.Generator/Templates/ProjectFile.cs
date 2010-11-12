
namespace Kistl.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class ProjectFile
    {
        protected virtual string GetAssemblyName()
        {
            return "Kistl.Objects." + ImplementationSuffix;
        }

        /// <summary>
        /// Override this to add one or more Properties to a global &lt;PropertyGroup/>
        /// </summary>
        protected virtual void ApplyAdditionalProperties() { }

        /// <summary>
        /// Add additional assembly references. By default this only adds a reference to the currently generating interface assembly.
        /// 
        /// Extend this to add one or more &lt;References/>s to the project file
        /// </summary>
        protected virtual void ApplyAdditionalReferences()
        {
            this.WriteLine("    <!-- local reference to newly generated code -->");
            this.WriteLine("    <ProjectReference Include=\"..\\Kistl.Objects\\Kistl.Objects.csproj\">");
            this.WriteLine("      <Project>{0C9E6E69-309F-46F7-A936-D5762229DEB9}</Project>");
            this.WriteLine("      <Name>Kistl.Objects</Name>");
            this.WriteLine("      <Private>False</Private>");
            this.WriteLine("    </ProjectReference>");

            // DAL Provider Base
            this.WriteLine(@"    <Reference Include=""Kistl.DalProvider.Base"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(KistlAPIPath)\Common\Kistl.DalProvider.Base.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");

            // common functionality for all
            this.WriteLine(@"    <Reference Include=""TempAppHelpers"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(KistlAPIPath)\Common\TempAppHelpers.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");
        }
    
        /// <summary>
        /// Override this to add one or more &lt;ItemGroup/>s to the project file
        /// </summary>
        protected virtual void ApplyAdditionalItemGroups() { }

        /// <summary>
        /// Override this to add one or more &lt;PropertyGroup/>s to the project file
        /// </summary>
        protected virtual void ApplyAdditionalPropertyGroups() { }
    }
}
