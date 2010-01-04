using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Arebis.CodeGeneration;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelMslEntityTypeMapping
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, ObjectClass obj)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.EfModel.ModelMslEntityTypeMapping", ctx, obj);
        }

        protected virtual void ApplyEntityTypeMapping(ObjectClass obj)
        {
            Call(Host, ctx, obj);
        }

        protected virtual void ApplyPropertyMappings()
        {
            var relevantRelations = ctx.GetQuery<Relation>()
                .Where(r => (r.A.Type.ID == cls.ID && r.Storage == StorageType.MergeIntoA)
                            || (r.B.Type.ID == cls.ID && r.Storage == StorageType.MergeIntoB))
                .ToList()
                .OrderBy(r => r.GetAssociationName());

            foreach (var rel in relevantRelations)
            {
                string propertyName;
                string columnName;

                if (rel.A.Type == cls && rel.NeedsPositionStorage(RelationEndRole.A) && rel.A.Navigator != null)
                {
                    propertyName = rel.A.Navigator.PropertyName + Kistl.API.Helper.PositionSuffix;
                    columnName = Construct.ListPositionColumnName(rel.B, String.Empty);
                    this.WriteLine("<ScalarProperty Name=\"{0}\" ColumnName=\"{1}\" />", propertyName, columnName);
                }

                if (rel.B.Type == cls && rel.NeedsPositionStorage(RelationEndRole.B) && rel.B.Navigator != null)
                {
                    propertyName = rel.B.Navigator.PropertyName + Kistl.API.Helper.PositionSuffix;
                    columnName = Construct.ListPositionColumnName(rel.A, String.Empty);
                    this.WriteLine("<ScalarProperty Name=\"{0}\" ColumnName=\"{1}\" />", propertyName, columnName);
                }
            }

            foreach (var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).OrderBy(p => p.PropertyName))
            {
                ApplyScalarProperty(prop, String.Empty);
            }

            foreach (var prop in cls.Properties.OfType<StructProperty>().Where(p => !p.IsList).OrderBy(p => p.PropertyName))
            {
                ApplyComplexProperty(prop, String.Empty);
            }
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
            else if (prop is ObjectReferenceProperty)
            {
                throw new ArgumentOutOfRangeException("prop", "cannot apply ObjectReferenceProperty as scalar");
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
            foreach (var subProp in prop.StructDefinition.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).OrderBy(p => p.PropertyName))
            {
                ApplyScalarProperty(subProp, newParent);
            }

            foreach (var subProp in prop.StructDefinition.Properties.OfType<StructProperty>().Where(p => !p.IsList).OrderBy(p => p.PropertyName))
            {
                ApplyComplexProperty(subProp, newParent);
            }

            this.WriteLine("</ComplexProperty>");
        }
    }
}
