
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
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="PresenterInfo")]
    [System.Diagnostics.DebuggerDisplay("PresenterInfo")]
    public class PresenterInfo__Implementation__ : BaseServerDataObject_EntityFramework, PresenterInfo
    {
    
		public PresenterInfo__Implementation__()
		{
            {
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// which controls are handled by this Presenter
        /// </summary>
        // enumeration property
        // implement the user-visible interface
        Kistl.App.GUI.VisualType PresenterInfo.ControlType
        {
            get
            {
                return _ControlType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ControlType != value)
                {
					var __oldValue = _ControlType;
                    NotifyPropertyChanging("ControlType", __oldValue, value);
                    _ControlType = value;
                    NotifyPropertyChanged("ControlType", __oldValue, value);
                }
            }
        }
        
        /// <summary>backing store for ControlType</summary>
        private Kistl.App.GUI.VisualType _ControlType;
        
        /// <summary>EF sees only this property, for ControlType</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int ControlType
        {
            get
            {
                return (int)((PresenterInfo)this).ControlType;
            }
            set
            {
                ((PresenterInfo)this).ControlType = (Kistl.App.GUI.VisualType)value;
            }
        }
        

        /// <summary>
        /// The Assembly of the Data Type
        /// </summary>
    /*
    Relation: FK_PresenterInfo_Assembly_PresenterInfo_54
    A: ZeroOrMore PresenterInfo as PresenterInfo
    B: ZeroOrOne Assembly as DataAssembly
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly DataAssembly
        {
            get
            {
                return DataAssembly__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                DataAssembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DataAssembly
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && DataAssembly != null)
                {
                    _fk_DataAssembly = DataAssembly.ID;
                }
                return _fk_DataAssembly;
            }
            set
            {
                _fk_DataAssembly = value;
            }
        }
        private int? _fk_DataAssembly;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_PresenterInfo_Assembly_PresenterInfo_54", "DataAssembly")]
        public Kistl.App.Base.Assembly__Implementation__ DataAssembly__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_PresenterInfo_Assembly_PresenterInfo_54",
                        "DataAssembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_PresenterInfo_Assembly_PresenterInfo_54",
                        "DataAssembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// The CLR namespace and class name of the Data Type
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string DataTypeName
        {
            get
            {
                return _DataTypeName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DataTypeName != value)
                {
					var __oldValue = _DataTypeName;
                    NotifyPropertyChanging("DataTypeName", __oldValue, value);
                    _DataTypeName = value;
                    NotifyPropertyChanged("DataTypeName", __oldValue, value);
                }
            }
        }
        private string _DataTypeName;

        /// <summary>
        /// Where to find the implementation of the Presenter
        /// </summary>
    /*
    Relation: FK_PresenterInfo_Assembly_PresenterInfo_53
    A: ZeroOrMore PresenterInfo as PresenterInfo
    B: ZeroOrOne Assembly as PresenterAssembly
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly PresenterAssembly
        {
            get
            {
                return PresenterAssembly__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                PresenterAssembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_PresenterAssembly
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && PresenterAssembly != null)
                {
                    _fk_PresenterAssembly = PresenterAssembly.ID;
                }
                return _fk_PresenterAssembly;
            }
            set
            {
                _fk_PresenterAssembly = value;
            }
        }
        private int? _fk_PresenterAssembly;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_PresenterInfo_Assembly_PresenterInfo_53", "PresenterAssembly")]
        public Kistl.App.Base.Assembly__Implementation__ PresenterAssembly__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_PresenterInfo_Assembly_PresenterInfo_53",
                        "PresenterAssembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_PresenterInfo_Assembly_PresenterInfo_53",
                        "PresenterAssembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// The CLR namespace and class name of the Presenter
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string PresenterTypeName
        {
            get
            {
                return _PresenterTypeName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PresenterTypeName != value)
                {
					var __oldValue = _PresenterTypeName;
                    NotifyPropertyChanging("PresenterTypeName", __oldValue, value);
                    _PresenterTypeName = value;
                    NotifyPropertyChanged("PresenterTypeName", __oldValue, value);
                }
            }
        }
        private string _PresenterTypeName;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(PresenterInfo));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (PresenterInfo)obj;
			var otherImpl = (PresenterInfo__Implementation__)obj;
			var me = (PresenterInfo)this;

			me.ControlType = other.ControlType;
			me.DataTypeName = other.DataTypeName;
			me.PresenterTypeName = other.PresenterTypeName;
			this.fk_DataAssembly = otherImpl.fk_DataAssembly;
			this.fk_PresenterAssembly = otherImpl.fk_PresenterAssembly;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_PresenterInfo != null)
            {
                OnToString_PresenterInfo(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<PresenterInfo> OnToString_PresenterInfo;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_PresenterInfo != null) OnPreSave_PresenterInfo(this);
        }
        public event ObjectEventHandler<PresenterInfo> OnPreSave_PresenterInfo;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_PresenterInfo != null) OnPostSave_PresenterInfo(this);
        }
        public event ObjectEventHandler<PresenterInfo> OnPostSave_PresenterInfo;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "ControlType":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(137).Constraints
						.Where(c => !c.IsValid(this, this.ControlType))
						.Select(c => c.GetErrorText(this, this.ControlType))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DataAssembly":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(147).Constraints
						.Where(c => !c.IsValid(this, this.DataAssembly))
						.Select(c => c.GetErrorText(this, this.DataAssembly))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DataTypeName":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(148).Constraints
						.Where(c => !c.IsValid(this, this.DataTypeName))
						.Select(c => c.GetErrorText(this, this.DataTypeName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "PresenterAssembly":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(138).Constraints
						.Where(c => !c.IsValid(this, this.PresenterAssembly))
						.Select(c => c.GetErrorText(this, this.PresenterAssembly))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "PresenterTypeName":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(139).Constraints
						.Where(c => !c.IsValid(this, this.PresenterTypeName))
						.Select(c => c.GetErrorText(this, this.PresenterTypeName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_PresenterAssembly.HasValue)
				PresenterAssembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)Context.Find<Kistl.App.Base.Assembly>(_fk_PresenterAssembly.Value);
			else
				PresenterAssembly__Implementation__ = null;
			if (_fk_DataAssembly.HasValue)
				DataAssembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)Context.Find<Kistl.App.Base.Assembly>(_fk_DataAssembly.Value);
			else
				DataAssembly__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream((int)((PresenterInfo)this).ControlType, binStream);
            BinarySerializer.ToStream(this.fk_DataAssembly, binStream);
            BinarySerializer.ToStream(this._DataTypeName, binStream);
            BinarySerializer.ToStream(this.fk_PresenterAssembly, binStream);
            BinarySerializer.ToStream(this._PresenterTypeName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStreamConverter(v => ((PresenterInfo)this).ControlType = (Kistl.App.GUI.VisualType)v, binStream);
            {
                var tmp = this.fk_DataAssembly;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_DataAssembly = tmp;
            }
            BinarySerializer.FromStream(out this._DataTypeName, binStream);
            {
                var tmp = this.fk_PresenterAssembly;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_PresenterAssembly = tmp;
            }
            BinarySerializer.FromStream(out this._PresenterTypeName, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            // TODO: Add XML Serializer here
            XmlStreamer.ToStream(this.fk_DataAssembly, xml, "DataAssembly", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._DataTypeName, xml, "DataTypeName", "Kistl.App.GUI");
            XmlStreamer.ToStream(this.fk_PresenterAssembly, xml, "PresenterAssembly", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._PresenterTypeName, xml, "PresenterTypeName", "Kistl.App.GUI");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            {
                var tmp = this.fk_DataAssembly;
                XmlStreamer.FromStream(ref tmp, xml, "DataAssembly", "http://dasz.at/Kistl");
                this.fk_DataAssembly = tmp;
            }
            XmlStreamer.FromStream(ref this._DataTypeName, xml, "DataTypeName", "Kistl.App.GUI");
            {
                var tmp = this.fk_PresenterAssembly;
                XmlStreamer.FromStream(ref tmp, xml, "PresenterAssembly", "http://dasz.at/Kistl");
                this.fk_PresenterAssembly = tmp;
            }
            XmlStreamer.FromStream(ref this._PresenterTypeName, xml, "PresenterTypeName", "Kistl.App.GUI");
        }

#endregion

    }


}