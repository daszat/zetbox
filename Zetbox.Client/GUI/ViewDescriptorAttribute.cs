using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client.GUI
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ViewDescriptorAttribute : Attribute
    {
        public ViewDescriptorAttribute(Kistl.App.GUI.Toolkit toolkit)
        {
            this.Toolkit = toolkit;
        }

        public Kistl.App.GUI.Toolkit Toolkit { get; set; }
    }
}
