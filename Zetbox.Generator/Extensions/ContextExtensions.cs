
namespace Zetbox.Generator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public static class ContextExtensions
    {
        public static IQueryable<ObjectClass> GetBaseClasses(this IZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass == null);
        }

        public static IQueryable<ObjectClass> GetDerivedClasses(this IZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass != null);
        }

        public static IEnumerable<Relation> GetRelationsWithSeparateStorage(this IZetboxContext ctx)
        {
            return ctx.GetQuery<Relation>()
                .Where(r => r.Storage == StorageType.Separate)
                .ToList()
                .OrderBy(r => r.GetAssociationName());
        }

        public static IEnumerable<Relation> GetRelationsWithoutSeparateStorage(this IZetboxContext ctx)
        {
            return ctx.GetQuery<Relation>()
                .Where(r => r.Storage != StorageType.Separate)
                .ToList()
                .OrderBy(r => r.GetAssociationName());
        }
    }
}
