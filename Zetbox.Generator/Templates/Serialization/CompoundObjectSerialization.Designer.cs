using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst")]
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
#line 17 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("");
#line 38 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
if (direction == SerializerDirection.ToStream)
    {

#line 41 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(this.",  memberName , ");\n");
#line 43 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else if (direction == SerializerDirection.FromStream)
    {

#line 47 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            {\n");
this.WriteObjects("                // use backing store to avoid notifications\n");
this.WriteObjects("                this.",  backingStoreName , " = ",  streamName , ".ReadCompoundObject<",  backingStoreType , ">();\n");
this.WriteObjects("                if (this.",  backingStoreName , " != null)\n");
this.WriteObjects("                    this.",  backingStoreName , ".AttachToObject(this, \"",  memberName , "\");\n");
this.WriteObjects("            }\n");
#line 54 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else if (direction == SerializerDirection.Export)
    {

#line 58 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\n");
#line 60 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else if (direction == SerializerDirection.MergeImport)
    {

#line 64 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\n");
this.WriteObjects("                XmlStreamer.FromStream(this.",  backingStoreName , ", ",  streamName , ");\n");
this.WriteObjects("                break;\n");
#line 68 "P:\zetbox\Zetbox.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else
    {
        throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}