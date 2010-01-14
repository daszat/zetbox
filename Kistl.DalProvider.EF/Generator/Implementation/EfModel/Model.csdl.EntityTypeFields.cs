using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.DalProvider.EF.Generator.Implementation.EfModel
{
    public partial class ModelCsdlEntityTypeFields
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, IEnumerable<Property> properties)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.EfModel.ModelCsdlEntityTypeFields", ctx, properties);
        }
    }
}
