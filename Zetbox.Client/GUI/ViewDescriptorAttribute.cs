using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.Client.GUI
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ViewDescriptorAttribute : Attribute
    {
        public ViewDescriptorAttribute(Zetbox.App.GUI.Toolkit toolkit)
        {
            this.Toolkit = toolkit;
        }

        public Zetbox.App.GUI.Toolkit Toolkit { get; set; }
    }
}
