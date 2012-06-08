
namespace Zetbox.DalProvider.NHibernate.Generator.Templates.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

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
