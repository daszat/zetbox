using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst")]
    public partial class CompoundObjectSerialization : Kistl.Generator.ResourceTemplate
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
#line 20 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
if (direction == SerializerDirection.ToStream)
	{

#line 23 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			BinarySerializer.ToStream(this.",  memberName , ", ",  streamName , ");\r\n");
#line 25 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.FromStream)
	{

#line 29 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			{\r\n");
this.WriteObjects("				// trick compiler into generating correct temporary variable\r\n");
this.WriteObjects("				var tmp = this.",  backingStoreName , ";\r\n");
this.WriteObjects("				BinarySerializer.FromStream(out tmp, ",  streamName , ");\r\n");
this.WriteObjects("				// use setter to de-/attach everything correctly\r\n");
this.WriteObjects("	            this.",  backingStoreName , " = tmp;\r\n");
this.WriteObjects("	        }\r\n");
#line 37 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.ToXmlStream)
	{

#line 41 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			// TODO: Add XML Serializer here\r\n");
#line 43 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.FromXmlStream)
	{

#line 47 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            // TODO: Add XML Serializer here\r\n");
#line 49 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.Export)
	{

#line 53 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 55 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.MergeImport)
	{

#line 59 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			XmlStreamer.FromStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 61 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else
	{
		throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}