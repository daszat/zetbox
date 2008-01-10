using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.API.Client;
using System.Xml;
using System.IO;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// Implementierung durch den Entwickler der Custom Actions für den Client
    /// </summary>
    public partial class CustomClientActions : API.Client.ICustomClientActions
    {
        #region Projekte
        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void Projekt_OnToString(Projekt obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void Mitarbeiter_OnToString(Mitarbeiter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void Task_OnToString(Task obj, Kistl.API.MethodReturnEventArgs<string> e)
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

        void impl_OnRechnungErstellen_Auftrag(Auftrag obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            RechnungXML xml = new RechnungXML();

            xml.Kundenname = obj.Kunde.Kundenname;
            xml.Adresse = obj.Kunde.Adresse;
            xml.Ort = obj.Kunde.Ort;
            xml.PLZ = obj.Kunde.PLZ;
            xml.Land = obj.Kunde.Land;

            xml.Auftrag = obj.Auftragsname;
            xml.Umsatz = obj.Auftragswert.Value.ToString("n2");

            xml.GesDauer = obj.Projekt.Kostentraeger.Sum(k => k.Taetigkeiten.Sum(t => t.Dauer)).Value.ToString("n1");

            xml.ZeitEntries = new List<RechnungXML.RechnungZeitEntry>();
            foreach (Kistl.App.Zeiterfassung.Zeitkonto z in obj.Projekt.Kostentraeger)
            {
                foreach (Kistl.App.Zeiterfassung.Taetigkeit t in z.Taetigkeiten)
                {
                    xml.ZeitEntries.Add(new RechnungXML.RechnungZeitEntry() 
                        { 
                            Datum = t.Datum.Value.ToShortDateString(), 
                            Dauer = t.Dauer.Value.ToString("n1"), 
                            Zeitkonto = z.Kontoname 
                        });
                }
            }

            xml.ZeitEntries = xml.ZeitEntries.OrderBy(ze => ze.Datum).ToList();

            // TODO: For Debugging only
            xml.ToFile(@"c:\temp\Rechnung.xml");

            string tmpFile = Path.GetTempFileName().Replace(".tmp", ".docx");
            File.Copy(@"c:\temp\Rechnung.docx", tmpFile, true);

            using (WordHelper word = new WordHelper(tmpFile))
            {
                XmlDocument doc = new XmlDocument();
                MemoryStream ms = new MemoryStream();
                xml.ToStream(ms);
                ms.Seek(0, SeekOrigin.Begin);
                doc.Load(ms);

                word.ApplyCustomXml(doc);
                word.AddTableEntries<RechnungXML.RechnungZeitEntry>(xml.ZeitEntries, "Datum", "/ns0:Rechnung[1]/ns0:ZeitEntries[1]/ns0:RechnungZeitEntry[{0}]");
            }

            System.Diagnostics.Process.Start(tmpFile);
        }

        void impl_OnToString_Auftrag(Auftrag obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Auftragsname;
        }

        void impl_OnToString_Kunde(Kunde obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Kundenname;
        }
        #endregion

        #region Method
        void imp_OnToString_Method(Kistl.App.Base.Method obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.MethodName;
        }
        #endregion

        #region Module
        void impl_OnToString_Module(Kistl.App.Base.Module obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ModuleName;
        }
        #endregion

        #region ObjectClass
        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void ObjectClass_OnToString(ObjectClass obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            //Kistl.App.Base.ModuleClient mClient = new Kistl.App.Base.ModuleClient();
            //Kistl.App.Base.Module m = mClient.GetObject(obj.fk_Module);
            //e.Result = m.Namespace + "." + obj.ClassName;
            //e.Result = obj.GetObject<Module>("Module").Namespace + "." + obj.ClassName;
            e.Result = obj.Module.Namespace + "." + obj.ClassName;
        }
        #endregion

        #region Properties
        void impl_OnToString_BaseProperty(Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0} {1}.{2}", 
                obj.GetDataType(), 
                obj.ObjectClass.ClassName, 
                obj.PropertyName);
        }

        void impl_OnToString_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "* " + e.Result;
        }

        void impl_OnToString_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "-> " + e.Result;
        }

        void impl_OnGetDataType_BaseProperty(Kistl.App.Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement BaseProperty.GetDataType()>";
        }

        void impl_OnGetDataType_StringProperty(Kistl.App.Base.StringProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        void impl_OnGetDataType_DoubleProperty(Kistl.App.Base.DoubleProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        void impl_OnGetDataType_BoolProperty(Kistl.App.Base.BoolProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        void impl_OnGetDataType_IntProperty(Kistl.App.Base.IntProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        void impl_OnGetDataType_DateTimeProperty(Kistl.App.Base.DateTimeProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        void impl_OnGetDataType_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            ObjectClass objClass = obj.ReferenceObjectClass;
            e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
        }

        void impl_OnGetDataType_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            ObjectClass objClass = obj.ReferenceProperty.ObjectClass;
            e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
        }
        #endregion

        #region Zeiterfassung
        void impl_OnToString_Taetigkeit(Kistl.App.Zeiterfassung.Taetigkeit obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Zeitkonto + ": " + obj.Datum.Value.ToShortDateString() + ", " + obj.Dauer.Value + "h";
        }

        void impl_OnToString_Kostentraeger(Kistl.App.Zeiterfassung.Kostentraeger obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // Do nothing
        }

        void impl_OnToString_Zeitkonto(Kistl.App.Zeiterfassung.Zeitkonto obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Kontoname;
        }

        void impl_OnToString_Kostenstelle(Kistl.App.Zeiterfassung.Kostenstelle obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
        }
        #endregion
    }
}
