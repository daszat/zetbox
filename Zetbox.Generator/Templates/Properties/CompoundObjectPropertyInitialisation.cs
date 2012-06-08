
namespace Zetbox.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public partial class CompoundObjectPropertyInitialisation
    {
        public static void Call(IGenerationHost _host, IZetboxContext ctx, IEnumerable<CompoundObjectProperty> properties, string implementationSuffix, string implementationPropertySuffix)
        {
            foreach (var p in properties.Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                Call(_host, ctx, p, implementationSuffix, implementationPropertySuffix);
            }
        }

        public static void Call(IGenerationHost _host, IZetboxContext ctx, CompoundObjectProperty property, string implementationSuffix, string implementationPropertySuffix)
        {
            string propertyName = property.Name;
            string backingStoreName = propertyName + implementationPropertySuffix;
            string typeName = property.GetElementTypeString();
            string implementationTypeName = typeName + implementationSuffix;
            bool isNull = property.IsNullable();

            Call(_host, ctx, implementationTypeName, propertyName, backingStoreName, isNull);
        }
    }
}
