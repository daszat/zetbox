using System;
using Kistl.API;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CalculatedProperty.cst")]
    public partial class CalculatedProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected string className;
		protected string referencedType;
		protected String propertyName;
		protected String getterEventName;


        public CalculatedProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, string referencedType, String propertyName, String getterEventName)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;
			this.referencedType = referencedType;
			this.propertyName = propertyName;
			this.getterEventName = getterEventName;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CalculatedProperty.cst"
this.WriteObjects("		public ",  referencedType , " ",  propertyName , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				if(",  getterEventName , " == null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					return null;\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("				var e = new PropertyGetterEventArgs<",  referencedType , ">(null);\r\n");
this.WriteObjects("				",  getterEventName , "(this, e);\r\n");
this.WriteObjects("				return e.Result;\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");

        }



    }
}