using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelCsdl
    {
        /// <summary>
        /// returns a &lt;Property/&gt; element describing the property 
        /// without regards for the IsList flag.
        /// </summary>
        /// therefore it can be used both when defining a type (IsList == 
        /// false) and when defining the CollectionEntry (IsList == true)
        public static string PlainPropertyDefinitionFromValueType(ValueTypeProperty prop)
        {
            // ValueTypeProperty
            string name = prop.PropertyName;
            string type = prop.GetPropertyTypeString();
            string maxlength = "";

            if (prop is EnumerationProperty)
            {
                name += Kistl.API.Helper.ImplementationSuffix;
                type = "Int32";
            }
            else
            {
                // translate to short name
                type = Type.GetType(type).Name;
            }

            if (prop is StringProperty)
            {
                maxlength = String.Format("MaxLength=\"{0}\" ", ((StringProperty)prop).Length.ToString());
            }

            return String.Format("<Property Name=\"{0}\" Type=\"{1}\" Nullable=\"{2}\" {3}/>", 
                name, type, prop.IsNullable.ToString().ToLowerInvariant(), maxlength);
        }

        protected virtual void ApplyEntityTypeFieldDefs(IEnumerable<Property> properties)
        {
            CallTemplate("Implementation.EfModel.ModelCsdlEntityTypeFields", ctx, properties);
        }

    }
}
