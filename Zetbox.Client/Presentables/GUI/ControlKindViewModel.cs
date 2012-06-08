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

namespace Zetbox.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.GUI;

    [ViewModelDescriptor]
    public class ControlKindViewModel
        : DataObjectViewModel
    {
        public new delegate ControlKindViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ControlKind obj);

        public ControlKindViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, ControlKind obj)
            : base(dependencies, dataCtx, parent, obj)
        {
            Kind = obj;
        }

        public ControlKind Kind { get; private set; }

        private ReadOnlyObservableCollection<ControlKindViewModel> _childControlKinds = null;
        public ReadOnlyObservableCollection<ControlKindViewModel> ChildControlKinds
        {
            get
            {
                if (_childControlKinds == null)
                {
                    _childControlKinds = new ReadOnlyObservableCollection<ControlKindViewModel>(new ObservableCollection<ControlKindViewModel>(
                        Kind.ChildControlKinds
                            .OrderBy(i => i.Name)
                            .Select(i => ViewModelFactory.CreateViewModel<ControlKindViewModel.Factory>().Invoke(DataContext, this, i))));
                }
                return _childControlKinds;
            }
        }
    }
}
