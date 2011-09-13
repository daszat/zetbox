// <autogenerated/>

namespace Kistl.App.GUI
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
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="YearFilterConfiguration")]
    [System.Diagnostics.DebuggerDisplay("YearFilterConfiguration")]
    public class YearFilterConfigurationEfImpl : Kistl.App.GUI.PropertyFilterConfigurationEfImpl, YearFilterConfiguration
    {
        [Obsolete]
        public YearFilterConfigurationEfImpl()
            : base(null)
        {
        }

        public YearFilterConfigurationEfImpl(Func<IFrozenContext> lazyCtx)
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
        public bool? IsCurrentYearDefault
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(bool?);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _IsCurrentYearDefault;
                if (OnIsCurrentYearDefault_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool?>(__result);
                    OnIsCurrentYearDefault_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_IsCurrentYearDefault != value)
                {
                    var __oldValue = _IsCurrentYearDefault;
                    var __newValue = value;
                    if (OnIsCurrentYearDefault_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool?>(__oldValue, __newValue);
                        OnIsCurrentYearDefault_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IsCurrentYearDefault", __oldValue, __newValue);
                    _IsCurrentYearDefault = __newValue;
                    NotifyPropertyChanged("IsCurrentYearDefault", __oldValue, __newValue);
                    if (OnIsCurrentYearDefault_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool?>(__oldValue, __newValue);
                        OnIsCurrentYearDefault_PostSetter(this, __e);
                    }
                }
            }
        }
        private bool? _IsCurrentYearDefault;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.YearFilterConfiguration, bool?> OnIsCurrentYearDefault_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.YearFilterConfiguration, bool?> OnIsCurrentYearDefault_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.YearFilterConfiguration, bool?> OnIsCurrentYearDefault_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnCreateFilterModel_YearFilterConfiguration")]
        public override Kistl.API.IFilterModel CreateFilterModel()
        {
            var e = new MethodReturnEventArgs<Kistl.API.IFilterModel>();
            if (OnCreateFilterModel_YearFilterConfiguration != null)
            {
                OnCreateFilterModel_YearFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.CreateFilterModel();
            }
            return e.Result;
        }
        public static event CreateFilterModel_Handler<YearFilterConfiguration> OnCreateFilterModel_YearFilterConfiguration;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_YearFilterConfiguration")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_YearFilterConfiguration != null)
            {
                OnGetLabel_YearFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<YearFilterConfiguration> OnGetLabel_YearFilterConfiguration;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(YearFilterConfiguration);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (YearFilterConfiguration)obj;
            var otherImpl = (YearFilterConfigurationEfImpl)obj;
            var me = (YearFilterConfiguration)this;

            me.IsCurrentYearDefault = other.IsCurrentYearDefault;
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
                    new PropertyDescriptorEfImpl<YearFilterConfigurationEfImpl, bool?>(
                        lazyCtx,
                        new Guid("495159f4-a983-4a2a-a8f1-2291647673fd"),
                        "IsCurrentYearDefault",
                        null,
                        obj => obj.IsCurrentYearDefault,
                        (obj, val) => obj.IsCurrentYearDefault = val),
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
        [EventBasedMethod("OnToString_YearFilterConfiguration")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_YearFilterConfiguration != null)
            {
                OnToString_YearFilterConfiguration(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<YearFilterConfiguration> OnToString_YearFilterConfiguration;

        [EventBasedMethod("OnPreSave_YearFilterConfiguration")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_YearFilterConfiguration != null) OnPreSave_YearFilterConfiguration(this);
        }
        public static event ObjectEventHandler<YearFilterConfiguration> OnPreSave_YearFilterConfiguration;

        [EventBasedMethod("OnPostSave_YearFilterConfiguration")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_YearFilterConfiguration != null) OnPostSave_YearFilterConfiguration(this);
        }
        public static event ObjectEventHandler<YearFilterConfiguration> OnPostSave_YearFilterConfiguration;

        [EventBasedMethod("OnCreated_YearFilterConfiguration")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_YearFilterConfiguration != null) OnCreated_YearFilterConfiguration(this);
        }
        public static event ObjectEventHandler<YearFilterConfiguration> OnCreated_YearFilterConfiguration;

        [EventBasedMethod("OnDeleting_YearFilterConfiguration")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_YearFilterConfiguration != null) OnDeleting_YearFilterConfiguration(this);
        }
        public static event ObjectEventHandler<YearFilterConfiguration> OnDeleting_YearFilterConfiguration;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            BinarySerializer.ToStream(this._IsCurrentYearDefault, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._IsCurrentYearDefault, binStream);
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
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            XmlStreamer.ToStream(this._IsCurrentYearDefault, xml, "IsCurrentYearDefault", "Kistl.App.GUI");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._IsCurrentYearDefault, xml, "IsCurrentYearDefault", "Kistl.App.GUI");
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
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this._IsCurrentYearDefault, xml, "IsCurrentYearDefault", "Kistl.App.GUI");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            XmlStreamer.FromStream(ref this._IsCurrentYearDefault, xml, "IsCurrentYearDefault", "Kistl.App.GUI");
        }

        #endregion

    }
}