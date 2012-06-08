
namespace Zetbox.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class ProjectFile
    {
        protected virtual string GetAssemblyName()
        {
            return "Zetbox.Objects." + ImplementationSuffix;
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
            this.WriteLine("    <ProjectReference Include=\"..\\Zetbox.Objects\\Zetbox.Objects.csproj\">");
            this.WriteLine("      <Project>{0C9E6E69-309F-46F7-A936-D5762229DEB9}</Project>");
            this.WriteLine("      <Name>Zetbox.Objects</Name>");
            this.WriteLine("      <Private>False</Private>");
            this.WriteLine("    </ProjectReference>");

            // DAL Provider Base
            this.WriteLine(@"    <Reference Include=""Zetbox.DalProvider.Base"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(ZetboxAPIPath)\Common\Core\Zetbox.DalProvider.Base.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");

            // common functionality for all
            this.WriteLine(@"    <Reference Include=""Zetbox.API.Common"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(ZetboxAPIPath)\Common\Core\Zetbox.API.Common.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");
        }

        /// <summary>
        /// Returns the output path relative to the ZetboxAPIPath. This is only relevant for projects which are 
        /// compiled in the IDE, as the generator will override the values here.
        /// </summary>
        protected virtual string GetRelativeOutputPath()
        {
            return "Unconfigured";
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
