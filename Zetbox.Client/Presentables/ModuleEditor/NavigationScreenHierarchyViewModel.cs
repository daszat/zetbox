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

namespace Zetbox.Client.Presentables.ModuleEditor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;
    using System.ComponentModel;

    [ViewModelDescriptor]
    public class NavigationScreenHierarchyViewModel : ViewModel, IRefreshCommandListener, IDeleteCommandParameter, IOpenCommandParameter, INewCommandParameter
    {
        public new delegate NavigationScreenHierarchyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Module module);

        public NavigationScreenHierarchyViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Module module, Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent)
        {
            this.ctxFactory = ctxFactory;
            this.Module = module;
        }

        protected readonly Func<IZetboxContext> ctxFactory;
        public Module Module { get; private set; }

        public override string Name
        {
            get { return "NavigationScreen Hierarchy"; }
        }

        public override string ToString()
        {
            return Name;
        }

        private NavigationEntryEditorViewModel _selectedItem;
        public NavigationEntryEditorViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        private ReadOnlyObservableCollection<NavigationEntryEditorViewModel> _rootScreens = null;
        public ReadOnlyObservableCollection<NavigationEntryEditorViewModel> RootScreens
        {
            get
            {
                if (_rootScreens == null)
                {
                    var moduleID = Module.ID;
                    _rootScreens = new ReadOnlyObservableCollection<NavigationEntryEditorViewModel>(new ObservableCollection<NavigationEntryEditorViewModel>(
                        DataContext.GetQuery<NavigationScreen>()
                        .Where(i => i.Module.ID == moduleID)
                        .Where(i => i.Parent == null)
                        .OrderBy(i => i.Title)
                        .ToList()
                        .Select(i => ViewModelFactory.CreateViewModel<NavigationEntryEditorViewModel.Factory>().Invoke(DataContext, this, i))));
                }
                return _rootScreens;
            }
        }

        #region Commands
        private RefreshCommand _RefreshCommand;
        public ICommandViewModel RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(DataContext, this);
                }
                return _RefreshCommand;
            }
        }

        private OpenDataObjectCommand _OpenCommand;
        public ICommandViewModel OpenCommand
        {
            get
            {
                if (_OpenCommand == null)
                {
                    _OpenCommand = ViewModelFactory.CreateViewModel<OpenDataObjectCommand.Factory>().Invoke(
                        DataContext,
                        this);
                }
                return _OpenCommand;
            }
        }

        public void Open()
        {
            if (OpenCommand.CanExecute(null))
                OpenCommand.Execute(null);
        }

        private NewDataObjectCommand _NewCommand;
        public ICommandViewModel NewCommand
        {
            get
            {
                if (_NewCommand == null)
                {
                    _NewCommand = ViewModelFactory.CreateViewModel<NewDataObjectCommand.Factory>().Invoke(
                        DataContext,
                        this,
                        typeof(NavigationEntry).GetObjectClass(FrozenContext));
                    _NewCommand.ObjectCreated += (obj) =>
                        ((NavigationEntry)obj).Parent = SelectedItem != null
                            ? obj.Context.Find<NavigationEntry>(SelectedItem.ID)
                            : null;
                }
                return _NewCommand;
            }
        }

        public void New()
        {
            if (NewCommand.CanExecute(null))
                NewCommand.Execute(null);
        }

        private DeleteDataObjectCommand _DeleteCommand;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this);
                }
                return _DeleteCommand;
            }
        }

        public void Delete()
        {
            if (DeleteCommand.CanExecute(null))
                DeleteCommand.Execute(null);
        }

        #endregion

        #region IRefreshCommandListener Members

        public void Refresh()
        {
            _rootScreens = null;
            OnPropertyChanged("RootScreens");
        }

        #endregion

        #region IDeleteCommandParameter members
        bool IDeleteCommandParameter.IsReadOnly { get { return false; } }
        bool IDeleteCommandParameter.AllowDelete { get { return true; } }
        IEnumerable<ViewModel> ICommandParameter.SelectedItems { get { return SelectedItem == null ? null : new[] { DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, SelectedItem.NavEntry) }; } }
        #endregion

        #region IOpenCommandParameter members
        bool IOpenCommandParameter.AllowOpen { get { return true; } }
        #endregion

        #region INewCommandParameter members
        bool INewCommandParameter.IsReadOnly { get { return false; } }
        bool INewCommandParameter.AllowAddNew { get { return true; } }
        #endregion
    }

    public class NavigationEntryEditorViewModel : ViewModel
    {
        public new delegate NavigationEntryEditorViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationEntry navEntry);

        public NavigationEntryEditorViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, NavigationEntry navEntry)
            : base(appCtx, dataCtx, parent)
        {
            this.NavEntry = navEntry;
        }

        public NavigationEntry NavEntry { get; private set; }

        public override string Name
        {
            get { return NavEntry.Title; }
        }

        public string Title
        {
            get { return NavEntry.Title; }
        }

        public int ID
        {
            get
            {
                return NavEntry.ID;
            }
        }

        public IEnumerable<NavigationEntryEditorViewModel> Children
        {
            get
            {
                NavEntry.TriggerFetch("Children");
                return NavEntry.Children.Select(i => ViewModelFactory.CreateViewModel<NavigationEntryEditorViewModel.Factory>().Invoke(DataContext, this, i));
            }
        }
    }
}
