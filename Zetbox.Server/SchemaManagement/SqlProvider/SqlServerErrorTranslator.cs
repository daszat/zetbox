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

namespace Zetbox.Server.SchemaManagement.SqlProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using System.Data.SqlClient;
    using Zetbox.API;
    using System.Threading.Tasks;

    public class SqlServerErrorTranslator : SqlErrorTranslator
    {
        public SqlServerErrorTranslator(IFrozenContext frozenCtx)
            : base(frozenCtx)
        {
        }

        public override async Task<Exception> Translate(Exception ex)
        {
            if (ex == null) throw new ArgumentNullException("ex");

            if (ex is SqlException)
            {
                return await TranslateSqlServerErrors((SqlException)ex);
            }
            else
            {
                return ex;
            }
        }

        private async Task<Exception> TranslateSqlServerErrors(SqlException ex)
        {
            if (ex.Errors.OfType<SqlError>().Any(e => e.Number == 2601))
            {
                return new UniqueConstraintViolationException((await ex.Errors.OfType<SqlError>().Where(e => e.Number == 2601).Select(async e => await ConstructUniqueConstraintDetail(e.Message)).WhenAll()).ToList());
            }
            else if (ex.Errors.OfType<SqlError>().Any(e => e.Number == 547))
            {
                return new FKViolationException((await ex.Errors.OfType<SqlError>().Where(e => e.Number == 547).Select(e => ConstructFKDetail(e.Message)).WhenAll()).ToList());
            }
            return ex;
        }
    }
}
