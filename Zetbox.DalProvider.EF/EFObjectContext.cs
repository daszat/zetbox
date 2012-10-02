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

namespace Zetbox.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Text;

    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;

    internal sealed class EfObjectContext : ObjectContext
    {
        public EfObjectContext(ZetboxConfig config)
            : base(BuildConnectionString(config), "Entities")
        {
        }

        /// <summary>
        /// Creates the Connectionstring.
        /// </summary>
        /// <returns></returns>
        private static string BuildConnectionString(ZetboxConfig config)
        {
            // Build connectionString
            var connectionString = config.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("metadata=res://Zetbox.Objects.EfImpl/Zetbox.Objects.EfImpl.Model.csdl|res://Zetbox.Objects.EfImpl/Zetbox.Objects.EfImpl.Model.msl|res://Zetbox.Objects.EfImpl/Zetbox.Objects.EfImpl.Model.{0}.ssdl;", connectionString.SchemaProvider);
            sb.AppendFormat("provider={0};", connectionString.DatabaseProvider);
            sb.AppendFormat("provider connection string='{0}'", connectionString.ConnectionString);

            return sb.ToString();
        }
    }
}
