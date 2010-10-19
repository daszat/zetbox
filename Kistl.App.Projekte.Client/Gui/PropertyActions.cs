namespace Kistl.App.Projekte.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using Kistl.App.Extensions;
    using Kistl.Client;

    /// <summary>
    /// Sets each Property a default ValueModelDescriptor.
    /// GUI Tasks - so in GUI Actions.
    /// Only called on client
    /// Note: OnNotifyCreated should always be implemented on the client side. importing or deploying also calls this event.
    /// </summary>
    public static class PropertyActions
    {
        public static void OnNotifyCreated_BoolProperty(Kistl.App.Base.BoolProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Bool);
        }

        public static void OnNotifyCreated_DateTimeProperty(Kistl.App.Base.DateTimeProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_DateTime);
        }

        public static void OnNotifyCreated_DoubleProperty(Kistl.App.Base.DoubleProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Double);
        }

        public static void OnNotifyCreated_EnumerationProperty(Kistl.App.Base.EnumerationProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Enum);
        }

        public static void OnNotifyCreated_GuidProperty(Kistl.App.Base.GuidProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Guid);
        }

        public static void OnNotifyCreated_IntProperty(Kistl.App.Base.IntProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Int);
        }

        public static void OnNotifyCreated_DecimalProperty(Kistl.App.Base.DecimalProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Decimal);
        }

        public static void OnNotifyCreated_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj)
        {
            // Is implemented by CreateNavigator
            // At creating time there is no way to discover if the navigator is a Reference or List
        }

        public static void OnNotifyCreated_StringProperty(Kistl.App.Base.StringProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_ReferencePropertyModel_String);
        }

        public static void OnNotifyCreated_CompoundObjectProperty(Kistl.App.Base.CompoundObjectProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_CompoundObjectPropertyViewModel);
        }
    }
}
