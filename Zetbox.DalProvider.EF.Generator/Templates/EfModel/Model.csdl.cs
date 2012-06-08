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
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    public partial class ModelCsdl
    {
        /// <summary>
        /// returns a &lt;Property/&gt; element describing the property 
        /// without regards for the IsList flag.
        /// </summary>
        /// therefore it can be used both when defining a type (IsList == 
        /// false) and when defining the CollectionEntry (IsList == true)
        internal static string PlainPropertyDefinitionFromValueType(ValueTypeProperty prop, string name, string implementationSuffix)
        {
            string type = prop.GetElementTypeString();
            string maxlength = String.Empty;
            string precScaleAttr = String.Empty;
            string concurrency = String.Empty;

            // strip nullable "?"
            if (prop.IsNullable() && type.EndsWith("?"))
            {
                type = type.Substring(0, type.Length - 1);
            }

            switch (type)
            {
                case "bool":
                    type = "Boolean";
                    break;
                case "decimal":
                    type = "Decimal";
                    break;
                case "double":
                    type = "Double";
                    break;
                case "int":
                    type = "Int32";
                    break;
                case "string":
                    type = "String";
                    break;
            }

            if (prop is EnumerationProperty)
            {
                type = "Int32";
                name += implementationSuffix;
            }

            if (prop is StringProperty)
            {
                maxlength = String.Format("MaxLength=\"{0}\" ", ((StringProperty)prop).GetMaxLength());
            }

            if (prop is DecimalProperty)
            {
                DecimalProperty dp = (DecimalProperty)prop;
                // must have one space at the end
                precScaleAttr = String.Format("Precision=\"{0}\" Scale=\"{1}\" ", dp.Precision, dp.Scale);
            }

            if (prop.ObjectClass is ObjectClass && ((ObjectClass)prop.ObjectClass).ImplementsIChangedBy() && prop.Name == "ChangedOn")
            {
                concurrency = "ConcurrencyMode=\"Fixed\"";
            }

            return String.Format("<Property Name=\"{0}\" Type=\"{1}\" Nullable=\"{2}\" {3}{4} {5}/>",
                name, type, prop.IsNullable() ? "true" : "false", maxlength, precScaleAttr, concurrency);
        }

        protected virtual void ApplyEntityTypeFieldDefs(IEnumerable<Property> properties)
        {
            EfModel.ModelCsdlEntityTypeFields.Call(Host, ctx, properties);
        }

        protected virtual string GetAbstractModifier(ObjectClass cls)
        {
            return cls.IsAbstract ? " Abstract=\"true\"" : string.Empty;
        }
    }
}
