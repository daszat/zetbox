using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.GUI.Hacks;
using Kistl.API.Client;

namespace Kistl.App.GUI
{
    public class CustomClientActions_GUI
    {
        public void OnToString_Icon(Icon obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.IconFile;
        }

        public void OnToString_Visual(Visual obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Method != null)
            {
                e.Result = String.Format("Method Visual: {0} for {1}", obj.ControlType, obj.Method);
            }
            else if (obj.Property != null)
            {
                e.Result = String.Format("Property Visual: {0} for {1}", obj.ControlType, obj.Property);
            }
            else
            {
                e.Result = String.Format("Other Visual: {0}, ", obj.ControlType, obj.Description);
            }
        }

        public void OnToString_PresentableModelDescriptor(PresentableModelDescriptor obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} (default: {1})",
                obj.PresentableModelRef == null ? "(no type)" : obj.PresentableModelRef.ToString(),
                obj.DefaultVisualType);
        }


        public void OnPrepareDefault_Template(Template obj, ObjectClass cls)
        {
            var displayedType = cls.GetDataType();
            obj.DisplayedTypeAssembly = obj.Context.GetQuery<Assembly>().Where(assembly => assembly.AssemblyName == displayedType.Assembly.FullName).SingleOrDefault();
            obj.DisplayedTypeFullName = displayedType.FullName;
            obj.DisplayName = String.Format("Default Template for {0}", displayedType.Name);
            obj.VisualTree = TemplateHelper.CreateDefaultVisualTree(obj.Context, cls);
        }

        public void OnToString_ViewDescriptor(ViewDescriptor obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}/{1} View of {2}",
                obj.Toolkit,
                obj.VisualType,
                obj.PresentedModelDescriptor == null || obj.PresentedModelDescriptor.PresentableModelRef == null
                    ? "(none)"
                    : obj.PresentedModelDescriptor.PresentableModelRef.ToString());
        }

    }
}
