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
    using Kistl.DALProvider.EF;
    using Kistl.API.Server;
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="BoolProperty")]
    public class BoolProperty__Implementation__ : Kistl.App.Base.ValueTypeProperty__Implementation__, BoolProperty
    {
        
        public BoolProperty__Implementation__()
        {
        }
        
        public event ToStringHandler<BoolProperty> OnToString_BoolProperty;
        
        public event ObjectEventHandler<BoolProperty> OnPreSave_BoolProperty;
        
        public event ObjectEventHandler<BoolProperty> OnPostSave_BoolProperty;
        
        public event GetPropertyTypeString_Handler<BoolProperty> OnGetPropertyTypeString_BoolProperty;
        
        public event GetGUIRepresentation_Handler<BoolProperty> OnGetGUIRepresentation_BoolProperty;
        
        public event GetPropertyType_Handler<BoolProperty> OnGetPropertyType_BoolProperty;
        
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
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        
        public override string GetPropertyTypeString()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetPropertyTypeString();
            if (OnGetPropertyTypeString_BoolProperty != null)
            {
                OnGetPropertyTypeString_BoolProperty(this, e);
            };
            return e.Result;
        }
        
        public override string GetGUIRepresentation()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetGUIRepresentation();
            if (OnGetGUIRepresentation_BoolProperty != null)
            {
                OnGetGUIRepresentation_BoolProperty(this, e);
            };
            return e.Result;
        }
        
        public override System.Type GetPropertyType()
        {
            MethodReturnEventArgs<System.Type> e = new MethodReturnEventArgs<System.Type>();
            e.Result = base.GetPropertyType();
            if (OnGetPropertyType_BoolProperty != null)
            {
                OnGetPropertyType_BoolProperty(this, e);
            };
            return e.Result;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
        }
    }
}
