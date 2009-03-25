using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Generators.Templates.Implementation;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\StructSerialization.cst")]
    public partial class StructSerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string memberName;


        public StructSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string memberName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.memberName = memberName;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\StructSerialization.cst"
string backingName = "_" + memberName;
	
	if (direction == SerializerDirection.ToStream)
	{

#line 23 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\StructSerialization.cst"
this.WriteObjects("			BinarySerializer.ToStream(this.",  memberName , ", ",  streamName , ");\r\n");
#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\StructSerialization.cst"
}
	else if (direction == SerializerDirection.FromStream)
	{

#line 29 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\StructSerialization.cst"
this.WriteObjects("			{\r\n");
this.WriteObjects("				// trick compiler into generating correct temporary variable\r\n");
this.WriteObjects("				var tmp = this.",  memberName , ";\r\n");
this.WriteObjects("				BinarySerializer.FromStream(out tmp, ",  streamName , ");\r\n");
this.WriteObjects("				// use setter to de-/attach everything correctly\r\n");
this.WriteObjects("	            this.",  memberName , " = tmp;\r\n");
this.WriteObjects("	        }\r\n");
#line 37 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\StructSerialization.cst"
}
	else
	{
		throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}