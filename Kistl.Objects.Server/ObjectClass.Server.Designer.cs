//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_ObjectClass_ObjectClass_BaseObjectClass", "A_ObjectClass", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass), "B_ObjectClass", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces" +
    "", "A_Interface", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Interface), "B_ObjectClass_ImplementsInterfacesCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_ImplementsInterfacesCollectionEntry))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent", "A_ObjectClass", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass), "B_ObjectClass_ImplementsInterfacesCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_ImplementsInterfacesCollectionEntry))]

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
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="ObjectClass")]
    public class ObjectClass : Kistl.App.Base.DataType
    {
        
        private string _TableName;
        
        private System.Nullable<int> _fk_BaseObjectClass = null;
        
        private bool _IsSimpleObject;
        
        public ObjectClass()
        {
        }
        
        [EdmScalarPropertyAttribute()]
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                if(_TableName != value)
                {
                    NotifyPropertyChanging("TableName"); 
                    _TableName = value; 
                    NotifyPropertyChanged("TableName");
                };
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_ObjectClass_ObjectClass_BaseObjectClass", "A_ObjectClass")]
        public Kistl.App.Base.ObjectClass BaseObjectClass
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectClass> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass>("Model.FK_ObjectClass_ObjectClass_BaseObjectClass", "A_ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.ObjectClass> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass>("Model.FK_ObjectClass_ObjectClass_BaseObjectClass", "A_ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public System.Nullable<int> fk_BaseObjectClass
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && BaseObjectClass != null)
                {
                    _fk_BaseObjectClass = BaseObjectClass.ID;
                }
                return _fk_BaseObjectClass;
            }
            set
            {
                _fk_BaseObjectClass = value;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_ObjectClass_ObjectClass_BaseObjectClass", "B_ObjectClass")]
        public EntityCollection<Kistl.App.Base.ObjectClass> SubClasses
        {
            get
            {
                EntityCollection<Kistl.App.Base.ObjectClass> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Base.ObjectClass>("Model.FK_ObjectClass_ObjectClass_BaseObjectClass", "B_ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent", "B_ObjectClass_ImplementsInterfacesCollectionEntry")]
        public EntityCollection<ObjectClass_ImplementsInterfacesCollectionEntry> ImplementsInterfaces
        {
            get
            {
                EntityCollection<ObjectClass_ImplementsInterfacesCollectionEntry> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<ObjectClass_ImplementsInterfacesCollectionEntry>("Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent", "B_ObjectClass_ImplementsInterfacesCollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public bool IsSimpleObject
        {
            get
            {
                return _IsSimpleObject;
            }
            set
            {
                if(_IsSimpleObject != value)
                {
                    NotifyPropertyChanging("IsSimpleObject"); 
                    _IsSimpleObject = value; 
                    NotifyPropertyChanged("IsSimpleObject");
                };
            }
        }
        
        public event ToStringHandler<ObjectClass> OnToString_ObjectClass;
        
        public event ObjectEventHandler<ObjectClass> OnPreSave_ObjectClass;
        
        public event ObjectEventHandler<ObjectClass> OnPostSave_ObjectClass;
        
        public event GetDataTypeString_Handler<ObjectClass> OnGetDataTypeString_ObjectClass;
        
        public event GetDataType_Handler<ObjectClass> OnGetDataType_ObjectClass;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectClass != null)
            {
                OnToString_ObjectClass(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectClass != null) OnPreSave_ObjectClass(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectClass != null) OnPostSave_ObjectClass(this);
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            ImplementsInterfaces.ToList().ForEach<ICollectionEntry>(i => ctx.Attach(i));
        }
        
        public override string GetDataTypeString()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetDataTypeString();
            if (OnGetDataTypeString_ObjectClass != null)
            {
                OnGetDataTypeString_ObjectClass(this, e);
            };
            return e.Result;
        }
        
        public override System.Type GetDataType()
        {
            MethodReturnEventArgs<System.Type> e = new MethodReturnEventArgs<System.Type>();
            e.Result = base.GetDataType();
            if (OnGetDataType_ObjectClass != null)
            {
                OnGetDataType_ObjectClass(this, e);
            };
            return e.Result;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._TableName, sw);
            BinarySerializer.ToBinary(this.fk_BaseObjectClass, sw);
            BinarySerializer.ToBinary(this.ImplementsInterfaces, sw);
            BinarySerializer.ToBinary(this._IsSimpleObject, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._TableName, sr);
            BinarySerializer.FromBinary(out this._fk_BaseObjectClass, sr);
            BinarySerializer.FromBinaryCollectionEntries(this.ImplementsInterfaces, sr);
            BinarySerializer.FromBinary(out this._IsSimpleObject, sr);
        }
    }
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="ObjectClass_ImplementsInterfacesCollectionEntry")]
    public class ObjectClass_ImplementsInterfacesCollectionEntry : Kistl.API.Server.BaseServerCollectionEntry
    {
        
        private int _ID;
        
        private int _fk_Value;
        
        private int _fk_Parent;
        
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces" +
            "", "A_Interface")]
        public Kistl.App.Base.Interface Value
        {
            get
            {
                EntityReference<Kistl.App.Base.Interface> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Interface>("Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces", "A_Interface");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Interface> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Interface>("Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_Interface_ImplementsInterfaces", "A_Interface");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent", "A_ObjectClass")]
        public ObjectClass Parent
        {
            get
            {
                EntityReference<ObjectClass> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<ObjectClass>("Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent", "A_ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<ObjectClass> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<ObjectClass>("Model.FK_ObjectClass_ImplementsInterfacesCollectionEntry_ObjectClass_fk_Parent", "A_ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public int fk_Value
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Value != null)
                {
                    _fk_Value = Value.ID;
                }
                return _fk_Value;
            }
            set
            {
                _fk_Value = value;
            }
        }
        
        public int fk_Parent
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Parent != null)
                {
                    _fk_Parent = Parent.ID;
                }
                return _fk_Parent;
            }
            set
            {
                _fk_Parent = value;
            }
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this.fk_Value, sw);
            BinarySerializer.ToBinary(this.fk_Parent, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._fk_Value, sr);
            BinarySerializer.FromBinary(out this._fk_Parent, sr);
        }
    }
}
