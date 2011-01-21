using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\DelegatingProperty.cst")]
    public partial class DelegatingProperty : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected string propName;
		protected string presentedType;
		protected string backingPropertyName;
		protected string backingType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string propName, string presentedType, string backingPropertyName, string backingType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.DelegatingProperty", ctx, propName, presentedType, backingPropertyName, backingType);
        }

        public DelegatingProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string propName, string presentedType, string backingPropertyName, string backingType)
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
#line 17 "P:\Kistl\Kistl.Generator\Templates\Properties\DelegatingProperty.cst"
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