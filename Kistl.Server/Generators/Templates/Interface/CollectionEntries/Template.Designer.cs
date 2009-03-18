using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Interface.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Interface\CollectionEntries\Template.cst")]
    public partial class Template : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.App.Base.Module module;


        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.App.Base.Module module)
            : base(_host)
        {
			this.ctx = ctx;
			this.module = module;

        }
        
        public override void Generate()
        {
#line 13 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("    using Kistl.API;\r\n");
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\CollectionEntries\Template.cst"
foreach(string ns in GetAdditionalImports())
    {

#line 21 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\CollectionEntries\Template.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 23 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\CollectionEntries\Template.cst"
}

#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\CollectionEntries\Template.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    /// <summary>\r\n");
this.WriteObjects("    /// ",  GetDescription() , "\r\n");
this.WriteObjects("    /// </summary>\r\n");
this.WriteObjects("    public interface ",  GetCeClassName() , " : ",  GetCeInterface() , " \r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("}");

        }



    }
}