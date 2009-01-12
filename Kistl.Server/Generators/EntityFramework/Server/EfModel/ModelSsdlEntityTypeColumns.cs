using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Server.EfModel
{
    public partial class ModelSsdlEntityTypeColumns
    {
        protected virtual void ApplyEntityTypeColumnDefs(IEnumerable<Property> properties, string prefix)
        {
            CallTemplate("Server.EfModel.ModelSsdlEntityTypeColumns", properties, prefix);
        }
    }
}
