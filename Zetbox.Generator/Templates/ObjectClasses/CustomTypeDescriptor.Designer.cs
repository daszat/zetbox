using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.SchemaManagement;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst")]
    public partial class CustomTypeDescriptor : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected ObjectClass cls;
		protected string ifName;
		protected string implName;
		protected string propertyDescriptorName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls, string ifName, string implName, string propertyDescriptorName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.CustomTypeDescriptor", ctx, cls, ifName, implName, propertyDescriptorName);
        }

        public CustomTypeDescriptor(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls, string ifName, string implName, string propertyDescriptorName)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;
			this.ifName = ifName;
			this.implName = implName;
			this.propertyDescriptorName = propertyDescriptorName;

        }

        public override void Generate()
        {
#line 35 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("        #region ",  this.GetType() , "\r\n");
#line 37 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
var properties = cls.Properties.OrderBy(p => p.Name).ToList();
    var rels = cls.GetRelations()
        .OrderBy(i => i.A.RoleName).ThenBy(i => i.Verb).ThenBy(i => i.B.RoleName)
        .OrderBy(i => i.A.Type.Name).ThenBy(i => i.B.Type.Name)
        .ThenBy(i => i.ExportGuid)
        .ToList();

    if (properties.Count > 0 || rels.Count > 0)
    {

#line 47 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("        private static readonly object _propertiesLock = new object();\r\n");
this.WriteObjects("        private static System.ComponentModel.PropertyDescriptor[] _properties;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private void _InitializePropertyDescriptors(Func<IFrozenContext> lazyCtx)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            if (_properties != null) return;\r\n");
this.WriteObjects("            lock (_propertiesLock)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // recheck for a lost race after aquiring the lock\r\n");
this.WriteObjects("                if (_properties != null) return;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                _properties = new System.ComponentModel.PropertyDescriptor[] {\r\n");
#line 60 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
foreach(var property in properties)
        {
            string propertyName = property.Name;
            if (property.IsAssociation().Result && !property.IsObjectReferencePropertySingle().Result)
            {

#line 66 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()\r\n");
this.WriteObjects("                    new ",  propertyDescriptorName , "<",  ifName , ", ",  property.GetPropertyTypeString().Result , ">(\r\n");
this.WriteObjects("                        lazyCtx,\r\n");
this.WriteObjects("                        new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("                        \"",  propertyName , "\",\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("                        null, // lists are read-only properties\r\n");
this.WriteObjects("                        obj => On",  propertyName , "_IsValid), \r\n");
#line 75 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} else if (property is CalculatedObjectReferenceProperty) { 
#line 76 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // property is CalculatedObjectReferenceProperty\r\n");
this.WriteObjects("                    new ",  propertyDescriptorName , "<",  ifName , ", ",  property.GetPropertyTypeString().Result , ">(\r\n");
this.WriteObjects("                        lazyCtx,\r\n");
this.WriteObjects("                        new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("                        \"",  propertyName , "\",\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("                        null, // CalculatedObjectReferenceProperty is a read-only property\r\n");
this.WriteObjects("						null), // no constraints on calculated properties \r\n");
#line 85 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} else { 
#line 86 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
var isReadonly = (property is ValueTypeProperty) && ((ValueTypeProperty)property).IsCalculated; 
#line 87 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // else\r\n");
this.WriteObjects("                    new ",  propertyDescriptorName , "<",  ifName , ", ",  property.GetPropertyTypeString().Result , ">(\r\n");
this.WriteObjects("                        lazyCtx,\r\n");
this.WriteObjects("                        new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("                        \"",  propertyName , "\",\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        obj => obj.",  propertyName , ",\r\n");
#line 94 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
if(isReadonly) { 
#line 95 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                        null, // calculated property\r\n");
this.WriteObjects("						null), // no constraints on calculated properties\r\n");
#line 97 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} else { 
#line 98 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                        (obj, val) => obj.",  propertyName , " = val,\r\n");
this.WriteObjects("						obj => On",  propertyName , "_IsValid), \r\n");
#line 100 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} 
#line 101 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} 
#line 102 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} 
#line 103 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
if ("Frozen".Equals(Settings["extrasuffix"])) 
#line 104 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
{ 
#line 105 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // skipping position columns for frozen context (not implemented)\r\n");
#line 106 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} else { 
#line 107 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // position columns\r\n");
#line 109 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
foreach(var rel in rels.Where(r => r.GetRelationType().Result == RelationType.one_n))
            {
            // only show debugging if there actually is an position column
            if (   (rel.A.Type == cls && rel.A.HasPersistentOrder)
                || (rel.B.Type == cls && rel.B.HasPersistentOrder))
            {

#line 116 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // rel: ",  rel.A.RoleName , " ",  rel.Verb , " ",  rel.B.RoleName , " (",  rel.ExportGuid , ")\r\n");
#line 118 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
}
            if (rel.A.Type == cls && rel.A.HasPersistentOrder)
            {
                var posColumnName = Construct.ListPositionPropertyName(rel.A);

#line 123 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // rel.A.Type == cls && rel.A.HasPersistentOrder\r\n");
this.WriteObjects("                    new ",  propertyDescriptorName , "<",  implName , ", int?>(\r\n");
this.WriteObjects("                        lazyCtx,\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        \"",  posColumnName , "\",\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        obj => obj.",  posColumnName , ",\r\n");
this.WriteObjects("                        (obj, val) => obj.",  posColumnName , " = val,\r\n");
this.WriteObjects("						null),\r\n");
#line 133 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
}

                if (rel.B.Type == cls && rel.B.HasPersistentOrder)
                {
                    var posColumnName = Construct.ListPositionPropertyName(rel.B);

#line 139 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // rel.B.Type == cls && rel.B.HasPersistentOrder\r\n");
this.WriteObjects("                    new ",  propertyDescriptorName , "<",  implName , ", int?>(\r\n");
this.WriteObjects("                        lazyCtx,\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        \"",  posColumnName , "\",\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        obj => obj.",  posColumnName , ",\r\n");
this.WriteObjects("                        (obj, val) => obj.",  posColumnName , " = val,\r\n");
this.WriteObjects("						null),\r\n");
#line 149 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
}
            }
        }

#line 153 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                };\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        protected override void CollectProperties(Func<IFrozenContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.CollectProperties(lazyCtx, props);\r\n");
this.WriteObjects("            _InitializePropertyDescriptors(lazyCtx);\r\n");
this.WriteObjects("            props.AddRange(_properties);\r\n");
this.WriteObjects("        }\r\n");
#line 163 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} 
#line 164 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("        #endregion // ",  this.GetType() , "\r\n");

        }

    }
}