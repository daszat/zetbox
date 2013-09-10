// <autogenerated/>

namespace Zetbox.App.Test
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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SecurityTestParent")]
    public class SecurityTestParentMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, SecurityTestParent
    {
        private static readonly Guid _objectClassID = new Guid("779f8526-3f7f-41be-8186-f22062c302a2");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public SecurityTestParentMemoryImpl()
            : base(null)
        {
        }

        public SecurityTestParentMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // object list property
        // Zetbox.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Zetbox.App.Test.SecurityTestChild> Children
        {
            get
            {
                if (_Children == null)
                {
                    TriggerFetchChildrenAsync().Wait();
                }
                return _Children;
            }
        }

        Zetbox.API.Async.ZbTask _triggerFetchChildrenTask;
        public Zetbox.API.Async.ZbTask TriggerFetchChildrenAsync()
        {
            if (_triggerFetchChildrenTask != null) return _triggerFetchChildrenTask;

            List<Zetbox.App.Test.SecurityTestChild> serverList = null;
            if (Helper.IsPersistedObject(this))
            {
                _triggerFetchChildrenTask = Context.GetListOfAsync<Zetbox.App.Test.SecurityTestChild>(this, "Children")
                    .OnResult(t =>
                    {
                        serverList = t.Result;
                    });
            }
            else
            {
                _triggerFetchChildrenTask = new Zetbox.API.Async.ZbTask(Zetbox.API.Async.ZbTask.Synchron, () =>
                {
                    serverList = new List<Zetbox.App.Test.SecurityTestChild>();
                });
            }

            _triggerFetchChildrenTask.OnResult(t =>
            {
                _Children = new OneNRelationList<Zetbox.App.Test.SecurityTestChild>(
                    "Parent",
                    null,
                    this,
                    OnChildrenCollectionChanged,
                    serverList);
            });
            return _triggerFetchChildrenTask;
        }

        internal void OnChildrenCollectionChanged()
        {
            NotifyPropertyChanged("Children", null, null);
            if (OnChildren_PostSetter != null && IsAttached)
                OnChildren_PostSetter(this);
        }

        private OneNRelationList<Zetbox.App.Test.SecurityTestChild> _Children;
public static event PropertyListChangedHandler<Zetbox.App.Test.SecurityTestParent> OnChildren_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.SecurityTestParent> OnChildren_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string Name
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
                    __result = _Name = __e.Result;
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
                    if (OnName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
                    NotifyPropertyChanged("Name", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Name");
                }
            }
        }
        private string _Name;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.SecurityTestParent, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.SecurityTestParent, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.SecurityTestParent, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.SecurityTestParent> OnName_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(SecurityTestParent);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (SecurityTestParent)obj;
            var otherImpl = (SecurityTestParentMemoryImpl)obj;
            var me = (SecurityTestParent)this;

            me.Name = other.Name;
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
                case "Name":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Children":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Children":
                return TriggerFetchChildrenAsync();
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
            _triggerFetchChildrenTask = null;
            _Children = null;
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<SecurityTestParent, ICollection<Zetbox.App.Test.SecurityTestChild>>(
                        lazyCtx,
                        new Guid("d4c5c1e6-1dab-4287-9a64-bd31b1c3923f"),
                        "Children",
                        null,
                        obj => obj.Children,
                        null, // lists are read-only properties
                        obj => OnChildren_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<SecurityTestParent, string>(
                        lazyCtx,
                        new Guid("0f8291ef-2a35-4d8e-bc17-761c0a1cfe88"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
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
        [EventBasedMethod("OnToString_SecurityTestParent")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_SecurityTestParent != null)
            {
                OnToString_SecurityTestParent(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<SecurityTestParent> OnToString_SecurityTestParent;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_SecurityTestParent")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_SecurityTestParent != null)
            {
                OnObjectIsValid_SecurityTestParent(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<SecurityTestParent> OnObjectIsValid_SecurityTestParent;

        [EventBasedMethod("OnNotifyPreSave_SecurityTestParent")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_SecurityTestParent != null) OnNotifyPreSave_SecurityTestParent(this);
        }
        public static event ObjectEventHandler<SecurityTestParent> OnNotifyPreSave_SecurityTestParent;

        [EventBasedMethod("OnNotifyPostSave_SecurityTestParent")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_SecurityTestParent != null) OnNotifyPostSave_SecurityTestParent(this);
        }
        public static event ObjectEventHandler<SecurityTestParent> OnNotifyPostSave_SecurityTestParent;

        [EventBasedMethod("OnNotifyCreated_SecurityTestParent")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_SecurityTestParent != null) OnNotifyCreated_SecurityTestParent(this);
        }
        public static event ObjectEventHandler<SecurityTestParent> OnNotifyCreated_SecurityTestParent;

        [EventBasedMethod("OnNotifyDeleting_SecurityTestParent")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_SecurityTestParent != null) OnNotifyDeleting_SecurityTestParent(this);
            Children.Clear();
        }
        public static event ObjectEventHandler<SecurityTestParent> OnNotifyDeleting_SecurityTestParent;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._Name = binStream.ReadString();
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