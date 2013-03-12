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

namespace Zetbox.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Proxy object for datagrids who are able to create new instances
    /// </summary>
    [DebuggerDisplay("Proxy for = {Object}")]
    public class DataObjectViewModelProxy
    {
        public DataObjectViewModelProxy()
        {
        }

        public int ID { get { return Object != null ? Object.ID : -1; } }

        public DataObjectViewModel Object { get; set; }

        public System.Drawing.Image Icon
        {
            get { return Object != null ? Object.Icon : null; }
        }

        public Highlight Highlight
        {
            get
            {
                return Object != null ? Object.Highlight : Highlight.None;
            }
        }

        /// <summary>
        /// TODO: Add propper implementation
        /// </summary>
        public Highlight HighlightAsync
        {
            get
            {
                return this.Highlight;
            }
        }

        public override string ToString()
        {
            return "Proxy for " + (Object == null ? "(null)" : Object.ToString());
        }
    }
}
