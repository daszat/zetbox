using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using System.Xml;
using System.IO;
using System.Xml.XPath;

namespace Kistl.Server.Packaging
{
    public class Importer
    {
        public static void Import(string filename)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                Trace.TraceInformation("Starting Import from {0}", filename);
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    using (FileStream fs = File.OpenRead(filename))
                    {
                        Import(ctx, fs);
                    }
                    Trace.TraceInformation("Submitting changes");
                    ctx.SubmitChanges();
                }
                Trace.TraceInformation("Import finished");
            }
        }

        public static void Import(IKistlContext ctx, Stream s)
        {
            // TODO: Das muss ich z.Z. machen, weil die erste Query eine Entity Query ist und noch nix geladen wurde....
            var testObj = ctx.GetQuery<Kistl.App.Base.ObjectClass>().FirstOrDefault();
            Debug.WriteLine(testObj != null ? testObj.ToString() : "");

            Dictionary<Guid, IPersistenceObject> objects = new Dictionary<Guid, IPersistenceObject>();
            using (XmlReader xml = XmlReader.Create(s, new XmlReaderSettings() { CloseInput = false }))
            {
                Console.WriteLine("Loading Export Guids");
                Dictionary<Type, List<Guid>> guids = LoadGuids(xml);

                Console.WriteLine("Prefeching Objects");
                PreFetchObjects(ctx, objects, guids);
            }
            s.Seek(0, SeekOrigin.Begin);
            using (XmlReader xml = XmlReader.Create(s, new XmlReaderSettings() { CloseInput = false }))
            {

                Console.WriteLine("Loading");

                // Find Root Element
                while (xml.Read() && xml.NodeType != XmlNodeType.Element && xml.LocalName != "KistlPackaging" && xml.NamespaceURI != "http://dasz.at/Kistl") ;

                // Read content
                while (xml.Read())
                {
                    if (xml.NodeType != XmlNodeType.Element) continue;
                    ImportElement(ctx, objects, xml);
                }
            }

            Trace.TraceInformation("Reloading References");
            foreach (var obj in objects.Values)
            {
                obj.ReloadReferences();
            }
        }

        private static void PreFetchObjects(IKistlContext ctx, Dictionary<Guid, IPersistenceObject> objects, Dictionary<Type, List<Guid>> guids)
        {
            foreach (Type t in guids.Keys)
            {
                Console.Write("{0}: {1}", t.FullName, guids[t].Count);
                IEnumerable<IPersistenceObject> result = ctx.FindPersistenceObjects(new InterfaceType(t), guids[t]);
                Console.WriteLine(" -> {0}", result.Count());

                foreach (IPersistenceObject obj in result)
                {
                    objects.Add(((IExportableInternal)obj).ExportGuid, obj);
                }
            }
            Console.WriteLine();

        }

        private static Dictionary<Type, List<Guid>> LoadGuids(XmlReader xml)
        {
            Dictionary<Type, List<Guid>> guids = new Dictionary<Type, List<Guid>>();
            XPathDocument doc = new XPathDocument(xml);
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator it = nav.Select("//*[@ExportGuid]");


            while (it.MoveNext())
            {
                Console.Write(".");
                string ns = it.Current.NamespaceURI;
                string tn = it.Current.LocalName;
                if (it.Current.MoveToAttribute("ExportGuid", ""))
                {
                    Guid exportGuid = it.Current.Value.ParseGuidValue();
                    if (exportGuid != Guid.Empty)
                    {
                        Console.Write(".");
                        string ifTypeName = string.Format("{0}.{1}, {2}", ns, tn, ApplicationContext.Current.InterfaceAssembly);
                        Type t = Type.GetType(ifTypeName);
                        if (t != null)
                        {
                            if (!guids.ContainsKey(t)) guids[t] = new List<Guid>();
                            guids[t].Add(exportGuid);
                        }
                        else
                        {
                            Trace.TraceWarning("Type {0} not found", ifTypeName);
                        }
                    }
                }
            }
            Console.WriteLine();
            return guids;
        }

        private static void ImportElement(IKistlContext ctx, Dictionary<Guid, IPersistenceObject> objects, XmlReader xml)
        {
            Guid exportGuid = xml.GetAttribute("ExportGuid").ParseGuidValue();
            if (exportGuid != Guid.Empty)
            {
                Console.Write(".");
                string ifTypeName = string.Format("{0}.{1}, {2}", xml.NamespaceURI, xml.LocalName, ApplicationContext.Current.InterfaceAssembly);
                Type t = Type.GetType(ifTypeName);
                if (t == null)
                {
                    Trace.TraceWarning("Type {0} not found", ifTypeName);
                    return;
                }

                InterfaceType ifType = new InterfaceType(t);

                IPersistenceObject obj = FindObject(ctx, objects, exportGuid, ifType);
                ((Kistl.App.Base.IExportable)obj).ExportGuid = exportGuid;

                var children = xml.ReadSubtree();
                while (children.Read())
                {
                    if (children.NodeType == XmlNodeType.Element)
                    {
                        ((IExportableInternal)obj).MergeImport(xml);
                    }
                }
            }
        }

        private static IPersistenceObject FindObject(IKistlContext ctx, Dictionary<Guid, IPersistenceObject> objects, Guid exportGuid, InterfaceType ifType)
        {
            if (!objects.ContainsKey(exportGuid))
            {
                IPersistenceObject obj;
                if (typeof(IDataObject).IsAssignableFrom(ifType.Type))
                {
                    obj = ctx.Create(ifType);
                }
                else if (typeof(IRelationCollectionEntry).IsAssignableFrom(ifType.Type))
                {
                    obj = ctx.CreateRelationCollectionEntry(ifType);
                }
                else
                {
                    throw new NotSupportedException("Interfacetype " + ifType.Type.FullName + " is not supported");
                }
                objects[exportGuid] = obj;
                return obj;
            }
            else
            {
                return objects[exportGuid];
            }
        }

        
    }
}
