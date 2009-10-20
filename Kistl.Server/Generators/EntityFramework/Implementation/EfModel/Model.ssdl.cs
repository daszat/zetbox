
namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public partial class ModelSsdl
    {
        protected static bool HasStorage(ObjectReferenceProperty p)
        {
            Relation rel = RelationExtensions.Lookup(p.Context, p);
            RelationEnd relEnd = rel.GetEnd(p);
            return rel.HasStorage(relEnd.GetRole());
        }

        protected static IEnumerable<P> RetrieveAndSortPropertiesOfType<P>(IEnumerable<Property> properties, Func<P, bool> predicate)
            where P : Property
        {
            return properties.OfType<P>()
                .OrderBy(p => p.ObjectClass.ClassName)
                .ThenBy(p => p.PropertyName)
                .Where(p => predicate(p));
        }

        protected virtual void ApplyEntityTypeColumnDefs<P>(IEnumerable<P> properties)
            where P : Property
        {
            Implementation.EfModel.ModelSsdlEntityTypeColumns.Call(Host, ctx, properties.Cast<Property>(), "");
        }
    }
}
