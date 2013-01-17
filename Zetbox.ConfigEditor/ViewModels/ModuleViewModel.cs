using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.Configuration;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class ModuleListViewModel : ViewModel
    {

        public ModuleListViewModel(Func<ZetboxConfig.Module[]> getModules, Action<ZetboxConfig.Module[]> setModules)
        {
            _getModules = getModules;
            _setModules = setModules;
        }

        private static readonly ZetboxConfig.Module[] Empty = new ZetboxConfig.Module[] { };
        private Func<ZetboxConfig.Module[]> _getModules;
        private Action<ZetboxConfig.Module[]> _setModules;
        private ZetboxConfig.Module[] Modules
        {
            get
            {
                return (_getModules() ?? Empty);
            }
            set
            {
                _setModules(value);
            }
        }

        private List<ModuleViewModel> _moduleViewModels;
        public IEnumerable<ModuleViewModel> List
        {
            get
            {
                if (_moduleViewModels == null)
                {
                    _moduleViewModels = Modules.Select(m => new ModuleViewModel(m, this)).ToList();
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
            var dlgVmdl = new SelectModuleDlgViewModel();
            var dlg = new SelectModuleDialog(dlgVmdl);
            if (dlg.ShowDialog() == true)
            {
                var result = Modules.Concat(dlgVmdl.Selected.Select(i => i.Module)).ToArray();
                Modules = result;
                _moduleViewModels = null;
                OnPropertyChanged("List");
            }
        }

        public void Remove(ModuleViewModel moduleViewModel)
        {
            var result = Modules.Except(new[] { moduleViewModel.Module }).ToArray();
            Modules = result;
            _moduleViewModels = null;
            OnPropertyChanged("List");            
        }

        public void Up(ModuleViewModel moduleViewModel)
        {
            var result = Modules.ToList();
            var idx = result.IndexOf(moduleViewModel.Module);
            if (idx <= 0) return;
            result.RemoveAt(idx);
            result.Insert(idx - 1, moduleViewModel.Module);
            Modules = result.ToArray();
            _moduleViewModels = null;
            OnPropertyChanged("List");
        }

        public void Down(ModuleViewModel moduleViewModel)
        {
            var result = Modules.ToList();
            var idx = result.IndexOf(moduleViewModel.Module);
            if (idx >= result.Count - 1) return;
            result.RemoveAt(idx);
            result.Insert(idx + 1, moduleViewModel.Module);
            Modules = result.ToArray();
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

        public ModuleViewModel(ModuleType type)
        {
            _module = new ZetboxConfig.Module() { TypeName = type.TypeName, NotOnFallback = type.NotOnFallback };
        }

        public void Repair()
        {
            if (ModulesCache.Instance.Contains(TypeName))
            {
                var type = ModulesCache.Instance[TypeName];
                if (type.NotOnFallback != _module.NotOnFallback)
                {
                    _module.NotOnFallback = _module.NotOnFallbackSpecified = type.NotOnFallback;
                }
            }
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
                return _module.NotOnFallback;
            }
        }

        public bool IsFeature
        {
            get
            {
                return ModulesCache.Instance[TypeName].IsFeature;
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
