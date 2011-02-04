
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
    using Kistl.App.Base;

    public class Importer
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Importer");

        #region Public Methods
        public static void Deploy(IKistlContext ctx, string filename)
        {
            using (var s = new FileSystemPackageProvider(filename, BasePackageProvider.Modes.Read))
            {
                Deploy(ctx, s);
            }
        }

        public static void Deploy(IKistlContext ctx, IPackageProvider s)
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
                Log.Info("Loading namespaces");
                var names = LoadModuleNames(s);
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

                s.RewindData();
                Dictionary<Guid, IPersistenceObject> importedObjects = new Dictionary<Guid, IPersistenceObject>();
                Log.Info("Loading");

                // Find Root Element
                while (s.Reader.Read() && s.Reader.NodeType != XmlNodeType.Element && s.Reader.LocalName != "KistlPackaging" && s.Reader.NamespaceURI != "http://dasz.at/Kistl") ;

                // Read content
                while (s.Reader.Read())
                {
                    if (s.Reader.NodeType != XmlNodeType.Element) continue;
                    var obj = ImportElement(ctx, currentObjects, s);
                    if (obj == null) throw new InvalidOperationException("Invalid import format: ImportElement returned NULL");
                    importedObjects[((IExportableInternal)obj).ExportGuid] = obj;
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
            using (var s = new FileSystemPackageProvider(filename, BasePackageProvider.Modes.Read))
            {
                LoadFromXml(ctx, s);
            }
        }

        public static void LoadFromXml(IKistlContext ctx, Stream stream)
        {
            using (var s = new StreamPackageProvider(stream, BasePackageProvider.Modes.Read))
            {
                LoadFromXml(ctx, s);
            }
        }

        /// <summary>
        /// Loads a database from the specified stream into the specified context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="s">a stream containing a database.xml</param>
        public static void LoadFromXml(IKistlContext ctx, IPackageProvider s)
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
                Dictionary<Type, List<Guid>> guids = LoadGuids(ctx, s);

                PreFetchObjects(ctx, objects, guids);

                using (Log.InfoTraceMethodCall("Loading"))
                {
                    s.RewindData();
                    Log.Info("Loading");

                    // Find Root Element
                    while (s.Reader.Read() && s.Reader.NodeType != XmlNodeType.Element && s.Reader.LocalName != "KistlPackaging" && s.Reader.NamespaceURI != "http://dasz.at/Kistl") ;

                    // Read content
                    while (s.Reader.Read())
                    {
                        if (s.Reader.NodeType != XmlNodeType.Element) continue;
                        ImportElement(ctx, objects, s);
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

        private static Dictionary<Type, List<Guid>> LoadGuids(IKistlContext ctx, IPackageProvider s)
        {
            Log.Info("Loading Export Guids");

            Dictionary<Type, List<Guid>> guids = new Dictionary<Type, List<Guid>>();
            XPathDocument doc = new XPathDocument(s.Reader);
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

        private static IEnumerable<string> LoadModuleNames(IPackageProvider s)
        {
            IList<string> namespaces = new List<string>();
            XPathDocument doc = new XPathDocument(s.Reader);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(s.Reader.NameTable);
            nsmgr.AddNamespace("KistlBase", "Kistl.App.Base");
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator it = nav.Select("//KistlBase:Module/KistlBase:Name", nsmgr);

            while (it.MoveNext())
            {
                namespaces.Add(it.Current.Value);
            }

            return namespaces;
        }

        private static IPersistenceObject ImportElement(IKistlContext ctx, Dictionary<Guid, IPersistenceObject> objects, IPackageProvider s)
        {
            Guid exportGuid = s.Reader.GetAttribute("ExportGuid").TryParseGuidValue();
            if (exportGuid != Guid.Empty)
            {
                string ifTypeName = string.Format("{0}.{1}", s.Reader.NamespaceURI, s.Reader.LocalName);
                InterfaceType ifType = ctx.GetInterfaceType(ifTypeName);
                // if (ifType == null)
                // {
                //     Log.WarnFormat("Type {0} not found", ifTypeName);
                //     return null;
                // }

                IPersistenceObject obj = FindObject(ctx, objects, exportGuid, ifType);

                using (var children = s.Reader.ReadSubtree())
                {
                    while (children.Read())
                    {
                        if (children.NodeType == XmlNodeType.Element)
                        {
                            ((IExportableInternal)obj).MergeImport(s.Reader);
                        }
                    }
                }

                if (obj is Blob && s.SupportsBlobs)
                {
                    var blob = (Blob)obj;
                    using (var stream = s.GetBlob(blob.ExportGuid))
                    {
                        blob.StoragePath = ctx.Internals().StoreBlobStream(stream, blob.ExportGuid, blob.CreatedOn.Value, blob.OriginalName);
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
                ctx.Internals().AttachAsNew(obj);
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
