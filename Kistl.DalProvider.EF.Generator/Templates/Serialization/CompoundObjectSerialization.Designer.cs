using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst")]
    public partial class CompoundObjectSerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.Generator.Templates.Serialization.SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberType;
		protected string memberName;
		protected string backingStoreType;
		protected string backingStoreName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string backingStoreType, string backingStoreName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.CompoundObjectSerialization", ctx, direction, streamName, xmlnamespace, xmlname, memberType, memberName, backingStoreType, backingStoreName);
        }

        public CompoundObjectSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberType, string memberName, string backingStoreType, string backingStoreName)
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
#line 22 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
if (direction == Kistl.Generator.Templates.Serialization.SerializerDirection.ToStream)
	{

#line 25 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			",  streamName , ".Write(this.",  memberName , ");\r\n");
#line 27 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == Kistl.Generator.Templates.Serialization.SerializerDirection.FromStream)
	{

#line 31 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			{\r\n");
this.WriteObjects("                // use backing store to avoid notifications\r\n");
this.WriteObjects("				",  backingStoreType , " tmp;\r\n");
this.WriteObjects("				",  streamName , ".Read(out tmp);\r\n");
this.WriteObjects("	            this.",  backingStoreName , " = tmp ?? new ",  backingStoreType , "(true, this, \"",  memberName , "\");\r\n");
this.WriteObjects("                this.",  backingStoreName , ".AttachToObject(this, \"",  memberName , "\");\r\n");
this.WriteObjects("	        }\r\n");
#line 39 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == Kistl.Generator.Templates.Serialization.SerializerDirection.ToXmlStream)
	{

#line 43 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			// TODO: Add XML Serializer here\r\n");
#line 45 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == Kistl.Generator.Templates.Serialization.SerializerDirection.FromXmlStream)
	{

#line 49 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            // TODO: Add XML Serializer here\r\n");
#line 51 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == Kistl.Generator.Templates.Serialization.SerializerDirection.Export)
	{

#line 55 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 57 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else if (direction == Kistl.Generator.Templates.Serialization.SerializerDirection.MergeImport)
	{

#line 61 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
this.WriteObjects("			XmlStreamer.FromStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 63 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Serialization\CompoundObjectSerialization.cst"
}
	else
	{
		throw new ArgumentOutOfRangeException("direction");
	}


        }

    }
}