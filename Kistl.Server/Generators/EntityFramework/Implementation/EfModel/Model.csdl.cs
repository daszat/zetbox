using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelCsdl
    {
        protected virtual void ApplyEntityTypeFieldDefs(IEnumerable<Property> properties)
        {
            CallTemplate("Implementation.EfModel.ModelCsdlEntityTypeFields", ctx, properties);
        }

        protected virtual void ApplyAssociationSetTemplate(NewRelation rel)
        {
            CallTemplate("Implementation.EfModel.ModelCsdlAssociationSet", ctx, rel);
        }

        protected virtual void ApplyAssociationSets()
        {
            foreach (var rel in NewRelation.GetAll(ctx).OrderBy(r => r.GetAssociationName()))
            {
                ApplyAssociationSetTemplate(rel);
            }
        }

        protected virtual void ApplyAssociationTemplate(NewRelation rel)
        {
            CallTemplate("Implementation.EfModel.ModelCsdlAssociation", ctx, rel);
        }

        protected virtual void ApplyAssociations()
        {
            foreach (var rel in NewRelation.GetAll(ctx).OrderBy(r => r.GetAssociationName()))
            {
                ApplyAssociationTemplate(rel);
            }
        }
    }
}
