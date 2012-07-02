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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public partial class Constructors
    {
        public virtual void ApplyCompoundObjectPropertyInitialisers()
        {
            foreach (var property in compoundObjectProperties.Where(cop => !cop.IsList).OrderBy(cop => cop.Name))
            {
                string propertyName = property.Name;
                string typeName = property.GetPropertyTypeString();
                string implementationTypeName = typeName + ImplementationSuffix;

                this.WriteObjects("            _", propertyName, " = new ", implementationTypeName, "(this, \"", propertyName, "\", null, null);");
                this.WriteLine();
            }
        }
    }
}
