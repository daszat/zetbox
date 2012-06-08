
namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;

    public partial class ModelSsdlEntityTypeColumns
    {
        protected virtual void ApplyEntityTypeColumnDefs(IEnumerable<Property> properties, string prefix, ISchemaProvider schemaProvider)
        {
            Call(Host, ctx, properties, prefix, schemaProvider);
        }
    }
}
