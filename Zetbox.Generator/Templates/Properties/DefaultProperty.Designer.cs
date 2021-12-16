using System;
using Zetbox.API;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\DefaultProperty.cst")]
    public partial class DefaultProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected string propName;
		protected string presentedType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string propName, string presentedType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.DefaultProperty", ctx, propName, presentedType);
        }

        public DefaultProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string propName, string presentedType)
            : base(_host)
        {
			this.ctx = ctx;
			this.propName = propName;
			this.presentedType = presentedType;

        }

        public override void Generate()
        {
#line 26 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\DefaultProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  presentedType , " ",  propName , " { get; set; }\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}