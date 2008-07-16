//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
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
    
    
    public class EnumerationEntry : BaseClientDataObject, ICloneable
    {
        
        private int _ID;
        
        private System.Nullable<int> _fk_Enumeration = null;
        
        private string _EnumerationEntryName;
        
        private int _EnumValue;
        
        public EnumerationEntry()
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
        public Kistl.App.Base.Enumeration Enumeration
        {
            get
            {
                if (fk_Enumeration == null) return null;
                return Context.Find<Kistl.App.Base.Enumeration>(fk_Enumeration.Value);
            }
            set
            {
                fk_Enumeration = value != null ? (int?)value.ID : null;
            }
        }
        
        public System.Nullable<int> fk_Enumeration
        {
            get
            {
                return _fk_Enumeration;
            }
            set
            {
                if (fk_Enumeration != value)
                {
                    NotifyPropertyChanging("Enumeration"); 
                    _fk_Enumeration = value;
                    NotifyPropertyChanged("Enumeration");
                }
            }
        }
        
        public string EnumerationEntryName
        {
            get
            {
                return _EnumerationEntryName;
            }
            set
            {
                NotifyPropertyChanging("EnumerationEntryName"); 
                _EnumerationEntryName = value; 
                NotifyPropertyChanged("EnumerationEntryName");;
            }
        }
        
        public int EnumValue
        {
            get
            {
                return _EnumValue;
            }
            set
            {
                NotifyPropertyChanging("EnumValue"); 
                _EnumValue = value; 
                NotifyPropertyChanged("EnumValue");;
            }
        }
        
        public event ToStringHandler<EnumerationEntry> OnToString_EnumerationEntry;
        
        public event ObjectEventHandler<EnumerationEntry> OnPreSave_EnumerationEntry;
        
        public event ObjectEventHandler<EnumerationEntry> OnPostSave_EnumerationEntry;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_EnumerationEntry != null)
            {
                OnToString_EnumerationEntry(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_EnumerationEntry != null) OnPreSave_EnumerationEntry(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_EnumerationEntry != null) OnPostSave_EnumerationEntry(this);
        }
        
        public override object Clone()
        {
            EnumerationEntry obj = new EnumerationEntry();
            CopyTo(obj);
            return obj;
        }
        
        public override void CopyTo(Kistl.API.IDataObject obj)
        {
            base.CopyTo(obj);
            ((EnumerationEntry)obj)._fk_Enumeration = this._fk_Enumeration;
            ((EnumerationEntry)obj)._EnumerationEntryName = this._EnumerationEntryName;
            ((EnumerationEntry)obj)._EnumValue = this._EnumValue;
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this.fk_Enumeration, sw);
            BinarySerializer.ToBinary(this._EnumerationEntryName, sw);
            BinarySerializer.ToBinary(this._EnumValue, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._fk_Enumeration, sr);
            BinarySerializer.FromBinary(out this._EnumerationEntryName, sr);
            BinarySerializer.FromBinary(out this._EnumValue, sr);
        }
    }
}
