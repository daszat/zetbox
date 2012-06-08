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
using System.Xml.Serialization;
using System.IO;

namespace Zetbox.App.Projekte
{
    [XmlRoot("Rechnung", Namespace = "http://dasz.at/Zetbox/Dokumente/Rechnung/")]
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
