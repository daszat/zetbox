
namespace Kistl.App.Packaging
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public class Exporter
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Exporter");

        public static void PublishFromContext(IKistlContext ctx, string filename, string[] moduleNamespaces)
        {
            using (var s = new FileSystemPackageProvider(filename, BasePackageProvider.Modes.Write))
            {
                PublishFromContext(ctx, s, moduleNamespaces);
            }
        }

        public static void PublishFromContext(IKistlContext ctx, Stream stream, string[] moduleNamespaces)
        {
            using (var s = new StreamPackageProvider(stream, BasePackageProvider.Modes.Write))
            {
                PublishFromContext(ctx, s, moduleNamespaces);
            }
        }

        public static void PublishFromContext(IKistlContext ctx, IPackageProvider s, string[] moduleNamespaces)
        {
            using (Log.DebugTraceMethodCall())
            {
                Log.InfoFormat("Starting Publish for Modules {0}", string.Join(", ", moduleNamespaces));
                Log.Debug("Loading modulelist");
                var moduleList = GetModules(ctx, moduleNamespaces);
                WriteStartDocument(s, ctx, new Kistl.App.Base.Module[] 
                        { 
                            ctx.GetQuery<Kistl.App.Base.Module>().First(m => m.Name == "KistlBase"),
                            ctx.GetQuery<Kistl.App.Base.Module>().First(m => m.Name == "GUI"),
                        });

                var propNamespaces = new string[] { "Kistl.App.Base", "Kistl.App.GUI" };

                foreach (var module in moduleList)
                {
                    Log.DebugFormat("Publishing objects for module {0}", module.Name);
                    var objects = PackagingHelper.GetMetaObjects(ctx, module);

                    Stopwatch watch = new Stopwatch();
                    watch.Start();

                    int counter = 0;
                    foreach (var obj in objects)
                    {
                        ExportObject(s, obj, propNamespaces);

                        counter++;
                        if (watch.ElapsedMilliseconds > 1000)
                        {
                            watch.Reset();
                            watch.Start();
                            Log.DebugFormat("{0:n0}% finished", (double)counter / (double)objects.Count * 100.0);
                        }
                    }
                    Log.Debug("100% finished");
                }
                Log.Info("Export finished");
            }
        }

        public static void ExportFromContext(IKistlContext ctx, string filename, string[] moduleNames)
        {
            using (var s = new FileSystemPackageProvider(filename, BasePackageProvider.Modes.Write))
            {
                ExportFromContext(ctx, s, moduleNames);
            }
        }

        public static void ExportFromContext(IKistlContext ctx, Stream stream, string[] moduleNames)
        {
            using (var s = new StreamPackageProvider(stream, BasePackageProvider.Modes.Write))
            {
                ExportFromContext(ctx, s, moduleNames);
            }
        }

        public static void ExportFromContext(IKistlContext ctx, IPackageProvider s, string[] moduleNames)
        {
            using (Log.DebugTraceMethodCall())
            {
                Log.InfoFormat("Starting Export for Modules {0}", string.Join(", ", moduleNames));
                var moduleList = GetModules(ctx, moduleNames);
                var moduleNamespaces = moduleList.Select(m => m.Namespace).ToArray();

                WriteStartDocument(s, ctx, moduleList);

                var iexpIf = ctx.GetIExportableInterface();
                foreach (var module in moduleList)
                {
                    Log.InfoFormat("  exporting {0}", module.Name);
                    foreach (var objClass in ctx.GetQuery<ObjectClass>().Where(o => o.Module == module).ToList().Where(o => o.ImplementsInterfaces.Contains(iexpIf)).OrderBy(o => o.Name))
                    {
                        Log.InfoFormat("    {0} ", objClass.Name);
                        foreach (var obj in ctx.GetQuery(objClass.GetDescribedInterfaceType()).OrderBy(obj => ((IExportable)obj).ExportGuid))
                        {
                            ExportObject(s, obj, moduleNamespaces);
                        }
                    }

                    int moduleID = module.ID; // Dont ask
                    foreach (var rel in ctx.GetQuery<Relation>().Where(r => r.Module.ID == moduleID)
                        .OrderBy(r => r.A.Type.Name).ThenBy(r => r.A.RoleName).ThenBy(r => r.B.Type.Name).ThenBy(r => r.B.RoleName).ThenBy(r => r.ExportGuid))
                    {
                        if (rel.GetRelationType() != RelationType.n_m)
                            continue;
                        if (!rel.A.Type.ImplementsIExportable())
                            continue;
                        if (!rel.B.Type.ImplementsIExportable())
                            continue;

                        try
                        {
                            var ifType = rel.GetEntryInterfaceType();
                            Log.InfoFormat("    {0} ", ifType.Type.Name);

                            MethodInfo mi = ctx.GetType().FindGenericMethod("FetchRelation", new Type[] { ifType.Type }, new Type[] { typeof(Guid), typeof(RelationEndRole), typeof(IDataObject) });
                            var relations = MagicCollectionFactory.WrapAsCollection<IPersistenceObject>(mi.Invoke(ctx, new object[] { rel.ExportGuid, RelationEndRole.A, null }));

                            foreach (var obj in relations.OrderBy(obj => ((IExportable)obj).ExportGuid))
                            {
                                ExportObject(s, obj, moduleNamespaces);
                            }
                        }
                        catch (TypeLoadException ex)
                        {
                            var message = String.Format("Failed to load InterfaceType for entries of {0}", rel);
                            Log.Warn(message, ex);
                        }
                    }
                }
                s.Writer.WriteEndElement();
                s.Writer.WriteEndDocument();

                Log.Info("Export finished");
            }
        }

        #region Xml/Export private Methods
        private static void ExportObject(IPackageProvider s, IPersistenceObject obj, string[] propNamespaces)
        {
            Type t = obj.ReadOnlyContext.GetInterfaceType(obj).Type;
            s.Writer.WriteStartElement(t.Name, t.Namespace);
            if (((IExportable)obj).ExportGuid == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format("At least one object of type {0} has an empty ExportGuid", t.FullName));
            }
            ((IExportableInternal)obj).Export(s.Writer, propNamespaces);

            if (obj is Blob && s.SupportsBlobs)
            {
                var blob = (Blob)obj;
                s.PutBlob(blob.ExportGuid, blob.OriginalName, blob.GetStream());
            }
            s.Writer.WriteEndElement();
        }

        private static void WriteStartDocument(IPackageProvider s, IKistlContext ctx, IEnumerable<Kistl.App.Base.Module> moduleList)
        {
            s.Writer.WriteStartDocument();
            if (moduleList.Count() == 1)
            {
                // use exported module as default namespace
                s.Writer.WriteStartElement("KistlPackaging", "http://dasz.at/Kistl");
                foreach (var module in moduleList)
                {
                    s.Writer.WriteAttributeString("xmlns", module.Name, null, module.Namespace);
                }
            }
            else
            {
                s.Writer.WriteStartElement("KistlPackaging", "http://dasz.at/Kistl");
                foreach (var module in moduleList)
                {
                    s.Writer.WriteAttributeString("xmlns", module.Name, null, module.Namespace);
                }
            }

            DateTime? lastChanged = new DateTime?[] { 
                ctx.GetQuery<Kistl.App.Base.Assembly>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.BaseParameter>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.Constraint>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.DataType>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.DefaultPropertyValue>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.EnumerationEntry>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.Method>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.MethodInvocation>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.Module>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.Property>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.PropertyInvocation>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.Relation>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.RelationEnd>().Max(d => d.ChangedOn),
                ctx.GetQuery<Kistl.App.Base.TypeRef>().Max(d => d.ChangedOn)
            }.Max();

            s.Writer.WriteAttributeString("date", XmlConvert.ToString(lastChanged ?? DateTime.Now, XmlDateTimeSerializationMode.Utc));
        }

        private static List<Kistl.App.Base.Module> GetModules(IKistlContext ctx, string[] moduleNames)
        {
            var moduleList = new List<Kistl.App.Base.Module>();
            if (moduleNames.Contains("*"))
            {
                moduleList.AddRange(ctx.GetQuery<Kistl.App.Base.Module>());
            }
            else
            {
                foreach (var name in moduleNames)
                {
                    var module = ctx.GetQuery<Kistl.App.Base.Module>().Where(m => m.Name == name).FirstOrDefault();
                    if (module == null)
                    {
                        Log.WarnFormat("Module {0} not found", name);
                        continue;
                    }
                    moduleList.Add(module);
                }
            }
            return moduleList.OrderBy(m => m.Name).ToList();
        }
        #endregion
    }
}
