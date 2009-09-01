using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.Xml;
using System.Collections;
using Kistl.API.Utils;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// http://www.codeplex.com/dbe/
    /// http://openxmldeveloper.org/archive/2007/04/18/1447.aspx
    /// </summary>
    public class WordHelper : IDisposable
    {
        private FileStream fs;
        private Package pckg;

        public WordHelper(string filename)
        {
            //open the file, this could throw an exception (e.g. if the file is not found)
            //having includeExceptionDetailInFaults="True" in config would cause this exception to be returned to the client
            try
            {
                //load the docx
                fs = File.Open(filename, FileMode.Open, FileAccess.ReadWrite);
                pckg = Package.Open(fs, FileMode.Open, FileAccess.ReadWrite); // use read write access
            }
            catch (IOException ex)
            {
                Logging.Log.Warn("An exception was thrown while trying to open file " + filename, ex);
                throw ex;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (pckg != null)
            {
                pckg.Flush();
                pckg.Close();
            }

            if (fs != null)
            {
                fs.Dispose();
            }

            fs = null;
            pckg = null;

            GC.SuppressFinalize(this);
        }

        #endregion

        public void ApplyCustomXml(XmlDocument xml)
        {
            Uri customXmlUri = new Uri("/customXml/item1.xml", UriKind.Relative);
            PackagePart customXmlPart = pckg.GetPart(customXmlUri);
            customXmlPart.GetStream().SetLength(0);
            customXmlPart.GetStream().Seek(0, SeekOrigin.Begin);
            xml.Save(customXmlPart.GetStream());
        }

        public void AddTableEntries<T>(List<T> list, string searchAttribute, string targetXPath)
        {
            if (list.Count < 2)//bail-out if we dont have to add rows
                return;

            PackagePart documentPart = null;

            // Given a file name, retrieve the "start" part--the document part.
            const string documentRelationshipType = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
            //  Get the main document part (workbook.xml, document.xml, presentation.xml).
            foreach (System.IO.Packaging.PackageRelationship relationship in pckg.GetRelationshipsByType(documentRelationshipType))
            {
                documentPart = pckg.GetPart(PackUriHelper.ResolvePartUri(new Uri("/", UriKind.Relative), relationship.TargetUri));
                break; //There should only be one document part in the package. 
            }

            //load the doc to a dom object (XmlDocument)
            XmlDocument doc = new XmlDocument();
            doc.Load(documentPart.GetStream());

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ds", "http://schemas.openxmlformats.org/officeDocument/2006/customXml");
            nsmgr.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            nsmgr.AddNamespace("", string.Empty);

            XmlNodeList tableNodeList = doc.SelectNodes("//w:tbl", nsmgr);
            foreach (XmlNode tableNode in tableNodeList)//iterate through all the tables of the doc
            {
                XmlNode tableCellProcSdtPropNode = tableNode.SelectSingleNode(@"./w:tr[2]/w:tc[1]//w:sdt/w:sdtPr/w:tag", nsmgr);
                if (tableCellProcSdtPropNode != null)
                {
                    foreach (XmlAttribute attrNode in tableCellProcSdtPropNode.Attributes)//iterate through the content control's attributes to locate the "tag"
                    {
                        if ((attrNode.Name == "w:val") && (attrNode.Value == searchAttribute))
                        {
                            for (int i = 0; i < list.Count - 1; i++)
                            {
                                XmlNode newRowNode = tableNode.AppendChild(tableNode.SelectSingleNode(@"./w:tr[2]", nsmgr).CloneNode(true));
                                foreach (XmlNode cellNode in newRowNode.SelectNodes(@"./w:tc", nsmgr))//take up all the cells of the newly added row in order to fix their attributes
                                {
                                    foreach (XmlAttribute attr in cellNode.SelectSingleNode(@".//w:sdt/w:sdtPr/w:dataBinding", nsmgr).Attributes)
                                        if (attr.Name == "w:xpath")
                                            attr.Value = string.Format(targetXPath, i + 2) + attr.Value.Substring(attr.Value.LastIndexOf("/"));
                                    cellNode.SelectSingleNode(@".//w:sdt/w:sdtPr", nsmgr).RemoveChild(cellNode.SelectSingleNode(@".//w:sdt/w:sdtPr/w:id", nsmgr));
                                }
                            }
                        }
                    }
                }
            }

            documentPart.GetStream().Seek(0, SeekOrigin.Begin);//reset the stream-pointer
            documentPart.GetStream().SetLength(0);
            doc.Save(documentPart.GetStream());
        }
    }
}
