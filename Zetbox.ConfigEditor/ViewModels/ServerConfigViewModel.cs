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

        public void Repair()
        {
            foreach (var m in Modules.List)
            {
                m.Repair();
            }
        }

        #region Properties

        public bool StartServer
        {
            get
            {
                return _cfg.StartServer;
            }
            set
            {
                _cfg.StartServer = value;
            }
        }

        public List<ZetboxConfig.ServerConfig.Database> ConnectionStrings
        {
            get
            {
                if (_cfg.ConnectionStrings == null)
                    _cfg.ConnectionStrings = new List<ZetboxConfig.ServerConfig.Database>();
                return _cfg.ConnectionStrings;
            }
            set
            {
                _cfg.ConnectionStrings = value;
            }
        }

        public string DocumentStore
        {
            get
            {
                return _cfg.DocumentStore;
            }
            set
            {
                _cfg.DocumentStore = value;
            }
        }

        public string CodeGenWorkingPath
        {
            get
            {
                return _cfg.CodeGenWorkingPath;
            }
            set
            {
                _cfg.CodeGenWorkingPath = value;
            }
        }

        public string CodeGenOutputPath
        {
            get
            {
                return _cfg.CodeGenOutputPath;
            }
            set
            {
                _cfg.CodeGenOutputPath = value;
            }
        }

        public string CodeGenArchivePath
        {
            get
            {
                return _cfg.CodeGenArchivePath;
            }
            set
            {
                _cfg.CodeGenArchivePath = value;
            }
        }

        public string[] CodeGenBinaryOutputPath
        {
            get
            {
                return _cfg.CodeGenBinaryOutputPath;
            }
            set
            {
                _cfg.CodeGenBinaryOutputPath = value;
            }
        }

        public List<ZetboxConfig.ServerConfig.ClientFilesLocation> ClientFilesLocations
        {
            get
            {
                if (_cfg.ClientFilesLocations == null)
                    _cfg.ClientFilesLocations = new List<ZetboxConfig.ServerConfig.ClientFilesLocation>();
                return _cfg.ClientFilesLocations;
            }
            set
            {
                _cfg.ClientFilesLocations = value;
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
