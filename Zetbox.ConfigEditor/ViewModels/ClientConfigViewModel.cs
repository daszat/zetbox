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

        public void Repair()
        {
            foreach (var m in Modules.List)
            {
                m.Repair();
            }
        }

        #region Properties
        public Guid? Application
        {
            get
            {
                return _cfg.Application;
            }
            set
            {
                _cfg.Application = value;
            }
        }

        public string Culture
        {
            get
            {
                return _cfg.Culture;
            }
            set
            {
                _cfg.Culture = value;
            }
        }

        public string UICulture
        {
            get
            {
                return _cfg.Culture;
            }
            set
            {
                _cfg.Culture = value;
            }
        }

        private ModuleListViewModel _modules;
        public ModuleListViewModel Modules
        {
            get
            {
                if (_modules == null)
                {
                    if (_cfg.Modules == null) _cfg.Modules = new List<ZetboxConfig.Module>();
                    _modules = new ModuleListViewModel(_cfg.Modules);
                }
                return _modules;
            }
        }
        #endregion
    }
}
