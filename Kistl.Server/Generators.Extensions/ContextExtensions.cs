using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.Extensions
{
    public static class ContextExtensions
    {

        public static IQueryable<ObjectClass> GetBaseClasses(this IKistlContext ctx)
        {
            return ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass == null);
        }

        public static IQueryable<ObjectClass> GetDerivedClasses(this IKistlContext ctx)
        {
            return ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass != null);
        }

        public static IQueryable<ObjectReferenceProperty> GetObjectReferencePropertiesWithStorage(this IKistlContext ctx)
        {
            return ctx.GetQuery<ObjectReferenceProperty>()
                .Where(prop => prop.ObjectClass is ObjectClass)
                .ToList() // TODO: once HasStorage is no extension method anymore, delete this line and combine the WHERE
                .Where(prop => prop.HasStorage())
                .AsQueryable();
        }

        public static IQueryable<Property> GetObjectListPropertiesWithStorage(this IKistlContext ctx)
        {
            return ctx.GetQuery<Property>()
                .Where(prop => prop.ObjectClass is ObjectClass && prop.IsList)
                .ToList() // TODO: once HasStorage is no extension method anymore, delete this line and combine the WHERE
                .Where(prop => prop.HasStorage())
                .AsQueryable();
        }

        public static IQueryable<Property> GetAssociationPropertiesWithStorage(this IKistlContext ctx)
        {
            // convoluted definition to re-use the more specific but overlapping GetObject* sets.
            return ctx.GetObjectReferencePropertiesWithStorage().Cast<Property>()
                .Concat(ctx.GetObjectListPropertiesWithStorage())
                .Distinct()
                .OrderBy(p => p.ObjectClass.ClassName + p.PropertyName);
        }
    }
}
