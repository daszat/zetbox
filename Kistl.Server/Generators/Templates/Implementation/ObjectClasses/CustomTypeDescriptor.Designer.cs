using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst")]
    public partial class CustomTypeDescriptor : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;


        public CustomTypeDescriptor(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("\r\n");
#line 16 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
var set = Settings;
	var clsName = cls.Name + Kistl.API.Helper.ImplementationSuffix + Settings["extrasuffix"];
	var properties = cls.Properties.OrderBy(p => p.Name).ToList();
	var rels = cls.Context.GetQuery<Relation>().Where(r => r.A.Type == cls || r.B.Type == cls).ToList();
	
	if (properties.Count > 0 || rels.Count > 0)
	{

#line 24 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("		private static readonly System.ComponentModel.PropertyDescriptor[] _properties = new System.ComponentModel.PropertyDescriptor[] {\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
foreach(var property in properties)
		{
			string propertyName = property.Name;
			if (property.IsAssociation() && !property.IsObjectReferencePropertySingle())
			{

#line 32 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("			// property.IsAssociation() && !property.IsObjectReferencePropertySingle()\r\n");
this.WriteObjects("			new CustomPropertyDescriptor<",  clsName , ", ",  property.GetCollectionTypeString() , ">(\r\n");
this.WriteObjects("				new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("				\"",  propertyName , "\",\r\n");
this.WriteObjects("				null,\r\n");
this.WriteObjects("				obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("				null), // lists are read-only properties\r\n");
#line 40 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
}
			else if (property is CalculatedObjectReferenceProperty)
			{

#line 44 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("			// property is CalculatedObjectReferenceProperty\r\n");
this.WriteObjects("			new CustomPropertyDescriptor<",  clsName , ", ",  property.ReferencedTypeAsCSharp() , ">(\r\n");
this.WriteObjects("				new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("				\"",  propertyName , "\",\r\n");
this.WriteObjects("				null,\r\n");
this.WriteObjects("				obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("				null), // CalculatedObjectReferenceProperty is a read-only property\r\n");
#line 52 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
} else {

#line 54 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("			// else\r\n");
this.WriteObjects("			new CustomPropertyDescriptor<",  clsName , ", ",  property.ReferencedTypeAsCSharp() , ">(\r\n");
this.WriteObjects("				new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("				\"",  propertyName , "\",\r\n");
this.WriteObjects("				null,\r\n");
this.WriteObjects("				obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("				(obj, val) => obj.",  propertyName , " = val),\r\n");
#line 62 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
}
		}
		
		if ("Frozen".Equals(Settings["extrasuffix"]))
		{

#line 68 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("			// skipping position columns for frozen context (not implemented)\r\n");
#line 70 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
} else {		
			foreach(var rel in rels.Where(r => r.GetRelationType() == RelationType.one_n))
			{

#line 74 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("			// rel: ",  rel.A.RoleName , " ",  rel.Verb , " ",  rel.B.RoleName , " (",  rel.ExportGuid , ")\r\n");
#line 76 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
if (rel.A.Type == cls && rel.A.HasPersistentOrder) 
			{
				var posColumnName = Construct.ListPositionPropertyName(rel.A);

#line 80 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("			// rel.A.Type == cls && rel.A.HasPersistentOrder\r\n");
this.WriteObjects("			new CustomPropertyDescriptor<",  clsName , ", int?>(\r\n");
this.WriteObjects("				null,\r\n");
this.WriteObjects("				\"",  posColumnName , "\",\r\n");
this.WriteObjects("				null,\r\n");
this.WriteObjects("				obj => obj.",  posColumnName , ",\r\n");
this.WriteObjects("				(obj, val) => obj.",  posColumnName , " = val),\r\n");
#line 88 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
}
			
				if (rel.B.Type == cls && rel.B.HasPersistentOrder) 
				{
					var posColumnName = Construct.ListPositionPropertyName(rel.B);

#line 94 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("			// rel.B.Type == cls && rel.B.HasPersistentOrder\r\n");
this.WriteObjects("			new CustomPropertyDescriptor<",  clsName , ", int?>(\r\n");
this.WriteObjects("				null,\r\n");
this.WriteObjects("				\"",  posColumnName , "\",\r\n");
this.WriteObjects("				null,\r\n");
this.WriteObjects("				obj => obj.",  posColumnName , ",\r\n");
this.WriteObjects("				(obj, val) => obj.",  posColumnName , " = val),\r\n");
#line 102 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
}
			}
		}

#line 106 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("		};\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		protected override void CollectProperties(List<System.ComponentModel.PropertyDescriptor> props)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			base.CollectProperties(props);\r\n");
this.WriteObjects("			props.AddRange(_properties);\r\n");
this.WriteObjects("		}\r\n");
#line 114 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
}

#line 116 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("	\r\n");

        }



    }
}