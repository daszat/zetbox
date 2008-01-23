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
    using System.Linq;
    using System.Text;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Collections;
    using System.Xml;
    using System.Xml.Serialization;
    using Kistl.API;
    using Kistl.API.Client;
    
    
    public class Property : Kistl.App.Base.BaseProperty, ICloneable
    {
        
        private bool _IsList;
        
        private bool _IsNullable;
        
        public bool IsList
        {
            get
            {
                return _IsList;
            }
            set
            {
                _IsList = value;
            }
        }
        
        public bool IsNullable
        {
            get
            {
                return _IsNullable;
            }
            set
            {
                _IsNullable = value;
            }
        }
        
        public event ToStringHandler<Property> OnToString_Property;
        
        public event ObjectEventHandler<Property> OnPreSave_Property;
        
        public event ObjectEventHandler<Property> OnPostSave_Property;
        
        public event GetDataType_Handler<Property> OnGetDataType_Property;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Property != null)
            {
                OnToString_Property(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Property != null) OnPreSave_Property(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Property != null) OnPostSave_Property(this);
        }
        
        public override object Clone()
        {
            Property obj = new Property();
            CopyTo(obj);
            return obj;
        }
        
        public void CopyTo(Property obj)
        {
            base.CopyTo(obj);
            obj.IsList = this.IsList;
            obj.IsNullable = this.IsNullable;
        }
        
        public override string GetDataType()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.GetDataType();
            if (OnGetDataType_Property != null)
            {
                OnGetDataType_Property(this, e);
            }
            return e.Result;
        }
    }
}
