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

namespace Zetbox.API.Server
{
    using System;
    using System.Data.Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Base;
    using Zetbox.API.SchemaManagement;
    using Zetbox.App.Extensions;

    public interface ISqlErrorTranslator
    {
        Exception Translate(Exception ex);
    }

    public abstract class SqlErrorTranslator : ISqlErrorTranslator
    {
        private static readonly object _lock = new object();
        private IFrozenContext _frozenCtx;

        public SqlErrorTranslator(IFrozenContext frozenCtx)
        {
            _frozenCtx = frozenCtx;
        }

        public abstract Exception Translate(Exception ex);

        private Dictionary<string, Relation> _relations;
        private void EnsureRelations()
        {
            if (_relations != null) return;
            lock (_lock)
            {
                if (_relations == null)
                {
                    _relations = _frozenCtx.GetQuery<Relation>()
                        .ToDictionary(r => r.GetAssociationName());
                }
            }
        }

        private Dictionary<string, IndexConstraint> _index;
        private void EnsureIndex()
        {
            if (_index != null) return;
            lock (_lock)
            {
                if (_index == null)
                {
                    _index = _frozenCtx.GetQuery<IndexConstraint>()
                        .Where(i => i.IsUnique)
                        .Where(i => i.Constrained is ObjectClass) // Only object classes can leed to an index in our database. Interfaces are just a "template"
                        .ToDictionary(i =>
                        {
                            var objClass = (ObjectClass)i.Constrained;
                            if (objClass.GetTableMapping() == TableMapping.TPH)
                            {
                                objClass = objClass.GetRootClass();
                            }
                            var columns = Construct.GetUCColNames(i);
                            // GetTableRef needs an open ISchemaProvider!
                            // Overkill -> construct table name for it's own
                            return Construct.IndexName(objClass.TableName, columns);
                        });
                }
            }
        }

        protected FKViolationExceptionDetail ConstructFKDetail(string msg)
        {
            EnsureRelations();
            var result = new FKViolationExceptionDetail() { DatabaseError = msg };
            var rel = _relations.FirstOrDefault(kv => msg.Contains(kv.Key)).Value; // KeyValuePair<> is a struct
            if (rel != null)
            {
                result.RelGuid = rel.ExportGuid;
            }
            return result;
        }

        protected UniqueConstraintViolationExceptionDetail ConstructUniqueConstraintDetail(string msg)
        {
            EnsureIndex();
            var result = new UniqueConstraintViolationExceptionDetail() { DatabaseError = msg };
            var idx = _index.FirstOrDefault(kv => msg.Contains(kv.Key)).Value; // KeyValuePair<> is a struct
            if (idx != null)
            {
                result.IdxGuid = idx.ExportGuid;
            }
            return result;
        }
    }
}
