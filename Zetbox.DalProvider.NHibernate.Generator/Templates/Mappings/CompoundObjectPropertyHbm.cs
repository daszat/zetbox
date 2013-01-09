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
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;

    public partial class CompoundObjectPropertyHbm
    {
        public static void Call(
            Arebis.CodeGeneration.IGenerationHost _host,
            IZetboxContext ctx,
            string prefix,
            CompoundObjectProperty prop,
            string propName,
            string columnName,
            bool forceDefinition,
            string implementationSuffix)
        {
            if (_host == null) throw new ArgumentNullException("_host");
            if (prop == null) throw new ArgumentNullException("prop");
            if (prop.CompoundObjectDefinition == null) throw new ArgumentException("CompoundObjectProperty has no definition", "prop");
            if (prop.CompoundObjectDefinition.Module == null) throw new ArgumentException("CompoundObjectProperty.CompoundObjectDefinition has no module", "prop");

            // shortcut unmapped properties
            //if (prop.IsCalculated)
            //{
            //    _host.WriteOutput(string.Format("<!-- CompoundObjectProperty {0} is calculated -->", prop.Name));
            //    return;
            //}

            propName = string.IsNullOrEmpty(propName) ? prop.Name : propName;
            columnName = string.IsNullOrEmpty(columnName) ? Construct.ColumnName(prop, prefix) : prefix + columnName;
            string valueClassAttr = String.Format("class=\"{0}.{1}{2},Zetbox.Objects.NHibernateImpl\"",
                prop.CompoundObjectDefinition.Module.Namespace,
                prop.CompoundObjectDefinition.Name,
                implementationSuffix);
            string isNullColumnAttr = String.Format("column=\"`{0}`\"", columnName);
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
            }
            string ceReverseKeyColumnName = Construct.ForeignKeyColumnName(prop);
            Call(_host,
                ctx,
                prefix,
                propName,
                columnName,
                prop.IsList && !forceDefinition,
                ceClassAttr,
                valueClassAttr,
                isNullColumnAttr,
                ceReverseKeyColumnName,
                prop.CompoundObjectDefinition.Properties);
        }
    }
}