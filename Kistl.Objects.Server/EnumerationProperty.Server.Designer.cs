//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_EnumerationProperty_Enumeration_Enumeration", "A_Enumeration", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Enumeration), "B_EnumerationProperty", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.EnumerationProperty))]

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
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="EnumerationProperty")]
    public class EnumerationProperty : Kistl.App.Base.ValueTypeProperty
    {
        
        private System.Nullable<int> _fk_Enumeration = null;
        
        public EnumerationProperty()
        {
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_EnumerationProperty_Enumeration_Enumeration", "A_Enumeration")]
        public Kistl.App.Base.Enumeration Enumeration
        {
            get
            {
                EntityReference<Kistl.App.Base.Enumeration> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Enumeration>("Model.FK_EnumerationProperty_Enumeration_Enumeration", "A_Enumeration");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Enumeration> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Enumeration>("Model.FK_EnumerationProperty_Enumeration_Enumeration", "A_Enumeration");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public System.Nullable<int> fk_Enumeration
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Enumeration != null)
                {
                    _fk_Enumeration = Enumeration.ID;
                }
                return _fk_Enumeration;
            }
            set
            {
                _fk_Enumeration = value;
            }
        }
        
        public event ToStringHandler<EnumerationProperty> OnToString_EnumerationProperty;
        
        public event ObjectEventHandler<EnumerationProperty> OnPreSave_EnumerationProperty;
        
        public event ObjectEventHandler<EnumerationProperty> OnPostSave_EnumerationProperty;
        
        public event GetPropertyTypeString_Handler<EnumerationProperty> OnGetPropertyTypeString_EnumerationProperty;
        
        public event GetGUIRepresentation_Handler<EnumerationProperty> OnGetGUIRepresentation_EnumerationProperty;
        
        public event GetPropertyType_Handler<EnumerationProperty> OnGetPropertyType_EnumerationProperty;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_EnumerationProperty != null)
            {
                OnToString_EnumerationProperty(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_EnumerationProperty != null) OnPreSave_EnumerationProperty(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_EnumerationProperty != null) OnPostSave_EnumerationProperty(this);
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        
        public override string GetPropertyTypeString()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetPropertyTypeString();
            if (OnGetPropertyTypeString_EnumerationProperty != null)
            {
                OnGetPropertyTypeString_EnumerationProperty(this, e);
            };
            return e.Result;
        }
        
        public override string GetGUIRepresentation()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetGUIRepresentation();
            if (OnGetGUIRepresentation_EnumerationProperty != null)
            {
                OnGetGUIRepresentation_EnumerationProperty(this, e);
            };
            return e.Result;
        }
        
        public override System.Type GetPropertyType()
        {
            MethodReturnEventArgs<System.Type> e = new MethodReturnEventArgs<System.Type>();
            e.Result = base.GetPropertyType();
            if (OnGetPropertyType_EnumerationProperty != null)
            {
                OnGetPropertyType_EnumerationProperty(this, e);
            };
            return e.Result;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this.fk_Enumeration, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._fk_Enumeration, sr);
        }
    }
}
