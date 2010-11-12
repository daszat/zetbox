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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\ObjectClasses\ReloadReferences.cst")]
    public partial class ReloadReferences : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;


        public ReloadReferences(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ReloadReferences()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            // Do not reload references if the current object has been deleted.\r\n");
this.WriteObjects("            // TODO: enable when MemoryContext uses MemoryDataObjects\r\n");
this.WriteObjects("            //if (this.ObjectState == DataObjectState.Deleted) return;\r\n");
#line 22 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\ReloadReferences.cst"
if (cls.BaseObjectClass != null) {

#line 24 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("            base.ReloadReferences();\r\n");
this.WriteObjects("\r\n");
#line 27 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\ReloadReferences.cst"
}

#line 29 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("            // fix direct object references\r\n");
#line 31 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\ReloadReferences.cst"
foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>()
        .Where(orp => !orp.IsList())
        .OrderBy(orp => orp.ObjectClass.Name)
        .ThenBy(orp => orp.Name))
    {
        Relation rel = Kistl.App.Extensions.RelationExtensions.Lookup(ctx, prop);
        RelationEnd relEnd = rel.GetEnd(prop);
        RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

        if (otherEnd.Type.ImplementsIExportable()) {
            string referencedInterface = otherEnd.Type.GetDataTypeString();
            string referencedImplementation = otherEnd.Type.GetDataTypeString() + ImplementationSuffix;
            string name = prop.Name;
            string implName = name + Kistl.API.Helper.ImplementationSuffix;
            string fkBackingName = "_fk_" + name;
            string fkGuidBackingName = "_fk_guid_" + name;
            bool isExportable = relEnd.Type.ImplementsIExportable() && otherEnd.Type.ImplementsIExportable();

            ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, name, implName, fkBackingName, fkGuidBackingName, isExportable);
        }
    }

#line 53 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("        }\r\n");

        }



    }
}