using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Arebis.CodeGeneration;
using Kistl.API;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelSsdlEntityTypeColumns
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, IEnumerable<Property> properties, string prefix)
        {
            host.CallTemplate("Implementation.EfModel.ModelSsdlEntityTypeColumns", ctx, properties, prefix);
        }

        protected virtual void ApplyEntityTypeColumnDefs(IEnumerable<Property> properties, string prefix)
        {
            Call(Host, ctx, properties, prefix);
        }
    }
}
