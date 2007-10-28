using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Kistl.API
{
    public static class ExtensionHelpers
    {
        public static string ToXmlString(this object obj)
        {
            XmlSerializer xml = new XmlSerializer(obj.GetType());
            StringBuilder sb = new StringBuilder();
            xml.Serialize(new System.IO.StringWriter(sb), obj);
            return sb.ToString();
        }

        public static T FromXmlString<T>(this string xmlStr) where T : new()
        {
            System.IO.StringReader sr = new System.IO.StringReader(xmlStr);
            XmlSerializer xml = new XmlSerializer(typeof(T));
            return (T)xml.Deserialize(sr);
        }
    }

    public class ToStringEventArgs
    {
        public string Result { get; set; }
    }

    public delegate void ToStringHandler<T>(T obj, ToStringEventArgs e) where T : class, IDataObject, new();
}
