//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
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
    public sealed class XMLObjectCollection : IXmlObjectCollection
    {
        
        private List<Object> _Objects = new List<Object>();
        
        [XmlArrayItem(Type=typeof(Kistl.App.Base.ObjectClass), ElementName="ObjectClass")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Projekt), ElementName="Projekt")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Task), ElementName="Task")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.BaseProperty), ElementName="BaseProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Mitarbeiter), ElementName="Mitarbeiter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Property), ElementName="Property")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.ValueTypeProperty), ElementName="ValueTypeProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.StringProperty), ElementName="StringProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Method), ElementName="Method")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.IntProperty), ElementName="IntProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.BoolProperty), ElementName="BoolProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.DoubleProperty), ElementName="DoubleProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.ObjectReferenceProperty), ElementName="ObjectReferenceProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.DateTimeProperty), ElementName="DateTimeProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.BackReferenceProperty), ElementName="BackReferenceProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.ObjectForDeletedProperties), ElementName="ObjectForDeletedProperties")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Module), ElementName="Module")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Auftrag), ElementName="Auftrag")]
        public List<Object> Objects
        {
            get
            {
                return _Objects;
            }
        }
        
        public List<T> ToList<T>()
            where T : IDataObject
        {
            return new List<T>(Objects.OfType<T>());
        }
    }
    
    [Serializable()]
    [XmlRoot(ElementName="Object")]
    public sealed class XMLObject : IXmlObject
    {
        
        private Object _Object;
        
        [XmlElement(Type=typeof(Kistl.App.Base.ObjectClass), ElementName="ObjectClass")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Projekt), ElementName="Projekt")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Task), ElementName="Task")]
        [XmlElement(Type=typeof(Kistl.App.Base.BaseProperty), ElementName="BaseProperty")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Mitarbeiter), ElementName="Mitarbeiter")]
        [XmlElement(Type=typeof(Kistl.App.Base.Property), ElementName="Property")]
        [XmlElement(Type=typeof(Kistl.App.Base.ValueTypeProperty), ElementName="ValueTypeProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.StringProperty), ElementName="StringProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.Method), ElementName="Method")]
        [XmlElement(Type=typeof(Kistl.App.Base.IntProperty), ElementName="IntProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.BoolProperty), ElementName="BoolProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.DoubleProperty), ElementName="DoubleProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.ObjectReferenceProperty), ElementName="ObjectReferenceProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.DateTimeProperty), ElementName="DateTimeProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.BackReferenceProperty), ElementName="BackReferenceProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.ObjectForDeletedProperties), ElementName="ObjectForDeletedProperties")]
        [XmlElement(Type=typeof(Kistl.App.Base.Module), ElementName="Module")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Auftrag), ElementName="Auftrag")]
        public Object Object
        {
            get
            {
                return _Object;
            }
            set
            {
                _Object = value;
            }
        }
    }
}
