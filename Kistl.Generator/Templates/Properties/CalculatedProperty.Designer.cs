using System;
using Kistl.API;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst")]
    public partial class CalculatedProperty : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected string className;
		protected string referencedType;
		protected string propertyName;
		protected string getterEventName;
		protected bool isCompound;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, string referencedType, string propertyName, string getterEventName, bool isCompound)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CalculatedProperty", ctx, className, referencedType, propertyName, getterEventName, isCompound);
        }

        public CalculatedProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, string referencedType, string propertyName, string getterEventName, bool isCompound)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;
			this.referencedType = referencedType;
			this.propertyName = propertyName;
			this.getterEventName = getterEventName;
			this.isCompound = isCompound;

        }

        public override void Generate()
        {
#line 13 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  referencedType , " ",  propertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  getterEventName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    throw new NotImplementedException(\"No handler registered on calculated property ",  className , ".",  propertyName , "\");\r\n");
this.WriteObjects("                }\r\n");
#line 22 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
if (isCompound) { 
#line 23 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("                var e = new PropertyGetterEventArgs<",  referencedType , ">(default(",  referencedType , "));\r");
this.WriteObjects("                ",  getterEventName , "(this, e);\r");
this.WriteObjects("                return e.Result;\r");
#line 26 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
} else { 
#line 27 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("                if (",  propertyName , "_IsDirty)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedType , ">(default(",  referencedType , "));\r\n");
this.WriteObjects("                    ",  getterEventName , "(this, e);\r\n");
this.WriteObjects("                    ",  ApplyStorageStatement("e.Result") , "\r\n");
this.WriteObjects("                    ",  propertyName , "_IsDirty = false;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  ApplyResultExpression() , ";\r\n");
#line 35 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
} 
#line 36 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 38 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
if (!isCompound) { 
#line 39 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("        ",  ApplyBackingStorageDefinition() , "\r\n");
#line 40 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
} 
#line 41 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("        private bool ",  propertyName , "_IsDirty = true;\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}