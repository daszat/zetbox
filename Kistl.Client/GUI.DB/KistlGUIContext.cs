using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;

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
            Presenter result = (Presenter)Activator.CreateInstance(controlType);
            result.InitializeComponent(obj, v, ctrl);
            return result;
        }

        public static Template FindTemplate(this IDataObject obj, TemplateUsage templateUsage)
        {
            return (from t in Template.List
                    where t.Type.Equals(obj.Type)
                        && t.Usage == templateUsage
                    select t).Single();
        }
    }


    public class ControlInfo
    {
        // public string Module { get; set; }
        public string Control { get; set; }
        public Platform Platform { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public bool Container { get; set; }

        public static ControlInfo[] Implementations = new[] {
            new ControlInfo() { Platform = Platform.ASPNET, Control = "group",
                Container = true,
                AssemblyName = "blah", ClassName = "foo" },
            new ControlInfo() { Platform = Platform.ASPNET, Control = "string",
                Container = false,
                AssemblyName = "blah", ClassName = "foo" },
            new ControlInfo() { Platform = Platform.WPF, Control = "group",
                Container = true,
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.GroupBoxWrapper" },
            new ControlInfo() { Platform = Platform.WPF, Control = "fk",
                Container = false,
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditPointerProperty" },
            new ControlInfo() { Platform = Platform.WPF, Control = "string",
                Container = false,
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditSimpleProperty" },
            new ControlInfo() { Platform = Platform.WPF, Control = "date",
                Container = false,
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditDateTimeProperty" },
            new ControlInfo() { Platform = Platform.WPF, Control = "int",
                Container = false,
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditIntProperty" },
            new ControlInfo() { Platform = Platform.WPF, Control = "double",
                Container = false,
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditDoubleProperty" },
            new ControlInfo() { Platform = Platform.WPF, Control = "main",
                Container = false,
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.Client.MainWindow" },
            new ControlInfo() { Platform = Platform.WPF, Control = "object",
                Container = true,
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.WPFWindow" },
            new ControlInfo() { Platform = Platform.WPF, Control = "list",
                Container = false,
                AssemblyName = "Kistl.Client, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.ObjectList" },
        };
    }

    public class PresenterInfo
    {
        // public string Module { get; set; }
        public string Control { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }

        public static PresenterInfo[] Implementations = new[] {
            new PresenterInfo() { Control = "group",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.GroupPresenter" },
            new PresenterInfo() { Control = "fk",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.PointerPresenter" },
            new PresenterInfo() { Control = "string",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.StringPresenter" },
            new PresenterInfo() { Control = "int",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.IntPresenter" },
            new PresenterInfo() { Control = "double",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DoublePresenter" },
            new PresenterInfo() { Control = "date",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DateTimePresenter" },
            new PresenterInfo() { Control = "main",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.ApplicationPresenter" },
            new PresenterInfo() { Control = "object",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.ObjectPresenter" },
            new PresenterInfo() { Control = "list",
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.BackReferencePresenter" },
        };

    }

    public enum Platform
    {
        WPF,
        ASPNET
    }

}
