using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Interface.Enumerations
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst")]
    public partial class Template : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Enumeration e;


        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Enumeration e)
            : base(_host)
        {
			this.ctx = ctx;
			this.e = e;

        }
        
        public override void Generate()
        {
#line 14 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  e.Module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  e.Description , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
this.WriteObjects("    public enum ",  e.ClassName , "\r\n");
this.WriteObjects("    {\r\n");
#line 24 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
foreach(EnumerationEntry entry in e.EnumerationEntries)
    {

#line 27 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// ",  entry.Description , "\r\n");
this.WriteObjects("		/// </summary>\r\n");
this.WriteObjects("		",  entry.Name , " = ",  entry.Value , ",\r\n");
this.WriteObjects("\r\n");
#line 33 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
}

#line 35 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Interface\Enumerations\Template.cst"
this.WriteObjects("	}\r\n");
this.WriteObjects("}");

        }



    }
}