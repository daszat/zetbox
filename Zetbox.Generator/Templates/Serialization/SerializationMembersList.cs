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

namespace Zetbox.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A list of names of members to serialize.
    /// </summary>
    /// This will be filled while building a data type. In the end, the 
    /// ToStream and FromStream methods are built from this list.
    // TODO: enhance with versioning and type information
    public class SerializationMembersList
        : List<SerializationMember>
    {
        /// <summary>
        /// Implicitely creates a <see cref="SerializationMember"/> to store 
        /// the given parameters.
        /// </summary>
        public void Add(string templatename, SerializerType type, string xmlns, string xmlname, params object[] templateparams)
        {
            this.Add(new SerializationMember(templatename, type, xmlns, xmlname, templateparams));
        }

        /// <summary>
        /// Add a SimplePropertySerialization entry for this membername
        /// </summary>
        public void Add(SerializerType type, string xmlns, string xmlname, string memberType, string memberName)
        {
            this.Add(new SerializationMember("Serialization.SimplePropertySerialization", type, xmlns, xmlname, memberType, memberName));
        }
    }
}
