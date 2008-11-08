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
    using Kistl.API.Client;
    
    
    public class BaseProperty__Implementation__ : BaseClientDataObject, BaseProperty
    {
        
        private System.Nullable<int> _fk_ObjectClass = null;
        
        private string _PropertyName;
        
        private string _AltText;
        
        private System.Nullable<int> _fk_Module = null;
        
        private BackReferenceCollection<Kistl.App.Base.Constraint> _Constraints;
        
        private string _Description;
        
        public BaseProperty__Implementation__()
        {
        }
        
        [XmlIgnore()]
        public Kistl.App.Base.DataType ObjectClass
        {
            get
            {
                if (fk_ObjectClass == null) return null;
                return Context.Find<Kistl.App.Base.DataType>(fk_ObjectClass.Value);
            }
            set
            {
                fk_ObjectClass = value != null ? (int?)value.ID : null;
            }
        }
        
        public System.Nullable<int> fk_ObjectClass
        {
            get
            {
                return _fk_ObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (fk_ObjectClass != value)
                {
                    NotifyPropertyChanging("ObjectClass"); 
                    _fk_ObjectClass = value;
                    NotifyPropertyChanged("ObjectClass");;
                }
            }
        }
        
        public string PropertyName
        {
            get
            {
                return _PropertyName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (PropertyName != value)
                {
                    NotifyPropertyChanging("PropertyName"); 
                    _PropertyName = value;
                    NotifyPropertyChanged("PropertyName");;
                }
            }
        }
        
        public string AltText
        {
            get
            {
                return _AltText;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (AltText != value)
                {
                    NotifyPropertyChanging("AltText"); 
                    _AltText = value;
                    NotifyPropertyChanged("AltText");;
                }
            }
        }
        
        [XmlIgnore()]
        public Kistl.App.Base.Module Module
        {
            get
            {
                if (fk_Module == null) return null;
                return Context.Find<Kistl.App.Base.Module>(fk_Module.Value);
            }
            set
            {
                fk_Module = value != null ? (int?)value.ID : null;
            }
        }
        
        public System.Nullable<int> fk_Module
        {
            get
            {
                return _fk_Module;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (fk_Module != value)
                {
                    NotifyPropertyChanging("Module"); 
                    _fk_Module = value;
                    NotifyPropertyChanged("Module");;
                }
            }
        }
        
        [XmlIgnore()]
        public ICollection<Kistl.App.Base.Constraint> Constraints
        {
            get
            {
                if (_Constraints == null)
                {
                    List<Kistl.App.Base.Constraint> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.Constraint>(this, "Constraints");
                    else
                        serverList = new List<Kistl.App.Base.Constraint>();

                    _Constraints = new BackReferenceCollection<Kistl.App.Base.Constraint>(
                         "ConstrainedProperty", this, serverList);
                }
                return _Constraints;
            }
        }
        
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (Description != value)
                {
                    NotifyPropertyChanging("Description"); 
                    _Description = value;
                    NotifyPropertyChanged("Description");;
                }
            }
        }
        
        public event ToStringHandler<BaseProperty> OnToString_BaseProperty;
        
        public event ObjectEventHandler<BaseProperty> OnPreSave_BaseProperty;
        
        public event ObjectEventHandler<BaseProperty> OnPostSave_BaseProperty;
        
        public event GetPropertyTypeString_Handler<BaseProperty> OnGetPropertyTypeString_BaseProperty;
        
        public event GetGUIRepresentation_Handler<BaseProperty> OnGetGUIRepresentation_BaseProperty;
        
        public event GetPropertyType_Handler<BaseProperty> OnGetPropertyType_BaseProperty;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BaseProperty != null)
            {
                OnToString_BaseProperty(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BaseProperty != null) OnPreSave_BaseProperty(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BaseProperty != null) OnPostSave_BaseProperty(this);
        }
        
        public override void ApplyChanges(Kistl.API.IDataObject obj)
        {
            base.ApplyChanges(obj);
            ((BaseProperty__Implementation__)obj).fk_ObjectClass = this.fk_ObjectClass;
            ((BaseProperty__Implementation__)obj).PropertyName = this.PropertyName;
            ((BaseProperty__Implementation__)obj).AltText = this.AltText;
            ((BaseProperty__Implementation__)obj).fk_Module = this.fk_Module;
            if(this._Constraints != null) this._Constraints.ApplyChanges(((BaseProperty__Implementation__)obj)._Constraints); else ((BaseProperty__Implementation__)obj)._Constraints = null; ((BaseProperty__Implementation__)obj).NotifyPropertyChanged("Constraints");
            ((BaseProperty__Implementation__)obj).Description = this.Description;
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            if(_Constraints != null) _Constraints.AttachToContext(ctx);
        }
        
        protected override string GetPropertyError(string prop)
        {
            switch(prop)
            {
                case "ObjectClass":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(8).Constraints
                            .Where(c => !c.IsValid(this, this.ObjectClass))
                            .Select(c => c.GetErrorText(this, this.ObjectClass))
                            .ToArray());
                case "PropertyName":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(9).Constraints
                            .Where(c => !c.IsValid(this, this.PropertyName))
                            .Select(c => c.GetErrorText(this, this.PropertyName))
                            .ToArray());
                case "AltText":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(41).Constraints
                            .Where(c => !c.IsValid(this, this.AltText))
                            .Select(c => c.GetErrorText(this, this.AltText))
                            .ToArray());
                case "Module":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(72).Constraints
                            .Where(c => !c.IsValid(this, this.Module))
                            .Select(c => c.GetErrorText(this, this.Module))
                            .ToArray());
                case "Constraints":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(170).Constraints
                            .Where(c => !c.IsValid(this, this.Constraints))
                            .Select(c => c.GetErrorText(this, this.Constraints))
                            .ToArray());
                case "Description":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(176).Constraints
                            .Where(c => !c.IsValid(this, this.Description))
                            .Select(c => c.GetErrorText(this, this.Description))
                            .ToArray());
            }
            return base.GetPropertyError(prop);
        }
        
        public virtual string GetPropertyTypeString()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            if (OnGetPropertyTypeString_BaseProperty != null)
            {
                OnGetPropertyTypeString_BaseProperty(this, e);
            };
            return e.Result;
        }
        
        public virtual string GetGUIRepresentation()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            if (OnGetGUIRepresentation_BaseProperty != null)
            {
                OnGetGUIRepresentation_BaseProperty(this, e);
            };
            return e.Result;
        }
        
        public virtual System.Type GetPropertyType()
        {
            MethodReturnEventArgs<System.Type> e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_BaseProperty != null)
            {
                OnGetPropertyType_BaseProperty(this, e);
            };
            return e.Result;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this.fk_ObjectClass, sw);
            BinarySerializer.ToBinary(this._PropertyName, sw);
            BinarySerializer.ToBinary(this._AltText, sw);
            BinarySerializer.ToBinary(this.fk_Module, sw);
            BinarySerializer.ToBinary(this._Description, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._fk_ObjectClass, sr);
            BinarySerializer.FromBinary(out this._PropertyName, sr);
            BinarySerializer.FromBinary(out this._AltText, sr);
            BinarySerializer.FromBinary(out this._fk_Module, sr);
            this._Constraints = new BackReferenceCollection<Kistl.App.Base.Constraint>("ConstrainedProperty", this); BinarySerializer.FromBinary(this._Constraints, sr);
            BinarySerializer.FromBinary(out this._Description, sr);
        }
        
        public delegate void GetPropertyTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> e);
        
        public delegate void GetGUIRepresentation_Handler<T>(T obj, MethodReturnEventArgs<string> e);
        
        public delegate void GetPropertyType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> e);
    }
}
