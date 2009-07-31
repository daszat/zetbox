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
    /// An account of work efforts. May be used to limit the hours being expended.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="WorkEffortAccount")]
    [System.Diagnostics.DebuggerDisplay("WorkEffortAccount")]
    public class WorkEffortAccount__Implementation__ : BaseServerDataObject_EntityFramework, WorkEffortAccount
    {
    
		public WorkEffortAccount__Implementation__()
		{
            {
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.IdProperty
        public override int ID
        {
            get
            {
				return _ID;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
					var __oldValue = _ID;
					var __newValue = value;
                    NotifyPropertyChanging("ID", __oldValue, __newValue);
                    _ID = __newValue;
                    NotifyPropertyChanged("ID", __oldValue, __newValue);

                }
            }
        }
        private int _ID;

        /// <summary>
        /// Maximal erlaubte Stundenanzahl
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual double? BudgetHours
        {
            get
            {
				var __value = _BudgetHours;
				if(OnBudgetHours_Getter != null)
				{
					var e = new PropertyGetterEventArgs<double?>(__value);
					OnBudgetHours_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_BudgetHours != value)
                {
					var __oldValue = _BudgetHours;
					var __newValue = value;
                    if(OnBudgetHours_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<double?>(__oldValue, __newValue);
						OnBudgetHours_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("BudgetHours", __oldValue, __newValue);
                    _BudgetHours = __newValue;
                    NotifyPropertyChanged("BudgetHours", __oldValue, __newValue);

                    if(OnBudgetHours_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<double?>(__oldValue, __newValue);
						OnBudgetHours_PostSetter(this, e);
                    }
                }
            }
        }
        private double? _BudgetHours;
		public event PropertyGetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnBudgetHours_Getter;
		public event PropertyPreSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnBudgetHours_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnBudgetHours_PostSetter;
        /// <summary>
        /// Zugeordnete Mitarbeiter
        /// </summary>
    /*
    Relation: FK_WorkEffortAccount_has_Mitarbeiter
    A: ZeroOrMore WorkEffortAccount as WorkEffortAccount
    B: ZeroOrMore Mitarbeiter as Mitarbeiter
    Preferred Storage: Separate
    */
        // collection reference property
		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.CollectionEntryListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
        {
            get
            {
                if (_MitarbeiterWrapper == null)
                {
                    _MitarbeiterWrapper = new EntityRelationBSideCollectionWrapper<Kistl.App.TimeRecords.WorkEffortAccount, Kistl.App.Projekte.Mitarbeiter, Kistl.App.TimeRecords.WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__>(
                            this,
                            Mitarbeiter__Implementation__);
                }
                return _MitarbeiterWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_WorkEffortAccount_has_Mitarbeiter_WorkEffortAccount", "CollectionEntry")]
        public EntityCollection<Kistl.App.TimeRecords.WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__> Mitarbeiter__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.TimeRecords.WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__>(
                        "Model.FK_WorkEffortAccount_has_Mitarbeiter_WorkEffortAccount",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityRelationBSideCollectionWrapper<Kistl.App.TimeRecords.WorkEffortAccount, Kistl.App.Projekte.Mitarbeiter, Kistl.App.TimeRecords.WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__> _MitarbeiterWrapper;


        /// <summary>
        /// Name des TimeRecordsskontos
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string Name
        {
            get
            {
				var __value = _Name;
				if(OnName_Getter != null)
				{
					var e = new PropertyGetterEventArgs<string>(__value);
					OnName_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Name != value)
                {
					var __oldValue = _Name;
					var __newValue = value;
                    if(OnName_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
						OnName_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
                    NotifyPropertyChanged("Name", __oldValue, __newValue);

                    if(OnName_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
						OnName_PostSetter(this, e);
                    }
                }
            }
        }
        private string _Name;
		public event PropertyGetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnName_Getter;
		public event PropertyPreSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnName_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnName_PostSetter;
        /// <summary>
        /// Space for notes
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string Notes
        {
            get
            {
				var __value = _Notes;
				if(OnNotes_Getter != null)
				{
					var e = new PropertyGetterEventArgs<string>(__value);
					OnNotes_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Notes != value)
                {
					var __oldValue = _Notes;
					var __newValue = value;
                    if(OnNotes_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
						OnNotes_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("Notes", __oldValue, __newValue);
                    _Notes = __newValue;
                    NotifyPropertyChanged("Notes", __oldValue, __newValue);

                    if(OnNotes_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
						OnNotes_PostSetter(this, e);
                    }
                }
            }
        }
        private string _Notes;
		public event PropertyGetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnNotes_Getter;
		public event PropertyPreSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnNotes_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnNotes_PostSetter;
        /// <summary>
        /// Aktuell gebuchte Stunden
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual double? SpentHours
        {
            get
            {
				var __value = _SpentHours;
				if(OnSpentHours_Getter != null)
				{
					var e = new PropertyGetterEventArgs<double?>(__value);
					OnSpentHours_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_SpentHours != value)
                {
					var __oldValue = _SpentHours;
					var __newValue = value;
                    if(OnSpentHours_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<double?>(__oldValue, __newValue);
						OnSpentHours_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("SpentHours", __oldValue, __newValue);
                    _SpentHours = __newValue;
                    NotifyPropertyChanged("SpentHours", __oldValue, __newValue);

                    if(OnSpentHours_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<double?>(__oldValue, __newValue);
						OnSpentHours_PostSetter(this, e);
                    }
                }
            }
        }
        private double? _SpentHours;
		public event PropertyGetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnSpentHours_Getter;
		public event PropertyPreSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnSpentHours_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnSpentHours_PostSetter;
		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(WorkEffortAccount));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (WorkEffortAccount)obj;
			var otherImpl = (WorkEffortAccount__Implementation__)obj;
			var me = (WorkEffortAccount)this;

			me.BudgetHours = other.BudgetHours;
			me.Name = other.Name;
			me.Notes = other.Notes;
			me.SpentHours = other.SpentHours;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_WorkEffortAccount != null)
            {
                OnToString_WorkEffortAccount(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<WorkEffortAccount> OnToString_WorkEffortAccount;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_WorkEffortAccount != null) OnPreSave_WorkEffortAccount(this);
        }
        public event ObjectEventHandler<WorkEffortAccount> OnPreSave_WorkEffortAccount;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_WorkEffortAccount != null) OnPostSave_WorkEffortAccount(this);
        }
        public event ObjectEventHandler<WorkEffortAccount> OnPostSave_WorkEffortAccount;

        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_WorkEffortAccount != null) OnCreated_WorkEffortAccount(this);
        }
        public event ObjectEventHandler<WorkEffortAccount> OnCreated_WorkEffortAccount;

        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_WorkEffortAccount != null) OnDeleting_WorkEffortAccount(this);
        }
        public event ObjectEventHandler<WorkEffortAccount> OnDeleting_WorkEffortAccount;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "BudgetHours":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("2f57b6c8-d798-43de-b9c8-29675ff0c65f")).Constraints
						.Where(c => !c.IsValid(this, this.BudgetHours))
						.Select(c => c.GetErrorText(this, this.BudgetHours))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Mitarbeiter":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("21ed2b37-6e10-4aff-b4c1-554a1cc0e967")).Constraints
						.Where(c => !c.IsValid(this, this.Mitarbeiter))
						.Select(c => c.GetErrorText(this, this.Mitarbeiter))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Name":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("763b0b46-8309-4532-ba98-36575f02a1d1")).Constraints
						.Where(c => !c.IsValid(this, this.Name))
						.Select(c => c.GetErrorText(this, this.Name))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Notes":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("79c8188d-d8e2-41b7-82c9-08f384fd6b68")).Constraints
						.Where(c => !c.IsValid(this, this.Notes))
						.Select(c => c.GetErrorText(this, this.Notes))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "SpentHours":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("f7816f8a-0b07-429c-9161-47ca495a2e41")).Constraints
						.Where(c => !c.IsValid(this, this.SpentHours))
						.Select(c => c.GetErrorText(this, this.SpentHours))
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
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
			
            base.ToStream(binStream, auxObjects);
            BinarySerializer.ToStream(this._BudgetHours, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(this._Notes, binStream);
            BinarySerializer.ToStream(this._SpentHours, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._BudgetHours, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
            BinarySerializer.FromStream(out this._Notes, binStream);
            BinarySerializer.FromStream(out this._SpentHours, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._BudgetHours, xml, "BudgetHours", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._Notes, xml, "Notes", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._SpentHours, xml, "SpentHours", "Kistl.App.TimeRecords");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._BudgetHours, xml, "BudgetHours", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._Notes, xml, "Notes", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._SpentHours, xml, "SpentHours", "Kistl.App.TimeRecords");
        }

#endregion

    }


}