using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.GUI.Renderer;
using Kistl.Client;

namespace Kistl.GUI.DB
{
    public static class KistlGUIContext
    {

        public static ControlInfo FindControlInfo(Toolkit platform, Visual visual)
        {
            if (visual == null)
                throw new ArgumentNullException("visual", "KistlGUIContext.FindControlInfo(platform, visual): visual must not be null");

            return FindControlInfo(platform, visual.ControlType);
        }

        private static ControlInfo FindControlInfo(Toolkit platform, VisualType type)
        {
            return (from ci in ControlInfo.Implementations
                    where ci.ControlType == type
                        && ci.Platform == platform
                    select ci).Single();
        }

        public static PresenterInfo FindPresenterInfo(Visual visual, Type sourceType)
        {
            if (visual == null)
                throw new ArgumentNullException("visual", "KistlGUIContext.FindPresenterInfo(visual, sourceType): visual must not be null");

            if (sourceType == null)
                throw new ArgumentNullException("sourceType", "KistlGUIContext.FindPresenterInfo(visual, sourceType): sourceType must not be null");

            return (from pi in PresenterInfo.Implementations
                    where
                        pi.Control == visual.ControlType
                        && pi.SourceType == sourceType
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
            if (controlType.IsGenericTypeDefinition)
                controlType = controlType.MakeGenericType(new Type[] { v.Property.GetDataCLRType() });

            IPresenter result = (IPresenter)Activator.CreateInstance(controlType);
            result.InitializeComponent(obj, v, ctrl);
            return result;
        }

        public static Template FindTemplate(this IDataObject obj, TemplateUsage templateUsage)
        {
            return Template.DefaultTemplate(obj.GetType());
        }
    }

    public class ControlInfo
    {
        // public string Module { get; set; }
        public VisualType ControlType { get; set; }
        public Toolkit Platform { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public bool Container { get; set; }

        // This is just a Mock
        public static IList<ControlInfo> Implementations = new List<ControlInfo>(new[] {
#region ASP.NET
            // The actual Renderer for APS.NET
            new ControlInfo() { Platform = Toolkit.ASPNET, ControlType = VisualType.Renderer,
                Container = true,
                AssemblyName = "Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0",
                ClassName = "Kistl.Client.ASPNET.Toolkit.Renderer" },

            // Main "Window" for ASP.NET
            new ControlInfo() { Platform = Toolkit.ASPNET, ControlType = VisualType.Object,
                Container = true,
                AssemblyName = "Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0",
                ClassName = "Kistl.Client.ASPNET.Toolkit.ObjectPanel" },

            // Controls
            new ControlInfo() { Platform = Toolkit.ASPNET, ControlType = VisualType.Boolean,
                Container = false,
                AssemblyName = "Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0",
                ClassName = "Kistl.Client.ASPNET.Toolkit.BoolPropertyControl" },

            new ControlInfo() { Platform = Toolkit.ASPNET, ControlType = VisualType.DateTime,
                Container = false,
                AssemblyName = "Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0",
                ClassName = "Kistl.Client.ASPNET.Toolkit.DateTimePropertyControl" },

            new ControlInfo() { Platform = Toolkit.ASPNET, ControlType = VisualType.Double,
                Container = false,
                AssemblyName = "Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0",
                ClassName = "Kistl.Client.ASPNET.Toolkit.DoublePropertyControl" },

            new ControlInfo() { Platform = Toolkit.ASPNET, ControlType = VisualType.Integer,
                Container = false,
                AssemblyName = "Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0",
                ClassName = "Kistl.Client.ASPNET.Toolkit.IntPropertyControl" },

            new ControlInfo() { Platform = Toolkit.ASPNET, ControlType = VisualType.ObjectList,
                Container = false,
                AssemblyName = "Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0",
                ClassName = "Kistl.Client.ASPNET.Toolkit.ObjectListControl" },

            new ControlInfo() { Platform = Toolkit.ASPNET, ControlType = VisualType.ObjectReference,
                Container = false,
                AssemblyName = "Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0",
                ClassName = "Kistl.Client.ASPNET.Toolkit.ObjectReferencePropertyControl" },

            new ControlInfo() { Platform = Toolkit.ASPNET, ControlType = VisualType.String,
                Container = false,
                AssemblyName = "Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0",
                ClassName = "Kistl.Client.ASPNET.Toolkit.StringPropertyControl" },

#endregion

#region WPF
            // The actual Renderer for WPF
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.Renderer,
                Container = true,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.Renderer" },

            // other WPF Controls, non-properties
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.Object,
                Container = true,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.ObjectTabItem" },
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.PropertyGroup,
                Container = true,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.GroupBoxWrapper" },

            // other WPF Controls, properties
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.ObjectReference,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.ObjectReferenceControl" },
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.ObjectList,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.ObjectListControl" },
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.SimpleObjectList,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EnumEntryListControl" },

            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.Boolean,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditBoolProperty" },
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.BooleanList,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.BooleanListControl" },

            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.DateTime,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditDateTimeProperty" },
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.DateTimeList,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.DateTimeListControl" },

            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.Double,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditDoubleProperty" },
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.DoubleList,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.DoubleListControl" },

            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.Integer,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditIntProperty" },
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.IntegerList,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.IntegerListControl" },

            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.String,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.EditSimpleProperty" },
            new ControlInfo() { Platform = Toolkit.WPF, ControlType = VisualType.StringList,
                Container = false,
                AssemblyName = "Kistl.Client.WPF, Version=1.0.0.0",
                ClassName = "Kistl.GUI.Renderer.WPF.StringListControl" },
#endregion
        });
    }

