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
		protected IKistlContext ctx;
		protected Property prop;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Interface.DataTypes.SimplePropertyListTemplate", ctx, prop);
        }

        public SimplePropertyListTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.prop = prop;

        }

        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyListTemplate.cst"
this.WriteObjects("\r\n");
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\SimplePropertyListTemplate.cst"
this.WriteObjects("        ",  GetPropertyTypeString() , " ",  GetPropertyName() , " { get; }");

        }

    }
}