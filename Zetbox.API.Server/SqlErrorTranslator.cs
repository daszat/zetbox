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
    using System.Threading.Tasks;
    using System.Threading;

    public interface ISqlErrorTranslator
    {
        Task<Exception> Translate(Exception ex);
    }

    public abstract class SqlErrorTranslator : ISqlErrorTranslator
    {
        private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private IFrozenContext _frozenCtx;

        public SqlErrorTranslator(IFrozenContext frozenCtx)
        {
            _frozenCtx = frozenCtx;
        }

        public abstract Task<Exception> Translate(Exception ex);

        private Dictionary<string, Relation> _relations;
        private async Task EnsureRelations()
        {
            if (_relations != null) return;
            await _lock.WaitAsync();
            try
            {
                if (_relations == null)
                {
                    _relations = _frozenCtx.GetQuery<Relation>()
                        .ToDictionary(r => r.GetAssociationName());
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        private Dictionary<string, IndexConstraint> _index;
        private async Task EnsureIndex()
        {
            if (_index != null) return;
            await _lock.WaitAsync();
            try
            {
                if (_index == null)
                {
                    var tmp = _frozenCtx.GetQuery<IndexConstraint>()
                        .Where(i => i.IsUnique)
                        .Where(i => i.Constrained is ObjectClass) // Only object classes can leed to an index in our database. Interfaces are just a "template"
                        .ToDictionary(async i =>
                        {
                            var objClass = (ObjectClass)i.Constrained;
                            if (objClass.GetTableMapping() == TableMapping.TPH)
                            {
                                objClass = objClass.GetRootClass();
                            }
                            var columns = await Construct.GetUCColNames(i);
                            // GetTableRef needs an open ISchemaProvider!
                            // Overkill -> construct table name for it's own
                            return Construct.IndexName(objClass.TableName, columns);
                        });
                    _index = new Dictionary<string, IndexConstraint>();
                    foreach (var kv in tmp)
                    {
                        var key = await kv.Key;
                        _index[key] = kv.Value;
                    }
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        protected async Task<FKViolationExceptionDetail> ConstructFKDetail(string msg)
        {
            await EnsureRelations();
            var result = new FKViolationExceptionDetail() { DatabaseError = msg };
            var rel = _relations.FirstOrDefault(kv => msg.Contains(kv.Key)).Value; // KeyValuePair<> is a struct
            if (rel != null)
            {
                result.RelGuid = rel.ExportGuid;
            }
            return result;
        }

        protected async Task<UniqueConstraintViolationExceptionDetail> ConstructUniqueConstraintDetail(string msg)
        {
            await EnsureIndex();
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
