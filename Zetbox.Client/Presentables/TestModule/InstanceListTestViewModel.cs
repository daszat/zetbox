﻿// This file is part of zetbox.
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.App.GUI;
using Zetbox.Client.Presentables.GUI;
using Zetbox.Client.Presentables.ZetboxBase;

namespace Zetbox.Client.Presentables.TestModule
{
    [ViewModelDescriptor]
    public class InstanceListTestViewModel : NavigationScreenViewModel
    {
        public new delegate InstanceListTestViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);
        private readonly Func<IZetboxContext> _ctxFactory;

        public InstanceListTestViewModel(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, dataCtx, parent, screen)
        {
            _ctxFactory = ctxFactory;
        }

        private InstanceListViewModel _TestList = null;
        public InstanceListViewModel TestList
        {
            get
            {
                if (_TestList == null)
                {
                    var qry = DataContext
                        .GetQuery<ObjectClass>()
                        .OrderBy(cls => cls.Name);

                    _TestList = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        _ctxFactory,
                        typeof(ObjectClass).GetObjectClass(FrozenContext),
                        () => qry);
                    _TestList.EnableAutoFilter = false;
                    _TestList.ShowCommands = false;
                    _TestList.ShowFilter = false;
                    _TestList.ViewMethod = InstanceListViewMethod.Details;
                }
                return _TestList;
            }
        }
    }
}