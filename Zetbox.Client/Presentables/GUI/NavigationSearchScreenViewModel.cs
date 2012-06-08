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
    using Zetbox.API.Common;
    using Zetbox.App.GUI;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.App.Base;

    [ViewModelDescriptor]
    public class NavigationSearchScreenViewModel
        : NavigationScreenViewModel
    {
        public new delegate NavigationSearchScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationSearchScreen screen);

        public NavigationSearchScreenViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, Func<IZetboxContext> ctxFactory, ViewModel parent, NavigationScreen screen)
            : base(dependencies, dataCtx, parent, screen)
        {
            _ctxFactory = ctxFactory;
        }

        public new NavigationSearchScreen Screen { get { return (NavigationSearchScreen)base.Screen; } }

        private readonly Func<IZetboxContext> _ctxFactory;

        protected virtual Func<IQueryable> InitializeQueryFactory()
        {
            return _queryFactory;
        }

        private Func<IQueryable> _queryFactory;
        public Func<IQueryable> QueryFactory
        {
            get
            {
                if (_queryFactory == null)
                {
                    _queryFactory = InitializeQueryFactory();
                }
                return _queryFactory;
            }
            set
            {
                if (_queryFactory != value)
                {
                    _queryFactory = value;
                    OnPropertyChanged("QueryFactory");

                    if (_listViewModel != null)
                    {
                        _listViewModel = null;
                        OnPropertyChanged("ListViewModel");
                    }
                }
            }
        }

        private ObjectClass _type;
        public ObjectClass Type
        {
            get
            {
                if (_type == null)
                {
                    _type = Screen.Type;
                }
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        private InstanceListViewModel _listViewModel;
        public InstanceListViewModel ListViewModel
        {
            get
            {
                EnsureListViewModel();
                return _listViewModel;
            }
        }

        private void EnsureListViewModel()
        {
            if (_listViewModel == null)
            {
                if (Type == null) throw new InvalidOperationException("Type is not set yet!");

                _listViewModel = ViewModelFactory
                    .CreateViewModel<InstanceListViewModel.Factory>()
                    .Invoke(DataContext, this, _ctxFactory, Type, QueryFactory);

                InitializeListViewModel(_listViewModel);
            }
        }

        protected virtual void InitializeListViewModel(InstanceListViewModel mdl)
        {
            var screen = this.Screen;

            if (screen.AllowAddNew.HasValue) mdl.AllowAddNew = screen.AllowAddNew.Value;
            if (screen.AllowDelete.HasValue) mdl.AllowDelete = screen.AllowDelete.Value;
            if (screen.AllowSelectColumns.HasValue) mdl.AllowSelectColumns = screen.AllowSelectColumns.Value;
            if (screen.AllowUserFilter.HasValue) mdl.AllowUserFilter = screen.AllowUserFilter.Value;
            if (screen.EnableAutoFilter.HasValue) mdl.EnableAutoFilter = screen.EnableAutoFilter.Value;
            if (screen.IsEditable.HasValue) mdl.IsEditable = screen.IsEditable.Value;
            if (screen.IsMultiselect.HasValue) mdl.IsMultiselect = screen.IsMultiselect.Value;
            if (screen.RespectRequiredFilter.HasValue) mdl.RespectRequiredFilter = screen.RespectRequiredFilter.Value;
            if (screen.ShowFilter.HasValue) mdl.ShowFilter = screen.ShowFilter.Value;
            if (screen.ShowMasterDetail.HasValue) mdl.ShowMasterDetail = screen.ShowMasterDetail.Value;
            if (screen.ShowOpenCommand.HasValue) mdl.ShowOpenCommand = screen.ShowOpenCommand.Value;
            if (screen.ShowRefreshCommand.HasValue) mdl.ShowRefreshCommand = screen.ShowRefreshCommand.Value;

            if (!string.IsNullOrEmpty(screen.InitialSort))
            {
                if (screen.InitialSortDirection.HasValue)
                {
                    mdl.SetInitialSort(screen.InitialSort, (System.ComponentModel.ListSortDirection)screen.InitialSortDirection.Value);
                }
                else
                {
                    mdl.SetInitialSort(screen.InitialSort);
                }
            }

            if (screen.RequestedWorkspaceKind != null) mdl.RequestedWorkspaceKind = screen.RequestedWorkspaceKind;
            if (screen.RequestedEditorKind != null) mdl.RequestedEditorKind = screen.RequestedEditorKind;
            if (screen.ViewMethod.HasValue) mdl.ViewMethod = screen.ViewMethod.Value;
        }
    }
}
