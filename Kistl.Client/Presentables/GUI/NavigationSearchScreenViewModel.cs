
namespace Kistl.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.App.GUI;
    using Kistl.App.Extensions;
    using Kistl.Client.Presentables.KistlBase;
    using Kistl.App.Base;

    [ViewModelDescriptor]
    public class NavigationSearchScreenViewModel
        : NavigationScreenViewModel
    {
        public new delegate NavigationSearchScreenViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationSearchScreen screen);

        public NavigationSearchScreenViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, Func<IKistlContext> ctxFactory, ViewModel parent, NavigationScreen screen)
            : base(dependencies, dataCtx, parent, screen)
        {
            _ctxFactory = ctxFactory;
        }

        public new NavigationSearchScreen Screen { get { return (NavigationSearchScreen)base.Screen; } }

        private readonly Func<IKistlContext> _ctxFactory;

        private Func<IQueryable> _queryFactory;
        public Func<IQueryable> QueryFactory
        {
            get
            {
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

        private ControlKind _requestedEditorKind;
        public ControlKind RequestedEditorKind
        {
            get
            {
                return _requestedEditorKind;
            }
            set
            {
                if (_requestedEditorKind != value)
                {
                    _requestedEditorKind = value;
                    OnPropertyChanged("RequestedEditorKind");
                }
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
                    .Invoke(DataContext, this, _ctxFactory, Type, _queryFactory);

                var screen = this.Screen;

                if (screen.AllowAddNew.HasValue) _listViewModel.AllowAddNew = screen.AllowAddNew.Value;
                if (screen.AllowDelete.HasValue) _listViewModel.AllowDelete = screen.AllowDelete.Value;
                if (screen.AllowSelectColumns.HasValue) _listViewModel.AllowSelectColumns = screen.AllowSelectColumns.Value;
                if (screen.AllowUserFilter.HasValue) _listViewModel.AllowUserFilter = screen.AllowUserFilter.Value;
                if (screen.EnableAutoFilter.HasValue) _listViewModel.EnableAutoFilter = screen.EnableAutoFilter.Value;
                if (screen.IsEditable.HasValue) _listViewModel.IsEditable = screen.IsEditable.Value;
                if (screen.IsMultiselect.HasValue) _listViewModel.IsMultiselect = screen.IsMultiselect.Value;
                if (screen.RespectRequiredFilter.HasValue) _listViewModel.RespectRequiredFilter = screen.RespectRequiredFilter.Value;
                if (screen.ShowFilter.HasValue) _listViewModel.ShowFilter = screen.ShowFilter.Value;
                if (screen.ShowMasterDetail.HasValue) _listViewModel.ShowMasterDetail = screen.ShowMasterDetail.Value;
                if (screen.ShowOpenCommand.HasValue) _listViewModel.ShowOpenCommand = screen.ShowOpenCommand.Value;
                if (screen.ShowRefreshCommand.HasValue) _listViewModel.ShowRefreshCommand = screen.ShowRefreshCommand.Value;

                if (!string.IsNullOrEmpty(screen.InitialSort))
                {
                    if (screen.InitialSortDirection.HasValue)
                    {
                        _listViewModel.SetInitialSort(screen.InitialSort, (System.ComponentModel.ListSortDirection)screen.InitialSortDirection.Value);
                    }
                    else
                    {
                        _listViewModel.SetInitialSort(screen.InitialSort);
                    }
                }

                //_listViewModel.RequestedWorkspaceKind = _requestedEditorKind;
                _listViewModel.RequestedEditorKind = _requestedEditorKind;
                _listViewModel.ViewMethod = InstanceListViewMethod.List;
            }
        }
    }
}
