using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.GeneratorsOld;
using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Server.EfModel
{
    public partial class ModelCsdl
    {
        protected virtual void ApplyEntityTypeFieldDefs(IEnumerable<Property> properties)
        {
            CallTemplate("Server.EfModel.ModelCsdlEntityTypeFields", properties);
        }
    }
}
