using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Interface.DataTypes
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyTemplate.cst")]
    public partial class SimplePropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Property prop;
		protected bool isReadonly;


        public SimplePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop, bool isReadonly)
            : base(_host)
        {
			this.ctx = ctx;
			this.prop = prop;
			this.isReadonly = isReadonly;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyTemplate.cst"
string name = prop.PropertyName;
	string type = prop.ReferencedTypeAsCSharp();

#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyTemplate.cst"
this.WriteObjects("		",  type , " ",  name , " {\r\n");
this.WriteObjects("			get;\r\n");
#line 21 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyTemplate.cst"
if (!isReadonly)
	{

#line 24 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyTemplate.cst"
this.WriteObjects("			set;\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyTemplate.cst"
}

#line 28 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyTemplate.cst"
this.WriteObjects("		}");

        }



    }
}