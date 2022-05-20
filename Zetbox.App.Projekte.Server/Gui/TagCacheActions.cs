// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using System.Reflection;
    using System.Linq.Dynamic;

    [Implementor]
    public class TagCacheActions
    {
        private static readonly object _lock = new object();

        private static IFrozenContext _frozenCtx;
        public TagCacheActions(IFrozenContext frozenCtx)
        {
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            _frozenCtx = frozenCtx;
        }

        [Invocation]
        public static System.Threading.Tasks.Task Rebuild(TagCache obj)
        {
            lock (_lock)
            {
                var ctx = obj.Context;

                // find all possible properties with a bad hack.
                var props = _frozenCtx
                                .GetQuery<StringProperty>()
                                .ToList()
                                .Where(p => p.ObjectClass is ObjectClass)
                                .Where(p => p.ValueModelDescriptor != null
                                         && p.ValueModelDescriptor.ViewModelTypeRef != null
                                         && p.ValueModelDescriptor.ViewModelTypeRef.Contains("TagPropertyEditorViewModel"))
                                .ToList();

                // Get & group all current tag caches
                var items = ctx.GetQuery<TagCache>()
                    .ToList()
                    .GroupBy(k => k.Name.ToLower())
                    .ToDictionary(k => k.Key, v => v.ToList());

                // Get all tags from items & create tag caches if tag was not found
                foreach (var prop in props)
                {
                    foreach (var item in GetUntypedQuery(ctx, (ObjectClass)prop.ObjectClass).Take(1000))
                    {
                        var tags = item.GetPropertyValue<string>(prop.Name);
                        if (string.IsNullOrWhiteSpace(tags)) continue;

                        foreach (var tag in tags.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var lowerTag = tag.ToLower();
                            if (!items.ContainsKey(lowerTag))
                            {
                                var tagCache = ctx.Create<TagCache>();
                                tagCache.Name = tag;
                                items[lowerTag] = new List<TagCache>() { tagCache };
                            }
                        }
                    }
                }

                // delete duplicats
                foreach (var tagsToDelete in items.Where(kv => kv.Value.Count > 1))
                {
                    foreach (var tag in tagsToDelete.Value.Skip(1))
                    {
                        ctx.Delete(tag);
                    }
                }
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        private static IQueryable GetUntypedQueryHack<T>(IZetboxContext ctx)
            where T : class, IDataObject
        {
            return ctx.GetQuery<T>();
        }

        private static IQueryable GetUntypedQuery(IZetboxContext ctx, ObjectClass cls)
        {
            var mi = MethodBase.GetCurrentMethod().DeclaringType.FindGenericMethod("GetUntypedQueryHack", new[] { cls.GetDescribedInterfaceType().Type }, new[] { typeof(IZetboxContext) }, isPrivate: true);
            return (IQueryable)mi.Invoke(null, new[] { ctx });
        }
    }
}
