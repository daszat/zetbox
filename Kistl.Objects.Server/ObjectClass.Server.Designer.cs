//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_ObjectClass_ObjectClass", "A_ObjectClass", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.ObjectClass), "B_ObjectClass", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_ObjectClass_Module", "A_Module", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module), "B_ObjectClass", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass))]

namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Collections;
    using System.Xml;
    using System.Xml.Serialization;
    using Kistl.API;
    using Kistl.API.Server;
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="ObjectClass")]
    public class ObjectClass : BaseServerDataObject, ICloneable
    {
        
        private int _ID = Helper.INVALIDID;
        
        private string _ClassName;
        
        private string _TableName;
        
        private int _fk_BaseObjectClass = Helper.INVALIDID;
        
        private int _fk_Module = Helper.INVALIDID;
        
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
        
        public override string EntitySetName
        {
            get
            {
                return "ObjectClass";
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                _ClassName = value;
            }
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
                _TableName = value;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_BaseProperty_ObjectClass", "B_BaseProperty")]
        [XmlIgnore()]
        public EntityCollection<Kistl.App.Base.BaseProperty> Properties
        {
            get
            {
                EntityCollection<Kistl.App.Base.BaseProperty> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Base.BaseProperty>("Model.FK_BaseProperty_ObjectClass", "B_BaseProperty");
                if (!c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_ObjectClass_ObjectClass", "A_ObjectClass")]
        [XmlIgnore()]
        public Kistl.App.Base.ObjectClass BaseObjectClass
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectClass> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass>("Model.FK_ObjectClass_ObjectClass", "A_ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.ObjectClass> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass>("Model.FK_ObjectClass_ObjectClass", "A_ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public int fk_BaseObjectClass
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_BaseObjectClass == Helper.INVALIDID && BaseObjectClass != null)
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
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_ObjectClass_ObjectClass", "B_ObjectClass")]
        [XmlIgnore()]
        public EntityCollection<Kistl.App.Base.ObjectClass> SubClasses
        {
            get
            {
                EntityCollection<Kistl.App.Base.ObjectClass> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Base.ObjectClass>("Model.FK_ObjectClass_ObjectClass", "B_ObjectClass");
                if (!c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Method_ObjectClass", "B_Method")]
        [XmlIgnore()]
        public EntityCollection<Kistl.App.Base.Method> Methods
        {
            get
            {
                EntityCollection<Kistl.App.Base.Method> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Base.Method>("Model.FK_Method_ObjectClass", "B_Method");
                if (!c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_ObjectClass_Module", "A_Module")]
        [XmlIgnore()]
        public Kistl.App.Base.Module Module
        {
            get
            {
                EntityReference<Kistl.App.Base.Module> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module>("Model.FK_ObjectClass_Module", "A_Module");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Module> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module>("Model.FK_ObjectClass_Module", "A_Module");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public int fk_Module
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_Module == Helper.INVALIDID && Module != null)
                {
                    _fk_Module = Module.ID;
                }
                return _fk_Module;
            }
            set
            {
                _fk_Module = value;
            }
        }
        
        public event ToStringHandler<ObjectClass> OnToString_ObjectClass;
        
        public event ObjectEventHandler<ObjectClass> OnPreSave_ObjectClass;
        
        public event ObjectEventHandler<ObjectClass> OnPostSave_ObjectClass;
        
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
        
        public override object Clone()
        {
            ObjectClass obj = new ObjectClass();
            CopyTo(obj);
            return obj;
        }
        
        public void CopyTo(ObjectClass obj)
        {
            base.CopyTo(obj);
            obj.ClassName = this.ClassName;
            obj.TableName = this.TableName;
            obj.fk_BaseObjectClass = this.fk_BaseObjectClass;
            obj.fk_Module = this.fk_Module;
        }
    }
    
    public sealed class ObjectClassServer : ServerObject<ObjectClass, XMLObjectCollection, XMLObject>
    {
    }
}
