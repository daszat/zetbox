
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
    [EdmEntityType(NamespaceName="Model", Name="ArbeitszeitEintrag")]
    [System.Diagnostics.DebuggerDisplay("ArbeitszeitEintrag")]
    public class ArbeitszeitEintrag__Implementation__ : BaseServerDataObject_EntityFramework, ArbeitszeitEintrag
    {
    
		public ArbeitszeitEintrag__Implementation__()
		{
            {
            }
        }

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
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// Wann die Anwesenheit angefangen hat.
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual DateTime Anfang
        {
            get
            {
                return _Anfang;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Anfang != value)
                {
					var __oldValue = _Anfang;
                    NotifyPropertyChanging("Anfang", __oldValue, value);
                    _Anfang = value;
                    NotifyPropertyChanged("Anfang", __oldValue, value);
                }
            }
        }
        private DateTime _Anfang;

        /// <summary>
        /// Wann die Anwesenheit geendet hat.
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual DateTime Ende
        {
            get
            {
                return _Ende;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Ende != value)
                {
					var __oldValue = _Ende;
                    NotifyPropertyChanging("Ende", __oldValue, value);
                    _Ende = value;
                    NotifyPropertyChanged("Ende", __oldValue, value);
                }
            }
        }
        private DateTime _Ende;

        /// <summary>
        /// Welcher Mitarbeiter diese Arbeitszeit geleistet hat.
        /// </summary>
    /*
    Relation: FK_ArbeitszeitEintrag_Mitarbeiter_Arbeitszeit_81
    A: ZeroOrMore ArbeitszeitEintrag as Arbeitszeit
    B: One Mitarbeiter as Mitarbeiter
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Mitarbeiter Mitarbeiter
        {
            get
            {
                return Mitarbeiter__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Mitarbeiter__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Mitarbeiter
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Mitarbeiter != null)
                {
                    _fk_Mitarbeiter = Mitarbeiter.ID;
                }
                return _fk_Mitarbeiter;
            }
            set
            {
                _fk_Mitarbeiter = value;
            }
        }
        private int? _fk_Mitarbeiter;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ArbeitszeitEintrag_Mitarbeiter_Arbeitszeit_81", "Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter__Implementation__ Mitarbeiter__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_ArbeitszeitEintrag_Mitarbeiter_Arbeitszeit_81",
                        "Mitarbeiter");
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
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_ArbeitszeitEintrag_Mitarbeiter_Arbeitszeit_81",
                        "Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ArbeitszeitEintrag));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ArbeitszeitEintrag)obj;
			var otherImpl = (ArbeitszeitEintrag__Implementation__)obj;
			var me = (ArbeitszeitEintrag)this;

			me.Anfang = other.Anfang;
			me.Ende = other.Ende;
			this.fk_Mitarbeiter = otherImpl.fk_Mitarbeiter;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ArbeitszeitEintrag != null)
            {
                OnToString_ArbeitszeitEintrag(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ArbeitszeitEintrag> OnToString_ArbeitszeitEintrag;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ArbeitszeitEintrag != null) OnPreSave_ArbeitszeitEintrag(this);
        }
        public event ObjectEventHandler<ArbeitszeitEintrag> OnPreSave_ArbeitszeitEintrag;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ArbeitszeitEintrag != null) OnPostSave_ArbeitszeitEintrag(this);
        }
        public event ObjectEventHandler<ArbeitszeitEintrag> OnPostSave_ArbeitszeitEintrag;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Anfang":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(238).Constraints
						.Where(c => !c.IsValid(this, this.Anfang))
						.Select(c => c.GetErrorText(this, this.Anfang))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Ende":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(239).Constraints
						.Where(c => !c.IsValid(this, this.Ende))
						.Select(c => c.GetErrorText(this, this.Ende))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Mitarbeiter":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(244).Constraints
						.Where(c => !c.IsValid(this, this.Mitarbeiter))
						.Select(c => c.GetErrorText(this, this.Mitarbeiter))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_Mitarbeiter.HasValue)
				Mitarbeiter__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)Context.Find<Kistl.App.Projekte.Mitarbeiter>(_fk_Mitarbeiter.Value);
			else
				Mitarbeiter__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Anfang, binStream);
            BinarySerializer.ToStream(this._Ende, binStream);
            BinarySerializer.ToStream(this.fk_Mitarbeiter, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Anfang, binStream);
            BinarySerializer.FromStream(out this._Ende, binStream);
            {
                var tmp = this.fk_Mitarbeiter;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Mitarbeiter = tmp;
            }
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._Anfang, xml, "Anfang", "Kistl.App.Zeiterfassung");
            XmlStreamer.ToStream(this._Ende, xml, "Ende", "Kistl.App.Zeiterfassung");
            XmlStreamer.ToStream(this.fk_Mitarbeiter, xml, "Mitarbeiter", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Anfang, xml, "Anfang", "Kistl.App.Zeiterfassung");
            XmlStreamer.FromStream(ref this._Ende, xml, "Ende", "Kistl.App.Zeiterfassung");
            {
                var tmp = this.fk_Mitarbeiter;
                XmlStreamer.FromStream(ref tmp, xml, "Mitarbeiter", "http://dasz.at/Kistl");
                this.fk_Mitarbeiter = tmp;
            }
        }

#endregion

    }


}