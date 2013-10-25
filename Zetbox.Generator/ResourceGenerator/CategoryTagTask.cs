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

namespace Zetbox.Generator.ResourceGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using Zetbox.App.Base;

    internal class CategoryTagTask : IResourceGeneratorTask
    {
        public void Generate(IResourceGenerator generator, IZetboxServerContext ctx, IEnumerable<Zetbox.App.Base.Module> modules)
        {
            var moduleNames = modules.Select(m => m.Name).ToArray();

            var tags = GetTags(ctx, moduleNames);
            var exceptTags = moduleNames.Contains("ZetboxBase") 
                    ? new List<string>() 
                    : GetTags(ctx, "ZetboxBase", "GUI");
            
            using (var writer = generator.AddFile("ZetboxBase\\CategoryTags"))
            {
                foreach (var tag in tags.Except(exceptTags).OrderBy(i => i))
                {
                    if(!string.IsNullOrWhiteSpace(tag))
                        writer.AddResource(tag, tag);
                }
            }
        }

        private static List<string> GetTags(IZetboxServerContext ctx, params string[] moduleNames)
        {
            var splitArray = ",".ToCharArray();
            var propTags = ctx.GetQuery<Property>()
                                .Where(p => p.CategoryTags != null)
                                .ToList()
                                .Where(p => moduleNames.Contains(p.Module.Name))
                                .SelectMany(p => p.CategoryTags.Split(splitArray, StringSplitOptions.RemoveEmptyEntries))
                                .Select(t => t.Trim());
            var methodTags = ctx.GetQuery<Method>()
                                .Where(m => m.CategoryTags != null)
                                .ToList()
                                .Where(m => moduleNames.Contains(m.Module.Name))
                                .SelectMany(m => m.CategoryTags.Split(splitArray, StringSplitOptions.RemoveEmptyEntries))
                                .Select(t => t.Trim());

            return propTags.Union(methodTags).Distinct().ToList();
        }
    }
}
