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

namespace Zetbox.Client.Presentables.Debugger
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;

    [ViewModelDescriptor]
    public class DebuggerWindowViewModel
        : WindowViewModel
    {
        public new delegate DebuggerWindowViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public DebuggerWindowViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
            
        }
        
        public override string Name
        {
            get { return DebuggerWindowViewModelResources.Name; }
        }

        private ZetboxContextDebuggerViewModel _contextDebugger;
        public ZetboxContextDebuggerViewModel ContextDebugger
        {
            get
            {
                if (_contextDebugger == null)
                {
                    _contextDebugger = ViewModelFactory.CreateViewModel<ZetboxContextDebuggerViewModel.Factory>().Invoke(DataContext, this);
                }
                return _contextDebugger;
            }
        }

        private CacheDebuggerViewModel _cacheDebugger;
        public CacheDebuggerViewModel CacheDebugger
        {
            get
            {
                if (_cacheDebugger == null)
                {
                    _cacheDebugger = ViewModelFactory.CreateViewModel<CacheDebuggerViewModel.Factory>().Invoke(DataContext, this);
                }
                return _cacheDebugger;
            }
        }

        private RequestDebuggerViewModel _requestDebugger;
        public RequestDebuggerViewModel RequestDebugger
        {
            get
            {
                if (_requestDebugger == null)
                {
                    _requestDebugger = ViewModelFactory.CreateViewModel<RequestDebuggerViewModel.Factory>().Invoke(DataContext, this);
                }
                return _requestDebugger;
            }
        }

        private PerfMonDebuggerViewModel _perfMonDebugger;
        public PerfMonDebuggerViewModel PerfMonDebugger
        {
            get
            {
                if (_perfMonDebugger == null)
                {
                    _perfMonDebugger = ViewModelFactory.CreateViewModel<PerfMonDebuggerViewModel.Factory>().Invoke(DataContext, this);
                }
                return _perfMonDebugger;
            }
        }
    }
}
