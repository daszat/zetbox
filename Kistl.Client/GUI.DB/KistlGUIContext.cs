using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.GUI.Renderer;

namespace Kistl.GUI.DB
{
    public static class KistlGUIContext
    {
        public static Template FindTaskTemplate()
        {
            return TaskEditTemplate.Create();
        }

        public static ControlInfo FindControlInfo(Toolkit platform, Visual visual)
        {
            return FindControlInfo(platform, visual.Name);
        }

        private static ControlInfo FindControlInfo(Toolkit platform, VisualType name)
        {
            return (from ci in ControlInfo.Implementations
                    where ci.Control == name
                        && ci.Platform == platform
                    select ci).Single();
        }

        public static PresenterInfo FindPresenterInfo(Visual visual)
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

        public static IRenderer CreateRenderer(Toolkit platform)
        {
            var info = FindControlInfo(platform, VisualType.Renderer);
            Type controlType = Type.GetType(String.Format("{0}, {1}", info.ClassName, info.AssemblyName), true);
            return (IRenderer)Activator.CreateInstance(controlType);
        }

        public static IPresenter CreatePresenter(PresenterInfo info, Kistl.API.IDataObject obj, Visual v, IBasicControl ctrl)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            Type controlType = Type.GetType(String.Format("{0}, {1}", info.ClassName, info.AssemblyName), true);
            IPresenter result = (IPresenter)Activator.CreateInstance(controlType);
            result.InitializeComponent(obj, v, ctrl);
            return result;
        }

        public static Template FindTemplate(this IDataObject obj, TemplateUsage templateUsage)
        {
            return Template.DefaultTemplate(obj.Type);
        }
    }

    public class ControlInfo
    {
        // public string Module { get; set; }
        public VisualType Control { get; set; }
        public Toolkit Platform { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public bool Container { get; set; }

        public static IList<ControlInfo> Implementations = new List<ControlInfo>(new[] {
            // Test Controls
            new ControlInfo() { Platform = Toolkit.ASPNET, Control = VisualType.PropertyGroup,
                Container = true,
                AssemblyName = "blah", ClassName = "foo" },
            new ControlInfo() { Platform = Toolkit.ASPNET, Control = VisualType.String,
                Container = false,
                AssemblyName = "blah", ClassName = "foo" },

            // The actual Renderer for WPF
            new ControlInfo() { Platform = Toolkit.WPF, Control = VisualType.Renderer,
                Container = true,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.Renderer" },

            // other WPF Controls, non-properties
            new ControlInfo() { Platform = Toolkit.WPF, Control = VisualType.Object,
                Container = true,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.WPFWindow" },
            new ControlInfo() { Platform = Toolkit.WPF, Control = VisualType.PropertyGroup,
                Container = true,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.GroupBoxWrapper" },

            // other WPF Controls, properties
            new ControlInfo() { Platform = Toolkit.WPF, Control = VisualType.ObjectReference,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditPointerProperty" },
            new ControlInfo() { Platform = Toolkit.WPF, Control = VisualType.ObjectList,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.ObjectList" },

            new ControlInfo() { Platform = Toolkit.WPF, Control = VisualType.Boolean,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditBoolProperty" },
            new ControlInfo() { Platform = Toolkit.WPF, Control = VisualType.DateTime,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditDateTimeProperty" },
            new ControlInfo() { Platform = Toolkit.WPF, Control = VisualType.Double,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditDoubleProperty" },
            new ControlInfo() { Platform = Toolkit.WPF, Control = VisualType.Integer,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditIntProperty" },
            new ControlInfo() { Platform = Toolkit.WPF, Control = VisualType.String,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditSimpleProperty" },
        });
    }

    public class PresenterInfo
    {
        // public string Module { get; set; }
        public VisualType Control { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }

        public static IList<PresenterInfo> Implementations = new List<PresenterInfo>(new[] {
            // non-property presenters
            new PresenterInfo() { Control = VisualType.Object,
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.ObjectPresenter" },
            new PresenterInfo() { Control = VisualType.PropertyGroup,
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.GroupPresenter" },

            // property presenters
            new PresenterInfo() { Control = VisualType.ObjectReference,
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.PointerPresenter" },
            new PresenterInfo() { Control = VisualType.ObjectList,
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.ObjectListPresenter" },

            new PresenterInfo() { Control = VisualType.Boolean,
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.BoolPresenter" },
            new PresenterInfo() { Control = VisualType.DateTime,
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DateTimePresenter" },
            new PresenterInfo() { Control = VisualType.Double,
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DoublePresenter" },
            new PresenterInfo() { Control = VisualType.Integer,
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.IntPresenter" },
            new PresenterInfo() { Control = VisualType.String,
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.StringPresenter" },
        });

    }

    public enum Toolkit
    {
        WPF,
        ASPNET,
        TEST
    }

}
