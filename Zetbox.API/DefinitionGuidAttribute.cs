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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This attribute is used to map from a class or property definition back to the instance that defined it.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class DefinitionGuidAttribute : Attribute
    {
        /// <summary>
        /// Save the Guid as string for minimal parsing overhead
        /// </summary>
        private readonly string guid;

        public DefinitionGuidAttribute(string guid)
        {
            this.guid = guid;
        }

        public Guid Guid
        {
            get { return new Guid(guid); }
        }
    }
}