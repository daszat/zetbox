using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Arebis.CodeGeneration;
using Kistl.API;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelMslEntityTypeMapping
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, ObjectClass obj)
        {
            host.CallTemplate("Implementation.EfModel.ModelMslEntityTypeMapping", ctx, obj);
        }

        protected virtual void ApplyEntityTypeMapping(ObjectClass obj)
        {
            Call(Host, ctx, obj);
        }

        protected virtual void ApplyScalarProperty(Property prop, string parentName)
        {
            string propertyName = prop.PropertyName;
            string columnName;

            if (prop is EnumerationProperty)
            {
                columnName = Construct.NestedColumnName(prop, parentName);
                propertyName += Kistl.API.Helper.ImplementationSuffix;
            }
            else if (prop is ValueTypeProperty)
            {
                columnName = Construct.NestedColumnName(prop, parentName);
            }
            else if (prop is ObjectReferenceProperty && prop.NeedsPositionColumn())
            {
                propertyName += Kistl.API.Helper.PositionSuffix;
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
            this.WriteLine("<ComplexProperty Name=\"{0}{1}\" TypeName=\"Model.{2}\">",
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
