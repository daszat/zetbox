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

namespace Zetbox.Server.SchemaManagement.NpgsqlProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using Npgsql;
    using Zetbox.API;

    public class PostgresqlErrorTranslator : ISqlErrorTranslator
    {
        public Exception Translate(Exception ex)
        {
            if (ex == null) throw new ArgumentNullException("ex");

            if (ex is NpgsqlException)
            {
                return TranslateNpgsqlErrors((NpgsqlException)ex);
            }
            else
            {
                return ex;
            }
        }

        private Exception TranslateNpgsqlErrors(NpgsqlException ex)
        {
            if (ex.Errors.OfType<NpgsqlError>().Any(e => e.Code == "23505"))
            {
                return new UniqueConstraintViolationException();
            }
            else if (ex.Errors.OfType<NpgsqlError>().Any(e => e.Code == "23503"))
            {
                return new FKViolationException();
            }
            return ex;
        }
    }
}
