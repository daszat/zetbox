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
		protected string memberType;
		protected string memberName;
		protected string backingStoreType;
		protected string backingStoreName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string backingStoreType, string backingStoreName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.CompoundObjectSerialization", ctx, direction, streamName, xmlnamespace, xmlname, memberType, memberName, backingStoreType, backingStoreName);
        }

        public CompoundObjectSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string backingStoreType, string backingStoreName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.memberType = memberType;
			this.memberName = memberName;
			this.backingStoreType = backingStoreType;
			this.backingStoreName = backingStoreName;

        }

        public override void Generate()
        {
#line 22 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
if (direction == SerializerDirection.ToStream)
	{

#line 25 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			BinarySerializer.ToStream(this.",  memberName , ", ",  streamName , ");\r\n");
#line 27 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.FromStream)
	{

#line 31 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			{\r\n");
this.WriteObjects("                // use backing store to avoid notifications\r\n");
this.WriteObjects("				",  backingStoreType , " tmp;\r\n");
this.WriteObjects("				BinarySerializer.FromStream(out tmp, ",  streamName , ");\r\n");
this.WriteObjects("	            this.",  backingStoreName , " = tmp;\r\n");
this.WriteObjects("                if (this.",  backingStoreName , " != null)\r\n");
this.WriteObjects("                    this.",  backingStoreName , ".AttachToObject(this, \"",  memberName , "\");\r\n");
this.WriteObjects("	        }\r\n");
#line 40 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.ToXmlStream)
	{

#line 44 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			// TODO: Add XML Serializer here\r\n");
#line 46 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.FromXmlStream)
	{

#line 50 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            // TODO: Add XML Serializer here\r\n");
#line 52 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.Export)
	{

#line 56 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 58 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == SerializerDirection.MergeImport)
	{

#line 62 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			XmlStreamer.FromStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 64 "P:\Kistl\Kistl.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else
	{
		throw new ArgumentOutOfRangeException("direction");
	}


        }

    }
}