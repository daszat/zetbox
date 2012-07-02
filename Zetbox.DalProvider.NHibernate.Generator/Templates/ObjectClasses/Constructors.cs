// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses
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
        public sealed class CompoundInitialisationDescriptor
        {
            public readonly string PropertyName;
            public readonly string BackingStoreName;
            public readonly string TypeName;
            public readonly string ImplementationTypeName;

            public CompoundInitialisationDescriptor(string propertyName, string backingStoreName, string typeName, string implementationTypeName)
            {
                this.PropertyName = propertyName;
                this.BackingStoreName = backingStoreName;
                this.TypeName = typeName;
                this.ImplementationTypeName = implementationTypeName;
            }

            public static IEnumerable<CompoundInitialisationDescriptor> CreateDescriptors(IEnumerable<CompoundObjectProperty> props, string implementationSuffix)
            {
                return props.Select(cop =>
                {
                    string propertyName = cop.Name;
                    string backingStoreName = "this.Proxy." + propertyName;
                    string typeName = cop.GetPropertyTypeString();
                    string implementationTypeName = typeName + implementationSuffix;

                    return new CompoundInitialisationDescriptor(propertyName, backingStoreName, typeName, implementationTypeName);
                });
            }
        }

        public virtual void ApplyCompoundObjectPropertyInitialisers()
        {
            foreach (var desc in compoundObjectInitialisers) //.Where(cop => !cop.IsList).OrderBy(cop => cop.Name))
            {
                this.WriteObjects("            if (", desc.BackingStoreName, " == null)");
                this.WriteLine();
                this.WriteObjects("            {");
                this.WriteLine();
                this.WriteObjects("                ", desc.BackingStoreName, " = new ", desc.ImplementationTypeName, "(this, \"", desc.PropertyName, "\", null, null);");
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
