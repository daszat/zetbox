
namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public partial class ModelMsl
    {
        protected virtual void ApplyEntityTypeMapping(ObjectClass obj)
        {
            EfModel.ModelMslEntityTypeMapping.Call(Host, ctx, obj);
        }
    }
}
