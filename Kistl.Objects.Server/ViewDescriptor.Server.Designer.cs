
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
    [EdmEntityType(NamespaceName="Model", Name="ViewDescriptor")]
    [System.Diagnostics.DebuggerDisplay("ViewDescriptor")]
    public class ViewDescriptor__Implementation__ : BaseServerDataObject_EntityFramework, ViewDescriptor
    {
    
		public ViewDescriptor__Implementation__()
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
        /// The control implementing this View
        /// </summary>
    /*
    Relation: FK_ViewDescriptor_TypeRef_View_76
    A: ZeroOrMore ViewDescriptor as View
    B: One TypeRef as ControlRef
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef ControlRef
        {
            get
            {
                return ControlRef__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                ControlRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ControlRef
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && ControlRef != null)
                {
                    _fk_ControlRef = ControlRef.ID;
                }
                return _fk_ControlRef;
            }
            set
            {
                _fk_ControlRef = value;
            }
        }
        private int? _fk_ControlRef;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ViewDescriptor_TypeRef_View_76", "ControlRef")]
        public Kistl.App.Base.TypeRef__Implementation__ ControlRef__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_ViewDescriptor_TypeRef_View_76",
                        "ControlRef");
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
                        "Model.FK_ViewDescriptor_TypeRef_View_76",
                        "ControlRef");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// The PresentableModel usable by this View
        /// </summary>
    /*
    Relation: FK_ViewDescriptor_PresentableModelDescriptor_View_75
    A: ZeroOrMore ViewDescriptor as View
    B: One PresentableModelDescriptor as PresentedModelDescriptor
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.PresentableModelDescriptor PresentedModelDescriptor
        {
            get
            {
                return PresentedModelDescriptor__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                PresentedModelDescriptor__Implementation__ = (Kistl.App.GUI.PresentableModelDescriptor__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_PresentedModelDescriptor
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && PresentedModelDescriptor != null)
                {
                    _fk_PresentedModelDescriptor = PresentedModelDescriptor.ID;
                }
                return _fk_PresentedModelDescriptor;
            }
            set
            {
                _fk_PresentedModelDescriptor = value;
            }
        }
        private int? _fk_PresentedModelDescriptor;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ViewDescriptor_PresentableModelDescriptor_View_75", "PresentedModelDescriptor")]
        public Kistl.App.GUI.PresentableModelDescriptor__Implementation__ PresentedModelDescriptor__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.PresentableModelDescriptor__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.PresentableModelDescriptor__Implementation__>(
                        "Model.FK_ViewDescriptor_PresentableModelDescriptor_View_75",
                        "PresentedModelDescriptor");
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
                EntityReference<Kistl.App.GUI.PresentableModelDescriptor__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.PresentableModelDescriptor__Implementation__>(
                        "Model.FK_ViewDescriptor_PresentableModelDescriptor_View_75",
                        "PresentedModelDescriptor");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.GUI.PresentableModelDescriptor__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Which toolkit provides this View
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
					var __oldValue = _Toolkit;
                    NotifyPropertyChanging("Toolkit", __oldValue, value);
                    _Toolkit = value;
                    NotifyPropertyChanged("Toolkit", __oldValue, value);
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
        /// The visual type of this View
        /// </summary>
        // enumeration property
        // implement the user-visible interface
        Kistl.App.GUI.VisualType ViewDescriptor.VisualType
        {
            get
            {
                return _VisualType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_VisualType != value)
                {
					var __oldValue = _VisualType;
                    NotifyPropertyChanging("VisualType", __oldValue, value);
                    _VisualType = value;
                    NotifyPropertyChanged("VisualType", __oldValue, value);
                }
            }
        }
        
        /// <summary>backing store for VisualType</summary>
        private Kistl.App.GUI.VisualType _VisualType;
        
        /// <summary>EF sees only this property, for VisualType</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int VisualType
        {
            get
            {
                return (int)((ViewDescriptor)this).VisualType;
            }
            set
            {
                ((ViewDescriptor)this).VisualType = (Kistl.App.GUI.VisualType)value;
            }
        }
        

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ViewDescriptor));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ViewDescriptor)obj;
			var otherImpl = (ViewDescriptor__Implementation__)obj;
			var me = (ViewDescriptor)this;

			me.Toolkit = other.Toolkit;
			me.VisualType = other.VisualType;
			this.fk_ControlRef = otherImpl.fk_ControlRef;
			this.fk_PresentedModelDescriptor = otherImpl.fk_PresentedModelDescriptor;
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



		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_PresentedModelDescriptor.HasValue)
				PresentedModelDescriptor__Implementation__ = (Kistl.App.GUI.PresentableModelDescriptor__Implementation__)Context.Find<Kistl.App.GUI.PresentableModelDescriptor>(_fk_PresentedModelDescriptor.Value);
			else
				PresentedModelDescriptor__Implementation__ = null;
			if (_fk_ControlRef.HasValue)
				ControlRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.Find<Kistl.App.Base.TypeRef>(_fk_ControlRef.Value);
			else
				ControlRef__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_ControlRef, binStream);
            BinarySerializer.ToStream(this.fk_PresentedModelDescriptor, binStream);
            BinarySerializer.ToStream((int)((ViewDescriptor)this).Toolkit, binStream);
            BinarySerializer.ToStream((int)((ViewDescriptor)this).VisualType, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_ControlRef;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_ControlRef = tmp;
            }
            {
                var tmp = this.fk_PresentedModelDescriptor;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_PresentedModelDescriptor = tmp;
            }
            BinarySerializer.FromStreamConverter(v => ((ViewDescriptor)this).Toolkit = (Kistl.App.GUI.Toolkit)v, binStream);
            BinarySerializer.FromStreamConverter(v => ((ViewDescriptor)this).VisualType = (Kistl.App.GUI.VisualType)v, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this.fk_ControlRef, xml, "ControlRef", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this.fk_PresentedModelDescriptor, xml, "PresentedModelDescriptor", "http://dasz.at/Kistl");
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            {
                var tmp = this.fk_ControlRef;
                XmlStreamer.FromStream(ref tmp, xml, "ControlRef", "http://dasz.at/Kistl");
                this.fk_ControlRef = tmp;
            }
            {
                var tmp = this.fk_PresentedModelDescriptor;
                XmlStreamer.FromStream(ref tmp, xml, "PresentedModelDescriptor", "http://dasz.at/Kistl");
                this.fk_PresentedModelDescriptor = tmp;
            }
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

#endregion

    }


}