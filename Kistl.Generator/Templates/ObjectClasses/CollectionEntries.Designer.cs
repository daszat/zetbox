using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Templates.CollectionEntries;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst")]
    public partial class CollectionEntries : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.CollectionEntries", ctx);
        }

        public CollectionEntries(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("// <autogenerated/>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using System;\r\n");
this.WriteObjects("using System.Collections;\r\n");
this.WriteObjects("using System.Collections.Generic;\r\n");
this.WriteObjects("using System.Xml;\r\n");
this.WriteObjects("using System.Xml.Serialization;\r\n");
this.WriteObjects("using System.Linq;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Kistl.API;\r\n");
this.WriteObjects("using Kistl.DalProvider.Base.RelationWrappers;\r\n");
#line 28 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
foreach(string ns in GetAdditionalImports().Distinct().OrderBy(s => s))
    {

#line 31 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 33 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
}

	foreach (var rel in ctx.GetQuery<Relation>()
	    .Where(r => r.Storage == StorageType.Separate)
	    .ToList()
	    .OrderBy(r => r.GetRelationClassName()))
	{


#line 42 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  rel.A.Type.Module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
#line 46 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
if (rel.A.Type.Module.Namespace != rel.B.Type.Module.Namespace)
		{

#line 49 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("	using ",  rel.B.Type.Module.Namespace , ";\r\n");
#line 51 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
}

		RelationEntry.Call(Host, ctx, rel);

#line 55 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("}\r\n");
#line 57 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
}

#line 60 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
foreach (var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .ToList() // NHibernate-on-linux workaround
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
	{


#line 69 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  prop.GetCollectionEntryNamespace() , "\r\n");
this.WriteObjects("{\r\n");
#line 73 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
ValueCollectionEntry.Call(Host, ctx, prop);

#line 75 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("}\r\n");
#line 77 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
}

#line 80 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
foreach (var prop in ctx.GetQuery<CompoundObjectProperty>()
        .Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
        .ToList() // NHibernate-on-linux workaround
        .OrderBy(p => p.ObjectClass.Name)
        .ThenBy(p => p.Name))
	{

#line 88 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  prop.GetCollectionEntryNamespace() , "\r\n");
this.WriteObjects("{\r\n");
#line 92 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
ValueCollectionEntry.Call(Host, ctx, prop);

#line 94 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("}\r\n");
#line 96 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\CollectionEntries.cst"
}


        }

    }
}