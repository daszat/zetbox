using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.InterfaceTemplates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/InterfaceTemplates/Properties/SimplePropertyTemplate.cst")]
    public partial class SimplePropertyTemplate : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected Property prop;
		protected bool isReadonly;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop, bool isReadonly)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.SimplePropertyTemplate", ctx, prop, isReadonly);
        }

        public SimplePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop, bool isReadonly)
            : base(_host)
        {
			this.ctx = ctx;
			this.prop = prop;
			this.isReadonly = isReadonly;

        }

        public override void Generate()
        {
#line 15 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/InterfaceTemplates/Properties/SimplePropertyTemplate.cst"
string name = prop.Name;
    string type = prop.ReferencedTypeAsCSharp();

#line 18 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/InterfaceTemplates/Properties/SimplePropertyTemplate.cst"
this.WriteObjects("        ",  type , " ",  name , " {\r\n");
this.WriteObjects("            get;\r\n");
#line 21 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/InterfaceTemplates/Properties/SimplePropertyTemplate.cst"
if (!isReadonly)
    {

#line 24 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/InterfaceTemplates/Properties/SimplePropertyTemplate.cst"
this.WriteObjects("            set;\r\n");
#line 26 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/InterfaceTemplates/Properties/SimplePropertyTemplate.cst"
}

#line 28 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/InterfaceTemplates/Properties/SimplePropertyTemplate.cst"
this.WriteObjects("        }\r\n");

        }

    }
}