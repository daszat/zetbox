using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.API.Client;
using Kistl.Client.Controls;

namespace Kistl.GUI.DB
{
    public static class KistlGUIContext
    {
        public static Template FindTaskTemplate()
        {
            return TaskEditTemplate.Create();
        }

        public static ControlInfo FindControlInfo(Platform platform, Visual visual)
        {
            return (from ci in ControlInfo.Implementations
                    where ci.Control == visual.Name
                        && ci.Platform == platform
                    select ci).Single();
        }

        public static PresenterInfo FindPresenterInfo(Visual visual, ControlInfo info)
        {
            return (from pi in PresenterInfo.Implementations
                    where pi.Control == visual.Name
                    select pi).Single();
        }

        public static IBasicControl CreateControl(ControlInfo info)
        {
            Type controlType = Type.GetType(String.Format("{0}, {1}", info.ClassName, info.AssemblyName), true);
            return (IBasicControl)Activator.CreateInstance(controlType);
        }

        public static Presenter CreatePresenter(PresenterInfo info, BaseClientDataObject obj, Visual v, IBasicControl ctrl)
        {
            Type controlType = Type.GetType(String.Format("{0}, {1}", info.ClassName, info.AssemblyName), true);
            Presenter result = (Presenter) Activator.CreateInstance(controlType);
            result.InitializeComponent(obj, v, ctrl);
            return result;
        }
    }


    public class ControlInfo
    {
        // public string Module { get; set; }
        public string Control { get; set; }
        public Platform Platform { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }

        public static ControlInfo[] Implementations = new[] {
            new ControlInfo() { Platform = Platform.ASPNET, Control = "group",
                AssemblyName = "blah", ClassName = "foo" },
            new ControlInfo() { Platform = Platform.WPF, Control = "group",
                AssemblyName = "PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
                ClassName = "System.Windows.Controls.GroupBox" },
            new ControlInfo() { Platform = Platform.WPF,Control = "fk",
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.Client.Controls.EditPointerProperty" },
            new ControlInfo() { Platform = Platform.WPF,Control = "string",
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.Client.Controls.EditSimpleProperty" },
            new ControlInfo() { Platform = Platform.WPF,Control = "date",
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.Client.Controls.EditDateTimeProperty" },
            new ControlInfo() { Platform = Platform.WPF,Control = "number",
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.Client.Controls.EditSimpleProperty" },
        };
    }

    public class PresenterInfo
    {
        // public string Module { get; set; }
        public string Control { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }

        public static PresenterInfo[] Implementations = new[] {
            new PresenterInfo() { Control = "fk",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.PointerPresenter" },
            new PresenterInfo() { Control = "string",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.StringPresenter" },
            new PresenterInfo() { Control = "date",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DateTimePresenter" }
        };

    }

    public enum Platform
    {
        WPF,
        ASPNET
    }
}
