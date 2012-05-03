

namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.App.Base;

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

        public static string GetEfPropName(Property prop)
        {
            if (prop is EnumerationProperty)
            {
                return prop.Name + Kistl.API.Helper.ImplementationSuffix;
            }
            else if (prop is CompoundObjectProperty)
            {
                if (((CompoundObjectProperty)prop).IsList)
                {
                    return "Value" + Kistl.API.Helper.ImplementationSuffix;
                }
                else
                {
                    return prop.Name + Kistl.API.Helper.ImplementationSuffix;
                }
            }
            else
            {
                return prop.Name;
            }
        }
    }
}
