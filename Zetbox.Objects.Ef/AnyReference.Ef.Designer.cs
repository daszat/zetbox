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
    /// A reference to any Object Class
    /// </summary>
    [EdmComplexType(NamespaceName="Model", Name="AnyReferenceEfImpl")]
    [System.Diagnostics.DebuggerDisplay("AnyReference")]
    public class AnyReferenceEfImpl : BaseServerCompoundObject_EntityFramework, AnyReference, ICompoundObject
    {
        private static readonly Guid _compoundObjectID = new Guid("fa366f50-f384-4a18-abcd-8b9fef3b41fa");
        public override Guid CompoundObjectID { get { return _compoundObjectID; } }

        public AnyReferenceEfImpl()
            : base(null) // TODO: pass parent's lazyCtx
        {

        }
        public AnyReferenceEfImpl(IPersistenceObject parent, string property)
            : base(null) // TODO: pass parent's lazyCtx
        {
            AttachToObject(parent, property);
        }
        public AnyReferenceEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {

        }
        public AnyReferenceEfImpl(Func<IFrozenContext> lazyCtx, IPersistenceObject parent, string property)
            : base(lazyCtx)
        {
            AttachToObject(parent, property);
        }

        /// <summary>
        /// Guid of the referenced object class
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public Guid? ObjClass
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ObjClass;
                if (OnObjClass_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid?>(__result);
                    OnObjClass_Getter(this, __e);
                    __result = _ObjClass = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ObjClass != value)
                {
                    var __oldValue = _ObjClass;
                    var __newValue = value;
                    if (OnObjClass_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid?>(__oldValue, __newValue);
                        OnObjClass_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ObjClass", __oldValue, __newValue);
                    _ObjClass = __newValue;
                    NotifyPropertyChanged("ObjClass", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnObjClass_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid?>(__oldValue, __newValue);
                        OnObjClass_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("ObjClass");
                }
            }
        }
        private Guid? _ObjClass_store;
        private Guid? _ObjClass {
            get { return _ObjClass_store; }
            set {
                ReportEfPropertyChanging("ObjClass");
                _ObjClass_store = value;
                ReportEfPropertyChanged("ObjClass");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.AnyReference, Guid?> OnObjClass_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.AnyReference, Guid?> OnObjClass_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.AnyReference, Guid?> OnObjClass_PostSetter;

        /// <summary>
        /// Guid of referenced object
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public Guid? ObjGuid
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ObjGuid;
                if (OnObjGuid_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid?>(__result);
                    OnObjGuid_Getter(this, __e);
                    __result = _ObjGuid = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ObjGuid != value)
                {
                    var __oldValue = _ObjGuid;
                    var __newValue = value;
                    if (OnObjGuid_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid?>(__oldValue, __newValue);
                        OnObjGuid_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ObjGuid", __oldValue, __newValue);
                    _ObjGuid = __newValue;
                    NotifyPropertyChanged("ObjGuid", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnObjGuid_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid?>(__oldValue, __newValue);
                        OnObjGuid_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("ObjGuid");
                }
            }
        }
        private Guid? _ObjGuid_store;
        private Guid? _ObjGuid {
            get { return _ObjGuid_store; }
            set {
                ReportEfPropertyChanging("ObjGuid");
                _ObjGuid_store = value;
                ReportEfPropertyChanged("ObjGuid");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.AnyReference, Guid?> OnObjGuid_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.AnyReference, Guid?> OnObjGuid_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.AnyReference, Guid?> OnObjGuid_PostSetter;

        /// <summary>
        /// ID of the referenced object
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public int? ObjID
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ObjID;
                if (OnObjID_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnObjID_Getter(this, __e);
                    __result = _ObjID = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ObjID != value)
                {
                    var __oldValue = _ObjID;
                    var __newValue = value;
                    if (OnObjID_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnObjID_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ObjID", __oldValue, __newValue);
                    _ObjID = __newValue;
                    NotifyPropertyChanged("ObjID", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnObjID_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnObjID_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("ObjID");
                }
            }
        }
        private int? _ObjID_store;
        private int? _ObjID {
            get { return _ObjID_store; }
            set {
                ReportEfPropertyChanging("ObjID");
                _ObjID_store = value;
                ReportEfPropertyChanged("ObjID");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.AnyReference, int?> OnObjID_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.AnyReference, int?> OnObjID_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.AnyReference, int?> OnObjID_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetObject_AnyReference")]
        public virtual Zetbox.API.IDataObject GetObject(Zetbox.API.IZetboxContext ctx)
        {
            var e = new MethodReturnEventArgs<Zetbox.API.IDataObject>();
            if (OnGetObject_AnyReference != null)
            {
                OnGetObject_AnyReference(this, e, ctx);
            }
            else
            {
                throw new NotImplementedException("No handler registered on AnyReference.GetObject");
            }
            return e.Result;
        }
        public delegate void GetObject_Handler<T>(T obj, MethodReturnEventArgs<Zetbox.API.IDataObject> ret, Zetbox.API.IZetboxContext ctx);
        public static event GetObject_Handler<AnyReference> OnGetObject_AnyReference;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<AnyReference> OnGetObject_AnyReference_CanExec;

        [EventBasedMethod("OnGetObject_AnyReference_CanExec")]
        public virtual bool GetObjectCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetObject_AnyReference_CanExec != null)
				{
					OnGetObject_AnyReference_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<AnyReference> OnGetObject_AnyReference_CanExecReason;

        [EventBasedMethod("OnGetObject_AnyReference_CanExecReason")]
        public virtual string GetObjectCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetObject_AnyReference_CanExecReason != null)
				{
					OnGetObject_AnyReference_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnSetObject_AnyReference")]
        public virtual void SetObject(Zetbox.API.IDataObject newObj)
        {
            // base.SetObject();
            if (OnSetObject_AnyReference != null)
            {
                OnSetObject_AnyReference(this, newObj);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method AnyReference.SetObject");
            }
        }
        public delegate void SetObject_Handler<T>(T obj, Zetbox.API.IDataObject newObj);
        public static event SetObject_Handler<AnyReference> OnSetObject_AnyReference;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<AnyReference> OnSetObject_AnyReference_CanExec;

        [EventBasedMethod("OnSetObject_AnyReference_CanExec")]
        public virtual bool SetObjectCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnSetObject_AnyReference_CanExec != null)
				{
					OnSetObject_AnyReference_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<AnyReference> OnSetObject_AnyReference_CanExecReason;

        [EventBasedMethod("OnSetObject_AnyReference_CanExecReason")]
        public virtual string SetObjectCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnSetObject_AnyReference_CanExecReason != null)
				{
					OnSetObject_AnyReference_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(AnyReference);
        }

        public override void ApplyChangesFrom(ICompoundObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (AnyReference)obj;
            var otherImpl = (AnyReferenceEfImpl)obj;
            var me = (AnyReference)this;

            me.ObjClass = other.ObjClass;
            me.ObjGuid = other.ObjGuid;
            me.ObjID = other.ObjID;
        }
        #region Zetbox.Generator.Templates.CompoundObjects.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_AnyReference")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_AnyReference != null)
            {
                OnToString_AnyReference(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<AnyReference> OnToString_AnyReference;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_AnyReference")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_AnyReference != null)
            {
                OnObjectIsValid_AnyReference(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<AnyReference> OnObjectIsValid_AnyReference;

        #endregion // Zetbox.Generator.Templates.CompoundObjects.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._ObjClass);
            binStream.Write(this._ObjGuid);
            binStream.Write(this._ObjID);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._ObjClass = binStream.ReadNullableGuid();
            this._ObjGuid = binStream.ReadNullableGuid();
            this._ObjID = binStream.ReadNullableInt32();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this._ObjClass, xml, "ObjClass", "Zetbox.App.Base");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this._ObjGuid, xml, "ObjGuid", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|ObjClass":
                this._ObjClass = XmlStreamer.ReadNullableGuid(xml);
                break;
            case "Zetbox.App.Base|ObjGuid":
                this._ObjGuid = XmlStreamer.ReadNullableGuid(xml);
                break;
            }
        }

        #endregion

    }
}