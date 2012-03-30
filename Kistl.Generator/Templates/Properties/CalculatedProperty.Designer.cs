using System;
using Kistl.API;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst")]
    public partial class CalculatedProperty : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected Serialization.SerializationMembersList serializationList;
		protected string modulenamespace;
		protected string className;
		protected string referencedType;
		protected string propertyName;
		protected string getterEventName;
		protected bool isCompound;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Serialization.SerializationMembersList serializationList, string modulenamespace, string className, string referencedType, string propertyName, string getterEventName, bool isCompound)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CalculatedProperty", ctx, serializationList, modulenamespace, className, referencedType, propertyName, getterEventName, isCompound);
        }

        public CalculatedProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Serialization.SerializationMembersList serializationList, string modulenamespace, string className, string referencedType, string propertyName, string getterEventName, bool isCompound)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.modulenamespace = modulenamespace;
			this.className = className;
			this.referencedType = referencedType;
			this.propertyName = propertyName;
			this.getterEventName = getterEventName;
			this.isCompound = isCompound;

        }

        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  referencedType , " ",  propertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  getterEventName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    throw new NotImplementedException(\"No handler registered on calculated property ",  className , ".",  propertyName , "\");\r\n");
this.WriteObjects("                }\r\n");
#line 24 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
if (isCompound) { 
#line 25 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("                var e = new PropertyGetterEventArgs<",  referencedType , ">(default(",  referencedType , "));\r");
this.WriteObjects("                ",  getterEventName , "(this, e);\r");
this.WriteObjects("                return e.Result;\r");
#line 28 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
} else { 
#line 29 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("                if (",  propertyName , "_IsDirty)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedType , ">(default(",  referencedType , "));\r\n");
this.WriteObjects("                    ",  getterEventName , "(this, e);\r\n");
this.WriteObjects("                    ",  ApplyStorageStatement("e.Result") , "\r\n");
this.WriteObjects("                    ",  propertyName , "_IsDirty = false;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  ApplyResultExpression() , ";\r\n");
#line 37 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
} 
#line 38 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 40 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
if (!isCompound) { 
#line 41 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("        ",  ApplyBackingStorageDefinition() , "\r\n");
#line 42 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
} 
#line 43 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("        private bool ",  propertyName , "_IsDirty = true;\r\n");
#line 44 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
AddSerialization(serializationList, propertyName); 
#line 45 "P:\Kistl\Kistl.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}