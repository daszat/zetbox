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
		protected string implName;


        public CustomTypeDescriptor(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls, string implName)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;
			this.implName = implName;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("\r\n");
#line 17 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
var properties = cls.Properties.OrderBy(p => p.Name).ToList();
	var rels = cls.Context.GetQuery<Relation>().Where(r => r.A.Type == cls || r.B.Type == cls)
		.OrderBy(i => i.A.Type.Name).ThenBy(i => i.Verb).ThenBy(i => i.B.Type.Name).ThenBy(i => i.ExportGuid)
		.ToList();
	
	if (properties.Count > 0 || rels.Count > 0)
	{

#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("		private static readonly object _propertiesLock = new object();\r\n");
this.WriteObjects("		private static System.ComponentModel.PropertyDescriptor[] _properties;\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		private void _InitializePropertyDescriptors(Func<IReadOnlyKistlContext> lazyCtx)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			if (_properties != null) return;\r\n");
this.WriteObjects("			lock (_propertiesLock)\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				// recheck for a lost race after aquiring the lock\r\n");
this.WriteObjects("				if (_properties != null) return;\r\n");
this.WriteObjects("				\r\n");
this.WriteObjects("				_properties = new System.ComponentModel.PropertyDescriptor[] {\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
foreach(var property in properties)
		{
			string propertyName = property.Name;
			if (property.IsAssociation() && !property.IsObjectReferencePropertySingle())
			{

#line 44 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("					// property.IsAssociation() && !property.IsObjectReferencePropertySingle()\r\n");
this.WriteObjects("					new CustomPropertyDescriptor<",  implName , ", ",  property.GetCollectionTypeString() , ">(\r\n");
this.WriteObjects("						lazyCtx,\r\n");
this.WriteObjects("						new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("						\"",  propertyName , "\",\r\n");
this.WriteObjects("						null,\r\n");
this.WriteObjects("						obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("						null), // lists are read-only properties\r\n");
#line 53 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
}
			else if (property is CalculatedObjectReferenceProperty)
			{

#line 57 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("					// property is CalculatedObjectReferenceProperty\r\n");
this.WriteObjects("					new CustomPropertyDescriptor<",  implName , ", ",  property.ReferencedTypeAsCSharp() , ">(\r\n");
this.WriteObjects("						lazyCtx,\r\n");
this.WriteObjects("						new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("						\"",  propertyName , "\",\r\n");
this.WriteObjects("						null,\r\n");
this.WriteObjects("						obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("						null), // CalculatedObjectReferenceProperty is a read-only property\r\n");
#line 66 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
} else {

#line 68 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("					// else\r\n");
this.WriteObjects("					new CustomPropertyDescriptor<",  implName , ", ",  property.ReferencedTypeAsCSharp() , ">(\r\n");
this.WriteObjects("						lazyCtx,\r\n");
this.WriteObjects("						new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("						\"",  propertyName , "\",\r\n");
this.WriteObjects("						null,\r\n");
this.WriteObjects("						obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("						(obj, val) => obj.",  propertyName , " = val),\r\n");
#line 77 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
}
		}
		

		if ("Frozen".Equals(Settings["extrasuffix"]))
		{

#line 84 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("					// skipping position columns for frozen context (not implemented)\r\n");
#line 86 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
} else {		
			foreach(var rel in rels.Where(r => r.GetRelationType() == RelationType.one_n))
			{

#line 90 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("					// rel: ",  rel.A.RoleName , " ",  rel.Verb , " ",  rel.B.RoleName , " (",  rel.ExportGuid , ")\r\n");
#line 92 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
if (rel.A.Type == cls && rel.A.HasPersistentOrder) 
			{
				var posColumnName = Construct.ListPositionPropertyName(rel.A);

#line 96 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("					// rel.A.Type == cls && rel.A.HasPersistentOrder\r\n");
this.WriteObjects("					new CustomPropertyDescriptor<",  implName , ", int?>(\r\n");
this.WriteObjects("						lazyCtx,\r\n");
this.WriteObjects("						null,\r\n");
this.WriteObjects("						\"",  posColumnName , "\",\r\n");
this.WriteObjects("						null,\r\n");
this.WriteObjects("						obj => obj.",  posColumnName , ",\r\n");
this.WriteObjects("						(obj, val) => obj.",  posColumnName , " = val),\r\n");
#line 105 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
}
			
				if (rel.B.Type == cls && rel.B.HasPersistentOrder) 
				{
					var posColumnName = Construct.ListPositionPropertyName(rel.B);

#line 111 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("					// rel.B.Type == cls && rel.B.HasPersistentOrder\r\n");
this.WriteObjects("					new CustomPropertyDescriptor<",  implName , ", int?>(\r\n");
this.WriteObjects("						lazyCtx,\r\n");
this.WriteObjects("						null,\r\n");
this.WriteObjects("						\"",  posColumnName , "\",\r\n");
this.WriteObjects("						null,\r\n");
this.WriteObjects("						obj => obj.",  posColumnName , ",\r\n");
this.WriteObjects("						(obj, val) => obj.",  posColumnName , " = val),\r\n");
#line 120 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
}
			}
		}

#line 124 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("				};\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("		\r\n");
this.WriteObjects("		protected override void CollectProperties(Func<IReadOnlyKistlContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			base.CollectProperties(props);\r\n");
this.WriteObjects("			_InitializePropertyDescriptors(lazyCtx);\r\n");
this.WriteObjects("			props.AddRange(_properties);\r\n");
this.WriteObjects("		}\r\n");
#line 135 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
}

#line 137 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("	\r\n");

        }



    }
}