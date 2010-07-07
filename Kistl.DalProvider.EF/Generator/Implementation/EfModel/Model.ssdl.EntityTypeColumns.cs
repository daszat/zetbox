
namespace Kistl.DalProvider.EF.Generator.Implementation.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;

    public partial class ModelSsdlEntityTypeColumns
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, IEnumerable<Property> properties, string prefix, ISchemaProvider schemaProvider)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.EfModel.ModelSsdlEntityTypeColumns", ctx, properties, prefix, schemaProvider);
        }

        protected virtual void ApplyEntityTypeColumnDefs(IEnumerable<Property> properties, string prefix, ISchemaProvider schemaProvider)
        {
            Call(Host, ctx, properties, prefix, schemaProvider);
        }
    }
}
