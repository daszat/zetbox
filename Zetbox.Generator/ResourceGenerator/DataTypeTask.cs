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

    internal class DataTypeTask : IResourceGeneratorTask
    {
        public void Generate(IResourceGenerator generator, IZetboxServerContext ctx, IEnumerable<Zetbox.App.Base.Module> modules)
        {
            var moduleNames = modules.Select(m => m.Name).ToArray();

            using (var writer = generator.AddFile("ZetboxBase\\DataTypes")) // See ZetboxAssetKeys.DataTypes
            {
                foreach (var dt in ctx.GetQuery<DataType>()
                                        .ToList()
                                        .Where(dt => moduleNames.Contains(dt.Module.Name))
                                        .OrderBy(dt => dt.Name))
                {
                    writer.AddResource(ZetboxAssetKeys.ConstructNameKey(dt), dt.Name);
                    writer.AddResource(ZetboxAssetKeys.ConstructDescriptionKey(dt), dt.Description);
                }
            }

            foreach (var grp in ctx.GetQuery<Property>()
                                        .ToList()
                                        .Where(p => moduleNames.Contains(p.Module.Name))
                                        .GroupBy(p => p.ObjectClass)
                                        .OrderBy(p => p.Key.Module.Namespace)
                                        .ThenBy(p => p.Key.Name))
            {
                var dtName = grp.Key.Module.Namespace + "." + grp.Key.Name;
                using (var writer = generator.AddFile("ZetboxBase\\Properties\\" + dtName)) // See ZetboxAssetKeys.ConstructBaseName
                {
                    foreach (var prop in grp.OrderBy(p => p.Name))
                    {
                        writer.AddResource(ZetboxAssetKeys.ConstructLabelKey(prop), string.IsNullOrWhiteSpace(prop.Label) ? prop.Name : prop.Label);
                        writer.AddResource(ZetboxAssetKeys.ConstructDescriptionKey(prop), prop.Description);
                    }
                }
            }

            foreach (var grp in ctx.GetQuery<Method>()
                                        .ToList()
                                        .Where(m => moduleNames.Contains(m.Module.Name))
                                        .GroupBy(m => m.ObjectClass)
                                        .OrderBy(m => m.Key.Module.Namespace)
                                        .ThenBy(m => m.Key.Name))
            {
                var dtName = grp.Key.Module.Namespace + "." + grp.Key.Name;
                using (var writer = generator.AddFile("ZetboxBase\\Methods\\" + dtName)) // See ZetboxAssetKeys.ConstructBaseName
                {
                    foreach (var meth in grp.OrderBy(p => p.Name))
                    {
                        writer.AddResource(ZetboxAssetKeys.ConstructLabelKey(meth), string.IsNullOrWhiteSpace(meth.Label) ? meth.Name : meth.Label);
                        writer.AddResource(ZetboxAssetKeys.ConstructDescriptionKey(meth), meth.Description);

                        foreach (var param in meth.Parameter)
                        {
                            writer.AddResource(ZetboxAssetKeys.ConstructLabelKey(param), string.IsNullOrWhiteSpace(param.Label) ? param.Name : param.Label);
                            writer.AddResource(ZetboxAssetKeys.ConstructDescriptionKey(param), param.Description);
                        }
                    }
                }
            }

            foreach (var enumeration in ctx.GetQuery<Enumeration>()
                                            .ToList()
                                            .Where(dt => moduleNames.Contains(dt.Module.Name))
                                            .OrderBy(dt => dt.Name))
            {
                using (var writer = generator.AddFile("ZetboxBase\\Enumerations\\" + enumeration.Module.Namespace + "." + enumeration.Name)) // See ZetboxAssetKeys.ConstructBaseName
                {
                    foreach (var entry in enumeration.EnumerationEntries)
                    {
                        writer.AddResource(ZetboxAssetKeys.ConstructLabelKey(entry), string.IsNullOrWhiteSpace(entry.Label) ? entry.Name : entry.Label);
                        writer.AddResource(ZetboxAssetKeys.ConstructDescriptionKey(entry), entry.Description);
                    }
                }
            }
        }
    }
}
