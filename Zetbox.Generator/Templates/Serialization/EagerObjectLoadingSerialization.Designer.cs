using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerObjectLoadingSerialization.cst")]
    public partial class EagerObjectLoadingSerialization : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string propertyName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string propertyName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.EagerObjectLoadingSerialization", ctx, direction, streamName, xmlnamespace, xmlname, propertyName);
        }

        public EagerObjectLoadingSerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string propertyName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.xmlnamespace = xmlnamespace;
			this.xmlname = xmlname;
			this.propertyName = propertyName;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerObjectLoadingSerialization.cst"
this.WriteObjects("");
#line 36 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerObjectLoadingSerialization.cst"
switch(direction)
    {
        case SerializerDirection.ToStream:

#line 40 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerObjectLoadingSerialization.cst"
this.WriteObjects("            if (auxObjects != null) {\n");
this.WriteObjects("                auxObjects.Add(",  propertyName , ");\n");
this.WriteObjects("            }\n");
#line 44 "P:\zetbox\Zetbox.Generator\Templates\Serialization\EagerObjectLoadingSerialization.cst"
break;
        case SerializerDirection.FromStream:
            break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }


        }

    }
}