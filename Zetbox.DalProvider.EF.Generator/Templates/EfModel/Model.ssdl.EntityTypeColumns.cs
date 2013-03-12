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

namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.API.Common;
    using Zetbox.App.Extensions;

    public partial class ModelSsdlEntityTypeColumns
    {
        protected virtual void ApplyEntityTypeColumnDefs(IEnumerable<Property> properties, string prefix, ISchemaProvider schemaProvider)
        {
            Call(Host, ctx, properties, prefix, schemaProvider);
        }

        protected virtual bool IsRealNullable(Property p)
        {
            var cls = p.ObjectClass as ObjectClass;
            if(cls == null)
            {
                return p.IsNullable();
            } 
            else 
            {
                // Has to be nullalbe when not the base class in TPH hierarchies
                return (cls.GetTableMapping() == TableMapping.TPH && cls.BaseObjectClass != null) || p.IsNullable();
            }
        }
    }
}
