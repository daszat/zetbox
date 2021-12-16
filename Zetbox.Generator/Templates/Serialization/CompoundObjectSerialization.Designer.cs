using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst")]
    public partial class CompoundObjectSerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberType;
		protected string memberName;
		protected string backingStoreType;
		protected string backingStoreName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string backingStoreType, string backingStoreName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.CompoundObjectSerialization", ctx, direction, streamName, xmlnamespace, xmlname, memberType, memberName, backingStoreType, backingStoreName);
        }

        public CompoundObjectSerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string backingStoreType, string backingStoreName)
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
#line 38 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
if (direction == SerializerDirection.ToStream)
    {

#line 41 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(this.",  memberName , ");\r\n");
#line 43 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else if (direction == SerializerDirection.FromStream)
    {

#line 47 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            {\r\n");
this.WriteObjects("                // use backing store to avoid notifications\r\n");
this.WriteObjects("                this.",  backingStoreName , " = ",  streamName , ".ReadCompoundObject<",  backingStoreType , ">();\r\n");
this.WriteObjects("                this.",  backingStoreName , ".AttachToObject(this, \"",  memberName , "\");\r\n");
this.WriteObjects("            }\r\n");
#line 53 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else if (direction == SerializerDirection.Export)
    {

#line 57 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ExportCompoundObject(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 59 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else if (direction == SerializerDirection.MergeImport)
    {

#line 63 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                XmlStreamer.MergeImportCompoundObject(this.",  backingStoreName , ", ",  streamName , ");\r\n");
this.WriteObjects("                break;\r\n");
#line 67 "D:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else
    {
        throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}