using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst")]
    public partial class CompoundObjectPropertyTemplate : Kistl.Generator.ResourceTemplate
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
#line 21 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  coType , " ",  propName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get { return ",  backingPropertyName , ".CompoundObject_IsNull ? null : ",  backingPropertyName , "; }\r\n");
this.WriteObjects("            set { ",  backingPropertyName , " = (",  coImplementationType , ")value; }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>backing store for ",  propName , "</summary>\r\n");
this.WriteObjects("        private ",  coImplementationType , " ",  backingStoreName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>backing property for ",  propName , ", takes care of attaching/detaching the values, mapped via EF</summary>\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [EdmComplexProperty()]\r\n");
this.WriteObjects("        public ",  coImplementationType , " ",  backingPropertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  backingStoreName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
#line 44 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
if(!isNullable)
                {

#line 47 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                if (value == null)\r\n");
this.WriteObjects("                    throw new ArgumentNullException(\"value\");\r\n");
#line 50 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
}

#line 52 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                if (!object.Equals(",  backingStoreName , ", value))\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var __oldValue = ",  backingStoreName , ";\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  propName , "\", \"",  backingPropertyName , "\", __oldValue, value);\r\n");
this.WriteObjects("                    if (",  backingStoreName , " != null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("                        ",  backingStoreName , ".DetachFromObject(this, \"",  propName , "\");\r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("                    if(value == null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("                        ",  backingStoreName , " = new ",  coImplementationType , "(true, this, \"",  propName , "\");\r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("                    else\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("                        ",  backingStoreName , " = (",  coImplementationType , ")value.Clone();\r\n");
this.WriteObjects("                        ",  backingStoreName , ".AttachToObject(this, \"",  propName , "\");\r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  propName , "\", \"",  backingPropertyName , "\", __oldValue, value);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
#line 76 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
AddSerialization(serializationList, propName, backingStoreName);

#line 78 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("           // BEGIN ",  this.GetType() , "");

        }



    }
}