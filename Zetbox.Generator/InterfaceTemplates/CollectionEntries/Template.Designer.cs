using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.InterfaceTemplates.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\InterfaceTemplates\CollectionEntries\Template.cst")]
    public partial class Template : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.App.Base.Module module;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.App.Base.Module module)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("CollectionEntries.Template", ctx, module);
        }

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.App.Base.Module module)
            : base(_host)
        {
			this.ctx = ctx;
			this.module = module;

        }

        public override void Generate()
        {
#line 30 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\CollectionEntries\Template.cst"
this.WriteObjects("// <autogenerated/>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("    using Zetbox.API;\r\n");
#line 36 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\CollectionEntries\Template.cst"
foreach(string ns in GetAdditionalImports().Distinct().OrderBy(s => s))
    {

#line 39 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\CollectionEntries\Template.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 41 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\CollectionEntries\Template.cst"
}

#line 43 "P:\zetbox\Zetbox.Generator\InterfaceTemplates\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  UglyXmlEncode(GetDescription()) , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
this.WriteObjects("    [Zetbox.API.DefinitionGuid(\"",  GetDefinitionGuid() , "\")]\r\n");
this.WriteObjects("    public interface ",  GetCeClassName() , " : ",  GetCeInterface() , " \r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("}");

        }

    }
}