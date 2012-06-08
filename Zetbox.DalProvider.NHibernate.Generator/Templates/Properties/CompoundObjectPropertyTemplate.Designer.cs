using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst")]
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
#line 21 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
#line 23 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
backingStoreName = "this.Proxy." + propName;

#line 25 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  coType , " ",  propName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get { return ",  backingPropertyName , ".CompoundObject_IsNull ? null : ",  backingPropertyName , "; }\r\n");
this.WriteObjects("            set { ",  backingPropertyName , " = (",  coImplementationType , ")value; }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>backing property for ",  UglyXmlEncode(propName) , ", takes care of attaching/detaching the values</summary>\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  coImplementationType , " ",  backingPropertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get \r\n");
this.WriteObjects("			{ \r\n");
this.WriteObjects("                if (!CurrentAccessRights.HasReadRights()) return null;\r\n");
this.WriteObjects("				return ",  backingStoreName , "; \r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
#line 43 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
if (!isNullable) { 
#line 44 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                if (value == null)\r\n");
this.WriteObjects("                    throw new ArgumentNullException(\"value\");\r\n");
#line 46 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
} 
#line 47 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
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
this.WriteObjects("						",  backingStoreName , " = new ",  coImplementationType , "(this, \"",  propName , "\", null, null) { CompoundObject_IsNull = true };\r\n");
this.WriteObjects("					}\r\n");
this.WriteObjects("                    else\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("					    __newValue = (",  coImplementationType , ")__newValue.Clone();\r\n");
this.WriteObjects("					    ",  backingStoreName , " = __newValue;\r\n");
this.WriteObjects("                        ",  backingStoreName , ".CompoundObject_IsNull = false;\r\n");
this.WriteObjects("					    ",  backingStoreName , ".AttachToObject(this, \"",  propName , "\");\r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("					NotifyPropertyChanged(\"",  propName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("				else\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					SetInitializedProperty(\"",  propName , "\");\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 79 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
AddSerialization(serializationList, coType, propName, coImplementationType, backingPropertyName);

#line 81 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , "");

        }

    }
}