//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1378
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
    
    
    public sealed class ObjectClassClient : ClientObject<ObjectClass>
    {
        
        // Autogeneriert, um die gebundenen Listen zu bekommen
        public List<Kistl.App.Base.BaseProperty> GetListOfProperties(int ID)
        {
            return Proxy.Service.GetListOf(Type, ID, "Properties").FromXmlString<XMLObjectCollection>().ToList<Kistl.App.Base.BaseProperty>();
        }
        
        // Autogeneriert, um die gebundenen Listen zu bekommen
        public List<Kistl.App.Base.ObjectClass> GetListOfSubClasses(int ID)
        {
            return Proxy.Service.GetListOf(Type, ID, "SubClasses").FromXmlString<XMLObjectCollection>().ToList<Kistl.App.Base.ObjectClass>();
        }
    }
}
