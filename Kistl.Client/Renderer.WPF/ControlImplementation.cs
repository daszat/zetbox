using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.GUI
{
    /// <summary>
    /// The render-specific functionality of a specific control
    /// </summary>
    public class ControlImplementation
    {
        // public string Module { get; set; }
        public string Control { get; set; }
        public Platform Platform { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }

        public static ControlImplementation[] Implementations = new[] {
            new ControlImplementation() { Platform = Platform.ASPNET, Control = "group", AssemblyName = "blah", ClassName = "foo" },
            new ControlImplementation() { Platform = Platform.WPF, Control = "group", AssemblyName = "PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", ClassName = "System.Windows.Controls.GroupBox" },
            new ControlImplementation() { Platform = Platform.WPF, Control = "fk", AssemblyName = "PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", ClassName = "System.Windows.Controls.TextBox" },
            new ControlImplementation() { Platform = Platform.WPF, Control = "string", AssemblyName = "PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", ClassName = "System.Windows.Controls.TextBox" },
            new ControlImplementation() { Platform = Platform.WPF, Control = "date", AssemblyName = "PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", ClassName = "System.Windows.Controls.TextBox" },
            new ControlImplementation() { Platform = Platform.WPF, Control = "number", AssemblyName = "PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", ClassName = "System.Windows.Controls.TextBox" },
        };
    }

    public enum Platform  {
        WPF,
        ASPNET
    };
}
