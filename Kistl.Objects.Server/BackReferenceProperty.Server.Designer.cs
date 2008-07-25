//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:2.0.50727.1433
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_BackReferenceProperty_ObjectReferenceProperty", "A_ObjectReferenceProperty", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectReferenceProperty), "B_BackReferenceProperty", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.BackReferenceProperty))]

namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Xml;
    using System.Xml.Serialization;
    using Kistl.API;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Kistl.API.Server;
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="BackReferenceProperty")]
    public class BackReferenceProperty : Kistl.App.Base.BaseProperty
    {
        
        private System.Nullable<int> _fk_ReferenceProperty = null;
        
        private bool _PreFetchToClient;
        
        public BackReferenceProperty()
        {
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_BackReferenceProperty_ObjectReferenceProperty", "A_ObjectReferenceProperty")]
        public Kistl.App.Base.ObjectReferenceProperty ReferenceProperty
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectReferenceProperty> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectReferenceProperty>("Model.FK_BackReferenceProperty_ObjectReferenceProperty", "A_ObjectReferenceProperty");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.ObjectReferenceProperty> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectReferenceProperty>("Model.FK_BackReferenceProperty_ObjectReferenceProperty", "A_ObjectReferenceProperty");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public System.Nullable<int> fk_ReferenceProperty
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && ReferenceProperty != null)
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
        
        [EdmScalarPropertyAttribute()]
        public bool PreFetchToClient
        {
            get
            {
                return _PreFetchToClient;
            }
            set
            {
                if(_PreFetchToClient != value)
                {
                    NotifyPropertyChanging("PreFetchToClient"); 
                    _PreFetchToClient = value; 
                    NotifyPropertyChanged("PreFetchToClient");
                };
            }
        }
        
        public event ToStringHandler<BackReferenceProperty> OnToString_BackReferenceProperty;
        
        public event ObjectEventHandler<BackReferenceProperty> OnPreSave_BackReferenceProperty;
        
        public event ObjectEventHandler<BackReferenceProperty> OnPostSave_BackReferenceProperty;
        
        public event GetPropertyTypeString_Handler<BackReferenceProperty> OnGetPropertyTypeString_BackReferenceProperty;
        
        public event GetGUIRepresentation_Handler<BackReferenceProperty> OnGetGUIRepresentation_BackReferenceProperty;
        
        public event GetPropertyType_Handler<BackReferenceProperty> OnGetPropertyType_BackReferenceProperty;
        
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
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BackReferenceProperty != null) OnPreSave_BackReferenceProperty(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BackReferenceProperty != null) OnPostSave_BackReferenceProperty(this);
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        
        public override string GetPropertyTypeString()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetPropertyTypeString();
            if (OnGetPropertyTypeString_BackReferenceProperty != null)
            {
                OnGetPropertyTypeString_BackReferenceProperty(this, e);
            };
            return e.Result;
        }
        
        public override string GetGUIRepresentation()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetGUIRepresentation();
            if (OnGetGUIRepresentation_BackReferenceProperty != null)
            {
                OnGetGUIRepresentation_BackReferenceProperty(this, e);
            };
            return e.Result;
        }
        
        public override System.Type GetPropertyType()
        {
            MethodReturnEventArgs<System.Type> e = new MethodReturnEventArgs<System.Type>();
            e.Result = base.GetPropertyType();
            if (OnGetPropertyType_BackReferenceProperty != null)
            {
                OnGetPropertyType_BackReferenceProperty(this, e);
            };
            return e.Result;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this.fk_ReferenceProperty, sw);
            BinarySerializer.ToBinary(this._PreFetchToClient, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._fk_ReferenceProperty, sr);
            BinarySerializer.FromBinary(out this._PreFetchToClient, sr);
        }
    }
}
