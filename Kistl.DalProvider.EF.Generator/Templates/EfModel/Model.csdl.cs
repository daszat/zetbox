
namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;

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
                    type = typeof(bool).FullName;
                    break;
                case "decimal":
                    type = typeof(decimal).FullName;
                    break;
                case "double":
                    type = typeof(double).FullName;
                    break;
                case "int":
                    type = typeof(int).FullName;
                    break;
                case "string":
                    type = typeof(string).FullName;
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
