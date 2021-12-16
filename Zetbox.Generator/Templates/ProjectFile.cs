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
            this.WriteLine("      <PrivateAssets>all</PrivateAssets>");
            this.WriteLine("    </ProjectReference>");

            // DAL Provider Base
            this.WriteLine(@"    <Reference Include=""Zetbox.DalProvider.Base"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(ZetboxAPIPath)\Common\Zetbox.DalProvider.Base.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");

            // common functionality for all
            this.WriteLine(@"    <Reference Include=""Zetbox.API.Common"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(ZetboxAPIPath)\Common\Zetbox.API.Common.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");
        }

        /// <summary>
        /// Returns the output path relative to the ZetboxAPIPath. This is only relevant for projects which are 
        /// compiled in the IDE, as the generator will override the values here.
        /// 
        /// This method MUST be overridden.
        /// </summary>
        protected virtual string GetRelativeOutputPath()
        {
            throw new InvalidOperationException("GetRelativeOutputPath is not overridden");
        }

        protected virtual string RelativeExternBuildOutputSubDirectory
        {
            get
            {
                return IsFallback ? "Fallback" : "Generated";
            }
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
