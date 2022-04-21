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
    [System.Diagnostics.DebuggerDisplay("ANewObjectClass")]
    public class ANewObjectClassMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, ANewObjectClass
    {
        private static readonly Guid _objectClassID = new Guid("798887f9-2d21-4d02-a143-bf1cbe07469b");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public ANewObjectClassMemoryImpl()
            : base(null)
        {
        }

        public ANewObjectClassMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string TestString
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _TestString;
                if (OnTestString_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnTestString_Getter(this, __e);
                    __result = _TestString = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_TestString != value)
                {
                    var __oldValue = _TestString;
                    var __newValue = value;
                    if (OnTestString_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnTestString_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestString", __oldValue, __newValue);
                    _TestString = __newValue;
                    NotifyPropertyChanged("TestString", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnTestString_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnTestString_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("TestString");
                }
            }
        }
        private string _TestString;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.ANewObjectClass, string> OnTestString_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.ANewObjectClass, string> OnTestString_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.ANewObjectClass, string> OnTestString_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.ANewObjectClass> OnTestString_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(ANewObjectClass);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (ANewObjectClass)obj;
            var otherImpl = (ANewObjectClassMemoryImpl)obj;
            var me = (ANewObjectClass)this;

            me.TestString = other.TestString;
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
                case "TestString":
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

        public override async System.Threading.Tasks.Task ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            await base.ReloadReferences();

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
                    new PropertyDescriptorMemoryImpl<ANewObjectClass, string>(
                        lazyCtx,
                        new Guid("e7371fa9-cd18-4cdc-91c3-a73c0984a019"),
                        "TestString",
                        null,
                        obj => obj.TestString,
                        (obj, val) => obj.TestString = val,
						obj => OnTestString_IsValid), 
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
        [EventBasedMethod("OnToString_ANewObjectClass")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ANewObjectClass != null)
            {
                OnToString_ANewObjectClass(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ANewObjectClass> OnToString_ANewObjectClass;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_ANewObjectClass")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_ANewObjectClass != null)
            {
                OnObjectIsValid_ANewObjectClass(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<ANewObjectClass> OnObjectIsValid_ANewObjectClass;

        [EventBasedMethod("OnNotifyPreSave_ANewObjectClass")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_ANewObjectClass != null) OnNotifyPreSave_ANewObjectClass(this);
        }
        public static event ObjectEventHandler<ANewObjectClass> OnNotifyPreSave_ANewObjectClass;

        [EventBasedMethod("OnNotifyPostSave_ANewObjectClass")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_ANewObjectClass != null) OnNotifyPostSave_ANewObjectClass(this);
        }
        public static event ObjectEventHandler<ANewObjectClass> OnNotifyPostSave_ANewObjectClass;

        [EventBasedMethod("OnNotifyCreated_ANewObjectClass")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("TestString");
            base.NotifyCreated();
            if (OnNotifyCreated_ANewObjectClass != null) OnNotifyCreated_ANewObjectClass(this);
        }
        public static event ObjectEventHandler<ANewObjectClass> OnNotifyCreated_ANewObjectClass;

        [EventBasedMethod("OnNotifyDeleting_ANewObjectClass")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_ANewObjectClass != null) OnNotifyDeleting_ANewObjectClass(this);
        }
        public static event ObjectEventHandler<ANewObjectClass> OnNotifyDeleting_ANewObjectClass;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._TestString);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._TestString = binStream.ReadString();
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