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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;
    using System.Collections;
    using System.Threading.Tasks;

    public interface ISelectionTaskViewModel
    {
        string Name { get; }

        string Title { get; }
        string Tooltip { get; }
        string SelectionType { get; }

        ReadOnlyCollection<CommandViewModel> AdditionalActions { get; }

        IEnumerable Items { get; }
        IList SelectedItems { get; }

        ICommandViewModel ChooseCommand { get; }
        ICommandViewModel CancelCommand { get; }
        ICommandViewModel SelectAndChooseCommand { get; }
    }

    public abstract class AbstractSelectionTaskViewModel<TViewModel>
       : WindowViewModel, IRefreshCommandListener
        where TViewModel : ViewModel
    {
        private Action<IEnumerable<TViewModel>> _callback;
        private ReadOnlyCollection<CommandViewModel> _additionalActions;

        public AbstractSelectionTaskViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Action<IEnumerable<TViewModel>> callback, IList<CommandViewModel> additionalActions)
            : base(appCtx, dataCtx, parent)
        {
            _callback = callback;
            _additionalActions = new ReadOnlyCollection<CommandViewModel>(additionalActions ?? new CommandViewModel[] { });
        }

        #region Public interface

        public ReadOnlyCollection<CommandViewModel> AdditionalActions
        {
            get
            {
                return _additionalActions;
            }
        }

        private ICommandViewModel _ChooseCommand = null;
        public ICommandViewModel ChooseCommand
        {
            get
            {
                if (_ChooseCommand == null)
                {
                    _ChooseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        DataObjectSelectionTaskViewModelResources.Choose,
                        DataObjectSelectionTaskViewModelResources.Choose_Tooltip,
                        () => Choose(SelectedItems.Cast<TViewModel>()),
                        () => Task.FromResult(SelectedItems != null && SelectedItems.Cast<TViewModel>().Count() > 0),
                        null);
                }
                return _ChooseCommand;
            }
        }

        public Task Choose(IEnumerable<TViewModel> items)
        {
            if (items != null && items.Count() > 0)
            {
                _callback(items);
                Show = false;
            }

            return Task.CompletedTask;
        }

        private ICommandViewModel _CancelCommand = null;
        public ICommandViewModel CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        DataObjectSelectionTaskViewModelResources.Cancel,
                        DataObjectSelectionTaskViewModelResources.Cancel_Tooltip,
                        Cancel,
                        null,
                        null);
                }
                return _CancelCommand;
            }
        }

        public Task Cancel()
        {
            _callback(null);
            Show = false;

            return Task.CompletedTask;
        }

        private ICommandViewModel _SelectAndChooseCommand = null;
        public ICommandViewModel SelectAndChooseCommand
        {
            get
            {
                if (_SelectAndChooseCommand == null)
                {
                    _SelectAndChooseCommand = ViewModelFactory.CreateViewModel<SimpleItemCommandViewModel<TViewModel>.Factory>().Invoke(
                        DataContext,
                        this,
                        DataObjectSelectionTaskViewModelResources.Choose,
                        DataObjectSelectionTaskViewModelResources.Choose_Tooltip,
                        (items) => Choose(items)
                    );
                }
                return _SelectAndChooseCommand;
            }
        }

        public abstract IEnumerable Items { get; }
        public abstract IList SelectedItems { get; }
        public abstract void Refresh();

        private string _title;
        public string Title
        {
            get
            {
                if (_title == null)
                {
                    _title = DataObjectSelectionTaskViewModelResources.SimpleName;
                }
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string _tooltip;
        public string Tooltip
        {
            get
            {
                if (_tooltip == null)
                {
                    _tooltip = Name;
                }
                return _tooltip;
            }
            set
            {
                if (_tooltip != value)
                {
                    _tooltip = value;
                    OnPropertyChanged("Tooltip");
                }
            }
        }

        public abstract string SelectionType { get; set; }

        #endregion
    }

    public class DataObjectSelectionTaskEventArgs : EventArgs
    {
        public DataObjectSelectionTaskEventArgs(DataObjectSelectionTaskViewModel vmdl)
        {
            TaskViewModel = vmdl;
        }

        public DataObjectSelectionTaskViewModel TaskViewModel { get; private set; }
    }

    public delegate System.Threading.Tasks.Task DataObjectSelectionTaskCreatedEventHandler(object sender, DataObjectSelectionTaskEventArgs e);

    [ViewModelDescriptor]
    public class DataObjectSelectionTaskViewModel
        : AbstractSelectionTaskViewModel<DataObjectViewModel>, ISelectionTaskViewModel
    {
        public new delegate DataObjectSelectionTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            ObjectClass type,
            Func<IQueryable> qry,
            Action<IEnumerable<DataObjectViewModel>> callback,
            IList<CommandViewModel> additionalActions);

        public DataObjectSelectionTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ObjectClass type,
            Func<IQueryable> qry,
            Action<IEnumerable<DataObjectViewModel>> callback,
            IList<CommandViewModel> additionalActions)
            : base(appCtx, dataCtx, parent, callback, additionalActions)
        {
            ListViewModel = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(dataCtx, this, type, qry);
            ListViewModel.AllowAddNew = true;
            ListViewModel.ObjectCreated += ListViewModel_ObjectCreated;
            ListViewModel.ViewMethod = InstanceListViewMethod.Details;

            ListViewModel.DefaultCommand = this.ChooseCommand;

            foreach (var cmd in AdditionalActions)
            {
                ListViewModel.Commands.Add(cmd);
            }
        }

        async Task ListViewModel_ObjectCreated(IDataObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            // Same like choose
            var mdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj);
            await Choose(new[] { mdl });
        }

        public InstanceListViewModel ListViewModel { get; private set; }

        public override void Refresh()
        {
            ListViewModel.Refresh();
        }

        public override IEnumerable Items
        {
            get { return ListViewModel.Instances; }
        }

        public override IList SelectedItems
        {
            get { return ListViewModel.SelectedItems; }
        }

        public override string Name
        {
            get { return string.Format(DataObjectSelectionTaskViewModelResources.Name, ListViewModel.DataTypeViewModel.DescribedType); }
        }

        private string _selectionType;
        public override string SelectionType
        {
            get
            {
                if (_selectionType == null)
                {
                    _selectionType = ListViewModel.DataTypeViewModel.DescribedType;
                }
                return _selectionType;
            }
            set
            {
                if (_selectionType != value)
                {
                    _selectionType = value;
                    OnPropertyChanged("SelectionType");
                }
            }
        }
    }

    [ViewModelDescriptor]
    public class SimpleSelectionTaskViewModel
        : AbstractSelectionTaskViewModel<ViewModel>, ISelectionTaskViewModel
    {
        public new delegate SimpleSelectionTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            IEnumerable<ViewModel> items,
            Action<IEnumerable<ViewModel>> callback,
            IList<CommandViewModel> additionalActions);

        public SimpleSelectionTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            IEnumerable<ViewModel> items,
            Action<IEnumerable<ViewModel>> callback,
            IList<CommandViewModel> additionalActions)
            : base(appCtx, dataCtx, parent, callback, additionalActions)
        {
            if (items == null) throw new ArgumentNullException("items");
            _items = items;
        }

        public override void Refresh()
        {
            // nothing to refresh
        }

        private IEnumerable<ViewModel> _items;
        public override IEnumerable Items { get { return _items; } }

        private List<ViewModel> _selectedItems = new List<ViewModel>();
        public override IList SelectedItems { get { return _selectedItems; } }

        public override string Name
        {
            get { return DataObjectSelectionTaskViewModelResources.SimpleName; }
        }

        private string _selectionType;
        public override string SelectionType
        {
            get
            {
                return _selectionType;
            }
            set
            {
                if (_selectionType != value)
                {
                    _selectionType = value;
                    OnPropertyChanged("SelectionType");
                }
            }
        }
    }
}
