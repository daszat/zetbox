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
    /// Testclass for the required_parent tests: child
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RequiredParentChild")]
    public class RequiredParentChildMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, RequiredParentChild
    {
        private static readonly Guid _objectClassID = new Guid("3e7f2f55-ff5c-4a13-ba58-74368e9c8780");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public RequiredParentChildMemoryImpl()
            : base(null)
        {
        }

        public RequiredParentChildMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// dummy property
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
                    UpdateChangedInfo = true;

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
		public static event PropertyGetterHandler<Zetbox.App.Test.RequiredParentChild, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.RequiredParentChild, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.RequiredParentChild, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.RequiredParentChild> OnName_IsValid;

        /// <summary>
        /// 
        /// </summary>
	        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Parent
        // fkBackingName=_fk_Parent; fkGuidBackingName=_fk_guid_Parent;
        // referencedInterface=Zetbox.App.Test.RequiredParent; moduleNamespace=Zetbox.App.Test;
        // inverse Navigator=Children; is list;
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Test.RequiredParent Parent
        {
            get { return ParentImpl; }
            set { ParentImpl = (Zetbox.App.Test.RequiredParentMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        private int? _fk_Parent;


        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Test.RequiredParentMemoryImpl ParentImpl
        {
            get
            {
                Zetbox.App.Test.RequiredParentMemoryImpl __value;
                if (_fk_Parent.HasValue)
                    __value = (Zetbox.App.Test.RequiredParentMemoryImpl)Context.Find<Zetbox.App.Test.RequiredParent>(_fk_Parent.Value);
                else
                    __value = null;

                if (OnParent_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Test.RequiredParent>(__value);
                    OnParent_Getter(this, e);
                    __value = (Zetbox.App.Test.RequiredParentMemoryImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_Parent == null) || (value != null && value.ID == _fk_Parent))
				{
					SetInitializedProperty("Parent");
                    return;
				}

                // cache old value to remove inverse references later
                var __oldValue = ParentImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Parent", __oldValue, __newValue);

                if (OnParent_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Test.RequiredParent>(__oldValue, __newValue);
                    OnParent_PreSetter(this, e);
                    __newValue = (Zetbox.App.Test.RequiredParentMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Parent = __newValue == null ? (int?)null : __newValue.ID;

                // now fixup redundant, inverse references
                // The inverse navigator will also fire events when changed, so should
                // only be touched after setting the local value above.
                // TODO: for complete correctness, the "other" Changing event should also fire
                //       before the local value is changed
                if (__oldValue != null)
                {
                    // remove from old list
                    (__oldValue.Children as IRelationListSync<Zetbox.App.Test.RequiredParentChild>).RemoveWithoutClearParent(this);
                }

                if (__newValue != null)
                {
                    // add to new list
                    (__newValue.Children as IRelationListSync<Zetbox.App.Test.RequiredParentChild>).AddWithoutSetParent(this);
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("Parent", __oldValue, __newValue);
                UpdateChangedInfo = true;

                if (OnParent_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Test.RequiredParent>(__oldValue, __newValue);
                    OnParent_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Parent
		public static event PropertyGetterHandler<Zetbox.App.Test.RequiredParentChild, Zetbox.App.Test.RequiredParent> OnParent_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.RequiredParentChild, Zetbox.App.Test.RequiredParent> OnParent_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.RequiredParentChild, Zetbox.App.Test.RequiredParent> OnParent_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.RequiredParentChild> OnParent_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(RequiredParentChild);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (RequiredParentChild)obj;
            var otherImpl = (RequiredParentChildMemoryImpl)obj;
            var me = (RequiredParentChild)this;

            me.Name = other.Name;
            this._fk_Parent = otherImpl._fk_Parent;
        }

        public override void AttachToContext(IZetboxContext ctx)
        {
            base.AttachToContext(ctx);
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Parent":
                    {
                        var __oldValue = _fk_Parent;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("Parent", __oldValue, __newValue);
                        _fk_Parent = __newValue;
                        NotifyPropertyChanged("Parent", __oldValue, __newValue);
                    }
                    break;
                default:
                    base.UpdateParent(propertyName, parentObj);
                    break;
            }
        }
        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Name":
                case "Parent":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_Parent.HasValue)
                ParentImpl = (Zetbox.App.Test.RequiredParentMemoryImpl)Context.Find<Zetbox.App.Test.RequiredParent>(_fk_Parent.Value);
            else
                ParentImpl = null;
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
                    new PropertyDescriptorMemoryImpl<RequiredParentChild, string>(
                        lazyCtx,
                        new Guid("82dc687e-3915-4f03-9a1f-75e42fcbe7cd"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<RequiredParentChild, Zetbox.App.Test.RequiredParent>(
                        lazyCtx,
                        new Guid("09fb9f88-7a59-4dae-8cad-9fbab99f32c3"),
                        "Parent",
                        null,
                        obj => obj.Parent,
                        (obj, val) => obj.Parent = val,
						obj => OnParent_IsValid), 
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
        [EventBasedMethod("OnToString_RequiredParentChild")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_RequiredParentChild != null)
            {
                OnToString_RequiredParentChild(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<RequiredParentChild> OnToString_RequiredParentChild;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_RequiredParentChild")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_RequiredParentChild != null)
            {
                OnObjectIsValid_RequiredParentChild(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<RequiredParentChild> OnObjectIsValid_RequiredParentChild;

        [EventBasedMethod("OnNotifyPreSave_RequiredParentChild")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_RequiredParentChild != null) OnNotifyPreSave_RequiredParentChild(this);
        }
        public static event ObjectEventHandler<RequiredParentChild> OnNotifyPreSave_RequiredParentChild;

        [EventBasedMethod("OnNotifyPostSave_RequiredParentChild")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_RequiredParentChild != null) OnNotifyPostSave_RequiredParentChild(this);
        }
        public static event ObjectEventHandler<RequiredParentChild> OnNotifyPostSave_RequiredParentChild;

        [EventBasedMethod("OnNotifyCreated_RequiredParentChild")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            SetNotInitializedProperty("Parent");
            base.NotifyCreated();
            if (OnNotifyCreated_RequiredParentChild != null) OnNotifyCreated_RequiredParentChild(this);
        }
        public static event ObjectEventHandler<RequiredParentChild> OnNotifyCreated_RequiredParentChild;

        [EventBasedMethod("OnNotifyDeleting_RequiredParentChild")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_RequiredParentChild != null) OnNotifyDeleting_RequiredParentChild(this);
        }
        public static event ObjectEventHandler<RequiredParentChild> OnNotifyDeleting_RequiredParentChild;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Name);
            binStream.Write(Parent != null ? Parent.ID : (int?)null);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._Name = binStream.ReadString();
            this._fk_Parent = binStream.ReadNullableInt32();
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