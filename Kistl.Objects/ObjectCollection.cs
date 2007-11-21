using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kistl.API
{
    [Serializable]
    [XmlRoot("ObjectCollection")]
    public class ObjectCollection
    {
        [XmlArrayItem(Type = typeof(Kistl.App.Base.ObjectClass), ElementName = "ObjectClass")]
        [XmlArrayItem(Type = typeof(Kistl.App.Base.BaseProperty), ElementName = "BaseProperty")]
        [XmlArrayItem(Type = typeof(Kistl.App.Base.Property), ElementName = "Property")]
        public List<BaseDataObject> Objects = new List<BaseDataObject>();

        public List<T> ToList<T>() where T : IDataObject
        {
            return new List<T>(Objects.OfType<T>());
        }
    }
}
