//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_StructProperty_Struct_StructDefinition", "A_Struct", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Struct__Implementation__), "B_StructProperty", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.StructProperty__Implementation__))]

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
    using Kistl.DALProvider.EF;
    using Kistl.API.Server;
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="StructProperty")]
    public class StructProperty__Implementation__ : Kistl.App.Base.Property__Implementation__, StructProperty
    {
        
        private System.Nullable<int> _fk_StructDefinition = null;
        
        public StructProperty__Implementation__()
        {
        }
        
        [XmlIgnore()]
        public Kistl.App.Base.Struct StructDefinition
        {
            get
            {
                return StructDefinition__Implementation__;
            }
            set
            {
                StructDefinition__Implementation__ = (Kistl.App.Base.Struct__Implementation__)value;
            }
        }
        
        public System.Nullable<int> fk_StructDefinition
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && StructDefinition != null)
                {
                    _fk_StructDefinition = StructDefinition.ID;
                }
                return _fk_StructDefinition;
            }
            set
            {
                _fk_StructDefinition = value;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_StructProperty_Struct_StructDefinition", "A_Struct")]
        public Kistl.App.Base.Struct__Implementation__ StructDefinition__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Struct__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Struct__Implementation__>("Model.FK_StructProperty_Struct_StructDefinition", "A_Struct");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Struct__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Struct__Implementation__>("Model.FK_StructProperty_Struct_StructDefinition", "A_Struct");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = (Kistl.App.Base.Struct__Implementation__)value;
            }
        }
        
        public event ToStringHandler<StructProperty> OnToString_StructProperty;
        
        public event ObjectEventHandler<StructProperty> OnPreSave_StructProperty;
        
        public event ObjectEventHandler<StructProperty> OnPostSave_StructProperty;
        
        public event GetPropertyTypeString_Handler<StructProperty> OnGetPropertyTypeString_StructProperty;
        
        public event GetGUIRepresentation_Handler<StructProperty> OnGetGUIRepresentation_StructProperty;
        
        public event GetPropertyType_Handler<StructProperty> OnGetPropertyType_StructProperty;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_StructProperty != null)
            {
                OnToString_StructProperty(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_StructProperty != null) OnPreSave_StructProperty(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_StructProperty != null) OnPostSave_StructProperty(this);
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        
        public override string GetPropertyTypeString()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetPropertyTypeString();
            if (OnGetPropertyTypeString_StructProperty != null)
            {
                OnGetPropertyTypeString_StructProperty(this, e);
            };
            return e.Result;
        }
        
        public override string GetGUIRepresentation()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetGUIRepresentation();
            if (OnGetGUIRepresentation_StructProperty != null)
            {
                OnGetGUIRepresentation_StructProperty(this, e);
            };
            return e.Result;
        }
        
        public override System.Type GetPropertyType()
        {
            MethodReturnEventArgs<System.Type> e = new MethodReturnEventArgs<System.Type>();
            e.Result = base.GetPropertyType();
            if (OnGetPropertyType_StructProperty != null)
            {
                OnGetPropertyType_StructProperty(this, e);
            };
            return e.Result;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this.fk_StructDefinition, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._fk_StructDefinition, sr);
        }
    }
}
