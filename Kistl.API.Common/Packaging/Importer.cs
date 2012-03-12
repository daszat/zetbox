
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
        public static void Deploy(IKistlContext ctx, params string[] filenames)
        {
            var packages = new List<FileSystemPackageProvider>();
            try
            {
                foreach (var f in filenames)
                {
                    packages.Add(new FileSystemPackageProvider(f, BasePackageProvider.Modes.Read));
                }
                Deploy(ctx, packages.ToArray());
            }
            finally
            {
                foreach (var p in packages)
                {
                    p.Dispose();
                }
            }
        }

        public static void Deploy(IKistlContext ctx, params IPackageProvider[] providers)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (providers == null) { throw new ArgumentNullException("providers"); }

            using (Log.InfoTraceMethodCall("Deploy"))
            {
                Log.InfoFormat("Starting Deployment from {0}", string.Join(", ", providers.Select(p => p.ToString()).ToArray()));
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
                var names = providers.SelectMany(p => LoadModuleNames(p)).Distinct().ToList();
                if (names.Count == 0) throw new InvalidOperationException("No modules found in import file");

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

                providers.ForEach(p => p.RewindData());
                Dictionary<Guid, IPersistenceObject> importedObjects = new Dictionary<Guid, IPersistenceObject>();
                Log.Info("Loading");

                foreach (var p in providers)
                {
                    var reader = p.Reader;
                    // Find Root Element
                    while (reader.Read() && reader.NodeType != XmlNodeType.Element && reader.LocalName != "KistlPackaging" && reader.NamespaceURI != "http://dasz.at/Kistl") ;

                    // Read content
                    while (reader.Read())
                    {
                        if (reader.NodeType != XmlNodeType.Element) continue;
                        var obj = ImportElement(ctx, currentObjects, p);
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
        /// <param name="filenames"></param>
        public static void LoadFromXml(IKistlContext ctx, params string[] filenames)
        {
            var packages = new List<FileSystemPackageProvider>();
            try
            {
                foreach (var f in filenames)
                {
                    packages.Add(new FileSystemPackageProvider(f, BasePackageProvider.Modes.Read));
                }
                LoadFromXml(ctx, packages.ToArray());
            }
            finally
            {
                foreach (var p in packages)
                {
                    p.Dispose();
                }
            }
        }

        public static void LoadFromXml(IKistlContext ctx, Stream stream, string streamDescription)
        {
            using (var s = new StreamPackageProvider(stream, BasePackageProvider.Modes.Read, streamDescription))
            {
                LoadFromXml(ctx, s);
            }
        }

        /// <summary>
        /// Loads a database from the specified stream into the specified context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="providers">a stream containing a database.xml</param>
        public static void LoadFromXml(IKistlContext ctx, params IPackageProvider[] providers)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (providers == null) { throw new ArgumentNullException("providers"); }

            using (Log.InfoTraceMethodCall("LoadFromXml"))
            {
                Log.InfoFormat("Starting Import from {0}", string.Join(", ", providers.Select(p => p.ToString()).ToArray()));
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
                Dictionary<Type, List<Guid>> guids = LoadGuids(ctx, providers);

                PreFetchObjects(ctx, objects, guids);

                using (Log.InfoTraceMethodCall("Loading"))
                {
                    providers.ForEach(p => p.RewindData());
                    Log.Info("Loading");
                    foreach (var p in providers)
                    {
                        var reader = p.Reader;

                        // Find Root Element
                        while (reader.Read() && reader.NodeType != XmlNodeType.Element && reader.LocalName != "KistlPackaging" && reader.NamespaceURI != "http://dasz.at/Kistl") ;

                        // Read content
                        while (reader.Read())
                        {
                            if (reader.NodeType != XmlNodeType.Element) continue;
                            ImportElement(ctx, objects, p);
                        }
                    }
                }

                using (Log.InfoTraceMethodCall("Reloading References"))
                {
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

        private static Dictionary<Type, List<Guid>> LoadGuids(IKistlContext ctx, IPackageProvider[] providers)
        {
            Log.Info("Loading Export Guids");

            Dictionary<Type, List<Guid>> guids = new Dictionary<Type, List<Guid>>();
            foreach (var reader in providers.Select(p => p.Reader))
            {
                XPathDocument doc = new XPathDocument(reader);
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
                            ifTypeName = MigrateTypeNameMapping(ifTypeName);
                            Type t = ctx.GetInterfaceType(ifTypeName).Type;
                            if (t != null)
                            {
                                if (!guids.ContainsKey(t)) guids[t] = new List<Guid>();
                                guids[t].Add(exportGuid);
                            }
                            else
                            {
                                Log.WarnOnce(string.Format("Type {0} not found", ifTypeName));
                            }
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
                ifTypeName = MigrateTypeNameMapping(ifTypeName);
                InterfaceType ifType = ctx.GetInterfaceType(ifTypeName);
                if (ifType.Type == null)
                {
                    Log.WarnOnce(string.Format("Type {0} not found", ifTypeName));
                    return null;
                }

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
                        blob.StoragePath = ctx.Internals().StoreBlobStream(stream, blob.ExportGuid, blob.CreatedOn, blob.OriginalName);
                    }
                }

                return obj;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Map from pre-migration class names to current ones.
        /// </summary>
        /// <remarks>
        /// This mapping keeps the saved schema XML in the destination database valid. Without this we couldn't deploy/migrate from the old schema to the new one.
        /// </remarks>
        /// <param name="ifTypeName"></param>
        /// <returns></returns>
        private static string MigrateTypeNameMapping(string ifTypeName)
        {
            switch(ifTypeName) {
                case "Kistl.App.Base.ObjectClass_implements_Interface_RelationEntry":
                    return "Kistl.App.Base.DataType_implements_Interface_RelationEntry";
                default:
                    return ifTypeName;
            }
        }

        private static IPersistenceObject FindObject(IKistlContext ctx, Dictionary<Guid, IPersistenceObject> objects, Guid exportGuid, InterfaceType ifType)
        {
            if (!objects.ContainsKey(exportGuid))
            {
                if (!ifType.Type.IsIDataObject() &&
                    !ifType.Type.IsIRelationEntry())
                {
                    throw new NotSupportedException(String.Format("Interfacetype {0} is not supported", ifType));
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
