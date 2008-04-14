//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_MethodInvocation_Module", "A_Module", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Module), "B_MethodInvocation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_MethodInvocation_Method", "A_Method", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Method), "B_MethodInvocation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_MethodInvocation_Assembly", "A_Assembly", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.Assembly), "B_MethodInvocation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_MethodInvocation_DataType", "A_DataType", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Base.DataType), "B_MethodInvocation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.MethodInvocation))]

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
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="MethodInvocation")]
    public class MethodInvocation : BaseServerDataObject, ICloneable
    {
        
        private int _ID = Helper.INVALIDID;
        
        private int _fk_Module = Helper.INVALIDID;
        
        private int _fk_Method = Helper.INVALIDID;
        
        private int _fk_Assembly = Helper.INVALIDID;
        
        private string _FullTypeName;
        
        private string _MemberName;
        
        private int _fk_InvokeOnObjectClass = Helper.INVALIDID;
        
        public MethodInvocation()
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
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_MethodInvocation_Module", "A_Module")]
        public Kistl.App.Base.Module Module
        {
            get
            {
                EntityReference<Kistl.App.Base.Module> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module>("Model.FK_MethodInvocation_Module", "A_Module");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Module> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module>("Model.FK_MethodInvocation_Module", "A_Module");
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
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_MethodInvocation_Method", "A_Method")]
        public Kistl.App.Base.Method Method
        {
            get
            {
                EntityReference<Kistl.App.Base.Method> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Method>("Model.FK_MethodInvocation_Method", "A_Method");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Method> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Method>("Model.FK_MethodInvocation_Method", "A_Method");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public int fk_Method
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_Method == Helper.INVALIDID && Method != null)
                {
                    _fk_Method = Method.ID;
                }
                return _fk_Method;
            }
            set
            {
                _fk_Method = value;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_MethodInvocation_Assembly", "A_Assembly")]
        public Kistl.App.Base.Assembly Assembly
        {
            get
            {
                EntityReference<Kistl.App.Base.Assembly> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly>("Model.FK_MethodInvocation_Assembly", "A_Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Assembly> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly>("Model.FK_MethodInvocation_Assembly", "A_Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public int fk_Assembly
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_Assembly == Helper.INVALIDID && Assembly != null)
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
        
        [EdmScalarPropertyAttribute()]
        public string FullTypeName
        {
            get
            {
                return _FullTypeName;
            }
            set
            {
                NotifyPropertyChanging("FullTypeName"); 
                _FullTypeName = value; 
                NotifyPropertyChanged("FullTypeName");;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string MemberName
        {
            get
            {
                return _MemberName;
            }
            set
            {
                NotifyPropertyChanging("MemberName"); 
                _MemberName = value; 
                NotifyPropertyChanged("MemberName");;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_MethodInvocation_DataType", "A_DataType")]
        public Kistl.App.Base.DataType InvokeOnObjectClass
        {
            get
            {
                EntityReference<Kistl.App.Base.DataType> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType>("Model.FK_MethodInvocation_DataType", "A_DataType");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.DataType> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType>("Model.FK_MethodInvocation_DataType", "A_DataType");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public int fk_InvokeOnObjectClass
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_InvokeOnObjectClass == Helper.INVALIDID && InvokeOnObjectClass != null)
                {
                    _fk_InvokeOnObjectClass = InvokeOnObjectClass.ID;
                }
                return _fk_InvokeOnObjectClass;
            }
            set
            {
                _fk_InvokeOnObjectClass = value;
            }
        }
        
        public event ToStringHandler<MethodInvocation> OnToString_MethodInvocation;
        
        public event ObjectEventHandler<MethodInvocation> OnPreSave_MethodInvocation;
        
        public event ObjectEventHandler<MethodInvocation> OnPostSave_MethodInvocation;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_MethodInvocation != null)
            {
                OnToString_MethodInvocation(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_MethodInvocation != null) OnPreSave_MethodInvocation(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_MethodInvocation != null) OnPostSave_MethodInvocation(this);
        }
        
        public override object Clone()
        {
            MethodInvocation obj = new MethodInvocation();
            CopyTo(obj);
            return obj;
        }
        
        public override void CopyTo(Kistl.API.IDataObject obj)
        {
            base.CopyTo(obj);
            ((MethodInvocation)obj)._fk_Module = this._fk_Module;
            ((MethodInvocation)obj)._fk_Method = this._fk_Method;
            ((MethodInvocation)obj)._fk_Assembly = this._fk_Assembly;
            ((MethodInvocation)obj)._FullTypeName = this._FullTypeName;
            ((MethodInvocation)obj)._MemberName = this._MemberName;
            ((MethodInvocation)obj)._fk_InvokeOnObjectClass = this._fk_InvokeOnObjectClass;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this.fk_Module, sw);
            BinarySerializer.ToBinary(this.fk_Method, sw);
            BinarySerializer.ToBinary(this.fk_Assembly, sw);
            BinarySerializer.ToBinary(this._FullTypeName, sw);
            BinarySerializer.ToBinary(this._MemberName, sw);
            BinarySerializer.ToBinary(this.fk_InvokeOnObjectClass, sw);
        }
        
        public override void FromStream(Kistl.API.IKistlContext ctx, System.IO.BinaryReader sr)
        {
            base.FromStream(ctx, sr);
            BinarySerializer.FromBinary(out this._fk_Module, sr);
            BinarySerializer.FromBinary(out this._fk_Method, sr);
            BinarySerializer.FromBinary(out this._fk_Assembly, sr);
            BinarySerializer.FromBinary(out this._FullTypeName, sr);
            BinarySerializer.FromBinary(out this._MemberName, sr);
            BinarySerializer.FromBinary(out this._fk_InvokeOnObjectClass, sr);
        }
    }
}
