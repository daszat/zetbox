using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Interface.DataTypes
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyListTemplate.cst")]
    public partial class SimplePropertyListTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		private Property prop;


        public SimplePropertyListTemplate(Arebis.CodeGeneration.IGenerationHost _host, Property prop)
            : base(_host)
        {
			this.prop = prop;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyListTemplate.cst"
this.WriteObjects("\r\n");
#line 14 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyListTemplate.cst"
this.WriteObjects("        ",  GetPropertyTypeString() , " ",  GetPropertyName() , " { get; }");

        }



    }
}