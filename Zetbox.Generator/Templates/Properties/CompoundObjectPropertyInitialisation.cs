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

namespace Zetbox.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public partial class CompoundObjectPropertyInitialisation
    {
        public static void Call(IGenerationHost _host, IZetboxContext ctx, IEnumerable<CompoundObjectProperty> properties, string implementationSuffix, string implementationPropertySuffix)
        {
            foreach (var p in properties.Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                Call(_host, ctx, p, implementationSuffix, implementationPropertySuffix);
            }
        }

        public static void Call(IGenerationHost _host, IZetboxContext ctx, CompoundObjectProperty property, string implementationSuffix, string implementationPropertySuffix)
        {
            string propertyName = property.Name;
            string backingStoreName = propertyName + implementationPropertySuffix;
            string typeName = property.GetElementTypeString();
            string implementationTypeName = typeName + implementationSuffix;
            bool isNull = property.IsNullable();

            Call(_host, ctx, implementationTypeName, propertyName, backingStoreName, isNull);
        }
    }
}
