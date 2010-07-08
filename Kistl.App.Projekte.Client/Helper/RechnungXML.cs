using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Kistl.App.Projekte
{
    [XmlRoot("Rechnung", Namespace = "http://dasz.at/Kistl/Dokumente/Rechnung/")]
    public class RechnungXML
    {
        public string Kundenname { get; set; }
        public string Auftrag { get; set; }
        public string Adresse { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Land { get; set; }

        public string Umsatz { get; set; }
        public string GesDauer { get; set; }

        public class RechnungZeitEntry
        {
            public string Datum { get; set; }
            public string WorkEffortAccount { get; set; }
            public string Dauer { get; set; }
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

            // System.IO.Packaging.Package word = System.IO.Packaging.Package.Open(String.Empty);
        }

        /// <summary>
        /// Serialize this Object to a File
        /// </summary>
        public void ToFile(string filename)
        {
            using (XmlTextWriter w = new XmlTextWriter(filename, Encoding.Unicode))
            {
                w.Formatting = Formatting.Indented;
                xml.Serialize(w, this);
            }
        }
    }
}
