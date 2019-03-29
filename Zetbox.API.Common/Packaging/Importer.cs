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

namespace Zetbox.App.Packaging
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.XPath;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;

    public class Importer
    {
        private readonly static log4net.ILog Log = Logging.Exporter;

        #region Public Methods
        public static void Deploy(IZetboxContext ctx, params string[] filenames)
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

        public static void Deploy(IZetboxContext ctx, params IPackageProvider[] providers)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (providers == null) { throw new ArgumentNullException("providers"); }

            using (Log.InfoTraceMethodCall("Deploy"))
            {
                Log.InfoFormat("Starting Deployment from {0}", string.Join(", ", providers.Select(p => p.ToString()).ToArray()));
                ctx.TransientState["__IS_CURRENTLY_IMPORTING__"] = true;
                try
                {
                    // TODO: Das muss ich z.Z. machen, weil die erste Query eine Entity Query ist und noch nix geladen wurde....
                    var testObj = ctx.GetQuery<Zetbox.App.Base.ObjectClass>().FirstOrDefault();
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
                    var module = ctx.GetQuery<Zetbox.App.Base.Module>().FirstOrDefault(m => m.Name == name);
                    if (module != null)
                    {
                        foreach (var obj in PackagingHelper.GetMetaObjects(ctx, module))
                        {
                            currentObjects[((Zetbox.App.Base.IExportable)obj).ExportGuid] = obj;
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
                    while (reader.Read() && reader.NodeType != XmlNodeType.Element && reader.LocalName != "ZetboxPackaging" && reader.NamespaceURI != "http://dasz.at/Zetbox") ;

                    // Read content
                    while (reader.Read())
                    {
                        if (reader.NodeType != XmlNodeType.Element) continue;
                        var obj = ImportElement(ctx, currentObjects, p);
                        if (obj != null)
                        {
                            importedObjects[((IExportableInternal)obj).ExportGuid] = obj;
                        }
                    }
                }

                Log.Debug("Reloading References");
                foreach (var obj in importedObjects.Values)
                {
                    obj.ReloadReferences();
                }

                var objectsToDelete = currentObjects.Where(p => !importedObjects.ContainsKey(p.Key));
                Log.DebugFormat("Deleting {0} objects marked for deletion", objectsToDelete.Count());
                foreach (var pairToDelete in objectsToDelete)
                {
                    // Don't delete blobs, the blob garbage collector should delete them.
                    if (pairToDelete.Value is Blob) continue;

                    ctx.Delete(pairToDelete.Value);
                }

                using (Log.DebugTraceMethodCall("Playback Notifications"))
                {
                    foreach (var obj in importedObjects.Values)
                    {
                        ((BaseNotifyingObject)obj).PlaybackNotifications();
                    }
                }

                ctx.TransientState.Remove("__IS_CURRENTLY_IMPORTING__");
                Log.Debug("Deployment finished");
            }
        }

        /// <summary>
        /// Deletes the given module. Each type is deleted and commited to aviod cirular and komplex delete graphs. Therefore, the context factory is necessary.
        /// </summary>
        /// <param name="ctx">A context factory. On the server side, it's recommended to create a server context.</param>
        /// <param name="module">The module to delete</param>
        /// <param name="doSubmit">A callback which should submit changes during module deletion. On the server side, it's recommended to call SubmitRestore().</param>
        public static void DeleteModule(Func<IZetboxContext> ctx, string module, Action<IZetboxContext> doSubmit)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (string.IsNullOrWhiteSpace(module)) throw new ArgumentNullException("module");
            if (doSubmit == null) throw new ArgumentNullException("doSubmit");

            var schemaList = PackagingHelper.GetModules(ctx(), new[] { module });
            if (schemaList.Count == 0)
            {
                throw new ArgumentException(string.Format("Module \"{0}\" not found.", module), "module");
            }
            else if (schemaList.Count > 1)
            {
                throw new ArgumentException("Can't use wildcards. Please delete only one module.", "module");
            }

            int moduleID = schemaList.Single().ID;

            Delete<Application>(ctx(), doSubmit, c => c.GetQuery<Application>().Where(i => i.Module.ID == moduleID));
            Delete<NavigationScreen_accessed_by_Groups_RelationEntry>(ctx(), doSubmit, c => c.Internals().GetPersistenceObjectQuery<NavigationScreen_accessed_by_Groups_RelationEntry>().Where(i => i.A.Module.ID == moduleID));
            while(Delete<NavigationEntry>(ctx(), doSubmit, c => c.GetQuery<NavigationEntry>().Where(i => i.Module.ID == moduleID).Where(i => i.Children.Count == 0)) > 0);

            // Security
            Delete<Group>(ctx(), doSubmit, c => c.GetQuery<Group>().Where(i => i.Module.ID == moduleID));
            Delete<GroupMembership>(ctx(), doSubmit, c => c.GetQuery<GroupMembership>().Where(i => i.Module.ID == moduleID));

            // Security
            Delete<RoleMembership_resolves_Relations_RelationEntry>(ctx(), doSubmit, c => c.Internals().GetPersistenceObjectQuery<RoleMembership_resolves_Relations_RelationEntry>().Where(i => i.A.Module.ID == moduleID));
            Delete<RoleMembership>(ctx(), doSubmit, c => c.GetQuery<RoleMembership>().Where(i => i.Module.ID == moduleID));

            Delete<FilterConfiguration>(ctx(), doSubmit, c => c.GetQuery<FilterConfiguration>().Where(i => i.Module.ID == moduleID));

            // Relations
            Delete<Relation>(ctx(), doSubmit, c => c.GetQuery<Relation>().Where(i => i.Module.ID == moduleID), obj => { obj.Context.Delete(obj.A); obj.Context.Delete(obj.B); });

            // TODO: Add Module to Constraint - or should that not be changable by other modules?
            // All Property Contstraints
            Delete<Zetbox.App.Base.Constraint>(ctx(), doSubmit, c => c.GetQuery<Zetbox.App.Base.Constraint>().Where(i => i.ConstrainedProperty.Module.ID == moduleID));

            // InstanceContstraints and Property Relation entries of UniqueConstraints
            Delete<InstanceConstraint>(ctx(), doSubmit, c => c.GetQuery<InstanceConstraint>().Where(i => i.Constrained.Module.ID == moduleID));
            Delete<UniqueContraints_ensures_unique_on_Properties_RelationEntry>(ctx(), doSubmit, c => c.Internals().GetPersistenceObjectQuery<UniqueContraints_ensures_unique_on_Properties_RelationEntry>().Where(i => i.A.Constrained.Module.ID == moduleID || i.B.Module.ID == moduleID));

            // TODO: Add Module to DefaultPropertyValue - or should that not be changable by other modules?
            Delete<DefaultPropertyValue>(ctx(), doSubmit, c => c.GetQuery<DefaultPropertyValue>().Where(i => i.Property.Module.ID == moduleID));

            // Properties <-> Methods
            Delete<ObjRefProp_shows_Methods_RelationEntry>(ctx(), doSubmit, c => c.Internals().GetPersistenceObjectQuery<ObjRefProp_shows_Methods_RelationEntry>().Where(i => i.A.Module.ID == moduleID || i.B.Module.ID == moduleID));

            Delete<Property>(ctx(), doSubmit, c => c.GetQuery<Property>().Where(i => i.Module.ID == moduleID));

            Delete<BaseParameter>(ctx(), doSubmit, c => c.GetQuery<BaseParameter>().Where(i => i.Method.Module.ID == moduleID));
            Delete<Method>(ctx(), doSubmit, c => c.GetQuery<Method>().Where(i => i.Module.ID == moduleID));

            // export only relation entry ending on a "local" class. Since we do not have proper inter-module dependencies in place, we cannot support pushing interface implementations across modules.
            Delete<DataType_implements_ImplementedInterfaces_RelationEntry>(ctx(), doSubmit, c => c.Internals().GetPersistenceObjectQuery<DataType_implements_ImplementedInterfaces_RelationEntry>()
                // Workaround for missing Module relation on DataType_implements_Interface_RelationEntry when creating ZetboxBase.xml
                .Where(i => i.A != null && i.A.Module != null && i.B != null)
                .Where(i => i.A.Module.ID == moduleID));
            Delete<EnumerationEntry>(ctx(), doSubmit, c => c.GetQuery<EnumerationEntry>().Where(i => i.Enumeration.Module.ID == moduleID));
            while(Delete<ObjectClass>(ctx(), doSubmit, c => c.GetQuery<ObjectClass>().Where(i => i.Module.ID == moduleID).Where(i => i.SubClasses.Count == 0)) > 0);
            Delete<DataType>(ctx(), doSubmit, c => c.GetQuery<DataType>().Where(i => i.Module.ID == moduleID), obj => { obj.ImplementsInterfaces.Clear(); });

            // Sequences
            Delete<Sequence>(ctx(), doSubmit, c => c.GetQuery<Sequence>().Where(i => i.Module != null && i.Module.ID == moduleID));

            var localCtx = ctx();
            var icons = localCtx.GetQuery<Icon>().Where(i => i.Module.ID == moduleID).ToList();
            Delete<Icon>(localCtx, doSubmit, c => icons.Select(i => i.Blob));
            Delete<Icon>(localCtx, doSubmit, c => icons);

            Delete<ViewModelDescriptor>(ctx(), doSubmit, c => c.GetQuery<ViewModelDescriptor>().Where(i => i.Module.ID == moduleID));
            Delete<ViewDescriptor>(ctx(), doSubmit, c => c.GetQuery<ViewDescriptor>().Where(i => i.Module.ID == moduleID));

            Delete<ControlKind>(ctx(), doSubmit, c => c.GetQuery<ControlKind>().Where(i => i.Module.ID == moduleID));
            Delete<Presentable_displayedBy_SecondaryControlKinds_RelationEntry>(ctx(), doSubmit, c => c.Internals().GetPersistenceObjectQuery<Presentable_displayedBy_SecondaryControlKinds_RelationEntry>().Where(i => i.A.Module.ID == moduleID));

            Delete<Assembly>(ctx(), doSubmit, c => c.GetQuery<Assembly>().Where(i => i.Module.ID == moduleID));

            // Finally
            Delete<Module>(ctx(), doSubmit, c => c.GetQuery<Module>().Where(i => i.ID == moduleID));
        }

        private static int Delete<T>(IZetboxContext ctx, Action<IZetboxContext> doSubmit, Func<IZetboxContext, IEnumerable> lst, Action<T> preDelete = null)
        {
            int counter = 0;
            foreach (IPersistenceObject obj in lst(ctx))
            {
                if (preDelete != null)
                {
                    preDelete((T)obj);
                }
                ctx.Delete(obj);
                counter++;
            }

            doSubmit(ctx);

            return counter;
        }

        /// <summary>
        /// Loads a database from the specified filename into the specified context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="filenames"></param>
        public static IEnumerable<IPersistenceObject> LoadFromXml(IZetboxContext ctx, params string[] filenames)
        {
            var packages = new List<FileSystemPackageProvider>();
            try
            {
                foreach (var f in filenames)
                {
                    packages.Add(new FileSystemPackageProvider(f, BasePackageProvider.Modes.Read));
                }
                return LoadFromXml(ctx, packages.ToArray());
            }
            finally
            {
                foreach (var p in packages)
                {
                    p.Dispose();
                }
            }
        }

        public static IEnumerable<IPersistenceObject> LoadFromXml(IZetboxContext ctx, Stream stream, string streamDescription)
        {
            using (var s = new StreamPackageProvider(stream, BasePackageProvider.Modes.Read, streamDescription))
            {
                return LoadFromXml(ctx, s);
            }
        }

        /// <summary>
        /// Loads a database from the specified stream into the specified context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="providers">a stream containing a database.xml</param>
        public static IEnumerable<IPersistenceObject> LoadFromXml(IZetboxContext ctx, params IPackageProvider[] providers)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (providers == null) { throw new ArgumentNullException("providers"); }

            using (Log.InfoTraceMethodCall("LoadFromXml"))
            {
                Log.DebugFormat("Starting Import from {0}", string.Join(", ", providers.Select(p => p.ToString()).ToArray()));
                ctx.TransientState["__IS_CURRENTLY_IMPORTING__"] = true;
                try
                {
                    using (Log.DebugTraceMethodCall("initialisation query"))
                    {
                        // the entity framework needs to be initialised by executing any "plain" query first
                        // TODO: repair the ef provider so it doesn't need this special casing.
                        var testObj = ctx.GetQuery<Zetbox.App.Base.ObjectClass>().FirstOrDefault();
                        Log.DebugFormat("query result: [{0}]", testObj);
                    }
                }
                catch (Exception ex)
                {
                    Log.Warn("Error while initialising, trying to proceed anyways", ex);
                }

                Dictionary<Guid, IPersistenceObject> importedObjects = new Dictionary<Guid, IPersistenceObject>();
                Dictionary<Type, List<Guid>> guids = LoadGuids(ctx, providers);

                PreFetchObjects(ctx, importedObjects, guids);

                using (Log.DebugTraceMethodCall("Loading"))
                {
                    providers.ForEach(p => p.RewindData());
                    Log.Debug("Loading");
                    foreach (var p in providers)
                    {
                        var reader = p.Reader;

                        // Find Root Element
                        while (reader.Read() && reader.NodeType != XmlNodeType.Element && reader.LocalName != "ZetboxPackaging" && reader.NamespaceURI != "http://dasz.at/Zetbox") ;

                        // Read content
                        while (reader.Read())
                        {
                            if (reader.NodeType != XmlNodeType.Element) continue;
                            ImportElement(ctx, importedObjects, p);
                        }
                    }
                }

                using (Log.DebugTraceMethodCall("Reloading References"))
                {
                    foreach (var obj in importedObjects.Values)
                    {
                        obj.ReloadReferences();
                    }
                }

                using (Log.DebugTraceMethodCall("Playback Notifications"))
                {
                    foreach (var obj in importedObjects.Values)
                    {
                        ((BaseNotifyingObject)obj).PlaybackNotifications();
                    }
                }
                ctx.TransientState.Remove("__IS_CURRENTLY_IMPORTING__");
                Log.Debug("Import finished");
                return importedObjects.Values;
            }
        }
        #endregion

        #region Implementation
        private static void PreFetchObjects(IZetboxContext ctx, Dictionary<Guid, IPersistenceObject> objects, Dictionary<Type, List<Guid>> guids)
        {
            Log.Debug("Prefetching Objects");
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

        private static Dictionary<Type, List<Guid>> LoadGuids(IZetboxContext ctx, IPackageProvider[] providers)
        {
            Log.Debug("Loading Export Guids");

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
                            if (string.IsNullOrWhiteSpace(ifTypeName))
                            {
                                continue;
                            }
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
            nsmgr.AddNamespace("ZetboxBase", "Zetbox.App.Base");
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator it = nav.Select("//ZetboxBase:Module/ZetboxBase:Name", nsmgr);

            while (it.MoveNext())
            {
                namespaces.Add(it.Current.Value);
            }

            return namespaces;
        }

        private static IPersistenceObject ImportElement(IZetboxContext ctx, Dictionary<Guid, IPersistenceObject> objects, IPackageProvider s)
        {
            Guid exportGuid = s.Reader.GetAttribute("ExportGuid").TryParseGuidValue();
            if (exportGuid != Guid.Empty)
            {
                string ifTypeName = string.Format("{0}.{1}", s.Reader.NamespaceURI, s.Reader.LocalName);
                ifTypeName = MigrateTypeNameMapping(ifTypeName);
                if (string.IsNullOrWhiteSpace(ifTypeName))
                {
                    return null;
                }
                InterfaceType ifType = ctx.GetInterfaceType(ifTypeName);
                if (ifType.Type == null)
                {
                    Log.WarnOnce(string.Format("Type {0} not found", ifTypeName));
                    return null;
                }

                IPersistenceObject obj = FindObject(ctx, objects, exportGuid, ifType);
                ((BaseNotifyingObject)obj).RecordNotifications();

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
                        blob.StoragePath = ctx.Internals()
                            .StoreBlobStream(stream, blob.ExportGuid, blob.CreatedOn, blob.OriginalName)
                            .ToUniversalPath();
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
            switch (ifTypeName)
            {
                case "Zetbox.App.Base.ServiceDescriptor":
                case "Zetbox.App.Base.TypeRef":
                case "Zetbox.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntry":
                case "Zetbox.App.GUI.ViewDescriptor_supports_TypeRef_RelationEntry":
                    return string.Empty;
                case "Zetbox.App.Base.ObjectClass_implements_Interface_RelationEntry":
                case "Zetbox.App.Base.DataType_implements_Interface_RelationEntry":
                    return "Zetbox.App.Base.DataType_implements_ImplementedInterfaces_RelationEntry";
                case "Zetbox.App.Base.IndexConstraint_ensures_unique_on_Property_RelationEntry":
                    return "Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntry";
                case "Zetbox.App.Base.RoleMembership_resolves_Relation_RelationEntry":
                    return "Zetbox.App.Base.RoleMembership_resolves_Relations_RelationEntry";

                case "Zetbox.App.GUI.NavigationEntry_accessed_by_Group_RelationEntry":
                    return "Zetbox.App.GUI.NavigationScreen_accessed_by_Groups_RelationEntry";
                case "Zetbox.App.GUI.ViewModelDescriptor_displayedBy_ControlKind_RelationEntry":
                    return "Zetbox.App.GUI.Presentable_displayedBy_SecondaryControlKinds_RelationEntry";
                case "Zetbox.App.GUI.ObjectReferenceProperty_shows_Method_RelationEntry":
                    return "Zetbox.App.GUI.ObjRefProp_shows_Methods_RelationEntry";
                default:
                    return ifTypeName;
            }
        }

        private static IPersistenceObject FindObject(IZetboxContext ctx, Dictionary<Guid, IPersistenceObject> objects, Guid exportGuid, InterfaceType ifType)
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
                ((Zetbox.App.Base.IExportable)obj).ExportGuid = exportGuid;
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
