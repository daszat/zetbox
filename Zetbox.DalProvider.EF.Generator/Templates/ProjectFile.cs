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

namespace Zetbox.DalProvider.Ef.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.Generator;

    public class ProjectFile
        : Zetbox.Generator.Templates.ProjectFile
    {

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string projectGuid, List<string> fileNames, IEnumerable<ISchemaProvider> schemaProviders)
            : base(_host, ctx, projectGuid, fileNames, schemaProviders)
        {
        }

        protected override void ApplyAdditionalReferences()
        {
            base.ApplyAdditionalReferences();

            // Entity Framework assemblies
            this.WriteLine(@"    <Reference Include=""System.Data"" />");
            this.WriteLine(@"    <Reference Include=""System.Data.Entity"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");

            //// used all over the place
            //this.WriteLine(@"    <Reference Include=""System.Data.Linq"">");
            //this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            //this.WriteLine(@"    </Reference>");

            //// used for ObservableCollection?
            //this.WriteLine(@"    <Reference Include=""WindowsBase"">");
            //this.WriteLine(@"      <RequiredTargetFramework>3.0</RequiredTargetFramework>");
            //this.WriteLine(@"    </Reference>");

            // EF Provider infrastructure
            this.WriteLine(@"    <Reference Include=""Zetbox.DalProvider.Ef"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(ZetboxAPIPath)\Server\EF\Zetbox.DalProvider.Ef.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");

            // Server API
            this.WriteLine(@"    <Reference Include=""Zetbox.API.Server"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(ZetboxAPIPath)\Server\Core\Zetbox.API.Server.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");
        }

        protected override void ApplyAdditionalItemGroups()
        {
            base.ApplyAdditionalItemGroups();

            this.WriteLine(@"  <ItemGroup>");
            this.WriteLine(@"    <EmbeddedResource Include=""Model.csdl"" />");
            this.WriteLine(@"    <EmbeddedResource Include=""Model.msl"" />");
            foreach (var provider in schemaProviders.Where(sp => sp.IsStorageProvider))
            {
                this.WriteLine(@"    <EmbeddedResource Include=""Model.{0}.ssdl"" />", provider.ConfigName);
            }
            // hardcoded views for mssql
            //this.WriteLine(@"    <Compile Include=""Model.MSSQLViews.cs"" />");
            this.WriteLine(@"  </ItemGroup>");

        }

        protected override void ApplyAdditionalPropertyGroups()
        {
            base.ApplyAdditionalPropertyGroups();

            this.WriteLine(@"  <PropertyGroup>");
            // can only generate views for (globally-)registered data providers
            //this.WriteLine(@"    <PreBuildEvent>""%25windir%25\Microsoft.NET\Framework\v3.5\EdmGen.exe"" /nologo /language:CSharp /mode:ViewGeneration ""/inssdl:$(ProjectDir)Model.MSSQL.ssdl"" ""/incsdl:$(ProjectDir)Model.csdl"" ""/inmsl:$(ProjectDir)Model.msl"" ""/outviews:$(ProjectDir)Model.MSSQLViews.cs""</PreBuildEvent>");
            this.WriteLine(@"  </PropertyGroup>");
        }

        protected override string GetRelativeOutputPath()
        {
            return @"Server\EF";
        }
    }
}
