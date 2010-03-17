using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst")]
    public partial class GetPropertyErrorTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;


        public GetPropertyErrorTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("\r\n");
#line 16 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
var properties = cls.Properties.OrderBy(p => p.Name).ToList();
	if (properties.Count > 0) {

#line 19 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("		private static readonly System.ComponentModel.PropertyDescriptor[] _properties = new System.ComponentModel.PropertyDescriptor[] {\r\n");
#line 21 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
foreach(var property in properties)
		{
			string propertyName = property.Name;
			if (property.IsAssociation() && !property.IsObjectReferencePropertySingle())
			{

#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("			new CustomPropertyDescriptor<",  cls.Name , ", ",  property.GetCollectionTypeString() , ">(\r\n");
this.WriteObjects("				new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("				\"",  propertyName , "\",\r\n");
this.WriteObjects("				null,\r\n");
this.WriteObjects("				obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("				null), // lists are read-only properties\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
}
			else if (property is CalculatedObjectReferenceProperty) {

#line 37 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("			new CustomPropertyDescriptor<",  cls.Name , ", ",  property.ReferencedTypeAsCSharp() , ">(\r\n");
this.WriteObjects("				new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("				\"",  propertyName , "\",\r\n");
this.WriteObjects("				null,\r\n");
this.WriteObjects("				obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("				null), // CalculatedObjectReferenceProperty is a read-only property\r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
} else {

#line 46 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("			new CustomPropertyDescriptor<",  cls.Name , ", ",  property.ReferencedTypeAsCSharp() , ">(\r\n");
this.WriteObjects("				new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("				\"",  propertyName , "\",\r\n");
this.WriteObjects("				null,\r\n");
this.WriteObjects("				obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("				(obj, val) => obj.",  propertyName , " = val),\r\n");
#line 53 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
}
		}

#line 56 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("		};\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		protected override void CollectProperties(List<System.ComponentModel.PropertyDescriptor> props)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			props.AddRange(_properties);\r\n");
this.WriteObjects("		}\r\n");
#line 63 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
}

#line 65 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("	\r\n");

        }



    }
}