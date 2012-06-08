
namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.App.Base;
    using Templates = Zetbox.Generator.Templates;

    public partial class ModelMsl
    {
        protected virtual void ApplyEntityTypeMapping(ObjectClass obj)
        {
            EfModel.ModelMslEntityTypeMapping.Call(Host, ctx, obj);
        }
    }
}
