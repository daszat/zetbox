using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Kistl.App.Projekte.Client
{
    [XmlRoot("Rechnung", Namespace = "http://dasz.at/Kistl/Dokumente/Rechnung/")]
    public class RechnungXML
    {
        public Kistl.App.Projekte.Auftrag Auftrag { get; set; }
        public Kistl.App.Projekte.Kunde Kunde { get; set; }

        public string Umsatz { get; set; }
        public string GetDauer { get; set; }

        public class RechnungZeitEntry
        {
            public Kistl.App.Zeiterfassung.Taetigkeit Taetigkeit { get; set; }
            public Kistl.App.Zeiterfassung.Zeitkonto Zeitkonto { get; set; }
        }

        public List<RechnungZeitEntry> ZeitEntries { get; set; }

        /// <summary>
        /// Serializer
        /// </summary>
        private static XmlSerializer xml = new XmlSerializer(typeof(RechnungXML));

        /// <summary>
        /// Serialize this Object to a Stream
        /// </summary>
        /// <param name="s"></param>
        public void ToStream(Stream s)
        {
            xml.Serialize(s, this);

            // System.IO.Packaging.Package word = System.IO.Packaging.Package.Open("");
        }

        /// <summary>
        /// Serialize this Object to a File
        /// </summary>
        /// <param name="s"></param>
        public void ToFile(string filename)
        {
            using (XmlTextWriter w = new XmlTextWriter(filename, Encoding.Default))
            {
                xml.Serialize(w, this);
            }
        }
    }
}
