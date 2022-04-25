using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst")]
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
		protected bool disableExport;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string xmlNamespace, string propName, string backingPropertyName, string backingStoreName, string coType, string coImplementationType, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.CompoundObjectPropertyTemplate", ctx, serializationList, xmlNamespace, propName, backingPropertyName, backingStoreName, coType, coImplementationType, disableExport);
        }

        public CompoundObjectPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string xmlNamespace, string propName, string backingPropertyName, string backingStoreName, string coType, string coImplementationType, bool disableExport)
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
			this.disableExport = disableExport;

        }

        public override void Generate()
        {
#line 37 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
#line 39 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
DelegatingProperty.Call(Host, ctx, propName, coType, backingPropertyName, coImplementationType); 
#line 40 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>backing store for ",  UglyXmlEncode(propName) , "</summary>\r\n");
this.WriteObjects("        private ",  coImplementationType , " ",  backingStoreName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>backing property for ",  UglyXmlEncode(propName) , ", takes care of attaching/detaching the values</summary>\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  coImplementationType , " ",  backingPropertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  backingStoreName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (value == null)\r\n");
this.WriteObjects("                    throw new ArgumentNullException(\"value\");\r\n");
this.WriteObjects("                if (!object.Equals(",  backingStoreName , ", value))\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var __oldValue = ",  backingStoreName , ";\r\n");
this.WriteObjects("                    var __newValue = value;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  propName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                    if (",  backingStoreName , " != null)\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("                        ",  backingStoreName , ".DetachFromObject(this, \"",  propName , "\");\r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("                    __newValue = (",  coImplementationType , ")__newValue.Clone();\r\n");
this.WriteObjects("                    ",  backingStoreName , " = __newValue;\r\n");
this.WriteObjects("                    ",  backingStoreName , ".AttachToObject(this, \"",  propName , "\");\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  propName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    if(IsAttached) UpdateChangedInfo = true;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 77 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
AddSerialization(serializationList, coType, propName, coImplementationType, backingPropertyName);

#line 79 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , "");

        }

    }
}