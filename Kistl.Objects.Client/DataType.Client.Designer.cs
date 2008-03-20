//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
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
    using Kistl.API.Client;
    
    
    public class DataType : BaseClientDataObject, ICloneable
    {
        
        private int _ID = Helper.INVALIDID;
        
        private int _fk_Module = Helper.INVALIDID;
        
        private string _ClassName;
        
        private int _fk_DefaultIcon = Helper.INVALIDID;
        
        public DataType()
        {
        }
        
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
        public Kistl.App.Base.Module Module
        {
            get
            {
                return Context.GetQuery<Kistl.App.Base.Module>().Single(o => o.ID == fk_Module);
            }
            set
            {
                NotifyPropertyChanging("Module"); 
                _fk_Module = value.ID;
                NotifyPropertyChanged("Module"); ;
            }
        }
        
        public int fk_Module
        {
            get
            {
                return _fk_Module;
            }
            set
            {
                NotifyPropertyChanging("Module"); 
                _fk_Module = value;
                NotifyPropertyChanged("Module"); ;
            }
        }
        
        public string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                NotifyPropertyChanging("ClassName"); 
                _ClassName = value; 
                NotifyPropertyChanged("ClassName");;
            }
        }
        
        [XmlIgnore()]
        public Kistl.App.GUI.Icon DefaultIcon
        {
            get
            {
                return Context.GetQuery<Kistl.App.GUI.Icon>().Single(o => o.ID == fk_DefaultIcon);
            }
            set
            {
                NotifyPropertyChanging("DefaultIcon"); 
                _fk_DefaultIcon = value.ID;
                NotifyPropertyChanged("DefaultIcon"); ;
            }
        }
        
        public int fk_DefaultIcon
        {
            get
            {
                return _fk_DefaultIcon;
            }
            set
            {
                NotifyPropertyChanging("DefaultIcon"); 
                _fk_DefaultIcon = value;
                NotifyPropertyChanged("DefaultIcon"); ;
            }
        }
        
        public event ToStringHandler<DataType> OnToString_DataType;
        
        public event ObjectEventHandler<DataType> OnPreSave_DataType;
        
        public event ObjectEventHandler<DataType> OnPostSave_DataType;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DataType != null)
            {
                OnToString_DataType(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_DataType != null) OnPreSave_DataType(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_DataType != null) OnPostSave_DataType(this);
        }
        
        public override object Clone()
        {
            DataType obj = new DataType();
            CopyTo(obj);
            return obj;
        }
        
        public override void CopyTo(Kistl.API.IDataObject obj)
        {
            base.CopyTo(obj);
            ((DataType)obj)._fk_Module = this._fk_Module;
            ((DataType)obj)._ClassName = this._ClassName;
            ((DataType)obj)._fk_DefaultIcon = this._fk_DefaultIcon;
        }
        
        public override void AttachToContext(KistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this.fk_Module, sw);
            BinarySerializer.ToBinary(this._ClassName, sw);
            BinarySerializer.ToBinary(this.fk_DefaultIcon, sw);
        }
        
        public override void FromStream(Kistl.API.IKistlContext ctx, System.IO.BinaryReader sr)
        {
            base.FromStream(ctx, sr);
            BinarySerializer.FromBinary(out this._fk_Module, sr);
            BinarySerializer.FromBinary(out this._ClassName, sr);
            BinarySerializer.FromBinary(out this._fk_DefaultIcon, sr);
        }
    }
}
