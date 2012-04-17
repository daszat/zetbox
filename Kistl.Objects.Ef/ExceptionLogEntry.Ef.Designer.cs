// <autogenerated/>

namespace Kistl.App.Base
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
    using Kistl.DalProvider.Base.RelationWrappers;

    using Kistl.API.Server;
    using Kistl.DalProvider.Ef;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Logentry for exceptions
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="ExceptionLogEntry")]
    [System.Diagnostics.DebuggerDisplay("ExceptionLogEntry")]
    public class ExceptionLogEntryEfImpl : BaseServerDataObject_EntityFramework, ExceptionLogEntry
    {
        private static readonly Guid _objectClassID = new Guid("72d9934d-aee9-4512-ad1e-1a30af1d353e");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public ExceptionLogEntryEfImpl()
            : base(null)
        {
        }

        public ExceptionLogEntryEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public DateTime Date
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(DateTime);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Date;
                if (OnDate_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime>(__result);
                    OnDate_Getter(this, __e);
                    __result = _Date = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Date != value)
                {
                    var __oldValue = _Date;
                    var __newValue = value;
                    if (OnDate_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime>(__oldValue, __newValue);
                        OnDate_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Date", __oldValue, __newValue);
                    _Date = __newValue;
                    NotifyPropertyChanged("Date", __oldValue, __newValue);

                    if (OnDate_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime>(__oldValue, __newValue);
                        OnDate_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Date");
				}
            }
        }
        private DateTime _Date;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ExceptionLogEntry, DateTime> OnDate_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ExceptionLogEntry, DateTime> OnDate_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ExceptionLogEntry, DateTime> OnDate_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.ExceptionLogEntry> OnDate_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Exception
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Exception;
                if (OnException_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnException_Getter(this, __e);
                    __result = _Exception = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Exception != value)
                {
                    var __oldValue = _Exception;
                    var __newValue = value;
                    if (OnException_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnException_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Exception", __oldValue, __newValue);
                    _Exception = __newValue;
                    NotifyPropertyChanged("Exception", __oldValue, __newValue);

                    if (OnException_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnException_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Exception");
				}
            }
        }
        private string _Exception;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnException_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnException_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnException_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.ExceptionLogEntry> OnException_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Level
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Level;
                if (OnLevel_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnLevel_Getter(this, __e);
                    __result = _Level = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Level != value)
                {
                    var __oldValue = _Level;
                    var __newValue = value;
                    if (OnLevel_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnLevel_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Level", __oldValue, __newValue);
                    _Level = __newValue;
                    NotifyPropertyChanged("Level", __oldValue, __newValue);

                    if (OnLevel_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnLevel_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Level");
				}
            }
        }
        private string _Level;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnLevel_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnLevel_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnLevel_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.ExceptionLogEntry> OnLevel_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Logger
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Logger;
                if (OnLogger_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnLogger_Getter(this, __e);
                    __result = _Logger = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Logger != value)
                {
                    var __oldValue = _Logger;
                    var __newValue = value;
                    if (OnLogger_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnLogger_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Logger", __oldValue, __newValue);
                    _Logger = __newValue;
                    NotifyPropertyChanged("Logger", __oldValue, __newValue);

                    if (OnLogger_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnLogger_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Logger");
				}
            }
        }
        private string _Logger;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnLogger_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnLogger_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnLogger_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.ExceptionLogEntry> OnLogger_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Message
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Message;
                if (OnMessage_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnMessage_Getter(this, __e);
                    __result = _Message = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Message != value)
                {
                    var __oldValue = _Message;
                    var __newValue = value;
                    if (OnMessage_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnMessage_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Message", __oldValue, __newValue);
                    _Message = __newValue;
                    NotifyPropertyChanged("Message", __oldValue, __newValue);

                    if (OnMessage_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnMessage_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Message");
				}
            }
        }
        private string _Message;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnMessage_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnMessage_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnMessage_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.ExceptionLogEntry> OnMessage_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Thread
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Thread;
                if (OnThread_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnThread_Getter(this, __e);
                    __result = _Thread = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Thread != value)
                {
                    var __oldValue = _Thread;
                    var __newValue = value;
                    if (OnThread_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnThread_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Thread", __oldValue, __newValue);
                    _Thread = __newValue;
                    NotifyPropertyChanged("Thread", __oldValue, __newValue);

                    if (OnThread_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnThread_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Thread");
				}
            }
        }
        private string _Thread;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnThread_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnThread_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ExceptionLogEntry, string> OnThread_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.ExceptionLogEntry> OnThread_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(ExceptionLogEntry);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (ExceptionLogEntry)obj;
            var otherImpl = (ExceptionLogEntryEfImpl)obj;
            var me = (ExceptionLogEntry)this;

            me.Date = other.Date;
            me.Exception = other.Exception;
            me.Level = other.Level;
            me.Logger = other.Logger;
            me.Message = other.Message;
            me.Thread = other.Thread;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        public override void SetNew()
        {
            base.SetNew();
        }
        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Date":
                case "Exception":
                case "Level":
                case "Logger":
                case "Message":
                case "Thread":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        #endregion // Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
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
                    new PropertyDescriptorEfImpl<ExceptionLogEntry, DateTime>(
                        lazyCtx,
                        new Guid("e093679e-1939-4a7d-97db-e48b5103062a"),
                        "Date",
                        null,
                        obj => obj.Date,
                        (obj, val) => obj.Date = val,
						obj => OnDate_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<ExceptionLogEntry, string>(
                        lazyCtx,
                        new Guid("203915b1-5bbf-49ff-864b-b66098b39481"),
                        "Exception",
                        null,
                        obj => obj.Exception,
                        (obj, val) => obj.Exception = val,
						obj => OnException_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<ExceptionLogEntry, string>(
                        lazyCtx,
                        new Guid("f8becad3-4bb8-47b2-b30c-1c7fc94eac12"),
                        "Level",
                        null,
                        obj => obj.Level,
                        (obj, val) => obj.Level = val,
						obj => OnLevel_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<ExceptionLogEntry, string>(
                        lazyCtx,
                        new Guid("fdf063e0-66f9-421d-9e60-e6aa9fa73d8e"),
                        "Logger",
                        null,
                        obj => obj.Logger,
                        (obj, val) => obj.Logger = val,
						obj => OnLogger_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<ExceptionLogEntry, string>(
                        lazyCtx,
                        new Guid("d80639ab-d40f-41db-a51f-c1a71440654e"),
                        "Message",
                        null,
                        obj => obj.Message,
                        (obj, val) => obj.Message = val,
						obj => OnMessage_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<ExceptionLogEntry, string>(
                        lazyCtx,
                        new Guid("24ab22e0-7dd3-4535-9abd-a94102f9ca80"),
                        "Thread",
                        null,
                        obj => obj.Thread,
                        (obj, val) => obj.Thread = val,
						obj => OnThread_IsValid), 
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
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_ExceptionLogEntry")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ExceptionLogEntry != null)
            {
                OnToString_ExceptionLogEntry(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ExceptionLogEntry> OnToString_ExceptionLogEntry;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_ExceptionLogEntry")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_ExceptionLogEntry != null)
            {
                OnObjectIsValid_ExceptionLogEntry(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<ExceptionLogEntry> OnObjectIsValid_ExceptionLogEntry;

        [EventBasedMethod("OnNotifyPreSave_ExceptionLogEntry")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_ExceptionLogEntry != null) OnNotifyPreSave_ExceptionLogEntry(this);
        }
        public static event ObjectEventHandler<ExceptionLogEntry> OnNotifyPreSave_ExceptionLogEntry;

        [EventBasedMethod("OnNotifyPostSave_ExceptionLogEntry")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_ExceptionLogEntry != null) OnNotifyPostSave_ExceptionLogEntry(this);
        }
        public static event ObjectEventHandler<ExceptionLogEntry> OnNotifyPostSave_ExceptionLogEntry;

        [EventBasedMethod("OnNotifyCreated_ExceptionLogEntry")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Date");
            SetNotInitializedProperty("Exception");
            SetNotInitializedProperty("Level");
            SetNotInitializedProperty("Logger");
            SetNotInitializedProperty("Message");
            SetNotInitializedProperty("Thread");
            base.NotifyCreated();
            if (OnNotifyCreated_ExceptionLogEntry != null) OnNotifyCreated_ExceptionLogEntry(this);
        }
        public static event ObjectEventHandler<ExceptionLogEntry> OnNotifyCreated_ExceptionLogEntry;

        [EventBasedMethod("OnNotifyDeleting_ExceptionLogEntry")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_ExceptionLogEntry != null) OnNotifyDeleting_ExceptionLogEntry(this);
        }
        public static event ObjectEventHandler<ExceptionLogEntry> OnNotifyDeleting_ExceptionLogEntry;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
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
				else 
				{
					SetInitializedProperty("ID");
				}
            }
        }
        private int _ID;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this._Date, binStream);
            BinarySerializer.ToStream(this._Exception, binStream);
            BinarySerializer.ToStream(this._Level, binStream);
            BinarySerializer.ToStream(this._Logger, binStream);
            BinarySerializer.ToStream(this._Message, binStream);
            BinarySerializer.ToStream(this._Thread, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._Date, binStream);
            BinarySerializer.FromStream(out this._Exception, binStream);
            BinarySerializer.FromStream(out this._Level, binStream);
            BinarySerializer.FromStream(out this._Logger, binStream);
            BinarySerializer.FromStream(out this._Message, binStream);
            BinarySerializer.FromStream(out this._Thread, binStream);
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            base.ToStream(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.ToStream(this._Date, xml, "Date", "Kistl.App.Base");
            XmlStreamer.ToStream(this._Exception, xml, "Exception", "Kistl.App.Base");
            XmlStreamer.ToStream(this._Level, xml, "Level", "Kistl.App.Base");
            XmlStreamer.ToStream(this._Logger, xml, "Logger", "Kistl.App.Base");
            XmlStreamer.ToStream(this._Message, xml, "Message", "Kistl.App.Base");
            XmlStreamer.ToStream(this._Thread, xml, "Thread", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._Date, xml, "Date", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Exception, xml, "Exception", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Level, xml, "Level", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Logger, xml, "Logger", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Message, xml, "Message", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Thread, xml, "Thread", "Kistl.App.Base");
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}