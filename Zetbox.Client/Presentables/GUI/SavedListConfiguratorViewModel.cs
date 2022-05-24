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
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Common.GUI;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Presentables.ZetboxBase;

    // No ViewModelDescriptor -> internal
    public partial class SavedListConfiguratorViewModel : ViewModel
    {
        public new delegate SavedListConfiguratorViewModel Factory(IZetboxContext dataCtx, InstanceListViewModel parent);

        public SavedListConfiguratorViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, InstanceListViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        protected override async Task<System.Collections.ObjectModel.ObservableCollection<ICommandViewModel>> CreateCommands()
        {
            var cmds = await base.CreateCommands();
            cmds.Add(SaveCommand);
            cmds.Add(SaveAsCommand);
            cmds.Add(RenameCommand);
            cmds.Add(ResetCommand);
            return cmds;
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

        private PropertyTask<ReadOnlyCollectionShortcut<SavedListConfigViewModel>> _configsTask;
        private PropertyTask<ReadOnlyCollectionShortcut<SavedListConfigViewModel>> EnsureConfigsTask()
        {
            if (_configsTask != null) return _configsTask;

            return _configsTask = new PropertyTask<ReadOnlyCollectionShortcut<SavedListConfigViewModel>>(
                notifier: () =>
                {
                    OnPropertyChanged("Configs");
                    OnPropertyChanged("ConfigsAsync");
                },
                createTask: () =>
                {
                    return Task.Run(async () =>
                    {
                        var ctx = ViewModelFactory.CreateNewContext();
                        try
                        {
                            var qry = await ctx.GetQuery<SavedListConfiguration>()
                                .Where(i => i.Type.ExportGuid == Parent.DataType.ExportGuid) // Parent.DataType might be from FrozenContext
                                .Where(i => i.Owner == null || i.Owner.ID == this.CurrentPrincipal.ID)
                                .ToListAsync();

                            var result = new ReadOnlyCollectionShortcut<SavedListConfigViewModel>();
                            foreach (var cfg in qry)
                            {
                                var obj = cfg.Configuration.FromXmlString<SavedListConfigurationList>();
                                foreach (var item in obj.Configs)
                                {
                                    result.Rw.Add(ViewModelFactory.CreateViewModel<SavedListConfigViewModel.Factory>().Invoke(DataContext, this, item, cfg.Owner != null));
                                }
                            }

                            return result;
                        }
                        finally
                        {
                            ctx.Dispose();
                        }
                    });
                },
                set: null);
        }

        public ReadOnlyObservableCollection<SavedListConfigViewModel> Configs
        {
            get { return EnsureConfigsTask().Get().Ro; }
        }

        public ReadOnlyObservableCollection<SavedListConfigViewModel> ConfigsAsync
        {
            get
            {
                var result = EnsureConfigsTask().GetAsync();
                return result != null ? result.Ro : null;
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
                    // TODO: unawaited Task
                    _ = Load();
                    Parent.Refresh();
                }
            }
        }

        public class ColumnInformation
        {
            public string Path { get; set; }
            public int Width { get; set; }
        }

        public delegate List<ColumnInformation> GetColumnInformationEventHandler();
        public event GetColumnInformationEventHandler GetColumnInformation;

        protected List<ColumnInformation> OnGetColumnInformation()
        {
            List<ColumnInformation> result = null;
            var temp = GetColumnInformation;
            if (temp != null)
            {
                result = temp();
            }
            return result ?? Parent.DisplayedColumns.Columns.Select(i => new ColumnInformation() { Path = i.Path, Width = 0 }).ToList();
        }

        #region Commands
        private ICommandViewModel _SaveCommand = null;
        public ICommandViewModel SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        SavedListConfiguratorViewModelResources.SaveCommandName,
                        SavedListConfiguratorViewModelResources.SaveCommandTooltip,
                        Save, null, null);
                    //_SaveCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.save.Find(FrozenContext);
                }
                return _SaveCommand;
            }
        }

        private ICommandViewModel _SaveAsCommand = null;
        public ICommandViewModel SaveAsCommand
        {
            get
            {
                if (_SaveAsCommand == null)
                {
                    _SaveAsCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        SavedListConfiguratorViewModelResources.SaveAsCommandName,
                        SavedListConfiguratorViewModelResources.SaveAsCommandTooltip,
                        SaveAs, null, null);
                    //_SaveCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.save.Find(FrozenContext);
                }
                return _SaveAsCommand;
            }
        }

        public async Task SaveAs()
        {
            string name = string.Empty;
            ViewModelFactory.CreateDialog(DataContext, SavedListConfiguratorViewModelResources.SaveDlgTitle)
                .AddString("name", SavedListConfiguratorViewModelResources.SaveDlgNameLabel)
                .Show((p) => { name = p["name"] as string; });
            if (string.IsNullOrEmpty(name)) return;
            await Save(name);
        }

        public async Task Save()
        {
            string name = string.Empty;
            if (SelectedItem == null || !SelectedItem.IsMyOwn)
            {
                // Create new
                ViewModelFactory.CreateDialog(DataContext, SavedListConfiguratorViewModelResources.SaveDlgTitle)
                    .AddString("name", SavedListConfiguratorViewModelResources.SaveDlgNameLabel)
                    .Show((p) => { name = p["name"] as string; });
                if (string.IsNullOrEmpty(name)) return;
            }
            else
            {
                // Update
                name = SelectedItem.Name;
            }

            await Save(name);
        }

        private ICommandViewModel _ResetCommand = null;
        public ICommandViewModel ResetCommand
        {
            get
            {
                if (_ResetCommand == null)
                {
                    _ResetCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        SavedListConfiguratorViewModelResources.ResetCommandName,
                        SavedListConfiguratorViewModelResources.ResetCommandTooltip,
                        Reset, null, null);
                    Task.Run(async () => _ResetCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.reload_png.Find(FrozenContext)));
                }
                return _ResetCommand;
            }
        }

        public async Task Reset()
        {
            SelectedItem = null;
            await Parent.ResetSort(refresh: false);
            Parent.FilterList.ResetUserFilter();
            Parent.ResetDisplayedColumns();
        }

        private ICommandViewModel _DeleteCommand = null;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        SavedListConfiguratorViewModelResources.DeleteCommandName,
                        SavedListConfiguratorViewModelResources.DeleteCommandTooltip,
                        Delete, 
                        () => Task.FromResult(SelectedItem != null),
                        () => Task.FromResult(SavedListConfiguratorViewModelResources.DeleteCommandReason));
                    Task.Run(async () => _DeleteCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.delete_png.Find(FrozenContext)));
                }
                return _DeleteCommand;
            }
        }

        public async Task Delete()
        {
            if (SelectedItem != null)
            {
                await Delete(SelectedItem);
            }
        }

        public Task Delete(SavedListConfigViewModel itemToDelete)
        {
            if (itemToDelete == null) throw new ArgumentNullException("itemToDelete");
            if (ViewModelFactory.GetDecisionFromUser(string.Format(SavedListConfiguratorViewModelResources.DeleteMessage, itemToDelete.Name), SavedListConfiguratorViewModelResources.DeleteTitle) == true)
            {
                using (var ctx = ViewModelFactory.CreateNewContext())
                {
                    var config = GetSavedConfig(ctx);
                    var obj = ExtractConfigurationObject(config);

                    var item = obj.Configs.FirstOrDefault(i => i.Name == itemToDelete.Name);
                    if (item != null)
                    {
                        obj.Configs.Remove(item);
                        config.Configuration = obj.ToXmlString();
                        ctx.SubmitChanges();
                        RemoveViewModel(item.Name);
                    }
                }
            }

            return Task.CompletedTask;
        }

        private ICommandViewModel _RenameCommand = null;
        public ICommandViewModel RenameCommand
        {
            get
            {
                if (_RenameCommand == null)
                {
                    _RenameCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        SavedListConfiguratorViewModelResources.RenameCommandName,
                        SavedListConfiguratorViewModelResources.RenameCommandTooltip,
                        Rename,
                        () => Task.FromResult(SelectedItem != null),
                        () => Task.FromResult(SavedListConfiguratorViewModelResources.RenameCommandReason));
                }
                return _RenameCommand;
            }
        }

        public async Task Rename()
        {
            if (SelectedItem != null)
            {
                await Rename(SelectedItem);
            }
        }

        public Task Rename(SavedListConfigViewModel itemToRename)
        {
            if (itemToRename == null) throw new ArgumentNullException("itemToRename");

            string newName = null;
            string oldName = itemToRename.Name;

            ViewModelFactory.CreateDialog(DataContext, SavedListConfiguratorViewModelResources.SaveDlgTitle)
                .AddString("name", SavedListConfiguratorViewModelResources.SaveDlgNameLabel)
                .Show(p => { newName = p["name"] as string; });
            if (string.IsNullOrEmpty(newName)) return Task.CompletedTask;

            using (var ctx = ViewModelFactory.CreateNewContext())
            {
                var config = GetSavedConfig(ctx);
                var obj = ExtractConfigurationObject(config);
                var item = obj.Configs.FirstOrDefault(i => i.Name == oldName);
                if (item != null)
                {
                    item.Name = newName;
                    config.Configuration = obj.ToXmlString();
                    ctx.SubmitChanges();
                    UpdateViewModel(oldName, item);
                }
            }

            return Task.CompletedTask;
        }
        #endregion

        #region Helper
        protected async Task<object> ResolveUntypedValue(object val, IValueModel mdl)
        {
            if (val == null) return null;

            if (mdl is IObjectReferenceValueModel)
            {
                var objRefMdl = (IObjectReferenceValueModel)mdl;
                if (val is Guid)
                {
                    return DataContext.FindPersistenceObject(DataContext.GetInterfaceType(await objRefMdl.ReferencedClass.GetDataType()), (Guid)val);
                }
                else if (val is int)
                {
                    return DataContext.FindPersistenceObject(DataContext.GetInterfaceType(await objRefMdl.ReferencedClass.GetDataType()), (int)val);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return val;
            }
        }
        protected object ExtractUntypedValue(object val)
        {
            if (val is IExportable)
            {
                return ((IExportable)val).ExportGuid;
            }
            else if (val is IPersistenceObject)
            {
                return ((IPersistenceObject)val).ID;
            }
            else if (val is Enum)
            {
                return (int)val;
            }
            else
            {
                return val;
            }
        }
        protected void UpdateViewModel(string name, SavedListConfig item)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (item == null) throw new ArgumentNullException("item");

            var configs = EnsureConfigsTask().Get();
            if (configs != null)
            {
                SavedListConfigViewModel vmdl = configs.Rw.SingleOrDefault(i => i.Object.Name == name && i.IsMyOwn);
                if (vmdl == null)
                {
                    vmdl = ViewModelFactory.CreateViewModel<SavedListConfigViewModel.Factory>().Invoke(DataContext, this, item, true);
                    configs.Rw.Add(vmdl);
                    SelectedItem = vmdl;
                }
                else
                {
                    vmdl.Object = item;
                }
            }
        }

        protected void RemoveViewModel(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            var configs = EnsureConfigsTask().Get();

            if (configs != null)
            {
                SavedListConfigViewModel vmdl = configs.Rw.SingleOrDefault(i => i.Object.Name == name && i.IsMyOwn);
                if (vmdl != null)
                {
                    configs.Rw.Remove(vmdl);
                    SelectedItem = null;
                }
            }
        }

        protected SavedListConfig ExtractItem(string name, SavedListConfigurationList obj)
        {
            var item = obj.Configs.FirstOrDefault(i => i.Name == name);
            if (item == null)
            {
                item = new SavedListConfig();
                item.Name = name;
                obj.Configs.Add(item);
            }
            return item;
        }

        protected SavedListConfigurationList ExtractConfigurationObject(SavedListConfiguration config)
        {
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
            return obj;
        }

        private SavedListConfiguration GetSavedConfig(IZetboxContext ctx)
        {
            var config = ctx.GetQuery<SavedListConfiguration>()
                .Where(i => i.Type.ExportGuid == Parent.DataType.ExportGuid) // Parent.DataType might be from FrozenContext
                .Where(i => i.Owner.ID == this.CurrentPrincipal.ID)
                .FirstOrDefault();
            if (config == null)
            {
                config = ctx.Create<SavedListConfiguration>();
                config.Owner = ctx.Find<Identity>(CurrentPrincipal.ID);
                config.Type = ctx.FindPersistenceObject<ObjectClass>(Parent.DataType.ExportGuid);  // Parent.DataType might be from FrozenContext
            }
            return config;
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
            this._object = obj;
            this.IsMyOwn = isMyOwn;
        }

        private SavedListConfig _object;
        public SavedListConfig Object
        {
            get
            {
                return _object;
            }
            set
            {
                if (_object != value)
                {
                    _object = value;
                    OnPropertyChanged("Object");
                    OnPropertyChanged("Name");
                }
            }
        }

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

        public override string ToString()
        {
            return Name;
        }

        public bool IsMyOwn { get; private set; }

        #region Commands
        private ICommandViewModel _DeleteCommand = null;
        public ICommandViewModel DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        SavedListConfiguratorViewModelResources.DeleteCommandName,
                        SavedListConfiguratorViewModelResources.DeleteCommandTooltip,
                        Delete, null, null);
                    Task.Run(async () => _DeleteCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.delete_png.Find(FrozenContext)));
                }
                return _DeleteCommand;
            }
        }

        public Task Delete()
        {
            return Parent.Delete(this);
        }

        private ICommandViewModel _RenameCommand = null;
        public ICommandViewModel RenameCommand
        {
            get
            {
                if (_RenameCommand == null)
                {
                    _RenameCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        SavedListConfiguratorViewModelResources.RenameCommandName,
                        SavedListConfiguratorViewModelResources.RenameCommandTooltip,
                        Rename, null, null);
                    Task.Run(async () => _RenameCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.pen_png.Find(FrozenContext)));
                }
                return _RenameCommand;
            }
        }

        public Task Rename()
        {
            return Parent.Rename(this);
        }
        #endregion
    }

}
