using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.Configuration;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class AssemblySearchPathsViewModel : ViewModel
    {
        private ZetboxConfig.AssemblySearchPathArray _cfg;

        public AssemblySearchPathsViewModel(ZetboxConfig.AssemblySearchPathArray cfg)
        {
            _cfg = cfg;
        }

        public bool EnableShadowCopy
        {
            get
            {
                return _cfg.EnableShadowCopy;
            }
            set
            {
                _cfg.EnableShadowCopy = value;
            }
        }

        public string[] Paths
        {
            get
            {
                return _cfg.Paths;
            }
            set
            {
                _cfg.Paths = value;
            }
        }
    }
}
