using System;
using Kistl.API;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\DefaultProperty.cst")]
    public partial class DefaultProperty : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected string propName;
		protected string presentedType;


        public DefaultProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string propName, string presentedType)
            : base(_host)
        {
			this.ctx = ctx;
			this.propName = propName;
			this.presentedType = presentedType;

        }
        
        public override void Generate()
        {
#line 10 "P:\Kistl\Kistl.Generator\Templates\Properties\DefaultProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  presentedType , " ",  propName , " { get; set; }\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }



    }
}