
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

    using Kistl.API.Client;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Kostentraeger")]
    public class Kostentraeger__Implementation__ : Kistl.App.Zeiterfassung.Zeitkonto__Implementation__, Kostentraeger
    {


        /// <summary>
        /// Projekt des Kostenträgers
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Projekt Projekt
        {
            get
            {
                if (fk_Projekt.HasValue)
                    return Context.Find<Kistl.App.Projekte.Projekt>(fk_Projekt.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();

                var oldValue = Projekt;
                
                // shortcut noops
                if (Object.Equals(oldValue, value))
					return;
                
                // fix up inverse reference
                if (value != null && value.ID != fk_Projekt)
                {
					if (oldValue != null)
						oldValue.Kostentraeger.Remove(this);
                    fk_Projekt = value.ID;
                    value.Kostentraeger.Add(this);
                }
                else
                {
					if (oldValue != null)
	                    oldValue.Kostentraeger.Remove(this);
                    fk_Projekt = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Projekt
        {
            get
            {
                return _fk_Projekt;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Projekt != value)
                {
                    NotifyPropertyChanging("Projekt");
                    _fk_Projekt = value;
                    NotifyPropertyChanging("Projekt");
                }
            }
        }
        private int? _fk_Projekt;

		public override Type GetInterfaceType()
		{
			return typeof(Kostentraeger);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Kostentraeger != null)
            {
                OnToString_Kostentraeger(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Kostentraeger> OnToString_Kostentraeger;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Kostentraeger != null) OnPreSave_Kostentraeger(this);
        }
        public event ObjectEventHandler<Kostentraeger> OnPreSave_Kostentraeger;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Kostentraeger != null) OnPostSave_Kostentraeger(this);
        }
        public event ObjectEventHandler<Kostentraeger> OnPostSave_Kostentraeger;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_Projekt, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Projekt, binStream);
        }

#endregion

    }


}