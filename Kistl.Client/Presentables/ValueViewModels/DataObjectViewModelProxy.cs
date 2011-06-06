using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client.Presentables.ValueViewModels
{
    public class DataObjectViewModelProxy
    {
        public DataObjectViewModelProxy()
        {
        }

        public DataObjectViewModel Object { get; set; }

        public App.GUI.Icon Icon
        {
            get { return Object != null ? Object.Icon : null; }
        }

        public Highlight Highlight
        {
            get
            {
                var h = Object != null ? Object.Highlight : null;
                return h ?? Highlight.None; // Avoid transparent background
            }
        }
    }
}
