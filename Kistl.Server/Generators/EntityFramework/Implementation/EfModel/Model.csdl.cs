using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

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
            return PlainPropertyDefinitionFromValueType(prop, prop.PropertyName);
        }

        /// <summary>
        /// returns a &lt;Property/&gt; element describing the property 
        /// without regards for the IsList flag.
        /// </summary>
        /// therefore it can be used both when defining a type (IsList == 
        /// false) and when defining the CollectionEntry (IsList == true)
        public static string PlainPropertyDefinitionFromValueType(ValueTypeProperty prop, string name)
        {
            string type = prop.GetPropertyTypeString();
            string maxlength = "";

            if (prop is EnumerationProperty)
            {
                type = "Int32";
                name += Kistl.API.Helper.ImplementationSuffix;
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
            Implementation.EfModel.ModelCsdlEntityTypeFields.Call(Host, ctx, properties);
        }

        internal static IEnumerable<Relation> GetRelationsWithSeparateStorage(IKistlContext ctx)
        {
            return ctx.GetQuery<Relation>()
                .Where(r => (int)r.Storage == (int)StorageType.Separate)
                .ToList()
                .OrderBy(r => r.GetAssociationName());
        }

        internal static IEnumerable<Relation> GetRelationsWithoutSeparateStorage(IKistlContext ctx)
        {
            return ctx.GetQuery<Relation>()
                .Where(r => (int)r.Storage != (int)StorageType.Separate)
                .ToList()
                .OrderBy(r => r.GetAssociationName());
        }

    }
}
