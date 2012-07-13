using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API.Common.GUI
{
    public class SavedListConfigurationList
    {
        public SavedListConfigurationList()
        {
            Configs = new List<SavedListConfig>();
        }

        public List<SavedListConfig> Configs { get; set; }
    }

    public class SavedListConfig
    {
        public string Name { get; set; }
    }
}
