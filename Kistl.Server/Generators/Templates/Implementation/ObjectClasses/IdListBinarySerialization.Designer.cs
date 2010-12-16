using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\IdListBinarySerialization.cst")]
    public partial class IdListBinarySerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected string memberName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string memberName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Implementation.ObjectClasses.IdListBinarySerialization", ctx, direction, streamName, memberName);
        }

        public IdListBinarySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, string memberName)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.memberName = memberName;

        }

        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\IdListBinarySerialization.cst"
string methodName = direction.ToString();

#line 19 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\IdListBinarySerialization.cst"
this.WriteObjects("            BinarySerializer.",  methodName , "(this.",  memberName , ", ",  streamName , ");\r\n");

        }

    }
}