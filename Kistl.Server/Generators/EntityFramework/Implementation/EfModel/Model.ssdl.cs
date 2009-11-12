
namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using System.Collections;

    public partial class ModelSsdl
    {
        // ContextBound Objects are not allowed to have Generic Methods
        // See MSDN: http://msdn.microsoft.com/en-us/library/system.contextboundobject.aspx
        private static class ModelSsdlHelper
        {
            public static IEnumerable<P> RetrieveAndSortPropertiesOfType<P>(IEnumerable<Property> properties, Func<P, bool> predicate)
                where P : Property
            {
                return properties.OfType<P>()
                    .OrderBy(p => p.ObjectClass.ClassName)
                    .ThenBy(p => p.PropertyName)
                    .Where(p => predicate(p));
            }
        }

        protected static bool HasStorage(ObjectReferenceProperty p)
        {
            Relation rel = RelationExtensions.Lookup(p.Context, p);
            RelationEnd relEnd = rel.GetEnd(p);
            return rel.HasStorage(relEnd.GetRole());
        }

        protected virtual void ApplyEntityTypeColumnDefs(IEnumerable properties)
        {
            Implementation.EfModel.ModelSsdlEntityTypeColumns.Call(Host, ctx, properties.Cast<Property>(), "");
        }
    }

}
