using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst")]
    public partial class ReloadReferences : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected DataType cls;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType cls)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ReloadReferences", ctx, cls);
        }

        public ReloadReferences(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("");
#line 31 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("\n");
this.WriteObjects("        public override void ReloadReferences()\n");
this.WriteObjects("        {\n");
this.WriteObjects("            // Do not reload references if the current object has been deleted.\n");
this.WriteObjects("            // TODO: enable when MemoryContext uses MemoryDataObjects\n");
this.WriteObjects("            //if (this.ObjectState == DataObjectState.Deleted) return;\n");
this.WriteObjects("            base.ReloadReferences();\n");
this.WriteObjects("\n");
this.WriteObjects("            // fix direct object references\n");
#line 41 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
// TODO: Use only 1 side relation ends
    foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>()
        .Where(orp => !orp.IsList())
        .OrderBy(orp => orp.ObjectClass.Name)
        .ThenBy(orp => orp.Name))
    {
        Relation rel = Zetbox.App.Extensions.RelationExtensions.Lookup(ctx, prop);
        RelationEnd relEnd = rel.GetEnd(prop);
        RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

        string referencedInterface = otherEnd.Type.GetDataTypeString();
        string referencedImplementation = otherEnd.Type.GetDataTypeString() + ImplementationSuffix;
        string name = prop.Name;
        string implName = name + Zetbox.API.Helper.ImplementationSuffix;
        string fkBackingName = "_fk_" + name;
        string fkGuidBackingName = "_fk_guid_" + name;
        bool isExportable = relEnd.Type.ImplementsIExportable() && otherEnd.Type.ImplementsIExportable();

        ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, name, implName, fkBackingName, fkGuidBackingName, isExportable);
    }

#line 62 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("        }\n");

        }

    }
}