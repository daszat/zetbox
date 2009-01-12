using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelCsdl
    {
        protected virtual void ApplyEntityTypeFieldDefs(IEnumerable<Property> properties)
        {
            CallTemplate("Implementation.EfModel.ModelCsdlEntityTypeFields", properties);
        }
    }
}
