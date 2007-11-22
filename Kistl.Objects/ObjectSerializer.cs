//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1378
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kistl.API
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
    
    
    [Serializable()]
    [XmlRoot(ElementName="ObjectCollection")]
    public sealed class XMLObjectCollection
    {
        
        [XmlArrayItem(Type=typeof(Kistl.App.Base.ObjectClass), ElementName="ObjectClass")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Projekt), ElementName="Projekt")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Task), ElementName="Task")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.BaseProperty), ElementName="BaseProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Mitarbeiter), ElementName="Mitarbeiter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Property), ElementName="Property")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.ValueTypeProperty), ElementName="ValueTypeProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.StringProperty), ElementName="StringProperty")]
        public List<BaseDataObject> Objects = new List<BaseDataObject>();
        
        public List<T> ToList<T>()
            where T : IDataObject
        {
            return new List<T>(Objects.OfType<T>());
        }
    }
    
    [Serializable()]
    [XmlRoot(ElementName="Object")]
    public sealed class XMLObject
    {
        
        [XmlElement(Type=typeof(Kistl.App.Base.ObjectClass), ElementName="ObjectClass")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Projekt), ElementName="Projekt")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Task), ElementName="Task")]
        [XmlElement(Type=typeof(Kistl.App.Base.BaseProperty), ElementName="BaseProperty")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Mitarbeiter), ElementName="Mitarbeiter")]
        [XmlElement(Type=typeof(Kistl.App.Base.Property), ElementName="Property")]
        [XmlElement(Type=typeof(Kistl.App.Base.ValueTypeProperty), ElementName="ValueTypeProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.StringProperty), ElementName="StringProperty")]
        public BaseDataObject Object;
    }
}
