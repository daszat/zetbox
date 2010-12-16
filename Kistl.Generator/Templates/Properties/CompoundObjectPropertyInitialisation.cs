
namespace Kistl.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public partial class CompoundObjectPropertyInitialisation
    {
        public static void Call(IGenerationHost _host, IKistlContext ctx, IEnumerable<CompoundObjectProperty> properties, string implementationSuffix, string implementationPropertySuffix)
        {
            foreach (var p in properties.Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                Call(_host, ctx, p, implementationSuffix, implementationPropertySuffix);
            }
        }

        public static void Call(IGenerationHost _host, IKistlContext ctx, CompoundObjectProperty property, string implementationSuffix, string implementationPropertySuffix)
        {
            // quick hack to un-break Ef
            //if (property.IsNullable())
            //    return;

            string propertyName = property.Name;
            string backingStoreName = propertyName + implementationPropertySuffix;
            string typeName = property.GetPropertyTypeString();
            string implementationTypeName = typeName + implementationSuffix;

            Call(_host, ctx, implementationTypeName, propertyName, backingStoreName);
        }

        //public static void Call(IGenerationHost _host, IKistlContext ctx, string implementationTypeName, string propertyName, string backingStoreName)
        //{
        //    if (_host == null) { throw new ArgumentNullException("_host"); }

        //    _host.CallTemplate("Properties.CompoundObjectPropertyInitialisation", ctx, implementationTypeName, propertyName, backingStoreName);
        //}
    }
}
