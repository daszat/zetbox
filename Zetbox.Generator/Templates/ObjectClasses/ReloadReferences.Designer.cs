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
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst")]
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
#line 31 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override async System.Threading.Tasks.Task ReloadReferences()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            // Do not reload references if the current object has been deleted.\r\n");
this.WriteObjects("            // TODO: enable when MemoryContext uses MemoryDataObjects\r\n");
this.WriteObjects("            //if (this.ObjectState == DataObjectState.Deleted) return;\r\n");
this.WriteObjects("            await base.ReloadReferences();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            // fix direct object references\r\n");
#line 41 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
// TODO: Use only 1 side relation ends
    foreach(var prop in GetDirectObjectReferences())
    {
        Relation rel = Zetbox.App.Extensions.RelationExtensions.Lookup(ctx, prop);
        RelationEnd relEnd = rel.GetEnd(prop).Result;
        RelationEnd otherEnd = rel.GetOtherEnd(relEnd).Result;

        string referencedInterface = otherEnd.Type.GetDataTypeString().Result;
        string referencedImplementation = otherEnd.Type.GetDataTypeString().Result + ImplementationSuffix;
        string name = prop.Name;
        string implName = name + Zetbox.API.Helper.ImplementationSuffix;
        string fkBackingName = "_fk_" + name;
        string fkGuidBackingName = "_fk_guid_" + name;
        bool isExportable = relEnd.Type.ImplementsIExportable().Result && otherEnd.Type.ImplementsIExportable().Result;

        ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, name, implName, fkBackingName, fkGuidBackingName, isExportable);
    }        

#line 59 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("            // fix cached lists references\r\n");
#line 61 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
foreach(var prop in GetListReferences())
    {
        string name = prop.Name;
        string taskName = "_triggerFetch" + name + "Task";
        string backingName = "_" + name;

#line 67 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("            ",  taskName , " = null;\r\n");
this.WriteObjects("            ",  backingName , " = null;\r\n");
#line 70 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
}

#line 72 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("        }\r\n");

        }

    }
}