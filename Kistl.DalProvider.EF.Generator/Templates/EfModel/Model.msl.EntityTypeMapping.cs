
namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Arebis.CodeGeneration;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator;
    using Kistl.Generator.Extensions;
    
    public partial class ModelMslEntityTypeMapping
    {
        protected virtual void ApplyEntityTypeMapping(ObjectClass obj)
        {
            Call(Host, ctx, obj);
        }

        protected virtual void ApplyPropertyMappings()
        {
            var relevantRelations = cls.GetRelations() // Managed by a cache
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
                    propertyName = Construct.ListPositionPropertyName(rel.A);
                    columnName = Construct.ListPositionColumnName(rel.B);
                    this.WriteLine("<ScalarProperty Name=\"{0}\" ColumnName=\"{1}\" />", propertyName, columnName);
                }

                if (rel.B.Type == cls && rel.NeedsPositionStorage(RelationEndRole.B) && rel.B.Navigator != null)
                {
                    propertyName = Construct.ListPositionPropertyName(rel.B);
                    columnName = Construct.ListPositionColumnName(rel.A);
                    this.WriteLine("<ScalarProperty Name=\"{0}\" ColumnName=\"{1}\" />", propertyName, columnName);
                }
            }

            foreach (var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                ModelMslEntityTypeMappingScalarProperty.Call(Host, ctx, prop, prop.Name, String.Empty);
            }

            foreach (var prop in cls.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                ModelMslEntityTypeMappingComplexProperty.Call(Host, ctx, prop, prop.Name, String.Empty);
            }
        }
    }
}
