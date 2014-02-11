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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using Zetbox.API;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.FilterViewModels;

    public partial class InstanceListViewModel
    {
        #region DragDrop
        public virtual bool CanDrop
        {
            get
            {
                return false;
            }
        }

        public virtual bool OnDrop(object data)
        {
            return false;
        }

        public virtual object DoDragDrop()
        {
            var lst = SelectedItems
                .Where(i => i.ObjectState.In(DataObjectState.Unmodified, DataObjectState.Modified, DataObjectState.New))
                .Select(i => i.Object)
                .Cast<IDataObject>()
                .ToArray();
            return lst.Length > 0 ? lst : null;
        }
        #endregion    
    }
}
