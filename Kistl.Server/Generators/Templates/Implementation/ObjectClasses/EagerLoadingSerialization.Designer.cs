using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst")]
    public partial class EagerLoadingSerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string collectionName;


        public EagerLoadingSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.collectionName = collectionName;

        }
        
        public override void Generate()
        {
#line 20 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
switch(direction)
	{
		case SerializerDirection.ToStream:

#line 24 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
this.WriteObjects("			{\r\n");
this.WriteObjects("				foreach(var obj in ",  collectionName , ")\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					auxObjects.Add(obj);\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("			}\r\n");
#line 31 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EagerLoadingSerialization.cst"
break;
		case SerializerDirection.FromStream:
			break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}