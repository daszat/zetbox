
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
    /// Metadefinition Object for BackReference Properties.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="BackReferenceProperty")]
    [System.Diagnostics.DebuggerDisplay("BackReferenceProperty")]
    public class BackReferenceProperty__Implementation__ : Kistl.App.Base.BaseProperty__Implementation__, BackReferenceProperty
    {


        /// <summary>
        /// Serialisierung der Liste zum Client
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual bool PreFetchToClient
        {
            get
            {
                return _PreFetchToClient;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PreFetchToClient != value)
                {
                    NotifyPropertyChanging("PreFetchToClient");
                    _PreFetchToClient = value;
                    NotifyPropertyChanged("PreFetchToClient");
                }
            }
        }
        private bool _PreFetchToClient;

        /// <summary>
        /// Das Property, welches auf diese Klasse zeigt
        /// </summary>
    /*
    Relation: FK_BackReferenceProperty_ObjectReferenceProperty_BackReferenceProperty_28
    A: ZeroOrMore BackReferenceProperty as BackReferenceProperty
    B: ZeroOrOne ObjectReferenceProperty as ReferenceProperty
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectReferenceProperty ReferenceProperty
        {
            get
            {
                return ReferenceProperty__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                ReferenceProperty__Implementation__ = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ReferenceProperty
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && ReferenceProperty != null)
                {
                    _fk_ReferenceProperty = ReferenceProperty.ID;
                }
                return _fk_ReferenceProperty;
            }
            set
            {
                _fk_ReferenceProperty = value;
            }
        }
        private int? _fk_ReferenceProperty;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_BackReferenceProperty_ObjectReferenceProperty_BackReferenceProperty_28", "ReferenceProperty")]
        public Kistl.App.Base.ObjectReferenceProperty__Implementation__ ReferenceProperty__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__>(
                        "Model.FK_BackReferenceProperty_ObjectReferenceProperty_BackReferenceProperty_28",
                        "ReferenceProperty");
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
                EntityReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__>(
                        "Model.FK_BackReferenceProperty_ObjectReferenceProperty_BackReferenceProperty_28",
                        "ReferenceProperty");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_BackReferenceProperty != null)
            {
                OnGetGUIRepresentation_BackReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetGUIRepresentation();
            }
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<BackReferenceProperty> OnGetGUIRepresentation_BackReferenceProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_BackReferenceProperty != null)
            {
                OnGetPropertyType_BackReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<BackReferenceProperty> OnGetPropertyType_BackReferenceProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_BackReferenceProperty != null)
            {
                OnGetPropertyTypeString_BackReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<BackReferenceProperty> OnGetPropertyTypeString_BackReferenceProperty;



		public override Type GetInterfaceType()
		{
			return typeof(BackReferenceProperty);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BackReferenceProperty != null)
            {
                OnToString_BackReferenceProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<BackReferenceProperty> OnToString_BackReferenceProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BackReferenceProperty != null) OnPreSave_BackReferenceProperty(this);
        }
        public event ObjectEventHandler<BackReferenceProperty> OnPreSave_BackReferenceProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BackReferenceProperty != null) OnPostSave_BackReferenceProperty(this);
        }
        public event ObjectEventHandler<BackReferenceProperty> OnPostSave_BackReferenceProperty;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._PreFetchToClient, binStream);
            BinarySerializer.ToStream(this.fk_ReferenceProperty, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._PreFetchToClient, binStream);
            {
                var tmp = this.fk_ReferenceProperty;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_ReferenceProperty = tmp;
            }
        }

#endregion

    }


}