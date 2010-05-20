using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client.GUI
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ViewDescriptorAttribute : Attribute
    {
        public ViewDescriptorAttribute(string module, Kistl.App.GUI.Toolkit toolkit)
        {
            this.Module = module;
            this.Toolkit = toolkit;
        }

        public string Module { get; set; }
        public string Kind { get; set; }
        public Kistl.App.GUI.Toolkit Toolkit { get; set; }
    }
}
