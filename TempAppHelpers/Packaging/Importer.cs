
namespace Kistl.App.Packaging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.XPath;

    using Kistl.API;
    using Kistl.API.Utils;

    public class Importer
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Importer");

        #region Public Methods

        public static void Deploy(IKistlContext ctx, Stream s)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (s == null) { throw new ArgumentNullException("s"); }

            using (Log.InfoTraceMethodCall("Deploy"))
            {
                Log.InfoFormat("Starting Deployment from {0}", s is FileStream ? ((FileStream)s).Name : s.GetType().Name);
                try
                {
                    // TODO: Das muss ich z.Z. machen, weil die erste Query eine Entity Query ist und noch nix geladen wurde....
                    var testObj = ctx.GetQuery<Kistl.App.Base.ObjectClass>().FirstOrDefault();
                    Debug.WriteLine(testObj != null ? testObj.ToString() : String.Empty);
                }
                catch
                {
                    // Ignore
                }

                Dictionary<Guid, IPersistenceObject> currentObjects = new Dictionary<Guid, IPersistenceObject>();
                using (XmlReader xml = XmlReader.Create(s, new XmlReaderSettings() { CloseInput = false }))
                {
                    Log.Info("Loading namespaces");
                    var names = LoadModuleNames(xml);
                    if (names.Count() == 0) throw new InvalidOperationException("No modules found in import file");

                    foreach (var name in names)
                    {
                        Log.InfoFormat("Prefetching objects for {0}", name);
                        var module = ctx.GetQuery<Kistl.App.Base.Module>().FirstOrDefault(m => m.Name == name);
                        if (module != null)
                        {
                            foreach (var obj in PackagingHelper.GetMetaObjects(ctx, module))
                            {
                                currentObjects[((Kistl.App.Base.IExportable)obj).ExportGuid] = obj;
                            }
                        }
                        else
                        {
                            Log.InfoFormat("Found new Module '{0}' in XML", name);
                        }
                    }
                }

                s.Seek(0, SeekOrigin.Begin);
                Dictionary<Guid, IPersistenceObject> importedObjects = new Dictionary<Guid, IPersistenceObject>();
                using (XmlReader xml = XmlReader.Create(s, new XmlReaderSettings() { CloseInput = false }))
                {
                    Log.Info("Loading");

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

                Log.Info("Reloading References");
                foreach (var obj in importedObjects.Values)
                {
                    obj.ReloadReferences();
                }

                var objectsToDelete = currentObjects.Where(p => !importedObjects.ContainsKey(p.Key));
                Log.InfoFormat("Deleting {0} objects marked for deletion", objectsToDelete.Count());
                foreach (var pairToDelete in objectsToDelete)
                {
                    ctx.Delete(pairToDelete.Value);
                }
                Log.Info("Deployment finished");
            }
        }

        /// <summary>
        /// Loads a database from the specified filename into the specified context.
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
        /// Loads a database from the specified stream into the specified context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="s">a stream containing a database.xml</param>
        public static void LoadFromXml(IKistlContext ctx, Stream s)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (s == null) { throw new ArgumentNullException("s"); }

            using (Log.InfoTraceMethodCall("LoadFromXml"))
            {
                Log.InfoFormat("Starting Import from {0}", s is FileStream ? ((FileStream)s).Name : s.GetType().Name);
                try
                {
                    using (Log.DebugTraceMethodCall("initialisation query"))
                    {
                        // the entity framework needs to be initialised by executing any "plain" query first
                        // TODO: repair the ef provider so it doesn't need this special casing.
                        var testObj = ctx.GetQuery<Kistl.App.Base.ObjectClass>().FirstOrDefault();
                        Log.DebugFormat("query result: [{0}]", testObj);
                    }
                }
                catch (Exception ex)
                {
                    Log.Warn("Error while initialising, trying to proceed anyways", ex);
                }

                Dictionary<Guid, IPersistenceObject> objects = new Dictionary<Guid, IPersistenceObject>();
                using (XmlReader xml = XmlReader.Create(s, new XmlReaderSettings() { CloseInput = false }))
                {
                    Dictionary<Type, List<Guid>> guids = LoadGuids(ctx, xml);

                    PreFetchObjects(ctx, objects, guids);
                }

                using (Log.InfoTraceMethodCall("Loading"))
                {
                    s.Seek(0, SeekOrigin.Begin);
                    using (XmlReader xml = XmlReader.Create(s, new XmlReaderSettings() { CloseInput = false }))
                    {

                        Log.Info("Loading");

                        // Find Root Element
                        while (xml.Read() && xml.NodeType != XmlNodeType.Element && xml.LocalName != "KistlPackaging" && xml.NamespaceURI != "http://dasz.at/Kistl") ;

                        // Read content
                        while (xml.Read())
                        {
                            if (xml.NodeType != XmlNodeType.Element) continue;
                            ImportElement(ctx, objects, xml);
                        }
                    }
                }

                using (Log.InfoTraceMethodCall("Reloading References"))
                {
                    Log.Info("Reloading References");
                    foreach (var obj in objects.Values)
                    {
                        obj.ReloadReferences();
                    }
                }
                Log.Info("Import finished");
            }
        }
        #endregion

        #region Implementation
        private static void PreFetchObjects(IKistlContext ctx, Dictionary<Guid, IPersistenceObject> objects, Dictionary<Type, List<Guid>> guids)
        {
            Log.Info("Prefetching Objects");
            foreach (Type t in guids.Keys)
            {
                IEnumerable<IPersistenceObject> result = ctx.FindPersistenceObjects(ctx.GetInterfaceType(t), guids[t]);
                if (Log.IsDebugEnabled)
                {
                    // avoid result.Count() evaluation if not needed
                    Log.DebugFormat("{0}: XML: {1}, Storage: {2}", t.FullName, guids[t].Count, result.Count());
                }

                foreach (IPersistenceObject obj in result)
                {
                    objects.Add(((IExportableInternal)obj).ExportGuid, obj);
                }
            }
        }

        private static Dictionary<Type, List<Guid>> LoadGuids(IKistlContext ctx, XmlReader xml)
        {
            Log.Info("Loading Export Guids");

            Dictionary<Type, List<Guid>> guids = new Dictionary<Type, List<Guid>>();
            XPathDocument doc = new XPathDocument(xml);
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator it = nav.Select("//*[@ExportGuid]");

            while (it.MoveNext())
            {
                string ns = it.Current.NamespaceURI;
                string tn = it.Current.LocalName;
                if (it.Current.MoveToAttribute("ExportGuid", String.Empty))
                {
                    Guid exportGuid = it.Current.Value.TryParseGuidValue();
                    if (exportGuid != Guid.Empty)
                    {
                        string ifTypeName = string.Format("{0}.{1}", ns, tn);
                        Type t = ctx.GetInterfaceType(ifTypeName).Type;
                        if (t != null)
                        {
                            if (!guids.ContainsKey(t)) guids[t] = new List<Guid>();
                            guids[t].Add(exportGuid);
                        }
                        else
                        {
                            Log.WarnFormat("Type {0} not found", ifTypeName);
                        }
                    }
                }
            }
            return guids;
        }

        private static IEnumerable<string> LoadModuleNames(XmlReader xml)
        {
            IList<string> namespaces = new List<string>();
            XPathDocument doc = new XPathDocument(xml);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("KistlBase", "Kistl.App.Base");
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator it = nav.Select("//KistlBase:Module/KistlBase:Name", nsmgr);

            while (it.MoveNext())
            {
                namespaces.Add(it.Current.Value);
            }

            return namespaces;
        }

        private static IPersistenceObject ImportElement(IKistlContext ctx, Dictionary<Guid, IPersistenceObject> objects, XmlReader xml)
        {
            Guid exportGuid = xml.GetAttribute("ExportGuid").TryParseGuidValue();
            if (exportGuid != Guid.Empty)
            {
                string ifTypeName = string.Format("{0}.{1}", xml.NamespaceURI, xml.LocalName);
                InterfaceType ifType = ctx.GetInterfaceType(ifTypeName);
                // if (ifType == null)
                // {
                //     Log.WarnFormat("Type {0} not found", ifTypeName);
                //     return null;
                // }

                IPersistenceObject obj = FindObject(ctx, objects, exportGuid, ifType);

                using (var children = xml.ReadSubtree())
                {
                    while (children.Read())
                    {
                        if (children.NodeType == XmlNodeType.Element)
                        {
                            ((IExportableInternal)obj).MergeImport(xml);
                        }
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
                if (!ifType.Type.IsIDataObject() &&
                    !ifType.Type.IsIRelationEntry())
                {
                    throw new NotSupportedException("Interfacetype " + ifType + " is not supported");
                }
                IPersistenceObject obj = ctx.Internals().CreateUnattached(ifType);
                objects[exportGuid] = obj;
                ((Kistl.App.Base.IExportable)obj).ExportGuid = exportGuid;
                ctx.Attach(obj);
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
