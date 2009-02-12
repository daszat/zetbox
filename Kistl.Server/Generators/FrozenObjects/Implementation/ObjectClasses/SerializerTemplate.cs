using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.FrozenObjects.Implementation.ObjectClasses
{
    public class SerializerTemplate
        : Templates.Implementation.ObjectClasses.SerializerTemplate
    {

        public SerializerTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, SerializationMembersList fields)
            : base(_host, ctx, direction, fields)
        {
        }

        public override void Generate()
        {
            if (direction == SerializerDirection.ToStream)
            {
                base.Generate();
            }
            else
            {
                this.WriteLine("        public override void FromStream(System.IO.BinaryReader binStream)");
                this.WriteLine("        {");
                this.WriteLine("            throw new NotImplementedException();");
                this.WriteLine("        }");
            }
        }
    }
}
