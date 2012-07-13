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
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ZetboxBase;
    using System.Collections.ObjectModel;
    using Zetbox.API.Utils;
    using Zetbox.App.GUI;
    using Zetbox.API.Common.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;
    using Zetbox.Client.GUI;
    using Zetbox.App.Base;

    // No ViewModelDescriptor -> internal
    public class SavedListConfiguratorViewModel : ViewModel
    {
        public new delegate SavedListConfiguratorViewModel Factory(IZetboxContext dataCtx, InstanceListViewModel parent);

        protected readonly Func<IZetboxContext> ctxFactory;

        public SavedListConfiguratorViewModel(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory, IZetboxContext dataCtx, InstanceListViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
            this.ctxFactory = ctxFactory;
        }

        public new InstanceListViewModel Parent
        {
            get
            {
                return (InstanceListViewModel)base.Parent;
            }
        }

        public override string Name
        {
            get { return "SavedListConfiguratorViewModel"; }
        }

        private ReadOnlyCollectionShortcut<SavedListConfigViewModel> _configs;
        public ReadOnlyObservableCollection<SavedListConfigViewModel> Configs
        {
            get
            {
                if (_configs == null)
                {
                    _configs = new ReadOnlyCollectionShortcut<SavedListConfigViewModel>();
                    using (var ctx = ctxFactory())
                    {
                        var configs = ctx.GetQuery<SavedListConfiguration>()
                            .Where(i => i.Type.ExportGuid == Parent.DataType.ExportGuid) // Parent.DataType might be from FrozenContext
                            .Where(i => i.Owner == null || i.Owner == this.CurrentIdentity);
                        foreach (var cfg in configs)
                        {
                            var obj = cfg.Configuration.FromXmlString<SavedListConfigurationList>();
                            foreach (var item in obj.Configs)
                            {
                                _configs.Rw.Add(ViewModelFactory.CreateViewModel<SavedListConfigViewModel.Factory>().Invoke(DataContext, this, item, cfg.Owner != null));
                            }
                        }
                    }
                }
                return _configs.Ro;
            }
        }

        private SavedListConfigViewModel _selectedItem;
        public SavedListConfigViewModel SelectedItem
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

        #region Commands
        private ICommandViewModel _SaveCommand = null;
        public ICommandViewModel SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Save", "", Save, null, null);
                }
                return _SaveCommand;
            }
        }

        

        public void Save()
        {
            string name = string.Empty;
            if (SelectedItem == null || !SelectedItem.IsMyOwn)
            {
                // Create new
                var dlg = new DialogCreator(DataContext, ViewModelFactory)
                            .AddString("Name");
                dlg.Show("Name", (p) => { name = p[0] as string; });
                if (string.IsNullOrEmpty(name)) return;
            }
            else
            {
                // Update
                name = SelectedItem.Name;
            }

            using (var ctx = ctxFactory())
            {
                var config = ctx.GetQuery<SavedListConfiguration>()
                    .Where(i => i.Type.ExportGuid == Parent.DataType.ExportGuid) // Parent.DataType might be from FrozenContext
                    .Where(i => i.Owner == this.CurrentIdentity)
                    .FirstOrDefault();
                if(config == null)
                {
                    config = ctx.Create<SavedListConfiguration>();
                    config.Owner = ctx.Find<Identity>(CurrentIdentity.ID);
                    config.Type = ctx.FindPersistenceObject<ObjectClass>(Parent.DataType.ExportGuid);  // Parent.DataType might be from FrozenContext
                }

                SavedListConfigurationList obj;
                try
                {
                    obj = !string.IsNullOrEmpty(config.Configuration) ? config.Configuration.FromXmlString<SavedListConfigurationList>() : new SavedListConfigurationList();
                }
                catch (Exception ex)
                {
                    Logging.Client.Warn("Error during deserializing SavedListConfigurationList, creating a new one", ex);
                    obj = new SavedListConfigurationList();
                }

                var item = obj.Configs.FirstOrDefault(i => i.Name == name);
                if (item == null)
                {
                    item = new SavedListConfig();
                    item.Name = name;
                    obj.Configs.Add(item);
                    if (_configs != null)
                    {
                        // Add also to our list
                        var vmdl = ViewModelFactory.CreateViewModel<SavedListConfigViewModel.Factory>().Invoke(DataContext, this, item, true);
                        this._configs.Rw.Add(vmdl);
                        SelectedItem = vmdl;
                    }
                }

                // Do the update

                config.Configuration = obj.ToXmlString();
                ctx.SubmitChanges();
            }
        }

        private ICommandViewModel _ResetCommand = null;
        public ICommandViewModel ResetCommand
        {
            get
            {
                if (_ResetCommand == null)
                {
                    _ResetCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Reset", "", Reset, null, null);
                }
                return _ResetCommand;
            }
        }

        public void Reset()
        {
        }
        #endregion
    }

    [ViewModelDescriptor]
    public class SavedListConfigViewModel : ViewModel
    {
        public new delegate SavedListConfigViewModel Factory(IZetboxContext dataCtx, SavedListConfiguratorViewModel parent, SavedListConfig obj, bool isMyOwn);

        public SavedListConfigViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, SavedListConfiguratorViewModel parent, SavedListConfig obj, bool isMyOwn)
            : base(appCtx, dataCtx, parent)
        {
            this.Object = obj;
            this.IsMyOwn = isMyOwn;
        }

        public SavedListConfig Object {get; private set;}

        public new SavedListConfiguratorViewModel Parent
        {
            get
            {
                return (SavedListConfiguratorViewModel)base.Parent;
            }
        }

        public override string Name
        {
            get { return Object.Name; }
        }

        public bool IsMyOwn { get; private set; }
    }

}
