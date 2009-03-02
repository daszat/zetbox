
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
    [EdmEntityType(NamespaceName="Model", Name="Kostentraeger")]
    [System.Diagnostics.DebuggerDisplay("Kostentraeger")]
    public class Kostentraeger__Implementation__ : Kistl.App.Zeiterfassung.Zeitkonto__Implementation__, Kostentraeger
    {


        /// <summary>
        /// Projekt des Kostenträgers
        /// </summary>
    /*
    Relation: FK_Projekt_Kostentraeger_Projekt_31
    A: 2 Projekt as Projekt
    B: 3 Kostentraeger as Kostentraeger
    Preferred Storage: 2
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Projekt Projekt
        {
            get
            {
                return Projekt__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Projekt__Implementation__ = (Kistl.App.Projekte.Projekt__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Projekt
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Projekt != null)
                {
                    _fk_Projekt = Projekt.ID;
                }
                return _fk_Projekt;
            }
            set
            {
                _fk_Projekt = value;
            }
        }
        private int? _fk_Projekt;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Projekt_Kostentraeger_Projekt_31", "Projekt")]
        public Kistl.App.Projekte.Projekt__Implementation__ Projekt__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Projekt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt__Implementation__>(
                        "Model.FK_Projekt_Kostentraeger_Projekt_31",
                        "Projekt");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Projekt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt__Implementation__>(
                        "Model.FK_Projekt_Kostentraeger_Projekt_31",
                        "Projekt");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Projekte.Projekt__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? Projekt_pos
        {
            get
            {
                return _Projekt_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Projekt_pos != value)
                {
                    NotifyPropertyChanging("Projekt_pos");
                    _Projekt_pos = value;
                    NotifyPropertyChanged("Projekt_pos");
                }
            }
        }
        private int? _Projekt_pos;
        

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
            BinarySerializer.ToStream(this.fk_Projekt, binStream);
            BinarySerializer.ToStream(this._Projekt_pos, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_Projekt;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Projekt = tmp;
            }
            BinarySerializer.FromStream(out this._Projekt_pos, binStream);
        }

#endregion

    }


}