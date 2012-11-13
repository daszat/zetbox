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

        protected readonly Func<IZetboxContext> ctxFactory;

        public SavedListConfiguratorViewModel(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory, IZetboxContext dataCtx, InstanceListViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
            this.ctxFactory = ctxFactory;
        }

        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = base.CreateCommands();
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
                    var ctx = ctxFactory();
                    var qryTask = ctx.GetQuery<SavedListConfiguration>()
                            .Where(i => i.Type.ExportGuid == Parent.DataType.ExportGuid) // Parent.DataType might be from FrozenContext
                            .Where(i => i.Owner == null || i.Owner == this.CurrentIdentity)
                            .ToListAsync();

                    var result = new ZbTask<ReadOnlyCollectionShortcut<SavedListConfigViewModel>>(qryTask);
                    result
                        .OnResult(t =>
                        {
                            t.Result = new ReadOnlyCollectionShortcut<SavedListConfigViewModel>();
                            foreach (var cfg in qryTask.Result)
                            {
                                var obj = cfg.Configuration.FromXmlString<SavedListConfigurationList>();
                                foreach (var item in obj.Configs)
                                {
                                    t.Result.Rw.Add(ViewModelFactory.CreateViewModel<SavedListConfigViewModel.Factory>().Invoke(DataContext, this, item, cfg.Owner != null));
                                }
                            }
                        });
                    return result;
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
                    Load();
                    Parent.ReloadInstances();
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

        public void SaveAs()
        {
            string name = string.Empty;
            ViewModelFactory.CreateDialog(SavedListConfiguratorViewModelResources.SaveDlgTitle)
                .AddString(SavedListConfiguratorViewModelResources.SaveDlgNameLabel)
                .Show((p) => { name = p[0] as string; });
            if (string.IsNullOrEmpty(name)) return;
            Save(name);
        }

        public void Save()
        {
            string name = string.Empty;
            if (SelectedItem == null || !SelectedItem.IsMyOwn)
            {
                // Create new
                ViewModelFactory.CreateDialog(SavedListConfiguratorViewModelResources.SaveDlgTitle)
                    .AddString(SavedListConfiguratorViewModelResources.SaveDlgNameLabel)
                    .Show((p) => { name = p[0] as string; });
                if (string.IsNullOrEmpty(name)) return;
            }
            else
            {
                // Update
                name = SelectedItem.Name;
            }

            Save(name);
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
                    _ResetCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.reload_png.Find(FrozenContext);
                }
                return _ResetCommand;
            }
        }

        public void Reset()
        {
            SelectedItem = null;
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
                        Delete, () => SelectedItem != null,
                        () => SavedListConfiguratorViewModelResources.DeleteCommandReason);
                    _DeleteCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.delete_png.Find(FrozenContext);
                }
                return _DeleteCommand;
            }
        }

        public void Delete()
        {
            if (SelectedItem != null)
            {
                Delete(SelectedItem);
            }
        }

        public void Delete(SavedListConfigViewModel itemToDelete)
        {
            if (itemToDelete == null) throw new ArgumentNullException("itemToDelete");
            if (ViewModelFactory.GetDecisionFromUser(string.Format(SavedListConfiguratorViewModelResources.DeleteMessage, itemToDelete.Name), SavedListConfiguratorViewModelResources.DeleteTitle) == true)
            {
                using (var ctx = ctxFactory())
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
                        Rename, () => SelectedItem != null,
                        () => SavedListConfiguratorViewModelResources.RenameCommandReason);
                }
                return _RenameCommand;
            }
        }

        public void Rename()
        {
            if (SelectedItem != null)
            {
                Rename(SelectedItem);
            }
        }

        public void Rename(SavedListConfigViewModel itemToRename)
        {
            if (itemToRename == null) throw new ArgumentNullException("itemToRename");

            string newName = null;
            string oldName = itemToRename.Name;

            ViewModelFactory.CreateDialog(SavedListConfiguratorViewModelResources.SaveDlgTitle)
                .AddString(SavedListConfiguratorViewModelResources.SaveDlgNameLabel)
                .Show((p) => { newName = p[0] as string; });
            if (string.IsNullOrEmpty(newName)) return;

            using (var ctx = ctxFactory())
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
        }
        #endregion

        #region Helper
        protected object ResolveUntypedValue(object val, IValueModel mdl)
        {
            if (val == null) return null;

            if (mdl is IObjectReferenceValueModel)
            {
                var objRefMdl = (IObjectReferenceValueModel)mdl;
                if (val is Guid)
                {
                    return DataContext.FindPersistenceObject(DataContext.GetInterfaceType(objRefMdl.ReferencedClass.GetDataType()), (Guid)val);
                }
                else if (val is int)
                {
                    return DataContext.FindPersistenceObject(DataContext.GetInterfaceType(objRefMdl.ReferencedClass.GetDataType()), (int)val);
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
                .Where(i => i.Owner == this.CurrentIdentity)
                .FirstOrDefault();
            if (config == null)
            {
                config = ctx.Create<SavedListConfiguration>();
                config.Owner = ctx.Find<Identity>(CurrentIdentity.ID);
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
                    _DeleteCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.delete_png.Find(FrozenContext);
                }
                return _DeleteCommand;
            }
        }

        public void Delete()
        {
            Parent.Delete(this);
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
                    _RenameCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.pen_png.Find(FrozenContext);
                }
                return _RenameCommand;
            }
        }

        public void Rename()
        {
            Parent.Rename(this);
        }
        #endregion
    }

}
