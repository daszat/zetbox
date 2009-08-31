// <autogenerated/>


namespace Kistl.App.Projekte
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
    [EdmEntityType(NamespaceName="Model", Name="Task")]
    [System.Diagnostics.DebuggerDisplay("Task")]
    public class Task__Implementation__ : BaseServerDataObject_EntityFramework, Task
    {
    
		public Task__Implementation__()
		{
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.IdProperty
        public override int ID
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ID;
                return __result;
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
        /// Aufwand in Stunden
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual double? Aufwand
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Aufwand;
                if (OnAufwand_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<double?>(__result);
                    OnAufwand_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Aufwand != value)
                {
                    var __oldValue = _Aufwand;
                    var __newValue = value;
                    if(OnAufwand_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<double?>(__oldValue, __newValue);
                        OnAufwand_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Aufwand", __oldValue, __newValue);
                    _Aufwand = __newValue;
                    NotifyPropertyChanged("Aufwand", __oldValue, __newValue);
                    if(OnAufwand_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<double?>(__oldValue, __newValue);
                        OnAufwand_PostSetter(this, __e);
                    }
                }
            }
        }
        private double? _Aufwand;
		public event PropertyGetterHandler<Kistl.App.Projekte.Task, double?> OnAufwand_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Projekte.Task, double?> OnAufwand_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Projekte.Task, double?> OnAufwand_PostSetter;
        /// <summary>
        /// Enddatum
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual DateTime? DatumBis
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DatumBis;
                if (OnDatumBis_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime?>(__result);
                    OnDatumBis_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DatumBis != value)
                {
                    var __oldValue = _DatumBis;
                    var __newValue = value;
                    if(OnDatumBis_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnDatumBis_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DatumBis", __oldValue, __newValue);
                    _DatumBis = __newValue;
                    NotifyPropertyChanged("DatumBis", __oldValue, __newValue);
                    if(OnDatumBis_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnDatumBis_PostSetter(this, __e);
                    }
                }
            }
        }
        private DateTime? _DatumBis;
		public event PropertyGetterHandler<Kistl.App.Projekte.Task, DateTime?> OnDatumBis_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Projekte.Task, DateTime?> OnDatumBis_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Projekte.Task, DateTime?> OnDatumBis_PostSetter;
        /// <summary>
        /// Start Datum
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual DateTime? DatumVon
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DatumVon;
                if (OnDatumVon_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime?>(__result);
                    OnDatumVon_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DatumVon != value)
                {
                    var __oldValue = _DatumVon;
                    var __newValue = value;
                    if(OnDatumVon_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnDatumVon_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DatumVon", __oldValue, __newValue);
                    _DatumVon = __newValue;
                    NotifyPropertyChanged("DatumVon", __oldValue, __newValue);
                    if(OnDatumVon_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnDatumVon_PostSetter(this, __e);
                    }
                }
            }
        }
        private DateTime? _DatumVon;
		public event PropertyGetterHandler<Kistl.App.Projekte.Task, DateTime?> OnDatumVon_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Projekte.Task, DateTime?> OnDatumVon_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Projekte.Task, DateTime?> OnDatumVon_PostSetter;
        /// <summary>
        /// Taskname
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Name
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Name;
                if (OnName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
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
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
                    NotifyPropertyChanged("Name", __oldValue, __newValue);
                    if(OnName_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Name;
		public event PropertyGetterHandler<Kistl.App.Projekte.Task, string> OnName_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Projekte.Task, string> OnName_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Projekte.Task, string> OnName_PostSetter;
        /// <summary>
        /// Verknüpfung zum Projekt
        /// </summary>
    /*
    Relation: FK_Projekt_has_Task
    A: ZeroOrOne Projekt as Projekt
    B: ZeroOrMore Task as Tasks
    Preferred Storage: MergeIntoB
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
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
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                Projekt__Implementation__ = (Kistl.App.Projekte.Projekt__Implementation__)value;
            }
        }
        
        private int? _fk_Projekt;
        private Guid? _fk_guid_Projekt = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Projekt_has_Task", "Projekt")]
        public Kistl.App.Projekte.Projekt__Implementation__ Projekt__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Projekt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt__Implementation__>(
                        "Model.FK_Projekt_has_Task",
                        "Projekt");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnProjekt_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Projekte.Projekt>(__value);
					OnProjekt_Getter(this, e);
					__value = (Kistl.App.Projekte.Projekt__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Projekt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt__Implementation__>(
                        "Model.FK_Projekt_has_Task",
                        "Projekt");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Projekte.Projekt __oldValue = (Kistl.App.Projekte.Projekt)r.Value;
                Kistl.App.Projekte.Projekt __newValue = (Kistl.App.Projekte.Projekt)value;

                if(OnProjekt_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Projekte.Projekt>(__oldValue, __newValue);
					OnProjekt_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Projekte.Projekt__Implementation__)__newValue;
                if(OnProjekt_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Projekte.Projekt>(__oldValue, __newValue);
					OnProjekt_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.Projekte.Task, Kistl.App.Projekte.Projekt> OnProjekt_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Projekte.Task, Kistl.App.Projekte.Projekt> OnProjekt_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Projekte.Task, Kistl.App.Projekte.Projekt> OnProjekt_PostSetter;
		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Task));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Task)obj;
			var otherImpl = (Task__Implementation__)obj;
			var me = (Task)this;

			me.Aufwand = other.Aufwand;
			me.DatumBis = other.DatumBis;
			me.DatumVon = other.DatumVon;
			me.Name = other.Name;
			this._fk_Projekt = otherImpl._fk_Projekt;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Task")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Task != null)
            {
                OnToString_Task(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Task> OnToString_Task;

        [EventBasedMethod("OnPreSave_Task")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Task != null) OnPreSave_Task(this);
        }
        public event ObjectEventHandler<Task> OnPreSave_Task;

        [EventBasedMethod("OnPostSave_Task")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Task != null) OnPostSave_Task(this);
        }
        public event ObjectEventHandler<Task> OnPostSave_Task;

        [EventBasedMethod("OnCreated_Task")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Task != null) OnCreated_Task(this);
        }
        public event ObjectEventHandler<Task> OnCreated_Task;

        [EventBasedMethod("OnDeleting_Task")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Task != null) OnDeleting_Task(this);
        }
        public event ObjectEventHandler<Task> OnDeleting_Task;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Aufwand":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("a28f7536-9b8a-49ca-bc97-d28e1c2c4d3e")).Constraints
						.Where(c => !c.IsValid(this, this.Aufwand))
						.Select(c => c.GetErrorText(this, this.Aufwand))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DatumBis":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("2b705496-388a-43a8-82e8-b17b652a55fc")).Constraints
						.Where(c => !c.IsValid(this, this.DatumBis))
						.Select(c => c.GetErrorText(this, this.DatumBis))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DatumVon":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("1485a7b7-c4d5-456a-a18a-0c409c3eca8e")).Constraints
						.Where(c => !c.IsValid(this, this.DatumVon))
						.Select(c => c.GetErrorText(this, this.DatumVon))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Name":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("91595e02-411c-40f2-ab83-4cced76e954d")).Constraints
						.Where(c => !c.IsValid(this, this.Name))
						.Select(c => c.GetErrorText(this, this.Name))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Projekt":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("5545ba8a-3e89-4b22-bd66-c12f3622ace0")).Constraints
						.Where(c => !c.IsValid(this, this.Projekt))
						.Select(c => c.GetErrorText(this, this.Projekt))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			// fix direct object references

			if (_fk_guid_Projekt.HasValue)
				Projekt__Implementation__ = (Kistl.App.Projekte.Projekt__Implementation__)Context.FindPersistenceObject<Kistl.App.Projekte.Projekt>(_fk_guid_Projekt.Value);
			else if (_fk_Projekt.HasValue)
				Projekt__Implementation__ = (Kistl.App.Projekte.Projekt__Implementation__)Context.Find<Kistl.App.Projekte.Projekt>(_fk_Projekt.Value);
			else
				Projekt__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
			
            base.ToStream(binStream, auxObjects);
            BinarySerializer.ToStream(this._Aufwand, binStream);
            BinarySerializer.ToStream(this._DatumBis, binStream);
            BinarySerializer.ToStream(this._DatumVon, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(Projekt != null ? Projekt.ID : (int?)null, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            {
                var tmp = this._Aufwand;
                BinarySerializer.FromStream(out tmp, binStream);
                this._Aufwand = tmp;
            }
            {
                var tmp = this._DatumBis;
                BinarySerializer.FromStream(out tmp, binStream);
                this._DatumBis = tmp;
            }
            {
                var tmp = this._DatumVon;
                BinarySerializer.FromStream(out tmp, binStream);
                this._DatumVon = tmp;
            }
            {
                var tmp = this._Name;
                BinarySerializer.FromStream(out tmp, binStream);
                this._Name = tmp;
            }
            BinarySerializer.FromStream(out this._fk_Projekt, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._Aufwand, xml, "Aufwand", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._DatumBis, xml, "DatumBis", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._DatumVon, xml, "DatumVon", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Projekte");
            XmlStreamer.ToStream(Projekt != null ? Projekt.ID : (int?)null, xml, "Projekt", "Kistl.App.Projekte");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            {
                var tmp = this._Aufwand;
                XmlStreamer.FromStream(ref tmp, xml, "Aufwand", "Kistl.App.Projekte");
                this._Aufwand = tmp;
            }
            {
                var tmp = this._DatumBis;
                XmlStreamer.FromStream(ref tmp, xml, "DatumBis", "Kistl.App.Projekte");
                this._DatumBis = tmp;
            }
            {
                var tmp = this._DatumVon;
                XmlStreamer.FromStream(ref tmp, xml, "DatumVon", "Kistl.App.Projekte");
                this._DatumVon = tmp;
            }
            {
                var tmp = this._Name;
                XmlStreamer.FromStream(ref tmp, xml, "Name", "Kistl.App.Projekte");
                this._Name = tmp;
            }
            XmlStreamer.FromStream(ref this._fk_Projekt, xml, "Projekt", "Kistl.App.Projekte");
        }

#endregion

    }


}