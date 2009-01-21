using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelCsdl
    {
        protected virtual void ApplyEntityTypeFieldDefs(IEnumerable<Property> properties)
        {
            CallTemplate("Implementation.EfModel.ModelCsdlEntityTypeFields", ctx, properties);
        }

        protected virtual void ApplyAssociationSetTemplate(AssociationInfo info)
        {
            CallTemplate("Implementation.EfModel.ModelCsdlAssociationSet", ctx, info);
        }

        protected virtual void ApplyAssociationSets()
        {
            foreach (var prop in this.ctx.GetAssociationPropertiesWithStorage())
            {
                var info = AssociationInfo.CreateInfo(ctx, prop);
                ApplyAssociationSetTemplate(info);

                var reverse = info.GetReverse();
                if (reverse != null)
                {
                    ApplyAssociationSetTemplate(reverse);
                }
            }
        }
    }
}
