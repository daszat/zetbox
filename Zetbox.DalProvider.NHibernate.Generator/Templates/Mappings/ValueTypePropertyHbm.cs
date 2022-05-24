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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Zetbox.API;
    using Zetbox.API.SchemaManagement;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;

    public partial class ValueTypePropertyHbm
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost _host,
            string prefix,
            ValueTypeProperty prop,
            string propName,
            string columnName,
            bool forceDefinition,
            string implementationSuffix,
            bool needsConcurrency)
        {
            if (_host == null) throw new ArgumentNullException("_host");
            if (prop == null) throw new ArgumentNullException("prop");

            // shortcut unmapped properties
            if (prop.IsCalculated)
            {
                _host.WriteOutput(string.Format("<!-- ValueTypeProperty {0} is calculated and persisted -->\n", prop.Name));
            }

            propName = string.IsNullOrEmpty(propName) ? prop.Name : propName;
            columnName = string.IsNullOrEmpty(columnName) ? Construct.ColumnName(prop, prefix) : prefix + columnName;
            if (needsConcurrency && propName == "ChangedOn")
            {
                // Will be generated via the version tag.
                return;
            }

            string typeAttr = String.Empty;
            if (prop is DateTimeProperty)
            {
                typeAttr = "type=\"Timestamp\"";
            }

            int length = 0;
            string ceClassAttr;
            if (prop.IsList && !forceDefinition)
            {
                // set the proper type for collection entries
                ceClassAttr = String.Format("class=\"{0}.{1}{2}+{1}Proxy,Zetbox.Objects.NHibernateImpl\"",
                    prop.GetCollectionEntryNamespace(),
                    prop.GetCollectionEntryClassName(),
                    implementationSuffix);
            }
            else
            {
                // not needed
                ceClassAttr = String.Empty;

                if (prop is StringProperty)
                {
                    length = ((StringProperty)prop).GetMaxLength().Result;
                }
            }

            string ceReverseKeyColumnName = Construct.ForeignKeyColumnName(prop).Result;
            string listPositionColumnName = Construct.ListPositionColumnName(prop);

            Call(_host,
                prefix,
                propName,
                columnName,
                prop.IsList && !forceDefinition,
                typeAttr,
                ceClassAttr,
                ceReverseKeyColumnName,
                listPositionColumnName,
                length);
        }
    }
}