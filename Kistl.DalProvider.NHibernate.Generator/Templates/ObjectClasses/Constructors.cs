
namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
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
                string backingStoreName = "this.Proxy." + propertyName;
                string typeName = property.GetPropertyTypeString();
                string implementationTypeName = typeName + ImplementationSuffix;
                string isNull = property.IsNullable() ? "true" : "false";

                this.WriteObjects("            if (", backingStoreName, " == null)");
                this.WriteLine();
                this.WriteObjects("            {");
                this.WriteLine();
                this.WriteObjects("                ", backingStoreName, " = new ", implementationTypeName, "(this, \"", propertyName, "\", null, null) { CompoundObject_IsNull = ", isNull , " };");
                this.WriteLine();
                this.WriteObjects("            }");
                this.WriteLine();
                this.WriteObjects("            else");
                this.WriteLine();
                this.WriteObjects("            {");
                this.WriteLine();
                this.WriteObjects("                ", backingStoreName, ".AttachToObject(this, \"", propertyName, "\");");
                this.WriteLine();
                this.WriteObjects("            }");
                this.WriteLine();

                this.WriteLine();
            }
        }
    }
}
