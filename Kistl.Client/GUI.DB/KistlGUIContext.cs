using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.App.GUI;
using Kistl.GUI.Renderer;
using Kistl.Client;

namespace Kistl.GUI.DB
{
    public static class KistlGUIContext
    {
        private static IKistlContext _GuiContext = null;
        public static IKistlContext GuiContext { get { if (_GuiContext == null) InitGUIContext(); return _GuiContext; } }
        private static void InitGUIContext() { _GuiContext = KistlContext.GetContext(); }

        public static ControlInfo FindControlInfo(Toolkit platform, Visual visual)
        {
            if (visual == null)
                throw new ArgumentNullException("visual", "KistlGUIContext.FindControlInfo(platform, visual): visual must not be null");

            return FindControlInfo(platform, visual.ControlType);
        }

        private static ControlInfo FindControlInfo(Toolkit platform, VisualType type)
        {
            // TODO: Remove superfluous ToList()
            return (from ci in GuiContext.GetQuery<ControlInfo>().ToList()
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

            // TODO: Remove superfluous ToList()
            return (from pi in GuiContext.GetQuery<PresenterInfo>().ToList()
                    where
                        pi.ControlType == visual.ControlType
                        && pi.DataAssembly.AssemblyName == sourceType.Assembly.FullName
                        && pi.DataTypeName == sourceType.FullName
                    select pi).Single();
        }

        public static IBasicControl CreateControl(ControlInfo info)
        {
            Type controlType = Type.GetType(String.Format("{0}, {1}", info.ClassName, info.Assembly.AssemblyName), true);
            return (IBasicControl)Activator.CreateInstance(controlType);
        }

        public static Kistl.GUI.Renderer.IRenderer CreateRenderer(Toolkit platform)
        {
            var info = FindControlInfo(platform, VisualType.Renderer);
            Type controlType = Type.GetType(String.Format("{0}, {1}", info.ClassName, info.Assembly.AssemblyName), true);
            return (Kistl.GUI.Renderer.IRenderer)Activator.CreateInstance(controlType);
        }

        public static IPresenter CreatePresenter(PresenterInfo info, Kistl.API.IDataObject obj, Visual v, IBasicControl ctrl)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            Type controlType = Type.GetType(String.Format("{0}, {1}", info.PresenterTypeName, info.PresenterAssembly.AssemblyName), true);
            if (controlType.IsGenericTypeDefinition)
                if (v.Property != null)
                    controlType = controlType.MakeGenericType(new Type[] { v.Property.GetPropertyType() });
                else if (v.Method != null)
                    controlType = controlType.MakeGenericType(new Type[] { v.Method.GetReturnParameter().GetParameterType() });

            IPresenter result = (IPresenter)Activator.CreateInstance(controlType);
            result.InitializeComponent(obj, v, ctrl);
            return result;
        }

        public static Template FindTemplate(this IDataObject obj, TemplateUsage templateUsage)
        {
            return Template.DefaultTemplate(obj.GetType());
        }
    }
}
