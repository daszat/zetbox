using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst")]
    public partial class CompoundObjectPropertyTemplate : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string xmlNamespace;
		protected string propName;
		protected string backingPropertyName;
		protected string backingStoreName;
		protected string coType;
		protected string coImplementationType;
		protected bool isNullable;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string xmlNamespace, string propName, string backingPropertyName, string backingStoreName, string coType, string coImplementationType, bool isNullable)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CompoundObjectPropertyTemplate", ctx, serializationList, xmlNamespace, propName, backingPropertyName, backingStoreName, coType, coImplementationType, isNullable);
        }

        public CompoundObjectPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string xmlNamespace, string propName, string backingPropertyName, string backingStoreName, string coType, string coImplementationType, bool isNullable)
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
#line 21 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
#line 23 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
DelegatingProperty.Call(Host, ctx, propName, coType, backingPropertyName, coImplementationType); 
#line 24 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>backing store for ",  UglyXmlEncode(propName) , "</summary>\r\n");
this.WriteObjects("        private ",  coImplementationType , " ",  backingStoreName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>backing property for ",  UglyXmlEncode(propName) , ", takes care of attaching/detaching the values</summary>\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  coImplementationType , " ",  backingPropertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get \r\n");
this.WriteObjects("			{ \r\n");
this.WriteObjects("                if (CurrentAccessRights == Kistl.API.AccessRights.None) return null;\r\n");
this.WriteObjects("				return ",  backingStoreName , "; \r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
#line 39 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
if (!isNullable) { 
#line 40 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                if (value == null)\r\n");
this.WriteObjects("                    throw new ArgumentNullException(\"value\");\r\n");
#line 42 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
} 
#line 43 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                if (!object.Equals(",  backingStoreName , ", value))\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var __oldValue = ",  backingStoreName , ";\r\n");
this.WriteObjects("					var __newValue = value;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("					NotifyPropertyChanging(\"",  propName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("					if (",  backingStoreName , " != null)\r\n");
this.WriteObjects("					{ \r\n");
this.WriteObjects("						",  backingStoreName , ".DetachFromObject(this, \"",  propName , "\");\r\n");
this.WriteObjects("					}\r\n");
this.WriteObjects("					if (__newValue == null)\r\n");
this.WriteObjects("					{\r\n");
this.WriteObjects("						",  backingStoreName , " = null;\r\n");
#line 57 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
ApplyStoreNull(); 
#line 58 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("					}\r\n");
this.WriteObjects("                    else\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("					    __newValue = (",  coImplementationType , ")__newValue.Clone();\r\n");
this.WriteObjects("					    ",  backingStoreName , " = __newValue;\r\n");
this.WriteObjects("					    ",  backingStoreName , ".AttachToObject(this, \"",  propName , "\");\r\n");
#line 64 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
ApplyStoreValue(); 
#line 65 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("					NotifyPropertyChanged(\"",  propName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 72 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
AddSerialization(serializationList, coType, propName, coImplementationType, backingPropertyName);

#line 74 "P:\Kistl\Kistl.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , "");

        }

    }
}