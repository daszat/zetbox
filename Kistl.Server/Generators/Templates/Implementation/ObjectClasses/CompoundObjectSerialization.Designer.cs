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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst")]
    public partial class CompoundObjectSerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberName;


        public CompoundObjectSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.memberName = memberName;

        }
        
        public override void Generate()
        {
#line 20 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
string backingName = memberName + Kistl.API.Helper.ImplementationSuffix;
	
	if (direction == SerializerDirection.ToStream)
	{

#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
this.WriteObjects("			BinarySerializer.ToStream(this.",  memberName , ", ",  streamName , ");\r\n");
#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.FromStream)
	{

#line 31 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
this.WriteObjects("			{\r\n");
this.WriteObjects("				// trick compiler into generating correct temporary variable\r\n");
this.WriteObjects("				var tmp = this.",  backingName , ";\r\n");
this.WriteObjects("				BinarySerializer.FromStream(out tmp, ",  streamName , ");\r\n");
this.WriteObjects("				// use setter to de-/attach everything correctly\r\n");
this.WriteObjects("	            this.",  backingName , " = tmp;\r\n");
this.WriteObjects("	        }\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.ToXmlStream)
	{

#line 43 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
this.WriteObjects("			// TODO: Add XML Serializer here\r\n");
#line 45 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.FromXmlStream)
	{

#line 49 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
this.WriteObjects("			// TODO: Add XML Serializer here\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
}
	else
	{
		throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}