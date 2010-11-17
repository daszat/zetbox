
namespace Kistl.Generator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public static class ContextExtensions
    {
        public static IQueryable<ObjectClass> GetBaseClasses(this IKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass == null);
        }

        public static IQueryable<ObjectClass> GetDerivedClasses(this IKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass != null);
        }

        public static IEnumerable<Relation> GetRelationsWithSeparateStorage(this IKistlContext ctx)
        {
            return ctx.GetQuery<Relation>()
                .Where(r => r.Storage == StorageType.Separate)
                .ToList()
                .OrderBy(r => r.GetAssociationName());
        }

        public static IEnumerable<Relation> GetRelationsWithoutSeparateStorage(this IKistlContext ctx)
        {
            return ctx.GetQuery<Relation>()
                .Where(r => r.Storage != StorageType.Separate)
                .ToList()
                .OrderBy(r => r.GetAssociationName());
        }
    }
}
