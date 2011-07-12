
namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using System.Collections;

    public partial class ModelSsdl
    {
        // ContextBound Objects are not allowed to have Generic Methods
        // See MSDN: http://msdn.microsoft.com/en-us/library/system.contextboundobject.aspx
        private static class ModelSsdlHelper
        {
            public static IEnumerable<P> RetrieveAndSortPropertiesOfType<P>(IEnumerable<Property> properties, Func<P, bool> predicate)
                where P : Property
            {
                return properties
                    .OfType<P>()
                    .OrderBy(p => p.ObjectClass.Name)
                    .ThenBy(p => p.Name)
                    .Where(p => predicate(p));
            }
        }

        protected virtual void ApplyEntityTypeColumnDefs(ObjectClass cls)
        {
            var relevantRelations = cls.GetRelations() // Managed by a cache
                .Where(r => (r.A.Type.ID == cls.ID && r.Storage == StorageType.MergeIntoA)
                            || (r.B.Type.ID == cls.ID && r.Storage == StorageType.MergeIntoB))
                .ToList()
                .OrderBy(r => r.GetAssociationName());

            EfModel.ModelSsdlEntityTypeColumnsRel.Call(
                Host,
                ctx,
                cls,
                relevantRelations,
                String.Empty,
                schemaProvider);

            EfModel.ModelSsdlEntityTypeColumns.Call(
                Host,
                ctx,
                ModelSsdlHelper.RetrieveAndSortPropertiesOfType<ValueTypeProperty>(cls.Properties, p => !p.IsList).Cast<Property>(),
                String.Empty,
                schemaProvider);
            EfModel.ModelSsdlEntityTypeColumns.Call(
                Host,
                ctx,
                ModelSsdlHelper.RetrieveAndSortPropertiesOfType<CompoundObjectProperty>(cls.Properties, p => !p.IsList).Cast<Property>(),
                String.Empty,
                schemaProvider);
        }

        protected virtual void ApplyEntityTypeColumnDefs(CompoundObjectProperty prop)
        {
            EfModel.ModelSsdlEntityTypeColumns.Call(
                Host,
                ctx,
                new Property[] { prop },
                String.Empty,
                schemaProvider);
        }
    }
}
