using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.Configuration;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class ModuleListViewModel : ViewModel
    {
        public ModuleListViewModel(ZetboxConfig.Module[] modules)
        {
            _modules = modules.Select(m => new ModuleViewModel(m)).ToList();
        }

        private List<ModuleViewModel> _modules;
        public IEnumerable<ModuleViewModel> List
        {
            get
            {
                return _modules;
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
                return "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
            }
        }
    }
}
