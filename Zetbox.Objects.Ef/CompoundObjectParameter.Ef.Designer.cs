// <autogenerated/>

namespace Zetbox.App.Base
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

    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Zetbox.API.Server;
    using Zetbox.DalProvider.Ef;

    /// <summary>
    /// Metadefinition Object for a CompoundObject Parameter.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="CompoundObjectParameter")]
    [System.Diagnostics.DebuggerDisplay("CompoundObjectParameter")]
    public class CompoundObjectParameterEfImpl : Zetbox.App.Base.BaseParameterEfImpl, CompoundObjectParameter
    {
        private static readonly Guid _objectClassID = new Guid("3915cfbf-33c4-4a25-bc5f-b2dd07a9439d");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public CompoundObjectParameterEfImpl()
            : base(null)
        {
        }

        public CompoundObjectParameterEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_CPParameter_has_CompoundObject
    A: ZeroOrMore CompoundObjectParameter as CPParameter
    B: One CompoundObject as CompoundObject
    Preferred Storage: MergeIntoA
    */
        // object reference property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for CompoundObject
        // fkBackingName=_fk_CompoundObject; fkGuidBackingName=_fk_guid_CompoundObject;
        // referencedInterface=Zetbox.App.Base.CompoundObject; moduleNamespace=Zetbox.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Base.CompoundObject CompoundObject
        {
            get { return CompoundObjectImpl; }
            set { CompoundObjectImpl = (Zetbox.App.Base.CompoundObjectEfImpl)value; }
        }

        private int? _fk_CompoundObject;

        private Guid? _fk_guid_CompoundObject = null;

        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_CPParameter_has_CompoundObject", "CompoundObject")]
        public Zetbox.App.Base.CompoundObjectEfImpl CompoundObjectImpl
        {
            get
            {
                Zetbox.App.Base.CompoundObjectEfImpl __value;
                EntityReference<Zetbox.App.Base.CompoundObjectEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Base.CompoundObjectEfImpl>(
                        "Model.FK_CPParameter_has_CompoundObject",
                        "CompoundObject");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnCompoundObject_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.CompoundObject>(__value);
                    OnCompoundObject_Getter(this, e);
                    __value = (Zetbox.App.Base.CompoundObjectEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                EntityReference<Zetbox.App.Base.CompoundObjectEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Base.CompoundObjectEfImpl>(
                        "Model.FK_CPParameter_has_CompoundObject",
                        "CompoundObject");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Zetbox.App.Base.CompoundObjectEfImpl __oldValue = (Zetbox.App.Base.CompoundObjectEfImpl)r.Value;
                Zetbox.App.Base.CompoundObjectEfImpl __newValue = (Zetbox.App.Base.CompoundObjectEfImpl)value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("CompoundObject", __oldValue, __newValue);

                if (OnCompoundObject_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.CompoundObject>(__oldValue, __newValue);
                    OnCompoundObject_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.CompoundObjectEfImpl)e.Result;
                }

                r.Value = (Zetbox.App.Base.CompoundObjectEfImpl)__newValue;

                if (OnCompoundObject_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.CompoundObject>(__oldValue, __newValue);
                    OnCompoundObject_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("CompoundObject", __oldValue, __newValue);
                UpdateChangedInfo = true;
            }
        }

        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for CompoundObject
		public static event PropertyGetterHandler<Zetbox.App.Base.CompoundObjectParameter, Zetbox.App.Base.CompoundObject> OnCompoundObject_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.CompoundObjectParameter, Zetbox.App.Base.CompoundObject> OnCompoundObject_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.CompoundObjectParameter, Zetbox.App.Base.CompoundObject> OnCompoundObject_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.CompoundObjectParameter> OnCompoundObject_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_CompoundObjectParameter")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_CompoundObjectParameter != null)
            {
                OnGetLabel_CompoundObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<CompoundObjectParameter> OnGetLabel_CompoundObjectParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObjectParameter> OnGetLabel_CompoundObjectParameter_CanExec;

        [EventBasedMethod("OnGetLabel_CompoundObjectParameter_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_CompoundObjectParameter_CanExec != null)
				{
					OnGetLabel_CompoundObjectParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObjectParameter> OnGetLabel_CompoundObjectParameter_CanExecReason;

        [EventBasedMethod("OnGetLabel_CompoundObjectParameter_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_CompoundObjectParameter_CanExecReason != null)
				{
					OnGetLabel_CompoundObjectParameter_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetParameterType_CompoundObjectParameter")]
        public override System.Type GetParameterType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_CompoundObjectParameter != null)
            {
                OnGetParameterType_CompoundObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
        public static event GetParameterType_Handler<CompoundObjectParameter> OnGetParameterType_CompoundObjectParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObjectParameter> OnGetParameterType_CompoundObjectParameter_CanExec;

        [EventBasedMethod("OnGetParameterType_CompoundObjectParameter_CanExec")]
        public override bool GetParameterTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterType_CompoundObjectParameter_CanExec != null)
				{
					OnGetParameterType_CompoundObjectParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObjectParameter> OnGetParameterType_CompoundObjectParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterType_CompoundObjectParameter_CanExecReason")]
        public override string GetParameterTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterType_CompoundObjectParameter_CanExecReason != null)
				{
					OnGetParameterType_CompoundObjectParameter_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetParameterTypeString_CompoundObjectParameter")]
        public override string GetParameterTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_CompoundObjectParameter != null)
            {
                OnGetParameterTypeString_CompoundObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
        public static event GetParameterTypeString_Handler<CompoundObjectParameter> OnGetParameterTypeString_CompoundObjectParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObjectParameter> OnGetParameterTypeString_CompoundObjectParameter_CanExec;

        [EventBasedMethod("OnGetParameterTypeString_CompoundObjectParameter_CanExec")]
        public override bool GetParameterTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterTypeString_CompoundObjectParameter_CanExec != null)
				{
					OnGetParameterTypeString_CompoundObjectParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObjectParameter> OnGetParameterTypeString_CompoundObjectParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterTypeString_CompoundObjectParameter_CanExecReason")]
        public override string GetParameterTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterTypeString_CompoundObjectParameter_CanExecReason != null)
				{
					OnGetParameterTypeString_CompoundObjectParameter_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(CompoundObjectParameter);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (CompoundObjectParameter)obj;
            var otherImpl = (CompoundObjectParameterEfImpl)obj;
            var me = (CompoundObjectParameter)this;

            this._fk_CompoundObject = otherImpl._fk_CompoundObject;
        }

        public override void AttachToContext(IZetboxContext ctx)
        {
            base.AttachToContext(ctx);
        }
        public override void SetNew()
        {
            base.SetNew();
        }
        #region Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "CompoundObject":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_CompoundObject.HasValue)
                CompoundObjectImpl = (Zetbox.App.Base.CompoundObjectEfImpl)Context.FindPersistenceObject<Zetbox.App.Base.CompoundObject>(_fk_guid_CompoundObject.Value);
            else
            if (_fk_CompoundObject.HasValue)
                CompoundObjectImpl = (Zetbox.App.Base.CompoundObjectEfImpl)Context.Find<Zetbox.App.Base.CompoundObject>(_fk_CompoundObject.Value);
            else
                CompoundObjectImpl = null;
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
                    new PropertyDescriptorEfImpl<CompoundObjectParameter, Zetbox.App.Base.CompoundObject>(
                        lazyCtx,
                        new Guid("43d03fec-b595-46d0-b5d5-cf4c5d21fda7"),
                        "CompoundObject",
                        null,
                        obj => obj.CompoundObject,
                        (obj, val) => obj.CompoundObject = val,
						obj => OnCompoundObject_IsValid), 
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
        [EventBasedMethod("OnToString_CompoundObjectParameter")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_CompoundObjectParameter != null)
            {
                OnToString_CompoundObjectParameter(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<CompoundObjectParameter> OnToString_CompoundObjectParameter;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_CompoundObjectParameter")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_CompoundObjectParameter != null)
            {
                OnObjectIsValid_CompoundObjectParameter(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<CompoundObjectParameter> OnObjectIsValid_CompoundObjectParameter;

        [EventBasedMethod("OnNotifyPreSave_CompoundObjectParameter")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_CompoundObjectParameter != null) OnNotifyPreSave_CompoundObjectParameter(this);
        }
        public static event ObjectEventHandler<CompoundObjectParameter> OnNotifyPreSave_CompoundObjectParameter;

        [EventBasedMethod("OnNotifyPostSave_CompoundObjectParameter")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_CompoundObjectParameter != null) OnNotifyPostSave_CompoundObjectParameter(this);
        }
        public static event ObjectEventHandler<CompoundObjectParameter> OnNotifyPostSave_CompoundObjectParameter;

        [EventBasedMethod("OnNotifyCreated_CompoundObjectParameter")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("CompoundObject");
            base.NotifyCreated();
            if (OnNotifyCreated_CompoundObjectParameter != null) OnNotifyCreated_CompoundObjectParameter(this);
        }
        public static event ObjectEventHandler<CompoundObjectParameter> OnNotifyCreated_CompoundObjectParameter;

        [EventBasedMethod("OnNotifyDeleting_CompoundObjectParameter")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_CompoundObjectParameter != null) OnNotifyDeleting_CompoundObjectParameter(this);
        }
        public static event ObjectEventHandler<CompoundObjectParameter> OnNotifyDeleting_CompoundObjectParameter;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Zetbox.App.Base.CompoundObjectEfImpl>("Model.FK_CPParameter_has_CompoundObject", "CompoundObject").EntityKey;
                binStream.Write(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            binStream.Read(out this._fk_CompoundObject);
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(CompoundObject != null ? CompoundObject.ExportGuid : (Guid?)null, xml, "CompoundObject", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|CompoundObject":
                this._fk_guid_CompoundObject = XmlStreamer.ReadNullableGuid(xml);
                break;
            }
        }

        #endregion

    }
}