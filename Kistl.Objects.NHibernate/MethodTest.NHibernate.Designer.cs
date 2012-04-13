// <autogenerated/>

namespace Kistl.App.Test
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

    using Kistl.API.Utils;
    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.NHibernate;

    /// <summary>
    /// Test class for methods
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MethodTest")]
    public class MethodTestNHibernateImpl : Kistl.DalProvider.NHibernate.DataObjectNHibernateImpl, MethodTest
    {
        private static readonly Guid _objectClassID = new Guid("68a664ee-67e0-4ba7-a0dc-148b9dfa32a7");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public MethodTestNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public MethodTestNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new MethodTestProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public MethodTestNHibernateImpl(Func<IFrozenContext> lazyCtx, MethodTestProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly MethodTestProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // object list property

        // Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Test.MethodTest> Children
        {
            get
            {
                if (_Children == null)
                {
                    _Children = new OneNRelationList<Kistl.App.Test.MethodTest>(
                        "Parent",
                        null,
                        this,
                        () => this.NotifyPropertyChanging("Children", null, null),
                        () => { this.NotifyPropertyChanged("Children", null, null); if(OnChildren_PostSetter != null && IsAttached) OnChildren_PostSetter(this); },
                        new ProjectedCollection<Kistl.App.Test.MethodTestNHibernateImpl.MethodTestProxy, Kistl.App.Test.MethodTest>(
                            Proxy.Children,
                            p => (Kistl.App.Test.MethodTest)OurContext.AttachAndWrap(p),
                            d => (Kistl.App.Test.MethodTestNHibernateImpl.MethodTestProxy)((NHibernatePersistenceObject)d).NHibernateProxy));
                }
                return _Children;
            }
        }
    
        private OneNRelationList<Kistl.App.Test.MethodTest> _Children;
public static event PropertyListChangedHandler<Kistl.App.Test.MethodTest> OnChildren_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.MethodTest> OnChildren_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Parent
        // fkBackingName=this.Proxy.Parent; fkGuidBackingName=_fk_guid_Parent;
        // referencedInterface=Kistl.App.Test.MethodTest; moduleNamespace=Kistl.App.Test;
        // inverse Navigator=Children; is list;
        // PositionStorage=none;
        // Target not exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Test.MethodTest Parent
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Test.MethodTestNHibernateImpl __value = (Kistl.App.Test.MethodTestNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Parent);

                if (OnParent_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Test.MethodTest>(__value);
                    OnParent_Getter(this, e);
                    __value = (Kistl.App.Test.MethodTestNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.Parent == null)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = (Kistl.App.Test.MethodTestNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Parent);
                var __newValue = (Kistl.App.Test.MethodTestNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                    return;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Parent", __oldValue, __newValue);

                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("Children", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("Children", null, null);
                }

                if (OnParent_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Test.MethodTest>(__oldValue, __newValue);
                    OnParent_PreSetter(this, e);
                    __newValue = (Kistl.App.Test.MethodTestNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.Parent = null;
                }
                else
                {
                    this.Proxy.Parent = __newValue.Proxy;
                }

                // now fixup redundant, inverse references
                // The inverse navigator will also fire events when changed, so should
                // only be touched after setting the local value above.
                // TODO: for complete correctness, the "other" Changing event should also fire
                //       before the local value is changed
                if (__oldValue != null)
                {
                    // remove from old list
                    (__oldValue.Children as IRelationListSync<Kistl.App.Test.MethodTest>).RemoveWithoutClearParent(this);
                }

                if (__newValue != null)
                {
                    // add to new list
                    (__newValue.Children as IRelationListSync<Kistl.App.Test.MethodTest>).AddWithoutSetParent(this);
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("Parent", __oldValue, __newValue);

                if (OnParent_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Test.MethodTest>(__oldValue, __newValue);
                    OnParent_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for Parent's id, used on dehydration only</summary>
        private int? _fk_Parent = null;


        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Parent
		public static event PropertyGetterHandler<Kistl.App.Test.MethodTest, Kistl.App.Test.MethodTest> OnParent_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.MethodTest, Kistl.App.Test.MethodTest> OnParent_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.MethodTest, Kistl.App.Test.MethodTest> OnParent_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.MethodTest> OnParent_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string StringProp
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.StringProp;
                if (OnStringProp_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnStringProp_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.StringProp != value)
                {
                    var __oldValue = Proxy.StringProp;
                    var __newValue = value;
                    if (OnStringProp_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnStringProp_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("StringProp", __oldValue, __newValue);
                    Proxy.StringProp = __newValue;
                    NotifyPropertyChanged("StringProp", __oldValue, __newValue);

                    if (OnStringProp_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnStringProp_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("StringProp");
				}
            }
        }

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Test.MethodTest, string> OnStringProp_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.MethodTest, string> OnStringProp_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.MethodTest, string> OnStringProp_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.MethodTest> OnStringProp_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGroup1_MethodTest")]
        public virtual void Group1()
        {
            // base.Group1();
            if (OnGroup1_MethodTest != null)
            {
                OnGroup1_MethodTest(this);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method MethodTest.Group1");
            }
        }
        public delegate void Group1_Handler<T>(T obj);
        public static event Group1_Handler<MethodTest> OnGroup1_MethodTest;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MethodTest> OnGroup1_MethodTest_CanExec;

        [EventBasedMethod("OnGroup1_MethodTest_CanExec")]
        public virtual bool Group1CanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGroup1_MethodTest_CanExec != null)
				{
					OnGroup1_MethodTest_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MethodTest> OnGroup1_MethodTest_CanExecReason;

        [EventBasedMethod("OnGroup1_MethodTest_CanExecReason")]
        public virtual string Group1CanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGroup1_MethodTest_CanExecReason != null)
				{
					OnGroup1_MethodTest_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGroup2_MethodTest")]
        public virtual void Group2()
        {
            // base.Group2();
            if (OnGroup2_MethodTest != null)
            {
                OnGroup2_MethodTest(this);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method MethodTest.Group2");
            }
        }
        public delegate void Group2_Handler<T>(T obj);
        public static event Group2_Handler<MethodTest> OnGroup2_MethodTest;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MethodTest> OnGroup2_MethodTest_CanExec;

        [EventBasedMethod("OnGroup2_MethodTest_CanExec")]
        public virtual bool Group2CanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGroup2_MethodTest_CanExec != null)
				{
					OnGroup2_MethodTest_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MethodTest> OnGroup2_MethodTest_CanExecReason;

        [EventBasedMethod("OnGroup2_MethodTest_CanExecReason")]
        public virtual string Group2CanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGroup2_MethodTest_CanExecReason != null)
				{
					OnGroup2_MethodTest_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnObjParameter_MethodTest")]
        public virtual void ObjParameter(Kistl.App.Test.MethodTest objParam)
        {
            // base.ObjParameter();
            if (OnObjParameter_MethodTest != null)
            {
                OnObjParameter_MethodTest(this, objParam);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method MethodTest.ObjParameter");
            }
        }
        public delegate void ObjParameter_Handler<T>(T obj, Kistl.App.Test.MethodTest objParam);
        public static event ObjParameter_Handler<MethodTest> OnObjParameter_MethodTest;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MethodTest> OnObjParameter_MethodTest_CanExec;

        [EventBasedMethod("OnObjParameter_MethodTest_CanExec")]
        public virtual bool ObjParameterCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnObjParameter_MethodTest_CanExec != null)
				{
					OnObjParameter_MethodTest_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MethodTest> OnObjParameter_MethodTest_CanExecReason;

        [EventBasedMethod("OnObjParameter_MethodTest_CanExecReason")]
        public virtual string ObjParameterCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnObjParameter_MethodTest_CanExecReason != null)
				{
					OnObjParameter_MethodTest_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnObjRet_MethodTest")]
        public virtual Kistl.App.Test.MethodTest ObjRet()
        {
            var e = new MethodReturnEventArgs<Kistl.App.Test.MethodTest>();
            if (OnObjRet_MethodTest != null)
            {
                OnObjRet_MethodTest(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on MethodTest.ObjRet");
            }
            return e.Result;
        }
        public delegate void ObjRet_Handler<T>(T obj, MethodReturnEventArgs<Kistl.App.Test.MethodTest> ret);
        public static event ObjRet_Handler<MethodTest> OnObjRet_MethodTest;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MethodTest> OnObjRet_MethodTest_CanExec;

        [EventBasedMethod("OnObjRet_MethodTest_CanExec")]
        public virtual bool ObjRetCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnObjRet_MethodTest_CanExec != null)
				{
					OnObjRet_MethodTest_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MethodTest> OnObjRet_MethodTest_CanExecReason;

        [EventBasedMethod("OnObjRet_MethodTest_CanExecReason")]
        public virtual string ObjRetCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnObjRet_MethodTest_CanExecReason != null)
				{
					OnObjRet_MethodTest_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnParameterless_MethodTest")]
        public virtual void Parameterless()
        {
            // base.Parameterless();
            if (OnParameterless_MethodTest != null)
            {
                OnParameterless_MethodTest(this);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method MethodTest.Parameterless");
            }
        }
        public delegate void Parameterless_Handler<T>(T obj);
        public static event Parameterless_Handler<MethodTest> OnParameterless_MethodTest;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MethodTest> OnParameterless_MethodTest_CanExec;

        [EventBasedMethod("OnParameterless_MethodTest_CanExec")]
        public virtual bool ParameterlessCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnParameterless_MethodTest_CanExec != null)
				{
					OnParameterless_MethodTest_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MethodTest> OnParameterless_MethodTest_CanExecReason;

        [EventBasedMethod("OnParameterless_MethodTest_CanExecReason")]
        public virtual string ParameterlessCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnParameterless_MethodTest_CanExecReason != null)
				{
					OnParameterless_MethodTest_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnStringParameter_MethodTest")]
        public virtual void StringParameter(string str)
        {
            // base.StringParameter();
            if (OnStringParameter_MethodTest != null)
            {
                OnStringParameter_MethodTest(this, str);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method MethodTest.StringParameter");
            }
        }
        public delegate void StringParameter_Handler<T>(T obj, string str);
        public static event StringParameter_Handler<MethodTest> OnStringParameter_MethodTest;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MethodTest> OnStringParameter_MethodTest_CanExec;

        [EventBasedMethod("OnStringParameter_MethodTest_CanExec")]
        public virtual bool StringParameterCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnStringParameter_MethodTest_CanExec != null)
				{
					OnStringParameter_MethodTest_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MethodTest> OnStringParameter_MethodTest_CanExecReason;

        [EventBasedMethod("OnStringParameter_MethodTest_CanExecReason")]
        public virtual string StringParameterCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnStringParameter_MethodTest_CanExecReason != null)
				{
					OnStringParameter_MethodTest_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnSummary_MethodTest")]
        public virtual void Summary()
        {
            // base.Summary();
            if (OnSummary_MethodTest != null)
            {
                OnSummary_MethodTest(this);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method MethodTest.Summary");
            }
        }
        public delegate void Summary_Handler<T>(T obj);
        public static event Summary_Handler<MethodTest> OnSummary_MethodTest;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MethodTest> OnSummary_MethodTest_CanExec;

        [EventBasedMethod("OnSummary_MethodTest_CanExec")]
        public virtual bool SummaryCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnSummary_MethodTest_CanExec != null)
				{
					OnSummary_MethodTest_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MethodTest> OnSummary_MethodTest_CanExecReason;

        [EventBasedMethod("OnSummary_MethodTest_CanExecReason")]
        public virtual string SummaryCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnSummary_MethodTest_CanExecReason != null)
				{
					OnSummary_MethodTest_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(MethodTest);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (MethodTest)obj;
            var otherImpl = (MethodTestNHibernateImpl)obj;
            var me = (MethodTest)this;

            me.StringProp = other.StringProp;
            this._fk_Parent = otherImpl._fk_Parent;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            var nhCtx = (NHibernateContext)ctx;
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Parent":
                    {
                        var __oldValue = (Kistl.App.Test.MethodTestNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Parent);
                        var __newValue = (Kistl.App.Test.MethodTestNHibernateImpl)parentObj;
                        NotifyPropertyChanging("Parent", __oldValue, __newValue);
                        this.Proxy.Parent = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("Parent", __oldValue, __newValue);
                    }
                    break;
                default:
                    base.UpdateParent(propertyName, parentObj);
                    break;
            }
        }
        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Parent":
                case "StringProp":
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

            if (_fk_Parent.HasValue)
                this.Parent = ((Kistl.App.Test.MethodTestNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Test.MethodTest>(_fk_Parent.Value));
            else
                this.Parent = null;
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<MethodTest, ICollection<Kistl.App.Test.MethodTest>>(
                        lazyCtx,
                        new Guid("bf48b883-8821-4c4e-8509-590a72604f9e"),
                        "Children",
                        null,
                        obj => obj.Children,
                        null, // lists are read-only properties
                        obj => OnChildren_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<MethodTest, Kistl.App.Test.MethodTest>(
                        lazyCtx,
                        new Guid("02a7d534-9325-48e5-bbc2-b61420afd940"),
                        "Parent",
                        null,
                        obj => obj.Parent,
                        (obj, val) => obj.Parent = val,
						obj => OnParent_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<MethodTest, string>(
                        lazyCtx,
                        new Guid("8d226658-fecc-4139-8234-aa88a4738b4d"),
                        "StringProp",
                        null,
                        obj => obj.StringProp,
                        (obj, val) => obj.StringProp = val,
						obj => OnStringProp_IsValid), 
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
        #region Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_MethodTest")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_MethodTest != null)
            {
                OnToString_MethodTest(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<MethodTest> OnToString_MethodTest;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_MethodTest")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_MethodTest != null)
            {
                OnObjectIsValid_MethodTest(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<MethodTest> OnObjectIsValid_MethodTest;

        [EventBasedMethod("OnNotifyPreSave_MethodTest")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_MethodTest != null) OnNotifyPreSave_MethodTest(this);
        }
        public static event ObjectEventHandler<MethodTest> OnNotifyPreSave_MethodTest;

        [EventBasedMethod("OnNotifyPostSave_MethodTest")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_MethodTest != null) OnNotifyPostSave_MethodTest(this);
        }
        public static event ObjectEventHandler<MethodTest> OnNotifyPostSave_MethodTest;

        [EventBasedMethod("OnNotifyCreated_MethodTest")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Parent");
            SetNotInitializedProperty("StringProp");
            base.NotifyCreated();
            if (OnNotifyCreated_MethodTest != null) OnNotifyCreated_MethodTest(this);
        }
        public static event ObjectEventHandler<MethodTest> OnNotifyCreated_MethodTest;

        [EventBasedMethod("OnNotifyDeleting_MethodTest")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_MethodTest != null) OnNotifyDeleting_MethodTest(this);
        }
        public static event ObjectEventHandler<MethodTest> OnNotifyDeleting_MethodTest;

        #endregion // Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods
        public override List<NHibernatePersistenceObject> GetParentsToDelete()
        {
            var result = base.GetParentsToDelete();

            return result;
        }

        public override List<NHibernatePersistenceObject> GetChildrenToDelete()
        {
            var result = base.GetChildrenToDelete();

            // Follow Parent_has_Children
            result.AddRange(Context.AttachedObjects
                .OfType<Kistl.App.Test.MethodTest>()
                .Where(child => child.Parent == this
                    && child.ObjectState == DataObjectState.Deleted)
                .Cast<NHibernatePersistenceObject>());

            return result;
        }


        public class MethodTestProxy
            : IProxyObject, ISortKey<int>
        {
            public MethodTestProxy()
            {
                Children = new Collection<Kistl.App.Test.MethodTestNHibernateImpl.MethodTestProxy>();
            }

            public virtual int ID { get; set; }

            public virtual Type ZBoxWrapper { get { return typeof(MethodTestNHibernateImpl); } }
            public virtual Type ZBoxProxy { get { return typeof(MethodTestProxy); } }

            public virtual ICollection<Kistl.App.Test.MethodTestNHibernateImpl.MethodTestProxy> Children { get; set; }

            public virtual Kistl.App.Test.MethodTestNHibernateImpl.MethodTestProxy Parent { get; set; }

            public virtual string StringProp { get; set; }

        }

        // make proxy available for the provider
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this.Proxy.Parent != null ? OurContext.GetIdFromProxy(this.Proxy.Parent) : (int?)null, binStream);
            BinarySerializer.ToStream(this.Proxy.StringProp, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_Parent, binStream);
            {
                string tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.StringProp = tmp;
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
            XmlStreamer.ToStream(this.Proxy.Parent != null ? OurContext.GetIdFromProxy(this.Proxy.Parent) : (int?)null, xml, "Parent", "Kistl.App.Test");
            XmlStreamer.ToStream(this.Proxy.StringProp, xml, "StringProp", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_Parent, xml, "Parent", "Kistl.App.Test");
            {
                // yuck
                string tmp = this.Proxy.StringProp;
                XmlStreamer.FromStream(ref tmp, xml, "StringProp", "Kistl.App.Test");
                this.Proxy.StringProp = tmp;
            }
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