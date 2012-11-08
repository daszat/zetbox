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


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.App.Base;

    public static class EfScalarPropHelper
    {
        public static void ApplyAttributesTemplate(CodeTemplate template)
        {
            if (template == null) throw new ArgumentNullException("template");

            template.WriteLine("        [XmlIgnore()]");
            template.WriteLine("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]");
            template.WriteLine("        [EdmScalarProperty()]");
        }

        public static void ApplyBackingStoreDefinition(CodeTemplate template, string type, string backingName, string efName)
        {
            if (template == null) throw new ArgumentNullException("template");
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException("type");
            if (string.IsNullOrEmpty(backingName)) throw new ArgumentNullException("backingName");

            template.WriteObjects("        private ", type, " ", backingName, "_store;");
            template.WriteLine();
            template.WriteObjects("        private ", type, " ", backingName, " {");
            template.WriteLine();
            template.WriteObjects("            get { return ", backingName, "_store; }");
            template.WriteLine();
            template.WriteObjects("            set {");
            template.WriteLine();
            template.WriteObjects("                ReportEfPropertyChanging(\"", efName, "\");");
            template.WriteLine();
            template.WriteObjects("                ", backingName, "_store = value;");
            template.WriteLine();
            template.WriteObjects("                ReportEfPropertyChanged(\"", efName, "\");");
            template.WriteLine();
            template.WriteObjects("            }");
            template.WriteLine();
            template.WriteObjects("        }");
            template.WriteLine();
        }

        public static void ApplyInitialisation(CodeTemplate template, string backingName)
        {
            if (template == null) throw new ArgumentNullException("template");
            if (string.IsNullOrEmpty(backingName)) throw new ArgumentNullException("backingName");

            template.WriteObjects("                if (", backingName, "_store == Guid.Empty) {\r\n");
            template.WriteObjects("                    __result = ", backingName, "_store = Guid.NewGuid();\r\n");
            template.WriteObjects("                }\r\n");
        }

        public static string GetEfPropName(Property prop)
        {
            if (prop is EnumerationProperty)
            {
                return prop.Name + Zetbox.API.Helper.ImplementationSuffix;
            }
            else if (prop is CompoundObjectProperty)
            {
                if (((CompoundObjectProperty)prop).IsList)
                {
                    return "Value" + Zetbox.API.Helper.ImplementationSuffix;
                }
                else
                {
                    return prop.Name + Zetbox.API.Helper.ImplementationSuffix;
                }
            }
            else
            {
                return prop.Name;
            }
        }
    }
}
