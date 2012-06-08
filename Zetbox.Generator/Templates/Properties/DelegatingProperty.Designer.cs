using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Properties\DelegatingProperty.cst")]
    public partial class DelegatingProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected string propName;
		protected string presentedType;
		protected string backingPropertyName;
		protected string backingType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string propName, string presentedType, string backingPropertyName, string backingType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.DelegatingProperty", ctx, propName, presentedType, backingPropertyName, backingType);
        }

        public DelegatingProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string propName, string presentedType, string backingPropertyName, string backingType)
            : base(_host)
        {
			this.ctx = ctx;
			this.propName = propName;
			this.presentedType = presentedType;
			this.backingPropertyName = backingPropertyName;
			this.backingType = backingType;

        }

        public override void Generate()
        {
#line 33 "P:\zetbox\Zetbox.Generator\Templates\Properties\DelegatingProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  presentedType , " ",  propName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get { return ",  backingPropertyName , "; }\r\n");
this.WriteObjects("            set { ",  backingPropertyName , " = (",  backingType , ")value; }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}