using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;

namespace Kistl.App.Projekte
{
    public class CustomClientActions_Projekte
    {
        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void OnToString_Projekt(Projekt obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void OnToString_Mitarbeiter(Mitarbeiter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void OnToString_Task(Task obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            if (obj.DatumVon.HasValue && obj.DatumBis.HasValue)
            {
                e.Result = string.Format("{0} [{1}] ({2} - {3})",
                    obj.Name, obj.Aufwand, obj.DatumVon.Value.ToShortDateString(), obj.DatumBis.Value.ToShortDateString());
            }
            else
            {
                e.Result = string.Format("{0} [{1}]",
                    obj.Name, obj.Aufwand);
            }
        }

        public void OnToString_Auftrag(Auftrag obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Auftragsname;
        }

        public void OnToString_Kunde(Kunde obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Kundenname;
        }

        public void OnRechnungErstellen_Auftrag(Auftrag obj)
        {
            //RechnungXML xml = new RechnungXML();

            //xml.Kundenname = obj.Kunde.Kundenname;
            //xml.Adresse = obj.Kunde.Adresse;
            //xml.Ort = obj.Kunde.Ort;
            //xml.PLZ = obj.Kunde.PLZ;
            //xml.Land = obj.Kunde.Land;

            //xml.Auftrag = obj.Auftragsname;
            //xml.Umsatz = obj.Auftragswert.Value.ToString("n2");

            //xml.GesDauer = obj.Projekt.Kostentraeger.Sum(k => k.Taetigkeiten.Sum(t => t.Dauer)).ToString("n1");

            //xml.ZeitEntries = new List<RechnungXML.RechnungZeitEntry>();
            //foreach (Kistl.App.TimeRecords.WorkEffortAccount z in obj.Projekt.Kostentraeger)
            //{
            //    foreach (Kistl.App.TimeRecords.Taetigkeit t in z.Taetigkeiten)
            //    {
            //        xml.ZeitEntries.Add(new RechnungXML.RechnungZeitEntry()
            //        {
            //            Datum = t.Datum.ToShortDateString(),
            //            Dauer = t.Dauer.ToString("n1"),
            //            WorkEffortAccount = z.Name
            //        });
            //    }
            //}

            //xml.ZeitEntries = xml.ZeitEntries.OrderBy(ze => ze.Datum).ToList();

            //// TODO: For Debugging only
            //xml.ToFile(@"c:\temp\Rechnung.xml");

            //string tmpFile = Path.GetTempFileName().Replace(".tmp", ".docx");
            //File.Copy(ApplicationContext.Current.Configuration.Client.DocumentStore + @"\Rechnung.docx", tmpFile, true);

            //using (WordHelper word = new WordHelper(tmpFile))
            //{
            //    XmlDocument doc = new XmlDocument();
            //    MemoryStream ms = new MemoryStream();
            //    xml.ToStream(ms);
            //    ms.Seek(0, SeekOrigin.Begin);
            //    doc.Load(ms);

            //    word.ApplyCustomXml(doc);
            //    word.AddTableEntries<RechnungXML.RechnungZeitEntry>(xml.ZeitEntries, "Datum", "/ns0:Rechnung[1]/ns0:ZeitEntries[1]/ns0:RechnungZeitEntry[{0}]");
            //}

            //System.Diagnostics.Process.Start(tmpFile);
        }
    }
}
