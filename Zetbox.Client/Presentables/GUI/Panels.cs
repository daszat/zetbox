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
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using System.Collections.ObjectModel;

    public abstract class PanelViewModel : ViewModel
    {
        public new delegate PanelViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string title, IEnumerable<ViewModel> children);

        private string _title;
        private readonly ObservableCollection<ViewModel> _children;

        protected PanelViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string title, IEnumerable<ViewModel> children)
            : base(dependencies, dataCtx, parent)
        {
            this._children = new ObservableCollection<ViewModel>(children);
            _title = title;
        }

        public override string Name
        {
            get { return _title; }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Title");
                }
            }
        }

        public ObservableCollection<ViewModel> Children
        {
            get
            {
                return _children;
            }
        }
    }

    [ViewModelDescriptor]
    public class GroupBoxViewModel : PanelViewModel
    {
        public new delegate GroupBoxViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children);

        public GroupBoxViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children)
            : base(dependencies, dataCtx, parent, name, children)
        {
        }
    }

    [ViewModelDescriptor]
    public class TabControlViewModel : PanelViewModel
    {
        public new delegate TabControlViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children);

        public TabControlViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children)
            : base(dependencies, dataCtx, parent, name, children)
        {
        }
    }

    [ViewModelDescriptor]
    public class TabItemViewModel : PanelViewModel
    {
        public new delegate TabItemViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children);

        public TabItemViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children)
            : base(dependencies, dataCtx, parent, name, children)
        {
        }
    }
}
