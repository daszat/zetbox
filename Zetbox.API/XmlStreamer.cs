// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace Zetbox.API
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

        public static bool ReadBoolean(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsBoolean();
        }
        public static bool? ReadNullableBoolean(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsBoolean();
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

        public static DateTime ReadDateTime(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsDateTime().ToLocalTime();
        }
        public static DateTime? ReadNullableDateTime(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsDateTime().ToLocalTime();
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

        public static Guid ReadGuid(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return new Guid(xml.ReadElementContentAsString());
        }
        public static Guid? ReadNullableGuid(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return new Guid(xml.ReadElementContentAsString());
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

        public static double ReadDouble(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsDouble();
        }
        public static double? ReadNullableDouble(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsDouble();
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

        public static float ReadFloat(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsFloat();
        }
        public static float? ReadNullableFloat(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsFloat();
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
        public static int ReadInt32(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsInt();
        }
        public static int? ReadNullableInt32(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsInt();
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

        public static decimal ReadDecimal(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsDecimal();
        }
        public static decimal? ReadNullableDecimal(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsDecimal();
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

        public static string ReadString(XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            return xml.ReadElementContentAsString();
        }
        #endregion

        #region Collection Entries
        private delegate void CallXmlFunction<T, X>(T obj, X xml);

        public static void ToStreamCollectionEntries<T>(IEnumerable<T> val, XmlWriter xml, string name, string ns)
            where T : IStreamable
        {
            WriteCollectionEntries<T>(val, xml, name, ns, (obj, x) => obj.ToStream(x));
        }

        public static void FromStreamCollectionEntries<T>(IDataObject parent, ICollection<T> val, XmlReader xml, string name, string ns)
            where T : IValueCollectionEntry, IStreamable, new()
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            // collection entries do not have sub-lists
            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                ReadCollectionEntries<T>(parent, val, xml, (obj, x) => obj.FromStream(x));
            }
        }

        public static void ExportCollectionEntries<T>(IEnumerable<T> val, XmlWriter xml, string name, string ns)
            where T : IExportableValueCollectionEntryInternal
        {
            WriteCollectionEntries<T>(val, xml, name, ns, (obj, x) => obj.Export(x, new string[] { "*" }));
        }

        public static void MergeImportCollectionEntries<T>(IDataObject parent, ICollection<T> val, XmlReader xml, string name, string ns)
            where T : IValueCollectionEntry, IExportableValueCollectionEntryInternal, new()
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            if (xml.LocalName == name && xml.NamespaceURI == ns)
            {
                ReadCollectionEntries<T>(parent, val, xml, (obj, x) => obj.MergeImport(x));
            }
        }

        public static void MergeImportCollectionEntries<T>(IDataObject parent, ICollection<T> val, XmlReader xml)
            where T : IValueCollectionEntry, IExportableValueCollectionEntryInternal, new()
        {
            ReadCollectionEntries<T>(parent, val, xml, (obj, x) => obj.MergeImport(x));
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

        private static void ReadCollectionEntries<T>(IDataObject parent, ICollection<T> val, XmlReader xml, CallXmlFunction<T, XmlReader> func)
            where T : IValueCollectionEntry, new()
        {
            if (val == null) { throw new ArgumentNullException("val"); }
            if (xml == null) { throw new ArgumentNullException("xml"); }

            //// reset target collection
            //val.Clear();

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
                        if (parent == null)
                        {
                            val.Add(obj);
                        }
                        else
                        {
                            if (parent.Context != null)
                            {
                                parent.Context.Internals().AttachAsNew(obj);
                            }
                            obj.ParentObject = parent;
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

        public static void FromStream(ICompoundObject val, XmlReader xml)
        {
            if (xml == null) { throw new ArgumentNullException("xml"); }
            if (val == null) { throw new ArgumentNullException("val"); }

            using (var entries = xml.ReadSubtree())
            {
                while (entries.Read())
                {
                    // compound objects do not have sub-lists
                    val.FromStream(xml);
                }
            }
        }
        #endregion
    }
}
