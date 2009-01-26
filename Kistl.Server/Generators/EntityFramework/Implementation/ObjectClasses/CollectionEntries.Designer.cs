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
this.WriteObjects("using System.Data.Metadata.Edm;\r\n");
this.WriteObjects("using System.Data.Objects.DataClasses;\r\n");
this.WriteObjects("using System.Xml;\r\n");
this.WriteObjects("using System.Xml.Serialization;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Kistl.API;\r\n");
this.WriteObjects("using Kistl.DALProvider.EF;\r\n");
this.WriteObjects("\r\n");
#line 27 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
foreach (var rel in NewRelation.GetAll(ctx)
		.Where(rel => rel.GetPreferredStorage() == StorageHint.Separate)
		.OrderBy(rel => rel.GetAssociationName()))
	{


#line 33 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("\r\n");
this.WriteObjects("namespace ",  rel.Right.Referenced.Namespace , "\r\n");
this.WriteObjects("{\r\n");
#line 37 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
if (rel.Left.Referenced.Namespace != rel.Right.Referenced.Namespace)
		{

#line 40 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("	using ",  rel.Left.Referenced.Namespace , ";\r\n");
#line 42 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
}

		this.CallTemplate("Implementation.ObjectClasses.CollectionEntry", ctx, rel);

#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
this.WriteObjects("}\r\n");
#line 49 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntries.cst"
}


        }



    }
}