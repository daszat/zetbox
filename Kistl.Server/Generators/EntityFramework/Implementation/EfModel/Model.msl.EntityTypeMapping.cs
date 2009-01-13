using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelMslEntityTypeMapping
    {
        protected virtual void ApplyEntityTypeMapping(ObjectClass obj)
        {
            CallTemplate("Implementation.EfModel.ModelMslEntityTypeMapping", obj);
        }

        protected virtual void ApplyScalarProperty(Property prop, string parentName)
        {
            string propertyName;
            string columnName;

            if (prop is EnumerationProperty)
            {
                propertyName = prop.PropertyName + Kistl.API.Helper.ImplementationSuffix;
                columnName = Construct.NestedColumnName(prop, parentName);
            }
            else if (prop is ValueTypeProperty)
            {
                propertyName = prop.PropertyName;
                columnName = Construct.NestedColumnName(prop, parentName);
            }
            else if (prop is ObjectReferenceProperty && prop.NeedsPositionColumn())
            {
                propertyName = prop.PropertyName + Kistl.API.Helper.PositionSuffix;
                columnName = Construct.ListPositionColumnName(prop, parentName);
            }
            else
            {
                return;
            }

            this.WriteLine("<ScalarProperty Name=\"{0}\" ColumnName=\"{1}\" />", propertyName, columnName);
        }

        protected virtual void ApplyComplexProperty(StructProperty prop, string parentName)
        {
            this.WriteLine("<ComplexProperty Name=\"{0}{1}\" TypeName=\"{2}\">",
                prop.PropertyName,
                Kistl.API.Helper.ImplementationSuffix,
                prop.StructDefinition.ClassName
                );

            string newParent = Construct.NestedColumnName(prop, parentName);
            foreach (var subProp in prop.StructDefinition.Properties.OfType<Property>().Where(p => !p.IsList))
            {
                ApplyScalarProperty(subProp, newParent);
            }

            foreach (var subProp in prop.StructDefinition.Properties.OfType<StructProperty>().Where(p => !p.IsList))
            {
                ApplyComplexProperty(subProp, newParent);
            }

            this.WriteLine("</ComplexProperty>");
        }

    }
}
