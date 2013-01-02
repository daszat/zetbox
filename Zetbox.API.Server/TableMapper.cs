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

namespace Zetbox.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.SchemaMigration;

    public static class TableMapper
    {
        public static TableRef GetTableRef(this ObjectClass cls, ISchemaProvider provider)
        {
            if (cls == null) throw new ArgumentNullException("cls");
            if (provider == null) throw new ArgumentNullException("provider");

            if (cls.GetTableMapping() == TableMapping.TPH)
            {
                cls = cls.GetRootClass();
            }
            return provider.GetTableName(cls.Module.SchemaName, cls.TableName);
        }
    }
}
