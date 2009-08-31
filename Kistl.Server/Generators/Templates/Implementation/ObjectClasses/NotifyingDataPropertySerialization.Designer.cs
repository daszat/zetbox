using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst")]
    public partial class NotifyingDataPropertySerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string backingStoreName;
		protected bool hasDefaultValue;
		protected string isSetFlagName;


        public NotifyingDataPropertySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string backingStoreName, bool hasDefaultValue, string isSetFlagName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.backingStoreName = backingStoreName;
			this.hasDefaultValue = hasDefaultValue;
			this.isSetFlagName = isSetFlagName;

        }
        
        public override void Generate()
        {
#line 21 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
switch(direction)
	{
		case SerializerDirection.ToStream:

#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(this.",  backingStoreName , ", ",  streamName , ");\r\n");
#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
break;
		case SerializerDirection.FromStream:
		    // use type-inference to get right "tmp" type

#line 31 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
this.WriteObjects("            {\r\n");
this.WriteObjects("                var tmp = this.",  backingStoreName , ";\r\n");
this.WriteObjects("                BinarySerializer.FromStream(out tmp, ",  streamName , ");\r\n");
this.WriteObjects("                this.",  backingStoreName , " = tmp;\r\n");
#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
if (hasDefaultValue) {

#line 38 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
this.WriteObjects("                this.",  isSetFlagName , " = true;\r\n");
#line 40 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
}

#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
this.WriteObjects("            }\r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
break;
		case SerializerDirection.ToXmlStream:

#line 47 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 49 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
break;
		case SerializerDirection.FromXmlStream:
		case SerializerDirection.MergeImport:
		    // use type-inference to get right "tmp" type

#line 54 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
this.WriteObjects("            {\r\n");
this.WriteObjects("                var tmp = this.",  backingStoreName , ";\r\n");
this.WriteObjects("                XmlStreamer.FromStream(ref tmp, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
this.WriteObjects("                this.",  backingStoreName , " = tmp;\r\n");
#line 59 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
if (hasDefaultValue) {

#line 61 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
this.WriteObjects("                this.",  isSetFlagName , " = true;\r\n");
#line 63 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
}

#line 65 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
this.WriteObjects("            }\r\n");
#line 67 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
break;
		case SerializerDirection.Export:

#line 69 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
this.WriteObjects("	\r\n");
this.WriteObjects("            if (modules.Contains(\"*\") || modules.Contains(\"",  xmlnamespace , "\")) XmlStreamer.ToStream(this.",  backingStoreName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 72 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\NotifyingDataPropertySerialization.cst"
break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}