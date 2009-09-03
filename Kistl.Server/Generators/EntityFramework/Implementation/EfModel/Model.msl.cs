using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelMsl
    {
        protected virtual void ApplyEntityTypeMapping(ObjectClass obj)
        {
            Implementation.EfModel.ModelMslEntityTypeMapping.Call(Host, ctx, obj);
        }
    }
}
