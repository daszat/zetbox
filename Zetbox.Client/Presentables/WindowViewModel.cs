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

namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    /// <summary>
    /// A top-level working space. This is the user's "Unit of Work".
    /// </summary>
    public abstract class WindowViewModel : ViewModel
    {
        public new delegate WindowViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public WindowViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public WindowViewModel(bool designMode)
            : base(designMode)
        {
        }

        private bool _show = true;
        public bool Show
        {
            get
            {
                return _show;
            }
            set
            {
                if (!value && !CanClose()) return;
                _show = value;
                OnPropertyChanged("Show");
            }
        }

        /// <summary>
        /// Views should call this before closing
        /// </summary>
        /// <returns></returns>
        public virtual bool CanClose()
        {
            return true;
        }
    }
}
