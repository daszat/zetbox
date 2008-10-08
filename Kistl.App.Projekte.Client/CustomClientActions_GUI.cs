using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.App.GUI
{
    public class CustomClientActions_GUI
    {
        public void OnToString_Icon(Icon obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.IconFile;
        }

        public void OnGetGUIRepresentation_BaseProperty(BaseProperty obj, MethodReturnEventArgs<string> e)
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

    }
}
