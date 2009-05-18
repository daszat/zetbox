// <autogenerated/>


namespace Kistl.App.TimeRecords
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
    /// A defined work effort of an employee.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="WorkEffort")]
    [System.Diagnostics.DebuggerDisplay("WorkEffort")]
    public class WorkEffort__Implementation__ : BaseServerDataObject_EntityFramework, WorkEffort
    {
    
		public WorkEffort__Implementation__()
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
        /// Point in time when the work effort started.
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual DateTime From
        {
            get
            {
                return _From;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_From != value)
                {
					var __oldValue = _From;
                    NotifyPropertyChanging("From", __oldValue, value);
                    _From = value;
                    NotifyPropertyChanged("From", __oldValue, value);
                }
            }
        }
        private DateTime _From;

        /// <summary>
        /// Which employee effected this work effort.
        /// </summary>
    /*
    Relation: FK_WorkEffort_Mitarbeiter_WorkEffort_82
    A: ZeroOrMore WorkEffort as WorkEffort
    B: One Mitarbeiter as Mitarbeiter
    Preferred Storage: MergeIntoA
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
        
        private int? _fk_Mitarbeiter;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_WorkEffort_Mitarbeiter_WorkEffort_82", "Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter__Implementation__ Mitarbeiter__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_WorkEffort_Mitarbeiter_WorkEffort_82",
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
                        "Model.FK_WorkEffort_Mitarbeiter_WorkEffort_82",
                        "Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// A short label describing this work effort.
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
					var __oldValue = _Name;
                    NotifyPropertyChanging("Name", __oldValue, value);
                    _Name = value;
                    NotifyPropertyChanged("Name", __oldValue, value);
                }
            }
        }
        private string _Name;

        /// <summary>
        /// Space for notes
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Notes != value)
                {
					var __oldValue = _Notes;
                    NotifyPropertyChanging("Notes", __oldValue, value);
                    _Notes = value;
                    NotifyPropertyChanged("Notes", __oldValue, value);
                }
            }
        }
        private string _Notes;

        /// <summary>
        /// Point in time (inclusive) when the work effort ended.
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual DateTime? Thru
        {
            get
            {
                return _Thru;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Thru != value)
                {
					var __oldValue = _Thru;
                    NotifyPropertyChanging("Thru", __oldValue, value);
                    _Thru = value;
                    NotifyPropertyChanged("Thru", __oldValue, value);
                }
            }
        }
        private DateTime? _Thru;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(WorkEffort));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (WorkEffort)obj;
			var otherImpl = (WorkEffort__Implementation__)obj;
			var me = (WorkEffort)this;

			me.From = other.From;
			me.Name = other.Name;
			me.Notes = other.Notes;
			me.Thru = other.Thru;
			this._fk_Mitarbeiter = otherImpl._fk_Mitarbeiter;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_WorkEffort != null)
            {
                OnToString_WorkEffort(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<WorkEffort> OnToString_WorkEffort;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_WorkEffort != null) OnPreSave_WorkEffort(this);
        }
        public event ObjectEventHandler<WorkEffort> OnPreSave_WorkEffort;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_WorkEffort != null) OnPostSave_WorkEffort(this);
        }
        public event ObjectEventHandler<WorkEffort> OnPostSave_WorkEffort;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "From":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(247).Constraints
						.Where(c => !c.IsValid(this, this.From))
						.Select(c => c.GetErrorText(this, this.From))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Mitarbeiter":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(249).Constraints
						.Where(c => !c.IsValid(this, this.Mitarbeiter))
						.Select(c => c.GetErrorText(this, this.Mitarbeiter))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Name":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(245).Constraints
						.Where(c => !c.IsValid(this, this.Name))
						.Select(c => c.GetErrorText(this, this.Name))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Notes":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(246).Constraints
						.Where(c => !c.IsValid(this, this.Notes))
						.Select(c => c.GetErrorText(this, this.Notes))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Thru":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(248).Constraints
						.Where(c => !c.IsValid(this, this.Thru))
						.Select(c => c.GetErrorText(this, this.Thru))
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
            BinarySerializer.ToStream(this._From, binStream);
            BinarySerializer.ToStream(Mitarbeiter != null ? Mitarbeiter.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(this._Notes, binStream);
            BinarySerializer.ToStream(this._Thru, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._From, binStream);
            BinarySerializer.FromStream(out this._fk_Mitarbeiter, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
            BinarySerializer.FromStream(out this._Notes, binStream);
            BinarySerializer.FromStream(out this._Thru, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._From, xml, "From", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._Notes, xml, "Notes", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._Thru, xml, "Thru", "Kistl.App.TimeRecords");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._From, xml, "From", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._Notes, xml, "Notes", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._Thru, xml, "Thru", "Kistl.App.TimeRecords");
        }

#endregion

    }


}