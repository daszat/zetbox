
namespace Kistl.App.Zeiterfassung
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
    [EdmEntityType(NamespaceName="Model", Name="TaetigkeitsArt")]
    [System.Diagnostics.DebuggerDisplay("TaetigkeitsArt")]
    public class TaetigkeitsArt__Implementation__ : BaseServerDataObject_EntityFramework, TaetigkeitsArt
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
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

        /// <summary>
        /// Name der Tätigkeitsart
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Name != value)
                {
                    NotifyPropertyChanging("Name");
                    _Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        private string _Name;

		public override Type GetInterfaceType()
		{
			return typeof(TaetigkeitsArt);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TaetigkeitsArt != null)
            {
                OnToString_TaetigkeitsArt(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<TaetigkeitsArt> OnToString_TaetigkeitsArt;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TaetigkeitsArt != null) OnPreSave_TaetigkeitsArt(this);
        }
        public event ObjectEventHandler<TaetigkeitsArt> OnPreSave_TaetigkeitsArt;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TaetigkeitsArt != null) OnPostSave_TaetigkeitsArt(this);
        }
        public event ObjectEventHandler<TaetigkeitsArt> OnPostSave_TaetigkeitsArt;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Name, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
        }

#endregion

    }


}