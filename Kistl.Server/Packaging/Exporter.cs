
namespace Kistl.Server.Packaging
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
            using (FileStream fs = File.OpenWrite(filename))
            {
                fs.SetLength(0);
                PublishFromContext(ctx, fs, moduleNamespaces);
            }
        }

        public static void PublishFromContext(IKistlContext ctx, Stream s, string[] moduleNamespaces)
        {
            using (Log.DebugTraceMethodCall())
            {
                Log.InfoFormat("Starting Publish for Modules {0}", string.Join(", ", moduleNamespaces));
                using (XmlWriter xml = XmlTextWriter.Create(s, new XmlWriterSettings() { Indent = true, CloseOutput = false, Encoding = Encoding.UTF8 }))
                {
                    Log.Debug("Loading modulelist");
                    var moduleList = GetModules(ctx, moduleNamespaces);
                    WriteStartDocument(xml, ctx, new Kistl.App.Base.Module[] 
                        { 
                            ctx.GetQuery<Kistl.App.Base.Module>().First(m => m.Namespace == "Kistl.App.Base"),
                            ctx.GetQuery<Kistl.App.Base.Module>().First(m => m.Namespace == "Kistl.App.GUI"),
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
                            ExportObject(xml, obj, propNamespaces);

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
                }
                Log.Info("Export finished");
            }
        }

        public static void ExportFromContext(IKistlContext ctx, string filename, string[] moduleNamespaces)
        {
            using (FileStream fs = File.OpenWrite(filename))
            {
                fs.SetLength(0);
                ExportFromContext(ctx, fs, moduleNamespaces);
            }
        }

        public static void ExportFromContext(IKistlContext ctx, Stream s, string[] moduleNamespaces)
        {
            using (Log.DebugTraceMethodCall())
            {
                Log.InfoFormat("Starting Export for Modules {0}", string.Join(", ", moduleNamespaces));
                using (XmlWriter xml = XmlTextWriter.Create(s, new XmlWriterSettings() { Indent = true, CloseOutput = false, Encoding = Encoding.UTF8 }))
                {
                    var moduleList = GetModules(ctx, moduleNamespaces);
                    WriteStartDocument(xml, ctx, moduleList);

                    var iexpIf = ctx.GetIExportableInterface();
                    foreach (var module in moduleList)
                    {
                        Log.InfoFormat("  exporting {0}", module.Name);
                        foreach (var objClass in module.DataTypes.OfType<ObjectClass>().Where(o => o.ImplementsInterfaces.Contains(iexpIf)))
                        {
                            Log.InfoFormat("    {0} ", objClass.Name);
                            foreach (var obj in ctx.GetQuery(objClass.GetDescribedInterfaceType()).OrderBy(obj => ((IExportable)obj).ExportGuid))
                            {
                                ExportObject(xml, obj, moduleNamespaces);
                            }
                        }

                        int moduleID = module.ID; // Dont ask
                        foreach (var rel in ctx.GetQuery<Relation>().Where(r => r.Module.ID == moduleID))
                        {
                            if (rel.GetRelationType() != RelationType.n_m) continue;
                            if (!rel.A.Type.ImplementsIExportable()) continue;
                            if (!rel.B.Type.ImplementsIExportable()) continue;

                            try
                            {
                                var ifType = rel.GetEntryInterfaceType();
                                Log.InfoFormat("    {0} ", ifType.Type.Name);

                                MethodInfo mi = ctx.GetType().FindGenericMethod("FetchRelation", new Type[] { ifType.Type }, new Type[] { typeof(Guid), typeof(RelationEndRole), typeof(IDataObject) });
                                var relations = MagicCollectionFactory.WrapAsCollection<IPersistenceObject>(mi.Invoke(ctx, new object[] { rel.ExportGuid, RelationEndRole.A, null }));

                                foreach (var obj in relations.OrderBy(obj => ((IExportable)obj).ExportGuid))
                                {
                                    ExportObject(xml, obj, moduleNamespaces);
                                }
                            }
                            catch (TypeLoadException ex)
                            {
                                var message = String.Format("Failed to load InterfaceType for entries of {0}", rel);
                                Log.Warn(message, ex);
                            }
                        }
                    }
                    xml.WriteEndElement();
                    xml.WriteEndDocument();
                }
                Log.Info("Export finished");
            }
        }

        #region Xml/Export private Methods
        private static void ExportObject(XmlWriter xml, IPersistenceObject obj, string[] propNamespaces)
        {
            Type t = obj.ReadOnlyContext.GetInterfaceType(obj).Type;
            xml.WriteStartElement(t.Name, t.Namespace);
            if (((IExportable)obj).ExportGuid == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format("At least one object of type {0} has an empty ExportGuid", t.FullName));
            }
            ((IExportableInternal)obj).Export(xml, propNamespaces);
            xml.WriteEndElement();
        }

        private static void WriteStartDocument(XmlWriter xml, IKistlContext ctx, IEnumerable<Kistl.App.Base.Module> moduleList)
        {
            xml.WriteStartDocument();
            xml.WriteStartElement("KistlPackaging", "http://dasz.at/Kistl");
            foreach (var module in moduleList)
            {
                xml.WriteAttributeString("xmlns", module.Name, null, module.Namespace);
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

            xml.WriteAttributeString("date", XmlConvert.ToString(lastChanged ?? DateTime.Now, XmlDateTimeSerializationMode.Utc));
        }

        private static List<Kistl.App.Base.Module> GetModules(IKistlContext ctx, string[] moduleNamespaces)
        {
            var moduleList = new List<Kistl.App.Base.Module>();
            if (moduleNamespaces.Contains("*"))
            {
                moduleList.AddRange(ctx.GetQuery<Kistl.App.Base.Module>().OrderBy(m => m.Namespace));
            }
            else
            {
                foreach (var ns in moduleNamespaces)
                {
                    var module = ctx.GetQuery<Kistl.App.Base.Module>().Where(m => m.Namespace == ns).FirstOrDefault();
                    if (module == null)
                    {
                        Log.WarnFormat("Module {0} not found", ns);
                        continue;
                    }
                    moduleList.Add(module);
                }
            }
            return moduleList;
        }
        #endregion
    }
}
