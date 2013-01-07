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

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using System.Collections;
    using System.Data;

    public partial class ModelSsdl
    {
        private string _int32DbType;
        protected string int32DbType
        {
            get
            {
                InitInt32Type();
                return _int32DbType;
            }
        }

        private void InitInt32Type()
        {
            if (_int32DbType == null)
                _int32DbType = schemaProvider.DbTypeToNative(DbType.Int32);
        }

        private string _guidDbType;
        protected string guidDbType
        {
            get
            {
                InitGuidType();
                return _guidDbType;
            }
        }

        private void InitGuidType()
        {
            if (_guidDbType == null)
                _guidDbType = schemaProvider.DbTypeToNative(DbType.Guid);
        }

        private string _nvarcharDbType;
        protected string nvarcharDbType
        {
            get
            {
                InitnvarcharType();
                return _nvarcharDbType;
            }
        }

        private void InitnvarcharType()
        {
            if (_nvarcharDbType == null)
                _nvarcharDbType = schemaProvider.DbTypeToNative(DbType.String);
        }

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

            if (cls.GetTableMapping() == TableMapping.TPH)
            {
                foreach (var subCls in cls.SubClasses)
                {
                    ApplyEntityTypeColumnDefs(subCls);
                }
            }
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

        protected IEnumerable<ObjectClass> GetEntityTypes()
        {
            
            return ctx.GetQuery<ObjectClass>()
                .ToList()
                .Where(c => c.GetTableMapping() == TableMapping.TPT || (c.GetTableMapping() == TableMapping.TPH && c.BaseObjectClass == null))
                .OrderBy(cls => cls.Name);
        }
    }
}
