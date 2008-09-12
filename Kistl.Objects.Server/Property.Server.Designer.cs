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
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Property")]
    public class Property__Implementation__ : Kistl.App.Base.BaseProperty__Implementation__, Property
    {
        
        private bool _IsList;
        
        private bool _IsNullable;
        
        public Property__Implementation__()
        {
        }
        
        [EdmScalarPropertyAttribute()]
        public bool IsList
        {
            get
            {
                return _IsList;
            }
            set
            {
                if (IsList != value)
                {
                    NotifyPropertyChanging("IsList"); 
                    _IsList = value;
                    NotifyPropertyChanged("IsList");;
                }
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public bool IsNullable
        {
            get
            {
                return _IsNullable;
            }
            set
            {
                if (IsNullable != value)
                {
                    NotifyPropertyChanging("IsNullable"); 
                    _IsNullable = value;
                    NotifyPropertyChanged("IsNullable");;
                }
            }
        }
        
        public event ToStringHandler<Property> OnToString_Property;
        
        public event ObjectEventHandler<Property> OnPreSave_Property;
        
        public event ObjectEventHandler<Property> OnPostSave_Property;
        
        public event GetPropertyTypeString_Handler<Property> OnGetPropertyTypeString_Property;
        
        public event GetGUIRepresentation_Handler<Property> OnGetGUIRepresentation_Property;
        
        public event GetPropertyType_Handler<Property> OnGetPropertyType_Property;
        
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
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        
        public override string GetPropertyTypeString()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetPropertyTypeString();
            if (OnGetPropertyTypeString_Property != null)
            {
                OnGetPropertyTypeString_Property(this, e);
            };
            return e.Result;
        }
        
        public override string GetGUIRepresentation()
        {
            MethodReturnEventArgs<System.String> e = new MethodReturnEventArgs<System.String>();
            e.Result = base.GetGUIRepresentation();
            if (OnGetGUIRepresentation_Property != null)
            {
                OnGetGUIRepresentation_Property(this, e);
            };
            return e.Result;
        }
        
        public override System.Type GetPropertyType()
        {
            MethodReturnEventArgs<System.Type> e = new MethodReturnEventArgs<System.Type>();
            e.Result = base.GetPropertyType();
            if (OnGetPropertyType_Property != null)
            {
                OnGetPropertyType_Property(this, e);
            };
            return e.Result;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._IsList, sw);
            BinarySerializer.ToBinary(this._IsNullable, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._IsList, sr);
            BinarySerializer.FromBinary(out this._IsNullable, sr);
        }
    }
}
