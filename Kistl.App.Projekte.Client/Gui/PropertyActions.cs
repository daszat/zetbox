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

    /// <summary>
    /// Sets each Property a default ValueModelDescriptor.
    /// GUI Tasks - so in GUI Actions.
    /// Only called on client
    /// Note: OnNotifyCreated should always be implemented on the client side. importing or deploying also calls this event.
    /// </summary>
    public static class PropertyActions
    {
        // TODO: Replace this when NamedInstances are introduced 
        public static readonly Guid ViewModelDescriptor_NullableValuePropertyModel_Bool = new Guid("09d1f453-d0d9-429e-88e7-e84b33de7c2e");
        public static readonly Guid ViewModelDescriptor_NullableValuePropertyModel_DateTime = new Guid("fc74b434-3801-4e4a-ab67-e65a9e014005");
        public static readonly Guid ViewModelDescriptor_NullableValuePropertyModel_Double = new Guid("3a3d0c1c-679e-4d4d-adeb-3ab260079ccb");
        public static readonly Guid ViewModelDescriptor_NullableValuePropertyModel_Enum = new Guid("a6ff986c-a485-4c4d-947c-e59d14112ec2");
        public static readonly Guid ViewModelDescriptor_NullableValuePropertyModel_Int = new Guid("edaf9334-dc36-4778-aa33-1e5cdaeeb767");
        public static readonly Guid ViewModelDescriptor_NullableValuePropertyModel_Decimal = new Guid("481d7a65-208c-4706-8d4d-64ea629a109c");
        public static readonly Guid ViewModelDescriptor_NullableValuePropertyModel_Guid = new Guid("2B6FB70F-A382-4057-A139-CC33333D619D");
        public static readonly Guid ViewModelDescriptor_ObjectReferenceModel = new Guid("83aae6fd-0fae-4348-b313-737a6e751027");
        public static readonly Guid ViewModelDescriptor_ObjectListModel = new Guid("9fce01fe-fd6d-4e21-8b55-08d5e38aea36");
        public static readonly Guid ViewModelDescriptor_ObjectCollectionModel = new Guid("67A49C49-B890-4D35-A8DB-1F8E43BFC7DF");
        public static readonly Guid ViewModelDescriptor_ReferencePropertyModel_String = new Guid("975eee82-e7e1-4a12-ab43-d2e3bc3766e4");
        public static readonly Guid ViewModelDescriptor_CompoundObjectPropertyViewModel = new Guid("A63B9F47-18B7-463D-A06B-7B636DE9553F");

        public static void OnNotifyCreated_BoolProperty(Kistl.App.Base.BoolProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_NullableValuePropertyModel_Bool);
        }

        public static void OnNotifyCreated_DateTimeProperty(Kistl.App.Base.DateTimeProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_NullableValuePropertyModel_DateTime);
        }

        public static void OnNotifyCreated_DoubleProperty(Kistl.App.Base.DoubleProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_NullableValuePropertyModel_Double);
        }

        public static void OnNotifyCreated_EnumerationProperty(Kistl.App.Base.EnumerationProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_NullableValuePropertyModel_Enum);
        }

        public static void OnNotifyCreated_GuidProperty(Kistl.App.Base.GuidProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_NullableValuePropertyModel_Guid);
        }

        public static void OnNotifyCreated_IntProperty(Kistl.App.Base.IntProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_NullableValuePropertyModel_Int);
        }

        public static void OnNotifyCreated_DecimalProperty(Kistl.App.Base.DecimalProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_NullableValuePropertyModel_Decimal);
        }

        public static void OnNotifyCreated_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj)
        {
            // Is implemented by CreateNavigator
            // At creating time there is no way to discover if the navigator is a Reference or List
        }

        public static void OnNotifyCreated_StringProperty(Kistl.App.Base.StringProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_ReferencePropertyModel_String);
        }

        public static void OnNotifyCreated_CompoundObjectProperty(Kistl.App.Base.CompoundObjectProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_CompoundObjectPropertyViewModel);
        }
    }
}
