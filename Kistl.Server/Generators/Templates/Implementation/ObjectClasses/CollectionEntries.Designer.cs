using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst")]
    public partial class CollectionEntries : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;


        public CollectionEntries(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("using System;\r\n");
this.WriteObjects("using System.Collections;\r\n");
this.WriteObjects("using System.Collections.Generic;\r\n");
this.WriteObjects("using System.Xml;\r\n");
this.WriteObjects("using System.Xml.Serialization;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Kistl.API;\r\n");
#line 24 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
foreach(string ns in GetAdditionalImports())
    {

#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("    using ",  ns , ";\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
}

	foreach (var rel in ctx.GetQuery<Relation>()
	    .Where(r => (int)r.Storage == (int)StorageType.Separate)
	    .ToList()
	    .OrderBy(r => r.GetCollectionEntryClassName()))
	{


#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  rel.A.Type.Module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
if (rel.A.Type.Module.Namespace != rel.B.Type.Module.Namespace)
		{

#line 45 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("	using ",  rel.B.Type.Module.Namespace , ";\r\n");
#line 47 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
}

		this.CallTemplate("Implementation.CollectionEntries.ObjectCollectionEntry", ctx, rel);

#line 51 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("}\r\n");
#line 53 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
}


	foreach (var prop in ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.ClassName)
        .OrderBy(p => p.PropertyName))
	{


#line 63 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  prop.ObjectClass.Module.Namespace , "\r\n");
this.WriteObjects("{\r\n");
#line 67 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
this.CallTemplate("Implementation.CollectionEntries.ValueCollectionEntry", ctx, prop);

#line 69 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("}\r\n");
#line 71 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CollectionEntries.cst"
}


        }



    }
}