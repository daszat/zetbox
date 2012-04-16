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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("IndexConstraint")]
    public class IndexConstraintMemoryImpl : Kistl.App.Base.InstanceConstraintMemoryImpl, IndexConstraint
    {
        private static readonly Guid _objectClassID = new Guid("1d5a58e9-fba6-4ef8-b3b7-9966a4dcba83");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public IndexConstraintMemoryImpl()
            : base(null)
        {
        }

        public IndexConstraintMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Index is created as a Unique Index
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public bool IsUnique
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(bool);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _IsUnique;
                if (!_isIsUniqueSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("2cc6e028-e01f-4879-bda8-78d459c0eaf4"));
                    if (__p != null) {
                        _isIsUniqueSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this._IsUnique = (bool)__tmp_value;
                    } else {
                        Kistl.API.Utils.Logging.Log.Warn("Unable to get default value for property 'IndexConstraint.IsUnique'");
                    }
                }
                if (OnIsUnique_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnIsUnique_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isIsUniqueSet = true;
                if (_IsUnique != value)
                {
                    var __oldValue = _IsUnique;
                    var __newValue = value;
                    if (OnIsUnique_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsUnique_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IsUnique", __oldValue, __newValue);
                    _IsUnique = __newValue;
                    NotifyPropertyChanged("IsUnique", __oldValue, __newValue);

                    if (OnIsUnique_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsUnique_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("IsUnique");
				}
            }
        }
        private bool _IsUnique;
        private bool _isIsUniqueSet = false;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.IndexConstraint, bool> OnIsUnique_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.IndexConstraint, bool> OnIsUnique_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.IndexConstraint, bool> OnIsUnique_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.IndexConstraint> OnIsUnique_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
   		// Kistl.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Kistl.App.Base.Property> Properties
		{
			get
			{
				if (_Properties == null)
				{
					Context.FetchRelation<Kistl.App.Base.IndexConstraint_ensures_unique_on_Property_RelationEntryMemoryImpl>(new Guid("29235ba6-5979-4ed8-8e75-6bd0837c7f28"), RelationEndRole.A, this);
					_Properties 
						= new ObservableBSideCollectionWrapper<Kistl.App.Base.IndexConstraint, Kistl.App.Base.Property, Kistl.App.Base.IndexConstraint_ensures_unique_on_Property_RelationEntryMemoryImpl, ICollection<Kistl.App.Base.IndexConstraint_ensures_unique_on_Property_RelationEntryMemoryImpl>>(
							this, 
							new RelationshipFilterASideCollection<Kistl.App.Base.IndexConstraint_ensures_unique_on_Property_RelationEntryMemoryImpl>(this.Context, this));
				}
				return (ICollection<Kistl.App.Base.Property>)_Properties;
			}
		}

		private ObservableBSideCollectionWrapper<Kistl.App.Base.IndexConstraint, Kistl.App.Base.Property, Kistl.App.Base.IndexConstraint_ensures_unique_on_Property_RelationEntryMemoryImpl, ICollection<Kistl.App.Base.IndexConstraint_ensures_unique_on_Property_RelationEntryMemoryImpl>> _Properties;

        public static event PropertyIsValidHandler<Kistl.App.Base.IndexConstraint> OnProperties_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_IndexConstraint")]
        public override string GetErrorText(Kistl.API.IDataObject constrainedObject)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_IndexConstraint != null)
            {
                OnGetErrorText_IndexConstraint(this, e, constrainedObject);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject);
            }
            return e.Result;
        }
        public static event GetErrorText_Handler<IndexConstraint> OnGetErrorText_IndexConstraint;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IndexConstraint> OnGetErrorText_IndexConstraint_CanExec;

        [EventBasedMethod("OnGetErrorText_IndexConstraint_CanExec")]
        public override bool GetErrorTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetErrorText_IndexConstraint_CanExec != null)
				{
					OnGetErrorText_IndexConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IndexConstraint> OnGetErrorText_IndexConstraint_CanExecReason;

        [EventBasedMethod("OnGetErrorText_IndexConstraint_CanExecReason")]
        public override string GetErrorTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetErrorText_IndexConstraint_CanExecReason != null)
				{
					OnGetErrorText_IndexConstraint_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnIsValid_IndexConstraint")]
        public override bool IsValid(Kistl.API.IDataObject constrainedObject)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_IndexConstraint != null)
            {
                OnIsValid_IndexConstraint(this, e, constrainedObject);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject);
            }
            return e.Result;
        }
        public static event IsValid_Handler<IndexConstraint> OnIsValid_IndexConstraint;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IndexConstraint> OnIsValid_IndexConstraint_CanExec;

        [EventBasedMethod("OnIsValid_IndexConstraint_CanExec")]
        public override bool IsValidCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnIsValid_IndexConstraint_CanExec != null)
				{
					OnIsValid_IndexConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IndexConstraint> OnIsValid_IndexConstraint_CanExecReason;

        [EventBasedMethod("OnIsValid_IndexConstraint_CanExecReason")]
        public override string IsValidCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnIsValid_IndexConstraint_CanExecReason != null)
				{
					OnIsValid_IndexConstraint_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(IndexConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (IndexConstraint)obj;
            var otherImpl = (IndexConstraintMemoryImpl)obj;
            var me = (IndexConstraint)this;

            me.IsUnique = other.IsUnique;
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
                case "IsUnique":
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
                    new PropertyDescriptorMemoryImpl<IndexConstraint, bool>(
                        lazyCtx,
                        new Guid("2cc6e028-e01f-4879-bda8-78d459c0eaf4"),
                        "IsUnique",
                        null,
                        obj => obj.IsUnique,
                        (obj, val) => obj.IsUnique = val,
						obj => OnIsUnique_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<IndexConstraint, ICollection<Kistl.App.Base.Property>>(
                        lazyCtx,
                        new Guid("3e4bfd37-1037-472b-a5d7-2c20a777e6fd"),
                        "Properties",
                        null,
                        obj => obj.Properties,
                        null, // lists are read-only properties
                        obj => OnProperties_IsValid), 
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
        [EventBasedMethod("OnToString_IndexConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_IndexConstraint != null)
            {
                OnToString_IndexConstraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<IndexConstraint> OnToString_IndexConstraint;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_IndexConstraint")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_IndexConstraint != null)
            {
                OnObjectIsValid_IndexConstraint(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<IndexConstraint> OnObjectIsValid_IndexConstraint;

        [EventBasedMethod("OnNotifyPreSave_IndexConstraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_IndexConstraint != null) OnNotifyPreSave_IndexConstraint(this);
        }
        public static event ObjectEventHandler<IndexConstraint> OnNotifyPreSave_IndexConstraint;

        [EventBasedMethod("OnNotifyPostSave_IndexConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_IndexConstraint != null) OnNotifyPostSave_IndexConstraint(this);
        }
        public static event ObjectEventHandler<IndexConstraint> OnNotifyPostSave_IndexConstraint;

        [EventBasedMethod("OnNotifyCreated_IndexConstraint")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_IndexConstraint != null) OnNotifyCreated_IndexConstraint(this);
        }
        public static event ObjectEventHandler<IndexConstraint> OnNotifyCreated_IndexConstraint;

        [EventBasedMethod("OnNotifyDeleting_IndexConstraint")]
        public override void NotifyDeleting()
        {
            Properties.Clear();
            base.NotifyDeleting();
            if (OnNotifyDeleting_IndexConstraint != null) OnNotifyDeleting_IndexConstraint(this);
        }
        public static event ObjectEventHandler<IndexConstraint> OnNotifyDeleting_IndexConstraint;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this._isIsUniqueSet, binStream);
            if (this._isIsUniqueSet) {
                BinarySerializer.ToStream(this._IsUnique, binStream);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._isIsUniqueSet, binStream);
            if (this._isIsUniqueSet) {
                BinarySerializer.FromStream(out this._IsUnique, binStream);
            }
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
            XmlStreamer.ToStream(this._isIsUniqueSet, xml, "IsIsUniqueSet", "Kistl.App.Base");
            if (this._isIsUniqueSet) {
                XmlStreamer.ToStream(this._IsUnique, xml, "IsUnique", "Kistl.App.Base");
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._isIsUniqueSet, xml, "IsIsUniqueSet", "Kistl.App.Base");
            if (this._isIsUniqueSet) {
                XmlStreamer.FromStream(ref this._IsUnique, xml, "IsUnique", "Kistl.App.Base");
            }
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            base.Export(xml, modules);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            System.Diagnostics.Debug.Assert(this._isIsUniqueSet, "Exported objects need to have all default values evaluated");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._IsUnique, xml, "IsUnique", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            // Import must have default value set
            XmlStreamer.FromStream(ref this._IsUnique, xml, "IsUnique", "Kistl.App.Base");
            this._isIsUniqueSet = true;
        }

        #endregion

    }
}