// <autogenerated/>

namespace Zetbox.App.SchemaMigration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Zetbox.API;
    using Zetbox.DalProvider.Base.RelationWrappers;

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// A log of all migration operations
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MigrationLog")]
    public class MigrationLogMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, MigrationLog
    {
        private static readonly Guid _objectClassID = new Guid("49745ac0-db34-41ee-875f-0f09f1432ab0");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public MigrationLogMemoryImpl()
            : base(null)
        {
        }

        public MigrationLogMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// The destination of this migration step
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string Destination
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Destination;
                if (OnDestination_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDestination_Getter(this, __e);
                    __result = _Destination = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Destination != value)
                {
                    var __oldValue = _Destination;
                    var __newValue = value;
                    if (OnDestination_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDestination_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Destination", __oldValue, __newValue);
                    _Destination = __newValue;
                    NotifyPropertyChanged("Destination", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnDestination_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDestination_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Destination");
                }
            }
        }
        private string _Destination;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.SchemaMigration.MigrationLog, string> OnDestination_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.SchemaMigration.MigrationLog, string> OnDestination_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.SchemaMigration.MigrationLog, string> OnDestination_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.SchemaMigration.MigrationLog> OnDestination_IsValid;

        /// <summary>
        /// The number of rows in the destination
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public int DestinationRows
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DestinationRows;
                if (OnDestinationRows_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int>(__result);
                    OnDestinationRows_Getter(this, __e);
                    __result = _DestinationRows = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DestinationRows != value)
                {
                    var __oldValue = _DestinationRows;
                    var __newValue = value;
                    if (OnDestinationRows_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int>(__oldValue, __newValue);
                        OnDestinationRows_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DestinationRows", __oldValue, __newValue);
                    _DestinationRows = __newValue;
                    NotifyPropertyChanged("DestinationRows", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnDestinationRows_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int>(__oldValue, __newValue);
                        OnDestinationRows_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("DestinationRows");
                }
            }
        }
        private int _DestinationRows;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.SchemaMigration.MigrationLog, int> OnDestinationRows_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.SchemaMigration.MigrationLog, int> OnDestinationRows_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.SchemaMigration.MigrationLog, int> OnDestinationRows_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.SchemaMigration.MigrationLog> OnDestinationRows_IsValid;

        /// <summary>
        /// The source of the migration step
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string Source
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Source;
                if (OnSource_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnSource_Getter(this, __e);
                    __result = _Source = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Source != value)
                {
                    var __oldValue = _Source;
                    var __newValue = value;
                    if (OnSource_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnSource_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Source", __oldValue, __newValue);
                    _Source = __newValue;
                    NotifyPropertyChanged("Source", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnSource_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnSource_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Source");
                }
            }
        }
        private string _Source;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.SchemaMigration.MigrationLog, string> OnSource_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.SchemaMigration.MigrationLog, string> OnSource_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.SchemaMigration.MigrationLog, string> OnSource_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.SchemaMigration.MigrationLog> OnSource_IsValid;

        /// <summary>
        /// The number of rows in this source
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public int SourceRows
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _SourceRows;
                if (OnSourceRows_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int>(__result);
                    OnSourceRows_Getter(this, __e);
                    __result = _SourceRows = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_SourceRows != value)
                {
                    var __oldValue = _SourceRows;
                    var __newValue = value;
                    if (OnSourceRows_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int>(__oldValue, __newValue);
                        OnSourceRows_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("SourceRows", __oldValue, __newValue);
                    _SourceRows = __newValue;
                    NotifyPropertyChanged("SourceRows", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnSourceRows_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int>(__oldValue, __newValue);
                        OnSourceRows_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("SourceRows");
                }
            }
        }
        private int _SourceRows;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.SchemaMigration.MigrationLog, int> OnSourceRows_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.SchemaMigration.MigrationLog, int> OnSourceRows_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.SchemaMigration.MigrationLog, int> OnSourceRows_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.SchemaMigration.MigrationLog> OnSourceRows_IsValid;

        /// <summary>
        /// When the logentry was written
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public DateTime Timestamp
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Timestamp;
                if (OnTimestamp_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime>(__result);
                    OnTimestamp_Getter(this, __e);
                    __result = _Timestamp = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Timestamp != value)
                {
                    var __oldValue = _Timestamp;
                    var __newValue = value;
                    if (__newValue.Kind == DateTimeKind.Unspecified)
                        __newValue = DateTime.SpecifyKind(__newValue, DateTimeKind.Local);
                    if (OnTimestamp_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime>(__oldValue, __newValue);
                        OnTimestamp_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Timestamp", __oldValue, __newValue);
                    _Timestamp = __newValue;
                    NotifyPropertyChanged("Timestamp", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnTimestamp_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime>(__oldValue, __newValue);
                        OnTimestamp_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Timestamp");
                }
            }
        }
        private DateTime _Timestamp;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.SchemaMigration.MigrationLog, DateTime> OnTimestamp_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.SchemaMigration.MigrationLog, DateTime> OnTimestamp_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.SchemaMigration.MigrationLog, DateTime> OnTimestamp_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.SchemaMigration.MigrationLog> OnTimestamp_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(MigrationLog);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (MigrationLog)obj;
            var otherImpl = (MigrationLogMemoryImpl)obj;
            var me = (MigrationLog)this;

            me.Destination = other.Destination;
            me.DestinationRows = other.DestinationRows;
            me.Source = other.Source;
            me.SourceRows = other.SourceRows;
            me.Timestamp = other.Timestamp;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Destination":
                case "DestinationRows":
                case "Source":
                case "SourceRows":
                case "Timestamp":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            default:
                return base.TriggerFetch(propName);
            }
        }

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
            // fix cached lists references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        private static readonly object _propertiesLock = new object();
        private static System.ComponentModel.PropertyDescriptor[] _properties;

        private void _InitializePropertyDescriptors(Func<IFrozenContext> lazyCtx)
        {
            if (_properties != null) return;
            lock (_propertiesLock)
            {
                // recheck for a lost race after aquiring the lock
                if (_properties != null) return;

                _properties = new System.ComponentModel.PropertyDescriptor[] {
                    // else
                    new PropertyDescriptorMemoryImpl<MigrationLog, string>(
                        lazyCtx,
                        new Guid("bad56a8d-e23e-47e3-8545-ee4c2689aab8"),
                        "Destination",
                        null,
                        obj => obj.Destination,
                        (obj, val) => obj.Destination = val,
						obj => OnDestination_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<MigrationLog, int>(
                        lazyCtx,
                        new Guid("6b2ac709-a61b-4d22-96e6-e36f4bcbce84"),
                        "DestinationRows",
                        null,
                        obj => obj.DestinationRows,
                        (obj, val) => obj.DestinationRows = val,
						obj => OnDestinationRows_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<MigrationLog, string>(
                        lazyCtx,
                        new Guid("fd0e1581-c4f0-476e-af24-2905e4aa377d"),
                        "Source",
                        null,
                        obj => obj.Source,
                        (obj, val) => obj.Source = val,
						obj => OnSource_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<MigrationLog, int>(
                        lazyCtx,
                        new Guid("52a3ddb3-c724-4427-a2e1-95bbf347906a"),
                        "SourceRows",
                        null,
                        obj => obj.SourceRows,
                        (obj, val) => obj.SourceRows = val,
						obj => OnSourceRows_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<MigrationLog, DateTime>(
                        lazyCtx,
                        new Guid("9e3a70c6-04f9-4464-93c9-68c1eec6b94f"),
                        "Timestamp",
                        null,
                        obj => obj.Timestamp,
                        (obj, val) => obj.Timestamp = val,
						obj => OnTimestamp_IsValid), 
                    // position columns
                };
            }
        }

        protected override void CollectProperties(Func<IFrozenContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
        {
            base.CollectProperties(lazyCtx, props);
            _InitializePropertyDescriptors(lazyCtx);
            props.AddRange(_properties);
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_MigrationLog")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_MigrationLog != null)
            {
                OnToString_MigrationLog(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<MigrationLog> OnToString_MigrationLog;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_MigrationLog")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_MigrationLog != null)
            {
                OnObjectIsValid_MigrationLog(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<MigrationLog> OnObjectIsValid_MigrationLog;

        [EventBasedMethod("OnNotifyPreSave_MigrationLog")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_MigrationLog != null) OnNotifyPreSave_MigrationLog(this);
        }
        public static event ObjectEventHandler<MigrationLog> OnNotifyPreSave_MigrationLog;

        [EventBasedMethod("OnNotifyPostSave_MigrationLog")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_MigrationLog != null) OnNotifyPostSave_MigrationLog(this);
        }
        public static event ObjectEventHandler<MigrationLog> OnNotifyPostSave_MigrationLog;

        [EventBasedMethod("OnNotifyCreated_MigrationLog")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Destination");
            SetNotInitializedProperty("DestinationRows");
            SetNotInitializedProperty("Source");
            SetNotInitializedProperty("SourceRows");
            SetNotInitializedProperty("Timestamp");
            base.NotifyCreated();
            if (OnNotifyCreated_MigrationLog != null) OnNotifyCreated_MigrationLog(this);
        }
        public static event ObjectEventHandler<MigrationLog> OnNotifyCreated_MigrationLog;

        [EventBasedMethod("OnNotifyDeleting_MigrationLog")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_MigrationLog != null) OnNotifyDeleting_MigrationLog(this);
        }
        public static event ObjectEventHandler<MigrationLog> OnNotifyDeleting_MigrationLog;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Destination);
            binStream.Write(this._DestinationRows);
            binStream.Write(this._Source);
            binStream.Write(this._SourceRows);
            binStream.Write(this._Timestamp);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._Destination = binStream.ReadString();
            this._DestinationRows = binStream.ReadInt32();
            this._Source = binStream.ReadString();
            this._SourceRows = binStream.ReadInt32();
            this._Timestamp = binStream.ReadDateTime();
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}