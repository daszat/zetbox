using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.Configuration;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class ClientConfigViewModel : ViewModel
    {
        private ZetboxConfig.ClientConfig _cfg;
        public ClientConfigViewModel(ZetboxConfig.ClientConfig cfg)
        {
            _cfg = cfg;
        }

        private ModuleListViewModel _modules;
        public ModuleListViewModel Modules
        {
            get
            {
                if (_modules == null)
                {
                    _modules = new ModuleListViewModel(() => _cfg.Modules, v => _cfg.Modules = v);
                }
                return _modules;
            }
        }
    }
}
