using System;
using Zetbox.API;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst")]
    public partial class CalculatedProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Serialization.SerializationMembersList serializationList;
		protected string modulenamespace;
		protected string className;
		protected string referencedType;
		protected string propertyName;
		protected string getterEventName;
		protected bool isCompound;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string modulenamespace, string className, string referencedType, string propertyName, string getterEventName, bool isCompound)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CalculatedProperty", ctx, serializationList, modulenamespace, className, referencedType, propertyName, getterEventName, isCompound);
        }

        public CalculatedProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string modulenamespace, string className, string referencedType, string propertyName, string getterEventName, bool isCompound)
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
#line 15 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  referencedType , " ",  propertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  getterEventName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    throw new NotImplementedException(\"No handler registered on calculated property ",  className , ".",  propertyName , "\");\r\n");
this.WriteObjects("                }\r\n");
#line 24 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
if (isCompound) { 
#line 25 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("                var e = new PropertyGetterEventArgs<",  referencedType , ">(default(",  referencedType , "));\r\n");
this.WriteObjects("                ",  getterEventName , "(this, e);\r\n");
this.WriteObjects("                return e.Result;\r\n");
#line 28 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
} else { 
#line 29 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("                if (_",  propertyName , "_IsDirty)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedType , ">(default(",  referencedType , "));\r\n");
this.WriteObjects("                    ",  getterEventName , "(this, e);\r\n");
this.WriteObjects("                    ",  ApplyStorageStatement("e.Result") , "\r\n");
this.WriteObjects("                    _",  propertyName , "_IsDirty = false;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  ApplyResultExpression() , ";\r\n");
#line 37 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
} 
#line 38 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 40 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
if (!isCompound) { 
#line 41 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("        ",  ApplyBackingStorageDefinition() , "\r\n");
#line 42 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
} 
#line 43 "P:\Zetbox\Zetbox.Generator\Templates\Properties\CalculatedProperty.cst"
this.WriteObjects("        private bool _",  propertyName , "_IsDirty = true; // Always true as it will not be stored in the database (yet)\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}