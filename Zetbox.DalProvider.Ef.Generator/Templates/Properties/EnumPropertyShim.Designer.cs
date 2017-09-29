using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumPropertyShim.cst")]
    public partial class EnumPropertyShim : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string enumType;
		protected string name;
		protected string efName;
		protected bool isNullable;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string enumType, string name, string efName, bool isNullable)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.EnumPropertyShim", ctx, enumType, name, efName, isNullable);
        }

        public EnumPropertyShim(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string enumType, string name, string efName, bool isNullable)
            : base(_host)
        {
			this.ctx = ctx;
			this.enumType = enumType;
			this.name = name;
			this.efName = efName;
			this.isNullable = isNullable;

        }

        public override void Generate()
        {
#line 33 "C:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EnumPropertyShim.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>EF sees only this property, for ",  UglyXmlEncode(name) , "</summary>\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [EdmScalarProperty()]\r\n");
this.WriteObjects("        public int",  isNullable ? "?" : String.Empty , " ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return (int",  isNullable ? "?" : String.Empty , ")this.",  name , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                this.",  name , " = (",  enumType , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");

        }

    }
}