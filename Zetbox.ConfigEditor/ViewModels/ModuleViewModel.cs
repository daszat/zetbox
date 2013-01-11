using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.Configuration;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class ModuleListViewModel : ViewModel
    {
        private Func<ZetboxConfig.Module[]> _getModules;
        private Action<ZetboxConfig.Module[]> _setModules;

        public ModuleListViewModel(Func<ZetboxConfig.Module[]> getModules, Action<ZetboxConfig.Module[]> setModules)
        {
            _getModules = getModules;
            _setModules = setModules;
        }

        private List<ModuleViewModel> _modules;
        public IEnumerable<ModuleViewModel> List
        {
            get
            {
                if (_modules == null)
                {
                    _modules = _getModules().Select(m => new ModuleViewModel(m)).ToList();
                }
                return _modules;
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
                _modules = null;
                var result = _getModules().Concat(dlgVmdl.Selected.Select(i => i.Module)).ToArray();
                _setModules(result);
                OnPropertyChanged("List");
            }
        }
    }

    public class ModuleViewModel : ViewModel
    {
        private ZetboxConfig.Module _module;
        public ModuleViewModel(ZetboxConfig.Module module)
        {
            _module = module;
        }

        public ModuleViewModel(ModuleType type)
        {
            _module = new ZetboxConfig.Module() { TypeName = type.TypeName, NotOnFallback = false };
        }

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

        public string Description
        {
            get
            {
                return ModulesCache.Instance[TypeName].Description;
            }
        }
    }
}
