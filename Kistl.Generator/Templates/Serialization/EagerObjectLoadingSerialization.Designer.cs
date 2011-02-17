using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/EagerObjectLoadingSerialization.cst")]
    public partial class EagerObjectLoadingSerialization : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string xmlnamespace;
		protected string xmlname;
		protected string propertyName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string propertyName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.EagerObjectLoadingSerialization", ctx, direction, streamName, xmlnamespace, xmlname, propertyName);
        }

        public EagerObjectLoadingSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string propertyName)
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
#line 20 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/EagerObjectLoadingSerialization.cst"
switch(direction)
	{
		case SerializerDirection.ToStream:

#line 24 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/EagerObjectLoadingSerialization.cst"
this.WriteObjects("			if (auxObjects != null) {\r\n");
this.WriteObjects("				auxObjects.Add(",  propertyName , ");\r\n");
this.WriteObjects("			}\r\n");
#line 28 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Serialization/EagerObjectLoadingSerialization.cst"
break;
		case SerializerDirection.FromStream:
			break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }

    }
}