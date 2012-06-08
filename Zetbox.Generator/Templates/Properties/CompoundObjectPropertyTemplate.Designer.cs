using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst")]
    public partial class CompoundObjectPropertyTemplate : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string xmlNamespace;
		protected string propName;
		protected string backingPropertyName;
		protected string backingStoreName;
		protected string coType;
		protected string coImplementationType;
		protected bool isNullable;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string xmlNamespace, string propName, string backingPropertyName, string backingStoreName, string coType, string coImplementationType, bool isNullable)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CompoundObjectPropertyTemplate", ctx, serializationList, xmlNamespace, propName, backingPropertyName, backingStoreName, coType, coImplementationType, isNullable);
        }

        public CompoundObjectPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string xmlNamespace, string propName, string backingPropertyName, string backingStoreName, string coType, string coImplementationType, bool isNullable)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.xmlNamespace = xmlNamespace;
			this.propName = propName;
			this.backingPropertyName = backingPropertyName;
			this.backingStoreName = backingStoreName;
			this.coType = coType;
			this.coImplementationType = coImplementationType;
			this.isNullable = isNullable;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("");
#line 37 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\n");
this.WriteObjects("        // implement the user-visible interface\n");
#line 39 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
DelegatingProperty.Call(Host, ctx, propName, coType, backingPropertyName, coImplementationType); 
#line 40 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("        /// <summary>backing store for ",  UglyXmlEncode(propName) , "</summary>\n");
this.WriteObjects("        private ",  coImplementationType , " ",  backingStoreName , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        /// <summary>backing property for ",  UglyXmlEncode(propName) , ", takes care of attaching/detaching the values</summary>\n");
this.WriteObjects("        ",  GetModifiers() , " ",  coImplementationType , " ",  backingPropertyName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get \n");
this.WriteObjects("			{ \n");
this.WriteObjects("                if (!CurrentAccessRights.HasReadRights()) return null;\n");
this.WriteObjects("				return ",  backingStoreName , "; \n");
this.WriteObjects("			}\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\n");
#line 55 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
if (!isNullable) { 
#line 56 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                if (value == null)\n");
this.WriteObjects("                    throw new ArgumentNullException(\"value\");\n");
#line 58 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
} 
#line 59 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                if (!object.Equals(",  backingStoreName , ", value))\n");
this.WriteObjects("                {\n");
this.WriteObjects("					var __oldValue = ",  backingStoreName , ";\n");
this.WriteObjects("					var __newValue = value;\n");
this.WriteObjects("\n");
this.WriteObjects("					NotifyPropertyChanging(\"",  propName , "\", __oldValue, __newValue);\n");
this.WriteObjects("\n");
this.WriteObjects("					if (",  backingStoreName , " != null)\n");
this.WriteObjects("					{ \n");
this.WriteObjects("						",  backingStoreName , ".DetachFromObject(this, \"",  propName , "\");\n");
this.WriteObjects("					}\n");
this.WriteObjects("					if (__newValue == null)\n");
this.WriteObjects("					{\n");
this.WriteObjects("						",  backingStoreName , " = null;\n");
#line 73 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
ApplyStoreNull(); 
#line 74 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("					}\n");
this.WriteObjects("                    else\n");
this.WriteObjects("                    {\n");
this.WriteObjects("					    __newValue = (",  coImplementationType , ")__newValue.Clone();\n");
this.WriteObjects("					    ",  backingStoreName , " = __newValue;\n");
this.WriteObjects("					    ",  backingStoreName , ".AttachToObject(this, \"",  propName , "\");\n");
#line 80 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
ApplyStoreValue(); 
#line 81 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                    }\n");
this.WriteObjects("\n");
this.WriteObjects("					NotifyPropertyChanged(\"",  propName , "\", __oldValue, __newValue);\n");
this.WriteObjects("				}\n");
this.WriteObjects("				else\n");
this.WriteObjects("				{\n");
this.WriteObjects("					SetInitializedProperty(\"",  propName , "\");\n");
this.WriteObjects("				}\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
#line 92 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
AddSerialization(serializationList, coType, propName, coImplementationType, backingPropertyName);

#line 94 "P:\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , "");

        }

    }
}