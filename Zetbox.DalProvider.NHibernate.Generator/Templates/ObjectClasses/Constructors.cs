
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
        public sealed class CompoundInitialisationDescriptor
        {
            public readonly string PropertyName;
            public readonly string BackingStoreName;
            public readonly string TypeName;
            public readonly string ImplementationTypeName;
            public readonly bool IsNull;

            public CompoundInitialisationDescriptor(string propertyName, string backingStoreName, string typeName, string implementationTypeName, bool isNull)
            {
                this.PropertyName = propertyName;
                this.BackingStoreName = backingStoreName;
                this.TypeName = typeName;
                this.ImplementationTypeName = implementationTypeName;
                this.IsNull = isNull;
            }

            public static IEnumerable<CompoundInitialisationDescriptor> CreateDescriptors(IEnumerable<CompoundObjectProperty> props, string implementationSuffix)
            {
                return props.Select(cop =>
                {
                    string propertyName = cop.Name;
                    string backingStoreName = "this.Proxy." + propertyName;
                    string typeName = cop.GetPropertyTypeString();
                    string implementationTypeName = typeName + implementationSuffix;
                    bool isNull = cop.IsNullable();

                    return new CompoundInitialisationDescriptor(propertyName, backingStoreName, typeName, implementationTypeName, isNull);
                });
            }
        }

        public virtual void ApplyCompoundObjectPropertyInitialisers()
        {
            foreach (var desc in compoundObjectInitialisers) //.Where(cop => !cop.IsList).OrderBy(cop => cop.Name))
            {
                string isNullString = desc.IsNull ? "true" : "false"; // avoid Culture-dependence

                this.WriteObjects("            if (", desc.BackingStoreName, " == null)");
                this.WriteLine();
                this.WriteObjects("            {");
                this.WriteLine();
                this.WriteObjects("                ", desc.BackingStoreName, " = new ", desc.ImplementationTypeName, "(this, \"", desc.PropertyName, "\", null, null) { CompoundObject_IsNull = ", isNullString, " };");
                this.WriteLine();
                this.WriteObjects("            }");
                this.WriteLine();
                this.WriteObjects("            else");
                this.WriteLine();
                this.WriteObjects("            {");
                this.WriteLine();
                this.WriteObjects("                ", desc.BackingStoreName, ".AttachToObject(this, \"", desc.PropertyName, "\");");
                this.WriteLine();
                this.WriteObjects("            }");
                this.WriteLine();

                this.WriteLine();
            }
        }

        public virtual void ApplyDefaultValueSetFlagInitialisers()
        {
            foreach (var flag in valueSetFlags)
            {
                this.WriteObjects("            ", flag, " = Proxy.ID > 0;");
                this.WriteLine();
            }
        }
    }
}
