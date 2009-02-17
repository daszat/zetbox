
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
    [EdmEntityType(NamespaceName="Model", Name="ControlInfo")]
    [System.Diagnostics.DebuggerDisplay("ControlInfo")]
    public class ControlInfo__Implementation__ : BaseServerDataObject_EntityFramework, ControlInfo
    {

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
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");;
                }
            }
        }
        private int _ID;

        /// <summary>
        /// The assembly containing the Control
        /// </summary>
    /*
    NewRelation: FK_ControlInfo_Assembly_ControlInfo_31 
    A: ZeroOrMore ControlInfo as ControlInfo (site: A, no Relation, prop ID=114)
    B: ZeroOrOne Assembly as Assembly (site: B, no Relation, prop ID=114)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly Assembly
        {
            get
            {
                return Assembly__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Assembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int fk_Assembly
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Assembly != null)
                {
                    _fk_Assembly = Assembly.ID;
                }
                return _fk_Assembly;
            }
            set
            {
                _fk_Assembly = value;
            }
        }
        private int _fk_Assembly;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ControlInfo_Assembly_ControlInfo_31", "Assembly")]
        public Kistl.App.Base.Assembly__Implementation__ Assembly__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_ControlInfo_Assembly_ControlInfo_31",
                        "Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_ControlInfo_Assembly_ControlInfo_31",
                        "Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// The name of the class implementing this Control
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ClassName != value)
                {
                    NotifyPropertyChanging("ClassName");
                    _ClassName = value;
                    NotifyPropertyChanged("ClassName");;
                }
            }
        }
        private string _ClassName;

        /// <summary>
        /// The type of Control of this implementation
        /// </summary>
        // enumeration property
        // implement the user-visible interface
        Kistl.App.GUI.VisualType ControlInfo.ControlType
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
                    NotifyPropertyChanging("ControlType");
                    _ControlType = value;
                    NotifyPropertyChanged("ControlType");
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
                return (int)((ControlInfo)this).ControlType;
            }
            set
            {
                ((ControlInfo)this).ControlType = (Kistl.App.GUI.VisualType)value;
            }
        }
        

        /// <summary>
        /// Whether or not this Control can contain other Controls
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual bool IsContainer
        {
            get
            {
                return _IsContainer;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsContainer != value)
                {
                    NotifyPropertyChanging("IsContainer");
                    _IsContainer = value;
                    NotifyPropertyChanged("IsContainer");;
                }
            }
        }
        private bool _IsContainer;

        /// <summary>
        /// The toolkit of this Control.
        /// </summary>
        // enumeration property
        // implement the user-visible interface
        Kistl.App.GUI.Toolkit ControlInfo.Platform
        {
            get
            {
                return _Platform;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Platform != value)
                {
                    NotifyPropertyChanging("Platform");
                    _Platform = value;
                    NotifyPropertyChanged("Platform");
                }
            }
        }
        
        /// <summary>backing store for Platform</summary>
        private Kistl.App.GUI.Toolkit _Platform;
        
        /// <summary>EF sees only this property, for Platform</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int Platform
        {
            get
            {
                return (int)((ControlInfo)this).Platform;
            }
            set
            {
                ((ControlInfo)this).Platform = (Kistl.App.GUI.Toolkit)value;
            }
        }
        

		public override Type GetInterfaceType()
		{
			return typeof(ControlInfo);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ControlInfo != null)
            {
                OnToString_ControlInfo(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ControlInfo> OnToString_ControlInfo;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ControlInfo != null) OnPreSave_ControlInfo(this);
        }
        public event ObjectEventHandler<ControlInfo> OnPreSave_ControlInfo;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ControlInfo != null) OnPostSave_ControlInfo(this);
        }
        public event ObjectEventHandler<ControlInfo> OnPostSave_ControlInfo;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_Assembly, binStream);
            BinarySerializer.ToStream(this._ClassName, binStream);
            BinarySerializer.ToStream((int)((ControlInfo)this).ControlType, binStream);
            BinarySerializer.ToStream(this._IsContainer, binStream);
            BinarySerializer.ToStream((int)((ControlInfo)this).Platform, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Assembly, binStream);
            BinarySerializer.FromStream(out this._ClassName, binStream);
            BinarySerializer.FromStreamConverter(v => ((ControlInfo)this).ControlType = (Kistl.App.GUI.VisualType)v, binStream);
            BinarySerializer.FromStream(out this._IsContainer, binStream);
            BinarySerializer.FromStreamConverter(v => ((ControlInfo)this).Platform = (Kistl.App.GUI.Toolkit)v, binStream);
        }

#endregion

    }


}