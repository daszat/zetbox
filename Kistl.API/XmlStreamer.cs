using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

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

        #region decimal
        public static void ToStream(decimal val, XmlWriter xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteElementString(name, ns, XmlConvert.ToString(val));
        }
        public static void ToStream(decimal? val, XmlWriter xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (!val.HasValue) return;
            xml.WriteElementString(name, ns, XmlConvert.ToString(val.Value));
        }

        public static void FromStream(ref decimal val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsDecimal();
            }
        }
        public static void FromStream(ref decimal? val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsDecimal();
            }
        }
        #endregion

        #region string
        public static void ToStream(string val, XmlWriter xml, string name, string ns)
        {
            if (val == null) return;
            if (xml == null) { throw new ArgumentNullException("xml"); }

            // Replace illegal chars
            // TODO: what is the right way to to this?
            // http://stackoverflow.com/questions/1927402/why-net-xml-api-doesnt-protect-me-from-null-character
            xml.WriteElementString(name, ns, val.Replace("\0", "\\0"));
        }

        public static void FromStream(ref string val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                val = xml.ReadElementContentAsString();
            }
        }

        public static void FromStreamConverter(Action<string> conv, XmlReader xml, string name, string ns)
        {
            if (conv == null) { throw new ArgumentNullException("conv"); }
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                string val = xml.ReadElementContentAsString();
                conv(val);
            }
        }
        #endregion

        #region Collection Entries
        private delegate void CallXmlFunction<T, X>(T obj, X xml);

        public static void ToStreamCollectionEntries<T>(IEnumerable<T> val, XmlWriter xml, string name, string ns)
            where T : IStreamable
        {
            WriteCollectionEntries<T>(val, xml, name, ns, (obj, x) => obj.ToStream(x));
        }

        public static void FromStreamCollectionEntries<T>(ICollection<T> val, XmlReader xml, string name, string ns)
            where T : IStreamable, new()
        {
            // collection entries do not have sub-lists
            ReadCollectionEntries<T>(val, xml, name, ns, (obj, x) => obj.FromStream(x));
        }

        public static void ExportCollectionEntries<T>(IEnumerable<T> val, XmlWriter xml, string name, string ns)
            where T : IExportableValueCollectionEntryInternal
        {
            WriteCollectionEntries<T>(val, xml, name, ns, (obj, x) => obj.Export(x, new string[] { "*" }));
        }

        public static void MergeImportCollectionEntries<T>(ICollection<T> val, XmlReader xml, string name, string ns)
            where T : IExportableValueCollectionEntryInternal, new()
        {
            ReadCollectionEntries<T>(val, xml, name, ns, (obj, x) => obj.MergeImport(x));
        }

        private static void WriteCollectionEntries<T>(IEnumerable<T> val, XmlWriter xml, string name, string ns, CallXmlFunction<T, XmlWriter> func)
        {
            if (val == null) { throw new ArgumentNullException("val"); }
            if (xml == null) { throw new ArgumentNullException("xml"); }

            xml.WriteStartElement(name, ns);
            foreach (T obj in val)
            {
                xml.WriteStartElement("CollectionEntry");
                func(obj, xml);
                xml.WriteEndElement();
            }
            xml.WriteEndElement();
        }

        private static void ReadCollectionEntries<T>(ICollection<T> val, XmlReader xml, string name, string ns, CallXmlFunction<T, XmlReader> func)
            where T : new()
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
                                        func(obj, children);
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

        #region ICompoundObject
        public static void ToStream(ICompoundObject val, XmlWriter xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }

            if (val != null)
            {
                xml.WriteStartElement(name, ns);
                val.ToStream(xml);
                xml.WriteEndElement();
            }
        }

        public static void FromStream(ICompoundObject val, XmlReader xml, string name, string ns)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            if (val == null) { throw new ArgumentNullException("val"); }

            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                using (var entries = xml.ReadSubtree())
                {
                    while (entries.Read())
                    {
                        // compound objects do not have sub-lists
                        val.FromStream(xml);
                    }
                }
            }
        }
        #endregion
    }
}
