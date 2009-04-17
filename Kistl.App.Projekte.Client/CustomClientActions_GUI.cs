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

        public void OnGetGUIRepresentation_Property(Property obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "<app:EditSimpleProperty xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:app=\"clr-namespace:Kistl.Client.Controls;assembly=Kistl.Client\"/>";
        }

        public void OnGetGUIRepresentation_BoolProperty(BoolProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "<app:EditBoolProperty xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:app=\"clr-namespace:Kistl.Client.Controls;assembly=Kistl.Client\"/>";
        }

        public void OnGetGUIRepresentation_DateTimeProperty(DateTimeProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "<app:EditDateTimeProperty xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:app=\"clr-namespace:Kistl.Client.Controls;assembly=Kistl.Client\"/>";
        }

        public void OnGetGUIRepresentation_ObjectReferenceProperty(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "<app:EditPointerProperty xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:app=\"clr-namespace:Kistl.Client.Controls;assembly=Kistl.Client\"/>";
        }

        public void OnGetGUIRepresentation_StringProperty(StringProperty obj, MethodReturnEventArgs<string> e)
        {
            // e.Result = "<app:EditSimpleProperty xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:app=\"clr-namespace:Kistl.Client.Controls;assembly=Kistl.Client\"/>";
        }

        public void OnToString_ControlInfo(ControlInfo obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}: {1} implemented by {2} from {3}", obj.Platform, obj.ControlType, obj.ClassName, obj.Assembly);
        }

        public void OnToString_PresenterInfo(PresenterInfo obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} implemented by {1} from {2}", obj.ControlType, obj.PresenterTypeName, obj.PresenterAssembly);
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
            e.Result = String.Format("{0}: Display model {1} with {2}",
                obj.Toolkit,
                obj.PresentedModelDescriptor == null || obj.PresentedModelDescriptor.PresentableModelRef == null
                    ? "(none)"
                    : obj.PresentedModelDescriptor.PresentableModelRef.AsType(true).Name,
                obj.ControlRef == null ? "(none)" : obj.ControlRef.AsType(true).Name);
        }

        public void OnGetDefaultModelRef_ObjectClass(ObjectClass objClass, MethodReturnEventArgs<TypeRef> e)
        {
            // TODO: use configuration for default model
            e.Result = objClass.DefaultModel ?? objClass.Context.Find<TypeRef>(83);
        }

        public void OnGetDefaultModelRef_BoolProperty(BoolProperty prop, MethodReturnEventArgs<TypeRef> e)
        {
            if (prop.IsList)
            {

            }
            else
            {
                //e.Result = typeof(NullableValuePropertyModel<Boolean>).ToRef(prop.Context);
            }
        }

        public void OnGetDefaultModelRef_DateTimeProperty(DateTimeProperty prop, MethodReturnEventArgs<TypeRef> e)
        {
            if (prop.IsList)
            {

            }
            else
            {
                //e.Result = typeof(NullableValuePropertyModel<Boolean>).ToRef(prop.Context);
            }
        }
    }
}
