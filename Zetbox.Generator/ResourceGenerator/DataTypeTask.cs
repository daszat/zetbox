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

    internal class DataTypeTask : IResourceGeneratorTask
    {
        public void Generate(IResourceGenerator generator, IZetboxServerContext ctx, App.Base.Module module)
        {
            using (var writer = generator.AddFile("ZetboxBase\\DataTypes"))
            {
                foreach (var dt in ctx.GetQuery<DataType>()
                                        .Where(dt => dt.Module.Name == module.Name)
                                        .OrderBy(dt => dt.Name))
                {
                    var fullName = dt.Module.Namespace + "." + dt.Name;
                    writer.AddResource(fullName, dt.Name);
                    writer.AddResource(fullName + "_description", dt.Description);

                }
            }

            using (var writer = generator.AddFile("ZetboxBase\\Properties"))
            {
                foreach (var prop in ctx.GetQuery<Property>()
                                            .Where(p => p.Module.Name == module.Name)
                                            .OrderBy(p => p.ObjectClass.Module.Namespace)
                                            .ThenBy(p => p.ObjectClass.Name)
                                            .ThenBy(p => p.Name))
                {
                    var fullName = prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name + "." + prop.Name;
                    writer.AddResource(fullName, prop.Name);
                    writer.AddResource(fullName + "_label", prop.Label);
                    writer.AddResource(fullName + "_description", prop.Description);
                }
            }

            using (var writer = generator.AddFile("ZetboxBase\\Methods"))
            {
                foreach (var meth in ctx.GetQuery<Method>()
                                            .Where(m => m.Module.Name == module.Name)
                                            .OrderBy(m => m.ObjectClass.Module.Namespace)
                                            .ThenBy(m => m.ObjectClass.Name)
                                            .ThenBy(m => m.Name))
                {
                    var fullName = meth.ObjectClass.Module.Namespace + "." + meth.ObjectClass.Name + "." + meth.Name;
                    writer.AddResource(fullName, meth.Name);
                    writer.AddResource(fullName + "_label", meth.Label);
                    writer.AddResource(fullName + "_description", meth.Description);

                    foreach (var param in meth.Parameter)
                    {
                        var param_fullName = fullName + "(" + param.Name + ")";
                        writer.AddResource(param_fullName, param.Name);
                        writer.AddResource(param_fullName + "_label", param.Label);
                        writer.AddResource(param_fullName + "_description", param.Description);
                    }
                }
            }

            foreach (var enumeration in ctx.GetQuery<Enumeration>()
                                            .Where(dt => dt.Module.Name == module.Name)
                                            .OrderBy(dt => dt.Name))
            {
                using (var writer = generator.AddFile("ZetboxBase\\Enumerations\\" + enumeration.Name))
                {
                    foreach (var entry in enumeration.EnumerationEntries)
                    {
                        writer.AddResource(entry.Name, entry.Name);
                        writer.AddResource(entry.Name + "_label", entry.Label);
                        writer.AddResource(entry.Name + "_description", entry.Description);
                    }
                }
            }
        }
    }
}
