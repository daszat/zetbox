
namespace Kistl.DalProvider.NHibernate.Generator.Templates.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public partial class Constructors
    {
        public virtual void ApplyCompoundObjectPropertyInitialisers()
        {
            foreach (var property in compoundObjectProperties.Where(cop => !cop.IsList).OrderBy(cop => cop.Name))
            {
                string propertyName = property.Name;
                //string backingStoreName = propertyName + ImplementationPropertySuffix;
                string typeName = property.GetPropertyTypeString();
                string implementationTypeName = typeName + ImplementationSuffix;
                //bool isNull = property.IsNullable();

                this.WriteObjects("            _", propertyName, " = new ", implementationTypeName, "(this, \"", propertyName, "\", null, null);");
                this.WriteLine();
            }
        }
    }
}
