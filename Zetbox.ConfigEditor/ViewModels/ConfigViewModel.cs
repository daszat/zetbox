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

        public string SourcePath { get; private set; }
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

        private ClientConfigViewModel _client;
        public ClientConfigViewModel Client
        {
            get
            {
                if (_client == null && _cfg.Client != null)
                {
                    _client = new ClientConfigViewModel(_cfg.Client);
                }
                return _client;
            }
        }

        private ServerConfigViewModel _server;
        public ServerConfigViewModel Server
        {
            get
            {
                if (_server == null && _cfg.Server != null)
                {
                    _server = new ServerConfigViewModel(_cfg.Server);
                }
                return _server;
            }
        }

        #endregion
    }
}
