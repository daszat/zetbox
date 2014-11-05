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

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// A small immutable class to describe Identities and GroupMemberships independently of a Context.
    /// </summary>
    /// <remarks>All properties mirror the same properties on Zetbox.Base.Identity.</remarks>
    public sealed class ZetboxPrincipal
    {
        /// <summary>The ID of the Identity</summary>
        public int ID { get; private set; }
        /// <summary>The UserName of the Identity</summary>
        public string UserName { get; private set; }
        /// <summary>The DisplayName of the Identity</summary>
        public string DisplayName { get; private set; }

        /// <summary>The Groups of the Identity</summary>
        public ReadOnlyCollection<ZetboxPrincipalGroup> Groups { get; private set; }

        public ZetboxPrincipal(int id, string userName, string displayName, IEnumerable<ZetboxPrincipalGroup> groups)
        {
            ID = id;
            UserName = userName;
            DisplayName = displayName;
            // duplicate to make it private
            Groups = groups.ToList().AsReadOnly();
        }
    }

    /// <summary>
    /// A small immutable class to describe Groups independently of a Context.
    /// </summary>
    public sealed class ZetboxPrincipalGroup
    {
        /// <summary>The ID of the Group</summary>
        public int ID { get; private set; }
        /// <summary>The Name of the Group</summary>
        public string Name { get; private set; }
        /// <summary>The ExportGuid of the Group</summary>
        public Guid ExportGuid { get; private set; }

        public ZetboxPrincipalGroup(int id, string name, Guid exportGuid)
        {
            ID = id;
            Name = name;
            ExportGuid = exportGuid;
        }
    }
}

