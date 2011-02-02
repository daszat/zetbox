using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client
{
    /// <summary>
    /// Temporary class for holding named object guid
    /// TODO: Replace this when NamedInstances are introduced 
    /// </summary>
    public static class NamedObjects
    {
        public static readonly Guid Module_KistlBase = new Guid("ef6d5bd8-5826-4ce6-a9ca-c8438ddde773");
        public static readonly Guid Module_GUI = new Guid("84f486f7-19fe-49ad-8e4a-6c05089e7684");

        public static readonly Guid Icon_delete_png = new Guid("84e97f4c-ceca-49d6-be50-aaa92b5b1ba8");
        public static readonly Guid Icon_new_png = new Guid("c9b11ee6-8a62-4af3-9ee7-d6e78eeadad7");
        public static readonly Guid Icon_reload_png = new Guid("bbfd9a59-257a-42e6-a1f4-90de28bd7f7a");
        public static readonly Guid Icon_fileopen_png = new Guid("f1f14c5c-4cbd-499e-a388-3cf56aa80a11");
        public static readonly Guid Icon_search_png = new Guid("dbb2f49e-48f4-49fc-a9a6-53a403f8ac4b");
        public static readonly Guid Icon_Printer_png = new Guid("6b6376e1-519b-4601-9b88-4c7b5594ca2d");
        

        public static readonly Guid ViewModelDescriptor_NullableValuePropertyModel_Bool = new Guid("09d1f453-d0d9-429e-88e7-e84b33de7c2e");
        public static readonly Guid ViewModelDescriptor_NullableValuePropertyModel_DateTime = new Guid("fc74b434-3801-4e4a-ab67-e65a9e014005");
        public static readonly Guid ViewModelDescriptor_NullableMonthPropertyViewModel = new Guid("e66f8cad-f532-4c75-8fb0-e1aa8baddd06");
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

        public static readonly Guid ViewModelDescriptor_DataObjectViewModel = new Guid("d8e95ac5-d46a-4dfa-a574-12ea299eadc4");

        public static readonly Guid ViewModelDescriptor_SingleValueFilterViewModel = new Guid("4FF2B6EC-A47F-431B-AA6D-D10B39F8D628");
        public static readonly Guid ViewModelDescriptor_OptionalPredicateFilterViewModel = new Guid("29550c38-e240-46e4-b856-c7066d8395eb");
        public static readonly Guid ViewModelDescriptor_RangeFilterViewModel = new Guid("47ea2958-1036-4fe2-afb5-84568651d817");

        public static readonly Guid ControlKind_Kistl_App_GUI_TextKind = new Guid("A3F09EF3-2FAC-4D7E-AEA8-CBF0EAB4DE70");
        public static readonly Guid ControlKind_Kistl_App_GUI_LauncherKind = new Guid("90D5FF7F-0C82-4278-BB8D-49C240F6BC2C");
        public static readonly Guid ControlKind_Kistl_App_GUI_PasswordKind = new Guid("2f6233fe-6fd6-4145-b438-64cc85f89fd8");
        public static readonly Guid ControlKind_Kistl_App_GUI_MultiLineTextboxKind = new Guid("b8391867-7c76-40fe-b387-96ab5597fd0d");

        public static readonly Guid ControlKind_Kistl_App_GUI_InstanceListKind = new Guid("4e7a8b0b-0c8e-49e1-aa35-d3a9ec7b8e4c");
        public static readonly Guid ControlKind_Kistl_App_GUI_InstanceListHorizontalKind = new Guid("997f9eb7-084f-4703-b905-2b97be27a9f1");

        public static readonly Guid ControlKind_Kistl_App_GUI_InstanceGridKind = new Guid("150c7db0-d068-4fe3-8137-5b23c73e1fc8");
        public static readonly Guid ControlKind_Kistl_App_GUI_InstanceGridHorizontalKind = new Guid("601d4613-016a-4b8b-97fb-53f0f0feb51a");

        public static readonly Guid ControlKind_Kistl_App_GUI_CommandKind = new Guid("8b4273bc-dee4-4985-926d-40d3833fe4f9");
        public static readonly Guid ControlKind_Kistl_App_GUI_CommandLinkKind = new Guid("1c39f2f9-f4ba-4653-b92f-4672dfc4294a");

        public static readonly Guid ControlKind_Kistl_App_GUI_ObjectRefKind = new Guid("b86945ed-9edc-42ca-9ca5-8124fbf47b5c");
        public static readonly Guid ControlKind_Kistl_App_GUI_ObjectRefDropdownKind = new Guid("d30d2676-7f2f-4f0d-895a-e46f19b8f526");
    }
}
