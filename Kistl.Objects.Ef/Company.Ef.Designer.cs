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

    using Kistl.API.Server;
    using Kistl.DalProvider.Ef;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Describes a Company
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Company")]
    [System.Diagnostics.DebuggerDisplay("Company")]
    public class CompanyEfImpl : BaseServerDataObject_EntityFramework, Company
    {
        [Obsolete]
        public CompanyEfImpl()
            : base(null)
        {
        }

        public CompanyEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Company name
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Name
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Name;
                if (OnName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnName_Getter(this, __e);
                    __result = __e.Result;
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
                    if (OnName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Name;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.Company, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.Company, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.Company, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.Company> OnName_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(Company);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Company)obj;
            var otherImpl = (CompanyEfImpl)obj;
            var me = (Company)this;

            me.Name = other.Name;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

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
                    new PropertyDescriptorEfImpl<Company, string>(
                        lazyCtx,
                        new Guid("4a038e35-fffb-4ba7-8009-1954c317a799"),
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
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Company")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Company != null)
            {
                OnToString_Company(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Company> OnToString_Company;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Company")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Company != null)
            {
                OnObjectIsValid_Company(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Company> OnObjectIsValid_Company;

        [EventBasedMethod("OnNotifyPreSave_Company")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Company != null) OnNotifyPreSave_Company(this);
        }
        public static event ObjectEventHandler<Company> OnNotifyPreSave_Company;

        [EventBasedMethod("OnNotifyPostSave_Company")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Company != null) OnNotifyPostSave_Company(this);
        }
        public static event ObjectEventHandler<Company> OnNotifyPostSave_Company;

        [EventBasedMethod("OnNotifyCreated_Company")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_Company != null) OnNotifyCreated_Company(this);
        }
        public static event ObjectEventHandler<Company> OnNotifyCreated_Company;

        [EventBasedMethod("OnNotifyDeleting_Company")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_Company != null) OnNotifyDeleting_Company(this);
        }
        public static event ObjectEventHandler<Company> OnNotifyDeleting_Company;

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
            BinarySerializer.ToStream(this._Name, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._Name, binStream);
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
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Test");
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