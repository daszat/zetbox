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

    [ViewModelDescriptor]
    public class NavigationScreenHierarchyViewModel : ViewModel, IRefreshCommandListener, IDeleteCommandParameter, IOpenCommandParameter
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

        private NavigationEntryViewModel _selectedItem;
        public NavigationEntryViewModel SelectedItem
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

        private ReadOnlyObservableCollection<NavigationEntryViewModel> _rootScreens = null;
        public ReadOnlyObservableCollection<NavigationEntryViewModel> RootScreens
        {
            get
            {
                if (_rootScreens == null)
                {
                    var moduleID = Module.ID;
                    _rootScreens = new ReadOnlyObservableCollection<NavigationEntryViewModel>(new ObservableCollection<NavigationEntryViewModel>(
                        DataContext.GetQuery<NavigationScreen>()
                        .Where(i => i.Module.ID == moduleID)
                        .Where(i => i.Parent == null)
                        .OrderBy(i => i.Title)
                        .Select(i => NavigationEntryViewModel.Fetch(ViewModelFactory, DataContext, this, i))));
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
                        this,
                        true);
                }
                return _OpenCommand;
            }
        }

        public void Open()
        {
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
                        typeof(NavigationEntry).GetObjectClass(FrozenContext),
                        true);
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
            NewCommand.Execute(null);
        }

        private DeleteDataObjectCommand _DeleteCommand;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<DeleteDataObjectCommand.Factory>().Invoke(DataContext, this, true);
                }
                return _DeleteCommand;
            }
        }

        public void Delete()
        {
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
        IEnumerable<ViewModel> IDeleteCommandParameter.SelectedItems { get { return new[] { SelectedItem }; } }
        #endregion

        #region IOpenCommandParameter members
        bool IActivateCommandParameter.IsInlineEditable { get { return false; } }
        bool IOpenCommandParameter.AllowOpen { get { return true; } }
        IEnumerable<ViewModel> IOpenCommandParameter.SelectedItems { get { return new[] { SelectedItem }; } }
        #endregion
    }
}
