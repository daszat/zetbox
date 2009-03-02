
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="ViewDescriptor")]
    [System.Diagnostics.DebuggerDisplay("ViewDescriptor")]
    public class ViewDescriptor__Implementation__ : BaseServerDataObject_EntityFramework, ViewDescriptor
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
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_ViewDescriptor_TypeRef_ViewDescriptor_68
    A: 3 ViewDescriptor as ViewDescriptor
    B: 1 TypeRef as LayoutRef
    Preferred Storage: 1
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef LayoutRef
        {
            get
            {
                return LayoutRef__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                LayoutRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_LayoutRef
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && LayoutRef != null)
                {
                    _fk_LayoutRef = LayoutRef.ID;
                }
                return _fk_LayoutRef;
            }
            set
            {
                _fk_LayoutRef = value;
            }
        }
        private int? _fk_LayoutRef;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ViewDescriptor_TypeRef_ViewDescriptor_68", "LayoutRef")]
        public Kistl.App.Base.TypeRef__Implementation__ LayoutRef__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_ViewDescriptor_TypeRef_ViewDescriptor_68",
                        "LayoutRef");
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
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_ViewDescriptor_TypeRef_ViewDescriptor_68",
                        "LayoutRef");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? LayoutRef_pos
        {
            get
            {
                return _LayoutRef_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_LayoutRef_pos != value)
                {
                    NotifyPropertyChanging("LayoutRef_pos");
                    _LayoutRef_pos = value;
                    NotifyPropertyChanged("LayoutRef_pos");
                }
            }
        }
        private int? _LayoutRef_pos;
        

        /// <summary>
        /// 
        /// </summary>
        // enumeration property
        // implement the user-visible interface
        Kistl.App.GUI.Toolkit ViewDescriptor.Toolkit
        {
            get
            {
                return _Toolkit;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Toolkit != value)
                {
                    NotifyPropertyChanging("Toolkit");
                    _Toolkit = value;
                    NotifyPropertyChanged("Toolkit");
                }
            }
        }
        
        /// <summary>backing store for Toolkit</summary>
        private Kistl.App.GUI.Toolkit _Toolkit;
        
        /// <summary>EF sees only this property, for Toolkit</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int Toolkit
        {
            get
            {
                return (int)((ViewDescriptor)this).Toolkit;
            }
            set
            {
                ((ViewDescriptor)this).Toolkit = (Kistl.App.GUI.Toolkit)value;
            }
        }
        

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_ViewDescriptor_TypeRef_ViewDescriptor_69
    A: 3 ViewDescriptor as ViewDescriptor
    B: 1 TypeRef as ViewRef
    Preferred Storage: 1
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef ViewRef
        {
            get
            {
                return ViewRef__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                ViewRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ViewRef
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && ViewRef != null)
                {
                    _fk_ViewRef = ViewRef.ID;
                }
                return _fk_ViewRef;
            }
            set
            {
                _fk_ViewRef = value;
            }
        }
        private int? _fk_ViewRef;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ViewDescriptor_TypeRef_ViewDescriptor_69", "ViewRef")]
        public Kistl.App.Base.TypeRef__Implementation__ ViewRef__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_ViewDescriptor_TypeRef_ViewDescriptor_69",
                        "ViewRef");
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
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_ViewDescriptor_TypeRef_ViewDescriptor_69",
                        "ViewRef");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? ViewRef_pos
        {
            get
            {
                return _ViewRef_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ViewRef_pos != value)
                {
                    NotifyPropertyChanging("ViewRef_pos");
                    _ViewRef_pos = value;
                    NotifyPropertyChanged("ViewRef_pos");
                }
            }
        }
        private int? _ViewRef_pos;
        

		public override Type GetInterfaceType()
		{
			return typeof(ViewDescriptor);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ViewDescriptor != null)
            {
                OnToString_ViewDescriptor(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ViewDescriptor> OnToString_ViewDescriptor;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ViewDescriptor != null) OnPreSave_ViewDescriptor(this);
        }
        public event ObjectEventHandler<ViewDescriptor> OnPreSave_ViewDescriptor;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ViewDescriptor != null) OnPostSave_ViewDescriptor(this);
        }
        public event ObjectEventHandler<ViewDescriptor> OnPostSave_ViewDescriptor;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_LayoutRef, binStream);
            BinarySerializer.ToStream(this._LayoutRef_pos, binStream);
            BinarySerializer.ToStream((int)((ViewDescriptor)this).Toolkit, binStream);
            BinarySerializer.ToStream(this.fk_ViewRef, binStream);
            BinarySerializer.ToStream(this._ViewRef_pos, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_LayoutRef;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_LayoutRef = tmp;
            }
            BinarySerializer.FromStream(out this._LayoutRef_pos, binStream);
            BinarySerializer.FromStreamConverter(v => ((ViewDescriptor)this).Toolkit = (Kistl.App.GUI.Toolkit)v, binStream);
            {
                var tmp = this.fk_ViewRef;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_ViewRef = tmp;
            }
            BinarySerializer.FromStream(out this._ViewRef_pos, binStream);
        }

#endregion

    }


}