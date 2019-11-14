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
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Base;

    /// <summary>
    /// A data context without identity, which is useful for various administrative tasks.
    /// </summary>
    public interface IZetboxServerContext
        : IZetboxContext
    {
        /// <summary>
        /// The Pricipal of this context. May be null for administrative/system-level work.
        /// </summary>
        ZetboxPrincipal Pricipal { get; }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects.
        /// This method does not fire any events or methods on added/changed objects. 
        /// It also does not change any IChanged property.
        /// </summary>
        /// <remarks>
        /// <para>This method is used when restoring data from backups or when importing. 
        /// In these cases it is important, that the object's live-cycles do not start 
        /// here, thus no events are triggered.</para>
        /// <para>Only IDataObjects are counted.</para>
        /// </remarks>
        /// <returns>Number of affected Objects</returns>
        int SubmitRestore();

        /// <summary>
        /// Creates a native DbCommand object. This can be used to communicated with the database directly.
        /// </summary>
        /// <returns></returns>
        IDbCommand CreateDbCommand();

        /// <summary>
        /// Returns the Database SchemaProvider. Currently POSTGRESQL or MSSQL.
        /// </summary>
        string SchemaProvider { get; }
    }
}
