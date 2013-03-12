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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;

    
    [ViewModelDescriptor]
    public class GroupMembershipViewModel : DataObjectViewModel
    {
        public new delegate GroupMembershipViewModel Factory(IZetboxContext dataCtx, ViewModel parent, GroupMembership obj);

        public GroupMembershipViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, GroupMembership obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.GroupMembership = obj;
        }

        protected readonly GroupMembership GroupMembership;

        public override Highlight Highlight
        {
            get
            {
                return new Highlight(null, "#000080", System.Drawing.FontStyle.Regular, null);
            }
        }

        public override Highlight HighlightAsync
        {
            get
            {
                return this.Highlight;
            }
        }
    }
}
