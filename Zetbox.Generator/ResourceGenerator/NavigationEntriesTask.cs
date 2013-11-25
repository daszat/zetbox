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
    using Zetbox.API.Common;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;

    internal class NavigationEntriesTask : IResourceGeneratorTask
    {
        public void Generate(IResourceGenerator generator, IZetboxServerContext ctx, IEnumerable<Zetbox.App.Base.Module> modules)
        {
            var moduleNames = modules.Select(m => m.Name).ToArray();

            using (var writer = generator.AddFile("GUI\\NavigationEntries")) // See ZetboxAssetKeys.NavigationEntries
            {
                foreach (var nav in ctx.GetQuery<NavigationEntry>()
                                        .ToList()
                                        .Where(e => moduleNames.Contains(e.Module.Name))
                                        .OrderBy(e => e.Title))
                {
                    writer.AddResource(ZetboxAssetKeys.ConstructTitleKey(nav), nav.Title);
                    writer.AddResource(ZetboxAssetKeys.ConstructColorKey(nav), nav.Color);
                }
            }
        }
    }
}
