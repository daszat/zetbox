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
using Kistl.API.Utils;

namespace Kistl.Server.Packaging
{
    /// <summary>
    /// 
    /// </summary>
    public class Importer
    {
        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public static void Deploy(string filename)
        {
            using (IKistlServerContext ctx = KistlContext.GetServerContext())
            {
                using (FileStream fs = File.OpenRead(filename))
                {
                    Deploy(ctx, fs);
                }
                Logging.Log.Info("Submitting changes");
                ctx.SubmitRestore();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="s"></param>
        public static void Deploy(IKistlContext ctx, Stream s)
        {
            using (Logging.Log.TraceMethodCall())
            {
                Logging.Log.InfoFormat("Starting Deployment from {0}", s is FileStream ? ((FileStream)s).Name : s.GetType().Name);
                try
                {
                    // TODO: Das muss ich z.Z. machen, weil die erste Query eine Entity Query ist und noch nix geladen wurde....
                    var testObj = ctx.GetQuery<Kistl.App.Base.ObjectClass>().FirstOrDefault();
                    Debug.WriteLine(testObj != null ? testObj.ToString() : "");
                }
                catch
                {
                    // Ignore
                }

                Dictionary<Guid, IPersistenceObject> currentObjects = new Dictionary<Guid, IPersistenceObject>();
                using (XmlReader xml = XmlReader.Create(s, new XmlReaderSettings() { CloseInput = false }))
                {
                    Logging.Log.Info("Loading namespaces");
                    var namespaces = LoadModuleNamespaces(xml);
                    if (namespaces.Count() == 0) throw new InvalidOperationException("No modules found in import file");

                    foreach (var ns in namespaces)
                    {
                        Logging.Log.InfoFormat("Prefeching objects for {0}", ns);
                        var module = ctx.GetQuery<Kistl.App.Base.Module>().FirstOrDefault(m => m.Namespace == ns);
                        if (module != null)
                        {
                            foreach (var obj in PackagingHelper.GetMetaObjects(ctx, module))
                            {
                                currentObjects[((Kistl.App.Base.IExportable)obj).ExportGuid] = obj;
                            }
                        }
                        else
                        {
                            Logging.Log.InfoFormat("Found new Module '{0}' in XML", ns);
                        }
                    }
                }

                s.Seek(0, SeekOrigin.Begin);
                Dictionary<Guid, IPersistenceObject> importedObjects = new Dictionary<Guid, IPersistenceObject>();
                using (XmlReader xml = XmlReader.Create(s, new XmlReaderSettings() { CloseInput = false }))
                {
                    Logging.Log.Info("Loading");

                    // Find Root Element
                    while (xml.Read() && xml.NodeType != XmlNodeType.Element && xml.LocalName != "KistlPackaging" && xml.NamespaceURI != "http://dasz.at/Kistl") ;

                    // Read content
                    while (xml.Read())
                    {
                        if (xml.NodeType != XmlNodeType.Element) continue;
                        var obj = ImportElement(ctx, currentObjects, xml);
                        if (obj == null) throw new InvalidOperationException("Invalid import format: ImportElement returned NULL");
                        importedObjects[((IExportableInternal)obj).ExportGuid] = obj;
                    }
                }

                Logging.Log.Info("Reloading References");
                foreach (var obj in importedObjects.Values)
                {
                    obj.ReloadReferences();
                }

                var objectsToDelete = currentObjects.Where(p => !importedObjects.ContainsKey(p.Key));
                Logging.Log.InfoFormat("Deleting {0} objects marked for deletion", objectsToDelete.Count());
                foreach (var pairToDelete in objectsToDelete)
                {
                    ctx.Delete(pairToDelete.Value);
                }
                Logging.Log.Info("Deployment finished");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public static void LoadFromXml(string filename)
        {
            using (IKistlServerContext ctx = KistlContext.GetServerContext())
            {
                using (FileStream fs = File.OpenRead(filename))
                {
                    LoadFromXml(ctx, fs);
                }
                Logging.Log.Info("Submitting changes");
                ctx.SubmitRestore();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="filename"></param>
        public static void LoadFromXml(IKistlContext ctx, string filename)
        {
            using (FileStream fs = File.OpenRead(filename))
            {
                LoadFromXml(ctx, fs);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="s"></param>
        public static void LoadFromXml(IKistlContext ctx, Stream s)
        {
            using (Logging.Log.TraceMethodCall())
            {
                Logging.Log.InfoFormat("Starting Import from {0}", s is FileStream ? ((FileStream)s).Name : s.GetType().Name);
                try
                {
                    // TODO: Das muss ich z.Z. machen, weil die erste Query eine Entity Query ist und noch nix geladen wurde....
                    var testObj = ctx.GetQuery<Kistl.App.Base.ObjectClass>().FirstOrDefault();
                    Debug.WriteLine(testObj != null ? testObj.ToString() : "");
                }
                catch
                {
                    // Ignore
                }

                Dictionary<Guid, IPersistenceObject> objects = new Dictionary<Guid, IPersistenceObject>();
                using (XmlReader xml = XmlReader.Create(s, new XmlReaderSettings() { CloseInput = false }))
                {
                    Logging.Log.Info("Loading Export Guids");
                    Dictionary<Type, List<Guid>> guids = LoadGuids(xml);

                    Logging.Log.Info("Prefeching Objects");
                    PreFetchObjects(ctx, objects, guids);
                }
                s.Seek(0, SeekOrigin.Begin);
                using (XmlReader xml = XmlReader.Create(s, new XmlReaderSettings() { CloseInput = false }))
                {

                    Logging.Log.Info("Loading");

                    // Find Root Element
                    while (xml.Read() && xml.NodeType != XmlNodeType.Element && xml.LocalName != "KistlPackaging" && xml.NamespaceURI != "http://dasz.at/Kistl") ;

                    // Read content
                    while (xml.Read())
                    {
                        if (xml.NodeType != XmlNodeType.Element) continue;
                        ImportElement(ctx, objects, xml);
                    }
                }

                Logging.Log.Info("Reloading References");
                foreach (var obj in objects.Values)
                {
                    obj.ReloadReferences();
                }
                Logging.Log.Info("Import finished");
            }
        }
        #endregion

        #region Implementation
        private static void PreFetchObjects(IKistlContext ctx, Dictionary<Guid, IPersistenceObject> objects, Dictionary<Type, List<Guid>> guids)
        {
            Logging.Log.Info("Prefetching Objects");
            foreach (Type t in guids.Keys)
            {
                IEnumerable<IPersistenceObject> result = ctx.FindPersistenceObjects(new InterfaceType(t), guids[t]);
                Logging.Log.DebugFormat("{0}: XML: {1}, Storage: {2}", t.FullName, guids[t].Count, result.Count());

                foreach (IPersistenceObject obj in result)
                {
                    objects.Add(((IExportableInternal)obj).ExportGuid, obj);
                }
            }
        }

        private static Dictionary<Type, List<Guid>> LoadGuids(XmlReader xml)
        {
            Dictionary<Type, List<Guid>> guids = new Dictionary<Type, List<Guid>>();
            XPathDocument doc = new XPathDocument(xml);
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator it = nav.Select("//*[@ExportGuid]");


            while (it.MoveNext())
            {
                string ns = it.Current.NamespaceURI;
                string tn = it.Current.LocalName;
                if (it.Current.MoveToAttribute("ExportGuid", ""))
                {
                    Guid exportGuid = it.Current.Value.ParseGuidValue();
                    if (exportGuid != Guid.Empty)
                    {
                        string ifTypeName = string.Format("{0}.{1}, {2}", ns, tn, ApplicationContext.Current.InterfaceAssembly);
                        Type t = Type.GetType(ifTypeName);
                        if (t != null)
                        {
                            if (!guids.ContainsKey(t)) guids[t] = new List<Guid>();
                            guids[t].Add(exportGuid);
                        }
                        else
                        {
                            Logging.Log.WarnFormat("Type {0} not found", ifTypeName);
                        }
                    }
                }
            }
            return guids;
        }

        private static IEnumerable<string> LoadModuleNamespaces(XmlReader xml)
        {
            IList<string> namespaces = new List<string>();
            XPathDocument doc = new XPathDocument(xml);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("KistlBase", "Kistl.App.Base");
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator it = nav.Select("//KistlBase:Module/KistlBase:Namespace", nsmgr);

            while (it.MoveNext())
            {
                namespaces.Add(it.Current.Value);
            }

            return namespaces;
        }

        private static IPersistenceObject ImportElement(IKistlContext ctx, Dictionary<Guid, IPersistenceObject> objects, XmlReader xml)
        {
            Guid exportGuid = xml.GetAttribute("ExportGuid").ParseGuidValue();
            if (exportGuid != Guid.Empty)
            {
                string ifTypeName = string.Format("{0}.{1}, {2}", xml.NamespaceURI, xml.LocalName, ApplicationContext.Current.InterfaceAssembly);
                Type t = Type.GetType(ifTypeName);
                if (t == null)
                {
                    Logging.Log.WarnFormat("Type {0} not found", ifTypeName);
                    return null;
                }

                InterfaceType ifType = new InterfaceType(t);

                IPersistenceObject obj = FindObject(ctx, objects, exportGuid, ifType);

                var children = xml.ReadSubtree();
                while (children.Read())
                {
                    if (children.NodeType == XmlNodeType.Element)
                    {
                        ((IExportableInternal)obj).MergeImport(xml);
                    }
                }

                return obj;
            }
            else
            {
                return null;
            }
        }

        private static IPersistenceObject FindObject(IKistlContext ctx, Dictionary<Guid, IPersistenceObject> objects, Guid exportGuid, InterfaceType ifType)
        {
            if (!objects.ContainsKey(exportGuid))
            {
                if (!typeof(IDataObject).IsAssignableFrom(ifType.Type) && 
                    !typeof(IRelationCollectionEntry).IsAssignableFrom(ifType.Type))
                {
                    throw new NotSupportedException("Interfacetype " + ifType + " is not supported");
                }
                IPersistenceObject obj = (IPersistenceObject)Activator.CreateInstance(ifType.ToImplementationType().Type);
                if (obj == null)
                {
                    throw new InvalidOperationException("Unable to create object of type " + ifType.ToImplementationType().Type);
                }
                ctx.Attach(obj);
                objects[exportGuid] = obj;
                ((Kistl.App.Base.IExportable)obj).ExportGuid = exportGuid;
                return obj;
            }
            else
            {
                return objects[exportGuid];
            }
        }
        #endregion

    }
}
