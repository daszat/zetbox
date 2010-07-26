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
		protected string backingStoreName;


        public CompoundObjectSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberName, string backingStoreName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.memberName = memberName;
			this.backingStoreName = backingStoreName;

        }
        
        public override void Generate()
        {
#line 21 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
if (direction == SerializerDirection.ToStream)
	{

#line 24 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
this.WriteObjects("			BinarySerializer.ToStream(this.",  memberName , ", ",  streamName , ");\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.FromStream)
	{

#line 30 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
this.WriteObjects("			{\r\n");
this.WriteObjects("				// trick compiler into generating correct temporary variable\r\n");
this.WriteObjects("				var tmp = this.",  backingStoreName , ";\r\n");
this.WriteObjects("				BinarySerializer.FromStream(out tmp, ",  streamName , ");\r\n");
this.WriteObjects("				// use setter to de-/attach everything correctly\r\n");
this.WriteObjects("	            this.",  backingStoreName , " = tmp;\r\n");
this.WriteObjects("	        }\r\n");
#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.ToXmlStream)
	{

#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
this.WriteObjects("			// TODO: Add XML Serializer here\r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.FromXmlStream)
	{

#line 48 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
this.WriteObjects("            // TODO: Add XML Serializer here\r\n");
#line 50 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.Export)
	{

#line 54 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.MergeImport)
	{

#line 60 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
this.WriteObjects("			XmlStreamer.FromStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 62 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\CompoundObjectSerialization.cst"
}
	else
	{
		throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}