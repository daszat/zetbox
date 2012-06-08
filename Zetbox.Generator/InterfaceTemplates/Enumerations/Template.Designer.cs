using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.InterfaceTemplates.Enumerations
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\InterfaceTemplates\Enumerations\Template.cst")]
    public partial class Template : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Enumeration e;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Enumeration e)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Enumerations.Template", ctx, e);
        }

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Enumeration e)
            : base(_host)
        {
			this.ctx = ctx;
			this.e = e;

        }

        public override void Generate()
        {
#line 30 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Enumerations\Template.cst"
this.WriteObjects("// <autogenerated/>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  e.Module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  UglyXmlEncode(e.Description) , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
#line 38 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Enumerations\Template.cst"
if (e.AreFlags) {                                
#line 39 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Enumerations\Template.cst"
this.WriteObjects("    [System.Flags]\r\n");
#line 40 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Enumerations\Template.cst"
}                                                
#line 41 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Enumerations\Template.cst"
this.WriteObjects("    [Zetbox.API.DefinitionGuid(\"",  e.ExportGuid , "\")]\r\n");
this.WriteObjects("    public enum ",  e.Name , "\r\n");
this.WriteObjects("    {\r\n");
#line 44 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Enumerations\Template.cst"
foreach(EnumerationEntry entry in e.EnumerationEntries.OrderBy(ee => ee.Value))
    {                                               
#line 46 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Enumerations\Template.cst"
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// ",  UglyXmlEncode(entry.Description) , "\r\n");
this.WriteObjects("		/// </summary>\r\n");
this.WriteObjects("		",  entry.Name , " = ",  entry.Value , ",\r\n");
this.WriteObjects("\r\n");
#line 51 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Enumerations\Template.cst"
}                                               
#line 52 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\Enumerations\Template.cst"
this.WriteObjects("	}\r\n");
this.WriteObjects("}");

        }

    }
}