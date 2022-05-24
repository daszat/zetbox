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
        public static void Call(IGenerationHost _host, IZetboxContext ctx, IEnumerable<CompoundObjectProperty> properties, bool asCollectionEntry, string implementationSuffix, string implementationPropertySuffix, string lazyCtxProperty)
        {
            foreach (var p in properties.Where(p => asCollectionEntry || !p.IsList).OrderBy(p => p.Name))
            {
                Call(_host, ctx, p, asCollectionEntry, implementationSuffix, implementationPropertySuffix, lazyCtxProperty);
            }
        }

        public static void Call(IGenerationHost _host, IZetboxContext ctx, CompoundObjectProperty property, bool asCollectionEntry, string implementationSuffix, string implementationPropertySuffix, string lazyCtxProperty)
        {
            string propertyName = asCollectionEntry ? "Value" : property.Name;
            string backingStoreName = propertyName + implementationPropertySuffix;
            string typeName = property.GetElementTypeString().Result;
            string implementationTypeName = typeName + implementationSuffix;
            string lazyCtxParam = string.IsNullOrEmpty(lazyCtxProperty) ? "null" : lazyCtxProperty;

            Call(_host, ctx, implementationTypeName, propertyName, backingStoreName, lazyCtxParam);
        }
    }
}
