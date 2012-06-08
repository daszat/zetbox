using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Templates.CollectionEntries;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst")]
    public partial class CollectionEntries : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.CollectionEntries", ctx);
        }

        public CollectionEntries(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("");
#line 32 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("// <autogenerated/>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using System;\r\n");
this.WriteObjects("using System.Collections;\r\n");
this.WriteObjects("using System.Collections.Generic;\r\n");
this.WriteObjects("using System.ComponentModel;\r\n");
this.WriteObjects("using System.Xml;\r\n");
this.WriteObjects("using System.Xml.Serialization;\r\n");
this.WriteObjects("using System.Linq;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Zetbox.API;\r\n");
this.WriteObjects("using Zetbox.DalProvider.Base.RelationWrappers;\r\n");
#line 45 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
foreach(string ns in GetAdditionalImports().Distinct().OrderBy(s => s))
    {

#line 48 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("using ",  ns , ";\r\n");
#line 50 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
}

	foreach (var rel in ctx.GetQuery<Relation>()
	    .Where(r => r.Storage == StorageType.Separate)
	    .ToList()
	    .OrderBy(r => r.GetRelationClassName()))
	{


#line 59 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  rel.Module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
#line 63 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
RelationEntry.Call(Host, ctx, rel);

#line 65 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("}\r\n");
#line 67 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
}

#line 70 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
foreach (var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .ToList() // NHibernate-on-linux workaround
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
	{


#line 79 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  prop.GetCollectionEntryNamespace() , "\r\n");
this.WriteObjects("{\r\n");
#line 83 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
ValueCollectionEntry.Call(Host, ctx, prop);

#line 85 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("}\r\n");
#line 87 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
}

#line 90 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
foreach (var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .ToList() // NHibernate-on-linux workaround
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
	{

#line 98 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  prop.GetCollectionEntryNamespace() , "\r\n");
this.WriteObjects("{\r\n");
#line 102 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
ValueCollectionEntry.Call(Host, ctx, prop);

#line 104 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("}\r\n");
#line 106 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\CollectionEntries.cst"
}


        }

    }
}