using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst")]
    public partial class CompoundObjectSerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberType;
		protected string memberName;
		protected string backingStoreType;
		protected string backingStoreName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string backingStoreType, string backingStoreName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.CompoundObjectSerialization", ctx, direction, streamName, xmlnamespace, xmlname, memberType, memberName, backingStoreType, backingStoreName);
        }

        public CompoundObjectSerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string backingStoreType, string backingStoreName)
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
#line 38 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
if (direction == Zetbox.Generator.Templates.Serialization.SerializerDirection.ToStream)
    {

#line 41 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            ",  streamName , ".Write(this.",  memberName , ");\r\n");
#line 43 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else if (direction == Zetbox.Generator.Templates.Serialization.SerializerDirection.FromStream)
    {

#line 47 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            {\r\n");
this.WriteObjects("                // use backing store to avoid notifications\r\n");
this.WriteObjects("                ",  backingStoreType , " tmp = ",  streamName , ".ReadCompoundObject<",  backingStoreType , ">();\r\n");
this.WriteObjects("                this.",  backingStoreName , " = tmp ?? new ",  backingStoreType , "(true, this, \"",  memberName , "\");\r\n");
this.WriteObjects("                this.",  backingStoreName , ".AttachToObject(this, \"",  memberName , "\");\r\n");
this.WriteObjects("            }\r\n");
#line 54 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else if (direction == Zetbox.Generator.Templates.Serialization.SerializerDirection.Export)
    {

#line 58 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 60 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else if (direction == Zetbox.Generator.Templates.Serialization.SerializerDirection.MergeImport)
    {

#line 64 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            case \"",  xmlnamespace , "|",  xmlname , "\":\r\n");
this.WriteObjects("                XmlStreamer.FromStream(this.",  backingStoreName , ", ",  streamName , ");\r\n");
this.WriteObjects("                break;\r\n");
#line 68 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
    else
    {
        throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}