
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

    using Kistl.DalProvider.Frozen;

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
        public virtual Kistl.App.Projekte.Projekt Projekt
        {
            get
            {
                return _Projekt;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Projekt != value)
                {
                    NotifyPropertyChanging("Projekt");
                    _Projekt = value;
                    NotifyPropertyChanged("Projekt");;
                }
            }
        }
        private Kistl.App.Projekte.Projekt _Projekt;

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


        internal Kostentraeger__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


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