using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.Configuration;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class ServerConfigViewModel : ViewModel
    {
        private ZetboxConfig.ServerConfig _cfg;
        public ServerConfigViewModel(ZetboxConfig.ServerConfig cfg)
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
                    _modules = new ModuleListViewModel(_cfg.Modules);
                }
                return _modules;
            }
        }
    }
}
