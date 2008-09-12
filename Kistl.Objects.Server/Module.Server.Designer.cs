//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Module")]
    public class Module__Implementation__ : BaseServerDataObject, Module
    {
        
        private int _ID;
        
        private string _Namespace;
        
        private string _ModuleName;
        
        private EntityCollectionWrapper<Kistl.App.Base.DataType, Kistl.App.Base.DataType__Implementation__> DataTypesWrapper;
        
        private EntityCollectionWrapper<Kistl.App.Base.Assembly, Kistl.App.Base.Assembly__Implementation__> AssembliesWrapper;
        
        public Module__Implementation__()
        {
        }
        
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
        
        [EdmScalarPropertyAttribute()]
        public string Namespace
        {
            get
            {
                return _Namespace;
            }
            set
            {
                if (Namespace != value)
                {
                    NotifyPropertyChanging("Namespace"); 
                    _Namespace = value;
                    NotifyPropertyChanged("Namespace");;
                }
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string ModuleName
        {
            get
            {
                return _ModuleName;
            }
            set
            {
                if (ModuleName != value)
                {
                    NotifyPropertyChanging("ModuleName"); 
                    _ModuleName = value;
                    NotifyPropertyChanged("ModuleName");;
                }
            }
        }
        
        [XmlIgnore()]
        public ICollection<Kistl.App.Base.DataType> DataTypes
        {
            get
            {
                if (DataTypesWrapper == null) DataTypesWrapper = new EntityCollectionWrapper<Kistl.App.Base.DataType, Kistl.App.Base.DataType__Implementation__>(DataTypes__Implementation__);
                return DataTypesWrapper;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_DataType_Module_Module", "B_DataType")]
        public EntityCollection<Kistl.App.Base.DataType__Implementation__> DataTypes__Implementation__
        {
            get
            {
                EntityCollection<Kistl.App.Base.DataType__Implementation__> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Base.DataType__Implementation__>("Model.FK_DataType_Module_Module", "B_DataType");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [XmlIgnore()]
        public ICollection<Kistl.App.Base.Assembly> Assemblies
        {
            get
            {
                if (AssembliesWrapper == null) AssembliesWrapper = new EntityCollectionWrapper<Kistl.App.Base.Assembly, Kistl.App.Base.Assembly__Implementation__>(Assemblies__Implementation__);
                return AssembliesWrapper;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Assembly_Module_Module", "B_Assembly")]
        public EntityCollection<Kistl.App.Base.Assembly__Implementation__> Assemblies__Implementation__
        {
            get
            {
                EntityCollection<Kistl.App.Base.Assembly__Implementation__> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Base.Assembly__Implementation__>("Model.FK_Assembly_Module_Module", "B_Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        public event ToStringHandler<Module> OnToString_Module;
        
        public event ObjectEventHandler<Module> OnPreSave_Module;
        
        public event ObjectEventHandler<Module> OnPostSave_Module;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Module != null)
            {
                OnToString_Module(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Module != null) OnPreSave_Module(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Module != null) OnPostSave_Module(this);
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._Namespace, sw);
            BinarySerializer.ToBinary(this._ModuleName, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._Namespace, sr);
            BinarySerializer.FromBinary(out this._ModuleName, sr);
        }
    }
}
