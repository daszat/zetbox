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
    
    
    public class ValueTypeProperty : Kistl.App.Base.Property, ICloneable
    {
        
        public ValueTypeProperty()
        {
        }
        
        public event ToStringHandler<ValueTypeProperty> OnToString_ValueTypeProperty;
        
        public event ObjectEventHandler<ValueTypeProperty> OnPreSave_ValueTypeProperty;
        
        public event ObjectEventHandler<ValueTypeProperty> OnPostSave_ValueTypeProperty;
        
        public event GetDataType_Handler<ValueTypeProperty> OnGetDataType_ValueTypeProperty;
        
        public event GetGUIRepresentation_Handler<ValueTypeProperty> OnGetGUIRepresentation_ValueTypeProperty;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ValueTypeProperty != null)
            {
                OnToString_ValueTypeProperty(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ValueTypeProperty != null) OnPreSave_ValueTypeProperty(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ValueTypeProperty != null) OnPostSave_ValueTypeProperty(this);
        }
        
        public override object Clone()
        {
            ValueTypeProperty obj = new ValueTypeProperty();
            CopyTo(obj);
            return obj;
        }
        
        public override void CopyTo(Kistl.API.IDataObject obj)
        {
            base.CopyTo(obj);
        }
        
        public override void AttachToContext(KistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        
        public override string GetDataType()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.GetDataType();
            if (OnGetDataType_ValueTypeProperty != null)
            {
                OnGetDataType_ValueTypeProperty(this, e);
            }
            return e.Result;
        }
        
        public override string GetGUIRepresentation()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.GetGUIRepresentation();
            if (OnGetGUIRepresentation_ValueTypeProperty != null)
            {
                OnGetGUIRepresentation_ValueTypeProperty(this, e);
            }
            return e.Result;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
        }
        
        public override void FromStream(Kistl.API.IKistlContext ctx, System.IO.BinaryReader sr)
        {
            base.FromStream(ctx, sr);
        }
    }
}
