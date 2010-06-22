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

    using Kistl.DalProvider.Memory;

    /// <summary>
    /// An account of work efforts. May be used to limit the hours being expended.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("WorkEffortAccount")]
    public class WorkEffortAccount__Implementation__Memory : BaseMemoryDataObject, WorkEffortAccount
    {
        [Obsolete]
        public WorkEffortAccount__Implementation__Memory()
            : base(null)
        {
            {
            }
        }

        public WorkEffortAccount__Implementation__Memory(Func<IReadOnlyKistlContext> lazyCtx)
            : base(lazyCtx)
        {
            {
            }
        }


        /// <summary>
        /// Maximal erlaubte Stundenanzahl
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual double? BudgetHours
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _BudgetHours;
                if (OnBudgetHours_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<double?>(__result);
                    OnBudgetHours_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_BudgetHours != value)
                {
                    var __oldValue = _BudgetHours;
                    var __newValue = value;
                    if(OnBudgetHours_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<double?>(__oldValue, __newValue);
                        OnBudgetHours_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("BudgetHours", __oldValue, __newValue);
                    _BudgetHours = __newValue;
                    NotifyPropertyChanged("BudgetHours", __oldValue, __newValue);
                    if(OnBudgetHours_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<double?>(__oldValue, __newValue);
                        OnBudgetHours_PostSetter(this, __e);
                    }
                }
            }
        }
        private double? _BudgetHours;
		public static event PropertyGetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnBudgetHours_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnBudgetHours_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnBudgetHours_PostSetter;

        /// <summary>
        /// Zugeordnete Mitarbeiter
        /// </summary>
        // collection reference property
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.CollectionEntryListProperty
		public ICollection<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
		{
			get
			{
				if (_Mitarbeiter == null)
				{
					Context.FetchRelation<WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__Memory>(new Guid("7db412de-b90b-48ba-8340-1e6ac8c8fbaf"), RelationEndRole.A, this);
					_Mitarbeiter 
						= new ClientRelationBSideCollectionWrapper<Kistl.App.TimeRecords.WorkEffortAccount, Kistl.App.Projekte.Mitarbeiter, WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__Memory>(
							this, 
							new RelationshipFilterASideCollection<WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__Memory>(this.Context, this));
				}
				return _Mitarbeiter;
			}
		}

		private ClientRelationBSideCollectionWrapper<Kistl.App.TimeRecords.WorkEffortAccount, Kistl.App.Projekte.Mitarbeiter, WorkEffortAccount_has_Mitarbeiter_RelationEntry__Implementation__Memory> _Mitarbeiter;

        /// <summary>
        /// Name des TimeRecordsskontos
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
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
                    if(OnName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
                    NotifyPropertyChanged("Name", __oldValue, __newValue);
                    if(OnName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Name;
		public static event PropertyGetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnName_PostSetter;

        /// <summary>
        /// Space for notes
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Notes
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Notes;
                if (OnNotes_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnNotes_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Notes != value)
                {
                    var __oldValue = _Notes;
                    var __newValue = value;
                    if(OnNotes_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnNotes_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Notes", __oldValue, __newValue);
                    _Notes = __newValue;
                    NotifyPropertyChanged("Notes", __oldValue, __newValue);
                    if(OnNotes_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnNotes_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Notes;
		public static event PropertyGetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnNotes_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnNotes_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, string> OnNotes_PostSetter;

        /// <summary>
        /// Aktuell gebuchte Stunden
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual double? SpentHours
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _SpentHours;
                if (OnSpentHours_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<double?>(__result);
                    OnSpentHours_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_SpentHours != value)
                {
                    var __oldValue = _SpentHours;
                    var __newValue = value;
                    if(OnSpentHours_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<double?>(__oldValue, __newValue);
                        OnSpentHours_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("SpentHours", __oldValue, __newValue);
                    _SpentHours = __newValue;
                    NotifyPropertyChanged("SpentHours", __oldValue, __newValue);
                    if(OnSpentHours_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<double?>(__oldValue, __newValue);
                        OnSpentHours_PostSetter(this, __e);
                    }
                }
            }
        }
        private double? _SpentHours;
		public static event PropertyGetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnSpentHours_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnSpentHours_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.TimeRecords.WorkEffortAccount, double?> OnSpentHours_PostSetter;

        public override Type GetImplementedInterface()
        {
            return typeof(WorkEffortAccount);
        }

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (WorkEffortAccount)obj;
			var otherImpl = (WorkEffortAccount__Implementation__Memory)obj;
			var me = (WorkEffortAccount)this;

			me.BudgetHours = other.BudgetHours;
			me.Name = other.Name;
			me.Notes = other.Notes;
			me.SpentHours = other.SpentHours;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_WorkEffortAccount")]
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
        public static event ToStringHandler<WorkEffortAccount> OnToString_WorkEffortAccount;

        [EventBasedMethod("OnPreSave_WorkEffortAccount")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_WorkEffortAccount != null) OnPreSave_WorkEffortAccount(this);
        }
        public static event ObjectEventHandler<WorkEffortAccount> OnPreSave_WorkEffortAccount;

        [EventBasedMethod("OnPostSave_WorkEffortAccount")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_WorkEffortAccount != null) OnPostSave_WorkEffortAccount(this);
        }
        public static event ObjectEventHandler<WorkEffortAccount> OnPostSave_WorkEffortAccount;

        [EventBasedMethod("OnCreated_WorkEffortAccount")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_WorkEffortAccount != null) OnCreated_WorkEffortAccount(this);
        }
        public static event ObjectEventHandler<WorkEffortAccount> OnCreated_WorkEffortAccount;

        [EventBasedMethod("OnDeleting_WorkEffortAccount")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_WorkEffortAccount != null) OnDeleting_WorkEffortAccount(this);
        }
        public static event ObjectEventHandler<WorkEffortAccount> OnDeleting_WorkEffortAccount;


		private static readonly object _propertiesLock = new object();
		private static System.ComponentModel.PropertyDescriptor[] _properties;
		
		private void _InitializePropertyDescriptors(Func<IReadOnlyKistlContext> lazyCtx)
		{
			if (_properties != null) return;
			lock (_propertiesLock)
			{
				// recheck for a lost race after aquiring the lock
				if (_properties != null) return;
				
				_properties = new System.ComponentModel.PropertyDescriptor[] {
					// else
					new CustomPropertyDescriptor<WorkEffortAccount__Implementation__Memory, double?>(
						lazyCtx,
						new Guid("2f57b6c8-d798-43de-b9c8-29675ff0c65f"),
						"BudgetHours",
						null,
						obj => obj.BudgetHours,
						(obj, val) => obj.BudgetHours = val),
					// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
					new CustomPropertyDescriptor<WorkEffortAccount__Implementation__Memory, ICollection<Kistl.App.Projekte.Mitarbeiter>>(
						lazyCtx,
						new Guid("21ed2b37-6e10-4aff-b4c1-554a1cc0e967"),
						"Mitarbeiter",
						null,
						obj => obj.Mitarbeiter,
						null), // lists are read-only properties
					// else
					new CustomPropertyDescriptor<WorkEffortAccount__Implementation__Memory, string>(
						lazyCtx,
						new Guid("763b0b46-8309-4532-ba98-36575f02a1d1"),
						"Name",
						null,
						obj => obj.Name,
						(obj, val) => obj.Name = val),
					// else
					new CustomPropertyDescriptor<WorkEffortAccount__Implementation__Memory, string>(
						lazyCtx,
						new Guid("79c8188d-d8e2-41b7-82c9-08f384fd6b68"),
						"Notes",
						null,
						obj => obj.Notes,
						(obj, val) => obj.Notes = val),
					// else
					new CustomPropertyDescriptor<WorkEffortAccount__Implementation__Memory, double?>(
						lazyCtx,
						new Guid("f7816f8a-0b07-429c-9161-47ca495a2e41"),
						"SpentHours",
						null,
						obj => obj.SpentHours,
						(obj, val) => obj.SpentHours = val),
				};
			}
		}
		
		protected override void CollectProperties(Func<IReadOnlyKistlContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
		{
			base.CollectProperties(props);
			_InitializePropertyDescriptors(lazyCtx);
			props.AddRange(_properties);
		}
	


#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            
            base.ToStream(binStream, auxObjects, eagerLoadLists);
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