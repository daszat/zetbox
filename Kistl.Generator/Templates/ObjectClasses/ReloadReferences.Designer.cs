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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ReloadReferences", ctx, cls);
        }

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
this.WriteObjects("            base.ReloadReferences();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            // fix direct object references\r\n");
#line 25 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\ReloadReferences.cst"
// TODO: Use only 1 side relation ends
    foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>()
        .Where(orp => !orp.IsList())
        .OrderBy(orp => orp.ObjectClass.Name)
        .ThenBy(orp => orp.Name))
    {
        Relation rel = Kistl.App.Extensions.RelationExtensions.Lookup(ctx, prop);
        RelationEnd relEnd = rel.GetEnd(prop);
        RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

        string referencedInterface = otherEnd.Type.GetDataTypeString();
        string referencedImplementation = otherEnd.Type.GetDataTypeString() + ImplementationSuffix;
        string name = prop.Name;
        string implName = name + Kistl.API.Helper.ImplementationSuffix;
        string fkBackingName = "_fk_" + name;
        string fkGuidBackingName = "_fk_guid_" + name;
        bool isExportable = relEnd.Type.ImplementsIExportable() && otherEnd.Type.ImplementsIExportable();

        ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, name, implName, fkBackingName, fkGuidBackingName, isExportable);
    }

#line 46 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("        }\r\n");

        }

    }
}