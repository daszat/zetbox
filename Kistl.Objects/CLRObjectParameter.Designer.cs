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
    
    
    public interface CLRObjectParameter : Kistl.App.Base.BaseParameter
    {
        
        Kistl.App.Base.Assembly Assembly
        {
            get;
            set;
        }
        
        string FullTypeName
        {
            get;
            set;
        }
    }
}