    public class PresenterInfo
    {
        // public string Module { get; set; }
        public VisualType Control { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        /// <summary>
        /// The Type of the object that is presented
        /// </summary>
        public Type SourceType { get; set; }

        public static IList<PresenterInfo> Implementations = new List<PresenterInfo>(new[] {
            // non-property presenters
            new PresenterInfo() { Control = VisualType.Object, SourceType = typeof(IDataObject),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.ObjectPresenter" },
            new PresenterInfo() { Control = VisualType.PropertyGroup, // TODO: SourceType = typeof(PropertyGroup),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.GroupPresenter" },

            // property presenters
            new PresenterInfo() { Control = VisualType.ObjectReference, SourceType = typeof(ObjectReferenceProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.ObjectReferencePresenter`1" },
            new PresenterInfo() { Control = VisualType.ObjectList, SourceType = typeof(ObjectReferenceProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.ObjectListPresenter`1" },
            new PresenterInfo() { Control = VisualType.ObjectList, SourceType = typeof(BackReferenceProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.BackReferencePresenter`1" },

            new PresenterInfo() { Control = VisualType.Boolean, SourceType = typeof(BoolProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultStructPresenter`1" },
            new PresenterInfo() { Control = VisualType.BooleanList, SourceType = typeof(BoolProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultListPresenter`1" },

            new PresenterInfo() { Control = VisualType.DateTime, SourceType = typeof(DateTimeProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultStructPresenter`1" },
            new PresenterInfo() { Control = VisualType.DateTimeList, SourceType = typeof(DateTimeProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultListPresenter`1" },

            new PresenterInfo() { Control = VisualType.Double, SourceType = typeof(DoubleProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultStructPresenter`1" },
            new PresenterInfo() { Control = VisualType.DoubleList, SourceType = typeof(DoubleProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultListPresenter`1" },

            new PresenterInfo() { Control = VisualType.Integer, SourceType = typeof(IntProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultStructPresenter`1" },
            new PresenterInfo() { Control = VisualType.IntegerList, SourceType = typeof(IntProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultListPresenter`1" },

            new PresenterInfo() { Control = VisualType.String, SourceType = typeof(StringProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultValuePresenter`1" },
            new PresenterInfo() { Control = VisualType.StringList, SourceType = typeof(StringProperty),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultListPresenter`1" },

            new PresenterInfo() { Control = VisualType.SimpleObjectList, SourceType = typeof(IList<EnumerationEntry>),
                AssemblyName = "Kistl.Client, Version=1.0.0.0", ClassName = "Kistl.GUI.DefaultListPresenter`1" },
        });

    }

    public enum Toolkit
    {
        WPF,
        ASPNET,
        TEST
    }

}
