using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using System.CodeDom;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public class IdProperty
        : Templates.Implementation.ObjectClasses.IdProperty
    {

        public IdProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host, ctx)
        {
        }

        public override void Generate()
        {
            this.WriteLine("// ID is inherited");
        }
    }
}
