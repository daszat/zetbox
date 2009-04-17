using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Kistl.API
{
    public class XmlStreamer
    {
        #region bool
        public static void ToStream(bool val, XmlWriter xml, string name, string ns)
        {
            xml.WriteElementString(name, ns, XmlConvert.ToString(val));
        }
        public static void ToStream(bool? val, XmlWriter xml, string name, string ns)
        {
            if (!val.HasValue) return;
            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value));
        }
        #endregion

        #region DateTime
        public static void ToStream(DateTime val, XmlWriter xml, string name, string ns)
        {
            xml.WriteElementString(name, ns, XmlConvert.ToString(val, XmlDateTimeSerializationMode.Utc));
        }
        public static void ToStream(DateTime? val, XmlWriter xml, string name, string ns)
        {
            if (!val.HasValue) return;
            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value, XmlDateTimeSerializationMode.Utc));
        }
        #endregion

        #region double
        public static void ToStream(double val, XmlWriter xml, string name, string ns)
        {
            xml.WriteElementString(name, ns, XmlConvert.ToString(val));
        }
        public static void ToStream(double? val, XmlWriter xml, string name, string ns)
        {
            if (!val.HasValue) return;
            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value));
        }
        #endregion

        #region float
        public static void ToStream(float val, XmlWriter xml, string name, string ns)
        {
            xml.WriteElementString(name, ns, XmlConvert.ToString(val));
        }
        public static void ToStream(float? val, XmlWriter xml, string name, string ns)
        {
            if (!val.HasValue) return;
            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value));
        }
        #endregion

        #region int
        public static void ToStream(int val, XmlWriter xml, string name, string ns)
        {
            xml.WriteElementString(name, ns, XmlConvert.ToString(val));
        }
        public static void ToStream(int? val, XmlWriter xml, string name, string ns)
        {
            if (!val.HasValue) return;
            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value));
        }
        #endregion

        #region string
        public static void ToStream(string val, XmlWriter xml, string name, string ns)
        {
            if (val == null) return;
            xml.WriteElementString(name, ns, val);
        }
        #endregion

        #region Collection Entries
        public static void ToStreamCollectionEntries<T>(IEnumerable<T> val, XmlWriter xml, string name, string ns)
            where T : IStreamable
        {
            xml.WriteStartElement(name, ns);
            foreach (IStreamable obj in val)
            {
                xml.WriteStartElement("CollectionEntry");
                obj.ToStream(xml, new string[] { "*" });
                xml.WriteEndElement();
            }
            xml.WriteEndElement();
        }

        public static void FromStreamCollectionEntries<T>(ICollection<T> val, XmlReader xml, string name, string ns)
            where T : IStreamable, new()
        {
        }
        #endregion
    }
}
