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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    
    public class AssemblyViewModel
        : DataObjectViewModel
    {
        public new delegate DataObjectViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Assembly obj);

        private Assembly _assembly;

        public AssemblyViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Assembly obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            _assembly = obj;
        }

        #region Public Interface

        private ReadOnlyProjectedList<Type, SystemTypeViewModel> _typeList;
        public IReadOnlyList<SystemTypeViewModel> Types
        {
            get
            {
                if (_typeList == null)
                {
                    _typeList = new ReadOnlyProjectedList<Type, SystemTypeViewModel>(
                        System.Reflection.Assembly.ReflectionOnlyLoad(_assembly.Name).GetTypes(),
                        t => ViewModelFactory.CreateViewModel<SystemTypeViewModel.Factory>().Invoke(DataContext, this, t),
                        mdl => mdl.Value);
                }
                return _typeList;
            }
        }

        #endregion
    }
}
