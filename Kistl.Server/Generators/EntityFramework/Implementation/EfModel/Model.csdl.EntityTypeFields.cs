using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelCsdlEntityTypeFields
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, IEnumerable<Property> properties)
        {
            host.CallTemplate("Implementation.EfModel.ModelCsdlEntityTypeFields", ctx, properties);
        }
    }
}
