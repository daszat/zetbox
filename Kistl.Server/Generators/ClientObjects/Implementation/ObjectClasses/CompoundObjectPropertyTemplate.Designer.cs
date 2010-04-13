using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst")]
    public partial class CompoundObjectPropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected CompoundObjectProperty prop;
		protected string propName;
		protected string backingPropertyName;
		protected string backingStoreName;
		protected string coType;
		protected string coImplementationType;


        public CompoundObjectPropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, CompoundObjectProperty prop, string propName, string backingPropertyName, string backingStoreName, string coType, string coImplementationType)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;
			this.propName = propName;
			this.backingPropertyName = backingPropertyName;
			this.backingStoreName = backingStoreName;
			this.coType = coType;
			this.coImplementationType = coImplementationType;

        }
        
        public override void Generate()
        {
#line 20 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("		// ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        public ",  coType , " ",  propName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get { return ",  backingPropertyName , "; }\r\n");
this.WriteObjects("            set { ",  backingPropertyName , " = (",  coImplementationType , ")value; }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>backing store for ",  propName , "</summary>\r\n");
this.WriteObjects("        private ",  coImplementationType , " ",  backingStoreName , ";\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        /// <summary>backing property for ",  propName , ", takes care of attaching/detaching the values</summary>\r\n");
this.WriteObjects("        private ",  coImplementationType , " ",  backingPropertyName , " {\r\n");
this.WriteObjects("            get { return ",  backingStoreName , "; }\r\n");
this.WriteObjects("            set {\r\n");
#line 35 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
if(!prop.IsNullable())
				{

#line 37 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("				            \r\n");
this.WriteObjects("                if (value == null)\r\n");
this.WriteObjects("					throw new ArgumentNullException(\"value\");\r\n");
#line 41 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
}                

#line 43 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("                var __oldValue = ",  backingStoreName , ";\r\n");
this.WriteObjects("                var __newValue = value;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                NotifyPropertyChanging(\"",  propName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                \r\n");
this.WriteObjects("				if (",  backingStoreName , " != null) ",  backingStoreName , ".DetachFromObject(this, \"",  propName , "\");\r\n");
this.WriteObjects("                ",  backingStoreName , " = value != null ? (",  coImplementationType , ")value.Clone() : null;\r\n");
this.WriteObjects("				if (",  backingStoreName , " != null) ",  backingStoreName , ".AttachToObject(this, \"",  propName , "\");\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  propName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("		}\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
AddSerialization(serializationList, propName, backingPropertyName);

#line 57 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\CompoundObjectPropertyTemplate.cst"
this.WriteObjects("  ");

        }



    }
}