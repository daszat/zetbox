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
    /// </summary>
    public class PropertyActions
    {
        // TODO: Replace this when NamedInstances are introduced 
        public static Guid PresentableModelDescriptor_NullableValuePropertyModel_Bool = new Guid("09d1f453-d0d9-429e-88e7-e84b33de7c2e");
        public static Guid PresentableModelDescriptor_NullableValuePropertyModel_DateTime = new Guid("fc74b434-3801-4e4a-ab67-e65a9e014005");
        public static Guid PresentableModelDescriptor_NullableValuePropertyModel_Double = new Guid("3a3d0c1c-679e-4d4d-adeb-3ab260079ccb");
        public static Guid PresentableModelDescriptor_NullableValuePropertyModel_Enum = new Guid("a6ff986c-a485-4c4d-947c-e59d14112ec2");
        public static Guid PresentableModelDescriptor_NullableValuePropertyModel_Int = new Guid("edaf9334-dc36-4778-aa33-1e5cdaeeb767");
        public static Guid PresentableModelDescriptor_NullableValuePropertyModel_Guid = new Guid("2B6FB70F-A382-4057-A139-CC33333D619D");
        public static Guid PresentableModelDescriptor_ObjectReferenceModel = new Guid("83aae6fd-0fae-4348-b313-737a6e751027");
        public static Guid PresentableModelDescriptor_ReferencePropertyModel_String = new Guid("975eee82-e7e1-4a12-ab43-d2e3bc3766e4");

        public void OnNotifyCreated_BoolProperty(Kistl.App.Base.BoolProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<PresentableModelDescriptor>(PresentableModelDescriptor_NullableValuePropertyModel_Bool);
        }

        public void OnNotifyCreated_DateTimeProperty(Kistl.App.Base.DateTimeProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<PresentableModelDescriptor>(PresentableModelDescriptor_NullableValuePropertyModel_DateTime);
        }

        public void OnNotifyCreated_DoubleProperty(Kistl.App.Base.DoubleProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<PresentableModelDescriptor>(PresentableModelDescriptor_NullableValuePropertyModel_Double);
        }

        public void OnNotifyCreated_EnumerationProperty(Kistl.App.Base.EnumerationProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<PresentableModelDescriptor>(PresentableModelDescriptor_NullableValuePropertyModel_Enum);
        }

        public void OnNotifyCreated_GuidProperty(Kistl.App.Base.GuidProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<PresentableModelDescriptor>(PresentableModelDescriptor_NullableValuePropertyModel_Guid);
        }

        public void OnNotifyCreated_IntProperty(Kistl.App.Base.IntProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<PresentableModelDescriptor>(PresentableModelDescriptor_NullableValuePropertyModel_Int);
        }

        public void OnNotifyCreated_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<PresentableModelDescriptor>(PresentableModelDescriptor_ObjectReferenceModel);
        }

        public void OnNotifyCreated_StringProperty(Kistl.App.Base.StringProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<PresentableModelDescriptor>(PresentableModelDescriptor_ReferencePropertyModel_String);
        }

        public void OnNotifyCreated_StructProperty(Kistl.App.Base.StructProperty obj)
        {
            // TODO:
        }
    }
}
