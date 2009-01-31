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


        public SimplePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.prop = prop;

        }
        
        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyTemplate.cst"
this.WriteObjects("\r\n");
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyTemplate.cst"
string name = prop.PropertyName;
	string type = prop.ReferencedTypeAsCSharp();

#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyTemplate.cst"
this.WriteObjects("		",  type , " ",  name , " { get; set; }");

        }



    }
}