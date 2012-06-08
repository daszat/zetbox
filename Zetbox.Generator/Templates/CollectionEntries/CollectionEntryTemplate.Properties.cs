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

namespace Zetbox.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract partial class CollectionEntryTemplate
    {
        /// <summary>
        /// The list of members to serialize
        /// </summary>
        protected Serialization.SerializationMembersList MembersToSerialize { get { return _MembersToSerialize; } }
        private Serialization.SerializationMembersList _MembersToSerialize = new Serialization.SerializationMembersList();

        protected virtual void ApplyIdPropertyTemplate()
        {
            Properties.IdProperty.Call(Host, ctx);
        }

        protected virtual void ApplyExportGuidPropertyTemplate()
        {
            Properties.ExportGuidProperty.Call(Host, ctx, this.MembersToSerialize, GetExportGuidBackingStoreReference());
        }

        protected abstract void ApplyAPropertyTemplate();
        protected abstract void ApplyBPropertyTemplate();

        protected abstract void ApplyAIndexPropertyTemplate();
        protected abstract void ApplyBIndexPropertyTemplate();
    }
}
