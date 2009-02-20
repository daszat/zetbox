
namespace Kistl.App.GUI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Kistl.API;

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Icon")]
    public class Icon__Implementation__Frozen : BaseFrozenDataObject, Icon
    {


        /// <summary>
        /// Filename of the Icon
        /// </summary>
        // value type property
        public virtual string IconFile
        {
            get
            {
                return _IconFile;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IconFile != value)
                {
                    NotifyPropertyChanging("IconFile");
                    _IconFile = value;
                    NotifyPropertyChanged("IconFile");
                }
            }
        }
        private string _IconFile;

		public override Type GetInterfaceType()
		{
			return typeof(Icon);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Icon != null)
            {
                OnToString_Icon(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Icon> OnToString_Icon;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Icon != null) OnPreSave_Icon(this);
        }
        public event ObjectEventHandler<Icon> OnPreSave_Icon;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Icon != null) OnPostSave_Icon(this);
        }
        public event ObjectEventHandler<Icon> OnPostSave_Icon;


        internal Icon__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, Icon__Implementation__Frozen> DataStore = new Dictionary<int, Icon__Implementation__Frozen>(13);
		internal static void CreateInstances()
		{
			DataStore[1] = new Icon__Implementation__Frozen(1);

			DataStore[2] = new Icon__Implementation__Frozen(2);

			DataStore[3] = new Icon__Implementation__Frozen(3);

			DataStore[4] = new Icon__Implementation__Frozen(4);

			DataStore[5] = new Icon__Implementation__Frozen(5);

			DataStore[6] = new Icon__Implementation__Frozen(6);

			DataStore[7] = new Icon__Implementation__Frozen(7);

			DataStore[8] = new Icon__Implementation__Frozen(8);

			DataStore[9] = new Icon__Implementation__Frozen(9);

			DataStore[10] = new Icon__Implementation__Frozen(10);

			DataStore[11] = new Icon__Implementation__Frozen(11);

			DataStore[12] = new Icon__Implementation__Frozen(12);

			DataStore[13] = new Icon__Implementation__Frozen(13);

		}

		internal static void FillDataStore() {
			DataStore[1].IconFile = @"app.ico";
			DataStore[1].Seal();
			DataStore[2].IconFile = @"Code_ClassCS.ico";
			DataStore[2].Seal();
			DataStore[3].IconFile = @"VSProject_genericproject.ico";
			DataStore[3].Seal();
			DataStore[4].IconFile = @"Resource_Bitmap.ico";
			DataStore[4].Seal();
			DataStore[5].IconFile = @"user.ico";
			DataStore[5].Seal();
			DataStore[6].IconFile = @"users.ico";
			DataStore[6].Seal();
			DataStore[7].IconFile = @"propertiesORoptions.ico";
			DataStore[7].Seal();
			DataStore[8].IconFile = @"UtilityText.ico";
			DataStore[8].Seal();
			DataStore[9].IconFile = @"otheroptions.ico";
			DataStore[9].Seal();
			DataStore[10].IconFile = @"cab.ico";
			DataStore[10].Seal();
			DataStore[11].IconFile = @"Code_Component.ico";
			DataStore[11].Seal();
			DataStore[12].IconFile = @"document.ico";
			DataStore[12].Seal();
			DataStore[13].IconFile = @"idr_dll.ico";
			DataStore[13].Seal();
	
		}

#region Serializer

        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.IO.BinaryReader binStream)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}