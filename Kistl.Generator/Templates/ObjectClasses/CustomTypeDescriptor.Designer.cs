using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst")]
    public partial class CustomTypeDescriptor : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;
		protected string ifName;
		protected string implName;
		protected string propertyDescriptorName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls, string ifName, string implName, string propertyDescriptorName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.CustomTypeDescriptor", ctx, cls, ifName, implName, propertyDescriptorName);
        }

        public CustomTypeDescriptor(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls, string ifName, string implName, string propertyDescriptorName)
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
#line 18 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("        #region ",  this.GetType() , "\r\n");
#line 20 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
var properties = cls.Properties.OrderBy(p => p.Name).ToList();
    var rels = cls.GetRelations()
        .OrderBy(i => i.A.RoleName).ThenBy(i => i.Verb).ThenBy(i => i.B.RoleName)
        .OrderBy(i => i.A.Type.Name).ThenBy(i => i.B.Type.Name)
        .ThenBy(i => i.ExportGuid)
        .ToList();

    if (properties.Count > 0 || rels.Count > 0)
    {

#line 30 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
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
#line 43 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
foreach(var property in properties)
        {
            string propertyName = property.Name;
            if (property.IsAssociation() && !property.IsObjectReferencePropertySingle())
            {

#line 49 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()\r\n");
this.WriteObjects("                    new ",  propertyDescriptorName , "<",  ifName , ", ",  property.GetPropertyTypeString() , ">(\r\n");
this.WriteObjects("                        lazyCtx,\r\n");
this.WriteObjects("                        new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("                        \"",  propertyName , "\",\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("                        null, // lists are read-only properties\r\n");
this.WriteObjects("                        obj => ((",  implName , ")obj).On",  propertyName , "_IsValid), \r\n");
#line 58 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} else if (property is CalculatedObjectReferenceProperty) { 
#line 59 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // property is CalculatedObjectReferenceProperty\r\n");
this.WriteObjects("                    new ",  propertyDescriptorName , "<",  ifName , ", ",  property.GetPropertyTypeString() , ">(\r\n");
this.WriteObjects("                        lazyCtx,\r\n");
this.WriteObjects("                        new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("                        \"",  propertyName , "\",\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        obj => obj.",  propertyName , ",\r\n");
this.WriteObjects("                        null, // CalculatedObjectReferenceProperty is a read-only property\r\n");
this.WriteObjects("						obj => ((",  implName , ")obj).On",  propertyName , "_IsValid),  \r\n");
#line 68 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} else { 
#line 69 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
var isReadonly = (property is ValueTypeProperty) && ((ValueTypeProperty)property).IsCalculated; 
#line 70 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // else\r\n");
this.WriteObjects("                    new ",  propertyDescriptorName , "<",  ifName , ", ",  property.GetPropertyTypeString() , ">(\r\n");
this.WriteObjects("                        lazyCtx,\r\n");
this.WriteObjects("                        new Guid(\"",  property.ExportGuid , "\"),\r\n");
this.WriteObjects("                        \"",  propertyName , "\",\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        obj => ((",  implName , ")obj).",  propertyName , ",\r\n");
#line 77 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
if(isReadonly) { 
#line 78 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                        null, // calculated property\r\n");
this.WriteObjects("						obj => ((",  implName , ")obj).On",  propertyName , "_IsValid), \r\n");
#line 80 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} else { 
#line 81 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                        (obj, val) => obj.",  propertyName , " = val,\r\n");
this.WriteObjects("						obj => ((",  implName , ")obj).On",  propertyName , "_IsValid), \r\n");
#line 83 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} 
#line 84 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} 
#line 85 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} 
#line 86 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
if ("Frozen".Equals(Settings["extrasuffix"])) 
#line 87 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
{ 
#line 88 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // skipping position columns for frozen context (not implemented)\r\n");
#line 89 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} else { 
#line 90 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // position columns\r\n");
#line 92 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
foreach(var rel in rels.Where(r => r.GetRelationType() == RelationType.one_n))
            {
            // only show debugging if there actually is an position column
            if (   (rel.A.Type == cls && rel.A.HasPersistentOrder)
                || (rel.B.Type == cls && rel.B.HasPersistentOrder))
            {

#line 99 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // rel: ",  rel.A.RoleName , " ",  rel.Verb , " ",  rel.B.RoleName , " (",  rel.ExportGuid , ")\r\n");
#line 101 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
}
            if (rel.A.Type == cls && rel.A.HasPersistentOrder)
            {
                var posColumnName = Construct.ListPositionPropertyName(rel.A);

#line 106 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // rel.A.Type == cls && rel.A.HasPersistentOrder\r\n");
this.WriteObjects("                    new ",  propertyDescriptorName , "<",  implName , ", int?>(\r\n");
this.WriteObjects("                        lazyCtx,\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        \"",  posColumnName , "\",\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        obj => obj.",  posColumnName , ",\r\n");
this.WriteObjects("                        (obj, val) => obj.",  posColumnName , " = val,\r\n");
this.WriteObjects("						null),\r\n");
#line 116 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
}

                if (rel.B.Type == cls && rel.B.HasPersistentOrder)
                {
                    var posColumnName = Construct.ListPositionPropertyName(rel.B);

#line 122 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("                    // rel.B.Type == cls && rel.B.HasPersistentOrder\r\n");
this.WriteObjects("                    new ",  propertyDescriptorName , "<",  implName , ", int?>(\r\n");
this.WriteObjects("                        lazyCtx,\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        \"",  posColumnName , "\",\r\n");
this.WriteObjects("                        null,\r\n");
this.WriteObjects("                        obj => obj.",  posColumnName , ",\r\n");
this.WriteObjects("                        (obj, val) => obj.",  posColumnName , " = val,\r\n");
this.WriteObjects("						null),\r\n");
#line 132 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
}
            }
        }

#line 136 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
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
#line 146 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
} 
#line 147 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CustomTypeDescriptor.cst"
this.WriteObjects("        #endregion // ",  this.GetType() , "\r\n");

        }

    }
}