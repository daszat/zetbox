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

    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.Memory;

    /// <summary>
    /// Represents an Identity
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Identity")]
    public class IdentityMemoryImpl : Kistl.DalProvider.Memory.DataObjectMemoryImpl, Identity
    {
        private static readonly Guid _objectClassID = new Guid("31d8890a-67fc-4a78-9d35-9ff0b9e09b4c");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public IdentityMemoryImpl()
            : base(null)
        {
        }

        public IdentityMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Displayname of this identity
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public string DisplayName
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DisplayName;
                if (OnDisplayName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDisplayName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayName != value)
                {
                    var __oldValue = _DisplayName;
                    var __newValue = value;
                    if (OnDisplayName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DisplayName", __oldValue, __newValue);
                    _DisplayName = __newValue;
                    NotifyPropertyChanged("DisplayName", __oldValue, __newValue);

                    if (OnDisplayName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayName_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("DisplayName");
				}
            }
        }
        private string _DisplayName;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.Identity, string> OnDisplayName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Identity, string> OnDisplayName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Identity, string> OnDisplayName_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.Identity> OnDisplayName_IsValid;

        /// <summary>
        /// Identites are member of groups
        /// </summary>
        // collection entry list property
   		// Kistl.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Kistl.App.Base.Group> Groups
		{
			get
			{
				if (_Groups == null)
				{
					Context.FetchRelation<Kistl.App.Base.Identity_memberOf_Group_RelationEntryMemoryImpl>(new Guid("3efb7ae8-ba6b-40e3-9482-b45d1c101743"), RelationEndRole.A, this);
					_Groups 
						= new ObservableBSideCollectionWrapper<Kistl.App.Base.Identity, Kistl.App.Base.Group, Kistl.App.Base.Identity_memberOf_Group_RelationEntryMemoryImpl, ICollection<Kistl.App.Base.Identity_memberOf_Group_RelationEntryMemoryImpl>>(
							this, 
							new RelationshipFilterASideCollection<Kistl.App.Base.Identity_memberOf_Group_RelationEntryMemoryImpl>(this.Context, this));
				}
				return (ICollection<Kistl.App.Base.Group>)_Groups;
			}
		}

		private ObservableBSideCollectionWrapper<Kistl.App.Base.Identity, Kistl.App.Base.Group, Kistl.App.Base.Identity_memberOf_Group_RelationEntryMemoryImpl, ICollection<Kistl.App.Base.Identity_memberOf_Group_RelationEntryMemoryImpl>> _Groups;

        public static event PropertyIsValidHandler<Kistl.App.Base.Identity> OnGroups_IsValid;

        /// <summary>
        /// Password of a generic identity
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public string Password
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Password;
                if (OnPassword_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnPassword_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Password != value)
                {
                    var __oldValue = _Password;
                    var __newValue = value;
                    if (OnPassword_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnPassword_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Password", __oldValue, __newValue);
                    _Password = __newValue;
                    NotifyPropertyChanged("Password", __oldValue, __newValue);

                    if (OnPassword_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnPassword_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Password");
				}
            }
        }
        private string _Password;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.Identity, string> OnPassword_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Identity, string> OnPassword_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Identity, string> OnPassword_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.Identity> OnPassword_IsValid;

        /// <summary>
        /// Username of a generic identity
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public string UserName
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _UserName;
                if (OnUserName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnUserName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_UserName != value)
                {
                    var __oldValue = _UserName;
                    var __newValue = value;
                    if (OnUserName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnUserName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("UserName", __oldValue, __newValue);
                    _UserName = __newValue;
                    NotifyPropertyChanged("UserName", __oldValue, __newValue);

                    if (OnUserName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnUserName_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("UserName");
				}
            }
        }
        private string _UserName;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.Identity, string> OnUserName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Identity, string> OnUserName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Identity, string> OnUserName_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.Identity> OnUserName_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(Identity);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Identity)obj;
            var otherImpl = (IdentityMemoryImpl)obj;
            var me = (Identity)this;

            me.DisplayName = other.DisplayName;
            me.Password = other.Password;
            me.UserName = other.UserName;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "DisplayName":
                case "Password":
                case "UserName":
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
                    new PropertyDescriptorMemoryImpl<Identity, string>(
                        lazyCtx,
                        new Guid("f93e6dbb-a704-460c-8183-ce8b1c2c47a2"),
                        "DisplayName",
                        null,
                        obj => obj.DisplayName,
                        (obj, val) => obj.DisplayName = val,
						obj => OnDisplayName_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<Identity, ICollection<Kistl.App.Base.Group>>(
                        lazyCtx,
                        new Guid("5f534204-f0d5-4d6f-8efa-7ff248580ba3"),
                        "Groups",
                        null,
                        obj => obj.Groups,
                        null, // lists are read-only properties
                        obj => OnGroups_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<Identity, string>(
                        lazyCtx,
                        new Guid("0d499610-99e3-42cc-b71b-49ed1a356355"),
                        "Password",
                        null,
                        obj => obj.Password,
                        (obj, val) => obj.Password = val,
						obj => OnPassword_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<Identity, string>(
                        lazyCtx,
                        new Guid("a4ce1f5f-311b-4510-8817-4cca40f0bf0f"),
                        "UserName",
                        null,
                        obj => obj.UserName,
                        (obj, val) => obj.UserName = val,
						obj => OnUserName_IsValid), 
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
        [EventBasedMethod("OnToString_Identity")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Identity != null)
            {
                OnToString_Identity(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Identity> OnToString_Identity;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Identity")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Identity != null)
            {
                OnObjectIsValid_Identity(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Identity> OnObjectIsValid_Identity;

        [EventBasedMethod("OnNotifyPreSave_Identity")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Identity != null) OnNotifyPreSave_Identity(this);
        }
        public static event ObjectEventHandler<Identity> OnNotifyPreSave_Identity;

        [EventBasedMethod("OnNotifyPostSave_Identity")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Identity != null) OnNotifyPostSave_Identity(this);
        }
        public static event ObjectEventHandler<Identity> OnNotifyPostSave_Identity;

        [EventBasedMethod("OnNotifyCreated_Identity")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("DisplayName");
            SetNotInitializedProperty("Password");
            SetNotInitializedProperty("UserName");
            base.NotifyCreated();
            if (OnNotifyCreated_Identity != null) OnNotifyCreated_Identity(this);
        }
        public static event ObjectEventHandler<Identity> OnNotifyCreated_Identity;

        [EventBasedMethod("OnNotifyDeleting_Identity")]
        public override void NotifyDeleting()
        {
            Groups.Clear();
            base.NotifyDeleting();
            if (OnNotifyDeleting_Identity != null) OnNotifyDeleting_Identity(this);
        }
        public static event ObjectEventHandler<Identity> OnNotifyDeleting_Identity;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this._DisplayName, binStream);
            BinarySerializer.ToStream(this._Password, binStream);
            BinarySerializer.ToStream(this._UserName, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._DisplayName, binStream);
            BinarySerializer.FromStream(out this._Password, binStream);
            BinarySerializer.FromStream(out this._UserName, binStream);
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
            XmlStreamer.ToStream(this._DisplayName, xml, "DisplayName", "Kistl.App.Base");
            XmlStreamer.ToStream(this._Password, xml, "Password", "Kistl.App.Base");
            XmlStreamer.ToStream(this._UserName, xml, "UserName", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._DisplayName, xml, "DisplayName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Password, xml, "Password", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._UserName, xml, "UserName", "Kistl.App.Base");
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