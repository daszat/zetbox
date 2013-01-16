using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.Configuration;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class ConfigViewModel : ViewModel
    {
        private ZetboxConfig _cfg;

        public ConfigViewModel(ZetboxConfig zetboxConfig, string srcPath)
        {
            this._cfg = zetboxConfig;
            this.SourcePath = srcPath;
        }

        #region Properties

        public string SourcePath { get; set; }
        public ZetboxConfig Config
        {
            get
            {
                return _cfg;
            }
        }

        public string ConfigName
        {
            get
            {
                return _cfg.ConfigName;
            }
            set
            {
                if (_cfg.ConfigName != value)
                {
                    _cfg.ConfigName = value;
                    OnPropertyChanged("ConfigName");
                }
            }
        }

        private ZetboxConfig.ClientConfig _deletedClientConfig;
        public bool HasClient
        {
            get
            {
                return _cfg.Client != null;
            }
            set
            {
                if (HasClient != value)
                {
                    if (value == true)
                    {
                        _cfg.Client = _deletedClientConfig ?? new ZetboxConfig.ClientConfig();
                    }
                    else
                    {
                        if(_cfg.Client != null)
                            _deletedClientConfig = _cfg.Client;
                        _cfg.Client = null;
                    }
                    _clientViewModel = null;
                    OnPropertyChanged("HasClient");
                    OnPropertyChanged("Client");
                }
            }
        }

        private ClientConfigViewModel _clientViewModel;
        public ClientConfigViewModel Client
        {
            get
            {
                if (_clientViewModel == null && _cfg.Client != null)
                {
                    _clientViewModel = new ClientConfigViewModel(_cfg.Client);
                }
                return _clientViewModel;
            }
        }

        private ZetboxConfig.ServerConfig _deletedServerConfig;
        public bool HasServer
        {
            get
            {
                return _cfg.Server != null;
            }
            set
            {
                if (HasServer != value)
                {
                    if (value == true)
                    {
                        _cfg.Server = _deletedServerConfig ?? new ZetboxConfig.ServerConfig();
                    }
                    else
                    {
                        if (_cfg.Server != null)
                            _deletedServerConfig = _cfg.Server;
                        _cfg.Server = null;
                    }
                    _serverViewModel = null;
                    OnPropertyChanged("HasServer");
                    OnPropertyChanged("Server");
                }
            }
        }

        private ServerConfigViewModel _serverViewModel;
        public ServerConfigViewModel Server
        {
            get
            {
                if (_serverViewModel == null && _cfg.Server != null)
                {
                    _serverViewModel = new ServerConfigViewModel(_cfg.Server);
                }
                return _serverViewModel;
            }
        }

        #endregion
    }
}
