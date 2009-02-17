
namespace Kistl.App.Test
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Muhblah")]
    [System.Diagnostics.DebuggerDisplay("Muhblah")]
    public class Muhblah__Implementation__ : BaseServerDataObject_EntityFramework, Muhblah
    {

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");;
                }
            }
        }
        private int _ID;

		public override Type GetInterfaceType()
		{
			return typeof(Muhblah);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Muhblah != null)
            {
                OnToString_Muhblah(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Muhblah> OnToString_Muhblah;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Muhblah != null) OnPreSave_Muhblah(this);
        }
        public event ObjectEventHandler<Muhblah> OnPreSave_Muhblah;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Muhblah != null) OnPostSave_Muhblah(this);
        }
        public event ObjectEventHandler<Muhblah> OnPostSave_Muhblah;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
        }

#endregion

    }


}