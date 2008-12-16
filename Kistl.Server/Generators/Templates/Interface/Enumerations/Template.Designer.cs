using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Interface.Enumerations
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst")]
    public partial class Template : Kistl.Server.Generators.KistlCodeTemplate
    {
		private Enumeration e;


        public Template(Arebis.CodeGeneration.IGenerationHost _host, Enumeration e)
            : base(_host)
        {
			this.e = e;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
this.WriteObjects("\r\n");
#line 14 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
this.WriteObjects("namespace ",  e.Module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("    using System;\r\n");
this.WriteObjects("    using System.Collections.Generic;\r\n");
this.WriteObjects("    using System.Collections.ObjectModel;\r\n");
this.WriteObjects("    using System.Linq;\r\n");
this.WriteObjects("    using System.Text;\r\n");
this.WriteObjects("    using System.Collections;\r\n");
this.WriteObjects("    using System.Xml;\r\n");
this.WriteObjects("    using System.Xml.Serialization;\r\n");
this.WriteObjects("    using Kistl.API;\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  e.Description , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
this.WriteObjects("    public enum ",  e.ClassName , "\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("		");
#line 32 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
foreach(EnumerationEntry entry in e.EnumerationEntries) { 
#line 33 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// ",  entry.Description , "\r\n");
this.WriteObjects("		/// </summary>\r\n");
this.WriteObjects("		",  entry.Name , " = ",  entry.Value , ",\r\n");
this.WriteObjects("		");
#line 37 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
} 
#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
this.WriteObjects("	}\r\n");
this.WriteObjects("}");

        }



    }
}