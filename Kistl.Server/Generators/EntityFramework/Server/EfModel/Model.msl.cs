using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Server.EfModel
{
    public partial class ModelMsl
    {
        protected virtual void ApplyEntityTypeMapping(ObjectClass obj)
        {
            CallTemplate("Server.EfModel.ModelMslEntityTypeMapping", obj);
        }

        protected virtual void ApplyCollectionEntryEntityTypeMapping(Property listProp)
        {
            CallTemplate("Server.EfModel.ModelMslCollectionEntryEntityTypeMapping", listProp);
        }
    }
}
