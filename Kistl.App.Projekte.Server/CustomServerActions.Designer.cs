using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// Das muss man auf diese Methode machen, weil ich beim Ableiten keine Chance habe, 
    /// fremde Objekte zu erweitern.
    /// Autogeneriet lt. Metadaten
    /// </summary>
    public partial class CustomServerActions : API.Server.ICustomServerActions
    {
        public void Attach(Kistl.API.IDataObject obj)
        {
            if (obj is Projekt)
            {
                Projekt impl = obj as Projekt;
                impl.OnToString_Projekt += new Kistl.API.ToStringHandler<Projekt>(Projekt_OnToString);
                impl.OnPreSave_Projekt += new Kistl.API.ObjectEventHandler<Projekt>(Projekt_OnPreSetObject);
            }

            if (obj is Task)
            {
                Task impl = obj as Task;
                impl.OnToString_Task += new Kistl.API.ToStringHandler<Task>(Task_OnToString);
                impl.OnPreSave_Task += new Kistl.API.ObjectEventHandler<Task>(Task_OnPreSetObject);
            }

            if (obj is App.Base.ObjectClass)
            {
                App.Base.ObjectClass impl = obj as App.Base.ObjectClass;
                impl.OnToString_ObjectClass += new Kistl.API.ToStringHandler<Kistl.App.Base.ObjectClass>(ObjectClass_OnToString);
                impl.OnPreSave_ObjectClass += new Kistl.API.ObjectEventHandler<Kistl.App.Base.ObjectClass>(impl_OnPreSave_ObjectClass);
            }

            if (obj is App.Base.Method)
            {
                App.Base.Method impl = obj as App.Base.Method;
                impl.OnToString_Method += new Kistl.API.ToStringHandler<Kistl.App.Base.Method>(imp_OnToString_Method);
            }

            if (obj is App.Base.BaseProperty)
            {
                App.Base.BaseProperty impl = obj as App.Base.BaseProperty;
                impl.OnToString_BaseProperty += new Kistl.API.ToStringHandler<Kistl.App.Base.BaseProperty>(impl_OnToString_BaseProperty);
                impl.OnGetDataType_BaseProperty += new Kistl.App.Base.BaseProperty.GetDataType_Handler<Kistl.App.Base.BaseProperty>(impl_OnGetDataType_BaseProperty);
            }

            if (obj is App.Base.StringProperty)
            {
                App.Base.StringProperty impl = obj as App.Base.StringProperty;
                impl.OnGetDataType_StringProperty += new Kistl.App.Base.BaseProperty.GetDataType_Handler<Kistl.App.Base.StringProperty>(impl_OnGetDataType_StringProperty);
            }

            if (obj is App.Base.IntProperty)
            {
                App.Base.IntProperty impl = obj as App.Base.IntProperty;
                impl.OnGetDataType_IntProperty += new Kistl.App.Base.BaseProperty.GetDataType_Handler<Kistl.App.Base.IntProperty>(impl_OnGetDataType_IntProperty);
            }

            if (obj is App.Base.BoolProperty)
            {
                App.Base.BoolProperty impl = obj as App.Base.BoolProperty;
                impl.OnGetDataType_BoolProperty += new Kistl.App.Base.BaseProperty.GetDataType_Handler<Kistl.App.Base.BoolProperty>(impl_OnGetDataType_BoolProperty);
            }

            if (obj is App.Base.DoubleProperty)
            {
                App.Base.DoubleProperty impl = obj as App.Base.DoubleProperty;
                impl.OnGetDataType_DoubleProperty += new Kistl.App.Base.BaseProperty.GetDataType_Handler<Kistl.App.Base.DoubleProperty>(impl_OnGetDataType_DoubleProperty);
            }

            if (obj is App.Base.DateTimeProperty)
            {
                App.Base.DateTimeProperty impl = obj as App.Base.DateTimeProperty;
                impl.OnGetDataType_DateTimeProperty += new Kistl.App.Base.BaseProperty.GetDataType_Handler<Kistl.App.Base.DateTimeProperty>(impl_OnGetDataType_DateTimeProperty);
            }

            if (obj is App.Base.ObjectReferenceProperty)
            {
                App.Base.ObjectReferenceProperty impl = obj as App.Base.ObjectReferenceProperty;
                impl.OnGetDataType_ObjectReferenceProperty += new Kistl.App.Base.BaseProperty.GetDataType_Handler<Kistl.App.Base.ObjectReferenceProperty>(impl_OnGetDataType_ObjectReferenceProperty);
            }

            if (obj is App.Base.BackReferenceProperty)
            {
                App.Base.BackReferenceProperty impl = obj as App.Base.BackReferenceProperty;
                impl.OnGetDataType_BackReferenceProperty += new Kistl.App.Base.BaseProperty.GetDataType_Handler<Kistl.App.Base.BackReferenceProperty>(impl_OnGetDataType_BackReferenceProperty);
            }

        }
    }
}
