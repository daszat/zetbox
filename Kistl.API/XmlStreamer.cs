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
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val));
        }

        public static void ToStream(bool? val, XmlWriter xml, string name, string ns)
        {
            if (!val.HasValue) return;
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value));
        }

        public static void FromStream(ref bool val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsBoolean();
            }
        }
        public static void FromStream(ref bool? val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsBoolean();
            }
        }
        #endregion

        #region DateTime
        public static void ToStream(DateTime val, XmlWriter xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val, XmlDateTimeSerializationMode.Utc));
        }
        public static void ToStream(DateTime? val, XmlWriter xml, string name, string ns)
        {
            if (!val.HasValue) return;
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value, XmlDateTimeSerializationMode.Utc));
        }

        public static void FromStream(ref DateTime val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsDateTime();
            }
        }
        public static void FromStream(ref DateTime? val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsDateTime();
            }
        }
        #endregion

        #region Guid
        public static void ToStream(Guid val, XmlWriter xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val));
        }
        public static void ToStream(Guid? val, XmlWriter xml, string name, string ns)
        {
            if (!val.HasValue) return;
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value));
        }

        public static void FromStream(ref Guid val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = new Guid(xml.ReadElementContentAsString());
            }
        }
        public static void FromStream(ref Guid? val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = new Guid(xml.ReadElementContentAsString());
            }
        }
        #endregion

        #region double
        public static void ToStream(double val, XmlWriter xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val));
        }
        public static void ToStream(double? val, XmlWriter xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (!val.HasValue) return;
            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value));
        }

        public static void FromStream(ref double val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsDouble();
            }
        }
        public static void FromStream(ref double? val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsDouble();
            }
        }
        #endregion

        #region float
        public static void ToStream(float val, XmlWriter xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val));
        }
        public static void ToStream(float? val, XmlWriter xml, string name, string ns)
        {
            if (!val.HasValue) return;
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value));
        }

        public static void FromStream(ref float val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsFloat();
            }
        }
        public static void FromStream(ref float? val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsFloat();
            }
        }
        #endregion

        #region int
        public static void ToStream(int val, XmlWriter xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val));
        }
        public static void ToStream(int? val, XmlWriter xml, string name, string ns)
        {
            if (!val.HasValue) return;
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value));
        }

        public static void FromStream(ref int val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsInt();
            }
        }
        public static void FromStream(ref int? val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsInt();
            }
        }
        public static void FromStreamConverter(Action<int> conv, XmlReader xml, string name, string ns)
        {
            if (conv == null) { throw new ArgumentNullException("conv"); }
            if (xml == null) { throw new ArgumentNullException("xml"); }
            
            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                int val = xml.ReadElementContentAsInt();
                conv(val);
            }
        }
        #endregion

        #region string
        public static void ToStream(string val, XmlWriter xml, string name, string ns)
        {
            if (val == null) return;
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, val);
        }

        public static void FromStream(ref string val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsString();
            }
        }
        #endregion

        #region Collection Entries
        public static void ToStreamCollectionEntries<T>(IEnumerable<T> val, XmlWriter xml, string name, string ns)
            where T : IStreamable
        {
            if (val == null) { throw new ArgumentNullException("val"); }
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteStartElement(name, ns);
            foreach (IStreamable obj in val)
            {
                xml.WriteStartElement("CollectionEntry");
                obj.ToStream(xml);
                xml.WriteEndElement();
            }
            xml.WriteEndElement();
        }

        public static void FromStreamCollectionEntries<T>(ICollection<T> val, XmlReader xml, string name, string ns)
            where T : IStreamable, new()
        {
            if (val == null) { throw new ArgumentNullException("val"); }
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                using (var entries = xml.ReadSubtree())
                {
                    while (entries.Read())
                    {
                        if (entries.NodeType == XmlNodeType.Element && entries.LocalName == "CollectionEntry")
                        {
                            var obj = new T();
                            using (var children = xml.ReadSubtree())
                            {
                                while (children.Read())
                                {
                                    if (children.NodeType == XmlNodeType.Element)
                                    {
                                        obj.FromStream(children);
                                    }
                                }
                            }
                            val.Add(obj);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
