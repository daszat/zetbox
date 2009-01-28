using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst")]
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
#line 17 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("using System.Collections;\r\n");
this.WriteObjects("using System.Collections.Generic;\r\n");
this.WriteObjects("using System.Data.Metadata.Edm;\r\n");
this.WriteObjects("using System.Data.Objects.DataClasses;\r\n");
this.WriteObjects("using System.Xml;\r\n");
this.WriteObjects("using System.Xml.Serialization;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Kistl.API;\r\n");
this.WriteObjects("using Kistl.DALProvider.EF;\r\n");
this.WriteObjects("\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
foreach (var rel in NewRelation.GetAll(ctx)
		.Where(rel => rel.GetPreferredStorage() == StorageHint.Separate)
		.OrderBy(rel => rel.GetAssociationName()))
	{


#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  rel.A.Type.Namespace , "\r\n");
this.WriteObjects("{\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
if (rel.A.Type.Namespace != rel.B.Type.Namespace)
		{

#line 42 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("	using ",  rel.B.Type.Namespace , ";\r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
}

		this.CallTemplate("Implementation.ObjectClasses.CollectionEntry", ctx, rel);

#line 48 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("}\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
}


        }



    }
}