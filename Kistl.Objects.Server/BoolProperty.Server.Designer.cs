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
    using Kistl.API.Server;
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="BoolProperty")]
    public class BoolProperty : Kistl.App.Base.ValueTypeProperty, ICloneable
    {
        
        public event ToStringHandler<BoolProperty> OnToString_BoolProperty;
        
        public event ObjectEventHandler<BoolProperty> OnPreSave_BoolProperty;
        
        public event ObjectEventHandler<BoolProperty> OnPostSave_BoolProperty;
        
        public event GetDataType_Handler<BoolProperty> OnGetDataType_BoolProperty;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BoolProperty != null)
            {
                OnToString_BoolProperty(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BoolProperty != null) OnPreSave_BoolProperty(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BoolProperty != null) OnPostSave_BoolProperty(this);
        }
        
        public override object Clone()
        {
            BoolProperty obj = new BoolProperty();
            CopyTo(obj);
            return obj;
        }
        
        public void CopyTo(BoolProperty obj)
        {
            base.CopyTo(obj);
        }
        
        public override string GetDataType()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.GetDataType();
            if (OnGetDataType_BoolProperty != null)
            {
                OnGetDataType_BoolProperty(this, e);
            }
            return e.Result;
        }
    }
}
