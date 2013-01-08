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

namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Arebis.CodeGeneration;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;

    public partial class ModelMslEntityTypeMapping
    {
        protected virtual void ApplyEntityTypeMapping(ObjectClass obj)
        {
            Call(Host, ctx, obj);
        }

        protected virtual void ApplyPropertyMappings(ObjectClass cls)
        {
            if (cls.GetTableMapping() == TableMapping.TPH && cls.BaseObjectClass != null)
            {
                ApplyPropertyMappings(cls.BaseObjectClass);
            }

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
                    this.WriteLine("          <ScalarProperty Name=\"{0}\" ColumnName=\"{1}\" />", propertyName, columnName);
                }

                if (rel.B.Type == cls && rel.NeedsPositionStorage(RelationEndRole.B) && rel.B.Navigator != null)
                {
                    propertyName = Construct.ListPositionPropertyName(rel.B);
                    columnName = Construct.ListPositionColumnName(rel.A);
                    this.WriteLine("          <ScalarProperty Name=\"{0}\" ColumnName=\"{1}\" />", propertyName, columnName);
                }
            }

            foreach (var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                ModelMslEntityTypeMappingScalarProperty.Call(Host, ctx, prop, String.Empty);
            }

            foreach (var prop in cls.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                ModelMslEntityTypeMappingComplexProperty.Call(Host, ctx, prop, String.Empty);
            }
        }

        protected virtual string GetStoreEntitySet()
        {
            var mapping = cls.GetTableMapping();
            if (mapping == TableMapping.TPT)
            {
                return cls.Name;
            }
            else if (mapping == TableMapping.TPH)
            {
                return cls.GetRootClass().Name;
            }
            else
            {
                throw new NotSupportedException(string.Format("Unknown table mapping: {0}", mapping));
            }
        }

        protected virtual string GetMappingTypeName()
        {
            var name = "Model." + cls.Name + "EfImpl";
            // IsTypeOf and TPH condition are incompatible
            return cls.GetTableMapping() == TableMapping.TPT ? string.Format("IsTypeOf({0})", name) : name;
        }
    }
}
