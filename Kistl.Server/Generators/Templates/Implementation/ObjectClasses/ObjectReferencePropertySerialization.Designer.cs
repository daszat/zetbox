using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ObjectReferencePropertySerialization.cst")]
    public partial class ObjectReferencePropertySerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string memberName;


        public ObjectReferencePropertySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string memberName)
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
#line 19 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ObjectReferencePropertySerialization.cst"
switch(direction)
	{
		case SerializerDirection.ToStream:

#line 23 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            BinarySerializer.ToStream(",  memberName , " != null ? ",  memberName , ".ID : (int?)null, ",  streamName , ");\r\n");
#line 25 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.FromStream:

#line 28 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            BinarySerializer.FromStream(out this._fk_",  memberName , ", ",  streamName , ");\r\n");
#line 30 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ObjectReferencePropertySerialization.cst"
break;
		case SerializerDirection.ToXmlStream:
			if(!string.IsNullOrEmpty(xmlname)) {

#line 34 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            XmlStreamer.ToStream(",  memberName , " != null ? ",  memberName , ".ExportGuid : (Guid?)null, ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 35 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ObjectReferencePropertySerialization.cst"
}
            break;
		case SerializerDirection.FromXmlStream:
			if(!string.IsNullOrEmpty(xmlname))

#line 40 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ObjectReferencePropertySerialization.cst"
this.WriteObjects("            XmlStreamer.FromStream(ref this._fk_",  memberName , ", ",  streamName , ", \"",  xmlname , "\", \"",  xmlnamespace , "\");\r\n");
#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ObjectReferencePropertySerialization.cst"
break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}