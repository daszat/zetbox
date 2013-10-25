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
    /// <summary>
    /// Which kind of host to be
    /// </summary>
    public enum HostType
    {
        /// <summary>
        /// Client host type
        /// </summary>
        Client,
        /// <summary>
        /// Server host type
        /// </summary>
        Server,
        /// <summary>
        /// A special type of server
        /// </summary>
        AspNetService,
        /// <summary>
        /// A web client
        /// </summary>
        AspNetClient,
        /// <summary>
        /// no predefined personality. This is used only in very rare cases.
        /// </summary>
        None,
        /// <summary>
        /// hosttype for cli tools
        /// </summary>
        All,
    }
}
