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

    using Kistl.API.Server;
    using Kistl.DalProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// The ControlKind specifies the toolkit-independent kind or type of control that should display a given Presentable.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="ControlKind")]
    [System.Diagnostics.DebuggerDisplay("ControlKind")]
    public class ControlKind__Implementation__ : BaseServerDataObject_EntityFramework, ControlKind, Kistl.API.IExportableInternal
    {
    
		public ControlKind__Implementation__()
		{
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.IdProperty
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

        /// <summary>
        /// Export Guid
        /// </summary>
        // value type property
        private bool _isExportGuidSet = false;
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual Guid ExportGuid
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ExportGuid;
                if (!_isExportGuidSet) {
                    var __p = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("758d8eba-b458-4cd4-98a8-e08713912654"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        __result = this._ExportGuid = (Guid)__p.DefaultValue.GetDefaultValue();
                    } else {
                        Kistl.API.Utils.Logging.Log.Warn("Unable to get default value for property 'ControlKind.ExportGuid'");
                    }
                }
                if (OnExportGuid_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid>(__result);
                    OnExportGuid_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isExportGuidSet = true;
                if (_ExportGuid != value)
                {
                    var __oldValue = _ExportGuid;
                    var __newValue = value;
                    if(OnExportGuid_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ExportGuid", __oldValue, __newValue);
                    _ExportGuid = __newValue;
                    NotifyPropertyChanged("ExportGuid", __oldValue, __newValue);
                    if(OnExportGuid_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PostSetter(this, __e);
                    }
                }
            }
        }
        private Guid _ExportGuid;
		public static event PropertyGetterHandler<Kistl.App.GUI.ControlKind, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ControlKind, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ControlKind, Guid> OnExportGuid_PostSetter;

        /// <summary>
        /// The requested width for displaying this control. May be NULL to request auto-sizing.
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual double? RequestedWidth
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _RequestedWidth;
                if (OnRequestedWidth_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<double?>(__result);
                    OnRequestedWidth_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_RequestedWidth != value)
                {
                    var __oldValue = _RequestedWidth;
                    var __newValue = value;
                    if(OnRequestedWidth_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<double?>(__oldValue, __newValue);
                        OnRequestedWidth_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("RequestedWidth", __oldValue, __newValue);
                    _RequestedWidth = __newValue;
                    NotifyPropertyChanged("RequestedWidth", __oldValue, __newValue);
                    if(OnRequestedWidth_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<double?>(__oldValue, __newValue);
                        OnRequestedWidth_PostSetter(this, __e);
                    }
                }
            }
        }
        private double? _RequestedWidth;
		public static event PropertyGetterHandler<Kistl.App.GUI.ControlKind, double?> OnRequestedWidth_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ControlKind, double?> OnRequestedWidth_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ControlKind, double?> OnRequestedWidth_PostSetter;

		public override Type GetImplementedInterface()
		{
			return typeof(ControlKind);
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ControlKind)obj;
			var otherImpl = (ControlKind__Implementation__)obj;
			var me = (ControlKind)this;

			me.ExportGuid = other.ExportGuid;
			me.RequestedWidth = other.RequestedWidth;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_ControlKind")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ControlKind != null)
            {
                OnToString_ControlKind(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ControlKind> OnToString_ControlKind;

        [EventBasedMethod("OnPreSave_ControlKind")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ControlKind != null) OnPreSave_ControlKind(this);
        }
        public static event ObjectEventHandler<ControlKind> OnPreSave_ControlKind;

        [EventBasedMethod("OnPostSave_ControlKind")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ControlKind != null) OnPostSave_ControlKind(this);
        }
        public static event ObjectEventHandler<ControlKind> OnPostSave_ControlKind;

        [EventBasedMethod("OnCreated_ControlKind")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_ControlKind != null) OnCreated_ControlKind(this);
        }
        public static event ObjectEventHandler<ControlKind> OnCreated_ControlKind;

        [EventBasedMethod("OnDeleting_ControlKind")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_ControlKind != null) OnDeleting_ControlKind(this);
        }
        public static event ObjectEventHandler<ControlKind> OnDeleting_ControlKind;


		private static readonly System.ComponentModel.PropertyDescriptor[] _properties = new System.ComponentModel.PropertyDescriptor[] {
			// else
			new CustomPropertyDescriptor<ControlKind__Implementation__, Guid>(
				new Guid("758d8eba-b458-4cd4-98a8-e08713912654"),
				"ExportGuid",
				null,
				obj => obj.ExportGuid,
				(obj, val) => obj.ExportGuid = val),
			// else
			new CustomPropertyDescriptor<ControlKind__Implementation__, double?>(
				new Guid("2a0d22cd-5538-4a2c-bfb9-287652637708"),
				"RequestedWidth",
				null,
				obj => obj.RequestedWidth,
				(obj, val) => obj.RequestedWidth = val),
		};
		
		protected override void CollectProperties(List<System.ComponentModel.PropertyDescriptor> props)
		{
			base.CollectProperties(props);
			props.AddRange(_properties);
		}
	

		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream(this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.ToStream(this._ExportGuid, binStream);
            }
            BinarySerializer.ToStream(this._RequestedWidth, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.FromStream(out this._ExportGuid, binStream);
            }
            BinarySerializer.FromStream(out this._RequestedWidth, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.ToStream(this._RequestedWidth, xml, "RequestedWidth", "Kistl.App.GUI");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.FromStream(ref this._RequestedWidth, xml, "RequestedWidth", "Kistl.App.GUI");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            
            xml.WriteAttributeString("ExportGuid", this._ExportGuid.ToString());
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this._RequestedWidth, xml, "RequestedWidth", "Kistl.App.GUI");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            this._isExportGuidSet = true;
            XmlStreamer.FromStream(ref this._RequestedWidth, xml, "RequestedWidth", "Kistl.App.GUI");
        }

#endregion

    }


}