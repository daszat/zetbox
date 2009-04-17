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
                this.WriteLine("        public override void ToStream(System.IO.BinaryWriter binStream)");
                this.WriteLine("        {");
                this.WriteLine("            throw new NotImplementedException();");
                this.WriteLine("        }");
            }
            else if (direction == SerializerDirection.FromStream)
            {
                this.WriteLine("        public override void FromStream(System.IO.BinaryReader binStream)");
                this.WriteLine("        {");
                this.WriteLine("            throw new NotImplementedException();");
                this.WriteLine("        }");
            }
            else if (direction == SerializerDirection.ToXmlStream)
            {
                this.WriteLine("        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)");
                this.WriteLine("        {");
                this.WriteLine("            throw new NotImplementedException();");
                this.WriteLine("        }");
            }
            else if (direction == SerializerDirection.FromXmlStream)
            {
                this.WriteLine("        public override void FromStream(System.Xml.XmlReader xml)");
                this.WriteLine("        {");
                this.WriteLine("            throw new NotImplementedException();");
                this.WriteLine("        }");
            }
        }
    }
}
