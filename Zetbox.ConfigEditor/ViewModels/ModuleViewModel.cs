
namespace Zetbox.ConfigEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;

    public class ModuleListViewModel : ViewModel
    {
        public ModuleListViewModel(List<ZetboxConfig.Module> modules)
        {
            Model = modules;
        }

        private List<ZetboxConfig.Module> Model
        {
            get;
            set;
        }

        private List<ModuleViewModel> _moduleViewModels;
        public IEnumerable<ModuleViewModel> List
        {
            get
            {
                if (_moduleViewModels == null)
                {
                    _moduleViewModels = Model.Select(m => new ModuleViewModel(m, this)).ToList();
                }
                return _moduleViewModels;
            }
        }


        private ICommandViewModel _AddCommand = null;
        public ICommandViewModel AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new SimpleCommandViewModel("Add", "Add a module", Add);
                }
                return _AddCommand;
            }
        }

        public void Add()
        {
            var dlgVmdl = new SelectModuleDlgViewModel(List);
            var dlg = new SelectModuleDialog(dlgVmdl);
            if (dlg.ShowDialog() == true)
            {
                Model.AddRange(dlgVmdl.Selected.Select(i => i.Module));
                _moduleViewModels = null;
                OnPropertyChanged("List");
            }
        }

        public void Remove(ModuleViewModel moduleViewModel)
        {
            Model.Remove(moduleViewModel.Module);
            _moduleViewModels = null;
            OnPropertyChanged("List");
        }

        public void Up(ModuleViewModel moduleViewModel)
        {
            var idx = Model.IndexOf(moduleViewModel.Module);
            if (idx <= 0) return;
            Model.RemoveAt(idx);
            Model.Insert(idx - 1, moduleViewModel.Module);
            _moduleViewModels = null;
            OnPropertyChanged("List");
        }

        public void Down(ModuleViewModel moduleViewModel)
        {
            var idx = Model.IndexOf(moduleViewModel.Module);
            if (idx >= Model.Count - 1) return;
            Model.RemoveAt(idx);
            Model.Insert(idx + 1, moduleViewModel.Module);
            _moduleViewModels = null;
            OnPropertyChanged("List");
        }
    }

    public class ModuleViewModel : ViewModel
    {
        private ZetboxConfig.Module _module;
        ModuleListViewModel _parent;

        public ModuleViewModel(ZetboxConfig.Module module, ModuleListViewModel parent)
        {
            _module = module;
            _parent = parent;
        }

        public ModuleViewModel(ModuleDescriptor type)
        {
            _module = new ZetboxConfig.Module() { TypeName = type.TypeName };
        }

        public void Repair()
        {
            // nothingto repair currently
        }


        #region Properties
        public ZetboxConfig.Module Module
        {
            get
            {
                return _module;
            }
        }

        public string TypeName
        {
            get
            {
                return _module.TypeName;
            }
        }

        public bool NotOnFallback
        {
            get
            {
                return ModulesCache.Instance[TypeName].NotOnFallback;
            }
        }

        public bool IsFeature
        {
            get
            {
                return ModulesCache.Instance[TypeName].IsFeature;
            }
        }

        public bool IsAutoloaded
        {
            get
            {
                return ModulesCache.Instance[TypeName].IsAutoloaded;
            }
        }

        public string Description
        {
            get
            {
                return ModulesCache.Instance[TypeName].Description;
            }
        }
        #endregion

        #region Commands
        private ICommandViewModel _RemoveCommand = null;
        public ICommandViewModel RemoveCommand
        {
            get
            {
                if (_RemoveCommand == null)
                {
                    _RemoveCommand = new SimpleCommandViewModel("Remove", "Removes this module", Remove, () => _parent != null);
                }
                return _RemoveCommand;
            }
        }

        public void Remove()
        {
            _parent.Remove(this);
        }

        private ICommandViewModel _UpCommand = null;
        public ICommandViewModel UpCommand
        {
            get
            {
                if (_UpCommand == null)
                {
                    _UpCommand = new SimpleCommandViewModel("Up", "Move this module up", Up, () => _parent != null);
                }
                return _UpCommand;
            }
        }

        public void Up()
        {
            _parent.Up(this);
        }

        private ICommandViewModel _DownCommand = null;
        public ICommandViewModel DownCommand
        {
            get
            {
                if (_DownCommand == null)
                {
                    _DownCommand = new SimpleCommandViewModel("Down", "Move this module down", Down, () => _parent != null);
                }
                return _DownCommand;
            }
        }

        public void Down()
        {
            _parent.Down(this);
        }
        #endregion
    }
}
